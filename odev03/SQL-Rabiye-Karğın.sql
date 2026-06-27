CREATE DATABASE InfotechLibraryDb;
GO

USE InfotechLibraryDb;
GO

CREATE TABLE dbo.Categories(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(10) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE dbo.Books(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CategoryId INT NOT NULL REFERENCES dbo.Categories(Id),
    Title NVARCHAR(200) NOT NULL,
    ISBN NVARCHAR(20) NULL UNIQUE,
    PublishYear INT NULL,
    Publisher NVARCHAR(100) NULL
);
GO

CREATE TABLE dbo.BookCopies(
    Id INT PRIMARY KEY IDENTITY(1,1),
    BookId INT NOT NULL REFERENCES dbo.Books(Id),
    CopyNumber INT NOT NULL,
    ShelfCode NVARCHAR(30) NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT N'Available',
    CONSTRAINT UQ_BookCopies UNIQUE(BookId, CopyNumber),
    CONSTRAINT CK_BookCopies_Status CHECK (Status IN (N'Available', N'Loaned'))
);
GO

CREATE TABLE dbo.Members(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CardNumber NVARCHAR(20) NOT NULL UNIQUE,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    MembershipDate DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    IsActive BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE dbo.Loans(
    Id INT PRIMARY KEY IDENTITY(1,1),
    MemberId INT NOT NULL REFERENCES dbo.Members(Id),
    CopyId INT NOT NULL REFERENCES dbo.BookCopies(Id),
    LoanDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    DueDate DATETIME2 NOT NULL,
    ReturnDate DATETIME2 NULL,
    CONSTRAINT CK_Loans_DueDate CHECK (DueDate > LoanDate)
);
GO


INSERT INTO dbo.Categories(Code, Name)
VALUES
('ROM', N'Roman'),
('SCI', N'Bilim'),
('HIS', N'Tarih'),
('PSY', N'Psikoloji');
GO


INSERT INTO dbo.Books(CategoryId, Title, ISBN, PublishYear, Publisher)
VALUES
(1, N'Suç ve Ceza', '9789750738609', 1866, N'İş Bankası Yayınları'),

(1, N'Sefiller', '9789754589023', 1862, N'Can Yayınları'),

(2, N'Zamanın Kısa Tarihi', '9780553380163', 1988, N'Bantam'),

(2, N'Kozmos', '9780345539434', 1980, N'Random House'),

(3, N'Türklerin Tarihi', '9786050834000', 2016, N'Kronik Kitap'),

(4, N'İnsanın Anlam Arayışı', '9789751418265', 1946, N'Remus');
GO


INSERT INTO dbo.BookCopies(BookId, CopyNumber, ShelfCode)
VALUES
(1, 1, 'A-101'),
(1, 2, 'A-102'),

(2, 1, 'A-201'),
(2, 2, 'A-202'),

(3, 1, 'B-101'),

(4, 1, 'B-201'),
(4, 2, 'B-202'),

(5, 1, 'C-101'),

(6, 1, 'D-101');
GO


INSERT INTO dbo.Members(CardNumber, FullName, Email)
VALUES
('CRD1001', N'Ahmet Yılmaz', 'ahmet@example.com'),

('CRD1002', N'Ayşe Demir', 'ayse@example.com'),

('CRD1003', N'Mehmet Kaya', 'mehmet@example.com'),

('CRD1004', N'Elif Çetin', 'elif@example.com'),

('CRD1005', N'Zeynep Arslan', 'zeynep@example.com');
GO




CREATE PROCEDURE dbo.usp_CheckoutBook
(
    @MemberId INT,
    @CopyId INT,
    @LoanDays INT
)
AS
BEGIN

    SET NOCOUNT ON;

    ------- Gün sayısı kontrolü ---------
    IF @LoanDays <= 0
    BEGIN
        RAISERROR(N'Ödünç süresi 0 dan büyük olmalıdır.',16,1);
        RETURN;
    END

    ------- Üye aktif kontrolü
    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.Members
        WHERE Id = @MemberId
        AND IsActive = 1
    )
    BEGIN
        RAISERROR(N'Üye bulunamadı veya aktif değil.',16,1);
        RETURN;
    END

    ------ Kopya müsait mi kontrolü
    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.BookCopies
        WHERE Id = @CopyId
        AND Status = N'Available'
    )
    BEGIN
        RAISERROR(N'Kitap kopyası müsait değil.',16,1);
        RETURN;
    END

    ------- Aynı kopyada aktif loan var mı
    IF EXISTS
    (
        SELECT 1
        FROM dbo.Loans
        WHERE CopyId = @CopyId
        AND ReturnDate IS NULL
    )
    BEGIN
        RAISERROR(N'Bu kopya zaten ödünç verilmiş.',16,1);
        RETURN;
    END

    BEGIN TRANSACTION;

    BEGIN TRY

        INSERT INTO dbo.Loans
        (
            MemberId,
            CopyId,
            DueDate
        )
        VALUES
        (
            @MemberId,
            @CopyId,
            DATEADD(DAY, @LoanDays, SYSDATETIME())
        );

        UPDATE dbo.BookCopies
        SET Status = N'Loaned'
        WHERE Id = @CopyId;

        COMMIT TRANSACTION;

    END TRY

    BEGIN CATCH

        ROLLBACK TRANSACTION;

        THROW;

    END CATCH

