USE StoredProcedures;
GO

CREATE PROCEDURE SearchForLastName @pattern text = '%' AS
SELECT *
FROM Customer
WHERE LastName LIKE @pattern
ORDER BY LastName;
GO

EXEC SearchForLastName;
GO
EXEC SearchForLastName @pattern = '%e%';
GO

CREATE PROCEDURE GetLastNameStartingWithP AS
    EXEC SearchForLastName @pattern = 'P%';
GO

EXEC GetLastNameStartingWithP;