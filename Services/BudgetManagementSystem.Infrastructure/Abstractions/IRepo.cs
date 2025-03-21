global using BudgetManagementSystem.Models.AbstractModel;
global using BudgetManagementSystem.ViewModels;
global using System.Linq.Expressions;

namespace BudgetManagementSystem.Infrastructure.Abstractions;

public interface IRepo<T> where T : BaseAudit
{
    IQueryable<T> GetAllByQuery(string includeProperties = "", Expression<Func<T, bool>> filter = null);
    IQueryable<T> GetAllByQuery(Expression<Func<T, bool>> filter = null, string includeProperties = "");
    IQueryable<T> GetAllByQuery(Expression<Func<T, bool>> filter = null);
    IQueryable<T> GetAllByFilterQuery(ICollection<Expression<Func<T, bool>>> filters = null);
    IQueryable<T> GetAllByQueryPagination(int skip, int pageSize, Expression<Func<T, bool>> filter = null, string includeProperties = "");
    IQueryable<T> GetAllByQueriesPagination(int skip, int pageSize, ICollection<Expression<Func<T, bool>>> filters = null, string includeProperties = "");
    Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
    Task<int> CountFiltersAsync(ICollection<Expression<Func<T, bool>>> filters = null);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");
    Task<bool> CheckQueryAsync(Expression<Func<T, bool>> filter, string includeProperties = "");
    Task<T> GetById(object id);
    Task AddRecord(T t);
    Task AddRecordRange(List<T> t);
    void UpdateRecord(T t);
    Task Delete(object t);
    Task DeleteAsync(Expression<Func<T, bool>> filter = null);
    void Remove(T t);
    Task<ResponseVm> SaveContextAsync();
    void DetachRecord(T t);
    T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = "");

    public string UserId { get; }
    IQueryable<T> Table { get; }
}

