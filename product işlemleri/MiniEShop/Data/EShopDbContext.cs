using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticAssets;
using Microsoft.EntityFrameworkCore;
using MiniEShop.Models;

namespace MiniEShop.Data;

public class EShopDbContext : IdentityDbContext<ApplicationUser>
{
    public EShopDbContext(DbContextOptions<EShopDbContext> options):base(options)
    {
        
    }
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Product>()
            .HasOne(p=>p.Category)
            .WithMany(c=>c.Products)
            .HasForeignKey(p=>p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Product>()
            .Property(p=>p.Price)
            .HasPrecision(10,2);
        
        builder.Entity<Cart>()
            .HasOne(c=>c.User)
            .WithMany()
            .HasForeignKey(c=>c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Cart>()
            .HasIndex(c=>c.UserId)
            .IsUnique();
        
        builder.Entity<CartItem>()
            .HasOne(i=>i.Cart)
            .WithMany(c=>c.Items)
            .HasForeignKey(i=>i.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<CartItem>()
            .HasOne(i=>i.Product)
            .WithMany()
            .HasForeignKey(i=>i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CartItem>()
            .HasIndex(i=>new {i.CartId,i.ProductId})
            .IsUnique();
        
        builder.Entity<Order>()
            .HasOne(o=>o.User)
            .WithMany()
            .HasForeignKey(o=>o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .Property(o=>o.TotalAmount)
            .HasPrecision(10,2);

        builder.Entity<Order>()
            .Property(o=>o.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Entity<OrderItem>()
            .HasOne(i=>i.Order)
            .WithMany(o=>o.Items)
            .HasForeignKey(i=>i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<OrderItem>()
            .HasOne(i=>i.Product)
            .WithMany()
            .HasForeignKey(i=>i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OrderItem>()
            .Property(i=>i.UnitPrice)
            .HasPrecision(10,2);
        
        SeedData(builder);
    }

    private static void SeedData(ModelBuilder builder)
    {

        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Elektronik", Description = "Telefon, bilgisayar ve aksesuarlar" },
            new Category { Id = 2, Name = "Ev & Yaşam", Description = "Ev yaşam ürünleri" },
            new Category { Id = 3, Name = "Moda", Description = "Giyim ve aksesuar" },
            new Category { Id = 4, Name = "Spor", Description = "Spor ekipmanları" },
            new Category { Id = 5, Name = "Kitap", Description = "Roman, eğitim ve hobi kitapları" }
        );

        builder.Entity<Product>().HasData(

            // Elektronik (13 Ürün)
            new Product { Id = 1, Name = "Kablosuz Kulaklık", Description = "Aktif gürültü önleyici", CategoryId = 1, Price = 3499m, StockQuantity = 45, ImageUrl = "https://placehold.co/400x400?text=Kablosuz+Kulaklik" },
            new Product { Id = 2, Name = "Bluetooth Hoparlör", Description = "Taşınabilir hoparlör", CategoryId = 1, Price = 1799m, StockQuantity = 32, ImageUrl = "https://placehold.co/400x400?text=Bluetooth+Hoparlor" },
            new Product { Id = 3, Name = "Akıllı Saat", Description = "Fitness takip özellikli", CategoryId = 1, Price = 4299m, StockQuantity = 28, ImageUrl = "https://placehold.co/400x400?text=Akilli+Saat" },
            new Product { Id = 4, Name = "Mekanik Klavye", Description = "RGB aydınlatmalı", CategoryId = 1, Price = 2599m, StockQuantity = 20, ImageUrl = "https://placehold.co/400x400?text=Mekanik+Klavye" },
            new Product { Id = 5, Name = "Gaming Mouse", Description = "6400 DPI", CategoryId = 1, Price = 999m, StockQuantity = 60, ImageUrl = "https://placehold.co/400x400?text=Gaming+Mouse" },
            new Product { Id = 6, Name = "Web Kamera", Description = "1080p", CategoryId = 1, Price = 1399m, StockQuantity = 25, ImageUrl = "https://placehold.co/400x400?text=Web+Kamera" },
            new Product { Id = 7, Name = "Powerbank", Description = "20000 mAh", CategoryId = 1, Price = 1199m, StockQuantity = 50, ImageUrl = "https://placehold.co/400x400?text=Powerbank" },
            new Product { Id = 8, Name = "USB Bellek", Description = "128 GB", CategoryId = 1, Price = 499m, StockQuantity = 80, ImageUrl = "https://placehold.co/400x400?text=USB+Bellek" },
            new Product { Id = 9, Name = "Harici SSD", Description = "1 TB", CategoryId = 1, Price = 3299m, StockQuantity = 18, ImageUrl = "https://placehold.co/400x400?text=Harici+SSD" },
            new Product { Id = 10, Name = "Laptop Standı", Description = "Alüminyum", CategoryId = 1, Price = 799m, StockQuantity = 41, ImageUrl = "https://placehold.co/400x400?text=Laptop+Standi" },
            new Product { Id = 11, Name = "HDMI Kablo", Description = "2 metre", CategoryId = 1, Price = 249m, StockQuantity = 95, ImageUrl = "https://placehold.co/400x400?text=HDMI+Kablo" },
            new Product { Id = 12, Name = "Kablosuz Şarj", Description = "15W", CategoryId = 1, Price = 699m, StockQuantity = 38, ImageUrl = "https://placehold.co/400x400?text=Kablosuz+Sarj" },
            new Product { Id = 13, Name = "Monitör", Description = "27 inç IPS", CategoryId = 1, Price = 7499m, StockQuantity = 14, ImageUrl = "https://placehold.co/400x400?text=Monitor" },

            // Ev & Yaşam (27 Ürün)
            new Product { Id = 14, Name = "Kahve Makinesi", Description = "Filtre kahve", CategoryId = 2, Price = 2499m, StockQuantity = 20, ImageUrl = "https://placehold.co/400x400?text=Kahve+Makinesi" },
            new Product { Id = 15, Name = "Elektrikli Süpürge", Description = "Sessiz çalışma", CategoryId = 2, Price = 6999m, StockQuantity = 10, ImageUrl = "https://placehold.co/400x400?text=Supurge" },
            new Product { Id = 16, Name = "Blender", Description = "1000W", CategoryId = 2, Price = 1499m, StockQuantity = 25, ImageUrl = "https://placehold.co/400x400?text=Blender" },
            new Product { Id = 17, Name = "Ütü", Description = "Buharlı", CategoryId = 2, Price = 1899m, StockQuantity = 15, ImageUrl = "https://placehold.co/400x400?text=Utu" },
            new Product { Id = 18, Name = "Tost Makinesi", Description = "1800W", CategoryId = 2, Price = 1699m, StockQuantity = 22, ImageUrl = "https://placehold.co/400x400?text=Tost+Makinesi" },
            new Product { Id = 19, Name = "Çay Makinesi", Description = "Cam demlik", CategoryId = 2, Price = 2299m, StockQuantity = 16, ImageUrl = "https://placehold.co/400x400?text=Cay+Makinesi" },
            new Product { Id = 20, Name = "Su Isıtıcı", Description = "1.7L", CategoryId = 2, Price = 899m, StockQuantity = 30, ImageUrl = "https://placehold.co/400x400?text=Su+Isitici" },
            new Product { Id = 21, Name = "Air Fryer", Description = "Yağsız pişirme", CategoryId = 2, Price = 4299m, StockQuantity = 12, ImageUrl = "https://placehold.co/400x400?text=Air+Fryer" },
            new Product { Id = 22, Name = "Mikser", Description = "El mikseri", CategoryId = 2, Price = 799m, StockQuantity = 24, ImageUrl = "https://placehold.co/400x400?text=Mikser" },
            new Product { Id = 23, Name = "Tencere Seti", Description = "7 Parça", CategoryId = 2, Price = 3999m, StockQuantity = 11, ImageUrl = "https://placehold.co/400x400?text=Tencere+Seti" },
            new Product { Id = 24, Name = "Tava", Description = "Granit", CategoryId = 2, Price = 699m, StockQuantity = 35, ImageUrl = "https://placehold.co/400x400?text=Tava" },
            new Product { Id = 25, Name = "Nevresim Takımı", Description = "Çift kişilik", CategoryId = 2, Price = 1299m, StockQuantity = 19, ImageUrl = "https://placehold.co/400x400?text=Nevresim" },
            new Product { Id = 26, Name = "Yastık", Description = "Ortopedik", CategoryId = 2, Price = 499m, StockQuantity = 42, ImageUrl = "https://placehold.co/400x400?text=Yastik" },
            new Product { Id = 27, Name = "Masa Lambası", Description = "LED", CategoryId = 2, Price = 599m, StockQuantity = 27, ImageUrl = "https://placehold.co/400x400?text=Masa+Lambasi" },
            new Product { Id = 28, Name = "Perde", Description = "Fon perde", CategoryId = 2, Price = 899m, StockQuantity = 18, ImageUrl = "https://placehold.co/400x400?text=Perde" },
            new Product { Id = 29, Name = "Halı", Description = "160x230", CategoryId = 2, Price = 2599m, StockQuantity = 9, ImageUrl = "https://placehold.co/400x400?text=Hali" },
            new Product { Id = 30, Name = "Banyo Seti", Description = "5 Parça", CategoryId = 2, Price = 999m, StockQuantity = 13, ImageUrl = "https://placehold.co/400x400?text=Banyo+Seti" },
            new Product { Id = 31, Name = "Çamaşır Sepeti", Description = "Katlanabilir", CategoryId = 2, Price = 399m, StockQuantity = 21, ImageUrl = "https://placehold.co/400x400?text=Camasir+Sepeti" },
            new Product { Id = 32, Name = "Saklama Kutusu", Description = "Plastik", CategoryId = 2, Price = 199m, StockQuantity = 48, ImageUrl = "https://placehold.co/400x400?text=Saklama+Kutusu" },
            new Product { Id = 33, Name = "Çöp Kovası", Description = "Pedallı", CategoryId = 2, Price = 449m, StockQuantity = 26, ImageUrl = "https://placehold.co/400x400?text=Cop+Kovasi" },
            new Product { Id = 34, Name = "Askılık", Description = "Metal", CategoryId = 2, Price = 349m, StockQuantity = 34, ImageUrl = "https://placehold.co/400x400?text=Askilik" },
            new Product { Id = 35, Name = "Raf", Description = "Duvar rafı", CategoryId = 2, Price = 699m, StockQuantity = 20, ImageUrl = "https://placehold.co/400x400?text=Raf" },
            new Product { Id = 36, Name = "Ayna", Description = "Dekoratif", CategoryId = 2, Price = 999m, StockQuantity = 14, ImageUrl = "https://placehold.co/400x400?text=Ayna" },
            new Product { Id = 37, Name = "Mum Seti", Description = "Kokulu", CategoryId = 2, Price = 299m, StockQuantity = 40, ImageUrl = "https://placehold.co/400x400?text=Mum+Seti" },
            new Product { Id = 38, Name = "Duvar Saati", Description = "Modern", CategoryId = 2, Price = 799m, StockQuantity = 17, ImageUrl = "https://placehold.co/400x400?text=Duvar+Saati" },
            new Product { Id = 39, Name = "Vazo", Description = "Seramik", CategoryId = 2, Price = 549m, StockQuantity = 23, ImageUrl = "https://placehold.co/400x400?text=Vazo" },
            new Product { Id = 40, Name = "Battaniye", Description = "Çift kişilik", CategoryId = 2, Price = 1199m, StockQuantity = 16, ImageUrl = "https://placehold.co/400x400?text=Battaniye" },

            // Moda (5 Ürün)
            new Product { Id = 41, Name = "Tişört", Description = "Pamuklu", CategoryId = 3, Price = 399m, StockQuantity = 100, ImageUrl = "https://placehold.co/400x400?text=Tisort" },
            new Product { Id = 42, Name = "Kot Pantolon", Description = "Slim Fit", CategoryId = 3, Price = 899m, StockQuantity = 50, ImageUrl = "https://placehold.co/400x400?text=Kot+Pantolon" },
            new Product { Id = 43, Name = "Spor Ayakkabı", Description = "Koşu ayakkabısı", CategoryId = 3, Price = 2199m, StockQuantity = 35, ImageUrl = "https://placehold.co/400x400?text=Spor+Ayakkabi" },
            new Product { Id = 44, Name = "Ceket", Description = "Mevsimlik", CategoryId = 3, Price = 1699m, StockQuantity = 18, ImageUrl = "https://placehold.co/400x400?text=Ceket" },
            new Product { Id = 45, Name = "Sırt Çantası", Description = "Günlük kullanım", CategoryId = 3, Price = 799m, StockQuantity = 30, ImageUrl = "https://placehold.co/400x400?text=Sirt+Cantasi" },

            // Spor (3 Ürün)
            new Product { Id = 46, Name = "Yoga Matı", Description = "Kaymaz yüzey", CategoryId = 4, Price = 599m, StockQuantity = 25, ImageUrl = "https://placehold.co/400x400?text=Yoga+Mati" },
            new Product { Id = 47, Name = "Dambıl Seti", Description = "10 kg", CategoryId = 4, Price = 1299m, StockQuantity = 14, ImageUrl = "https://placehold.co/400x400?text=Dambil+Seti" },
            new Product { Id = 48, Name = "Futbol Topu", Description = "5 Numara", CategoryId = 4, Price = 699m, StockQuantity = 22, ImageUrl = "https://placehold.co/400x400?text=Futbol+Topu" },

            // Kitap (2 Ürün)
            new Product { Id = 49, Name = "Clean Code", Description = "Yazılım geliştirme", CategoryId = 5, Price = 799m, StockQuantity = 15, ImageUrl = "https://placehold.co/400x400?text=Clean+Code" },
            new Product { Id = 50, Name = "Design Patterns", Description = "GoF", CategoryId = 5, Price = 899m, StockQuantity = 12, ImageUrl = "https://placehold.co/400x400?text=Design+Patterns" }
        );

    }

}
