using CompetencyApp.Infrastructure.Concrete;
using CompetencyApp.Models.CompetencyMgt;

namespace Kampus.DataAccess.Context;

public sealed class SeedERPOrganizationData
{
    private readonly CompetencyCoreDbContext _context;
    private readonly ErpEmployeeService _erpservice;


    public SeedERPOrganizationData(CompetencyCoreDbContext context, ErpEmployeeService erpservice)
    {
        _context = context;
        _erpservice = erpservice;
    }

    public async Task IntializeSeedOperation()
    {
        _ = await PopulateDepartments();
        _ = await PopulateDivisions();
        _ = await PopulateOffices();
        _ = await PopulateJobRoles();
        _ = await PopulateOfficeJobRoles();
        _ = await PopulateJobGrades();
    }


    public async Task<bool> PopulateDepartments()
    {
        if (!_context.Department.Any())
        {
            var deptList = new List<Department>();
            // get the department from erp database
            var erpDepartments = await _erpservice.AllEmployeeDepartments();

            foreach (var dept in erpDepartments)
            {
                var department = new Department
                {
                    DepartmentCode = dept?.DepartmentId.ToString() ?? "",
                    DepartmentName = dept.DepartmentName,
                    IsBranch = dept.DepartmentName.ToLower().Contains("branch")
                };

                deptList.Add(department);
            }
            await _context.Department.AddRangeAsync(deptList);
            await _context.SaveChangesAsync();
        }
        return true;
    }




    public async Task<bool> PopulateDivisions()
    {
        if (!_context.Divisions.Any())
        {
            var divisonList = new List<Division>();

            // get the divisions in the department
            var erpDivisions = await _erpservice.AllEmployeeDivisions();

            foreach (var erpDivision in erpDivisions)
            {
                var dept = await _context.Department.FirstOrDefaultAsync(x => x.DepartmentName.Equals(erpDivision.DepartmentName.ToString()));
                var division = new Division
                {
                    DepartmentId = dept.DepartmentId,
                    DivisionCode = erpDivision?.DivisionId.ToString() ?? "",
                    DivisionName = erpDivision.DivisionName
                };
                divisonList.Add(division);
            }
            await _context.Divisions.AddRangeAsync(divisonList);
            await _context.SaveChangesAsync();
        }
        return true;
    }


    public async Task<bool> PopulateOffices()
    {
        if (!_context.Offices.Any())
        {
            var officeList = new List<Office>();

            // get the offices in the division
            var erpOfficess = await _erpservice.AllEmployeeOffices();
            foreach (var erpDivision in erpOfficess.Where(x => x.DivisionId.HasValue))
            {
                var divison = await _context.Divisions.FirstOrDefaultAsync(x => x.DivisionName.Equals(erpDivision.DivisionName));
                var office = new Office
                {
                    DivisionId = divison.DivisionId,
                    OfficeCode = erpDivision.OfficeId.ToString(),
                    OfficeName = erpDivision.OfficeName
                };
                officeList.Add(office);
            }

            await _context.Offices.AddRangeAsync(officeList);
            await _context.SaveChangesAsync();
        }
        return true;
    }


    public async Task<bool> PopulateJobRoles()
    {
        if (!_context.JobRoles.Any())
        {
            var jobRoleList = new List<JobRole>();

            // get the department from erp database
            var officeJobRoles = await _erpservice.AllOfficeJobRoles();

            foreach (var officeJobRole in officeJobRoles.Where(x => !string.IsNullOrWhiteSpace(x.JobRoleName)))
            {
                var jobRole = new JobRole
                {
                    JobRoleName = officeJobRole.JobRoleName.ToUpper(),
                    Description = officeJobRole.OfficeName,
                };
                jobRoleList.Add(jobRole);
            }
            await _context.JobRoles.AddRangeAsync(jobRoleList);
            await _context.SaveChangesAsync();
        }
        return true;
    }

    public async Task<bool> PopulateOfficeJobRoles()
    {
        if (!_context.OfficeJobRole.Any())
        {
            var officeJobRoles = new List<OfficeJobRole>();

            // get the department from erp database
            var jobRoles = await _erpservice.AllFullOfficeJobRoles();

            foreach (var jobRole in jobRoles.Where(x => !string.IsNullOrWhiteSpace(x.JobRoleName)))
            {
                var office = await _context.Offices.FirstOrDefaultAsync(x => x.OfficeName.Equals(jobRole.OfficeName.ToString()));
                var sysJobRole = await _context.JobRoles.FirstOrDefaultAsync(x => x.JobRoleName.Equals(jobRole.JobRoleName.ToUpper()));

                if (office != null && sysJobRole != null)
                {
                    var officeJobRole = new OfficeJobRole
                    {
                        JobRoleId = sysJobRole.JobRoleId,
                        OfficeId = office.OfficeId,
                    };

                    officeJobRoles.Add((officeJobRole));
                }

            }
            await _context.OfficeJobRole.AddRangeAsync(officeJobRoles);
            var response = await _context.SaveChangesAsync();
        }
        return true;
    }


    public async Task<bool> PopulateJobGrades()
    {
        if (!_context.JobGrades.Any())
        {
            var jobGrades = new List<JobGrade>();
            // get the department from erp database
            var erpJobGrades = await _erpservice.AllJobGrades();

            foreach (var jobGrade in erpJobGrades)
            {
                var newJobGrade = new JobGrade
                {
                    GradeCode = jobGrade?.GradeId.ToString() ?? "",
                    GradeName = jobGrade.GradeName,
                };

                jobGrades.Add(newJobGrade);
            }
            await _context.JobGrades.AddRangeAsync(jobGrades);
            await _context.SaveChangesAsync();
        }
        return true;
    }
}