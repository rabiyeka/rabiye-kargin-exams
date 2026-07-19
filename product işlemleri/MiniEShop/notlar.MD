Bu çalışmada hangi konuları ele alıyoruz?
1) Monolit eticaret mvc uygulamasının vizyonunu tanımak
2) Katman yapımızda yenilikler göreceksiniz
3) Generic Repository Design Pattern
4) UnitOfWork Design Pattern
5) Soyutlama

Tarayıcı -> Controller -> Services -> IUnitOfWork -> IRepository -> DB

Entities:
ApplicationUser  <- Cart <- CartItem -> Product
ApplicationUser  <- Order <- OrderItem -> Product
Category <- Product

