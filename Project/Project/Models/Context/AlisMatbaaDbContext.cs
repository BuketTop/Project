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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

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
                Name = "New"
            });

            orderStatusList.Add(new OrderStatus()
            {
                Name = "Preparing"
            });

            orderStatusList.Add(new OrderStatus()
            {
                Name = "Cargo"
            });

            orderStatusList.Add(new OrderStatus()
            {
                Name = "Delivered"
            });

            orderStatusList.Add(new OrderStatus()
            {
                Name = "Cancel"
            });


            foreach (OrderStatus orderStatus in orderStatusList)
            {
                context.OrderStatus.Add(orderStatus);
            }

            base.Seed(context);
        }
    }
}
