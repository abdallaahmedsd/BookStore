-- SQL Migration Script
-- Created on: 02/08/2025 16:07:41

-- Update existing author record
ALTER PROCEDURE [People].[SP_UpdateAuthor]
    @Id INT,
    @PersonId INT,
    @FirstName NVARCHAR(100) = NULL,
    @LastName NVARCHAR(100) = NULL,
    @Email NVARCHAR(100) = NULL,
    @Phone NVARCHAR(50) = NULL,
    @ProfileImage NVARCHAR(MAX) = NULL,
    @NationalityID INT = NULL,
    @Bio NVARCHAR(MAX) = NULL,
    @UserID INT
AS
BEGIN
    BEGIN TRY
        SET NOCOUNT ON;

        -- Validate input parameters
        IF People.Fun_IsAuthorExistsById(@Id) <> 1
            THROW 50003, 'Author Id does not exist.', 1;
            
        IF People.Fun_IsPersonExistsById(@PersonId) <> 1
            THROW 50004, 'Person Id does not exist.', 1;
            
        IF dbo.Fun_IsUserExistsById(@UserID) <> 1
            THROW 50001, 'Invalid User Id', 1;
            
        IF @Bio IS NOT NULL AND @Bio = ''
            THROW 50002, 'Invalid Bio', 1;

        -- Update author record with transaction
        BEGIN TRANSACTION;
            -- Update person details
            EXEC People.SP_UpdatePerson
                @Id = @PersonId,
                @FirstName = @FirstName,
                @LastName = @LastName,
                @NationalityID = @NationalityID,
                @Phone = @Phone,
                @Email = @Email,
                @ProfileImage = @ProfileImage;

            -- Update author bio
            UPDATE Authors
            SET Bio = ISNULL(@Bio, Bio)
            WHERE Id = @Id;

        COMMIT TRANSACTION;
        RETURN 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
        RETURN 0;
    END CATCH;
END;
