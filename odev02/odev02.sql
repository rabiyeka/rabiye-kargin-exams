
CREATE DATABASE IzmirYazEtkinlikleri;
GO

USE IzmirYazEtkinlikleri;
GO


CREATE TABLE EtkinlikKategorileri (
    KategoriID   INT IDENTITY(1,1) PRIMARY KEY,
    Kod          VARCHAR(10)  NOT NULL UNIQUE,
    Ad           VARCHAR(100) NOT NULL
);


CREATE TABLE Gonulluler (
    GonulluID       INT IDENTITY(1,1) PRIMARY KEY,
    AdSoyad         VARCHAR(100) NOT NULL,
    Eposta          VARCHAR(150) NULL,
    Telefon         VARCHAR(20)  NULL,
    KayitTarihi     DATE         NOT NULL DEFAULT GETDATE(),
    KategoriID      INT          NOT NULL,
    Aktif           BIT          NOT NULL DEFAULT 1,  
    Notlar          VARCHAR(500) NULL,
  
    SahaTercih      BIT          NOT NULL DEFAULT 1,  -- 1=saha, 0=yalnızca çevrimiçi
    CONSTRAINT FK_Gonullu_Kategori FOREIGN KEY (KategoriID) REFERENCES EtkinlikKategorileri(KategoriID),
    CONSTRAINT UQ_Gonullu_Eposta  UNIQUE (Eposta),
    CONSTRAINT CHK_Gonullu_Iletisim CHECK (Eposta IS NOT NULL OR Telefon IS NOT NULL)
);


CREATE TABLE Etkinlikler (
    EtkinlikID      INT IDENTITY(1,1) PRIMARY KEY,
    Ad              VARCHAR(200) NOT NULL,
    Baslangic       DATETIME     NOT NULL,
    Bitis           DATETIME     NOT NULL,
    Butce           DECIMAL(10,2) NOT NULL,
    Durum           VARCHAR(20)  NOT NULL DEFAULT 'Planlandi',
    CONSTRAINT CHK_Etkinlik_Tarih CHECK (Bitis >= Baslangic),
    CONSTRAINT CHK_Etkinlik_Durum CHECK (Durum IN ('Planlandi', 'Devam Ediyor', 'Tamamlandi', 'Iptal'))
);


CREATE TABLE GonulluAtamalari (
    AtamaID         INT IDENTITY(1,1) PRIMARY KEY,
    GonulluID       INT          NOT NULL,
    EtkinlikID      INT          NOT NULL,
    GorevRolu        VARCHAR(50)  NOT NULL,
    TahminiSaat     INT          NULL,
    CONSTRAINT FK_Atama_Gonullu   FOREIGN KEY (GonulluID)   REFERENCES Gonulluler(GonulluID),
    CONSTRAINT FK_Atama_Etkinlik  FOREIGN KEY (EtkinlikID)  REFERENCES Etkinlikler(EtkinlikID),
    CONSTRAINT UQ_Gonullu_Etkinlik UNIQUE (GonulluID, EtkinlikID)
);


ALTER TABLE Etkinlikler
ADD Mekan VARCHAR(150) NULL;
GO


INSERT INTO EtkinlikKategorileri (Kod, Ad) VALUES
('MUZ',  'Müzik'),
('BEL',  'Belgesel'),
('OYUN', 'Çocuk Oyunu');


INSERT INTO Gonulluler (AdSoyad, Eposta, Telefon, KayitTarihi, KategoriID, Aktif, Notlar, SahaTercih) VALUES
('Ayşe Yılmaz',    'ayse@mail.com',   '05321111111', '2022-03-15', 1, 1, NULL,           1),
('Mehmet Kaya',    'mehmet@mail.com', '05322222222', '2023-06-01', 1, 1, 'Konser deneyimi', 1),
('Zeynep Demir',   'zeynep@mail.com', NULL,          '2023-09-10', 2, 1, NULL,           0),
('Ali Çelik',      NULL,              '05324444444', '2024-01-20', 2, 1, NULL,           1),
('Fatma Öztürk',   'fatma@mail.com',  '05325555555', '2024-05-05', 3, 0, 'Pasif gönüllü', 1);


