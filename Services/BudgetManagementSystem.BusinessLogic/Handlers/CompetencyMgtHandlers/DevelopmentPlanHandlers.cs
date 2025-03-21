namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetDevelopmentPlansHandler(IRepo<DevelopmentPlan> repo) : IRequestHandler<GetDevelopmentPlansQuery, List<DevelopmentPlanVm>>
{
    public async ValueTask<List<DevelopmentPlanVm>> Handle(GetDevelopmentPlansQuery request, CancellationToken cancellationToken)
    {
        IQueryable<DevelopmentPlan> query;
        if (request.CompetencyProfileReviewId.HasValue && request.CompetencyProfileReviewId > 0)
        {
            query = repo.GetAllByQuery(x => x.CompetencyReviewProfileId.Equals(request.CompetencyProfileReviewId)).Include(i => i.CompetencyReviewProfile);
        }
        else
        {
            query = repo.GetAllByQuery(x => x.EmployeeNumber.Equals(null));
        }

        return await query.Select(s => new DevelopmentPlanVm
        {
            EmployeeNumber = s.EmployeeNumber,
            DevelopmentPlanId = s.DevelopmentPlanId,
            CompetencyReviewProfileId = s.CompetencyReviewProfileId,
            Activity = s.Activity,
            CompletionDate = s.CompletionDate,
            LearningResource = s.LearningResource,
            TargetDate = s.TargetDate,
            CompetencyCategoryName = s.CompetencyReviewProfile.CompetencyCategoryName,
            CompetencyName = s.CompetencyReviewProfile.CompetencyName,
            CurrentGap = s.CompetencyReviewProfile.CompetencyGap,
            ReviewPeriod = s.CompetencyReviewProfile.ReviewPeriodName,
            EmployeeName = s.CompetencyReviewProfile.EmployeeName,
            TaskStatus = s.TaskStatus,
            TrainingTypeName = s.TrainingTypeName,
            IsActive = s.IsActive
        }).ToListAsync();
    }
}

public class SaveDevelopmentPlanHandler(IRepo<DevelopmentPlan> repo) : IRequestHandler<SaveDevelopmentPlanCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(SaveDevelopmentPlanCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.DevelopmentPlanId > 0)
        {
            var developmentPlan = await repo.GetById(request.Vm.DevelopmentPlanId);
            if (developmentPlan != null)
            {
                developmentPlan.EmployeeNumber = request.Vm.EmployeeNumber;
                developmentPlan.DevelopmentPlanId = request.Vm.DevelopmentPlanId;
                developmentPlan.CompetencyReviewProfileId = request.Vm.CompetencyReviewProfileId;
                developmentPlan.Activity = request.Vm.Activity;
                developmentPlan.CompletionDate = request.Vm.CompletionDate;
                developmentPlan.LearningResource = request.Vm.LearningResource;
                developmentPlan.TrainingTypeName = request.Vm.TrainingTypeName;
                developmentPlan.TargetDate = request.Vm.TargetDate;
                developmentPlan.TaskStatus = request.Vm.TaskStatus;
                developmentPlan.IsActive = request.Vm.IsActive;

                repo.UpdateRecord(developmentPlan);
                message = "Competency Review Profile has been updated successfully";
            }
        }
        else
        {
            var model = new DevelopmentPlan
            {
                EmployeeNumber = request.Vm.EmployeeNumber,
                DevelopmentPlanId = request.Vm.DevelopmentPlanId,
                CompetencyReviewProfileId = request.Vm.CompetencyReviewProfileId,
                Activity = request.Vm.Activity,
                CompletionDate = request.Vm.CompletionDate,
                LearningResource = request.Vm.LearningResource,
                TargetDate = request.Vm.TargetDate,
                TaskStatus = request.Vm.TaskStatus,
                TrainingTypeName = request.Vm.TrainingTypeName,
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

public class DeleteDevelopmentPlanHandler(IRepo<DevelopmentPlan> repo) : IRequestHandler<DeleteDevelopmentPlanCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(DeleteDevelopmentPlanCmd request, CancellationToken cancellationToken)
    {
        var developmentPlan = await repo.GetById(request.Id);

        if (request.IsSoftDelete && developmentPlan != null)
        {
            developmentPlan.SoftDeleted = true;
            developmentPlan.IsActive = false;
            repo.UpdateRecord(developmentPlan);
        }
        else
        {
            await repo.Delete(request.Id);
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Competency Review Profile has been updated successfully" : result.Message };
    }
}

