using Audit.EntityFramework;
using BudgetManagementSystem.Models.CompetencyMgt;
using BudgetManagementSystem.Models.Organogram;
using BudgetManagementSystem.ViewModels.OrganogramVm;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using PMS.Models;
using PMS.Models.Auditing;
using PMS.Models.PerformanceMgt.CoreModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;


namespace BudgetManagementSystem.DataAccessLayer.Context;

public sealed class DbCoreInitializer
{


    public static void Initialize(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<CompetencyCoreDbContext>();
        //var smd_Service = serviceScope.ServiceProvider.GetRequiredService<ISMD>();
        //context.Database.EnsureCreated();
        context.Database.Migrate();
        // pmscontext.Database.Migrate();
      

    
           _=SetUpAudit(app);
        //  _= LoadDummyData(app);


        var roleTask = Task.Run(() => PopulateRoles(context));
        roleTask.Wait();

        var bankYearTask = Task.Run(() => PopulateBankYears(context));
        bankYearTask.Wait();



        if (!context.Directorates.Any())
        {
            context.Directorates.Add(new Directorate
            {
                IsActive = true,
                DirectorateCode = "CS",
                DirectorateName = "Corporate Services",
            });
            context.Directorates.Add(new Directorate
            {
                IsActive = true,
                DirectorateCode = "EP",
                DirectorateName = "Economic Policy",
            });
            context.Directorates.Add(new Directorate
            {
                IsActive = true,
                DirectorateCode = "FSS",
                DirectorateName = "Financial System Stability",
            });
            context.Directorates.Add(new Directorate
            {
                IsActive = true,
                DirectorateCode = "GOV",
                DirectorateName = "Governors",
            });
            context.Directorates.Add(new Directorate
            {
                IsActive = true,
                DirectorateCode = "OP",
                DirectorateName = "Operations",
            });


            var task = Task.Run(() => context.SaveChangesAsync());
            task.Wait();
        }

        if (!context.Ratings.Any())
        {
            context.Ratings.Add(new Rating
            {
                IsActive = true,
                Name = "Entry",
                Value = 1
            });

            context.Ratings.Add(new Rating
            {
                IsActive = true,
                Name = "Basic",
                Value = 2
            });

            context.Ratings.Add(new Rating
            {
                IsActive = true,
                Name = "Intermediate",
                Value = 3
            });

            context.Ratings.Add(new Rating
            {
                IsActive = true,
                Name = "Expert",
                Value = 4
            });
            var task = Task.Run(() => context.SaveChangesAsync());
            task.Wait();
        }

        if (!context.CompetencyCategories.Any())
        {
            context.CompetencyCategories.Add(new CompetencyCategory
            {
                IsActive = true,
                CategoryName = "Technical",
                IsTechnical = true
            });
            context.CompetencyCategories.Add(new CompetencyCategory
            {
                IsActive = true,
                CategoryName = "Organisational",
                IsTechnical = false
            });
            context.CompetencyCategories.Add(new CompetencyCategory
            {
                IsActive = true,
                CategoryName = "Leadership",
                IsTechnical = false
            });
            context.CompetencyCategories.Add(new CompetencyCategory
            {
                IsActive = true,
                CategoryName = "Professional",
                IsTechnical = false
            });

            var task = Task.Run(() => context.SaveChangesAsync());
            task.Wait();
        }

        if (!context.ReviewTypes.Any())
        {
            context.ReviewTypes.Add(new ReviewType
            {
                IsActive = true,
                ReviewTypeName = ReviewTypeName.Supervisor.ToString(),
            });
            context.ReviewTypes.Add(new ReviewType
            {
                IsActive = true,
                ReviewTypeName = ReviewTypeName.Peers.ToString(),
            });
            context.ReviewTypes.Add(new ReviewType
            {
                IsActive = true,
                ReviewTypeName = ReviewTypeName.Self.ToString(),
            });
            context.ReviewTypes.Add(new ReviewType
            {
                IsActive = true,
                ReviewTypeName = ReviewTypeName.Subordinates.ToString(),
            });
            context.ReviewTypes.Add(new ReviewType
            {
                IsActive = true,
                ReviewTypeName = ReviewTypeName.Superior.ToString(),
            });
            var task = Task.Run(() => context.SaveChangesAsync());
            task.Wait();
        }

        if (!context.JobGradeGroups.Any())
        {
            context.JobGradeGroups.Add(new JobGradeGroup
            {
                IsActive = true,
                GroupName = "Junior",
                Order = 4
            });
            context.JobGradeGroups.Add(new JobGradeGroup
            {
                IsActive = true,
                GroupName = "Officer",
                Order = 3
            });
            context.JobGradeGroups.Add(new JobGradeGroup
            {
                IsActive = true,
                GroupName = "Manager",
                Order = 2
            });
            context.JobGradeGroups.Add(new JobGradeGroup
            {
                IsActive = true,
                GroupName = "Executive",
                Order = 1
            });
            var task = Task.Run(() => context.SaveChangesAsync());
            task.Wait();
        }

        if (!context.Permissions.Any())
        {
            context.Permissions.AddRange(new List<Permission>
            {
                new() { Name = "CreateCompetency", Description = "Initiate competency additions/reviews" },
                new() { Name = "ApproveNewCompetency", Description = "Approve competency additions/reviews" },
                new() { Name = "RejectNewCompetency", Description = "Reject/Return competency additions/reviews" },
                new() { Name = "ModifyCompetency", Description = "Edit competency additions/reviews" },
                new() { Name = "AssignCompetencyToJob", Description = "Initiate Competency additions/removal to/from job role" },
                new() { Name = "ApproveCompetencyToJobAssignment", Description = "Approve Competency additions/removal to/from job role" },
                new() { Name = "RejectCompetencyToJobAssignment", Description = "Reject/Return additions/removal to/from job role" },
                new() { Name = "ModifyCompetencyToJobAssignment", Description = "Modify additions/removal to/from job role" },
                new() { Name = "CreateReviewPeriod", Description = "Initiate/Extend competency review deployment period" },
                new() { Name = "ApproveReviewPeriod", Description = "Approve competency review deployment period" },
                new() { Name = "RejectReviewPeriod", Description = "Reject competency review deployment period" },
                new() { Name = "ExtendReviewPeriod", Description = "Extend competency review deployment period" },
                new() { Name = "GenerateReviewDeploymentStatusReport", Description = "Generate competency status deployment report" },
                new() { Name = "GenerateReviewOutcomeReport", Description = "Generate competency outcome deployment report" },
                new() { Name = "EmployeeToJobAssignment", Description = "Initiate the assigning of employee to Job Role" },
                new() { Name = "ApproveEmployeeToJobAssignment", Description = "Approve the assigning of employee to Job Role" },
                new() { Name = "RejectEmployeeToJobAssignment", Description = "Reject/Return the assigning of employee to Job Role" }
            });
            var task = Task.Run(() => context.SaveChangesAsync());
            task.Wait();
        }

        if (!context.TrainingTypes.Any())
        {
            context.TrainingTypes.Add(new TrainingType
            {
                IsActive = true,
                TrainingTypeName = "In-person Classroom Training",
            });
            context.TrainingTypes.Add(new TrainingType
            {
                IsActive = true,
                TrainingTypeName = "Online eLearning Modules (Udemy, IMF, EDX etc)",
            });
            context.TrainingTypes.Add(new TrainingType
            {
                IsActive = true,
                TrainingTypeName = "Webinars and Virtual Classrooms",
            });
            context.TrainingTypes.Add(new TrainingType
            {
                IsActive = true,
                TrainingTypeName = "Conferences and Seminars",
            });
            context.TrainingTypes.Add(new TrainingType
            {
                IsActive = true,
                TrainingTypeName = "Certification Programs",
            });
            context.TrainingTypes.Add(new TrainingType
            {
                IsActive = true,
                TrainingTypeName = "Job Rotation",
            });
            context.TrainingTypes.Add(new TrainingType
            {
                IsActive = true,
                TrainingTypeName = "Shadowing",
            });
            context.TrainingTypes.Add(new TrainingType
            {
                IsActive = true,
                TrainingTypeName = "Coaching and Mentoring",
            });

            context.TrainingTypes.Add(new TrainingType
            {
                IsActive = true,
                TrainingTypeName = "Reading Lists (recommended reading materials that align with competency needs)",
            });
            context.TrainingTypes.Add(new TrainingType
            {
                IsActive = true,
                TrainingTypeName = "COOL (Online self-paced courses)",
            });

            var task = Task.Run(() => context.SaveChangesAsync());
            task.Wait();
        }

        if (!context.ObjectiveCategories.Any())
        {
            context.ObjectiveCategories.Add(new ObjectiveCategory()
            {
                ObjectiveCategoryId = Guid.NewGuid().ToString(),
                 Name = "A",
                  Description = "Category A Objectives",
                   Status = Status.ApprovedAndActive.Humanize(),
                    
                   
                 
            });

            context.ObjectiveCategories.Add(new ObjectiveCategory()
            {
                ObjectiveCategoryId = Guid.NewGuid().ToString(),
                Name = "B",
                Description = "Category B Objectives",
                Status = Status.ApprovedAndActive.Humanize(),



            });

            context.ObjectiveCategories.Add(new ObjectiveCategory()
            {
                ObjectiveCategoryId = Guid.NewGuid().ToString(),
                Name = "C",
                Description = "Category C Objectives",
                Status = Status.ApprovedAndActive.Humanize(),



            });

            context.ObjectiveCategories.Add(new ObjectiveCategory()
            {
                ObjectiveCategoryId = Guid.NewGuid().ToString(),
                Name = "D",
                Description = "Category D Objectives",
                Status = Status.ApprovedAndActive.Humanize(),



            });

            var task = Task.Run(() => context.SaveChangesAsync());
            task.Wait();
        }
    }

