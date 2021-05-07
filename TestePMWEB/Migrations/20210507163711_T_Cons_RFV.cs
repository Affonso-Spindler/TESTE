using Microsoft.EntityFrameworkCore.Migrations;

namespace TestePMWEB.Migrations
{
    public partial class T_Cons_RFV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tI_Cons_RFV]'))
	DROP TRIGGER [dbo].[tI_Cons_RFV]
GO

CREATE TRIGGER tI_Cons_RFV
	ON Clientes
AFTER INSERT AS
BEGIN
	SET NOCOUNT ON;
		Insert into Cons_RFV(ID_CLIENTE) 
			select ID FROM inserted
END
GO


IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tD_Cons_RFV]'))
	DROP TRIGGER [dbo].[tD_Cons_RFV]
GO

CREATE TRIGGER tD_Cons_RFV
	ON Clientes
FOR DELETE AS
BEGIN
	SET NOCOUNT ON;
		DELETE FROM Cons_RFV WHERE Cons_RFV.ID_CLIENTE in (select ID from deleted)
END
GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
