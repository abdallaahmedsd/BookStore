-- SQL Migration Script
-- Created on: 02/12/2025 08:29:30

CREATE PROCEDURE Sales.SP_GetOrderDetailsViewModleByOrderId
    @OrderID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Validate parameter
        IF Sales.Fun_IsOrderExistsById(@OrderID) <> 1
            THROW 50001, 'OrderID Not Exists', 1;

        SELECT 
            o.Id, 
            o.UserID, 
            o.OrderDate, 
            o.TotalAmoumt, -- Fixed the typo
            o.Status, 
            s.EstimatedDelivery, 
            s.ZipCode,
            s.ShippingAddress,
            s.City, 
            s.CountryID,
            u.FirstName + ' ' + u.LastName AS FullName, 
            u.Email
        FROM Sales.Orders o
        INNER JOIN AspNetUsers u ON o.UserID = u.Id
        INNER JOIN Sales.Shippings s ON s.OrderID = o.Id
        WHERE o.Id = @OrderID;
    END TRY
    BEGIN CATCH
       THROW;
    END CATCH
END