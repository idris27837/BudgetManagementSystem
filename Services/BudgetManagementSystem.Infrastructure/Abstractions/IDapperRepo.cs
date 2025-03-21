namespace BudgetManagementSystem.Infrastructure.Abstractions;

public interface IDapperRepo<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T> GetById(int id);

    Task<T> GetCustomSingleQuery(string sql, object queryParams);
    Task<List<T>> GetCustomQuery(string sql, object queryParams);

}

