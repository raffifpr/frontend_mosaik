using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mosaik.idAPI.Migrations
{
    public partial class Add5Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MosaikChildren",
                columns: table => new
                {
                    MosaikChildID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MosaikChildren", x => x.MosaikChildID);
                });

            migrationBuilder.CreateTable(
                name: "MosaikChildRestricts",
                columns: table => new
                {
                    MosaikChildRestrictID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildID = table.Column<int>(type: "integer", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Notif = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MosaikChildRestricts", x => x.MosaikChildRestrictID);
                });

            migrationBuilder.CreateTable(
                name: "MosaikHistories",
                columns: table => new
                {
                    MosaikHistoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userID = table.Column<int>(type: "integer", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    AccessedTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MosaikHistories", x => x.MosaikHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "MosaikParents",
                columns: table => new
                {
                    MosaikParentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MosaikParents", x => x.MosaikParentID);
                });

            migrationBuilder.CreateTable(
                name: "MosaikParentsChildren",
                columns: table => new
                {
                    MosaikParentChildID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parentID = table.Column<int>(type: "integer", nullable: false),
                    childID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MosaikParentsChildren", x => x.MosaikParentChildID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MosaikChildren");

            migrationBuilder.DropTable(
                name: "MosaikChildRestricts");

            migrationBuilder.DropTable(
                name: "MosaikHistories");

            migrationBuilder.DropTable(
                name: "MosaikParents");

            migrationBuilder.DropTable(
                name: "MosaikParentsChildren");
        }
    }
}
