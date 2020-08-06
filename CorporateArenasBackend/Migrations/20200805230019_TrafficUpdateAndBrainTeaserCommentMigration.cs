using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CorporateArenasBackend.Migrations
{
    public partial class TrafficUpdateAndBrainTeaserCommentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrainTeasers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Riddle = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrainTeasers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrafficUpdateComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    TrafficUpdateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficUpdateComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrafficUpdateComments_TrafficUpdates_TrafficUpdateId",
                        column: x => x.TrafficUpdateId,
                        principalTable: "TrafficUpdates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrainTeaserComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    BrainTeaserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrainTeaserComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrainTeaserComments_BrainTeasers_BrainTeaserId",
                        column: x => x.BrainTeaserId,
                        principalTable: "BrainTeasers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrainTeaserComments_BrainTeaserId",
                table: "BrainTeaserComments",
                column: "BrainTeaserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficUpdateComments_TrafficUpdateId",
                table: "TrafficUpdateComments",
                column: "TrafficUpdateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrainTeaserComments");

            migrationBuilder.DropTable(
                name: "TrafficUpdateComments");

            migrationBuilder.DropTable(
                name: "BrainTeasers");
        }
    }
}
