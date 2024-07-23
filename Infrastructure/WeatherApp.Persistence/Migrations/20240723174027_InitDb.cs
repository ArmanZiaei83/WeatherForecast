using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: false),
                    GenerationTimeMs = table.Column<float>(type: "real", nullable: false),
                    Elevation = table.Column<float>(type: "real", nullable: false),
                    UtcOffsetSeconds = table.Column<int>(type: "int", nullable: false),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimezoneAbbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hourly",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature2M = table.Column<float>(type: "real", nullable: false),
                    RelativeHumidity2M = table.Column<float>(type: "real", nullable: false),
                    WindSpeed10M = table.Column<float>(type: "real", nullable: false),
                    WeatherForecastId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hourly", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hourly_WeatherForecasts_WeatherForecastId",
                        column: x => x.WeatherForecastId,
                        principalTable: "WeatherForecasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HourlyUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temperature2M = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelativeHumidity2M = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WindSpeed10M = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherForecastId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourlyUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourlyUnits_WeatherForecasts_WeatherForecastId",
                        column: x => x.WeatherForecastId,
                        principalTable: "WeatherForecasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hourly_WeatherForecastId",
                table: "Hourly",
                column: "WeatherForecastId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyUnits_WeatherForecastId",
                table: "HourlyUnits",
                column: "WeatherForecastId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hourly");

            migrationBuilder.DropTable(
                name: "HourlyUnits");

            migrationBuilder.DropTable(
                name: "WeatherForecasts");
        }
    }
}
