using Microsoft.EntityFrameworkCore.Migrations;

namespace TestePMWEB.Migrations
{
    public partial class TRIGGER_Cons_Clientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tI_Cons_Clientes]'))
	DROP TRIGGER [dbo].[tI_Cons_Clientes]
GO

CREATE TRIGGER tI_Cons_Clientes
	ON Clientes
AFTER INSERT AS
BEGIN
	SET NOCOUNT ON;
		Insert into Cons_Clientes(ID_CLIENTE, DATA_NASCIMENTO, UF, CIDADE, FAIXA, TIERS, TEMPO_MEDIOCOMPRAS, QTD_COMPRAS_12M, LTV) 
			select ID, DATA_NASCIMENTO, UF, CIDADE, 0, 'Básico', 0, 0, 0 FROM inserted
END
GO


IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tD_Cons_Clientes]'))
	DROP TRIGGER [dbo].[tD_Cons_Clientes]
GO

CREATE TRIGGER tD_Cons_Clientes
	ON Clientes
FOR DELETE AS
BEGIN
	SET NOCOUNT ON;
		DELETE FROM Cons_CLientes WHERE Cons_Clientes.ID_CLIENTE in (select ID from deleted)
END
GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
