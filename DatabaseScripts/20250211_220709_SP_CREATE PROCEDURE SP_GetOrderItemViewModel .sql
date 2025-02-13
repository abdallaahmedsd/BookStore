-- SQL Migration Script
-- Created on: 02/11/2025 22:07:09

Alter PROCEDURE Sales.SP_GetOrderItemViewModel 


AS
BEGIN

        SELECT oI.Id, oI.BookID, b.CoverImage, b.Title, b.Price, oI.SubTotal, oI.Quantity, o.UserID FROM Sales.OrderItems oI 
		INNER JOIN Books.Books b ON oI.BookID = b.Id 
		INNER JOIN Sales.Orders o ON o.Id = oI.OrderID
END


GO

Alter PROCEDURE Sales.SP_GetOrderListViewModel
  

AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, o.OrderDate, o.TotalAmoumt, Status
    FROM Sales.Orders o
      
END;