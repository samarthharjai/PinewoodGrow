using Microsoft.EntityFrameworkCore.Migrations;

namespace PinewoodGrow.Data
{
    public static class ExtraMigration
    {
        public static void Steps(MigrationBuilder migrationBuilder)
        {


            #region Main Tables
            //Households
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetHouseholdTimestampOnUpdate
                    AFTER UPDATE ON Households
                    BEGIN
                        UPDATE Households
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetHouseholdTimestampOnInsert
                    AFTER INSERT ON Households
                    BEGIN
                        UPDATE Households
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            //Members
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetMemberTimestampOnUpdate
                    AFTER UPDATE ON Members
                    BEGIN
                        UPDATE Members
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetMemberTimestampOnInsert
                    AFTER INSERT ON Members
                    BEGIN
                        UPDATE Members
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");   
            //Situations
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetSituationTimestampOnUpdate
                    AFTER UPDATE ON Situations
                    BEGIN
                        UPDATE Situations
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetSituationTimestampOnInsert
                    AFTER INSERT ON Situations
                    BEGIN
                        UPDATE Situations
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            //Receipts * no need for concurency on Receipts
            /*migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetReceiptTimestampOnUpdate
                    AFTER UPDATE ON Receipts
                    BEGIN
                        UPDATE Receipts
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetReceiptTimestampOnInsert
                    AFTER INSERT ON Receipts
                    BEGIN
                        UPDATE Receipts
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");*/

            //Products
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetProductTimestampOnUpdate
                    AFTER UPDATE ON Products
                    BEGIN
                        UPDATE Products
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetProductTimestampOnInsert
                    AFTER INSERT ON Products
                    BEGIN
                        UPDATE Products
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");

            //ProductUnitPrices
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetProductUnitPriceTimestampOnUpdate
                    AFTER UPDATE ON ProductUnitPrices
                    BEGIN
                        UPDATE ProductUnitPrices
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetProductUnitPriceTimestampOnInsert
                    AFTER INSERT ON ProductUnitPrices
                    BEGIN
                        UPDATE ProductUnitPrices
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");

            //MemberSituations
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetMemberSituationTimestampOnUpdate
                    AFTER UPDATE ON MemberSituations
                    BEGIN
                        UPDATE MemberSituations
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetMemberSituationTimestampOnInsert
                    AFTER INSERT ON MemberSituations
                    BEGIN
                        UPDATE MemberSituations
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");

            //Dietaries
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetDietaryTimestampOnUpdate
                    AFTER UPDATE ON Dietaries
                    BEGIN
                        UPDATE Dietaries
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetDietaryTimestampOnInsert
                    AFTER INSERT ON Dietaries
                    BEGIN
                        UPDATE Dietaries
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            #endregion

            #region Temp Tables
            //TempHouseholds
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetTempHouseholdTimestampOnUpdate
                    AFTER UPDATE ON TempHouseholds
                    BEGIN
                        UPDATE TempHouseholds
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetTempHouseholdTimestampOnInsert
                    AFTER INSERT ON TempHouseholds
                    BEGIN
                        UPDATE TempHouseholds
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");


            //TempMembers
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetTempMemberTimestampOnUpdate
                    AFTER UPDATE ON TempMembers
                    BEGIN
                        UPDATE TempMembers
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetTempMemberTimestampOnInsert
                    AFTER INSERT ON TempMembers
                    BEGIN
                        UPDATE TempMembers
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");

            //TempMemberSituations
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetTempMemberSituationTimestampOnUpdate
                    AFTER UPDATE ON TempMemberSituations
                    BEGIN
                        UPDATE TempMemberSituations
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetTempMemberSituationTimestampOnInsert
                    AFTER INSERT ON TempMemberSituations
                    BEGIN
                        UPDATE TempMemberSituations
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");

            #endregion




        }
    }
}
