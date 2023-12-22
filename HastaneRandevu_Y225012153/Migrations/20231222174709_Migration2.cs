using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRandevu_Y225012153.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoktorBransId",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "DoktorKlinik",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "DoktorTarih",
                table: "Doktorlar");

            migrationBuilder.RenameColumn(
                name: "DoktorId",
                table: "Doktorlar",
                newName: "DoktorID");

            migrationBuilder.AlterColumn<string>(
                name: "DoktorAdi",
                table: "Doktorlar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AnabilimDaliID",
                table: "Doktorlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DoktorSoyad",
                table: "Doktorlar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnabilimDallari",
                columns: table => new
                {
                    AnabilimDaliID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnabilimDaliAdi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnabilimDallari", x => x.AnabilimDaliID);
                });

            migrationBuilder.CreateTable(
                name: "CalismaSaatleri",
                columns: table => new
                {
                    CalismaSaatiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoktorID = table.Column<int>(type: "int", nullable: false),
                    CalismaGunu = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalismaSaatleri", x => x.CalismaSaatiID);
                    table.ForeignKey(
                        name: "FK_CalismaSaatleri_Doktorlar_DoktorID",
                        column: x => x.DoktorID,
                        principalTable: "Doktorlar",
                        principalColumn: "DoktorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoktorID = table.Column<int>(type: "int", nullable: false),
                    HastaAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HastaSoyadi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RandevuTarih = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuID);
                    table.ForeignKey(
                        name: "FK_Randevular_Doktorlar_DoktorID",
                        column: x => x.DoktorID,
                        principalTable: "Doktorlar",
                        principalColumn: "DoktorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_AnabilimDaliID",
                table: "Doktorlar",
                column: "AnabilimDaliID");

            migrationBuilder.CreateIndex(
                name: "IX_CalismaSaatleri_DoktorID",
                table: "CalismaSaatleri",
                column: "DoktorID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_DoktorID",
                table: "Randevular",
                column: "DoktorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doktorlar_AnabilimDallari_AnabilimDaliID",
                table: "Doktorlar",
                column: "AnabilimDaliID",
                principalTable: "AnabilimDallari",
                principalColumn: "AnabilimDaliID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doktorlar_AnabilimDallari_AnabilimDaliID",
                table: "Doktorlar");

            migrationBuilder.DropTable(
                name: "AnabilimDallari");

            migrationBuilder.DropTable(
                name: "CalismaSaatleri");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Doktorlar_AnabilimDaliID",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "AnabilimDaliID",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "DoktorSoyad",
                table: "Doktorlar");

            migrationBuilder.RenameColumn(
                name: "DoktorID",
                table: "Doktorlar",
                newName: "DoktorId");

            migrationBuilder.AlterColumn<string>(
                name: "DoktorAdi",
                table: "Doktorlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoktorBransId",
                table: "Doktorlar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoktorKlinik",
                table: "Doktorlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DoktorTarih",
                table: "Doktorlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
