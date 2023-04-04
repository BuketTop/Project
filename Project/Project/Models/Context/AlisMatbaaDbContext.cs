using Project.Model.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Project.Model.Context
{
    public class AlisMatbaaDbContext : DbContext
    {
        public AlisMatbaaDbContext() : base("AlisMatbaaDbContext")
        {
            Database.SetInitializer<AlisMatbaaDbContext>(new ProfileInitializer());
        }

        public DbSet<User> User { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        public DbSet<Currency> Currency { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductOption> ProductOption { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<OrderDetail>().Property(a => a.Price).HasPrecision(12, 2);
            modelBuilder.Entity<OrderDetail>().Property(a => a.TRYPrice).HasPrecision(12, 2);
            modelBuilder.Entity<OrderDetail>().Property(a => a.TRYRate).HasPrecision(12, 2);
            modelBuilder.Entity<OrderDetail>().Property(a => a.TRYTotal).HasPrecision(12, 2);


            modelBuilder.Entity<OrderDetail>()
                .HasRequired<ProductOption>(s => s.ProductOption)
                .WithMany(g => g.OrderDetail)
                .HasForeignKey<int>(s => s.ProductOptionId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }

    //Creating Custom DB Initializer
    public class ProfileInitializer : DropCreateDatabaseIfModelChanges<AlisMatbaaDbContext>
    {
        protected override void Seed(AlisMatbaaDbContext context)
        {
            IList<User> userList = new List<User>();
            userList.Add(new User()
            {
                Name = "Merve",
                Surname = "Alancay",
                Mail = "mervealancay@gmail.com",
                PhoneNumber = "",
                Password = "202cb962ac59075b964b07152d234b70"
            });


            foreach (User user in userList)
            {
                context.User.Add(user);
            }


            IList<OrderStatus> orderStatusList = new List<OrderStatus>();
            orderStatusList.Add(new OrderStatus()
            {
                Name = "Yeni"
            });

            orderStatusList.Add(new OrderStatus()
            {
                Name = "Hazırlanıyor"
            });

            orderStatusList.Add(new OrderStatus()
            {
                Name = "Kargoda"
            });

            orderStatusList.Add(new OrderStatus()
            {
                Name = "Teslim Edildi"
            });

            orderStatusList.Add(new OrderStatus()
            {
                Name = "İptal"
            });


            foreach (OrderStatus orderStatus in orderStatusList)
            {
                context.OrderStatus.Add(orderStatus);
            }



            IList<Currency> currencyList = new List<Currency>();
            currencyList.Add(new Currency()
            {
                Name = "TRY"
            });

            currencyList.Add(new Currency()
            {
                Name = "USD"
            });

            currencyList.Add(new Currency()
            {
                Name = "EUR"
            });


            foreach (Currency currency in currencyList)
            {
                context.Currency.Add(currency);
            }


            IList<Product> productList = new List<Product>();
            productList.Add(new Product()
            {
                Name = "Kartvizit",
                ProductOption = new List<ProductOption>()
                {
                    new ProductOption()
                    {
                        Name="Kuşe",
                        CurrencyId=2,
                        Price=1
                    },
                    new ProductOption()
                    {
                        Name="1.Hamur",
                        CurrencyId=2,
                        Price=2
                    }
                }
            });
            productList.Add(new Product()
            {
                Name = "Broşür",
                ProductOption = new List<ProductOption>()
                {
                    new ProductOption()
                    {
                        Name="Kuşe",
                        CurrencyId=2,
                        Price=10
                    },
                    new ProductOption()
                    {
                        Name="1.Hamur",
                        CurrencyId=2,
                        Price=15
                    }
                }
            });

            foreach (Product product in productList)
            {
                context.Product.Add(product);
            }



            base.Seed(context);
        }
    }
}
