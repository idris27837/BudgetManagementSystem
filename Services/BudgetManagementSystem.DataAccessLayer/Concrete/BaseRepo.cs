using BudgetManagementSystem.ViewModels;

namespace BudgetManagementSystem.DataAccessLayer.Concrete;

public class BaseRepo<T> : IRepo<T> where T : BaseAudit
{
    private CompetencyCoreDbContext Db { get; }
    private DbSet<T> _dbSet;
    public IUserDbContext _userContext { get; set; }

    public BaseRepo(CompetencyCoreDbContext db, IUserDbContext userContext)
    {
        Db = db;
        _dbSet = Db.Set<T>();
        _userContext = userContext;
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



    public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = "")
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
        return  query.FirstOrDefault();
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
    public virtual IQueryable<T> Table
    {
        get
        {
            return this._dbSet;
        }
    }

}

