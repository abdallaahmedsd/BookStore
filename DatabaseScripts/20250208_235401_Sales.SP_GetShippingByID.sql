-- SQL Migration Script
-- Created on: 02/08/2025 23:54:01


-- Get Shipping by ID
CREATE PROCEDURE Sales.SP_GetShippingByID
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Validate Shipping existence
        IF NOT EXISTS(SELECT 1 FROM Sales.Shippings WHERE Id = @Id)
            THROW 50001, 'This Shipping does not exist', 1;

        -- Return Shipping details
        SELECT *
        FROM Sales.Shippings
        WHERE Id = @Id 
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO