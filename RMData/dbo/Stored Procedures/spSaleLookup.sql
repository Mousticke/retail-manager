CREATE PROCEDURE [dbo].[spSaleLookup]
	@CashierId nvarchar(128),
	@SaleDate datetime2
AS
BEGIN 
	SET NOCOUNT ON;

	SELECT Id 
	FROM [dbo].[Sale]
	WHERE CashierId = @CashierId AND SaleDate = @SaleDate;
END