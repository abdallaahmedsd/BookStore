-- SQL Migration Script
-- Created on: 02/07/2025 17:57:48

CREATE PROCEDURE [Books].[SP_GetBookNavgationById]
    @Id INT
AS
BEGIN
    /*
    Summary:
    The stored procedure [Books].[SP_GetBookNavgationById] retrieves comprehensive details about a book, 
    including author, category, and language information, based on the provided book ID. It ensures that the 
    book exists and is not marked as deleted before fetching the information. The procedure handles potential 
    errors gracefully using a TRY...CATCH block.
    
    Usage:
    To execute the stored procedure, you can use the following SQL command:
    
    DECLARE @BookId INT = 1; -- Replace with the actual book ID you want to retrieve
    EXEC [Books].[SP_GetBookNavgationById] @Id = @BookId;
    */

    -- Validate input parameter to ensure it exists in the database and is not marked as deleted
    IF Books.Fun_IsBookExistsByID(@Id) <> 1
    BEGIN
        THROW 50001,'Invalid book ID.', 1;
    END
    SET NOCOUNT ON;

    BEGIN TRY
        -- Get book details by book ID
        -- This query retrieves basic book information such as Id, Title, Price, Description, ISBA, PublicationDate, and CoverImage
        SELECT
            b.Id,
            b.Title,
            b.Price,
            b.Description,
            b.ISBA,
            b.PublicationDate,
            b.CoverImage
        FROM
            Books.Books b
        WHERE
            b.Id = @Id AND b.IsDeleted = 0;

        -- Get detailed author information including Bio and CreatedBy fields
        -- This query joins Books, Authors, and People tables to retrieve comprehensive author information
        SELECT
            b.*, 
            a.Bio, 
            a.CreatedBy, 
            p.*
        FROM
            Books.Books b
        LEFT JOIN People.Authors a ON b.AuthorID = a.Id
        INNER JOIN People.People p ON a.PersonID = p.Id
        WHERE
            b.Id = @Id AND b.IsDeleted = 0;

        -- Get category details for the book
        -- This query retrieves the CategoryID, CategoryName, and CreatedBy fields from the Categories table
        SELECT
            c.Id AS CategoryID,
            c.Name AS CategoryName,
            c.CreatedBy
        FROM
            Books.Books b
        LEFT JOIN Books.Categories c ON b.CategoryID = c.Id
        WHERE
            b.Id = @Id AND b.IsDeleted = 0;

        -- Get language details for the book
        -- This query retrieves the LanguageID and LanguageName fields from the Languages table
        SELECT
            l.Id AS LanguageID,
            l.LanguageName
        FROM
            Books.Books b
        LEFT JOIN Books.Languages l ON b.LanguageID = l.Id
        WHERE
            b.Id = @Id AND b.IsDeleted = 0;
    END TRY
    BEGIN CATCH
        -- Handle any errors that occurred during the execution
        -- The THROW statement re-throws the error that caused the catch block to execute
        THROW;
    END CATCH
END
GO


