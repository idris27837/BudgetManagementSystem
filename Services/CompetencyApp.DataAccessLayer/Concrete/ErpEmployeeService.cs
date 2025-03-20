using CompetencyApp.Infrastructure.Abstractions;
using CompetencyApp.Models.CompetencyMgt;
using CompetencyApp.Models.ErpModel;
using CompetencyApp.ViewModels;
using CompetencyApp.ViewModels.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CompetencyApp.Infrastructure.Concrete;

public class ErpEmployeeService(ErpDataDbContext context, StaffIDMaskDBContext staffIDMaskContext, CompetencyCoreDbContext competencydbcontext)
{
    protected ErpDataDbContext _context = context;

    protected StaffIDMaskDBContext _staffIDMaskContext = staffIDMaskContext;
    protected CompetencyCoreDbContext _competencyDBContext = competencydbcontext;

    public async Task<EmployeeErpDetailsDTO> GetEmployeeDetail(string employeeId)
    {
        var employee = await _context.EmployeeDetails.Select(e => new EmployeeErpDetailsDTO
        {
            UserName = e.UserName,
            EmailAddress = e.EmailAddress,
            FirstName = e.FirstName,
            MiddleNames = e.MiddleNames,
            LastName = e.LastName,
            EmployeeNumber = e.EmployeeNumber,
            JobName = e.JobName,
            DepartmentName = e.DepartmentName,
            DivisionName = e.DivisionName,
            HeadOfDivName = e.HeadOfDivName,
            OfficeName = e.OfficeName,
            SupervisorId = e.SupervisorId,
            HeadOfOfficeId = e.HeadOfOfficeId,
            HeadOfDivId = e.HeadOfDivId,
            HeadOfDeptId = e.HeadOfDeptId,
            DepartmentId = e.DepartmentId,
            OfficeId = e.OfficeId,
            Grade = e.Grade,
            DivisionId = e.DivisionId,
            Position = e.Position,
            PersonId = e.PersonId,

        }).FirstOrDefaultAsync(s => s.EmployeeNumber.Equals(employeeId));

        if (employee is not null)
            employee.Position = !string.IsNullOrWhiteSpace(employee.Position) ? employee.Position.Split(".").FirstOrDefault() : employee.JobName;
        return employee;
    }

    public async Task<EmployeeErpDetailsDTO> GetEmployeeDetailByUserName(string UserName)
    {
        var employee = await _context.EmployeeDetails.Select(e => new EmployeeErpDetailsDTO
        {
            UserName = e.UserName,
            EmailAddress = e.EmailAddress,
            FirstName = e.FirstName,
            MiddleNames = e.MiddleNames,
            LastName = e.LastName,
            EmployeeNumber = e.EmployeeNumber,
            JobName = e.JobName,
            DepartmentName = e.DepartmentName,
            DivisionName = e.DivisionName,
            HeadOfDivName = e.HeadOfDivName,
            OfficeName = e.OfficeName,
            SupervisorId = e.SupervisorId,
            HeadOfOfficeId = e.HeadOfOfficeId,
            HeadOfDivId = e.HeadOfDivId,
            HeadOfDeptId = e.HeadOfDeptId,
            DepartmentId = e.DepartmentId,
            OfficeId = e.OfficeId,
            Grade = e.Grade,
            DivisionId = e.DivisionId,
            Position = e.Position,
            PersonId = e.PersonId,

        }).FirstOrDefaultAsync(s => s.UserName.Equals(UserName));

        if (employee is not null)
            employee.Position = !string.IsNullOrWhiteSpace(employee.Position) ? employee.Position.Split(".").FirstOrDefault() : employee.JobName;
        return employee;
    }

    public async Task<List<EmployeeDetails>> GetEmployeeSubordinates(string employeeId) =>
        await _context.EmployeeDetails.AsNoTracking().Where(x => x.SupervisorId == employeeId && x.PersonTypeId == EmployeeType.ActiveStaff)
        .ToListAsync();

