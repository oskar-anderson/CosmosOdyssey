using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    /// <inheritdoc />
    public partial class DbCreation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PriceListId",
                table: "ProvidedRoute",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedRoute_PriceListId",
                table: "ProvidedRoute",
                column: "PriceListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvidedRoute_PriceList_PriceListId",
                table: "ProvidedRoute",
                column: "PriceListId",
                principalTable: "PriceList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvidedRoute_PriceList_PriceListId",
                table: "ProvidedRoute");

            migrationBuilder.DropIndex(
                name: "IX_ProvidedRoute_PriceListId",
                table: "ProvidedRoute");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "ProvidedRoute");
        }
    }
}
