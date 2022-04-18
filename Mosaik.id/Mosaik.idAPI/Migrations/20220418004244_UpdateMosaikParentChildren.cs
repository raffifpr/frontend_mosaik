using Microsoft.EntityFrameworkCore.Migrations;

namespace Mosaik.idAPI.Migrations
{
    public partial class UpdateMosaikParentChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Authorized",
                table: "MosaikParentsChildren",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authorized",
                table: "MosaikParentsChildren");
        }
    }
}
