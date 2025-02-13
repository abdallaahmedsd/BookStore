-- SQL Migration Script
-- Created on: 02/10/2025 22:25:36



-- Create a new order with specified total amount, status, and user ID
CREATE PROCEDURE Sales.SP_CreateOrder
    @TotalAmoumt DECIMAL(10,2), -- Total amount of the order
    @Status TINYINT,            -- Order status (1: Progress, 2: Complete, 3: Cancel)
    @UserID INT,                -- User ID associated with the order
    @NewId_output INT OUTPUT -- Output parameter for the new order ID
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Validate inputs
        IF @TotalAmoumt < 0
            THROW 50000, 'TotalAmoumt must be a non-negative value.', 1;

        IF @Status NOT IN (1, 2, 3)
            THROW 50001, 'Invalid Status value. Use 1 (Progress), 2 (Complete), or 3 (Cancel).', 1;

        -- Insert the new order
        INSERT INTO Sales.Orders (TotalAmoumt, Status, UserID)
        VALUES (@TotalAmoumt, @Status, @UserID);

        -- Retrieve the ID of the newly created order
        SET @NewId_output = SCOPE_IDENTITY();

    END TRY
    BEGIN CATCH
        -- Handle any errors
        THROW;
    END CATCH;
END;

GO

CREATE PROCEDURE Sales.SP_CreateOrderItem
				@OrderID INT,
				@BookID INT,
				@Quantity INT,
				@NewId_output INT OUTPUT
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;
		IF Sales.Fun_IsOrderExistsById(@OrderId) <> 1
			THROW 500001,'The Order does not exist',1

		IF Books.Fun_IsBookExistsByID(@BookID) <> 1
			THROW 500001,'The Book does not exist',1
			
		IF @Quantity <= 0
			THROW 500001,'The Quantity should be grater than 0',1

		-- Calculate subtotal
		DECLARE @SubTotal DECIMAL(10,2);
		DECLARE @Price DECIMAL(10,2);
		SELECT @Price =  Price FROM Books.Books WHERE Id = @BookID;
		
		SET @SubTotal = @Quantity * @Price;

		IF @SubTotal < 0
			THROW 500001,'The SubTotal does not be negitive',1

		INSERT INTO Sales.OrderItems
           (OrderID
           ,BookID
           ,Quantity
           ,SubTotal)
     VALUES
           (@OrderID,
			@BookID,
			@Quantity,
			@SubTotal)

		SET @NewId_output = SCOPE_IDENTITY();

	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END


GO

CREATE FUNCTION Sales.Fun_IsOrderExistsById (@OrderId INT)
RETURNS BIT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Sales.Orders WHERE Id = @OrderId AND IsDeleted = 0)
		RETURN 1;
	RETURN 0;
END;


GO


-- Function to validate book existence by ID
CREATE FUNCTION Books.Fun_IsBookExistsByID(@Id INT)
RETURNS BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Books.Books WHERE Id = @Id AND IsDeleted = 0)
        RETURN 1
    RETURN 0
END

GO



CREATE PROCEDURE Sales.SP_CreateShipping
    @OrderID INT,
    @ShippingAddress NVARCHAR(500),
    @TrackingNumber NVARCHAR(100),
    @EstimatedDelivery DATETIME,
	@NewId_output INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Validate inputs
        IF Sales.Fun_IsOrderExistsById(@OrderID) <> 1
            THROW 60000, 'Order not found or has been deleted.', 1;

        IF @ShippingAddress IS NULL OR @ShippingAddress = ''
            THROW 60001, 'ShippingAddress cannot be empty.', 1;

        IF @TrackingNumber IS NULL OR @TrackingNumber = ''
            THROW 60002, 'TrackingNumber cannot be empty.', 1;

        IF @EstimatedDelivery <= GETDATE()
            THROW 60003, 'EstimatedDelivery must be a future date.', 1;

        -- Insert new shipping
        INSERT INTO Sales.Shippings (OrderID, ShippingAddress, TrackingNumber, EstimatedDelivery)
        VALUES (@OrderID, @ShippingAddress, @TrackingNumber, @EstimatedDelivery);

		SET @NewId_output = SCOPE_IDENTITY();

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