END;
GO







CREATE PROCEDURE dbo.usp_ReturnBook
(
    @LoanId INT
)
AS
BEGIN

    SET NOCOUNT ON;

    DECLARE @CopyId INT;

    SELECT @CopyId = CopyId
    FROM dbo.Loans
    WHERE Id = @LoanId
    AND ReturnDate IS NULL;

    IF @CopyId IS NULL
    BEGIN
        RAISERROR(N'Aktif ödünç kaydı bulunamadı.',16,1);
        RETURN;
    END

    BEGIN TRANSACTION;

    BEGIN TRY

        UPDATE dbo.Loans
        SET ReturnDate = SYSDATETIME()
        WHERE Id = @LoanId;

        UPDATE dbo.BookCopies
        SET Status = N'Available'
        WHERE Id = @CopyId;

        COMMIT TRANSACTION;

    END TRY

    BEGIN CATCH

        ROLLBACK TRANSACTION;

        THROW;

    END CATCH

END;
GO



CREATE VIEW dbo.v_ActiveLoansDetail
AS

SELECT
    l.Id AS LoanId,
    m.FullName,
    m.CardNumber,
    b.Title,
    c.Name AS CategoryName,
    bc.ShelfCode,
    l.LoanDate,
    l.DueDate,

    CASE
        WHEN l.DueDate < SYSDATETIME()
        THEN N'Gecikmiş'
        ELSE N'Zamanında'
    END AS LoanStatus

FROM dbo.Loans l

INNER JOIN dbo.Members m
ON l.MemberId = m.Id

INNER JOIN dbo.BookCopies bc
ON l.CopyId = bc.Id

INNER JOIN dbo.Books b
ON bc.BookId = b.Id

INNER JOIN dbo.Categories c
ON b.CategoryId = c.Id

WHERE l.ReturnDate IS NULL;
GO


-- Gecikmiş ve henüz iade edilmemiş ödünçler

WITH LateLoans AS
(
    SELECT *
    FROM dbo.v_ActiveLoansDetail
    WHERE DueDate < SYSDATETIME()
)

SELECT *
FROM LateLoans;
GO


-- Daha önce en az bir kitap iade etmiş üyeler

EXEC dbo.usp_CheckoutBook
    @MemberId = 1,
    @CopyId = 1,
    @LoanDays = 7;
GO

EXEC dbo.usp_ReturnBook
    @LoanId = 1;
GO

SELECT
    m.FullName,
    m.CardNumber
FROM dbo.Members m
WHERE EXISTS
(
    SELECT 1
    FROM dbo.Loans l
    WHERE l.MemberId = m.Id
    AND l.ReturnDate IS NOT NULL
);
GO


-- Hiç ödünç almamış üyeler

SELECT
    m.FullName,
    m.CardNumber
FROM dbo.Members m

LEFT JOIN dbo.Loans l
ON m.Id = l.MemberId

WHERE l.Id IS NULL;
GO



-- Kitabı ödünç al
EXEC dbo.usp_CheckoutBook
    @MemberId = 1,
    @CopyId = 1,
    @LoanDays = 7;

-- Loan kaydını kontrol et
SELECT *
FROM dbo.Loans;

-- Kopya durumunu kontrol et
SELECT *
FROM dbo.BookCopies;

-- Ödünç kaydını iade et
EXEC dbo.usp_ReturnBook
    @LoanId = 1;

-- Güncel durum kontrolü
SELECT *
FROM dbo.Loans;

SELECT *
FROM dbo.BookCopies;
GO