    public async Task<List<EmployeeDetails>> GetEmployeeSubordinatesByOfficeAndGrades(string employeeId, string gradeName, int officeId) =>
   await _context.EmployeeDetails.Where(x => x.OfficeId == officeId && x.EmployeeNumber != employeeId && Convert.ToInt32(x.Grade) > Convert.ToInt32(gradeName) && x.PersonTypeId == EmployeeType.ActiveStaff)
   .ToListAsync();
    public async Task<List<EmployeeDetails>> GetEmployeeSubordinatesByDivisionAndGrades(string employeeId, string gradeName, int? divisionId) =>
 await _context.EmployeeDetails.Where(x => x.DivisionId == divisionId && Convert.ToInt32(x.Grade) > Convert.ToInt32(gradeName)
 && x.EmployeeNumber != employeeId && x.PersonTypeId == EmployeeType.ActiveStaff)
 .ToListAsync();
    public async Task<List<EmployeeDetails>> GetEmployeeSuperiorByOfficeAndGrades(string employeeId, string gradeName, int officeId) =>
    await _context.EmployeeDetails.Where(x => x.OfficeId == officeId && Convert.ToInt32(x.Grade) < Convert.ToInt32(gradeName)
    && x.EmployeeNumber != employeeId && x.PersonTypeId == EmployeeType.ActiveStaff)
    .ToListAsync();
    public async Task<List<EmployeeDetails>> GetEmployeeSuperiorByDivisionAndGrades(string employeeId, string gradeName, int? divisionId) =>
   await _context.EmployeeDetails.Where(x => x.DivisionId == divisionId && Convert.ToInt32(x.Grade) < Convert.ToInt32(gradeName)
   && x.EmployeeNumber != employeeId && x.PersonTypeId == EmployeeType.ActiveStaff)
   .ToListAsync();


    public async Task<List<EmployeeDetails>> GetEmployeeByOfficeAndGrades(string employeeId, string gradeName, int officeId) =>
      await _context.EmployeeDetails.AsNoTracking().Where(x => x.OfficeId ==officeId && x.Grade == gradeName
      && x.EmployeeNumber != employeeId && x.PersonTypeId == EmployeeType.ActiveStaff)
      .ToListAsync();

    public async Task<List<EmployeeDetails>> GetEmployeePeersByDivisionAndGrades(string employeeId, string gradeName, int? divisionId) =>
    await _context.EmployeeDetails.AsNoTracking().Where(x => x.DivisionId == divisionId && x.Grade == gradeName
    && x.EmployeeNumber != employeeId && x.PersonTypeId == EmployeeType.ActiveStaff)
    .ToListAsync();

    public async Task<List<EmployeeDetails>> GetEmployeePeersByDepartmentAndGrades(string employeeId, string gradeName, int? deptId) =>
   await _context.EmployeeDetails.AsNoTracking().Where(x => x.DepartmentId == deptId && x.Grade == gradeName
   && x.EmployeeNumber != employeeId && x.PersonTypeId == EmployeeType.ActiveStaff)
   .ToListAsync();

    public async Task<List<ErpOrganizationVm>> AllEmployeeDepartments()
    {
        var allRecords = await _context.EmployeeDetails.AsNoTracking().ToListAsync();
        return allRecords.DistinctBy(x => x.DepartmentName)
          .Select(s => new ErpOrganizationVm
          {
              DepartmentId = s.DepartmentId,
              DepartmentName = s.DepartmentName,
          }).ToList();
    }

    public async Task<List<ErpOrganizationVm>> AllEmployeeDivisions()
    {
        var allRecords = await _context.EmployeeDetails.AsNoTracking().ToListAsync();

        return allRecords.DistinctBy(x => x.DivisionName)
              .Select(s => new ErpOrganizationVm
              {
                  DepartmentId = s.DepartmentId,
                  DepartmentName = s.DepartmentName,
                  DivisionId = s.DivisionId,
                  DivisionName = s.DivisionName
              }).ToList();

    }

    public async Task<List<ErpOrganizationVm>> AllEmployeeOffices()
    {
        var allRecords = await _context.EmployeeDetails.AsNoTracking().ToListAsync();

        return allRecords.DistinctBy(x => x.OfficeId)
              .Select(s => new ErpOrganizationVm
              {
                  DepartmentId = s.DepartmentId,
                  DepartmentName = s.DepartmentName,
                  DivisionId = s.DivisionId,
                  DivisionName = s.DivisionName,
                  OfficeId = s.OfficeId,
                  OfficeName = s.OfficeName
              }).ToList();

    }

