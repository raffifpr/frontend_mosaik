using Microsoft.EntityFrameworkCore.Migrations;

namespace Mosaik.idAPI.Migrations
{
    public partial class uniqueKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MosaikUsers_Email",
                table: "MosaikUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MosaikParents_Email",
                table: "MosaikParents",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MosaikChildren_Email",
                table: "MosaikChildren",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MosaikUsers_Email",
                table: "MosaikUsers");

            migrationBuilder.DropIndex(
                name: "IX_MosaikParents_Email",
                table: "MosaikParents");

            migrationBuilder.DropIndex(
                name: "IX_MosaikChildren_Email",
                table: "MosaikChildren");
        }
    }
}
