--USE "MVC_DB";

CREATE TABLE "Books"(
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(100) NOT NULL,
    "Author" VARCHAR(50) NOT NULL,
    "Category" VARCHAR(50) NOT NULL,
    "Price" NUMERIC(18,2) NOT NULL,
    "Description" TEXT
);

INSERT INTO "Books" ("Title", "Author", "Category", "Price", "Description") VALUES
    ('Nutuk', 'Mustafa Kemal Atatürk', 'Tarih', 120.00, 'Cumhuriyetimizin kuruluş destanının bizzat kurucusu tarafından anlatımı.'),
    ('Simyacı', 'Paulo Coelho', 'Edebiyat', 95.00, 'Kendi kişisel menkıbesini bulmaya çalışan Endülüslü çoban Santiago''nun öyküsü.'),
    ('Sapiens', 'Yuval Noah Harari', 'Tarih / Bilim', 180.00, 'İnsan türünün kısa bir tarihi.'),
    ('1984', 'George Orwell', 'Distopya', 85.00, 'Bireyselliğin ve özgürlüğün yok edildiği totaliter bir dünya düzeni.'),
    ('Suç ve Ceza', 'Fyodor Dostoyevski', 'Klasik Edebiyat', 150.00, 'Bir üniversite öğrencisinin ahlaki ikilemleri ve vicdan azabı.'),
    ('Kürk Mantolu Madonna', 'Sabahattin Ali', 'Roman', 65.00, 'Raif Efendi ile Maria Puder arasındaki hüzünlü aşk hikayesi.'),
    ('Tutunamayanlar', 'Oğuz Atay', 'Modern Klasik', 190.00, 'Türk edebiyatının en önemli modernist romanlarından biri.'),
    ('Devlet', 'Platon', 'Felsefe', 110.00, 'İdeal devlet düzeni ve adalet kavramı üzerine antik bir başyapıt.'),
    ('Zamanın Kısa Tarihi', 'Stephen Hawking', 'Popüler Bilim', 130.00, 'Evrenin başlangıcı, kara delikler ve uzay-zaman kavramı.'),
    ('Dune', 'Frank Herbert', 'Bilim Kurgu', 210.00, 'Çöl gezegeni Arrakis''te geçen epik bir iktidar ve hayatta kalma mücadelesi.'),
    ('Şeker Portakalı', 'Jose Mauro de Vasconcelos', 'Edebiyat', 75.00, 'Küçük Zeze''nin hayal gücü ve acılarla dolu dünyası.'),
    ('Kozmos', 'Carl Sagan', 'Bilim / Astronomi', 160.00, 'Evrenin yapısı, yaşamın kökeni ve insanlığın kozmostaki yeri.');