    public async Task<List<EmployeeErpDetailsDTO>> GetAllEmployees() =>
        await _context.EmployeeDetails.AsNoTracking().Where(x => x.EmployeeNumber != null && x.PersonTypeId == EmployeeType.ActiveStaff)
        .Select(e => new EmployeeErpDetailsDTO
        {
            UserName = e.UserName,
            EmailAddress = e.EmailAddress,
            FirstName = e.FirstName,
            MiddleNames = e.MiddleNames,
            LastName = e.LastName,
            EmployeeNumber = e.EmployeeNumber,
            JobName = e.JobName,
            DepartmentName = e.DepartmentName,
            DivisionName = e.DivisionName,
            HeadOfDivName = e.HeadOfDivName,
            OfficeName = e.OfficeName,
            SupervisorId = e.SupervisorId,
            HeadOfOfficeId = e.HeadOfOfficeId,
            HeadOfDivId = e.HeadOfDivId,
            HeadOfDeptId = e.HeadOfDeptId,
            DepartmentId = e.DepartmentId,
            OfficeId = e.OfficeId,
            Grade = e.Grade,
            DivisionId = e.DivisionId,
            Position = e.Position,
        })
        .ToListAsync();

    public async Task<List<EmployeeErpDetailsDTO>> GetAllByDepartmentId(int departmentId) =>
      await _context.EmployeeDetails.AsNoTracking().Where(x =>
            x.DepartmentId == departmentId && x.EmployeeNumber != null && x.PersonTypeId == EmployeeType.ActiveStaff)
        .Select(e => new EmployeeErpDetailsDTO
        {
            UserName = e.UserName,
            EmailAddress = e.EmailAddress,
            FirstName = e.FirstName,
            MiddleNames = e.MiddleNames,
            LastName = e.LastName,
            EmployeeNumber = e.EmployeeNumber,
            JobName = e.JobName,
            DepartmentName = e.DepartmentName,
            DivisionName = e.DivisionName,
            HeadOfDivName = e.HeadOfDivName,
            OfficeName = e.OfficeName,
            SupervisorId = e.SupervisorId,
            HeadOfOfficeId = e.HeadOfOfficeId,
            HeadOfDivId = e.HeadOfDivId,
            HeadOfDeptId = e.HeadOfDeptId,
            DepartmentId = e.DepartmentId,
            OfficeId = e.OfficeId,
            Grade = e.Grade,
            DivisionId = e.DivisionId,
            Position = e.Position,
        })
        .ToListAsync();

    public async Task<List<EmployeeErpDetailsDTO>> GetAllByDivisionId(int divisionId) =>
       await _context.EmployeeDetails.AsNoTracking().Where(x =>
               x.DivisionId == divisionId && x.EmployeeNumber != null && x.PersonTypeId == EmployeeType.ActiveStaff)
        .Select(e => new EmployeeErpDetailsDTO
        {
            UserName = e.UserName,
            EmailAddress = e.EmailAddress,
            FirstName = e.FirstName,
            MiddleNames = e.MiddleNames,
            LastName = e.LastName,
            EmployeeNumber = e.EmployeeNumber,
            JobName = e.JobName,
            DepartmentName = e.DepartmentName,
            DivisionName = e.DivisionName,
            HeadOfDivName = e.HeadOfDivName,
            OfficeName = e.OfficeName,
            SupervisorId = e.SupervisorId,
            HeadOfOfficeId = e.HeadOfOfficeId,
            HeadOfDivId = e.HeadOfDivId,
            HeadOfDeptId = e.HeadOfDeptId,
            DepartmentId = e.DepartmentId,
            OfficeId = e.OfficeId,
            Grade = e.Grade,
            DivisionId = e.DivisionId,
            Position = e.Position,

        }).ToListAsync();

