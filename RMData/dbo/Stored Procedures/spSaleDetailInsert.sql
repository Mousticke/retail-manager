CREATE PROCEDURE [dbo].[spSaleDetailInsert]
	@SaleId int,
	@ProductId int,
	@Quantity int,
	@PurchasePrice money,
	@Tax money
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[SaleDetail](SaleId, ProductId, PurchasePrice, Quantity, Tax)
	VALUES (@SaleId, @ProductId, @PurchasePrice, @Quantity, @Tax);

END