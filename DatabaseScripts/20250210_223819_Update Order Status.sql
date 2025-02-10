-- SQL Migration Script
-- Created on: 02/10/2025 22:38:19


CREATE PROCEDURE [Sales].[SP_UpdateOrderStatus]
    @Id INT,
    @Status TINYINT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Validate Order ID
        IF Sales.Fun_IsOrderExistsById(@Id) <> 1
            THROW 60004, 'Order record not found.', 1;

        -- Validate Status
        IF @Status NOT IN (1, 2, 3)
            THROW 60005, 'Invalid Status value. Use 1 (Progress), 2 (Complete), 3 (Cancel).', 1;

        -- Update status
        UPDATE Sales.Orders
        SET Status = @Status
        WHERE Id = @Id;
	RETURN 1;
    END TRY
    BEGIN CATCH
        THROW;
		RETURN 0;
    END CATCH;
END;
