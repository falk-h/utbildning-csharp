CREATE DATABASE StoredProcedures;
GO

USE StoredProcedures;
CREATE TABLE Customer
(
    Id        int         NOT NULL PRIMARY KEY,
    FirstName varchar(20) NOT NULL,
    LastName  varchar(20) NOT NULL,
);
INSERT INTO Customer (Id, FirstName, LastName)
VALUES (1, 'Klas', 'Banan'),
       (2, 'Astrid', 'Svensson'),
       (3, 'Mark', 'Andersson');
GO

CREATE PROCEDURE GetMarksAndAstrids AS
SELECT *
FROM Customer
WHERE FirstName = 'Mark'
   OR FirstName = 'Astrid'
ORDER BY FirstName DESC;
GO

EXEC GetMarksAndAstrids;