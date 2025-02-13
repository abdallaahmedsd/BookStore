-- SQL Migration Script
-- Created on: 02/11/2025 19:09:09

CREATE PROCEDURE Sales.SP_GetOrderListViewModel
    @OrderID INT -- Optional filter by a specific order

AS
BEGIN
    SET NOCOUNT ON;

	IF Sales.Fun_IsOrderExistsById(@OrderID) <> 1
		THROW 50001, 'Order Id Not Exsits', 1
    SELECT Id, o.OrderDate, o.TotalAmoumt, Status
    FROM Sales.Orders o
    WHERE o.Id = @OrderID
      
END;
