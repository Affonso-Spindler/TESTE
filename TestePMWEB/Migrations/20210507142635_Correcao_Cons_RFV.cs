using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestePMWEB.Migrations
{
    public partial class Correcao_Cons_RFV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TICKET_MEDIO_ALL",
                table: "CONS_RFV",
                type: "decimal(8, 3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TICKET_MEDIO_12M",
                table: "CONS_RFV",
                type: "decimal(8, 3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8, 3)");

            migrationBuilder.AlterColumn<int>(
                name: "FREQUENCIA_COMPRA_ALL",
                table: "CONS_RFV",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FREQUENCIA_COMPRA_12M",
                table: "CONS_RFV",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_ULTIMA_COMPRA",
                table: "CONS_RFV",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TICKET_MEDIO_ALL",
                table: "CONS_RFV",
                type: "decimal(8, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8, 3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TICKET_MEDIO_12M",
                table: "CONS_RFV",
                type: "decimal(8, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8, 3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FREQUENCIA_COMPRA_ALL",
                table: "CONS_RFV",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FREQUENCIA_COMPRA_12M",
                table: "CONS_RFV",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_ULTIMA_COMPRA",
                table: "CONS_RFV",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
