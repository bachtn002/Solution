using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Models
{
    public partial class QL_CTVContext : DbContext
    {
        public QL_CTVContext()
        {
        }

        public QL_CTVContext(DbContextOptions<QL_CTVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCart> TCarts { get; set; }
        public virtual DbSet<TCategory> TCategories { get; set; }
        public virtual DbSet<TCustomer> TCustomers { get; set; }
        public virtual DbSet<TDebt> TDebts { get; set; }
        public virtual DbSet<TOrder> TOrders { get; set; }
        public virtual DbSet<TPayment> TPayments { get; set; }
        public virtual DbSet<TProduct> TProducts { get; set; }
        public virtual DbSet<TProductCategory> TProductCategories { get; set; }
        public virtual DbSet<TProductInventory> TProductInventories { get; set; }
        public virtual DbSet<TRole> TRoles { get; set; }
        public virtual DbSet<TShop> TShops { get; set; }
        public virtual DbSet<TShopUser> TShopUsers { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }
        public virtual DbSet<TmCustomerType> TmCustomerTypes { get; set; }
        public virtual DbSet<TmGender> TmGenders { get; set; }
        public virtual DbSet<TmOrderStatus> TmOrderStatuses { get; set; }
        public virtual DbSet<TmPaymentMethod> TmPaymentMethods { get; set; }
        public virtual DbSet<TmPaymentStatus> TmPaymentStatuses { get; set; }
        public virtual DbSet<TmProductStatus> TmProductStatuses { get; set; }
        public virtual DbSet<TmShippingMethod> TmShippingMethods { get; set; }
        public virtual DbSet<TmShopStatus> TmShopStatuses { get; set; }
        public virtual DbSet<TmUserLevel> TmUserLevels { get; set; }
        public virtual DbSet<TmUserStatus> TmUserStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=145.239.255.230;database=QL_CTV;user=api_user;password=diennk_ay4DZabZhe5hr2MZe89g7rrAfTGEwANCYTLv6Yyn4AGb27ke6XEhKarcnqjEqnmk", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.17-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            modelBuilder.Entity<TCart>(entity =>
            {
                entity.HasKey(e => e.CartId)
                    .HasName("PRIMARY");

                entity.ToTable("T_Cart");

                entity.Property(e => e.CartId).HasColumnType("bigint(20)");

                entity.Property(e => e.Amount).HasPrecision(20, 6);

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountAmount).HasPrecision(20, 6);

                entity.Property(e => e.DiscountRate).HasComment("Tỉ lệ chiết khấu");

                entity.Property(e => e.IsDelete)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.OrderId)
                    .HasColumnType("bigint(20)")
                    .HasComment("Filed này sẽ update khi user create Order");

                entity.Property(e => e.Price).HasPrecision(20, 6);

                entity.Property(e => e.ProductId).HasColumnType("bigint(20)");

                entity.Property(e => e.Qty).HasComment("Số lượng");

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("T_Category");

                entity.Property(e => e.CategoryId).HasColumnType("bigint(20)");

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasColumnType("bit(1)");

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.NameCategory)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.ParentId).HasColumnType("int(11)");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bit(1)")
                    .HasComment("Mỗi shop có category riêng");
            });

            modelBuilder.Entity<TCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PRIMARY");

                entity.ToTable("T_Customer");

                entity.Property(e => e.CustomerId).HasColumnType("bigint(20)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerTypeId).HasColumnType("tinyint(4)");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.GenderId).HasColumnType("tinyint(4)");

                entity.Property(e => e.IsDelete)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("Field này dùng để đánh dấu bản ghi đã bị xóa ");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TDebt>(entity =>
            {
                entity.HasKey(e => e.DebtId)
                    .HasName("PRIMARY");

                entity.ToTable("T_Debt");

                entity.Property(e => e.DebtId).HasColumnType("bigint(20)");

                entity.Property(e => e.Advance)
                    .HasPrecision(20, 6)
                    .HasComment("Tiền tạm ứng");

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.DeadlinedUtcDate)
                    .HasColumnType("datetime")
                    .HasComment("Hạn thanh toán");

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnType("int(11)");

                entity.Property(e => e.ShopId).HasColumnType("bigint(20)");

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PRIMARY");

                entity.ToTable("T_Order");

                entity.Property(e => e.OrderId).HasColumnType("bigint(20)");

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnType("bigint(20)");

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.OrderStatusId).HasColumnType("tinyint(4)");

                entity.Property(e => e.PaymentMethodId).HasPrecision(20, 6);

                entity.Property(e => e.ShopId).HasColumnType("bigint(20)");

                entity.Property(e => e.TotalAmount).HasPrecision(20, 6);

                entity.Property(e => e.TotalDiscountAmount).HasPrecision(20, 6);

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PRIMARY");

                entity.ToTable("T_Payment");

                entity.Property(e => e.PaymentId)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(2500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.OrderId).HasColumnType("bigint(20)");

                entity.Property(e => e.PaymentAmount).HasPrecision(20, 6);

                entity.Property(e => e.PaymentStatusId).HasColumnType("tinyint(4)");
            });

            modelBuilder.Entity<TProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PRIMARY");

                entity.ToTable("T_Product");

                entity.HasComment("Vì bán qua cộng tác viên nên ko cần quá nhiều thông tin sản phẩm");

                entity.Property(e => e.ProductId).HasColumnType("bigint(20)");

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Images)
                    .IsRequired()
                    .HasColumnType("json")
                    .HasDefaultValueSql("''")
                    .HasComment("Các ảnh lưu dạng mảng json");

                entity.Property(e => e.IsDelete).HasColumnType("bit(1)");

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Price)
                    .HasPrecision(20, 6)
                    .HasComment("Giá bán sản phẩm");

                entity.Property(e => e.ProductStatusId).HasColumnType("tinyint(4)");

                entity.Property(e => e.Properties)
                    .IsRequired()
                    .HasColumnType("json")
                    .HasDefaultValueSql("''")
                    .HasComment("Các thông tin lưu dạng json tùy define từng shop");

                entity.Property(e => e.ShopId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TProductCategory>(entity =>
            {
                entity.HasKey(e => e.ProductCategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("T_ProductCategory");

                entity.Property(e => e.ProductCategoryId).HasColumnType("bigint(20)");

                entity.Property(e => e.CategoryId).HasColumnType("bigint(20)");

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasColumnType("bit(1)");

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TProductInventory>(entity =>
            {
                entity.HasKey(e => e.ProductInventoryId)
                    .HasName("PRIMARY");

                entity.ToTable("T_ProductInventory");

                entity.HasComment("Quản lý kho của sản phẩm\r\nSau này mở rộng có thêm thông tin của nhà cung cấp");

                entity.Property(e => e.ProductInventoryId)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.DiscountRate).HasComment("Tỷ lệ rate theo từng đợt nhập kho");

                entity.Property(e => e.IsDelete).HasColumnType("bit(1)");

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("'0'")
                    .HasComment("Ghi chú về lần nhập kho này");

                entity.Property(e => e.Price)
                    .HasPrecision(20, 6)
                    .HasComment("Giá nhập kho");

                entity.Property(e => e.ProductId).HasColumnType("bigint(20)");

                entity.Property(e => e.Qty)
                    .HasColumnType("int(11)")
                    .HasComment("Số lượng");
            });

            modelBuilder.Entity<TRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PRIMARY");

                entity.ToTable("T_Role");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.GrantAccess)
                    .IsRequired()
                    .HasColumnType("json")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.IsDelete)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsSystemRole)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<TShop>(entity =>
            {
                entity.HasKey(e => e.ShopId)
                    .HasName("PRIMARY");

                entity.ToTable("T_Shop");

                entity.Property(e => e.ShopId).HasColumnType("bigint(20)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2500);

                entity.Property(e => e.IsDelete)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.ShopStatusId).HasColumnType("tinyint(4)");

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TShopUser>(entity =>
            {
                entity.HasKey(e => e.ShopUserId)
                    .HasName("PRIMARY");

                entity.ToTable("T_ShopUser");

                entity.Property(e => e.ShopUserId).HasColumnType("bigint(20)");

                entity.Property(e => e.CreatedUtcDate).HasColumnType("date");

                entity.Property(e => e.IsDelete).HasColumnType("bit(1)");

                entity.Property(e => e.RetiredUtcDate).HasColumnType("date");

                entity.Property(e => e.RoleId).HasColumnType("int(11)");

                entity.Property(e => e.ShopId).HasColumnType("bigint(20)");

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("T_User");

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CreatedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.GenderId).HasColumnType("tinyint(4)");

                entity.Property(e => e.IsDelete)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.ModifiedUtcDate).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.RoleId).HasColumnType("int(11)");

                entity.Property(e => e.UserStatusId).HasColumnType("tinyint(4)");
            });

            modelBuilder.Entity<TmCustomerType>(entity =>
            {
                entity.HasKey(e => e.CustomerLevelId)
                    .HasName("PRIMARY");

                entity.ToTable("TM_CustomerType");

                entity.Property(e => e.CustomerLevelId)
                    .HasColumnType("tinyint(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomerLevelName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TmGender>(entity =>
            {
                entity.HasKey(e => e.GenderId)
                    .HasName("PRIMARY");

                entity.ToTable("TM_Gender");

                entity.Property(e => e.GenderId)
                    .HasColumnType("tinyint(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.GenderName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TmOrderStatus>(entity =>
            {
                entity.HasKey(e => e.OrderStatusId)
                    .HasName("PRIMARY");

                entity.ToTable("TM_OrderStatus");

                entity.Property(e => e.OrderStatusId)
                    .HasColumnType("tinyint(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TmPaymentMethod>(entity =>
            {
                entity.HasKey(e => e.PaymentMethodId)
                    .HasName("PRIMARY");

                entity.ToTable("TM_PaymentMethod");

                entity.Property(e => e.PaymentMethodId)
                    .HasColumnType("tinyint(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.PaymentMethodName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<TmPaymentStatus>(entity =>
            {
                entity.HasKey(e => e.PaymentStatusId)
                    .HasName("PRIMARY");

                entity.ToTable("TM_PaymentStatus");

                entity.Property(e => e.PaymentStatusId)
                    .HasColumnType("tinyint(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.PaymentStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TmProductStatus>(entity =>
            {
                entity.HasKey(e => e.ProductStatusId)
                    .HasName("PRIMARY");

                entity.ToTable("TM_ProductStatus");

                entity.Property(e => e.ProductStatusId)
                    .HasColumnType("tinyint(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TmShippingMethod>(entity =>
            {
                entity.HasKey(e => e.ShippingMethodId)
                    .HasName("PRIMARY");

                entity.ToTable("TM_ShippingMethod");

                entity.Property(e => e.ShippingMethodId)
                    .HasColumnType("tinyint(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ShippingMethodName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<TmShopStatus>(entity =>
            {
                entity.HasKey(e => e.ShopStatusId)
                    .HasName("PRIMARY");

                entity.ToTable("TM_ShopStatus");

                entity.Property(e => e.ShopStatusId)
                    .HasColumnType("tinyint(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ShopStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TmUserLevel>(entity =>
            {
                entity.HasKey(e => e.UserLevelId)
                    .HasName("PRIMARY");

                entity.ToTable("TM_UserLevel");

                entity.Property(e => e.UserLevelId)
                    .HasColumnType("tinyint(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserLevelName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TmUserStatus>(entity =>
            {
                entity.HasKey(e => e.UserStatusId)
                    .HasName("PRIMARY");

                entity.ToTable("TM_UserStatus");

                entity.Property(e => e.UserStatusId)
                    .HasColumnType("tinyint(4)")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
