using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniEShop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Telefon, bilgisayar ve aksesuarlar", "Elektronik" },
                    { 2, "Ev yaşam ürünleri", "Ev & Yaşam" },
                    { 3, "Giyim ve aksesuar", "Moda" },
                    { 4, "Spor ekipmanları", "Spor" },
                    { 5, "Roman, eğitim ve hobi kitapları", "Kitap" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, "Aktif gürültü önleyici", "https://placehold.co/400x400?text=Kablosuz+Kulaklik", "Kablosuz Kulaklık", 3499m, 45 },
                    { 2, 1, "Taşınabilir hoparlör", "https://placehold.co/400x400?text=Bluetooth+Hoparlor", "Bluetooth Hoparlör", 1799m, 32 },
                    { 3, 1, "Fitness takip özellikli", "https://placehold.co/400x400?text=Akilli+Saat", "Akıllı Saat", 4299m, 28 },
                    { 4, 1, "RGB aydınlatmalı", "https://placehold.co/400x400?text=Mekanik+Klavye", "Mekanik Klavye", 2599m, 20 },
                    { 5, 1, "6400 DPI", "https://placehold.co/400x400?text=Gaming+Mouse", "Gaming Mouse", 999m, 60 },
                    { 6, 1, "1080p", "https://placehold.co/400x400?text=Web+Kamera", "Web Kamera", 1399m, 25 },
                    { 7, 1, "20000 mAh", "https://placehold.co/400x400?text=Powerbank", "Powerbank", 1199m, 50 },
                    { 8, 1, "128 GB", "https://placehold.co/400x400?text=USB+Bellek", "USB Bellek", 499m, 80 },
                    { 9, 1, "1 TB", "https://placehold.co/400x400?text=Harici+SSD", "Harici SSD", 3299m, 18 },
                    { 10, 1, "Alüminyum", "https://placehold.co/400x400?text=Laptop+Standi", "Laptop Standı", 799m, 41 },
                    { 11, 1, "2 metre", "https://placehold.co/400x400?text=HDMI+Kablo", "HDMI Kablo", 249m, 95 },
                    { 12, 1, "15W", "https://placehold.co/400x400?text=Kablosuz+Sarj", "Kablosuz Şarj", 699m, 38 },
                    { 13, 1, "27 inç IPS", "https://placehold.co/400x400?text=Monitor", "Monitör", 7499m, 14 },
                    { 14, 2, "Filtre kahve", "https://placehold.co/400x400?text=Kahve+Makinesi", "Kahve Makinesi", 2499m, 20 },
                    { 15, 2, "Sessiz çalışma", "https://placehold.co/400x400?text=Supurge", "Elektrikli Süpürge", 6999m, 10 },
                    { 16, 2, "1000W", "https://placehold.co/400x400?text=Blender", "Blender", 1499m, 25 },
                    { 17, 2, "Buharlı", "https://placehold.co/400x400?text=Utu", "Ütü", 1899m, 15 },
                    { 18, 2, "1800W", "https://placehold.co/400x400?text=Tost+Makinesi", "Tost Makinesi", 1699m, 22 },
                    { 19, 2, "Cam demlik", "https://placehold.co/400x400?text=Cay+Makinesi", "Çay Makinesi", 2299m, 16 },
                    { 20, 2, "1.7L", "https://placehold.co/400x400?text=Su+Isitici", "Su Isıtıcı", 899m, 30 },
                    { 21, 2, "Yağsız pişirme", "https://placehold.co/400x400?text=Air+Fryer", "Air Fryer", 4299m, 12 },
                    { 22, 2, "El mikseri", "https://placehold.co/400x400?text=Mikser", "Mikser", 799m, 24 },
                    { 23, 2, "7 Parça", "https://placehold.co/400x400?text=Tencere+Seti", "Tencere Seti", 3999m, 11 },
                    { 24, 2, "Granit", "https://placehold.co/400x400?text=Tava", "Tava", 699m, 35 },
                    { 25, 2, "Çift kişilik", "https://placehold.co/400x400?text=Nevresim", "Nevresim Takımı", 1299m, 19 },
                    { 26, 2, "Ortopedik", "https://placehold.co/400x400?text=Yastik", "Yastık", 499m, 42 },
                    { 27, 2, "LED", "https://placehold.co/400x400?text=Masa+Lambasi", "Masa Lambası", 599m, 27 },
                    { 28, 2, "Fon perde", "https://placehold.co/400x400?text=Perde", "Perde", 899m, 18 },
                    { 29, 2, "160x230", "https://placehold.co/400x400?text=Hali", "Halı", 2599m, 9 },
                    { 30, 2, "5 Parça", "https://placehold.co/400x400?text=Banyo+Seti", "Banyo Seti", 999m, 13 },
                    { 31, 2, "Katlanabilir", "https://placehold.co/400x400?text=Camasir+Sepeti", "Çamaşır Sepeti", 399m, 21 },
                    { 32, 2, "Plastik", "https://placehold.co/400x400?text=Saklama+Kutusu", "Saklama Kutusu", 199m, 48 },
                    { 33, 2, "Pedallı", "https://placehold.co/400x400?text=Cop+Kovasi", "Çöp Kovası", 449m, 26 },
                    { 34, 2, "Metal", "https://placehold.co/400x400?text=Askilik", "Askılık", 349m, 34 },
                    { 35, 2, "Duvar rafı", "https://placehold.co/400x400?text=Raf", "Raf", 699m, 20 },
                    { 36, 2, "Dekoratif", "https://placehold.co/400x400?text=Ayna", "Ayna", 999m, 14 },
                    { 37, 2, "Kokulu", "https://placehold.co/400x400?text=Mum+Seti", "Mum Seti", 299m, 40 },
                    { 38, 2, "Modern", "https://placehold.co/400x400?text=Duvar+Saati", "Duvar Saati", 799m, 17 },
                    { 39, 2, "Seramik", "https://placehold.co/400x400?text=Vazo", "Vazo", 549m, 23 },
                    { 40, 2, "Çift kişilik", "https://placehold.co/400x400?text=Battaniye", "Battaniye", 1199m, 16 },
                    { 41, 3, "Pamuklu", "https://placehold.co/400x400?text=Tisort", "Tişört", 399m, 100 },
                    { 42, 3, "Slim Fit", "https://placehold.co/400x400?text=Kot+Pantolon", "Kot Pantolon", 899m, 50 },
                    { 43, 3, "Koşu ayakkabısı", "https://placehold.co/400x400?text=Spor+Ayakkabi", "Spor Ayakkabı", 2199m, 35 },
                    { 44, 3, "Mevsimlik", "https://placehold.co/400x400?text=Ceket", "Ceket", 1699m, 18 },
                    { 45, 3, "Günlük kullanım", "https://placehold.co/400x400?text=Sirt+Cantasi", "Sırt Çantası", 799m, 30 },
                    { 46, 4, "Kaymaz yüzey", "https://placehold.co/400x400?text=Yoga+Mati", "Yoga Matı", 599m, 25 },
                    { 47, 4, "10 kg", "https://placehold.co/400x400?text=Dambil+Seti", "Dambıl Seti", 1299m, 14 },
                    { 48, 4, "5 Numara", "https://placehold.co/400x400?text=Futbol+Topu", "Futbol Topu", 699m, 22 },
                    { 49, 5, "Yazılım geliştirme", "https://placehold.co/400x400?text=Clean+Code", "Clean Code", 799m, 15 },
                    { 50, 5, "GoF", "https://placehold.co/400x400?text=Design+Patterns", "Design Patterns", 899m, 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId_ProductId",
                table: "CartItems",
                columns: new[] { "CartId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
