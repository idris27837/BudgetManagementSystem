using Azure.Core;
using Azure.Identity;
using BudgetManagementSystem.BusinessLogic.Abstractions;
using BudgetManagementSystem.DataAccessLayer.Context;
using BudgetManagementSystem.ViewModels.PMSVms;
using BudgetManagementSystem.ViewModels.PMSVms.SetupVm;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using PMS.Models;
using PMS.Models.CoreModels;
using PMS.Models.PerformanceMgt.CoreModels;
using System.Collections;
using System.Data;

namespace BudgetManagementSystem.BusinessLogic.Handlers.PerformanceMgtHandlers
{
    public class SMDService : BaseService, ISMDService
    {
        private readonly IPMSRepo<Strategy> _strategyRepo;
        private readonly IPMSRepo<EnterpriseObjective> _enterpriseObjectiveRepo;
        private readonly IPMSRepo<DivisionObjective> _divisionObjectiveRepo;
        private readonly IPMSRepo<DepartmentObjective> _departmentObjectiveRepo;
        private readonly IPMSRepo<OfficeObjective> _officeObjectiveRepo;
        private readonly IPMSRepo<ObjectiveCategory> _objectiveCategoryRepo;
        private readonly IPMSRepo<CategoryDefinition> _categoryDefRepo;
        private readonly IRepo<Department> _deptRepo;
        private readonly IRepo<Division> _divRepo;
        private readonly IRepo<Office> _officeRepo;

        public SMDService(CompetencyCoreDbContext context, IUserDbContext userDbContext, IPMSRepo<SequenceNumber> sequenceRepo,
            IPMSRepo<Strategy> strategyRepo,
            IPMSRepo<EnterpriseObjective> enterpriseObjectiveRepo,
            IPMSRepo<DivisionObjective> divisionObjectiveRepo,
            IPMSRepo<DepartmentObjective> departmentObjectiveRepo,
             IPMSRepo<OfficeObjective> officeObjectiveRepo,
             IPMSRepo<ObjectiveCategory> objectiveCategoryRepo,
             IPMSRepo<CategoryDefinition> categoryDefRepo,
             IRepo<Department> deptRepo,
             IRepo<Division> divRepo,
             IRepo<Office> officeRepo
            )
            : base(sequenceRepo, context, userDbContext)
        {
            _strategyRepo = strategyRepo;
            _enterpriseObjectiveRepo = enterpriseObjectiveRepo;
            _divisionObjectiveRepo = divisionObjectiveRepo;
            _officeObjectiveRepo = officeObjectiveRepo;
            _departmentObjectiveRepo = departmentObjectiveRepo;
            _officeObjectiveRepo = officeObjectiveRepo;
            _objectiveCategoryRepo = objectiveCategoryRepo;
            _categoryDefRepo = categoryDefRepo;
            _deptRepo = deptRepo;
            _divRepo = divRepo;
            _officeRepo = officeRepo;
        }


        #region data retrieval
        public async Task<IList<EnterpriseObjective>> GetAllEnterpriseObjectives(Status RecordStatus)
        {


            return await _enterpriseObjectiveRepo.GetRecordsWithSatus(RecordStatus);
        }

        public async Task<IList<DepartmentObjective>> GetAllDepartmentObjectives(Status RecordStatus)
        {
            return await _departmentObjectiveRepo.GetRecordsWithSatus(RecordStatus);
        }

        public async Task<IList<DivisionObjective>> GetAllDivisionObjectives(Status RecordStatus)
        {
            return await _divisionObjectiveRepo.GetRecordsWithSatus(RecordStatus);
        }

        public async Task<IList<OfficeObjective>> GetAllOfficeObjectives(Status RecordStatus)
        {
            return await _officeObjectiveRepo.GetRecordsWithSatus(RecordStatus);
        }

        public async Task<IList<ObjectiveCategory>> GetAllObjectiveCategories(Status RecordStatus)
        {
            return await _objectiveCategoryRepo.GetRecordsWithSatus(RecordStatus);
        }

        public async Task<IList<CategoryDefinition>> GetAllCategoryDefinitions(string categoryId)
        {

            var catDefs = await _categoryDefRepo.GetRecordsWithSatus(Status.ApprovedAndActive);

            return catDefs.Where(c=> c.ObjectiveCategoryId == categoryId).ToList();
        }

