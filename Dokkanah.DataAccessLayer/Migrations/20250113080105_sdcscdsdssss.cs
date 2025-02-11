using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dokaanah.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class sdcscdsdssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "AspNetUsers",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "AspNetUsers");
        }
    }
}
