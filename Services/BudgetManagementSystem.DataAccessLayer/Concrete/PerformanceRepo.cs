using BudgetManagementSystem.Models.PerformanceMgt;
using BudgetManagementSystem.ViewModels;
using PMS.Models;
using System.Collections;
using System.ComponentModel;

namespace BudgetManagementSystem.DataAccessLayer.Concrete;

public class PerformanceRepo<T> : IPMSRepo<T> where T : BaseEntity
{
    private CompetencyCoreDbContext Db { get; }
    private DbSet<T> _dbSet;
    public IUserDbContext _userContext { get; set; }

    public PerformanceRepo(CompetencyCoreDbContext db, IUserDbContext userContext)
    {
        Db = db;
        _dbSet = Db.Set<T>();
        _userContext = userContext;
    }



    public virtual CompetencyCoreDbContext GetDBContext()
    {
        return this.Db;
    }

    public IQueryable<T> GetAllByQuery(Expression<Func<T, bool>> filter = null, string includeProperties = "")
    {
        IQueryable<T> query = _dbSet.AsNoTracking();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return query;
    }
    public IQueryable<T> GetAllByQuery(Expression<Func<T, bool>> filter = null)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return query;
    }


    public IQueryable<T> GetAllByQuery(string includeProperties = "", Expression<Func<T, bool>> filter = null)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return query;
    }

    public IQueryable<T> GetAllByFilterQuery(ICollection<Expression<Func<T, bool>>> filters = null)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        foreach (var filter in filters)
        {
            query = query.Where(filter);
        }

        return query;
    }

    public IQueryable<T> GetAllByQueryPagination(int skip, int pageSize, Expression<Func<T, bool>> filter = null, string includeProperties = "")
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query.Skip(skip).Take(pageSize);
    }

    public IQueryable<T> GetAllByQueriesPagination(int skip, int pageSize, ICollection<Expression<Func<T, bool>>> filters = null, string includeProperties = "")
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        foreach (var filter in filters)
        {
            query = query.Where(filter);
        }

        return query.Skip(skip).Take(pageSize);
    }

    public Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();


        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query.CountAsync();
    }

    public Task<int> CountFiltersAsync(ICollection<Expression<Func<T, bool>>> filters = null)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        foreach (var filter in filters)
        {
            query = query.Where(filter);
        }

        return query.CountAsync();
    }


    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<bool> CheckQueryAsync(Expression<Func<T, bool>> filter, string includeProperties = "")
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return await query.AnyAsync(filter);
    }
    public async Task<T> GetById(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddRecord(T t)
    {
        await Db.AddAsync(t);
    }

    public async Task AddRecordRange(List<T> t)
    {
        await Db.AddRangeAsync(t);
    }

    public void UpdateRecord(T t)
    {
        if (Db.Entry(t).State == EntityState.Detached)
        {
            _dbSet.Attach(t);
        }
        Db.Entry(t).State = EntityState.Modified;
    }
    public void DetachRecord(T t)
    {
        Db.Entry(t).State = EntityState.Detached;
    }

    public async Task<ResponseVm> SaveContextAsync()
    {
        try
        {
            await Db.SaveChangesAsync();
            return new ResponseVm { IsSuccess = true, Message = "Committed Successfully" };
        }
        catch (Exception e)
        {
            return new ResponseVm { IsSuccess = false, Message = e.Message };
        }
    }

    public async Task<bool> CheckAny(int id)
    {
        var t = await _dbSet.FindAsync(id);
        return t != null;
    }


    public async Task Delete(object id)
    {
        var t = await _dbSet.FindAsync(id);
        if (Db.Entry(t).State == EntityState.Detached)
        {
            _dbSet.Attach(t);
        }
        _dbSet.Remove(t);
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> filter = null)
    {
        var entities = await _dbSet.Where(filter).ToListAsync();

        foreach (var entity in entities)
        {
            if (Db.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

    }



    public void Remove(T t)
    {
        _dbSet.Remove(t);
    }



    public string UserId
    {
        get
        {
            return _userContext?.UserId ?? "";
        }
    }


    /// </summary>
    public virtual IQueryable<T> Table
    {
        get
        {
            return this._dbSet;
        }
    }


    public virtual async Task<IList<T>> GetRecordsWithSatus(Status RecordStatus)
    {
        switch (RecordStatus)
        {
            case Status.All:

                return await this.Table.ToListAsync();

            default:

                return await this.Table.Where(o => o.Status == Enum.GetName(RecordStatus)).ToListAsync();
        }
    }


    /// <summary>
    /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
    /// </summary>
    public virtual IQueryable<T> TableNoTracking
    {
        get
        {
            return this._dbSet.AsNoTracking();
        }
    }




    public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> queryable = _dbSet;
        foreach (Expression<Func<T, object>> includeProperty in includeProperties)
        {
            queryable = queryable.Include<T, object>(includeProperty);
        }

        return queryable;
    }


    // <summary>
    /// Insert entities
    /// </summary>
    /// <param name="entities">Entities</param>
    public virtual async Task Insert(IEnumerable<T> entities)
    {
        try
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var entity in entities)
                this._dbSet.Add(entity);

            await this.Db.SaveChangesAsync();
        }
        //catch (DbEntityValidationException dbEx)
        //{
        //    var msg = string.Empty;

        //    foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        foreach (var validationError in validationErrors.ValidationErrors)
        //            msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

        //    var fail = new Exception(msg, dbEx);
        //    //Debug.WriteLine(fail.Message, fail);
        //    throw fail;
        //}
        catch (Exception ex) { throw ex; }
    }

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public async Task Update(T entity)
    {
        try
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            await this.Db.SaveChangesAsync();
        }
        //catch (DbEntityValidationException dbEx)
        //{
        //    var msg = string.Empty;

        //    foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        foreach (var validationError in validationErrors.ValidationErrors)
        //            msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

        //    var fail = new Exception(msg, dbEx);
        //    //Debug.WriteLine(fail.Message, fail);
        //    throw fail;
        //}
        catch (Exception ex) { throw ex; }
    }

    public virtual async Task Insert(T entity)
    {
        try
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            await this._dbSet.AddAsync(entity);

            await this.Db.SaveChangesAsync();

        }
        //catch (DbEntityValidationException dbEx)
        //{
        //    var msg = string.Empty;

        //    foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        foreach (var validationError in validationErrors.ValidationErrors)
        //            msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

        //    var fail = new Exception(msg, dbEx);
        //    //Debug.WriteLine(fail.Message, fail);
        //    throw fail;
        //}
        catch (Exception ex) { throw ex; }
    }

}