        public EnterpriseObjective GetEnterpriseObjective(string id)
        {
            var obj = _enterpriseObjectiveRepo.GetAllIncluding(
                o => o.EnterpriseObjectivesCategory,
                o => o.Strategy
                ).FirstOrDefault(o => o.EnterpriseObjectiveId == id);

            return obj;
        }

        public DivisionObjective GetDivisionObjective(string id)
        {
            var obj = _divisionObjectiveRepo.GetAllIncluding(
                o => o.Division,
                o => o.OfficeObjectives
                ).FirstOrDefault(o => o.DivisionObjectiveId == id);

            return obj;
        }

        public DepartmentObjective GetDepartmentObjective(string id)
        {
            var obj = _departmentObjectiveRepo.GetAllIncluding(
                o => o.Department,
                o => o.DivisionObjectives
                ).FirstOrDefault(o => o.DepartmentObjectiveId == id);

            return obj;
        }



        public OfficeObjective GetOfficeObjective(string id)
        {
            var obj = _officeObjectiveRepo.GetAllIncluding(
                o => o.Office
                ).FirstOrDefault(o => o.OfficeObjectiveId == id);

            return obj;
        }

        public ObjectiveCategory GetObjectiveCategory(string id)
        {
            var obj = _objectiveCategoryRepo.GetAllIncluding(
            
                ).FirstOrDefault(o => o.ObjectiveCategoryId == id);

            return obj;
        }

        public Strategy GetStrategy(string id)
        {
            var strategy = _strategyRepo.GetAllIncluding(
                s => s.EnterpriseObjectives
               ).FirstOrDefault(s => s.StrategyId == id);

            return strategy;
        }


        public CategoryDefinition GetCategoryDefinition(string id)
        {
            var categDef = _categoryDefRepo.GetAllIncluding(

               ).FirstOrDefault(c => c.DefinitionId == id);

            return categDef;
        }

        #endregion




        #region ListVm
        public async Task<GenericListVm> GetStrategies()
        {

            var respone = new GenericListVm();
            try
            {
                var strategyListVm = new List<StrategyVm>();

                var strategies = await _strategyRepo.Table.ToListAsync();

                foreach (var strategy in strategies)
                {

                    var strategyVm = ObjectMapper.Map<StrategyVm>(strategy);

                    strategyListVm.Add(strategyVm);


                }

                respone.TotalRecord = strategyListVm.Count;
                respone.ListData = strategyListVm;


                respone.Message = NotificationMessages.OperationCompleted;
                respone.IsSuccess = true;


            }
            catch (Exception ex)
            {
                respone.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    respone.Errors.Add(ex.InnerException.Message);
                }


                
            }

            return respone;
        }


       public async Task<GenericListVm> GetDepartmentObjectives(string status)
        {

            var respone = new GenericListVm();
            try
            {
                Status recordStatus = (Status)Enum.Parse(typeof(Status), status);
                var departmentObjectiveListVm = new List<DepartmentObjectiveVm>();

                var departmentObjectives = await GetAllDepartmentObjectives(recordStatus);

                foreach (var departmentObjective in departmentObjectives)
                {

                    var departmentObjectivesVm = ObjectMapper.Map<DepartmentObjectiveVm>(departmentObjective);

                    departmentObjectiveListVm.Add(departmentObjectivesVm);


                }

                respone.TotalRecord = departmentObjectiveListVm.Count;
                respone.ListData = departmentObjectiveListVm;
 

                respone.Message = NotificationMessages.OperationCompleted;
                respone.IsSuccess = true;


            }
            catch (Exception ex)
            {
                respone.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    respone.Errors.Add(ex.InnerException.Message);
                }


                
            }

