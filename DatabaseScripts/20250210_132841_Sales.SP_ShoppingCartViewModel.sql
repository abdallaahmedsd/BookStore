-- SQL Migration Script
-- Created on: 02/10/2025 13:28:41

CREATE PROCEDURE Sales.SP_ShoppingCartViewModel
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF dbo.Fun_IsUserExistsById(@UserID) <> 1
            THROW 50001, 'The User does not exist', 1;

        SELECT
            sc.*, b.Title, b.CoverImage,b.Price
        FROM 
            Sales.ShoppingCards sc
        INNER JOIN 
            Books.Books b ON sc.BookID = b.Id 
        WHERE 
           sc.UserID = @UserID AND b.IsDeleted = 0;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
