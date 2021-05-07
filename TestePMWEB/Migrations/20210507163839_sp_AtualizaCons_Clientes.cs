using Microsoft.EntityFrameworkCore.Migrations;

namespace TestePMWEB.Migrations
{
    public partial class sp_AtualizaCons_Clientes : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
IF EXISTS(SELECT id FROM sysobjects WHERE name='p_AtualizaCons_Clientes' AND xtype='P')
	DROP PROCEDURE [dbo].p_AtualizaCons_Clientes
GO

CREATE PROCEDURE [dbo].p_AtualizaCons_Clientes
AS
BEGIN
	Declare @TempoMedioRelacionamento decimal = (SELECT AVG(FLOOR(DATEDIFF(DAY, Pedidos.DATA_PEDIDO, GETDATE())/365.25)) as TEMPO_MEDIORELACIONAMENTO FROM Pedidos)

	CREATE TABLE #temp
	(
		ID_CLIENTE INT NOT NULL,
		TEMPO_MEDIOCOMPRAS INT NULL,
		QTD_COMPRAS_12M INT NULL,
		FAIXA MONEY NULL,
		TIERS NVARCHAR(20) NULL,
		LTV MONEY NULL
	)
	INSERT INTO #temp(ID_CLIENTE, TEMPO_MEDIOCOMPRAS, FAIXA, TIERS, QTD_COMPRAS_12M, LTV)
	SELECT P.ID_CLIENTE
	, (DATEDIFF(D, MIN(P.DATA_PEDIDO), GETDATE())/ COUNT(distinct(P.ID_PEDIDO))) AS TEMPO_MEDIOCOMPRAS
	, SUM(P.VALOR_UNITARIO) AS FAIXA
	, dbo.fn_GetTier(SUM(P.VALOR_UNITARIO)) as TIERS
	, ISNULL(TP.QTD_Pedidos,0) as QTD_COMPRAS_12M
	, ISNULL(((SUM(P.VALOR_UNITARIO)/COUNT(distinct(P.ID_PEDIDO))) 
		* (COUNT(distinct(P.ID_PEDIDO))/FLOOR(DATEDIFF(DAY, MIN(P.DATA_PEDIDO), GETDATE())/365.25)) 
		* @TempoMedioRelacionamento), 0) as LTV
	FROM Pedidos P
	LEFT JOIN Clientes C ON C.ID = P.ID_CLIENTE
	LEFT JOIN (SELECT COUNT(distinct(ID_PEDIDO)) as QTD_Pedidos
			, ID_CLIENTE
			, (SUM(VALOR_UNITARIO)/COUNT(distinct(ID_PEDIDO))) AS TICKET_MEDIO
			, (DATEDIFF(D, MIN(DATA_PEDIDO), GETDATE())/ COUNT(distinct(ID_PEDIDO))) AS FREQUENCIA_COMPRA
			FROM Pedidos
			WHERE DATA_PEDIDO >= DATEADD(MONTH,-12, GETDATE())
			GROUP BY ID_CLIENTE) AS TP ON tp.ID_CLIENTE = P.ID_CLIENTE
	GROUP BY  P.ID_CLIENTE, C.DATA_NASCIMENTO, C.UF, C.CIDADE, QTD_Pedidos, TP.TICKET_MEDIO, TP.FREQUENCIA_COMPRA

	UPDATE Cons_Clientes
		set Cons_Clientes.TEMPO_MEDIOCOMPRAS = #temp.TEMPO_MEDIOCOMPRAS
			, Cons_Clientes.FAIXA = #temp.FAIXA
			, Cons_Clientes.TIERS = #temp.TIERS
			, Cons_Clientes.QTD_COMPRAS_12M = #temp.QTD_COMPRAS_12M
			, Cons_Clientes.LTV = #temp.LTV
		FROM Cons_Clientes INNER JOIN #temp on #temp.ID_CLIENTE = Cons_Clientes.ID_CLIENTE
		
	DROP TABLE #temp
END
");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
