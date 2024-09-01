using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using MVC_Domain.Core.BaseEntities;
using MVC_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MVC_Domain.Entities;
using Azure;
using Microsoft.AspNetCore.Http;
using BlogMainStructure.Infrastructure.Configurations;

namespace MVC_Infrastructure.AppContext
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with HTTP context accessor.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor to get the current user information.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets or sets the DbSet for Author entities.
        /// </summary>
        public virtual DbSet<Tag> Authors { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Article entities.
        /// </summary>
        public virtual DbSet<Article> Articles { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Author entities.
        /// </summary>
        public virtual DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Configures the model and applies entity configurations from the assembly.
        /// </summary>
        /// <param name="builder">The model builder to configure the model.</param>

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);
            //Yukardaki kod blogu IEntityConfiguration interface'inin bulundugu namespace'deki tum sınıflara configuration uygulamasını saglar.

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()  //save changese ovverride ettik.
                                           //Save changes olmadan veri tabanına bir sey gönderemeyiz.
        {
            SetBaseProperties();
            return base.SaveChanges();
        }

        private void SetBaseProperties()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();  //Bu satır context tarafından DBye gönderilen işlem bloklarını collection
                                                                //olarak barındırır.Her işlem blogu (State) ve entityleri de içerisinde bulunur.
                                                                //ChangeTracker veri tabanına yapılan işlemleri takip eder. Contextte bişey değiştiği zaman bunu anlıyor. Silinecekse
                                                                //silinmiş oluyor,degişecekse değiştirilmiş oluyor.
            var userId = "User bulunamadı";
            foreach (var entry in entries)
            {
                SetIfAdded(entry, userId);
                SetIfModified(entry, userId);
                SetIfDeleted(entry, userId);
            }


        }

        private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)  //soft ve hard delete işlemlerinin oldugu yer.
        {
            if (entry.State != EntityState.Deleted) //entitystate.deleted ise soft delete yapmayacagımız için diren databaseden silecegımız
                                                    //için return ediyoruz
            {
                return;
            }
            if (entry.Entity is not AuditableEntity entity)  //auditable entity olanlar sadece silinebiliyor, o zaman bu kullanılıyor.
            {
                return;
            }
            entry.State = EntityState.Modified; //soft delete işlemi bir modifikasyon olduğu için bunu kullanarak                                  
            entry.Entity.Status = Status.Deleted;//databasedeki state ini deleted e cekiyoruz.
            entity.DeletedDate = DateTime.Now; // silinme tarihi ve
            entity.DeletedBy = userId; // silen kişi bilgileri de burda kaydediliyor.
        }

        private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.Status = Status.Added;
                entry.Entity.UpdatedBy = userId;
                entry.Entity.UpdatedDate = DateTime.Now;
            }
        }

        private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Status = Status.Added;
                entry.Entity.CreatedBy = userId;
                entry.Entity.CreatedDate = DateTime.Now;
            }


        }
    }
}
