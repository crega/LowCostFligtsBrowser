using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostFligtsBrowser.Infrastructure.Persistence.Migrations
{
    public partial class ColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TodoLists",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TodoItems",
                type: "int",
                nullable: false,
                defaultValue: 1);

            //migrationBuilder.CreateTable(
            //    name: "Airports",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        gmt = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        iata_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        city_iata_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        icao_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        country_iso2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        geoname_id = table.Column<long>(type: "bigint", nullable: true),
            //        longitude = table.Column<double>(type: "float", nullable: false),
            //        latitude = table.Column<double>(type: "float", nullable: false),
            //        airport_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        country_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        timezone = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Airports", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Airports");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TodoLists");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TodoItems");
        }
    }
}
