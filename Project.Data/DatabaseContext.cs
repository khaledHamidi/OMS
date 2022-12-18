using Microsoft.EntityFrameworkCore;
using OMS.Core.Entities;
using System.Reflection;
namespace OMS.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.UtcNow;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReference.UpdatedDate = DateTime.UtcNow;
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.UtcNow;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;

                                entityReference.UpdatedDate = DateTime.UtcNow;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            modelBuilder.Entity<Company>().HasData(
                new Company()
                {
                    Id = 1,
                    Name = "Teknosa",
                    Status = true,
                    EndDate = new DateTime(2022, 2, 2),
                    StartDate = new DateTime(2022, 2, 2),
                    CreatedDate = DateTime.UtcNow
                },
                new Company()
                {
                    Id = 2,
                    Name = "Enoca",
                    Status = true,
                    EndDate = new DateTime(2022, 2, 2),
                    StartDate = new DateTime(2022, 2, 2),
                    CreatedDate = DateTime.UtcNow
                }
            );


            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Price = 10,
                    Stock = 99,
                    CompanyId = 1,
                    Name = "Telephone",
                    CreatedDate = DateTime.UtcNow
                },
                new Product()
                {
                    Id = 2,
                    Name = "Biligsayar",
                    Price = 10,
                    Stock = 99,
                    CompanyId = 2,
                    CreatedDate = DateTime.UtcNow
                }
            );


            modelBuilder.Entity<Order>().HasData(
    new Order()
    {
        Id = 1,
        ClientName = "Khaled HAMIDI",
        ProductId = 1,
        CreatedDate = DateTime.UtcNow
    },
       new Order()
       {
           Id = 2,
           ClientName = "Ahmed Ali",
           ProductId = 2,
           CreatedDate = DateTime.UtcNow
       }, new Order()
       {
           Id = 3,
           ClientName = "Murat Oglu",
           ProductId = 2,
           CreatedDate = DateTime.UtcNow
       }, new Order()
       {
           Id = 4,
           ClientName = "Ziyat Ziyat",
           ProductId = 2,
           CreatedDate = DateTime.UtcNow
       }
);


            base.OnModelCreating(modelBuilder);
        }
    }
}