    public async Task<List<EmployeeErpDetailsDTO>> GetAllByOfficeId(int officeId)
    {
        return await _context.EmployeeDetails.AsNoTracking().Where(x =>
           x.OfficeId == officeId && x.EmployeeNumber != null && x.PersonTypeId == EmployeeType.ActiveStaff).Select(e => new EmployeeErpDetailsDTO
           {
               UserName = e.UserName,
               EmailAddress = e.EmailAddress,
               FirstName = e.FirstName,
               MiddleNames = e.MiddleNames,
               LastName = e.LastName,
               EmployeeNumber = e.EmployeeNumber,
               JobName = e.JobName,
               DepartmentName = e.DepartmentName,
               DivisionName = e.DivisionName,
               HeadOfDivName = e.HeadOfDivName,
               OfficeName = e.OfficeName,
               SupervisorId = e.SupervisorId,
               HeadOfOfficeId = e.HeadOfOfficeId,
               HeadOfDivId = e.HeadOfDivId,
               HeadOfDeptId = e.HeadOfDeptId,
               DepartmentId = e.DepartmentId,
               OfficeId = e.OfficeId,
               Grade = e.Grade,
               DivisionId = e.DivisionId,
               Position = e.Position,

           }).ToListAsync();
    }


    public async Task<List<ERPOfficeJobRoleVm>> AllOfficeJobRoles()
    {
        var allRecords = await _context.EmployeeDetails.AsNoTracking().ToListAsync();

        var positions = allRecords.Where(x => !string.IsNullOrWhiteSpace(x.Position)).DistinctBy(x => x.Position)
              .Select(s => new ERPOfficeJobRoleVm
              {
                  OfficeFullName = s.OfficeName,
                  OfficeId = s.OfficeId,
                  OfficeName = s.Position,
                  JobRoleName = s.Position.Split(".").FirstOrDefault(),
              }).ToList();
        return positions.DistinctBy(x => x.JobRoleName).ToList();

    }

    public async Task<List<ERPOfficeJobRoleVm>> AllFullOfficeJobRoles()
    {
        var allRecords = await _context.EmployeeDetails.AsNoTracking().ToListAsync();

        var filterJobROles = allRecords.Where(x => !string.IsNullOrWhiteSpace(x.Position))
              .Select(s => new ERPOfficeJobRoleVm
              {
                  OfficeFullName = s.OfficeName,
                  OfficeId = s.OfficeId,
                  OfficeName = s.OfficeName,
                  JobRoleName = s.Position.Split(".").FirstOrDefault(),
              }).ToList();

        return filterJobROles
                .GroupBy(p => new { p.OfficeName, p.JobRoleName })
                .Select(s => new ERPOfficeJobRoleVm
                {
                    OfficeFullName = s.Select(o => o.OfficeName).FirstOrDefault(),
                    OfficeId = s.Select(o => o.OfficeId).FirstOrDefault(),
                    OfficeName = s.Select(o => o.OfficeName).FirstOrDefault(),
                    JobRoleName = s.Select(o => o.JobRoleName).FirstOrDefault(),
                }).ToList();

    }

    public async Task<List<EROJobGradeVm>> AllJobGrades()
    {
        var allRecords = await _context.EmployeeDetails.AsNoTracking().Where(s => !string.IsNullOrEmpty(s.Grade)).ToListAsync();

        return allRecords.DistinctBy(x => x.Grade)
              .Select(s => new EROJobGradeVm
              {
                  GradeId = s.GradeId?.ToString() ?? "",
                  GradeName = s.Grade,
              }).ToList();
    }

