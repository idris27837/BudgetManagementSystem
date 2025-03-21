using PMS.Models;
using BudgetManagementSystem.Models.AbstractModel;
using PMS.Models.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManagementSystem.DataAccessLayer.Context;
using Microsoft.IdentityModel.Tokens;
using BudgetManagementSystem.ViewModels.PMSVms;
using Hangfire.PostgreSql.Utils;
using System.Data;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System.IO;
using BudgetManagementSystem.Models.PerformanceMgt;

namespace BudgetManagementSystem.BusinessLogic.Concretes
{
    public abstract class BaseService
    {
        private readonly IPMSRepo<SequenceNumber> _sequenceNumberRepo;
        private readonly CompetencyCoreDbContext _dbContext;
        private readonly IUserDbContext _user_context;


        internal BaseService(IPMSRepo<SequenceNumber> sequenceNumberRepo, CompetencyCoreDbContext context, IUserDbContext user_context)
        {
            _sequenceNumberRepo = sequenceNumberRepo;
            _dbContext = context;
            _user_context = user_context;

        }


       public  async Task<string> GenerateCode(SequenceNumberTypes type, long lenght = 3, string concatStr = "", ConCat pos = ConCat.Before)
        {
            var nextNumber = await GetNextNumber(type);

            switch (pos)
            {
                case ConCat.After:

                    return $"{padWithZeros(nextNumber.ToString(), lenght)}{concatStr}";


                default:

                    return $"{concatStr}{padWithZeros(nextNumber.ToString(), lenght)}";
            }

        }


        protected async Task<long> GetNextNumber(SequenceNumberTypes type)
        {
            long nextNumber = 1;
            var sequence = (from n in _sequenceNumberRepo.Table
                            where n.SequenceNumberType == type
                            select n).FirstOrDefault();
            if (sequence == null)
            {

                sequence = new SequenceNumber();
                sequence.Description = Enum.GetName(typeof(SequenceNumberTypes), type);
                sequence.UsePrefix = false;
                sequence.SequenceNumberType = type;
                sequence.NextNumber = nextNumber + 1;
               await _sequenceNumberRepo.Insert(sequence);
            }
            else
            {
                nextNumber = sequence.NextNumber;
                sequence.NextNumber += 1;
               await _sequenceNumberRepo.Update(sequence);
            }
            return nextNumber;
        }


        protected CompetencyCoreDbContext getDbContext()
        {
            return _dbContext;
        }


        protected string padWithZeros(string field, long maxlenght)
        {
            var lenght = field.Length;
            if (lenght == maxlenght)
            {
                return field;
            }
            if (lenght > maxlenght)
            {
                throw new Exception("input string cannot be greater in lenght than the specified maximum lenght");
            }

            var padding = new StringBuilder();
            var zerosToAdd = maxlenght - lenght;
            for (int i = 0; i < zerosToAdd; i++)
            {
                padding.Append("0");
            }
            padding.Append(field);
            return padding.ToString();
        }



        protected async Task Save<T>(T entity) where T : BaseEntity
        {

            var repo = new PerformanceRepo<T>(_dbContext, _user_context);


            if (entity.Id == 0)
            {
                await repo.Insert(entity);
            }
            else
            {
                await repo.Update(entity);
            }

        }




        protected async Task Save<T>(IEnumerable<T> entities) where T : BaseEntity
        {

            var repo = new PerformanceRepo<T>(_dbContext, _user_context);
            foreach( var entity in entities)
            {
                await Save(entity);
            }
                

        }


