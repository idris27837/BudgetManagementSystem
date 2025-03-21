using BudgetManagementSystem.Models.Constants;
using BudgetManagementSystem.ViewModels.ReportVm;

namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetCompetencyReviewProfilesHandler(IRepo<CompetencyReviewProfile> repo) : IRequestHandler<GetCompetencyReviewProfilesQuery, List<CompetencyReviewProfileVm>>
{
    public async ValueTask<List<CompetencyReviewProfileVm>> Handle(GetCompetencyReviewProfilesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CompetencyReviewProfile> query;

        query = repo.GetAllByQuery(x => x.EmployeeNumber.Equals(request.EmployeeNumber) && x.ReviewPeriodId.Equals(request.ReviewPeriodId)).Include(i => i.DevelopmentPlans);

        return await query.Select(s => new CompetencyReviewProfileVm
        {
            EmployeeNumber = s.EmployeeNumber,
            CompetencyReviewProfileId = s.CompetencyReviewProfileId,
            CompetencyId = s.CompetencyId,
            CompetencyName = s.CompetencyName,
            ExpectedRatingId = s.ExpectedRatingId,
            ExpectedRatingName = s.ExpectedRatingName,
            ExpectedRatingValue = s.ExpectedRatingValue,
            ReviewPeriodId = s.ReviewPeriodId,
            ReviewPeriodName = s.ReviewPeriodName,
            AverageRatingId = s.AverageRatingId,
            AverageRatingName = s.AverageRatingName,
            AverageRatingValue = s.AverageRatingValue,
            AverageScore = s.AverageScore,
            CompetencyCategoryName = s.CompetencyCategoryName,
            IsActive = s.IsActive,
            EmployeeFullName = s.EmployeeName,
            OfficeId = s.OfficeId,
            OfficeName = s.OfficeName,
            DivisionId = s.DivisionId,
            DivisionName = s.DivisionName,
            DepartmentId = s.DepartmentId,
            DepartmentName = s.DepartmentName,
            GradeName = s.GradeName,
            JobRoleName = s.JobRoleName,
            JobRoleId = s.JobRoleId,
            ProgressCount = s.DevelopmentPlans.Count(x => x.TaskStatus.Equals(DevelopmentTaskStatus.InProgress.ToString())),
            CompletedCount = s.DevelopmentPlans.Count(x => x.TaskStatus.Equals(DevelopmentTaskStatus.Completed.ToString())),
            NumberOfDevelopmentPlans = s.DevelopmentPlans.Count()
        }).ToListAsync(cancellationToken);
    }
}
public class GetOfficeCompetencyReviewProfilesHandler(IRepo<CompetencyReviewProfile> repo) : IRequestHandler<GetOfficeCompetencyReviewProfilesQuery, ManagerOverviewReportVm>
{
    public async ValueTask<ManagerOverviewReportVm> Handle(GetOfficeCompetencyReviewProfilesQuery request, CancellationToken cancellationToken)
    {
        var result = new ManagerOverviewReportVm { BasicEmployeeDatas = [] };
        IQueryable<CompetencyReviewProfile> query;


        query = repo.GetAllByQuery(x => x.OfficeId.Equals(request.OfficeId.ToString()) && x.ReviewPeriodId.Equals(request.ReviewPeriodId));
        var competencyProfiles = await query.ToListAsync(cancellationToken);

        result.NoNotStartedReviews = competencyProfiles.Where(x => x.AverageRatingId == 0).DistinctBy(d => new { d.EmployeeNumber, d.CompetencyCategoryName }).Count();
        result.NoStartedReviews = competencyProfiles.Where(x => string.IsNullOrWhiteSpace(x.AverageRatingName)).DistinctBy(d => new { d.EmployeeNumber, d.CompetencyCategoryName }).Count();
        result.NoOfCompletedReviews = competencyProfiles.Where(x => !string.IsNullOrWhiteSpace(x.AverageRatingName)).DistinctBy(d => new { d.EmployeeNumber, d.CompetencyCategoryName }).Count();

        var employees = competencyProfiles.Select(x => x.EmployeeNumber).Distinct().ToList();

        foreach (var employee in employees)
        {
            var employeeData = competencyProfiles.FirstOrDefault(x => x.EmployeeNumber.Equals(employee));
            result.BasicEmployeeDatas.Add(new BasicEmployeeData
            {
                EmployeeNumber = employee,
                FullName = employeeData.EmployeeName,
                Department = employeeData.DepartmentName,
                Grade = employeeData.GradeName,
                Office = employeeData.OfficeName,
                Position = employeeData.JobRoleName,
                NoOfCompletedReviews = competencyProfiles.Where(x => x.EmployeeNumber.Equals(employee) && !string.IsNullOrWhiteSpace(x.AverageRatingName)).DistinctBy(d => d.CompetencyCategoryName).Count(),
                NoOfNotCOmpletedReviews = competencyProfiles.Where(x => x.EmployeeNumber.Equals(employee) && string.IsNullOrWhiteSpace(x.AverageRatingName)).DistinctBy(d => d.CompetencyCategoryName).Count()
            });
        }
        return result;
    }
}
public class GetGroupCompetencyReviewProfilesQueryHandler(IRepo<CompetencyReviewProfile> repo, IRepo<Rating> ratingRepo) : IRequestHandler<GetGroupCompetencyReviewProfilesQuery, GroupedCompetencyReviewProfileVm>
{
    public async ValueTask<GroupedCompetencyReviewProfileVm> Handle(GetGroupCompetencyReviewProfilesQuery request, CancellationToken cancellationToken)
    {
        var ratings = await ratingRepo.GetAllByQuery(x => x.IsActive.Equals(true)).ToListAsync(cancellationToken);
        var result = new GroupedCompetencyReviewProfileVm
        {
            CategoryCompetencyDetailStats = [],
            CategoryCompetencyStats = []
        };
        IQueryable<CompetencyReviewProfile> query;

        if (request.OfficeId.HasValue && request.OfficeId > 0)
        {
            query = repo.GetAllByQuery(x => x.OfficeId.Equals(request.OfficeId.ToString()) && x.ReviewPeriodId.Equals(request.ReviewPeriodId));
        }
        else if (request.DivisionId.HasValue && request.DivisionId > 0)
        {
            query = repo.GetAllByQuery(x => x.DivisionId.Equals(request.DivisionId.ToString()) && x.ReviewPeriodId.Equals(request.ReviewPeriodId));
        }
        else if (request.DepartmentId.HasValue && request.DepartmentId > 0)
        {
            query = repo.GetAllByQuery(x => x.DepartmentId.Equals(request.DepartmentId.ToString()) && x.ReviewPeriodId.Equals(request.ReviewPeriodId));
        }
        else
        {
            query = repo.GetAllByQuery(x => x.DepartmentId.Equals("") && x.ReviewPeriodId.Equals(request.ReviewPeriodId));
        }

        var competencyProfiles = await query.ToListAsync(cancellationToken);

        var categories = competencyProfiles.Select(x => x.CompetencyCategoryName).Distinct().ToList();
        foreach (var category in categories)
        {
            result.CategoryCompetencyStats.Add(new CategoryCompetencyStat
            {
                CategoryName = category,
                Actual = competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category)).Average(s => s.AverageRatingValue),
                Expected = competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category)).Average(s => s.ExpectedRatingValue),
            });

            var categoryCompetencyDetailStat = new CategoryCompetencyDetailStat
            {
                CategoryName = category,
                AverageRating = competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category)).Average(s => s.AverageRatingValue),
                HighestRating = competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category)).MaxBy(s => s.AverageRatingValue).AverageRatingValue,
                LowestRating = competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category)).MinBy(s => s.AverageRatingValue).AverageRatingValue,
                MostCommonRating = competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category)).Average(s => s.AverageRatingValue),
                GroupCompetencyRatings = [],
                CompetencyRatingStat = []
            };



            foreach (var competency in competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category)).DistinctBy(s => s.CompetencyName))
            {
                categoryCompetencyDetailStat.GroupCompetencyRatings.Add(new ChartDataVm
                {
                    Label = competency.CompetencyName,
                    Actual = competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category)
                                                    && s.CompetencyName.Equals(competency.CompetencyName)).Average(s => s.AverageRatingValue),
                    Expected = competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category)
                                                   && s.CompetencyName.Equals(competency.CompetencyName)).Average(s => s.ExpectedRatingValue),
                });
            }

            foreach (var rating in ratings)
            {
                categoryCompetencyDetailStat.CompetencyRatingStat.Add(new CompetencyRatingStat
                {
                    RatingOrder = rating.RatingId,
                    RatingName = rating.Name,
                    RatingValue = rating.Value,
                    NumberOfStaff = 0
                });
            }
            var employees = competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category))
                                                .DistinctBy(x => x.EmployeeNumber).ToList();
            foreach (var employee in employees)
            {
                var dd = competencyProfiles.Where(s => s.CompetencyCategoryName.Equals(category) && s.EmployeeNumber.Equals(employee.EmployeeNumber))
                                            .Average(s => s.AverageRatingValue);
                if (dd > 0)
                {
                    var ratingStat = categoryCompetencyDetailStat.CompetencyRatingStat.Where(s => s.RatingValue.Equals((int)dd)).FirstOrDefault();
                    ratingStat.NumberOfStaff++;
                    ratingStat.StaffPercentage = Math.Round(((double)ratingStat.NumberOfStaff / (double)employees.Count) * 100d, 2);
                }
            }

            categoryCompetencyDetailStat.GroupCompetencyRatings = [.. categoryCompetencyDetailStat.GroupCompetencyRatings.OrderBy(o => o.Actual)];
            result.CategoryCompetencyDetailStats.Add(categoryCompetencyDetailStat);
        }
        return result;
    }
}

