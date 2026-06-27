CREATE TABLE "Authors"(
    "Id" SERIAL PRIMARY KEY,
    "FirstName" VARCHAR(50) NOT NULL,
    "LastName" VARCHAR(50) NOT NULL,
    "Age" INTEGER NOT NULL
);

INSERT INTO "Authors" ("FirstName", "LastName", "Age") VALUES
    ('Orhan', 'Pamuk', 72),
    ('Elif', 'Shafak', 53),
    ('Yaşar', 'Kemal', 92);