using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using MVC_Domain.Core.BaseEntities;
using MVC_Domain.Enums;
using MVC_Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Infrastructure.DataAccess.EntityFramework
{
    public class EFBaseRepository<TEntity> : IRepository, IAsyncRepository, IAsyncUpdatableRepository<TEntity>,
        IAsyncInsertableRepository<TEntity>, IAsyncDeletableRepository<TEntity>, IAsyncFindableRepository<TEntity>,
        IAsyncQueryableRepository<TEntity>, IAsyncOrderableRepository<TEntity>, IAsyncTransactionRepository where TEntity : BaseEntity
    {
        protected readonly DbContext _context;  //protected kalıtım verdiğim yerlerde kullanılabilir.
        protected readonly DbSet<TEntity> _table; //her yerde uzun uzun DbSet<TEntity> olarak yazmayalım diye _table olarak sabitledik.
        public EFBaseRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await _table.AddAsync(entity); //AddAsync normalde dönüşü entry ama biz entity istiyoruz.O yuzden bu sekılde yazdık.
            return entry.Entity;


        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities); //await kullandığım metod asenkron ise yazmak zorundayım. await yazınca async otomatik gelir.
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression is null ? GetAllActives().AnyAsync() : GetAllActives().AnyAsync(expression);
            //eger bir kosul varsa kosula göre sorgula true ya da false dön,eger yoksa tabloda herhangi
            //bir veri var mı yok mu ona göre true ya da false dön bool. 

        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public Task<IExecutionStrategy> CreateExecutionStrategy()
        {
            return Task.FromResult(_context.Database.CreateExecutionStrategy());
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.FromResult(_table.Remove(entity)); //Delete de asenkron yok.Task.FromResult() ile asenkron yapıyoruz.
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true) //tracking e takılması bizim için avantaj
                                                                                  //o yuzden defaultu true tanımladık.
        {
            return await GetAllActives(tracking).ToListAsync();  //GetAllActives() deleted olmayanların hepsini getirecek.
                                                                 //Takibe dayalın statusu delete olmayanları getirir.
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            return await GetAllActives(tracking).Where(expression).ToListAsync();  //Bir collection içinde gelir.
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool orderBySDesc, bool tracking = true)
        {
            return orderBySDesc ? await GetAllActives(tracking).OrderByDescending(orderBy).ToListAsync() : await
                GetAllActives(tracking).OrderBy(orderBy).ToListAsync();
        }//Sıralamanı ters yap(Adan Zye değil de Zden Aya.True ise. False ise tam tersi.

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderBy, bool orderBySDesc, bool tracking = true)
        {
            var values = GetAllActives(tracking).Where(expression); //takip ve koşul durumu
            return orderBySDesc ? await values.OrderByDescending(orderBy).ToListAsync() : await
                values.OrderBy(orderBy).ToListAsync(); //sıralama durumuna göre return ediyoruz.
        }

        //expression tarafı herhangi bir koşul
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            return await GetAllActives(tracking).FirstOrDefaultAsync(expression); //Koşulu saglayan ilk veriyi getir diyorum FirstOrDefault ile.
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true)
        {
            return await GetAllActives(tracking).FirstOrDefaultAsync(x => x.Id == id); //GetById için ne dedik?
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }



        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.FromResult(_table.Update(entity).Entity); // en son .Entity yazmazsak entry dönüyor.Ama bize entity dönmesi lazım.
                                                                        // Update'in asenkronu yok bu yüzden Task.FromResult() ile asenkrona cevirmiş olduk.
        }

        protected IQueryable<TEntity> GetAllActives(bool tracking = true)
        {
            var values = _table.Where(x => x.Status != Status.Deleted); //statusu delete olanları getirmemek için metod yazıyoruz.
                                                                        //Yani GetAllActives() deleted olmayanların tumunu getirir.
            return tracking ? values : values.AsNoTracking(); //Gelen veri trackinge dahil olmadan gelir.Takibe dahil olmaz.
        }
    }
}
