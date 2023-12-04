using Microsoft.EntityFrameworkCore;
using SASSTS.Model.Entities;

namespace SASSTS.DataAccess.EntityFramework.Context
{
    public class SASSTSDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("DESKTOP-R04PVQ3\\SQLEXPRESS01; Initial Catalog=SASSTS; Integrated Security=true; TrustServerCertificate=True");
            //optionsBuilder.UseSqlServer("Server = localhost;Database=SASSTS;Integrated Security=true; Trust Server Certificate=True");
            //optionsBuilder.UseSqlServer("Server = DESKTOP-R04PVQ3\\sa;Database=SASSTS;User Id=SA;Password=12345;Trust Server Certificate=True");
            optionsBuilder.UseSqlServer("Data Source = .\\SQLEXPRESS; Initial Catalog = SASSTS; Trust Server Certificate = True; Integrated Security = True");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Accounting> Accountings  { get; set; }
        public DbSet<Authority> Authorities { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketDetail> BasketDetails { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ManagementReport> ManagementReports { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<ProcessHistory> ProcessHistories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<StockOperation> StockOperations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<RandomNumber> RandomNumbers { get; set; }
        public DbSet<EmployeeImage> EmployeeImages { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<TimeReport> Timereports { get; set; }
        public DbSet<MessageReceiver> MessageReceivers { get; set; }
        public DbSet<Support> Supports { get; set; }
    }
}
