-- SQL Migration Script
-- Created on: 02/13/2025 21:58:20

CREATE PROCEDURE Sales.SP_DeletePaymentsByOrderId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

		IF Sales.Fun_IsOrderExistsById(@Id) <> 1
			THROW 50001, 'Order Id Not Exists', 1;

        DELETE FROM Sales.Payments
        WHERE OrderID = @Id;

		RETURN 1;
    END TRY

    BEGIN CATCH
		THROW;
		RETURN 0;
    END CATCH
END;