    private static async Task PopulateRoles(CompetencyCoreDbContext context)
    {
        foreach (var role in RoleName.GetRoleList())
        {
            if (!context.Roles.Any(a => a.Name.ToUpper().Equals(role.ToUpper())))
            {
                context.Roles.Add(new ApplicationRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = role,
                    NormalizedName = role.ToUpper(),
                });
            }
        }

        await context.SaveChangesAsync();
    }

    /// <summary>b
    /// Populates the bank years.
    /// </summary>
    /// <param name="context">The context.</param>
    private static void PopulateBankYears(CompetencyCoreDbContext context)
    {
        if (!context.BankYears.Any())
        {
            context.BankYears.Add(new BankYear
            {
                IsActive = false,
                YearName = "2019",
            });
            context.BankYears.Add(new BankYear
            {
                IsActive = false,
                YearName = "2020",
            });
            context.BankYears.Add(new BankYear
            {
                IsActive = false,
                YearName = "2021",
            });
            context.BankYears.Add(new BankYear
            {
                IsActive = false,
                YearName = "2022",
            });
            context.BankYears.Add(new BankYear
            {
                IsActive = true,
                YearName = "2023",
            });
            context.BankYears.Add(new BankYear
            {
                IsActive = false,
                YearName = "2024",
            });

            var task = Task.Run(() => context.SaveChangesAsync());
            task.Wait();
        }
    }


    private static async Task LoadDummyData(IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CompetencyCoreDbContext>();
        var objectiveRepo = scope.ServiceProvider.GetRequiredService<IPMSRepo<EnterpriseObjective>>();
        var transaction = context.Database.BeginTransaction();
        try
        {

            var curstrategy = await context.Strategies.FirstOrDefaultAsync();
            var defCategory = await context.ObjectiveCategories.FirstOrDefaultAsync();



            if (curstrategy != null)
            {
                for (var i = 1; i <= 4; i++)
                {
                    var enterprise_objective = new EnterpriseObjective();
                    enterprise_objective.Name = $"Dummy Enterprise Objective {i}";
                    enterprise_objective.Description = enterprise_objective.Name + " Description";
                    enterprise_objective.Target = enterprise_objective.Name + " Target";
                    enterprise_objective.Kpi = enterprise_objective.Name + " KPI";
                    enterprise_objective.EnterpriseObjectivesCategoryId = defCategory.ObjectiveCategoryId;
                    enterprise_objective.StrategyId = curstrategy.StrategyId;
                    enterprise_objective.EnterpriseObjectiveId = Guid.NewGuid().ToString();
                    enterprise_objective.RecordStatus = Status.Active;
                    enterprise_objective.DepartmentObjectives = new List<DepartmentObjective>();

                    var departments = await context.Department.Where(d => !(string.IsNullOrEmpty(d.DepartmentCode)) && !(d.DepartmentName.ToLower().Contains("branch"))).ToListAsync();

                    foreach (var department in departments)
                    {
                        var department_objective = new DepartmentObjective();

                        if (department.DepartmentId == 11)
                        {

                        }

                        department_objective.Name = $"Dummy Department Objective {i} for " + department.DepartmentName;
                        department_objective.DepartmentId = department.DepartmentId;
                        department_objective.Kpi = "KPI for " + department_objective.Name;
                        department_objective.Target = "Target for " + department_objective.Name;
                        department_objective.Description = "Description for " + department_objective.Name;
                        department_objective.DepartmentObjectiveId = Guid.NewGuid().ToString();
                        department_objective.EnterpriseObjectiveId = enterprise_objective.EnterpriseObjectiveId;
                        
                        department_objective.DivisionObjectives = new List<DivisionObjective>();

                        department_objective.WorkProducts = new List<WorkProductDefinition>();

                        department_objective.WorkProducts.Add(new WorkProductDefinition()
                        {
                            Name = department_objective.Name + "Workproduct 1",
                            Description = "Description",
                            IsActive = true,
                            RecordStatus = Status.Active,
                             WorkProductDefinitionId = Guid.NewGuid().ToString(),
                              ReferenceNo = Guid.NewGuid().ToString(),
                            Status = Status.Active.Humanize(),
                            Deliverables = "Deliverables",
                                ObjectiveLevel = ObjectiveLevel.Department.Humanize(),
                                 ObjectiveId = department_objective.ObjectiveId,
                                  

                        });
                        department_objective.WorkProducts.Add(new WorkProductDefinition()
                        {
                            Name = department_objective.Name + "Workproduct 2",
                            Description = "Description",
                            IsActive = true,
                            RecordStatus = Status.Active,
                            WorkProductDefinitionId = Guid.NewGuid().ToString(),
                            ReferenceNo = Guid.NewGuid().ToString(),
                            Status = Status.Active.Humanize(),
                            Deliverables = "Deliverables",
                            ObjectiveLevel = ObjectiveLevel.Department.Humanize(),
                            ObjectiveId = department_objective.ObjectiveId,


                        });

                        var divisions = await context.Divisions.Where(d => d.DepartmentId == department.DepartmentId).ToListAsync();

                        foreach (var division in divisions )
                        {
                            var division_objective = new DivisionObjective();

                            if (division.DivisionId == 243)
                            {

                            }

                            division_objective.Name = $"Dummy Division Objective for {i} " + division.DivisionName;
                            division_objective.DivisionId = division.DivisionId;
                            division_objective.Kpi = "KPI for " + division_objective.Name;
                            division_objective.Target = "Target for " + division_objective.Name;
                            division_objective.Description = "Description for " + division_objective.Name;
                            division_objective.DivisionObjectiveId = Guid.NewGuid().ToString();
                            division_objective.DepartmentObjectiveId = department_objective.DepartmentObjectiveId;
                            division_objective.RecordStatus = Status.Active;
                            division_objective.OfficeObjectives = new List<OfficeObjective>();


                            division_objective.WorkProducts = new List<WorkProductDefinition>();

                            division_objective.WorkProducts.Add(new WorkProductDefinition()
                            {
                                Name = department_objective.Name + "Workproduct 1",
                                Description = "Description",
                                IsActive = true,
                                RecordStatus = Status.Active,
                                WorkProductDefinitionId = Guid.NewGuid().ToString(),
                                ReferenceNo = Guid.NewGuid().ToString(),
                                Status = Status.Active.Humanize(),
                                Deliverables = "Deliverables",
                                ObjectiveLevel = ObjectiveLevel.Department.Humanize(),
                                ObjectiveId = department_objective.ObjectiveId,


                            });
                            division_objective.WorkProducts.Add(new WorkProductDefinition()
                            {
                                Name = department_objective.Name + "Workproduct 2",
                                Description = "Description",
                                IsActive = true,
                                RecordStatus = Status.Active,
                                WorkProductDefinitionId = Guid.NewGuid().ToString(),
                                ReferenceNo = Guid.NewGuid().ToString(),
                                Deliverables = "Deliverables",
                                 Status = Status.Active.Humanize(),
                                
                                ObjectiveLevel = ObjectiveLevel.Department.Humanize(),
                                ObjectiveId = department_objective.ObjectiveId,


                            });

                            var offices = await context.Offices.Where(o => o.DivisionId == division.DivisionId).ToListAsync();

                            foreach (var office in offices)
                            {
                                var office_objective = new OfficeObjective();

                                if(office.OfficeId == 1066)
                                {

                                }


                                office_objective.Name = $"Dummy office Objective {i} for " + office.OfficeName;
                                office_objective.OfficeId = office.OfficeId;
                                office_objective.Kpi = "KPI for " + office_objective.Name;
                                office_objective.Target = "Target for " + office_objective.Name;
                                office_objective.Description = "Description for " + office_objective.Name;
                                office_objective.OfficeObjectiveId = Guid.NewGuid().ToString();
                                office_objective.DivisionObjectiveId = division_objective.DivisionObjectiveId;
                                office_objective.RecordStatus = Status.Active;

                                office_objective.JobGradeGroupId = 2;// new Random().Next(1, 4);
                                


                                office_objective.WorkProducts = new List<WorkProductDefinition>();

                                office_objective.WorkProducts.Add(new WorkProductDefinition()
                                {
                                    Name = department_objective.Name + "Workproduct 1",
                                    Description = "Description",
                                    IsActive = true,
                                    RecordStatus = Status.Active,
                                    WorkProductDefinitionId = Guid.NewGuid().ToString(),
                                    ReferenceNo = Guid.NewGuid().ToString(),
                                    Deliverables = "Deliverables",
                                    Status = Status.Active.Humanize(),
                                    ObjectiveLevel = ObjectiveLevel.Department.Humanize(),
                                    ObjectiveId = department_objective.ObjectiveId,


                                });
                                office_objective.WorkProducts.Add(new WorkProductDefinition()
                                {
                                    Name = department_objective.Name + "Workproduct 2",
                                    Description = "Description",
                                    IsActive = true,
                                    RecordStatus = Status.Active,
                                   
                                    WorkProductDefinitionId = Guid.NewGuid().ToString(),
                                    ReferenceNo = Guid.NewGuid().ToString(),
                                    Status = Status.Active.Humanize(),
                                    Deliverables = "Deliverables",
                                    ObjectiveLevel = ObjectiveLevel.Department.Humanize(),
                                    ObjectiveId = department_objective.ObjectiveId,


                                });

                                division_objective.OfficeObjectives.Add(office_objective);


                            }

                            department_objective.DivisionObjectives.Add(division_objective);
                        }

                        enterprise_objective.DepartmentObjectives.Add(department_objective);

                     
                    }


                    await objectiveRepo.Insert(enterprise_objective);

                    
                  
                }


                await transaction.CommitAsync();

            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }


    }

    private static async Task SetUpAudit(IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CompetencyCoreDbContext>();

        try
        {
            var entityTypes = context.Model.GetEntityTypes().Select(t => t.ClrType).ToList();

            foreach (var entryType in entityTypes)
            {
                bool isnew = false;

                var auditTag = entryType.GetCustomAttributes(typeof(AuditIncludeAttribute), false).SingleOrDefault() as AuditIncludeAttribute;

                if (auditTag != null)
                {

                    TableAttribute tableAttr = entryType.GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
                    string tableName = tableAttr != null ? tableAttr.Name : entryType.Name;

                    var autitabEntity = await context.AuditableEntities.Where
                       (a => a.EntityName == tableName).Include(a => a.AuditableAttributes).FirstOrDefaultAsync();

                   isnew = autitabEntity == null;

                    autitabEntity = autitabEntity ?? new AuditableEntity();

                    autitabEntity.EntityName = tableName;
                    autitabEntity.EnableAudit = true;
                    int n_new_entries = 0;
                    foreach (var atrb in entryType.GetProperties())
                    {

                        var isexempt = Attribute.IsDefined(atrb, typeof(AuditIgnoreAttribute));

                        if (!isexempt)
                        {
                            var atr = autitabEntity.AuditableAttributes.FirstOrDefault(a => a.AttributeName == atrb.Name);

                            if (atr is null)
                            {
                                autitabEntity.AuditableAttributes.Add(new AuditableAttribute()
                                {
                                    AttributeName = atrb.Name,
                                    EnableAudit = true,

                                });

                                n_new_entries++;
                            }
                        }
                  
                     

                    }


                    if (isnew)
                    {
                        context.AuditableEntities.Add(autitabEntity);
                    }
                    else
                    {
                        if (n_new_entries > 0)
                        {
                            context.AuditableEntities.Update(autitabEntity);
                        }
                    }
                    await context.SaveChangesAsync();
                }
            }
        

        }
        catch(Exception ex)
        {
            throw;
        }

    }

}