            return respone;
        }

        public async Task<GenericListVm> GetEnterpriseObjectives(string status)
        {
            var response = new GenericListVm();
            try
            {

                Status recordStatus = (Status)Enum.Parse(typeof(Status), status);

                var enterpriseObjectiveListVm = new List<EnterpriseObjectiveVm>();

                var enterpriseObjectives = await GetAllEnterpriseObjectives(recordStatus);

                foreach (var enterpriseObjective in enterpriseObjectives)
                {
                    var enterpriseObjectiveVm = ObjectMapper.Map<EnterpriseObjectiveVm>(enterpriseObjective);
                    enterpriseObjectiveListVm.Add(enterpriseObjectiveVm);
                }

                response.TotalRecord = enterpriseObjectiveListVm.Count;
                response.ListData = enterpriseObjectiveListVm;

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
                
            }

            return response;
        }

        public async Task<GenericListVm> GetDivisionObjectives(string status)
        {
            var response = new GenericListVm();
            try
            {
                Status recordStatus = (Status)Enum.Parse(typeof(Status), status);

                var divisionObjectiveListVm = new List<DivisionObjectiveVm>();

                var divisionObjectives = await GetAllDivisionObjectives(recordStatus);

                foreach (var divisionObjective in divisionObjectives)
                {
                    var divisionObjectiveVm = ObjectMapper.Map<DivisionObjectiveVm>(divisionObjective);
                    divisionObjectiveListVm.Add(divisionObjectiveVm);
                }

                response.TotalRecord = divisionObjectiveListVm.Count;
                response.ListData = divisionObjectiveListVm;

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
            
            }

            return response;
        }


        public async Task<GenericListVm> GetOfficeObjectives(string status)
        {
            var response = new GenericListVm();
            try
            {

                Status recordStatus = (Status)Enum.Parse(typeof(Status), status);

                var officeObjectiveListVm = new List<OfficeObjectiveVm>();

                var officeObjectives = await GetAllOfficeObjectives(recordStatus);

                foreach (var officeObjective in officeObjectives)
                {
                    var officeObjectiveVm = ObjectMapper.Map<OfficeObjectiveVm>(officeObjective);
                    officeObjectiveListVm.Add(officeObjectiveVm);
                }

                response.TotalRecord = officeObjectiveListVm.Count;
                response.ListData = officeObjectiveListVm;

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
             
            }

            return response;
        }

        public async Task<GenericListVm> GetObjectiveCategories(string status)
        {
            var response = new GenericListVm();
            try
            {

                Status recordStatus = (Status)Enum.Parse(typeof(Status), status);

                var objectiveCategoryListVm = new List<ObjectiveCategoryVm>();

                var objectiveCategories = await GetAllObjectiveCategories(recordStatus);

                foreach (var objectiveCategory in objectiveCategories)
                {
                    var objectiveCategoryVm = ObjectMapper.Map<ObjectiveCategoryVm>(objectiveCategory);
                    objectiveCategoryListVm.Add(objectiveCategoryVm);
                }

                response.TotalRecord = objectiveCategoryListVm.Count;
                response.ListData = objectiveCategoryListVm;

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
               
            }

            return response;
        }


        public async Task<GenericListVm> GetCategoryDefinitions(string categoryId)
        {
            var response = new GenericListVm();
            try
            {
               

                var categoryDefinitionListVm = new List<CategoryDefinitionVm>();

                var categoryDefinitions = await GetAllCategoryDefinitions(categoryId);

                foreach (var categoryDefinition in categoryDefinitions)
                {
                    var categoryDefinitionVm = ObjectMapper.Map<CategoryDefinitionVm>(categoryDefinition);
                    categoryDefinitionListVm.Add(categoryDefinitionVm);
                }

                response.TotalRecord = categoryDefinitionListVm.Count;
                response.ListData = categoryDefinitionListVm;

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
            }

            return response;
        }


        #endregion





        #region setup
        public async Task<ResponseVm> StrategySetup(StrategyVm request, OperationTypes operationType = OperationTypes.Add, IFormFile file = null)
        {
            var respone = new ResponseVm();
            try
            {
                Strategy strategy;



                switch (operationType)
                {
                    case OperationTypes.Add:

                        strategy = ObjectMapper.Map<Strategy>(request);

                        strategy.StrategyId = base.GenerateCode(SequenceNumberTypes.Strategies);
                       
                        base.GenerateCode(SequenceNumberTypes.CompetencyReviewers);
                        break;

                    case OperationTypes.Update:


                        strategy = GetStrategy(request.StrategyId);

                        strategy.StartDate = request.StartDate;
                        strategy.EndDate = request.EndDate;
                        strategy.Description = request.Description;
                        strategy.BankYearId = request.BankYearId;
                       

                        break;

                    default:

                        throw new NotImplementedException();

                }

                if (file != null)
                {
                    string base64Image = null;
                    if (file != null && file.ContentDisposition != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            byte[] fileBytes = memoryStream.ToArray();
                            base64Image = Convert.ToBase64String(fileBytes);
                        }
                    }
                    strategy.FileImage = base64Image;
                }

                strategy.Status = Enum.GetName(Status.PendingApproval);
                await base.Save(strategy);

                respone.Message = NotificationMessages.OperationCompleted;
                respone.IsSuccess = true;


            }
            catch (Exception ex)
            {
                respone.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    respone.Errors.Add(ex.InnerException.Message);
                }


             
            }

            return respone;
        }



        public async Task<ResponseVm> DivisionObjectiveSetup(DivisionObjectiveVm request, OperationTypes operationType = OperationTypes.Add)
        {
            var response = new ResponseVm();
            try
            {
                DivisionObjective divisionObjective;

                switch (operationType)
                {
                    case OperationTypes.Add:
                        divisionObjective = ObjectMapper.Map<DivisionObjective>(request);
                        divisionObjective.DivisionObjectiveId = base.GenerateCode(SequenceNumberTypes.DivisionObjectives);
                        break;

                    case OperationTypes.Update:
                        divisionObjective = GetDivisionObjective(request.DivisionObjectiveId);
                        if (divisionObjective == null)
                        {
                            throw new ArgumentException("Division Objective not found");
                        }
                        divisionObjective.Name = request.Name;
                        divisionObjective.Description = request.Description;
                        divisionObjective.Kpi = request.Kpi;
                        divisionObjective.DepartmentObjectiveId = request.DepartmentObjectiveId;
                   
                        break;

                    default:
                        throw new NotImplementedException();
                }


                divisionObjective.Status = Enum.GetName(Status.PendingApproval);
                await base.Save(divisionObjective);

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
            
            }

            return response;
        }


        public async Task<ResponseVm> OfficeObjectiveSetup(OfficeObjectiveVm request, OperationTypes operationType = OperationTypes.Add)
        {
            var response = new ResponseVm();
            try
            {
                OfficeObjective officeObjective;

                switch (operationType)
                {
                    case OperationTypes.Add:
                        officeObjective = ObjectMapper.Map<OfficeObjective>(request);
                        officeObjective.OfficeObjectiveId = base.GenerateCode(SequenceNumberTypes.OfficeObjectives);
                        break;

                    case OperationTypes.Update:
                        officeObjective = GetOfficeObjective(request.OfficeObjectiveId);
                        if (officeObjective == null)
                        {
                            throw new ArgumentException("Office Objective not found");
                        }
                        officeObjective.Name = request.Name;
                        officeObjective.Description = request.Description;
                        officeObjective.Kpi = request.Kpi;
                        officeObjective.JobGradeGroupId = request.JobGradeGroupId;
                        officeObjective.DivisionObjectiveId = request.DivisionObjectiveId;
                        break;

                    default:
                        throw new NotImplementedException();
                }
                officeObjective.Status = Enum.GetName(Status.PendingApproval);
                await base.Save(officeObjective);

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
             
            }

            return response;
        }


        public async Task<ResponseVm> DepartmentObjectiveSetup(DepartmentObjectiveVm request, OperationTypes operationType = OperationTypes.Add)
        {
            var response = new ResponseVm();
            try
            {
                DepartmentObjective departmentObjective;

                switch (operationType)
                {
                    case OperationTypes.Add:
                        departmentObjective = ObjectMapper.Map<DepartmentObjective>(request);
                        departmentObjective.DepartmentObjectiveId = base.GenerateCode(SequenceNumberTypes.DepartmentObjectives);
                        break;

                    case OperationTypes.Update:
                        departmentObjective = GetDepartmentObjective(request.DepartmentObjectiveId);
                        if (departmentObjective == null)
                        {
                            throw new ArgumentException("Department Objective not found");
                        }
                        departmentObjective.Name = request.Name;
                        departmentObjective.Description = request.Description;
                        departmentObjective.Kpi = request.Kpi;
                        departmentObjective.DepartmentId = request.DepartmentId;
                        departmentObjective.EnterpriseObjectiveId = request.EnterpriseObjectiveId;
                        break;

                    default:
                        throw new NotImplementedException();
                }
                departmentObjective.Status = Enum.GetName(Status.PendingApproval);
                await base.Save(departmentObjective);

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
              
            }

            return response;
        }




        public async Task<ResponseVm> EnterpriseObjectiveSetup(EnterpriseObjectiveVm request, OperationTypes operationType = OperationTypes.Add)
        {
            var response = new ResponseVm();
            try
            {
                EnterpriseObjective enterpriseObjective;

                switch (operationType)
                {
                    case OperationTypes.Add:
                        enterpriseObjective = ObjectMapper.Map<EnterpriseObjective>(request);
                        enterpriseObjective.EnterpriseObjectiveId = base.GenerateCode(SequenceNumberTypes.EnterpriseObjectives);
                        break;

                    case OperationTypes.Update:
                        enterpriseObjective = GetEnterpriseObjective(request.EnterpriseObjectiveId);
                        if (enterpriseObjective == null)
                        {
                            throw new ArgumentException("Enterprise Objective not found");
                        }
                        enterpriseObjective.Name = request.Name;
                        enterpriseObjective.Description = request.Description;
                        enterpriseObjective.Kpi = request.Kpi;
                        enterpriseObjective.StrategyId = request.StrategyId;
                        enterpriseObjective.EnterpriseObjectivesCategoryId = request.EnterpriseObjectivesCategoryId;
                        
                        // Add any other properties specific to EnterpriseObjective that need updating
                        break;

                    default:
                        throw new NotImplementedException();
                }
                enterpriseObjective.Status = Enum.GetName(Status.PendingApproval);
                await base.Save(enterpriseObjective);

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
               
            }

            return response;
        }


        public async Task<ResponseVm> ObjectiveCategorySetup(ObjectiveCategoryVm request, OperationTypes operationType = OperationTypes.Add)
        {
            var response = new ResponseVm();
            try
            {
                ObjectiveCategory objectiveCategory;

                switch (operationType)
                {
                    case OperationTypes.Add:
                        objectiveCategory = ObjectMapper.Map<ObjectiveCategory>(request);
                        objectiveCategory.ObjectiveCategoryId = base.GenerateCode(SequenceNumberTypes.ObjectiveCategory);
                        break;

                    case OperationTypes.Update:
                        objectiveCategory = GetObjectiveCategory(request.ObjectiveCategoryId);
                        if (objectiveCategory == null)
                        {
                            throw new ArgumentException("Objective Category not found");
                        }
                        objectiveCategory.Name = request.Name;
                        objectiveCategory.Description = request.Description;
                        // Add any other properties specific to ObjectiveCategory that need updating
                        break;

                    default:
                        throw new NotImplementedException();
                }
                objectiveCategory.Status = Enum.GetName(Status.PendingApproval);
                await base.Save(objectiveCategory);

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
               
            }

            return response;
        }

        public async Task<ResponseVm> SetupCategoryDefinition(CategoryDefinitionVm request, OperationTypes operationType = OperationTypes.Add)
        {
            var response = new ResponseVm();
            try
            {
                CategoryDefinition categoryDefinition;

                switch (operationType)
                {
                    case OperationTypes.Add:
                        categoryDefinition = ObjectMapper.Map<CategoryDefinition>(request);
                        categoryDefinition.DefinitionId = base.GenerateCode(SequenceNumberTypes.CategoryDefinitions);
                        break;

                    case OperationTypes.Update:
                        categoryDefinition = GetCategoryDefinition(request.DefinitionId);
                        if (categoryDefinition == null)
                        {
                            throw new ArgumentException("Category Definition not found");
                        }

                        categoryDefinition.Description = request.Description;
                        categoryDefinition.EnforceWorkProductLimit = request.EnforceWorkProductLimit;
                        categoryDefinition.MaxNoWorkProduct = request.MaxNoWorkProduct;
                        categoryDefinition.GradeGroupId = request.GradeGroupId;
                        categoryDefinition.MaxNoObjectives = request.MaxNoObjectives;
                        categoryDefinition.Weight = request.Weight;
                        categoryDefinition.ObjectiveCategoryId = request.ObjectiveCategoryId;
                        categoryDefinition.IsCompulsory = request.IsCompulsory;
                        break;

                    default:
                        throw new NotImplementedException();
                }
                await base.Save(categoryDefinition);

                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
            }

            return response;
        }





        #endregion


        #region Approval

        public async Task<ResponseVm> AprroveOrRejectRecord<T>(T approvalRequest) where T : ApprovalBase
        {
            var response = new ResponseVm();
            try
            {
               dynamic record;

                foreach (var RecordId in approvalRequest.RecordIds)
                {



                    switch (approvalRequest.EntityType)
                    {
                        case nameof(EnterpriseObjective):
                            record = GetEnterpriseObjective(RecordId);
                            if (record == null)
                            {
                                throw new ArgumentException($"{approvalRequest.EntityType}  not found");
                            }
                            await base.ApproveOrReject(record as EnterpriseObjective, approvalRequest);
                            break;

                        case nameof(DepartmentObjective):
                            record = GetDepartmentObjective(RecordId);
                            if (record == null)
                            {
                                throw new ArgumentException($"{approvalRequest.EntityType}  not found");
                            }
                            await base.ApproveOrReject(record as DepartmentObjective, approvalRequest);
                            break;


                        case nameof(DivisionObjective):
                            record = GetDivisionObjective(RecordId);
                            if (record == null)
                            {
                                throw new ArgumentException($"{approvalRequest.EntityType}  not found");
                            }
                            await base.ApproveOrReject(record as DivisionObjective, approvalRequest);
                            break;


                        case nameof(OfficeObjective):
                            record = GetOfficeObjective(RecordId);
                            if (record == null)
                            {
                                throw new ArgumentException($"{approvalRequest.EntityType}  not found");
                            }
                            await base.ApproveOrReject(record as OfficeObjective, approvalRequest);
                            break;


                        case nameof(Strategy):
                            record = GetStrategy(RecordId);
                            if (record == null)
                            {
                                throw new ArgumentException($"{approvalRequest.EntityType}  not found");
                            }
                            await base.ApproveOrReject(record as Strategy, approvalRequest);
                            break;

                        case nameof(ObjectiveCategory):
                            record = GetObjectiveCategory(RecordId);
                            if (record == null)
                            {
                                throw new ArgumentException($"{approvalRequest.EntityType}  not found");
                            }
                            await base.ApproveOrReject(record as ObjectiveCategory, approvalRequest);
                            break;

                        default:
                            throw new NotImplementedException();
                    }

                }
 
                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }

            }

            return response;
        }

        #endregion


        public async Task<ResponseVm> ProcessObectivesUpload(IFormFile file)
        {
            var response = new ResponseVm();

            using var transaction = base.getDbContext().Database.BeginTransaction();

            try
            {
              var dataset = await base.GetExcelFileData(file);

                if (dataset != null && dataset.Tables.Count > 0)
                {

                    var dt = dataset.Tables[0];

                    var enterpriseObjectiveGrp = dt.AsEnumerable().GroupBy(r => r.Field<string>("EObjName") ).Select(grp => grp.ToList() ).ToList();


                    foreach (var entry in enterpriseObjectiveGrp)
                    {
                        var category = _objectiveCategoryRepo.Table.Where(o=> o.Name.ToLower().Contains(entry.FirstOrDefault().Field<string>("EObjCategory").ToLower())).FirstOrDefault();
                        var strategy = _strategyRepo.Table.Where(o=> o.Name.ToLower().Contains(entry.FirstOrDefault().Field<string>("Strategy").ToLower())).FirstOrDefault();

                        if (category == null) throw new Exception(NotificationMessages.NoRecordFound + $" for Category");
                        if (strategy == null) throw new Exception(NotificationMessages.NoRecordFound + $" for Strategy");

                        var enterpriseObjective = new EnterpriseObjective()
                        {
                            Description = entry.FirstOrDefault().Field<string>("EObjDesc"),
                            Name = entry.FirstOrDefault().Field<string>("EObjName"),
                            Kpi = entry.FirstOrDefault().Field<string>("EObjKPI"),
                            Target = entry.FirstOrDefault().Field<string>("EObjTarget"),
                            StrategyId = strategy.StrategyId,
                            EnterpriseObjectivesCategoryId = category.ObjectiveCategoryId,
                            Status = Enum.GetName(Status.PendingApproval)
                        };




                        enterpriseObjective.EnterpriseObjectiveId = base.GenerateCode(SequenceNumberTypes.EnterpriseObjectives);


                      await  base.Save(enterpriseObjective);

                        enterpriseObjective.DepartmentObjectives = new List<DepartmentObjective>();

                        var departmentObjectiveGrp = entry.GroupBy(r => r.Field<string>("DeptObjName")).Select(grp => grp.ToList()).ToList();

                        foreach ( var d in departmentObjectiveGrp)
                        {
                            var department =  _deptRepo.Table.Where(o => o.DepartmentName.ToLower().Contains(d.FirstOrDefault().Field<string>("Dept").ToLower())).FirstOrDefault();

                            if (department == null) throw new Exception(NotificationMessages.NoRecordFound + $" for Department");

                            enterpriseObjective.DepartmentObjectives.Add(new DepartmentObjective()
                            {
                                DepartmentId = department.DepartmentId,
                                Description = d.FirstOrDefault().Field<string>("DeptObjDesc"),
                                Name = d.FirstOrDefault().Field<string>("DeptObjName"),
                                Kpi = d.FirstOrDefault().Field<string>("DeptObjKPI"),
                                Target = d.FirstOrDefault().Field<string>("DeptObjTarget"),
                                EnterpriseObjectiveId = enterpriseObjective.EnterpriseObjectiveId,
                                Status = Enum.GetName(Status.PendingApproval),
                                DepartmentObjectiveId = base.GenerateCode(SequenceNumberTypes.DepartmentObjectives),
                                DivisionObjectives = new List<DivisionObjective>()

                                
                            });

                            await base.Save(enterpriseObjective);
                            var divisionObjectiveGrp = d.GroupBy(r => r.Field<string>("DivObjName")).Select(grp => grp.ToList()).ToList();
                            
                            foreach (var div in divisionObjectiveGrp)
                            {
                                var division =  _divRepo.Table.Where(o => o.DivisionName.ToLower().Contains(div.FirstOrDefault().Field<string>("Division").ToLower())).FirstOrDefault();

                                if (division == null) throw new Exception(NotificationMessages.NoRecordFound + $" for Division");

                                enterpriseObjective.DepartmentObjectives.LastOrDefault().DivisionObjectives.Add(new DivisionObjective()
                                {
                                    DivisionId = division.DivisionId,
                                    Description = div.FirstOrDefault().Field<string>("DivObjDesc"),
                                    Name = div.FirstOrDefault().Field<string>("DivObjName"),
                                    Kpi = div.FirstOrDefault().Field<string>("DivObjKPI"),
                                    Target = div.FirstOrDefault().Field<string>("DivObjTarget"),
                                    DepartmentObjectiveId = enterpriseObjective.DepartmentObjectives.LastOrDefault().DepartmentObjectiveId,
                                    Status = Enum.GetName(Status.PendingApproval),
                                    DivisionObjectiveId = base.GenerateCode(SequenceNumberTypes.DivisionObjectives),
                                    OfficeObjectives = new List<OfficeObjective>()

                                });

                                await base.Save(enterpriseObjective);
                                var officeObjectiveGrp = div.GroupBy(r => r.Field<string>("OffObjName")).Select(grp => grp.ToList()).ToList();

                                foreach (var off in officeObjectiveGrp)
                                {
                                    var office =  _officeRepo.Table.Where(o => o.OfficeName.ToLower().Contains(off.FirstOrDefault().Field<string>("Office").ToLower())).FirstOrDefault();

                                    if (office == null) throw new Exception(NotificationMessages.NoRecordFound + $" for Office");

                                    enterpriseObjective.DepartmentObjectives.LastOrDefault().DivisionObjectives.LastOrDefault().OfficeObjectives.Add(new OfficeObjective()
                                    {
                                        OfficeId = office.OfficeId,
                                        Description = off.FirstOrDefault().Field<string>("OffObjDesc"),
                                        Name = off.FirstOrDefault().Field<string>("OffObjName"),
                                        Kpi = off.FirstOrDefault().Field<string>("OffObjKPI"),
                                        Target = off.FirstOrDefault().Field<string>("OffObjTarget"),
                                        DivisionObjectiveId = enterpriseObjective.DepartmentObjectives.LastOrDefault().DivisionObjectives.LastOrDefault().DivisionObjectiveId,
                                        Status = Enum.GetName(Status.PendingApproval),
                                        OfficeObjectiveId = base.GenerateCode(SequenceNumberTypes.OfficeObjectives)

                                    });

                                }

                            }

                        }

                        await base.Save(enterpriseObjective);
                    }

                }

                await transaction.CommitAsync();
                response.Message = NotificationMessages.OperationCompleted;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                response.Errors.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    response.Errors.Add(ex.InnerException.Message);
                }
           
            }

            return response;
        }


    }




}
