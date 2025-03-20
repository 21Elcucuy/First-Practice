using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace First_Practice.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a8e38263-ecc5-403d-a4ef-e409cab5a717", "a41358f4-7768-4e95-aaee-7ab4008049d7", "User", "USER" },
                    { "d0f5a365-5746-4044-b9f4-592fcab2e01b", "a5a9c42b-1f6a-4e3b-a2f7-36a9b8bce53e", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8e38263-ecc5-403d-a4ef-e409cab5a717");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0f5a365-5746-4044-b9f4-592fcab2e01b");
        }
    }
}
