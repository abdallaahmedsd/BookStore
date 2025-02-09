-- SQL Migration Script
-- Created on: 02/09/2025 23:17:58

USE [BookstoreDB]
GO
/****** Object:  StoredProcedure [Sales].[SP_UpdateShoppingCard]    Script Date: 2/9/2025 11:22:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- SQL Migration Script
-- Created on: 02/09/2025 21:51:34


ALTER PROCEDURE [Sales].[SP_UpdateShoppingCard]
    @Id INT,                     
    @BookId DECIMAL(10,2), 
    @Quantity TINYINT = NULL,
	@CustomerID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF Sales.Fun_IsShippingCardExistsById(@Id) <> 1
            THROW 50002, 'Shopping Card not found or has been deleted.', 1;


        IF Books.Fun_IsBookExistsByID(@BookId) <> 1
            THROW 50002, 'Book Id not found or has been deleted.', 1;


        IF @Quantity IS NOT NULL AND @Quantity < 1
            THROW 50003, 'Quantity must be a gratter than 0 .', 1;


		DECLARE @SubTotal DECIMAL

		SELECT @SubTotal = CAST(@Quantity As decimal) *  Price From Books.Books WHERE Id = @BookId;

		    IF @SubTotal IS NOT NULL AND @SubTotal < 1
				THROW 50003, 'SubTototal must be a gratter than 0 .', 1;
        UPDATE Sales.ShoppingCards
        SET SubTotal = ISNULL(@SubTotal, SubTotal),
            Quantity = ISNULL(@Quantity, Quantity),
			BookID = ISNULL(@BookID, BookID),
			UserID = ISNULL(@CustomerID, UserID)
        WHERE Id = @Id;
		RETURN 1;
    END TRY
    BEGIN CATCH
        -- Handle any errors
        THROW;
    END CATCH;
END
GO