public class GetCompetencyMatrixReviewProfilesQueryHandler(IRepo<CompetencyReviewProfile> repo, IRepo<Rating> ratingRepo) : IRequestHandler<GetCompetencyMatrixReviewProfilesQuery, CompetencyMatrixReviewOverviewVm>
{
    public async ValueTask<CompetencyMatrixReviewOverviewVm> Handle(GetCompetencyMatrixReviewProfilesQuery request, CancellationToken cancellationToken)
    {
        var ratings = await ratingRepo.GetAllByQuery(x => x.IsActive.Equals(true)).ToListAsync(cancellationToken);
        var result = new CompetencyMatrixReviewOverviewVm
        {
            CompetencyMatrixReviewProfiles = [],
            CompetencyNames = []
        };

        IQueryable<CompetencyReviewProfile> query;

        if (request.OfficeId.HasValue && request.OfficeId > 0)
        {
            query = repo.GetAllByQuery(x => x.OfficeId.Equals(request.OfficeId.ToString()) && x.ReviewPeriodId.Equals(request.ReviewPeriodId)
                                && !x.CompetencyCategoryName.ToLower().Equals("technical"));
        }
        else if (request.DivisionId.HasValue && request.DivisionId > 0)
        {
            query = repo.GetAllByQuery(x => x.DivisionId.Equals(request.DivisionId.ToString()) && x.ReviewPeriodId.Equals(request.ReviewPeriodId)
                        && !x.CompetencyCategoryName.ToLower().Equals("technical"));
        }
        else if (request.DepartmentId.HasValue && request.DepartmentId > 0)
        {
            query = repo.GetAllByQuery(x => x.DepartmentId.Equals(request.DepartmentId.ToString()) && x.ReviewPeriodId.Equals(request.ReviewPeriodId)
                                    && !x.CompetencyCategoryName.ToLower().Equals("technical"));
        }
        else
        {
            query = repo.GetAllByQuery(x => x.DepartmentId.Equals("") && x.ReviewPeriodId.Equals(request.ReviewPeriodId));
        }

        var competencyProfiles = await query.ToListAsync(cancellationToken);

        result.CompetencyNames = [.. competencyProfiles.Select(x => x.CompetencyName).Distinct().Order()];

        var employees = competencyProfiles.Select(x => x.EmployeeNumber).Distinct().ToList();
        foreach (var employee in employees)
        {
            var employeeCompetencies = competencyProfiles.Where(x => x.EmployeeNumber.Equals(employee)).ToList();

            var profile = new CompetencyMatrixReviewProfileVm
            {
                EmployeeId = employeeCompetencies.FirstOrDefault()?.EmployeeNumber,
                EmployeeName = employeeCompetencies.FirstOrDefault()?.EmployeeName,
                OfficeName = employeeCompetencies.FirstOrDefault()?.OfficeName,
                DivisionName = employeeCompetencies.FirstOrDefault()?.DivisionName,
                DepartmentName = employeeCompetencies.FirstOrDefault()?.DepartmentName,
                Grade = employeeCompetencies.FirstOrDefault()?.GradeName,
                Position = employeeCompetencies.FirstOrDefault()?.JobRoleName,
                GapCount = employeeCompetencies.Count(s => s.HaveGap.Equals(true)),
                NoOfCompetent = employeeCompetencies.Count(s => s.HaveGap.Equals(false)),
                NoOfCompetencies = employeeCompetencies.Count,
                OverallAverage = Math.Round(employeeCompetencies.Average(s => s.AverageRatingValue), 2),
                CompetencyMatrixDetails = []
            };

            foreach (var employeeCompetency in employeeCompetencies.OrderBy(o => o.CompetencyName))
            {
                profile.CompetencyMatrixDetails.Add(new CompetencyMatrixDetailVm
                {
                    CompetencyName = employeeCompetency.CompetencyName,
                    AverageScore = employeeCompetency.AverageRatingValue,
                    ExpectedRatingValue = employeeCompetency.ExpectedRatingValue,
                });
            }
            result.CompetencyMatrixReviewProfiles.Add(profile);
        }
        result.CompetencyMatrixReviewProfiles = [.. result.CompetencyMatrixReviewProfiles.OrderByDescending(o => o.OverallAverage)];
        return result;
    }
}
public class GetTechnicalCompetencyMatrixReviewProfilesHandler(IRepo<CompetencyReviewProfile> repo, IRepo<Rating> ratingRepo) : IRequestHandler<GetTechnicalCompetencyMatrixReviewProfilesQuery, CompetencyMatrixReviewOverviewVm>
{
    public async ValueTask<CompetencyMatrixReviewOverviewVm> Handle(GetTechnicalCompetencyMatrixReviewProfilesQuery request, CancellationToken cancellationToken)
    {
        var ratings = await ratingRepo.GetAllByQuery(x => x.IsActive.Equals(true)).ToListAsync(cancellationToken);
        var result = new CompetencyMatrixReviewOverviewVm
        {
            CompetencyMatrixReviewProfiles = [],
            CompetencyNames = []
        };

        IQueryable<CompetencyReviewProfile> query;

        query = repo.GetAllByQuery(x => x.ReviewPeriodId.Equals(request.ReviewPeriodId)
                                && x.JobRoleId.Equals(request.JobRoleId.ToString()) && x.CompetencyCategoryName.ToLower().Equals("technical"));

        var competencyProfiles = await query.ToListAsync(cancellationToken);

        result.CompetencyNames = [.. competencyProfiles.Select(x => x.CompetencyName).Distinct().Order()];

        var employees = competencyProfiles.Select(x => x.EmployeeNumber).Distinct().ToList();
        foreach (var employee in employees)
        {
            var employeeCompetencies = competencyProfiles.Where(x => x.EmployeeNumber.Equals(employee)).ToList();

            var profile = new CompetencyMatrixReviewProfileVm
            {
                EmployeeId = employeeCompetencies.FirstOrDefault()?.EmployeeNumber,
                EmployeeName = employeeCompetencies.FirstOrDefault()?.EmployeeName,
                OfficeName = employeeCompetencies.FirstOrDefault()?.OfficeName,
                DivisionName = employeeCompetencies.FirstOrDefault()?.DivisionName,
                DepartmentName = employeeCompetencies.FirstOrDefault()?.DepartmentName,
                Grade = employeeCompetencies.FirstOrDefault()?.GradeName,
                Position = employeeCompetencies.FirstOrDefault()?.JobRoleName,
                GapCount = employeeCompetencies.Count(s => s.HaveGap.Equals(true)),
                NoOfCompetent = employeeCompetencies.Count(s => s.HaveGap.Equals(false)),
                NoOfCompetencies = employeeCompetencies.Count,
                OverallAverage = Math.Round(employeeCompetencies.Average(s => s.AverageRatingValue), 2),
                CompetencyMatrixDetails = []
            };

            foreach (var employeeCompetency in employeeCompetencies.OrderBy(o => o.CompetencyName))
            {
                profile.CompetencyMatrixDetails.Add(new CompetencyMatrixDetailVm
                {
                    CompetencyName = employeeCompetency.CompetencyName,
                    AverageScore = employeeCompetency.AverageRatingValue,
                    ExpectedRatingValue = employeeCompetency.ExpectedRatingValue,
                });
            }
            result.CompetencyMatrixReviewProfiles.Add(profile);
        }
        result.CompetencyMatrixReviewProfiles = [.. result.CompetencyMatrixReviewProfiles.OrderByDescending(o => o.OverallAverage)];
        return result;
    }
}

