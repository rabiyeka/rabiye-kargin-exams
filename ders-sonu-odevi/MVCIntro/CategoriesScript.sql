CREATE TABLE "Categories"(
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(250) NULL
);


INSERT INTO "Categories" ("Name", "Description") VALUES
    ('Roman','Roman açıklaması'),
    ('Edebiyat','Edebiyat açıklaması'),
    ('Bilim','Bilim açıklaması');