-- SQL Migration Script
-- Created on: 02/12/2025 08:35:18


CREATE PROCEDURE [Sales].[SP_CreatePayment]
				@OrderID INT,
				@NewId_output INT OUTPUT
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;
		IF Sales.Fun_IsOrderExistsById(@OrderID) <> 1
			THROW 500008,'The Order does not exist',1;


		DECLARE @Amount DECIMAL(10,2);
		DECLARE @UserID INT;
		SELECT @Amount = TotalAmoumt FROM Sales.Orders WHERE Id = @OrderID;
		
		IF @Amount <= 0
			THROW 500009,'The Amount should be grater than 0',1;

		SELECT @UserID = UserID FROM Sales.Orders WHERE Id = @OrderID;

		IF dbo.Fun_IsUserExistsById(@UserID) <> 1
			THROW 500008,'The User does not exist',1;

			INSERT INTO [Sales].[Payments]
           ([OrderID]
           ,[PaymentDate]
           ,[Amoumt]
		   ,[UserID])
     VALUES
           (@OrderID,
           GETDATE(),
		   @Amount,
		   @UserID)
	 SET @NewId_output = SCOPE_IDENTITY();

	END TRY
	BEGIN CATCH
		THROW;
	END CATCH

END