public class GetCompetencyGapsHandler(IRepo<CompetencyReviewProfile> repo) : IRequestHandler<GetCompetencyGapsQuery, List<CompetencyReviewProfileVm>>
{
    public async ValueTask<List<CompetencyReviewProfileVm>> Handle(GetCompetencyGapsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CompetencyReviewProfile> query;

        query = repo.GetAllByQuery(x => x.EmployeeNumber.Equals(request.EmployeeNumber) && x.HaveGap.Equals(true)).Include(i => i.DevelopmentPlans);

        return await query.Select(s => new CompetencyReviewProfileVm
        {
            EmployeeNumber = s.EmployeeNumber,
            CompetencyReviewProfileId = s.CompetencyReviewProfileId,
            CompetencyId = s.CompetencyId,
            CompetencyName = s.CompetencyName,
            ExpectedRatingId = s.ExpectedRatingId,
            ExpectedRatingName = s.ExpectedRatingName,
            ExpectedRatingValue = s.ExpectedRatingValue,
            ReviewPeriodId = s.ReviewPeriodId,
            ReviewPeriodName = s.ReviewPeriodName,
            AverageRatingId = s.AverageRatingId,
            AverageRatingName = s.AverageRatingName,
            AverageRatingValue = s.AverageRatingValue,
            AverageScore = s.AverageScore,
            CompetencyCategoryName = s.CompetencyCategoryName,
            IsActive = s.IsActive,
            EmployeeFullName = s.EmployeeName,
            OfficeId = s.OfficeId,
            OfficeName = s.OfficeName,
            DivisionId = s.DivisionId,
            DivisionName = s.DivisionName,
            DepartmentId = s.DepartmentId,
            DepartmentName = s.DepartmentName,
            GradeName = s.GradeName,
            JobRoleName = s.JobRoleName,
            JobRoleId = s.JobRoleId,
            ProgressCount = s.DevelopmentPlans.Count(x => x.TaskStatus.Equals(DevelopmentTaskStatus.InProgress.ToString())),
            CompletedCount = s.DevelopmentPlans.Count(x => x.TaskStatus.Equals(DevelopmentTaskStatus.Completed.ToString())),
            NumberOfDevelopmentPlans = s.DevelopmentPlans.Count()
        }).ToListAsync(cancellationToken);
    }
}



