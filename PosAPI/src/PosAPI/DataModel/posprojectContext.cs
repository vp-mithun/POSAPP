using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PosAPI
{
    public partial class posprojectContext : DbContext
    {
        public posprojectContext(DbContextOptions<posprojectContext> options)
            :base(options)
        {

        }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Attendence> Attendence { get; set; }
        public virtual DbSet<Attributes> Attributes { get; set; }
        public virtual DbSet<AttributesValues> AttributesValues { get; set; }
        public virtual DbSet<Banners> Banners { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<BusinessSettings> BusinessSettings { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<ComposeMail> ComposeMail { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Content> Content { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<DamageProducts> DamageProducts { get; set; }
        public virtual DbSet<DummyAttendence> DummyAttendence { get; set; }
        public virtual DbSet<DummyExchange> DummyExchange { get; set; }
        public virtual DbSet<DummyProduct> DummyProduct { get; set; }
        public virtual DbSet<DummySale> DummySale { get; set; }
        public virtual DbSet<DummySaleexchange> DummySaleexchange { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeGroup> EmployeeGroup { get; set; }
        public virtual DbSet<ExchangeStock> ExchangeStock { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<IteamList> IteamList { get; set; }
        public virtual DbSet<IteamMaster> IteamMaster { get; set; }
        public virtual DbSet<Lrbook> Lrbook { get; set; }
        public virtual DbSet<MenuData> MenuData { get; set; }
        public virtual DbSet<MenuNames> MenuNames { get; set; }
        public virtual DbSet<MsgTemplates> MsgTemplates { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Outorder> Outorder { get; set; }
        public virtual DbSet<PPoints> PPoints { get; set; }
        public virtual DbSet<Paymentmethods> Paymentmethods { get; set; }
        public virtual DbSet<Permisions> Permisions { get; set; }
        public virtual DbSet<ProductAttrValues> ProductAttrValues { get; set; }
        public virtual DbSet<ProductTransfer> ProductTransfer { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<Productattributes> Productattributes { get; set; }
        public virtual DbSet<Products> Products { get; set; }        
        public virtual DbSet<Purchasebook> Purchasebook { get; set; }
        public virtual DbSet<Purchaseorder> Purchaseorder { get; set; }
        public virtual DbSet<Purchasereturns> Purchasereturns { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<ReOrders> ReOrders { get; set; }
        public virtual DbSet<Register> Register { get; set; }
        public virtual DbSet<Relations> Relations { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SaleOrders> SaleOrders { get; set; }
        public virtual DbSet<Salebook> Salebook { get; set; }
        public virtual DbSet<SalemanCommistion> SalemanCommistion { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<ShippingCost> ShippingCost { get; set; }
        public virtual DbSet<ShopDetails> ShopDetails { get; set; }
        public virtual DbSet<ShopSubdomains> ShopSubdomains { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Suspendlist> Suspendlist { get; set; }
        public virtual DbSet<Taxes> Taxes { get; set; }
        public virtual DbSet<UserAddress> UserAddress { get; set; }
        public virtual DbSet<UserPoints> UserPoints { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsingIngredients> UsingIngredients { get; set; }
        public virtual DbSet<Variants> Variants { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }
        public virtual DbSet<Withdrawals> Withdrawals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            //optionsBuilder.UseMySql(@"Server=localhost;User Id=root;Password=M!thun@123;Database=posproject");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComposeMail>(entity =>
            {
                entity.Property(e => e.Trash).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<DummySale>(entity =>
            {
                entity.Property(e => e.Black).HasDefaultValueSql("0");

                entity.Property(e => e.Ex).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<DummySaleexchange>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<MsgTemplates>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("1");

                entity.Property(e => e.Black).HasDefaultValueSql("0");

                entity.Property(e => e.Return).HasDefaultValueSql("0");

                entity.Property(e => e.Sales).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Purchaseorder>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Relations>(entity =>
            {
                entity.Property(e => e.Reject).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<SaleOrders>(entity =>
            {
                entity.Property(e => e.Black).HasDefaultValueSql("0");

                entity.Property(e => e.Editstat).HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.Property(e => e.Black).HasDefaultValueSql("0");

                entity.Property(e => e.Editstat).HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Suspendlist>(entity =>
            {
                entity.Property(e => e.Ex).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Taxes>(entity =>
            {
                entity.Property(e => e.BranchId).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Withdrawals>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });
        }
    }
}