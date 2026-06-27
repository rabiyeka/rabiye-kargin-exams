// Web uygulaması yapılandırmak ve başlatmak için gerekli olan WebApplicationBuilder tipindeki nesne oluşturulur.
var builder = WebApplication.CreateBuilder(args);

// ASP.NET Core MVC mimarisinin çalışabilmesi için gerekli olan servisleri(View,Controller,TempData, ModelBinding vb) uygulamanın IoC servis havuzuna(DI Container) kaydedilir.
builder.Services.AddControllersWithViews();

// Yapılandırılan servisleri ve builder ayarlarını derleyerek çalışmaya hazır bir WebApplication nesnesi üretilir.
var app = builder.Build();


// Optimize edilmiş static assetlerimizi(Css, Js, resimlerimiz...) yönetimini devreye alır.
// Derleme zmanında dosyaları sıkıştırır ve benzersiz hash değerleri kullanarak önbellek yönetimini otomatikleştirir.
app.MapStaticAssets();

// MVC için varsayılan rota şablonunu/kalıbını belirler.
// WithStaticAssets, bu rotaların optimize edilmiş static assetlerimizi tanımasını sağlar.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
).WithStaticAssets();

// Uygulamayı ayağa kaldırır.
app.Run();