INSERT INTO Etkinlikler (Ad, Baslangic, Bitis, Butce, Durum, Mekan) VALUES
('Yaz Konseri 2024',        '2024-07-15 20:00', '2024-07-15 23:00', 50000.00, 'Tamamlandi',  'Kordon Açık Alan'),
('Belgesel Gecesi',         '2024-08-10 19:00', '2024-08-10 21:30', 15000.00, 'Tamamlandi',  'Kültür Merkezi Salon A'),
('Çocuk Tiyatrosu Festivali','2024-09-01 14:00', '2024-09-01 17:00', 25000.00, 'Planlandi',  'Alsancak Park');


INSERT INTO GonulluAtamalari (GonulluID, EtkinlikID, GorevRolu, TahminiSaat) VALUES
(1, 1, 'Host',       4),
(2, 1, 'Teknik',     6),
(3, 2, 'Host',       3),
(4, 2, 'Fotoğraf',   4),
(1, 2, 'Teknik',     2),
(5, 3, 'Host',       5),
(2, 3, 'Teknik',     4),
(3, 3, 'Fotoğraf',   3),
(4, 1, 'Fotoğraf',   3);


UPDATE Gonulluler
SET Telefon = '05321112233'
WHERE AdSoyad = 'Ayşe Yılmaz';

SELECT 'UPDATE sonrası - Ayşe Yılmaz' AS Islem;
SELECT * FROM Gonulluler WHERE AdSoyad = 'Ayşe Yılmaz';


INSERT INTO EtkinlikKategorileri (Kod, Ad) VALUES ('TEST', 'Test Kategori');
DELETE FROM EtkinlikKategorileri WHERE Kod = 'TEST';

SELECT 'DELETE sonrası - Kategoriler' AS Islem;
SELECT * FROM EtkinlikKategorileri;


UPDATE Gonulluler SET Aktif = 0 WHERE AdSoyad = 'Fatma Öztürk';

SELECT 'Soft delete sonrası - Pasif gönüllüler' AS Islem;
SELECT * FROM Gonulluler WHERE Aktif = 0;


SELECT 'Tüm Kategoriler' AS Tablo; SELECT * FROM EtkinlikKategorileri;
SELECT 'Tüm Gönüllüler'  AS Tablo; SELECT * FROM Gonulluler;
SELECT 'Tüm Etkinlikler' AS Tablo; SELECT * FROM Etkinlikler;
SELECT 'Tüm Atamalar'    AS Tablo; SELECT * FROM GonulluAtamalari;

GO


SELECT
    g.AdSoyad       AS GonulluAdi,
    g.Eposta,
    g.Telefon,
    k.Ad            AS KategoriAdi
FROM Gonulluler g
JOIN EtkinlikKategorileri k ON g.KategoriID = k.KategoriID
WHERE g.Aktif = 1
ORDER BY g.AdSoyad;


SELECT
    g.AdSoyad       AS GonulluAdi,
    g.KayitTarihi,
    k.Ad            AS KategoriAdi
FROM Gonulluler g
JOIN EtkinlikKategorileri k ON g.KategoriID = k.KategoriID
WHERE g.KayitTarihi >= '2023-01-01'
ORDER BY g.KayitTarihi;


SELECT
    k.Ad            AS KategoriAdi,
    COUNT(g.GonulluID) AS GonulluSayisi
FROM EtkinlikKategorileri k
LEFT JOIN Gonulluler g ON k.KategoriID = g.KategoriID
GROUP BY k.Ad
HAVING COUNT(g.GonulluID) >= 1;


SELECT
    e.Ad            AS EtkinlikAdi,
    COUNT(a.AtamaID) AS AtananGonulluSayisi,
    e.Butce
FROM Etkinlikler e
LEFT JOIN GonulluAtamalari a ON e.EtkinlikID = a.EtkinlikID
GROUP BY e.Ad, e.Butce
ORDER BY e.Butce DESC;


SELECT
    e.Ad            AS EtkinlikAdi,
    e.Butce,
    g.AdSoyad       AS GonulluAdi,
    a.GorevRolu
FROM Etkinlikler e
JOIN GonulluAtamalari a ON e.EtkinlikID = a.EtkinlikID
JOIN Gonulluler g       ON a.GonulluID  = g.GonulluID
WHERE e.Butce = (SELECT MAX(Butce) FROM Etkinlikler);


SELECT
    g.AdSoyad       AS GonulluAdi,
    COUNT(DISTINCT a.EtkinlikID) AS EtkinlikSayisi
FROM Gonulluler g
JOIN GonulluAtamalari a ON g.GonulluID = a.GonulluID
GROUP BY g.AdSoyad
HAVING COUNT(DISTINCT a.EtkinlikID) >= 2;
