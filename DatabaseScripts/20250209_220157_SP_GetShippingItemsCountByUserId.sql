-- SQL Migration Script
-- Created on: 02/09/2025 22:01:57

CREATE PROCEDURE Sales.SP_GetShoppingItemsCountByUserId
			@UserID INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY

		IF dbo.Fun_IsUserExistsById(@UserID) <> 1
			THROW 50003,'The User does not exist', 1;

		SELECT 
			COUNT(Id) As ItemsCount 
		FROM 
			Sales.ShoppingCards
		WHERE
			UserID = @UserID

	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END