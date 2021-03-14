CREATE PROCEDURE [dbo].[spSaleInsert]
	@Id int output,
	@CashierId nvarchar(128),
	@SaleDate datetime2,
	@SubTotal money,
	@Tax money,
	@Total money
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Sale](CashierId, SubTotal, Tax, Total, SaleDate)
	VALUES (@CashierId, @SubTotal, @Tax, @Total, @SaleDate);

	SELECT @Id = SCOPE_IDENTITY();
END