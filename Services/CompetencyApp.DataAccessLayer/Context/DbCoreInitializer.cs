using CompetencyApp.Models.CompetencyMgt;

namespace Kampus.DataAccess.Context;

public sealed class DbCoreInitializer
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<CompetencyCoreDbContext>();
        //context.Database.EnsureCreated();
        context.Database.Migrate();

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
}
