using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "latin1");

            migrationBuilder.CreateTable(
                name: "T_Cart",
                columns: table => new
                {
                    CartId = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint(20)", nullable: false),
                    OrderId = table.Column<long>(type: "bigint(20)", nullable: true, comment: "Filed này sẽ update khi user create Order"),
                    ProductId = table.Column<long>(type: "bigint(20)", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false, comment: "Số lượng"),
                    Price = table.Column<decimal>(type: "decimal(20,6)", precision: 20, scale: 6, nullable: false),
                    DiscountRate = table.Column<double>(type: "double", nullable: false, comment: "Tỉ lệ chiết khấu"),
                    DiscountAmount = table.Column<decimal>(type: "decimal(20,6)", precision: 20, scale: 6, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(20,6)", precision: 20, scale: 6, nullable: false),
                    Note = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, defaultValueSql: "''", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    IsDelete = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValueSql: "b'0'"),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.CartId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_Category",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<ulong>(type: "bit(1)", nullable: false, comment: "Mỗi shop có category riêng"),
                    NameCategory = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    ParentId = table.Column<int>(type: "int(11)", nullable: true),
                    IsDelete = table.Column<ulong>(type: "bit(1)", nullable: false),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.CategoryId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_Customer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint(20)", nullable: false),
                    GenderId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    FullName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Mobile = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    CustomerTypeId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    IsDelete = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValueSql: "b'0'", comment: "Field này dùng để đánh dấu bản ghi đã bị xóa "),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.CustomerId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_Debt",
                columns: table => new
                {
                    DebtId = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint(20)", nullable: false),
                    ShopId = table.Column<long>(type: "bigint(20)", nullable: false),
                    OrderId = table.Column<int>(type: "int(11)", nullable: false),
                    Advance = table.Column<decimal>(type: "decimal(20,6)", precision: 20, scale: 6, nullable: true, comment: "Tiền tạm ứng"),
                    DeadlinedUtcDate = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Hạn thanh toán"),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.DebtId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_Order",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint(20)", nullable: false),
                    ShopId = table.Column<long>(type: "bigint(20)", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint(20)", nullable: false),
                    OrderStatusId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(20,6)", precision: 20, scale: 6, nullable: false),
                    TotalDiscountAmount = table.Column<decimal>(type: "decimal(20,6)", precision: 20, scale: 6, nullable: false),
                    PaymentMethodId = table.Column<decimal>(type: "decimal(20,6)", precision: 20, scale: 6, nullable: false),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.OrderId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_Payment",
                columns: table => new
                {
                    PaymentId = table.Column<long>(type: "bigint(20)", nullable: false),
                    OrderId = table.Column<long>(type: "bigint(20)", nullable: false),
                    PaymentStatusId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(20,6)", precision: 20, scale: 6, nullable: false),
                    Notes = table.Column<string>(type: "varchar(2500)", maxLength: 2500, nullable: false, defaultValueSql: "''", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PaymentId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_Product",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<long>(type: "bigint(20)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Price = table.Column<decimal>(type: "decimal(20,6)", precision: 20, scale: 6, nullable: false, comment: "Giá bán sản phẩm"),
                    Images = table.Column<string>(type: "json", nullable: false, defaultValueSql: "''", comment: "Các ảnh lưu dạng mảng json", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Properties = table.Column<string>(type: "json", nullable: false, defaultValueSql: "''", comment: "Các thông tin lưu dạng json tùy define từng shop", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Description = table.Column<string>(type: "varchar(2500)", maxLength: 2500, nullable: false, defaultValueSql: "''", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    ProductStatusId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    IsDelete = table.Column<ulong>(type: "bit(1)", nullable: false),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ProductId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_ProductCategory",
                columns: table => new
                {
                    ProductCategoryId = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<long>(type: "bigint(20)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint(20)", nullable: false),
                    IsDelete = table.Column<ulong>(type: "bit(1)", nullable: false),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ProductCategoryId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_ProductInventory",
                columns: table => new
                {
                    ProductInventoryId = table.Column<long>(type: "bigint(20)", nullable: false),
                    ProductId = table.Column<long>(type: "bigint(20)", nullable: false),
                    Qty = table.Column<int>(type: "int(11)", nullable: false, comment: "Số lượng"),
                    Price = table.Column<decimal>(type: "decimal(20,6)", precision: 20, scale: 6, nullable: false, comment: "Giá nhập kho"),
                    DiscountRate = table.Column<double>(type: "double", nullable: false, comment: "Tỷ lệ rate theo từng đợt nhập kho"),
                    Note = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, defaultValueSql: "'0'", comment: "Ghi chú về lần nhập kho này", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    IsDelete = table.Column<ulong>(type: "bit(1)", nullable: false),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ProductInventoryId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int(11)", nullable: false),
                    RoleName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    GrantAccess = table.Column<string>(type: "json", nullable: false, defaultValueSql: "''", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    IsSystemRole = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValueSql: "b'0'"),
                    IsDelete = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValueSql: "b'0'"),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_Shop",
                columns: table => new
                {
                    ShopId = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Avatar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    Description = table.Column<string>(type: "varchar(2500)", maxLength: 2500, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    IsDelete = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValueSql: "b'0'"),
                    ShopStatusId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    UserId = table.Column<long>(type: "bigint(20)", nullable: false),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ShopId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_ShopUser",
                columns: table => new
                {
                    ShopUserId = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<long>(type: "bigint(20)", nullable: false),
                    UserId = table.Column<long>(type: "bigint(20)", nullable: false),
                    RoleId = table.Column<int>(type: "int(11)", nullable: false),
                    IsDelete = table.Column<ulong>(type: "bit(1)", nullable: false),
                    CreatedUtcDate = table.Column<DateTime>(type: "date", nullable: false),
                    RetiredUtcDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ShopUserId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "T_User",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint(20)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Mobile = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    PasswordHash = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    FullName = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    GenderId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Avatar = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, defaultValueSql: "''", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1"),
                    UserStatusId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    RoleId = table.Column<int>(type: "int(11)", nullable: false),
                    IsDelete = table.Column<ulong>(type: "bit(1)", nullable: false, defaultValueSql: "b'0'"),
                    CreatedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedUtcDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "TM_CustomerType",
                columns: table => new
                {
                    CustomerLevelId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    CustomerLevelName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.CustomerLevelId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "TM_Gender",
                columns: table => new
                {
                    GenderId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    GenderName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GenderId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "TM_OrderStatus",
                columns: table => new
                {
                    OrderStatusId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    OrderStatusName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.OrderStatusId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "TM_PaymentMethod",
                columns: table => new
                {
                    PaymentMethodId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    PaymentMethodName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PaymentMethodId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "TM_PaymentStatus",
                columns: table => new
                {
                    PaymentStatusId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    PaymentStatusName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.PaymentStatusId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "TM_ProductStatus",
                columns: table => new
                {
                    ProductStatusId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    ProductStatusName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ProductStatusId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "TM_ShippingMethod",
                columns: table => new
                {
                    ShippingMethodId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    ShippingMethodName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''", collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ShippingMethodId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "TM_ShopStatus",
                columns: table => new
                {
                    ShopStatusId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    ShopStatusName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ShopStatusId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "TM_UserLevel",
                columns: table => new
                {
                    UserLevelId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    UserLevelName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.UserLevelId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");

            migrationBuilder.CreateTable(
                name: "TM_UserStatus",
                columns: table => new
                {
                    UserStatusId = table.Column<sbyte>(type: "tinyint(4)", nullable: false),
                    UserStatusName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "latin1_swedish_ci")
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.UserStatusId);
                })
                .Annotation("MySql:CharSet", "latin1")
                .Annotation("Relational:Collation", "latin1_swedish_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Cart");

            migrationBuilder.DropTable(
                name: "T_Category");

            migrationBuilder.DropTable(
                name: "T_Customer");

            migrationBuilder.DropTable(
                name: "T_Debt");

            migrationBuilder.DropTable(
                name: "T_Order");

            migrationBuilder.DropTable(
                name: "T_Payment");

            migrationBuilder.DropTable(
                name: "T_Product");

            migrationBuilder.DropTable(
                name: "T_ProductCategory");

            migrationBuilder.DropTable(
                name: "T_ProductInventory");

            migrationBuilder.DropTable(
                name: "T_Role");

            migrationBuilder.DropTable(
                name: "T_Shop");

            migrationBuilder.DropTable(
                name: "T_ShopUser");

            migrationBuilder.DropTable(
                name: "T_User");

            migrationBuilder.DropTable(
                name: "TM_CustomerType");

            migrationBuilder.DropTable(
                name: "TM_Gender");

            migrationBuilder.DropTable(
                name: "TM_OrderStatus");

            migrationBuilder.DropTable(
                name: "TM_PaymentMethod");

            migrationBuilder.DropTable(
                name: "TM_PaymentStatus");

            migrationBuilder.DropTable(
                name: "TM_ProductStatus");

            migrationBuilder.DropTable(
                name: "TM_ShippingMethod");

            migrationBuilder.DropTable(
                name: "TM_ShopStatus");

            migrationBuilder.DropTable(
                name: "TM_UserLevel");

            migrationBuilder.DropTable(
                name: "TM_UserStatus");
        }
    }
}
