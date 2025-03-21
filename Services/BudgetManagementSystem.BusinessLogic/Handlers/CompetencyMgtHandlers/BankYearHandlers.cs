using BudgetManagementSystem.Models.Core;
using BudgetManagementSystem.ViewModels.CoreVm;

namespace OfficeMgt.BusinessLogic.Handlers.CoreModelHandlers;

public class AddOrUpdateBankYearHandler(IRepo<BankYear> repo) : IRequestHandler<SaveBankYearCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(SaveBankYearCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.BankYearId > 0)
        {
            var bankYear = await repo.GetById(request.Vm.BankYearId);
            if (bankYear != null)
            {
                bankYear.YearName = request.Vm.YearName;

                repo.UpdateRecord(bankYear);
                message = $"{bankYear.YearName} Bank Year has been Updated Successfully";
            }
        }
        else
        {
            var model = new BankYear
            {
                YearName = request.Vm.YearName,
                IsActive = request.Vm.IsActive
            };
            await repo.AddRecord(model);
            message = $"{model.YearName} Bank Year has been Created Successfully";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteBankYearHandler(IRepo<BankYear> repo) : IRequestHandler<DeleteBankYearCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(DeleteBankYearCmd request, CancellationToken cancellationToken)
    {
        var bankYear = await repo.GetById(request.Id);

        if (request.IsSoftDelete && bankYear != null)
        {
            bankYear.SoftDeleted = true;
            bankYear.IsActive = false;
            repo.UpdateRecord(bankYear);
        }
        else
        {
            await repo.Delete(request.Id);
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"{bankYear.YearName} Bank Year has been deleted successfully" : result.Message };
    }
}

public class GetBankYearQueryHandler(IRepo<BankYear> repo) : IRequestHandler<GetBankYearQuery, List<BankYearVm>>
{
    public async ValueTask<List<BankYearVm>> Handle(GetBankYearQuery request, CancellationToken cancellationToken)
    {
        IQueryable<BankYear> query;

        query = repo.GetAllByQuery(null);


        return await query.Select(s => new BankYearVm
        {
            BankYearId = s.BankYearId,
            YearName = s.YearName,
        }).ToListAsync(cancellationToken);
    }
}