using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dokaanah.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isAgree",
                table: "AspNetUsers",
                newName: "IsAgree");

            migrationBuilder.RenameColumn(
                name: "confirmPassword",
                table: "AspNetUsers",
                newName: "ConfirmPassword");

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

            migrationBuilder.RenameColumn(
                name: "IsAgree",
                table: "AspNetUsers",
                newName: "isAgree");

            migrationBuilder.RenameColumn(
                name: "ConfirmPassword",
                table: "AspNetUsers",
                newName: "confirmPassword");
        }
    }
}