        protected async Task<dynamic> ApproveOrReject<T, A>(T entity, A approval, Func<T, Task<dynamic>> ApproveFunction = null) where T : BaseWorkFlow where A : ApprovalBase
        {

            var repo = new PerformanceRepo<T>(_dbContext, _user_context);

            switch (approval.approval)
            {
                case Approval.Approved:
                    entity.Status = Enum.GetName(Status.ApprovedAndActive);
                    entity.RecordStatus = Status.Active;
                    entity.DateApproved = DateTime.Now;
                    entity.IsApproved = true;
                    entity.IsActive = true;
                    entity.ApprovedBy = _user_context.UserId.IsNullOrEmpty() ? "SYSTEM" : _user_context.UserId;
                    await repo.Update(entity);
                    if (ApproveFunction is not null)
                    {
                        try
                        {
                            return await ApproveFunction?.Invoke(entity);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    break;
                case Approval.Rejected:
                    entity.Status = Enum.GetName(Status.Rejected);
                    entity.RecordStatus = Status.Rejected;
                    entity.DateRejected = DateTime.Now;
                    entity.IsRejected = true;
                    entity.RejectionReason = (approval as RejectionRequestVm).RejectionReason;
                    entity.IsActive = false;
                    entity.RejectedBy = _user_context.UserId.IsNullOrEmpty() ? "SYSTEM" : _user_context.UserId;
                    await repo.Update(entity);
                    break;
                default:
                    throw new Exception("Invalid status");
            }

            return true;

        }

        //protected DateTime GetStartOrEndDate(int year, int value, ReviewPeriodRange range  = ReviewPeriodRange.Quarterly, bool IsStart = true)
        //{

        //    int multiply = 0;

        //    if (value == 0) value = 1;

        //    switch(range)
        //    {
        //        case ReviewPeriodRange.Quarterly:
        //            if (value > 4) throw new ArgumentException("Invalid Quarter Value");
        //            multiply = 3;


        //            break;

        //        case ReviewPeriodRange.BiAnually:
        //            if (value > 2) throw new ArgumentException("Invalid Bi-Annual Value");
        //            multiply = 6;

        //            break;

        //        case ReviewPeriodRange.Annually:
        //            if (value > 1) throw new ArgumentException("Invalid Annual Value");
        //            multiply = 12;

        //            break;


        //    }

        //    int month = value * multiply;

        //    if (IsStart) month -= (multiply-1);



        //    int day = IsStart ? 1 : DateTime.DaysInMonth(year, month);
        //    return new DateTime(year, month, day);
        //}

        protected DateTime GetStartOrEndDate(int year, int value, ReviewPeriodRange range = ReviewPeriodRange.Quarterly, bool IsStart = true)
        {
            if (value == 0) value = 1;  // Default to 1 if no value provided

            int month = 0;

            switch (range)
            {
                case ReviewPeriodRange.Quarterly:
                    if (value > 4) throw new ArgumentException("Invalid Quarter Value");
                    month = (value - 1) * 3 + 1;  // Calculate the starting month for the quarter
                    break;

                case ReviewPeriodRange.BiAnually:
                    if (value > 2) throw new ArgumentException("Invalid Bi-Annual Value");
                    month = (value - 1) * 6 + 1;  // Starting month for bi-annual period
                    break;

                case ReviewPeriodRange.Annually:
                    if (value > 1) throw new ArgumentException("Invalid Annual Value");
                    month = 1;  // Always starts in January
                    break;

                default:
                    throw new ArgumentException("Invalid Range");
            }

            if (!IsStart)
            {
                month += 2;  // Move to the last month of the quarter
            }

            // Determine the day: 1 for start, last day of the month for end
            int day = IsStart ? 1 : DateTime.DaysInMonth(year, month);

            return new DateTime(year, month, day);
        }


        protected async Task<DataSet> GetExcelFileData(IFormFile file)
        {


            BufferedStream m_file = null;
            try
            {
                IExcelDataReader reader;


                using var memoryStream = new MemoryStream();

                await file.CopyToAsync(memoryStream);


                m_file = new BufferedStream(memoryStream);




                bool isxls = file.FileName.ToLower().EndsWith("xls");

                try
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    reader = isxls ? ExcelReaderFactory.CreateBinaryReader(m_file) : ExcelReaderFactory.CreateReader(m_file);
                }
                catch (Exception s)
                {

                    m_file.Close();
                    m_file.Dispose();
                    throw new Exception("Could read file :" + file.FileName);

                }




                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };




                var dataSet = reader.AsDataSet(conf);

                foreach (DataColumn col in dataSet.Tables[0].Columns)
                {
                    col.ColumnName = col.ColumnName.Trim().Replace(" ", "_").Replace("/", "");
                }




                m_file.Close();
                return dataSet;


            }
            catch (Exception r)
            {
                try
                {
                    m_file.Close();
                }
                catch
                {
                }

                throw;

            }
        }

    }

}
