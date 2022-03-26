using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PinewoodGrow.Data.GMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullAddress = table.Column<string>(maxLength: 150, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    PlaceID = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dietaries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dietaries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GroceryStores",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    FullAddress = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryStores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Illnesses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Illnesses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(maxLength: 99, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Situations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Situations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TempAddresses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullAddress = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    PlaceID = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempAddresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Households",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FamilySize = table.Column<int>(nullable: false),
                    FamilyName = table.Column<string>(nullable: false),
                    Dependants = table.Column<int>(nullable: false),
                    IsFixedAddress = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AddressID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Households", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Households_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TravelDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroceryDistance = table.Column<double>(nullable: false),
                    GroceryDrive = table.Column<double>(nullable: false),
                    GroceryBike = table.Column<double>(nullable: false),
                    GroceryWalk = table.Column<double>(nullable: false),
                    GrowDistance = table.Column<double>(nullable: false),
                    GrowDrive = table.Column<double>(nullable: false),
                    GrowBike = table.Column<double>(nullable: false),
                    GrowWalk = table.Column<double>(nullable: false),
                    GroceryID = table.Column<string>(nullable: true),
                    AddressID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TravelDetails_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelDetails_GroceryStores_GroceryID",
                        column: x => x.GroceryID,
                        principalTable: "GroceryStores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 99, nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    ProductTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TempHouseholds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FamilySize = table.Column<int>(nullable: false),
                    FamilyName = table.Column<string>(nullable: false),
                    Dependants = table.Column<int>(nullable: false),
                    IsFixedAddress = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AddressID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempHouseholds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TempHouseholds_TempAddresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "TempAddresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    DOB = table.Column<DateTime>(nullable: true),
                    Telephone = table.Column<string>(maxLength: 11, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Income = table.Column<double>(nullable: false),
                    Notes = table.Column<string>(maxLength: 2000, nullable: true),
                    Consent = table.Column<bool>(nullable: false),
                    VolunteerID = table.Column<int>(nullable: false),
                    CompletedOn = table.Column<DateTime>(nullable: false),
                    HouseholdID = table.Column<int>(nullable: false),
                    GenderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Members_Genders_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Genders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Members_Households_HouseholdID",
                        column: x => x.HouseholdID,
                        principalTable: "Households",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Members_Volunteers_VolunteerID",
                        column: x => x.VolunteerID,
                        principalTable: "Volunteers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductUnitPrices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductPrice = table.Column<double>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUnitPrices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductUnitPrices_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TempMembers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Income = table.Column<double>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Consent = table.Column<bool>(nullable: false),
                    VolunteerID = table.Column<int>(nullable: true),
                    CompletedOn = table.Column<DateTime>(nullable: false),
                    TempHouseholdID = table.Column<int>(nullable: true),
                    HouseholdID = table.Column<int>(nullable: true),
                    GenderID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMembers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TempMembers_Genders_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Genders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TempMembers_Households_HouseholdID",
                        column: x => x.HouseholdID,
                        principalTable: "Households",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TempMembers_TempHouseholds_TempHouseholdID",
                        column: x => x.TempHouseholdID,
                        principalTable: "TempHouseholds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TempMembers_Volunteers_VolunteerID",
                        column: x => x.VolunteerID,
                        principalTable: "Volunteers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberDietaries",
                columns: table => new
                {
                    DietaryID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberDietaries", x => new { x.DietaryID, x.MemberID });
                    table.ForeignKey(
                        name: "FK_MemberDietaries_Dietaries_DietaryID",
                        column: x => x.DietaryID,
                        principalTable: "Dietaries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberDietaries_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberHouseholds",
                columns: table => new
                {
                    MemberID = table.Column<int>(nullable: false),
                    HouseholdID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberHouseholds", x => new { x.MemberID, x.HouseholdID });
                    table.ForeignKey(
                        name: "FK_MemberHouseholds_Households_HouseholdID",
                        column: x => x.HouseholdID,
                        principalTable: "Households",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberHouseholds_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberIllnesses",
                columns: table => new
                {
                    IllnessID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberIllnesses", x => new { x.IllnessID, x.MemberID });
                    table.ForeignKey(
                        name: "FK_MemberIllnesses_Illnesses_IllnessID",
                        column: x => x.IllnessID,
                        principalTable: "Illnesses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberIllnesses_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberSituations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SituationID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false),
                    SituationIncome = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSituations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MemberSituations_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberSituations_Situations_SituationID",
                        column: x => x.SituationID,
                        principalTable: "Situations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransDate = table.Column<DateTime>(nullable: false),
                    NetTotal = table.Column<double>(nullable: false),
                    MemberID = table.Column<int>(nullable: false),
                    VolunteerID = table.Column<int>(nullable: false),
                    PaymentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sales_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Volunteers_VolunteerID",
                        column: x => x.VolunteerID,
                        principalTable: "Volunteers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductTypeID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    ProductUnitPriceID = table.Column<int>(nullable: false),
                    CompletedOn = table.Column<DateTime>(nullable: false),
                    HouseholdID = table.Column<int>(nullable: false),
                    VolunteerID = table.Column<int>(nullable: false),
                    PaymentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Receipts_Households_HouseholdID",
                        column: x => x.HouseholdID,
                        principalTable: "Households",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_ProductTypes_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_ProductUnitPrices_ProductUnitPriceID",
                        column: x => x.ProductUnitPriceID,
                        principalTable: "ProductUnitPrices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Volunteers_VolunteerID",
                        column: x => x.VolunteerID,
                        principalTable: "Volunteers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempMemberDietaries",
                columns: table => new
                {
                    DietaryID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMemberDietaries", x => new { x.DietaryID, x.MemberID });
                    table.ForeignKey(
                        name: "FK_TempMemberDietaries_Dietaries_DietaryID",
                        column: x => x.DietaryID,
                        principalTable: "Dietaries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempMemberDietaries_TempMembers_MemberID",
                        column: x => x.MemberID,
                        principalTable: "TempMembers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempMemberHouseholds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberID = table.Column<int>(nullable: true),
                    HouseholdID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMemberHouseholds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TempMemberHouseholds_TempHouseholds_HouseholdID",
                        column: x => x.HouseholdID,
                        principalTable: "TempHouseholds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TempMemberHouseholds_TempMembers_MemberID",
                        column: x => x.MemberID,
                        principalTable: "TempMembers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TempMemberIllnesses",
                columns: table => new
                {
                    IllnessID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMemberIllnesses", x => new { x.IllnessID, x.MemberID });
                    table.ForeignKey(
                        name: "FK_TempMemberIllnesses_Illnesses_IllnessID",
                        column: x => x.IllnessID,
                        principalTable: "Illnesses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempMemberIllnesses_TempMembers_MemberID",
                        column: x => x.MemberID,
                        principalTable: "TempMembers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempMemberSituations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SituationID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false),
                    SituationIncome = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempMemberSituations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TempMemberSituations_TempMembers_MemberID",
                        column: x => x.MemberID,
                        principalTable: "TempMembers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempMemberSituations_Situations_SituationID",
                        column: x => x.SituationID,
                        principalTable: "Situations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(maxLength: 255, nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    MemberID = table.Column<int>(nullable: true),
                    TempMemberDocument_MemberID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_TempMembers_TempMemberDocument_MemberID",
                        column: x => x.TempMemberDocument_MemberID,
                        principalTable: "TempMembers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleDetails",
                columns: table => new
                {
                    SaleID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    ID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetails", x => new { x.SaleID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_SaleDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetails_Sales_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sales",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    ProductUnitPriceID = table.Column<int>(nullable: false),
                    ReceiptID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Invoices_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_ProductUnitPrices_ProductUnitPriceID",
                        column: x => x.ProductUnitPriceID,
                        principalTable: "ProductUnitPrices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Receipts_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Receipts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileContent",
                columns: table => new
                {
                    FileContentID = table.Column<int>(nullable: false),
                    Content = table.Column<byte[]>(nullable: true),
                    MimeType = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContent", x => x.FileContentID);
                    table.ForeignKey(
                        name: "FK_FileContent_UploadedFiles_FileContentID",
                        column: x => x.FileContentID,
                        principalTable: "UploadedFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PlaceID",
                table: "Addresses",
                column: "PlaceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Households_AddressID",
                table: "Households",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Households_ID",
                table: "Households",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProductID",
                table: "Invoices",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProductUnitPriceID",
                table: "Invoices",
                column: "ProductUnitPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReceiptID",
                table: "Invoices",
                column: "ReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberDietaries_MemberID",
                table: "MemberDietaries",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberHouseholds_HouseholdID",
                table: "MemberHouseholds",
                column: "HouseholdID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberIllnesses_MemberID",
                table: "MemberIllnesses",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_GenderID",
                table: "Members",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_HouseholdID",
                table: "Members",
                column: "HouseholdID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_VolunteerID",
                table: "Members",
                column: "VolunteerID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSituations_MemberID",
                table: "MemberSituations",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSituations_SituationID_MemberID",
                table: "MemberSituations",
                columns: new[] { "SituationID", "MemberID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ID",
                table: "Products",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeID",
                table: "Products",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnitPrices_ProductID",
                table: "ProductUnitPrices",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_HouseholdID",
                table: "Receipts",
                column: "HouseholdID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_PaymentID",
                table: "Receipts",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ProductID",
                table: "Receipts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ProductTypeID",
                table: "Receipts",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ProductUnitPriceID",
                table: "Receipts",
                column: "ProductUnitPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_VolunteerID",
                table: "Receipts",
                column: "VolunteerID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_ProductID",
                table: "SaleDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_MemberID",
                table: "Sales",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PaymentID",
                table: "Sales",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_VolunteerID",
                table: "Sales",
                column: "VolunteerID");

            migrationBuilder.CreateIndex(
                name: "IX_TempAddresses_PlaceID",
                table: "TempAddresses",
                column: "PlaceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempHouseholds_AddressID",
                table: "TempHouseholds",
                column: "AddressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TempMemberDietaries_MemberID",
                table: "TempMemberDietaries",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_TempMemberHouseholds_HouseholdID",
                table: "TempMemberHouseholds",
                column: "HouseholdID");

            migrationBuilder.CreateIndex(
                name: "IX_TempMemberHouseholds_MemberID",
                table: "TempMemberHouseholds",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_TempMemberIllnesses_MemberID",
                table: "TempMemberIllnesses",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_TempMembers_GenderID",
                table: "TempMembers",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_TempMembers_HouseholdID",
                table: "TempMembers",
                column: "HouseholdID");

            migrationBuilder.CreateIndex(
                name: "IX_TempMembers_TempHouseholdID",
                table: "TempMembers",
                column: "TempHouseholdID");

            migrationBuilder.CreateIndex(
                name: "IX_TempMembers_VolunteerID",
                table: "TempMembers",
                column: "VolunteerID");

            migrationBuilder.CreateIndex(
                name: "IX_TempMemberSituations_MemberID",
                table: "TempMemberSituations",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_TempMemberSituations_SituationID_MemberID",
                table: "TempMemberSituations",
                columns: new[] { "SituationID", "MemberID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelDetails_AddressID",
                table: "TravelDetails",
                column: "AddressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelDetails_GroceryID",
                table: "TravelDetails",
                column: "GroceryID");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_MemberID",
                table: "UploadedFiles",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_TempMemberDocument_MemberID",
                table: "UploadedFiles",
                column: "TempMemberDocument_MemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileContent");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "MemberDietaries");

            migrationBuilder.DropTable(
                name: "MemberHouseholds");

            migrationBuilder.DropTable(
                name: "MemberIllnesses");

            migrationBuilder.DropTable(
                name: "MemberSituations");

            migrationBuilder.DropTable(
                name: "SaleDetails");

            migrationBuilder.DropTable(
                name: "TempMemberDietaries");

            migrationBuilder.DropTable(
                name: "TempMemberHouseholds");

            migrationBuilder.DropTable(
                name: "TempMemberIllnesses");

            migrationBuilder.DropTable(
                name: "TempMemberSituations");

            migrationBuilder.DropTable(
                name: "TravelDetails");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Dietaries");

            migrationBuilder.DropTable(
                name: "Illnesses");

            migrationBuilder.DropTable(
                name: "Situations");

            migrationBuilder.DropTable(
                name: "GroceryStores");

            migrationBuilder.DropTable(
                name: "TempMembers");

            migrationBuilder.DropTable(
                name: "ProductUnitPrices");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "TempHouseholds");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Households");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "TempAddresses");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
