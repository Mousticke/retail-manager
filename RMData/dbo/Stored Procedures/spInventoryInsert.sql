CREATE PROCEDURE [dbo].[spInventoryInsert]
	@ProductId int, 
	@Quantity int, 
	@PurchasePrice money, 
	@PurchaseDate datetime2
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Inventory](ProductId, Quantity, PurchaseDate, PurchasePrice)
	VALUES (@ProductId, @Quantity, @PurchaseDate, @PurchasePrice);
END