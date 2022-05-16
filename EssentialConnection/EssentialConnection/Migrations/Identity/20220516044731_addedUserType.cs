using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EssentialConnection.Migrations.Identity
{
    public partial class addedUserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimeiroNome",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Sobrenome",
                table: "AspNetUsers",
                newName: "NomeCompleto");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "AspNetUsers",
                type: "int",
                maxLength: 255,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "NomeCompleto",
                table: "AspNetUsers",
                newName: "Sobrenome");

            migrationBuilder.AddColumn<string>(
                name: "PrimeiroNome",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
