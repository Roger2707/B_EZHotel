using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EZHotel.Migrations
{
    /// <inheritdoc />
    public partial class seedDBUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("619d7706-770c-47ac-92a4-7e5c741edc78"), "Manager" },
                    { new Guid("6e44c69b-e62f-42cf-bd59-12f29e83a6dc"), "Customer" },
                    { new Guid("b0a9cc22-2046-45a6-accf-da011b819870"), "Staff" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Avatar", "Birthday", "CreatedAt", "Email", "FullName", "HashPassword", "IsActive", "PhoneNumber", "PublicId", "RoleId", "UserName" },
                values: new object[,]
                {
                    { new Guid("30785cf6-960b-4c7a-9f14-012e325eaedf"), "321 Main St", "", new DateTime(1995, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 15, 3, 35, 42, 265, DateTimeKind.Utc).AddTicks(9031), "customer@example.com", "Quincy Thai", "$2a$11$Y79OHzHIfzaDDnW6safgouXvaEkUAeyg4aA9Q.MsCnWmuHmNfBiI6", true, "0934567890", "", new Guid("6e44c69b-e62f-42cf-bd59-12f29e83a6dc"), "quincy" },
                    { new Guid("4bb57790-0eec-473d-94e4-768d2da58c18"), "456 Main St", "", new DateTime(1993, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 15, 3, 35, 41, 951, DateTimeKind.Utc).AddTicks(6893), "staff@example.com", "Simon Nguyen", "$2a$11$y2z6KOHHiyAge8MT3U8zLOM3TP2kzKAqBfZfSKBBC21PSmZRGzzyG", true, "0987654321", "", new Guid("b0a9cc22-2046-45a6-accf-da011b819870"), "simon" },
                    { new Guid("d6085050-f6d7-43d7-9fa3-c0b4e9ac279f"), "789 Main St", "", new DateTime(1999, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 15, 3, 35, 42, 109, DateTimeKind.Utc).AddTicks(3477), "rogerhuynh2707@gmail.com", "Roger Huynh", "$2a$11$IIh7kGEAS4exB7SI18PifOGWF8Ea910ihVsRPLJ7QMeCsyOEeZC.O", true, "0776198888", "", new Guid("619d7706-770c-47ac-92a4-7e5c741edc78"), "roger" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "LoyaltyPoints", "Nationality", "PassportNumber", "PaymentMethod", "PreferredRoomType", "SpecialRequest" },
                values: new object[] { new Guid("30785cf6-960b-4c7a-9f14-012e325eaedf"), 100, "Vietnam", "A12345678", 1, 1, "Late check-in" });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "BankAccount", "Certifications", "EmergencyContact", "EmployeeCode", "HireDate", "Position", "Salary" },
                values: new object[,]
                {
                    { new Guid("4bb57790-0eec-473d-94e4-768d2da58c18"), "1234567890", "Hotel Management", "0901234567", "EMP001", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leader - Customer Service", 500m },
                    { new Guid("d6085050-f6d7-43d7-9fa3-c0b4e9ac279f"), "9876543210", "Leadership", "0909876543", "MGR001", new DateTime(2015, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hotel Manager", 2000m }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "ApprovalLimit", "AuthorityLevel", "PerformanceBonusRate" },
                values: new object[] { new Guid("d6085050-f6d7-43d7-9fa3-c0b4e9ac279f"), 5000m, 3, 0.1m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("30785cf6-960b-4c7a-9f14-012e325eaedf"));

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("d6085050-f6d7-43d7-9fa3-c0b4e9ac279f"));

            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: new Guid("4bb57790-0eec-473d-94e4-768d2da58c18"));

            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: new Guid("d6085050-f6d7-43d7-9fa3-c0b4e9ac279f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("30785cf6-960b-4c7a-9f14-012e325eaedf"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4bb57790-0eec-473d-94e4-768d2da58c18"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6e44c69b-e62f-42cf-bd59-12f29e83a6dc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b0a9cc22-2046-45a6-accf-da011b819870"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d6085050-f6d7-43d7-9fa3-c0b4e9ac279f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("619d7706-770c-47ac-92a4-7e5c741edc78"));
        }
    }
}
