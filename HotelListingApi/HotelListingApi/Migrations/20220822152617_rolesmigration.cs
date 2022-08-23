using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListingApi.Migrations
{
    public partial class rolesmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a68b630f-a736-41e7-b020-dc12d17167ac", "67679651-c536-4ad0-a753-0626e1ac3ccf", "Supervisor", "SUPERVISOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7953acde-66b5-45bf-9876-adec071cc100", "2a203981-5e6f-4ddc-9960-5b28031e6df2", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29af2992-22fe-4a25-a540-a7c12ddcfba1", "ea85a25d-8d34-43c8-8bef-e60d57ec94d5", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29af2992-22fe-4a25-a540-a7c12ddcfba1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7953acde-66b5-45bf-9876-adec071cc100");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a68b630f-a736-41e7-b020-dc12d17167ac");
        }
    }
}