public class CloseCompetencyGapHandler(IRepo<CompetencyReviewProfile> repo) : IRequestHandler<CloseCompetencyGapCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(CloseCompetencyGapCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.CompetencyReviewProfileId > 0)
        {
            var competencyReviewProfile = await repo.GetById(request.Vm.CompetencyReviewProfileId);
            if (competencyReviewProfile != null)
            {
                competencyReviewProfile.CompetencyId = request.Vm.CompetencyId;

                if (request.Vm.HaveGap.Equals(true))
                {
                    competencyReviewProfile.AverageRatingId = request.Vm.ExpectedRatingId;
                    competencyReviewProfile.AverageRatingName = request.Vm.ExpectedRatingName;
                    competencyReviewProfile.AverageRatingValue = request.Vm.ExpectedRatingValue;
                    competencyReviewProfile.AverageScore = request.Vm.ExpectedRatingId;
                    competencyReviewProfile.HaveGap = false;
                    competencyReviewProfile.CompetencyGap = 0;

                    competencyReviewProfile.UpdatedBy = request.Vm.EmployeeNumber;
                    competencyReviewProfile.DateUpdated = DateTime.Now;
                    repo.UpdateRecord(competencyReviewProfile);
                    message = "Competency Gap has been closed successfully!";
                }
                else
                {
                    message = "Competency Gap cannot not be closed when you still have a gap!!!";
                }
                    

            }
        }
        else
        {
            message = "Competency Profile does not exist!";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm
        {
            IsSuccess = result.IsSuccess,
            Message = result.IsSuccess ? message : result.Message
        };
    }
}