    public async Task<List<EmployeeErpDetailsDTO>> GetHeadOfDivisionSubordinate(int divisionId)
    {
        var divisionalSubordinates = new List<EmployeeErpDetailsDTO>();
        var allRecords = await _context.EmployeeDetails.AsNoTracking().Where(x => x.DivisionId.Equals(divisionId) && x.PersonTypeId == EmployeeType.ActiveStaff).ToListAsync();
        var allHeadOfOffice = allRecords.DistinctBy(x => x.HeadOfOfficeId).ToList();

        foreach (var item in allHeadOfOffice)
        {
            divisionalSubordinates.Add(allRecords.Where(x => x.EmployeeNumber.Equals(item.EmployeeNumber))
                .Select(e => new EmployeeErpDetailsDTO
                {
                    UserName = e.UserName,
                    EmailAddress = e.EmailAddress,
                    FirstName = e.FirstName,
                    MiddleNames = e.MiddleNames,
                    LastName = e.LastName,
                    EmployeeNumber = e.EmployeeNumber,
                    JobName = e.JobName,
                    DepartmentName = e.DepartmentName,
                    DivisionName = e.DivisionName,
                    HeadOfDivName = e.HeadOfDivName,
                    OfficeName = e.OfficeName,
                    SupervisorId = e.SupervisorId,
                    HeadOfOfficeId = e.HeadOfOfficeId,
                    HeadOfDivId = e.HeadOfDivId,
                    HeadOfDeptId = e.HeadOfDeptId,
                    DepartmentId = e.DepartmentId,
                    OfficeId = e.OfficeId,
                    Grade = e.Grade,
                    DivisionId = e.DivisionId,
                    Position = e.Position,

                }).FirstOrDefault());
        }
        return divisionalSubordinates;
    }
    public async Task<List<EmployeeErpDetailsDTO>> GetHeadOfDepartmentSubordinate(int departmentId)
    {
        var divisionalSubordinates = new List<EmployeeErpDetailsDTO>();
        var allRecords = await _context.EmployeeDetails.AsNoTracking().Where(x => x.DepartmentId.Equals(departmentId) && x.PersonTypeId == EmployeeType.ActiveStaff).ToListAsync();
        var allHeadOfOffice = allRecords.DistinctBy(x => x.HeadOfDivId).ToList();

        foreach (var item in allHeadOfOffice)
        {
            divisionalSubordinates.Add(allRecords.Where(x => x.EmployeeNumber.Equals(item.EmployeeNumber))
                .Select(e => new EmployeeErpDetailsDTO
                {
                    UserName = e.UserName,
                    EmailAddress = e.EmailAddress,
                    FirstName = e.FirstName,
                    MiddleNames = e.MiddleNames,
                    LastName = e.LastName,
                    EmployeeNumber = e.EmployeeNumber,
                    JobName = e.JobName,
                    DepartmentName = e.DepartmentName,
                    DivisionName = e.DivisionName,
                    HeadOfDivName = e.HeadOfDivName,
                    OfficeName = e.OfficeName,
                    SupervisorId = e.SupervisorId,
                    HeadOfOfficeId = e.HeadOfOfficeId,
                    HeadOfDivId = e.HeadOfDivId,
                    HeadOfDeptId = e.HeadOfDeptId,
                    DepartmentId = e.DepartmentId,
                    OfficeId = e.OfficeId,
                    Grade = e.Grade,
                    DivisionId = e.DivisionId,
                    Position = e.Position,

                }).FirstOrDefault());
        }
        return divisionalSubordinates;
    }

    public async Task<StaffIDMaskDetailsDTO> GetStaffIDMaskDetail(string employeeId)
    {
        var StaffIdMask = await _staffIDMaskContext.StaffIDMaskDetails.Select(e => new StaffIDMaskDetailsDTO
        {
            Name = e.Name,
            EmployeeNumber = e.EmployeeNumber,
            CurrentPicture = e.CurrentPicture,
            NewPicture = e.NewPicture,
            Status = e.Status

        }).FirstOrDefaultAsync(s => s.EmployeeNumber.Equals(employeeId));
        return StaffIdMask;
    }

    public async Task<ResponseVm> AddUpdateStaffJobRole(StaffJobRoles vm)
    {
        var r = _competencyDBContext.StaffJobRoles.Where(x => x.EmployeeId.Equals(vm.EmployeeId)).AsNoTracking().FirstOrDefault();
        var employee = await GetEmployeeDetail(vm.EmployeeId);
        var supervisor = await GetEmployeeSupervisor(employee);

        if (r != null && r.StaffJobRoleId != 0)
        {
            await _competencyDBContext.StaffJobRoles.Where(x => x.EmployeeId == vm.EmployeeId).ExecuteUpdateAsync(x => x.SetProperty(z => z.JobRoleId, vm.JobRoleId).SetProperty(z => z.JobRoleName, vm.JobRoleName).SetProperty(z => z.SupervisorId, supervisor.EmployeeNumber).SetProperty(z => z.IsApproved, false).SetProperty(z => z.Status, vm.Status).SetProperty(z => z.UpdatedBy, vm.EmployeeId).SetProperty(z => z.DateUpdated, DateTime.UtcNow));
        }
        else
        {
            vm.SupervisorId = supervisor.EmployeeNumber;
            _competencyDBContext.StaffJobRoles.Add(vm);
            _competencyDBContext.SaveChanges();
        }
        return new ResponseVm { IsSuccess = true, Message = "Job Role was updated successfully" };
    }
    public StaffJobRoles GetStaffJobRoleById(string staffid)
    {
        var staffJobRol = _competencyDBContext.StaffJobRoles.Where(x => x.EmployeeId.Equals(staffid)).FirstOrDefault();
        return staffJobRol;
    }


