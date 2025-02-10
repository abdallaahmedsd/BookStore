-- SQL Migration Script
-- Created on: 02/10/2025 09:18:41

CREATE PROCEDURE [Sales].[SP_GetShoppingCardItem]
				@Id INT
AS
BEGIN
    -- Set NOCOUNT to ON to avoid extra result sets
    SET NOCOUNT ON;

    BEGIN TRY
        -- Check if the user (customer) exists
         IF NOT EXISTS(SELECT 1 FROM Sales.ShoppingCards WHERE Id = @Id)
            THROW 50002, 'The Shopping Cart does not exist', 1;

        -- Retrieve the shopping cart items for the customer with pagination
        SELECT 
			Id,
            BookID,
            Quantity,
            SubTotal,
			UserID
        FROM Sales.ShoppingCards
        WHERE Id = @Id
    END TRY
    BEGIN CATCH
        -- Re-throw the error to be handled by the calling program
        THROW;
    END CATCH
END;
GO