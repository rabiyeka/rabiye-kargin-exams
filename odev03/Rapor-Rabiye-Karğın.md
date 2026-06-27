kitap ile fiziksel kitap kopyası birbirinden ayrı tutulmuştur.
Loans tablosu üyeler ile kitap kopyaları arasındaki ilişkiyi kurmakta.
Ödünç alma ve iade işlemlerinde transaction yapısını kullanma nedeni veri tutarlılığını korumaktır. bir kitap ödünç alındığında hem Loans tablosuna kayıt eklenmekte hem de ilgili kitap kopyasının durumu “Loaned” olarak güncellenmektedir. Bu işlemlerin aynı anda gerçekleşmesi sistemde hatalı veya eksik veri oluşmasını engellemektedir.

Categories > Books = Bire Çok (1-N)
Bir kategoriye ait birden fazla kitap olabilir, ancak bir kitap sadece bir kategoriye ait olabilir.

Books > BookCopies = Bire Çok (1-N)
Bir kitabın birden fazla fiziksel kopyası olabilir, ancak her kopya sadece bir kitaba bağlıdır.

Members > Loans = Bire Çok (1-N)
Bir üyenin birden fazla ödünç kaydı olabilir, ancak her ödünç kaydı sadece bir üyeye aittir.

BookCopies > Loans = Bire Çok (1-N)
Bir kitap kopyası zaman içinde birden fazla kez ödünç verilebilir, ancak her ödünç kaydı yalnızca bir kitap kopyasına bağlıdır.

Members <> BookCopies = Çoka Çok (N-N)
Üyeler ve kitap kopyaları arasında çoka çok ilişki vardır. Bu ilişki Loans tablosu üzerinden kurulmaktadır. Bir üye birçok kitap ödünç alabilir, bir kitap kopyası da farklı zamanlarda birçok üyeye ödünç verilebilir.

