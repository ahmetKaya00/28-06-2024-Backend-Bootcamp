using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcoreApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTableEgitmen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EgitmenId",
                table: "Bootcamps",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Egitmenler",
                columns: table => new
                {
                    OgretmenId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: true),
                    Soyad = table.Column<string>(type: "TEXT", nullable: true),
                    Eposta = table.Column<string>(type: "TEXT", nullable: true),
                    Telefon = table.Column<string>(type: "TEXT", nullable: true),
                    BaslamaTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egitmenler", x => x.OgretmenId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bootcamps_EgitmenId",
                table: "Bootcamps",
                column: "EgitmenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bootcamps_Egitmenler_EgitmenId",
                table: "Bootcamps",
                column: "EgitmenId",
                principalTable: "Egitmenler",
                principalColumn: "OgretmenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bootcamps_Egitmenler_EgitmenId",
                table: "Bootcamps");

            migrationBuilder.DropTable(
                name: "Egitmenler");

            migrationBuilder.DropIndex(
                name: "IX_Bootcamps_EgitmenId",
                table: "Bootcamps");

            migrationBuilder.DropColumn(
                name: "EgitmenId",
                table: "Bootcamps");
        }
    }
}
