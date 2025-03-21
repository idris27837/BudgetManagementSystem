namespace BudgetManagementSystem.DataAccessLayer.Context;

public class ValidationDbContextServiceProvider : IServiceProvider
{
    private readonly DbContext _currContext;

    public ValidationDbContextServiceProvider(DbContext currContext)
    {
        _currContext = currContext;
    }

    public object GetService(Type serviceType)
    {
        if (serviceType == typeof(DbContext))
        {
            return _currContext;
        }
        return null;
    }
}

