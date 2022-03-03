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
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 99, nullable: false),
                    UnitPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
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
                    HouseIncome = table.Column<double>(nullable: false),
                    FamilySize = table.Column<int>(nullable: false),
                    FamilyName = table.Column<string>(nullable: false),
                    Dependants = table.Column<int>(nullable: false),
                    LICO = table.Column<bool>(nullable: false),
                    AddressID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Households", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Households_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    SituationID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSituations", x => new { x.SituationID, x.MemberID });
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
                name: "UploadedFiles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(maxLength: 255, nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    MemberID = table.Column<int>(nullable: true)
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
                name: "IX_Households_AddressID",
                table: "Households",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Households_ID",
                table: "Households",
                column: "ID",
                unique: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileContent");

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
                name: "TravelDetails");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "Dietaries");

            migrationBuilder.DropTable(
                name: "Illnesses");

            migrationBuilder.DropTable(
                name: "Situations");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "GroceryStores");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Households");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
