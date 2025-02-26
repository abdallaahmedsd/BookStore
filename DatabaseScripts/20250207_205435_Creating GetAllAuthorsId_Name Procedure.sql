-- SQL Migration Script
-- Created on: 02/07/2025 20:54:35

CREATE PROCEDURE [People].[SP_GetAllAuthorsId_Name] 
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Select all Authors Id And Name that are not deleted
        SELECT a.Id, p.FirstName + ' ' + p.LastName AS FullName
        FROM People.Authors a INNER JOIN People.People p ON a.PersonID = p.Id
        WHERE IsDeleted = 0
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END