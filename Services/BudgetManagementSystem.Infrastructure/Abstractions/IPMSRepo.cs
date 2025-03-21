global using BudgetManagementSystem.Models.AbstractModel;
global using BudgetManagementSystem.ViewModels;
global using System.Linq.Expressions;
using BudgetManagementSystem.Models;
using BudgetManagementSystem.Models.BudgetMgt;

namespace BudgetManagementSystem.Infrastructure.Abstractions;

public interface IPMSRepo<T> where T : BaseEntity
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
    public string UserId { get; }
    IQueryable<T> Table { get; }
    IQueryable<T> TableNoTracking { get; }
    IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
    Task Insert(T entity);
    Task Update(T entity);
    Task Insert(IEnumerable<T> entities);
    Task<IList<T>> GetRecordsWithSatus(Status RecordStatus);
}

