using Microsoft.EntityFrameworkCore.Migrations;

namespace TestePMWEB.Migrations
{
    public partial class Function : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF EXISTS(SELECT 1 FROM SYSOBJECTS WHERE id = OBJECT_ID('fn_GetTier'))
                                    DROP FUNCTION fn_GetTier
                                GO
                                CREATE FUNCTION [dbo].fn_GetTier(@valor real)
                                RETURNS VARCHAR(10)
                                BEGIN
                                    RETURN( CASE  
                                         WHEN @valor < 1000 THEN 'Básico'  
                                         WHEN @valor < 2000 THEN 'Prata'
                                         WHEN @valor < 5000 THEN 'Ouro'
                                         ELSE 'Super'
                                      END)
                                END
                                GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
