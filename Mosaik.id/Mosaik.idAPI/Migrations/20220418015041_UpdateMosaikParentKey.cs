using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mosaik.idAPI.Migrations
{
    public partial class UpdateMosaikParentKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MosaikParents",
                table: "MosaikParents");

            migrationBuilder.AlterColumn<int>(
                name: "MosaikParentID",
                table: "MosaikParents",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MosaikParents",
                table: "MosaikParents",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MosaikParents",
                table: "MosaikParents");

            migrationBuilder.AlterColumn<int>(
                name: "MosaikParentID",
                table: "MosaikParents",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MosaikParents",
                table: "MosaikParents",
                column: "MosaikParentID");
        }
    }
}
