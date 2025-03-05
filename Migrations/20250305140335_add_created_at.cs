using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebFirstRun.Migrations
{
    /// <inheritdoc />
    public partial class add_created_at : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "product_category",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "product_category");
        }
    }
}
