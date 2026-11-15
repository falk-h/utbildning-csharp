CREATE DATABASE School;
GO
USE School;
CREATE TABLE Students
(
    SID     int         NOT NULL PRIMARY KEY,
    FName   varchar(20) NOT NULL,
    LName   varchar(20) NOT NULL,
    Comment text        NULL,
);
INSERT INTO Students (SID, FName, LName, Comment)
VALUES (1, 'Klas', 'Banan', NULL),
       (2, 'Alfons', 'Åberg', 'fiktiv'),
       (3, 'Alfons', 'Åberg', 'den riktiga');
SELECT *
FROM Students;