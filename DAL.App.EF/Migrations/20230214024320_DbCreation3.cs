using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    /// <inheritdoc />
    public partial class DbCreation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Counter",
                table: "PriceList");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "ProvidedRoute",
                type: "double precision",
                precision: 28,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(28,2)",
                oldPrecision: 28,
                oldScale: 2);

            migrationBuilder.AddColumn<long>(
                name: "Distance",
                table: "ProvidedRoute",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "ProvidedRoute");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProvidedRoute",
                type: "numeric(28,2)",
                precision: 28,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 28,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "Counter",
                table: "PriceList",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
