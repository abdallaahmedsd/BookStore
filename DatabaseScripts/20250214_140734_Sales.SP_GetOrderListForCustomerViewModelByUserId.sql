-- SQL Migration Script
-- Created on: 02/14/2025 14:07:34

CREATE PROCEDURE Sales.SP_GetOrderListForCustomerViewModelByUserId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY

		IF dbo.Fun_IsUserExistsById(@Id) <> 1
			THROW 50001, 'Order Id Not Exists', 1;

        SELECT 
            o.OrderDate,
            o.Status,
            o.TotalAmoumt, -- Fixed typo
            s.TrackingNumber,
            s.Carrier
        FROM Sales.Orders o JOIN Sales.Shippings s ON s.OrderID = o.Id
        WHERE o.UserID = @Id;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