    private async Task<EmployeeErpDetailsDTO> GetEmployeeSupervisor(EmployeeErpDetailsDTO employee)
    {
        EmployeeErpDetailsDTO supervisor;
        //if (employee.EmployeeNumber.Equals(employee.HeadOfOfficeId))
        //{
        //    supervisor = await GetEmployeeDetail(employee.HeadOfDivId);
        //}
        //else if (employee.EmployeeNumber.Equals(employee.HeadOfDivId))
        //{
        //    supervisor = await GetEmployeeDetail(employee.HeadOfDeptId);
        //}
        //else if (employee.EmployeeNumber.Equals(employee.HeadOfDeptId))
        //{
        //    supervisor = await GetEmployeeDetail(employee.SupervisorId);
        //}
        //else
        //{
        //    supervisor = await GetEmployeeDetail(employee.HeadOfOfficeId);
        //}
        supervisor = await GetEmployeeDetail(employee.HeadOfOfficeId);
        return supervisor;
    }

    public List<StaffJobRoles> GetStaffJobRoleUpdateRequest(string LinemanagerId)
    {
        //var staffJobRol = _competencyDBContext.StaffJobRoles.Where(x => !x.IsApproved && x.SupervisorId.Equals(LinemanagerId)).ToList();
        var staffJobRol = _competencyDBContext.StaffJobRoles.Where(x => x.SupervisorId.Equals(LinemanagerId)).ToList();

        return staffJobRol;
    }

    public async Task<List<string>> GetAllPendingReviewers()
    {
       var ActivePeriod = _competencyDBContext.ReviewPeriods.AsNoTracking().Where(x => x.IsActive.Equals(true) && x.IsApproved.Equals(true)).FirstOrDefault();
        var result = await _competencyDBContext.CompetencyReviews.AsNoTracking().Where(x => x.ActualRatingValue.Equals(0) && x.ReviewPeriodId.Equals(ActivePeriod.ReviewPeriodId)).ToListAsync();

        return result.DistinctBy(q => q.ReviewerId).Select(s => s.ReviewerId).ToList();
    }
public List<OfficeJobRoleVm> GetJobRolesByOffice(StaffJobRoles officename)
    {
        var query = from jobrole in _competencyDBContext.JobRoles
                    join officejobrole in _competencyDBContext.OfficeJobRole on jobrole.JobRoleId equals officejobrole.JobRoleId
                    join offic in _competencyDBContext.Offices on officejobrole.OfficeId equals offic.OfficeId
                    select new OfficeJobRoleVm
                    {
                        JobRoleName = jobrole.JobRoleName,
                        OfficeName = offic.OfficeName,
                        OfficeId = officejobrole.OfficeId,
                        JobRoleId = jobrole.JobRoleId,
                        OfficeJobRoleId= officejobrole.OfficeJobRoleId,
                        IsActive = jobrole.IsActive,
                    };


        var result = query.Where(x=>x.OfficeName.Equals(officename.FullName) && x.IsActive==true).ToList();
        return result.DistinctBy(s=>s.JobRoleName).ToList();
    }

