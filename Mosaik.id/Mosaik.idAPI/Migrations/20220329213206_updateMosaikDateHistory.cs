using Microsoft.EntityFrameworkCore.Migrations;

namespace Mosaik.idAPI.Migrations
{
    public partial class updateMosaikDateHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "MosaikDateHistories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "MosaikDateHistories");
        }
    }
}
