using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestePMWEB.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EMAIL = table.Column<string>(maxLength: 50, nullable: false),
                    NOME = table.Column<string>(maxLength: 50, nullable: false),
                    DATA_NASCIMENTO = table.Column<DateTime>(nullable: false),
                    CIDADE = table.Column<string>(maxLength: 50, nullable: false),
                    UF = table.Column<string>(maxLength: 2, nullable: false),
                    PERMISSAO_RECEBE_EMAIL = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    ID_PEDIDO = table.Column<int>(nullable: false),
                    ID_CLIENTE = table.Column<int>(nullable: false),
                    ID_PRODUTO = table.Column<int>(nullable: false),
                    CLIENTEID = table.Column<int>(nullable: true),
                    DEPARTAMENTO = table.Column<string>(maxLength: 50, nullable: true),
                    QUANTIDADE = table.Column<int>(nullable: false),
                    VALOR_UNITARIO = table.Column<decimal>(type: "decimal(8, 3)", nullable: false),
                    PARCELAS = table.Column<int>(nullable: false),
                    DATA_PEDIDO = table.Column<DateTime>(nullable: false),
                    MEIO_PAGAMENTO = table.Column<string>(maxLength: 50, nullable: true),
                    STATUS_PAGAMENTO = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => new { x.ID_PEDIDO, x.ID_CLIENTE, x.ID_PRODUTO });
                    table.UniqueConstraint("AK_Pedidos_ID_CLIENTE_ID_PEDIDO_ID_PRODUTO", x => new { x.ID_CLIENTE, x.ID_PEDIDO, x.ID_PRODUTO });
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_CLIENTEID",
                        column: x => x.CLIENTEID,
                        principalTable: "Clientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CLIENTEID",
                table: "Pedidos",
                column: "CLIENTEID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