public class SaveCompetencyReviewProfileHandler(IRepo<CompetencyReviewProfile> repo) : IRequestHandler<SaveCompetencyReviewProfileCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(SaveCompetencyReviewProfileCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.CompetencyReviewProfileId > 0)
        {
            var competencyReviewProfile = await repo.GetById(request.Vm.CompetencyReviewProfileId);
            if (competencyReviewProfile != null)
            {
                competencyReviewProfile.EmployeeNumber = request.Vm.EmployeeNumber;
                competencyReviewProfile.CompetencyReviewProfileId = request.Vm.CompetencyReviewProfileId;
                competencyReviewProfile.CompetencyId = request.Vm.CompetencyId;
                competencyReviewProfile.CompetencyName = request.Vm.CompetencyName;
                competencyReviewProfile.ExpectedRatingId = request.Vm.ExpectedRatingId;
                competencyReviewProfile.ExpectedRatingName = request.Vm.ExpectedRatingName;
                competencyReviewProfile.ExpectedRatingValue = request.Vm.ExpectedRatingValue;
                competencyReviewProfile.ReviewPeriodId = request.Vm.ReviewPeriodId;
                competencyReviewProfile.ReviewPeriodName = request.Vm.ReviewPeriodName;
                competencyReviewProfile.AverageRatingId = request.Vm.AverageRatingId;
                competencyReviewProfile.AverageRatingName = request.Vm.AverageRatingName;
                competencyReviewProfile.AverageRatingValue = request.Vm.AverageRatingValue;
                competencyReviewProfile.AverageScore = request.Vm.AverageScore;
                competencyReviewProfile.CompetencyCategoryName = request.Vm.CompetencyCategoryName;
                competencyReviewProfile.OfficeId = request.Vm.OfficeId;
                competencyReviewProfile.OfficeName = request.Vm.OfficeName;
                competencyReviewProfile.DivisionId = request.Vm.DivisionId;
                competencyReviewProfile.DivisionName = request.Vm.DivisionName;
                competencyReviewProfile.DepartmentId = request.Vm.DepartmentId;
                competencyReviewProfile.DepartmentName = request.Vm.DepartmentName;
                competencyReviewProfile.JobRoleId = request.Vm.JobRoleId;
                competencyReviewProfile.JobRoleName = request.Vm.JobRoleName;
                competencyReviewProfile.GradeName = request.Vm.GradeName;
                competencyReviewProfile.IsActive = request.Vm.IsActive;

                repo.UpdateRecord(competencyReviewProfile);
                message = "Competency Review Profile has been updated successfully";
            }
        }
        else
        {
            var model = new CompetencyReviewProfile
            {
                EmployeeNumber = request.Vm.EmployeeNumber,
                CompetencyReviewProfileId = request.Vm.CompetencyReviewProfileId,
                CompetencyId = request.Vm.CompetencyId,
                CompetencyName = request.Vm.CompetencyName,
                ExpectedRatingId = request.Vm.ExpectedRatingId,
                ExpectedRatingName = request.Vm.ExpectedRatingName,
                ExpectedRatingValue = request.Vm.ExpectedRatingValue,
                ReviewPeriodId = request.Vm.ReviewPeriodId,
                ReviewPeriodName = request.Vm.ReviewPeriodName,
                AverageRatingId = request.Vm.AverageRatingId,
                AverageRatingName = request.Vm.AverageRatingName,
                AverageRatingValue = request.Vm.AverageRatingValue,
                AverageScore = request.Vm.AverageScore,
                CompetencyCategoryName = request.Vm.CompetencyCategoryName,
                OfficeId = request.Vm.OfficeId,
                OfficeName = request.Vm.OfficeName,
                DivisionId = request.Vm.DivisionId,
                DivisionName = request.Vm.DivisionName,
                DepartmentId = request.Vm.DepartmentId,
                DepartmentName = request.Vm.DepartmentName,
                JobRoleId = request.Vm.JobRoleId,
                JobRoleName = request.Vm.JobRoleName,
                GradeName = request.Vm.GradeName,
                IsActive = request.Vm.IsActive
            };
            await repo.AddRecord(model);
            message = "Competency Review Profile has been Created Successfully";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm
        {
            IsSuccess = result.IsSuccess,
            Message = result.IsSuccess ? message : result.Message
        };
    }
}

public class DeleteCompetencyReviewProfileHandler(IRepo<CompetencyReviewProfile> repo) : IRequestHandler<DeleteCompetencyReviewProfileCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(DeleteCompetencyReviewProfileCmd request, CancellationToken cancellationToken)
    {
        var competencyReviewProfile = await repo.GetById(request.Id);

        if (request.IsSoftDelete && competencyReviewProfile != null)
        {
            competencyReviewProfile.SoftDeleted = true;
            competencyReviewProfile.IsActive = false;
            repo.UpdateRecord(competencyReviewProfile);
        }
        else
        {
            await repo.Delete(request.Id);
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Competency Review Profile has been updated successfully" : result.Message };
    }
}