    public async Task<ResponseVm> ApproveRejectStaffJobRole(StaffJobRoles vm)
    {
        if (vm.Status == "Approved" && vm.SoaStatus.Equals(true))
        {
            await _competencyDBContext.StaffJobRoles.Where(x => x.EmployeeId == vm.EmployeeId).ExecuteUpdateAsync(x => x.SetProperty(z => z.Status, vm.Status).SetProperty(z => z.ApprovedBy, vm.ApprovedBy).SetProperty(z => z.DateApproved, System.DateTime.UtcNow).SetProperty(z => z.IsApproved, true).SetProperty(z => z.SoaStatus, true).SetProperty(z => z.SoaResponse, vm.SoaResponse).SetProperty(z => z.UpdatedBy, vm.UpdatedBy).SetProperty(z => z.DateUpdated, System.DateTime.UtcNow));

            await _context.EmployeeDetails.AsNoTracking().Where(x => x.EmployeeNumber.Equals(vm.EmployeeId)).ExecuteUpdateAsync(x => x.SetProperty(z => z.Position, vm.JobRoleName));
            return new ResponseVm { IsSuccess = true, Message = "Job Role was updated successfully" };
        }
        else if (vm.Status == "Rejected-LineManager")
        {
            await _competencyDBContext.StaffJobRoles.Where(x => x.EmployeeId == vm.EmployeeId).ExecuteUpdateAsync(x => x.SetProperty(z => z.Status, vm.Status).SetProperty(z => z.RejectedBy, vm.RejectedBy).SetProperty(z => z.DateRejected, System.DateTime.UtcNow).SetProperty(z => z.IsApproved, false).SetProperty(z => z.IsRejected, true).SetProperty(z => z.RejectionReason, vm.RejectionReason));
            return new ResponseVm { IsSuccess = true, Message = "Job Role was Rejected successfully" };
        }
        else
        {
            await _competencyDBContext.StaffJobRoles.Where(x => x.EmployeeId == vm.EmployeeId).ExecuteUpdateAsync(x => x.SetProperty(z => z.SoaStatus,false).SetProperty(z => z.SoaResponse, vm.SoaResponse).SetProperty(z => z.UpdatedBy, vm.UpdatedBy).SetProperty(z => z.DateUpdated, System.DateTime.UtcNow));
            return new ResponseVm { IsSuccess = false, Message = vm.SoaResponse };
        }

        //if (vm != null && vm.Status.Equals("HRD-Approved"))
        //{
        //    await _competencyDBContext.StaffJobRoles.Where(x => x.EmployeeId == vm.EmployeeId).ExecuteUpdateAsync(x => x.SetProperty(z => z.Status, vm.Status).SetProperty(z => z.HrdIsApproved, true).SetProperty(z => z.HrdApprovedBy, vm.HrdApprovedBy).SetProperty(z => z.HrdDateApproved, vm.HrdDateApproved));
        //}
        //if (vm != null && vm.Status.Equals("HRD-Rejected"))
        //{
        //    await _competencyDBContext.StaffJobRoles.Where(x => x.EmployeeId == vm.EmployeeId).ExecuteUpdateAsync(x => x.SetProperty(z => z.Status, vm.Status).SetProperty(z => z.HrdIsRejected, true).SetProperty(z => z.HrdRejectedBy, vm.HrdRejectedBy).SetProperty(z => z.HrdDateRejected, vm.HrdDateRejected));
        //}


    }

    public async Task<List<String>> GetHeadOfOfficeIds(int empofficeid) => await _context.EmployeeDetails.AsNoTracking().Where(x => x.OfficeId == empofficeid).Select(s=>s.HeadOfOfficeId).ToListAsync();

    public async Task<List<String>> GetHeadDivisionIds(int? empDivisionId) => await _context.EmployeeDetails.AsNoTracking().Where(x => x.DivisionId == empDivisionId).Select(s => s.HeadOfDivId).ToListAsync();

    public async Task<List<String>> GetHeadDepartmentIds(int? empDeptId) => await _context.EmployeeDetails.AsNoTracking().Where(x => x.DepartmentId == empDeptId).Select(s => s.HeadOfDeptId).ToListAsync();

    public async Task<List<String>> GetSubordinatesIds(int empofficeid) => await _context.EmployeeDetails.AsNoTracking().Where(x => x.OfficeId == empofficeid).Select(s => s.HeadOfOfficeId).ToListAsync();

    public async Task<List<string>> GetGovernorDGsEmails() => await _context.EmployeeDetails.AsNoTracking().Where(x => x.JobName.Contains("GOVERNOR")).Select(s => s.EmailAddress).ToListAsync();


}
