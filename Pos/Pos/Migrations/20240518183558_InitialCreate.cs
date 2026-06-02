using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Landmark = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    City = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    State = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AlternateContactNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDefault = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ThresholdPoints = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Benefits = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    PointMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivery__3214EC071FE57639", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__3214EC07B9F69100", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NotifiableType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Data = table.Column<string>(type: "text", nullable: true),
                    ReadAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    NotifiableId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Position__3214EC07236A43FE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductBatch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BatchNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBatch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultJobSheetStatus = table.Column<string>(type: "text", nullable: true),
                    JobSheetNumberPrefix = table.Column<string>(type: "text", nullable: true),
                    DefaultRepairChecklist = table.Column<string>(type: "text", nullable: true),
                    ProductConfiguration = table.Column<string>(type: "text", nullable: true),
                    ProblemReportedByTheCustomer = table.Column<string>(type: "text", nullable: true),
                    ConditionOfTheProduct = table.Column<string>(type: "text", nullable: true),
                    XdayServiceWarranty = table.Column<string>(type: "text", nullable: true),
                    ServiceDisclaimers = table.Column<string>(type: "text", nullable: true),
                    RepairTermsConditions = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reward",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PointsRequired = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reward", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DomainName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ConnectionString = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SubscriptionType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SubscriptionExpiry = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warranty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warranty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Printer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CharactersPerLine = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PrinterIpAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PrinterPort = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Printer_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Opened = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    DeviceId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TerminalId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Register_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserLogin = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PinOne = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PinTwo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PinThree = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PinFour = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsAdmin = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Modele",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    DeviceId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modele", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modele_Brand",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modele_Device",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Part",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeviceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Part", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Part_Device",
                        column: x => x.DeviceID,
                        principalTable: "Device",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__3214EC07A403663E", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Employee__Depart__68343A95",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Employee__Positi__69285ECE",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductFieldValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "text", nullable: true),
                    ProductFieldId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFieldValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFieldValue_ProductField",
                        column: x => x.ProductFieldId,
                        principalTable: "ProductField",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleHasPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_has_permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_has_permissions_permissions",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_has_permissions_roles",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariationValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    VariationID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariationValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariationValue_Variation",
                        column: x => x.VariationID,
                        principalTable: "Variation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhatsAppStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    accountSid = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    authToken = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FromPhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WhatsAppMaintStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WhatsAppMaintInvoiceStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WhatsAppPhoneCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MailMailer = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MailStatus = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MailAllowHtml = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MailHost = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MailPort = table.Column<int>(type: "int", nullable: true),
                    MailUsername = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MailPassword = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PurchaseCodeCondition = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PurchaseCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Logo = table.Column<byte[]>(type: "image", nullable: true),
                    IsLockScreen = table.Column<bool>(type: "bit", nullable: true),
                    LockScreenImg = table.Column<byte[]>(type: "image", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    SubTitle = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Compte = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Rib = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Nis = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Rc = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Ai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IdFiscal = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsSoundAdded = table.Column<bool>(type: "bit", nullable: true),
                    IsSoundDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsSoundSelected = table.Column<bool>(type: "bit", nullable: true),
                    IsSoundDenied = table.Column<bool>(type: "bit", nullable: true),
                    IsSoundWrong = table.Column<bool>(type: "bit", nullable: true),
                    PrinterReciept = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PrinterRecieptId = table.Column<int>(type: "int", nullable: true),
                    PrinterDocument = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PrinterDocumentId = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setting_Printer",
                        column: x => x.PrinterRecieptId,
                        principalTable: "Printer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Setting_Printer1",
                        column: x => x.PrinterDocumentId,
                        principalTable: "Printer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Adjustment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Doc = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TotalQty = table.Column<int>(type: "int", nullable: true),
                    Item = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjustment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adjustment_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Adjustment_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuditTrail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TableName = table.Column<string>(type: "text", nullable: true),
                    ActionType = table.Column<string>(type: "text", nullable: true),
                    KeyValues = table.Column<string>(type: "text", nullable: true),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: false),
                    ChangeTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditTrail_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    TotalPointsAccumulated = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentPoints = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentDue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TierLevelId = table.Column<int>(type: "int", nullable: true),
                    PriceGroup = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_CustomerTier",
                        column: x => x.TierLevelId,
                        principalTable: "CustomerTier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_PriceGroup",
                        column: x => x.PriceGroup,
                        principalTable: "PriceGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customer_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    ExpenseCategoryId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_ExpenseCategory",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expense_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expense_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NbrProducts = table.Column<int>(type: "int", nullable: true),
                    InventoryMonth = table.Column<int>(type: "int", nullable: true),
                    InventoryYear = table.Column<int>(type: "int", nullable: true),
                    DiffAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Gap = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Sku = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BarcodeType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProductDescription = table.Column<string>(type: "text", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    DeviceId = table.Column<int>(type: "int", nullable: true),
                    WarrantyId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProductType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    InitialQuantity = table.Column<int>(type: "int", nullable: true),
                    AlertQuantity = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    PurchasePriceExcTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PurchasePriceIncTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Xmargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SellingPriceTaxType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Upc = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Isbn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UnitPerBox = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NumberOfBox = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Device",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Unit1",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Warranty",
                        column: x => x.WarrantyId,
                        principalTable: "Warranty",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RegisterRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true),
                    TotalCashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalCashSubmitted = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalCheques = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalChequesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalChequesSubmitted = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalOtherAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalRefundsAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalExpensesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalGiftCardAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalReturnOrdersAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CashInHand = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClosedById = table.Column<int>(type: "int", nullable: true),
                    ClosedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    TransferredToId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisterRecord_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegisterRecord_Register",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegisterRecord_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegisterRecord_User1",
                        column: x => x.ClosedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegisterRecord_User2",
                        column: x => x.TransferredToId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technical",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PinOne = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PinTwo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PinThree = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PinFour = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technical_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FromWarehouseId = table.Column<int>(type: "int", nullable: true),
                    ToWarehouseId = table.Column<int>(type: "int", nullable: true),
                    Item = table.Column<int>(type: "int", nullable: true),
                    TotalQty = table.Column<int>(type: "int", nullable: true),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Doc = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfer_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transfer_Warehouse",
                        column: x => x.FromWarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transfer_Warehouse1",
                        column: x => x.ToWarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserHasPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_has_permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_has_permissions_permissions",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_has_permissions_users",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserHasRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_has_roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_has_roles_roles",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_has_roles_users",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Make = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Capacity = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Vehicles__3214EC0782C057C9", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RepairChecklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PartId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairChecklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairChecklist_Part",
                        column: x => x.PartId,
                        principalTable: "Part",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    TimeIn = table.Column<TimeOnly>(type: "time", nullable: true),
                    TimeOut = table.Column<TimeOnly>(type: "time", nullable: true),
                    HoursWorked = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Attendan__3214EC07EEEB981D", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Attendanc__Emplo__6DED13EB",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payroll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PayPeriodStart = table.Column<DateOnly>(type: "date", nullable: true),
                    PayPeriodEnd = table.Column<DateOnly>(type: "date", nullable: true),
                    GrossPay = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    NetPay = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Deductions = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Overtime = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payroll__3214EC0701774F48", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payroll_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Payroll__Employe__72B1C908",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Waste",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    TotalLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Items = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Waste_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Waste_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Waste_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActivityLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PointsChanged = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLog_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bank_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bank_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bank_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Redemption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RedemptionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PointsSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    RewardId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Redemption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Redemption_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Redemption_Reward",
                        column: x => x.RewardId,
                        principalTable: "Reward",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RewardHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateClaimed = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    RewardId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RewardHistory_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RewardHistory_Reward",
                        column: x => x.RewardId,
                        principalTable: "Reward",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SaleDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SaleStatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DiscountType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdditionalNotes = table.Column<string>(type: "text", nullable: true),
                    ShippingDetails = table.Column<string>(type: "text", nullable: true),
                    AdditionalShippingCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentSatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NumberItems = table.Column<int>(type: "int", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Due = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RemainingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReturnAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    SaleYear = table.Column<int>(type: "int", nullable: true),
                    SaleMonth = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sale_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sale_Hold_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleHold",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SaleDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SaleStatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DiscountType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdditionalNotes = table.Column<string>(type: "text", nullable: true),
                    ShippingDetails = table.Column<string>(type: "text", nullable: true),
                    AdditionalShippingCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentSatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NumberItems = table.Column<int>(type: "int", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Due = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReturnAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    SaleYear = table.Column<int>(type: "int", nullable: true),
                    SaleMonth = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale_Hold", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleHold_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleHold_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleHold_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleHold_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PointsEarned = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PointsUsed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TheoricalStock = table.Column<int>(type: "int", nullable: true),
                    PhysicalStock = table.Column<int>(type: "int", nullable: true),
                    Gap = table.Column<int>(type: "int", nullable: true),
                    DiffAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    InventoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryItem_Inventory",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryItem_Product",
                        column: x => x.ItemId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductAdjustment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AdjustmentId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdjustment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAdjustment_Adjustment",
                        column: x => x.AdjustmentId,
                        principalTable: "Adjustment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAdjustment_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAdjustment_Variation",
                        column: x => x.VariantId,
                        principalTable: "Variation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductBarCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBarCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductBarCode_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductHasField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    ProductFieldId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHasField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHasField_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductHasField_ProductField",
                        column: x => x.ProductFieldId,
                        principalTable: "ProductField",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupPrice = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    PriceGroupId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrice_PriceGroup",
                        column: x => x.PriceGroupId,
                        principalTable: "PriceGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductPrice_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductVariant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    VariationId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariant_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariant_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductVariant_Variation",
                        column: x => x.VariationId,
                        principalTable: "Variation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductVariantValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sku = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PePriceExc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PePriceInc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    VariationId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariantValue_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductVariantValue_Variation",
                        column: x => x.VariationId,
                        principalTable: "Variation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductWarehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeiNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductBatchId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWarehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductWarehouse_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductWarehouse_ProductBatch",
                        column: x => x.ProductBatchId,
                        principalTable: "ProductBatch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductWarehouse_Variation",
                        column: x => x.VariantId,
                        principalTable: "Variation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductWarehouse_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PromotionCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotion_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Promotion_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PurchaseStatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DiscountType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdditionalNotes = table.Column<string>(type: "text", nullable: true),
                    ShippingDetails = table.Column<string>(type: "text", nullable: true),
                    AdditionalShippingCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentSatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NumberItems = table.Column<int>(type: "int", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Due = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReturnAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    PurchaseYear = table.Column<int>(type: "int", nullable: true),
                    PurchaseMonth = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchase_Supplier",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchase_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchase_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PasswordPatternLock = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CommentByTechnician = table.Column<string>(type: "text", nullable: true),
                    ProductConfiguration = table.Column<string>(type: "text", nullable: true),
                    ProblemReportedByTheCustomer = table.Column<string>(type: "text", nullable: true),
                    ConditionOfTheProduct = table.Column<string>(type: "text", nullable: true),
                    EstimatedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateOfReceipt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpectedDateOfExamination = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExplainProblem = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true),
                    PartsUsed = table.Column<string>(type: "text", nullable: true),
                    MaintenanceRepairChecklist = table.Column<string>(type: "text", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ModeleId = table.Column<int>(type: "int", nullable: true),
                    DeviceId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    TechnicalId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenance_Brand",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maintenance_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maintenance_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maintenance_Device",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maintenance_Modele",
                        column: x => x.ModeleId,
                        principalTable: "Modele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maintenance_Technical",
                        column: x => x.TechnicalId,
                        principalTable: "Technical",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maintenance_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    InvoiceDate = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    InvoiceStatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DiscountType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdditionalNotes = table.Column<string>(type: "text", nullable: true),
                    ShippingDetails = table.Column<string>(type: "text", nullable: true),
                    AdditionalShippingCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentSatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NumberItems = table.Column<int>(type: "int", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Due = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReturnAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    InvoiceYear = table.Column<int>(type: "int", nullable: true),
                    InvoiceMonth = table.Column<int>(type: "int", nullable: true),
                    RepairCompletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PartsUsed = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    TechnicalId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    DeviceId = table.Column<int>(type: "int", nullable: true),
                    ModelId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairInvoice_Brand",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RepairInvoice_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RepairInvoice_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RepairInvoice_Device",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RepairInvoice_Modele",
                        column: x => x.ModelId,
                        principalTable: "Modele",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RepairInvoice_Technical",
                        column: x => x.TechnicalId,
                        principalTable: "Technical",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RepairInvoice_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RepairInvoice_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TodoList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TechnicalId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoList_Technical",
                        column: x => x.TechnicalId,
                        principalTable: "Technical",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TodoList_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductTransfer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductBatchId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    ImeiNumber = table.Column<string>(type: "text", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    PurchaseUnitId = table.Column<int>(type: "int", nullable: true),
                    NetUnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTransfer_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTransfer_ProductBatch",
                        column: x => x.ProductBatchId,
                        principalTable: "ProductBatch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTransfer_Transfer",
                        column: x => x.TransferId,
                        principalTable: "Transfer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTransfer_Variation",
                        column: x => x.VariantId,
                        principalTable: "Variation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryRouteId = table.Column<int>(type: "int", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeliveryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EstimatedTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    ActualTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Deliveri__3214EC07C1667F4C", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Deliveries_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Deliverie__Deliv__1F846F7F",
                        column: x => x.DeliveryRouteId,
                        principalTable: "DeliveryRoutes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Deliverie__Vehic__207893B8",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WasteItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    WasteAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastPurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WasteId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WasteItem_Product",
                        column: x => x.WasteId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WasteItem_Waste",
                        column: x => x.WasteId,
                        principalTable: "Waste",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddBank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankTransaction_Bank",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankTransaction_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankTransfer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CurrentBankId = table.Column<int>(type: "int", nullable: true),
                    ToBankId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankTransfer_Bank",
                        column: x => x.CurrentBankId,
                        principalTable: "Bank",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankTransfer_Bank1",
                        column: x => x.ToBankId,
                        principalTable: "Bank",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankTransfer_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Return",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SaleId = table.Column<int>(type: "int", nullable: true),
                    CashRegisterId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    Item = table.Column<int>(type: "int", nullable: true),
                    TotalQty = table.Column<int>(type: "int", nullable: true),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderTaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Doc = table.Column<string>(type: "text", nullable: true),
                    ReturnNote = table.Column<string>(type: "text", nullable: true),
                    StaffNote = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Return", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Return_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Return_RegisterRecord",
                        column: x => x.CashRegisterId,
                        principalTable: "RegisterRecord",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Return_Sale",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Return_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Return_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SaleQuantity = table.Column<int>(type: "int", nullable: true),
                    UnitCostBd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitCostBt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitSellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProfitMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    SaleId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleDetail_Hold_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleDetail_Hold_Sale",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SalePayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    BusinessLocationId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Due = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalePayment_BusinessLocation",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalePayment_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalePayment_Sale",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalePayment_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SaleDetailHold",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SaleQuantity = table.Column<int>(type: "int", nullable: true),
                    UnitCostBd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitCostBt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitSellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProfitMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    SaleId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetail_Hold", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleDetailHold_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SaleDetailHold_SaleHold",
                        column: x => x.SaleId,
                        principalTable: "SaleHold",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PurchaseQuantity = table.Column<int>(type: "int", nullable: true),
                    UnitCostBd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitCostBt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitSellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProfitMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseDetail_Purchase",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReturnPurchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    CashRegisterId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    Item = table.Column<int>(type: "int", nullable: true),
                    TotalQty = table.Column<int>(type: "int", nullable: true),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderTaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Doc = table.Column<string>(type: "text", nullable: true),
                    ReturnNote = table.Column<string>(type: "text", nullable: true),
                    StaffNote = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnPurchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnPurchase_Purchase",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReturnPurchase_RegisterRecord",
                        column: x => x.CashRegisterId,
                        principalTable: "RegisterRecord",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReturnPurchase_Supplier",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReturnPurchase_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReturnPurchase_Warehouse",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceRepairChecklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceId = table.Column<int>(type: "int", nullable: true),
                    RepairChecklistId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRepairChecklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceRepairChecklist_Maintenance",
                        column: x => x.MaintenanceId,
                        principalTable: "Maintenance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaintenanceRepairChecklist_RepairChecklist",
                        column: x => x.RepairChecklistId,
                        principalTable: "RepairChecklist",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceRepairChecklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    RepairChecklistId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRepairChecklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceRepairChecklist_RepairChecklist",
                        column: x => x.RepairChecklistId,
                        principalTable: "RepairChecklist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceRepairChecklist_RepairInvoice",
                        column: x => x.InvoiceId,
                        principalTable: "RepairInvoice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RepairInvoiceDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    UnitCostBd = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitCostBt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitSellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProfitMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    RepairInvoiceId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairInvoiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairInvoiceDetail_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RepairInvoiceDetail_RepairInvoice",
                        column: x => x.RepairInvoiceId,
                        principalTable: "RepairInvoice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    UnitWeight = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TotalWeight = table.Column<decimal>(type: "decimal(21,2)", nullable: true, computedColumnSql: "([Quantity]*[UnitWeight])", stored: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivery__3214EC072ED98A9C", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryItems_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__DeliveryI__Deliv__253D48D5",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductReturn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductBatchId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    ImeiNumber = table.Column<string>(type: "text", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    NetUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReturn_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductReturn_ProductBatch",
                        column: x => x.ProductBatchId,
                        principalTable: "ProductBatch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductReturn_Return",
                        column: x => x.ReturnId,
                        principalTable: "Return",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductReturn_Variation",
                        column: x => x.VariantId,
                        principalTable: "Variation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductPurchaseReturn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductBatchId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    ImeiNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: true),
                    NetUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPurchaseReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPurchaseReturn_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductPurchaseReturn_ProductBatch",
                        column: x => x.ProductBatchId,
                        principalTable: "ProductBatch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductPurchaseReturn_ReturnPurchase1",
                        column: x => x.ReturnId,
                        principalTable: "ReturnPurchase",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_CustomerId",
                table: "ActivityLog",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Adjustment_UserId",
                table: "Adjustment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Adjustment_WarehouseId",
                table: "Adjustment",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EmployeeId",
                table: "Attendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_UserId",
                table: "Attendance",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrail_UserId",
                table: "AuditTrail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_BusinessLocationId",
                table: "Bank",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_CustomerId",
                table: "Bank",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_UserId",
                table: "Bank",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_BankId",
                table: "BankTransaction",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_UserId",
                table: "BankTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransfer_CurrentBankId",
                table: "BankTransfer",
                column: "CurrentBankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransfer_ToBankId",
                table: "BankTransfer",
                column: "ToBankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransfer_UserId",
                table: "BankTransfer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PriceGroup",
                table: "Customer",
                column: "PriceGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_TierLevelId",
                table: "Customer",
                column: "TierLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserID",
                table: "Customer",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DeliveryRouteId",
                table: "Deliveries",
                column: "DeliveryRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_EmployeeId",
                table: "Deliveries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_UserId",
                table: "Deliveries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_VehicleId",
                table: "Deliveries",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_DeliveryId",
                table: "DeliveryItems",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_ProductId",
                table: "DeliveryItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PositionId",
                table: "Employee",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ExpenseCategoryId",
                table: "Expense",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_UserId",
                table: "Expense",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_WarehouseId",
                table: "Expense",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_UserId",
                table: "Inventory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_WarehouseId",
                table: "Inventory",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_InventoryId",
                table: "InventoryItem",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_ItemId",
                table: "InventoryItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRepairChecklist_InvoiceId",
                table: "InvoiceRepairChecklist",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRepairChecklist_RepairChecklistId",
                table: "InvoiceRepairChecklist",
                column: "RepairChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_BrandId",
                table: "Maintenance",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_BusinessLocationId",
                table: "Maintenance",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_CustomerId",
                table: "Maintenance",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_DeviceId",
                table: "Maintenance",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_ModeleId",
                table: "Maintenance",
                column: "ModeleId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_TechnicalId",
                table: "Maintenance",
                column: "TechnicalId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_UserId",
                table: "Maintenance",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRepairChecklist_MaintenanceId",
                table: "MaintenanceRepairChecklist",
                column: "MaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRepairChecklist_RepairChecklistId",
                table: "MaintenanceRepairChecklist",
                column: "RepairChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Modele_BrandId",
                table: "Modele",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Modele_DeviceId",
                table: "Modele",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Part_DeviceID",
                table: "Part",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_EmployeeId",
                table: "Payroll",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_UserId",
                table: "Payroll",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Printer_BusinessLocationId",
                table: "Printer",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BusinessLocationId",
                table: "Product",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeviceId",
                table: "Product",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitId",
                table: "Product",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UserId",
                table: "Product",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_WarehouseId",
                table: "Product",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_WarrantyId",
                table: "Product",
                column: "WarrantyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdjustment_AdjustmentId",
                table: "ProductAdjustment",
                column: "AdjustmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdjustment_ProductId",
                table: "ProductAdjustment",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdjustment_VariantId",
                table: "ProductAdjustment",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBarCode_ProductId",
                table: "ProductBarCode",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFieldValue_ProductFieldId",
                table: "ProductFieldValue",
                column: "ProductFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHasField_ProductFieldId",
                table: "ProductHasField",
                column: "ProductFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHasField_ProductId",
                table: "ProductHasField",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrice_PriceGroupId",
                table: "ProductPrice",
                column: "PriceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrice_ProductId",
                table: "ProductPrice",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchaseReturn_ProductBatchId",
                table: "ProductPurchaseReturn",
                column: "ProductBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchaseReturn_ProductId",
                table: "ProductPurchaseReturn",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchaseReturn_ReturnId",
                table: "ProductPurchaseReturn",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturn_ProductBatchId",
                table: "ProductReturn",
                column: "ProductBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturn_ProductId",
                table: "ProductReturn",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturn_ReturnId",
                table: "ProductReturn",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturn_VariantId",
                table: "ProductReturn",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfer_ProductBatchId",
                table: "ProductTransfer",
                column: "ProductBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfer_ProductId",
                table: "ProductTransfer",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfer_TransferId",
                table: "ProductTransfer",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfer_VariantId",
                table: "ProductTransfer",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariant",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_VariationId",
                table: "ProductVariant",
                column: "VariationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantValue_ProductId",
                table: "ProductVariantValue",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantValue_VariationId",
                table: "ProductVariantValue",
                column: "VariationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_ProductBatchId",
                table: "ProductWarehouse",
                column: "ProductBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_ProductId",
                table: "ProductWarehouse",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_VariantId",
                table: "ProductWarehouse",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_WarehouseId",
                table: "ProductWarehouse",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_ProductId",
                table: "Promotion",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_UserId",
                table: "Promotion",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_BusinessLocationId",
                table: "Purchase",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_SupplierId",
                table: "Purchase",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_UserId",
                table: "Purchase",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_WarehouseId",
                table: "Purchase",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetail_PurchaseId",
                table: "PurchaseDetail",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Redemption_CustomerId",
                table: "Redemption",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Redemption_RewardId",
                table: "Redemption",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_BusinessLocationId",
                table: "Register",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterRecord_BusinessLocationId",
                table: "RegisterRecord",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterRecord_ClosedById",
                table: "RegisterRecord",
                column: "ClosedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterRecord_RegisterId",
                table: "RegisterRecord",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterRecord_TransferredToId",
                table: "RegisterRecord",
                column: "TransferredToId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterRecord_UserId",
                table: "RegisterRecord",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairChecklist_PartId",
                table: "RepairChecklist",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInvoice_BrandId",
                table: "RepairInvoice",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInvoice_BusinessLocationId",
                table: "RepairInvoice",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInvoice_CustomerId",
                table: "RepairInvoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInvoice_DeviceId",
                table: "RepairInvoice",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInvoice_ModelId",
                table: "RepairInvoice",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInvoice_TechnicalId",
                table: "RepairInvoice",
                column: "TechnicalId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInvoice_UserId",
                table: "RepairInvoice",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInvoice_WarehouseId",
                table: "RepairInvoice",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInvoiceDetail_ProductId",
                table: "RepairInvoiceDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairInvoiceDetail_RepairInvoiceId",
                table: "RepairInvoiceDetail",
                column: "RepairInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_CashRegisterId",
                table: "Return",
                column: "CashRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_CustomerId",
                table: "Return",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_SaleId",
                table: "Return",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_UserId",
                table: "Return",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Return_WarehouseId",
                table: "Return",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnPurchase_CashRegisterId",
                table: "ReturnPurchase",
                column: "CashRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnPurchase_PurchaseId",
                table: "ReturnPurchase",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnPurchase_SupplierId",
                table: "ReturnPurchase",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnPurchase_UserId",
                table: "ReturnPurchase",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnPurchase_WarehouseId",
                table: "ReturnPurchase",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_RewardHistory_CustomerId",
                table: "RewardHistory",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RewardHistory_RewardId",
                table: "RewardHistory",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleHasPermission_PermissionId",
                table: "RoleHasPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleHasPermission_RoleId",
                table: "RoleHasPermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_BusinessLocationId",
                table: "Sale",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerId",
                table: "Sale",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_WarehouseId",
                table: "Sale",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_ProductId",
                table: "SaleDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_SaleId",
                table: "SaleDetail",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetailHold_ProductId",
                table: "SaleDetailHold",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetailHold_SaleId",
                table: "SaleDetailHold",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleHold_BusinessLocationId",
                table: "SaleHold",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleHold_CustomerId",
                table: "SaleHold",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleHold_UserId",
                table: "SaleHold",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleHold_WarehouseId",
                table: "SaleHold",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SalePayment_BusinessLocationId",
                table: "SalePayment",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SalePayment_CustomerId",
                table: "SalePayment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalePayment_SaleId",
                table: "SalePayment",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_SalePayment_UserId",
                table: "SalePayment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_PrinterDocumentId",
                table: "Setting",
                column: "PrinterDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_PrinterRecieptId",
                table: "Setting",
                column: "PrinterRecieptId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_UserID",
                table: "Supplier",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Technical_UserID",
                table: "Technical",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TodoList_TechnicalId",
                table: "TodoList",
                column: "TechnicalId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoList_UserId",
                table: "TodoList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_FromWarehouseId",
                table: "Transfer",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_ToWarehouseId",
                table: "Transfer",
                column: "ToWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_UserId",
                table: "Transfer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BusinessLocationId",
                table: "User",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHasPermission_PermissionId",
                table: "UserHasPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHasPermission_UserId",
                table: "UserHasPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHasRole_RoleId",
                table: "UserHasRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHasRole_UserId",
                table: "UserHasRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VariationValue_VariationID",
                table: "VariationValue",
                column: "VariationID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Waste_BusinessLocationId",
                table: "Waste",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Waste_EmployeeId",
                table: "Waste",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Waste_UserId",
                table: "Waste",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteItem_WasteId",
                table: "WasteItem",
                column: "WasteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLog");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "AuditTrail");

            migrationBuilder.DropTable(
                name: "BankTransaction");

            migrationBuilder.DropTable(
                name: "BankTransfer");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "DeliveryItems");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "InventoryItem");

            migrationBuilder.DropTable(
                name: "InvoiceRepairChecklist");

            migrationBuilder.DropTable(
                name: "MaintenanceRepairChecklist");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Payroll");

            migrationBuilder.DropTable(
                name: "ProductAdjustment");

            migrationBuilder.DropTable(
                name: "ProductBarCode");

            migrationBuilder.DropTable(
                name: "ProductFieldValue");

            migrationBuilder.DropTable(
                name: "ProductHasField");

            migrationBuilder.DropTable(
                name: "ProductPrice");

            migrationBuilder.DropTable(
                name: "ProductPurchaseReturn");

            migrationBuilder.DropTable(
                name: "ProductReturn");

            migrationBuilder.DropTable(
                name: "ProductTransfer");

            migrationBuilder.DropTable(
                name: "ProductVariant");

            migrationBuilder.DropTable(
                name: "ProductVariantValue");

            migrationBuilder.DropTable(
                name: "ProductWarehouse");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "PurchaseDetail");

            migrationBuilder.DropTable(
                name: "Redemption");

            migrationBuilder.DropTable(
                name: "RepairInvoiceDetail");

            migrationBuilder.DropTable(
                name: "RepairSetting");

            migrationBuilder.DropTable(
                name: "RewardHistory");

            migrationBuilder.DropTable(
                name: "RoleHasPermission");

            migrationBuilder.DropTable(
                name: "SaleDetail");

            migrationBuilder.DropTable(
                name: "SaleDetailHold");

            migrationBuilder.DropTable(
                name: "SalePayment");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "TodoList");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "UserHasPermission");

            migrationBuilder.DropTable(
                name: "UserHasRole");

            migrationBuilder.DropTable(
                name: "VariationValue");

            migrationBuilder.DropTable(
                name: "WasteItem");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "ExpenseCategory");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "RepairChecklist");

            migrationBuilder.DropTable(
                name: "Adjustment");

            migrationBuilder.DropTable(
                name: "ProductField");

            migrationBuilder.DropTable(
                name: "ReturnPurchase");

            migrationBuilder.DropTable(
                name: "Return");

            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropTable(
                name: "ProductBatch");

            migrationBuilder.DropTable(
                name: "RepairInvoice");

            migrationBuilder.DropTable(
                name: "Reward");

            migrationBuilder.DropTable(
                name: "SaleHold");

            migrationBuilder.DropTable(
                name: "Printer");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Variation");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Waste");

            migrationBuilder.DropTable(
                name: "DeliveryRoutes");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Part");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "RegisterRecord");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Modele");

            migrationBuilder.DropTable(
                name: "Technical");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Warranty");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "CustomerTier");

            migrationBuilder.DropTable(
                name: "PriceGroup");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BusinessLocation");
        }
    }
}
