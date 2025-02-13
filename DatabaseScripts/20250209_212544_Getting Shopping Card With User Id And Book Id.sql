-- SQL Migration Script
-- Created on: 02/09/2025 21:25:44


CREATE PROCEDURE Sales.SP_GetShoppingCardByUserIDandBookID
				@UserID INT,
				@BookID INT
AS
BEGIN
    -- Set NOCOUNT to ON to avoid extra result sets
    SET NOCOUNT ON;

    BEGIN TRY
        -- Check if the user (customer) exists
         IF dbo.Fun_IsUserExistsById(@UserID) <>1
            THROW 50002, 'The Shopping Cart does not exist', 1;

        -- Retrieve the shopping cart items for the customer with pagination
        SELECT 
			Id,
            BookID,
            Quantity,
            SubTotal,
			UserID
        FROM Sales.ShoppingCards
        WHERE UserID = @UserID AND BookID = @BookID
    END TRY
    BEGIN CATCH
        -- Re-throw the error to be handled by the calling program
        THROW;
    END CATCH
END;
