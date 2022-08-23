using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListingApi.Migrations
{
    public partial class HotelDatabaseSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "HotelId", "CountryId", "HotelAddress", "HotelName", "HotelRating" },
                values: new object[] { 1, 1, "NEGRIL", "SANDALS RESORT AND SPA", 5.0 });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "HotelId", "CountryId", "HotelAddress", "HotelName", "HotelRating" },
                values: new object[] { 2, 3, "GEORGE TOWN", "COMFORT SUITE", 4.5 });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "HotelId", "CountryId", "HotelAddress", "HotelName", "HotelRating" },
                values: new object[] { 3, 2, "NASSUA", "GRAND PALLDIUM", 3.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 3);
        }
    }
}
