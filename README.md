# ObjectManagementSystem

<b>Projemizin ismi:</b> ObjectManagementSystem

Okulumuz bünyesinde öğrencilerin kullanımına sunulmuş teknik alet ve eşyaların online olarak rezerve edilmesi, okula gelinerek ödünç alınması ve geri teslim edilme sürecinin takip edilmesini kolaylaştıran bir web sitesi projesi tasarlamaktayız.

Projemizin müşterisi olan Tuba Çonka Yıldız Hocamızın istekleri ve talepleri doğrultusunda gerekli güncellemeler ve optimizasyonlar yapılmaktadır.

Projemizin geliştirme süreçlerini içeren "Sprintplan", gereksinim analizini gösteren "Anforderungsanalyse", müşteri taleplerini ve onayını gösteren "Kundenanfragen" ve projemizin kapsamlı dokümantasyonunu özetleyen "Projektdokumentation" dosyalarımıza "Documents" klasörü altında ulaşabilirsiniz.

<b>Proje müşterisi:</b> Tuba Çonka Yıldız

<b>Proje başlangıç tarihi:</b> 04.10.2021

<b>Proje bitiş tarihi:</b> 06.01.2022


# Rapor Kayıtları

# # 08.10.2021

* Database için tablolar ve ilişkileri oluşturuldu.
* Database'in üzerine sağ tık -> Tasks -> Script next next denerek script desktop'a kaydedildi.
* Üyerlerle paylaşılmak üzere Github'a eklendi.
* Diğer üyeler bunu indirip Microsoft SQL Management System'da öncesinde create new Database "DB_LIBRARY" yaparak ve daha sonrasında ctrl+a yapıp execute'e basarak çalıştırabilirler.


* NOT: Code -> open with GitHub Desktop diyerek desktop üzerinden kodlama yapılabilir. Daha sonra değişiklikler açıklaması yapılarak commit edilir ve push origin denilerek ortak alana aktarılır.

# # 10.10.2021

* Kategori bölümü tamamlandı. Kategori ekleme, güncelleme ve silme kısımları ayarlandı. Tasarım düzenlemesi gerekli.
* Tedbir amacli categoryPart branch'ı acip oraya yükledim çünkü veri tabanı bağlantısında web.config içerisinde connectionStrings kısmında kalın yazılarla belirtilen yere herkesin kendi server'ının properties'ine bakıp adını kopyalayıp oraya yapıştırması ve öyle bağlanmayı denemesi lazım.
!!Database ismi ve tabloları burada paylaştığımız script olmalı farklı bir isim ve tablo yapısında sorun çıkabilir.!!

### web.config içerisindeki bölüm
  <connectionStrings
    <add name="DB_LIBRARYEntities" connectionString="metadata=res://*/Models.Entity.Model1.csdl|res://*/Models.Entity.Model1.ssdl|res://*/Models.Entity.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=  **DESKTOP-UE5II9K**  ;initial catalog=DB_LIBRARY;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient"
  </connectionStrings>

# # 11.10.2021

* NOT1 : Eğer kodu visual studio ile actığınızda sağ tarafta references, properties vs görünmüyorsa proje, dosya olarak açılmış demektir. Çalışan bir proje olarak açmak için altta bulunan LibraryManagementSystem.sln'e tıklayabilirsiniz.
* NOT2 : Eğer hala Model ksımının altında entity dosyası görünmüyorsa github desktop üzerinden yukarıda da yazıldığı gibi current branch'i categoryPart yapıp fetch origin diyerek gelmesini sağlayabilirsiniz.
* NOT3 : Eğer projeye bir şey eklemek istediğinizde aradığınız şeyi bulamıyorsanız, -create new project en altta -install more tools and features diyerek .Net ile ilgili her şeyi işaretleyip -update tıklayın.
* NOT4 : Eğer projeyi çalıştırdığınızda "System.NullReferenceException" veriyorsa App_Start -> BundleConfig.cs ->  bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/bootstrap.js")); bu kod yerine -> bundles.Add(new Bundle("~/bundles/mybundle").Include(
        "~/Scripts/bootstrap.js")); bu kodu yazmalısınız.\
        
Kategori tablosu güzelleştirildi.

# # 19.10.2021

* Kitap bölümü tamamlandı

# # 21.10.2021

* Kitap bölümüne arama çubuğu eklendi --> diğer bölümlere de aynı şekilde ekleyebiliriz\
* Personel ekleme, güncelleme, silme kısmı ayarlandı\
* Üye ekleme, güncelleme, silme ve input validation kısımları tamamlandı\
* Üye tablosu için sayfalama ayarı yapıldı (yani her sayfada tablo içinde belirli sayıda üye gösteriliyor) --> diger bölümlere de eklenebilir, farklı tipleri kullanılabilir 

# # 29.10.2021

* Kitap ödünç verme ve alma bölümleri tamamlandı.
* Chat kısmını eklemek için işimize yarayabilecek linkler 
   ** https://pusher.com/tutorials/chat-aspnet/
* Artık en güncel branch main (merge'ledim) ona ekstra branch açılarak çalışılabilinir.

# # 01.11.2021

* Display Window bölümü tamamlandı.
* Book Tablosuna BOOKIMAGE kolonu eklendi varchar(250).
* Hızlıresim.com'a resimler eklenerek linkleri <img source=""> kısmına yazılarak websitemizde görüntülenmesi sağlandı.

# # 02.11.2021

* Dashboard bölümü tamamlandı.
* Statistics bölümü yaratıldı ve kullanıcıi kitap sayısı gibi özellikler db'den çekilerek eklendi.
* Boş bir chart eklendi ileride kullanılabilir.
* Anasayfa'ya photo slides eklendi fakat css'leri çakışıyor.

# # 04.11.2021

* Öğrenci panel bölümü ayarlandı
* Tasarımı düzenlenebilir


# # 06.11.2021

* Yazılan kodların üzerinden geçildi gerekli düzeltmeler yapıldı
* Grup içi görüşme --> durum değerlendirmesi yapıldı ve yapılacaklar gözden geçirildi

# # 08.11.2021

* Hoca tarafından kütüphane kitap ödünç alma sisteminin obje ödünç alma sistemine çevrilmesi talebi geldi

# # 12.11.2021

* Kütüphane sisteminin obje ödünç alma sistemine çevrilmesine başlandı
* Veri tabanında gerekli güncellemeler yapıldı (tablolar, ilişkiler ve bazı fazla bilgiler değiştirildi)

# # 14.11.2021

* Anasayfa menü kısmında login logout kısımları düzenlendi
* Anasayfada kütüphane ile ilgili bilgiler obje yönetim paneli içeriği ile değiştirildi

# # 17.11.2021

* Anasayfada objelerin görüntülenmesi ve ödünç alınabilmeleri modal ve popup yapısı araştırıldı --> henüz tamamlanmadı
* Obje ödünç almış olan kişilerin veri tabanından panel kısmından silinmemesi için uyarı mekanizması eklendi

# # 20.11.2021 - 27.11.2021

* Sınav haftası olduğundan sadece proje durumu gözden geçirildi ve eksikler not alındı

# # 26.11.2021

* Ödünç verme işlemine dahil olmuş görevlilerin veri tabanından silinme işlemi tüm ödünç işlemleri tamamlanana kadar engellendi
* Kullanıcı paneline "My Items" bölümü eklendi. Kullanıcı ödünç aldığı objelerin bilgisini, teslim tarihini görüntüleyebiliyor

# # 29.11.2021

* Ödünç verilen objelerin görevli tarafından teslim tarihinin 7 günlük periyotlar ile uzatılabilme özelliği eklendi

# # 30.11.2021

* Proje içerisindeki dosyalar düzenlendi, sayfaların resimleri, css ve js kodları content ve images klasörü altında toplandı
* Anasayfada objelerin ödünç alınabilmesi için buton eklendi ve gerekli kod kısmı yazıldı
* alert ile ödünç alma işleminin doğrulanması sağlandı

# # 02.12.2021

* Projemizin müşterisi olan Tuba hocamız ile görüşme düzenlendi. Proje hakkında sunum gerçekleştirildi ve hocamızın talepleri, önerileri dinlendi ve not alındı.
* Hocamızın talepleri:
    - Kullanıcı giriş yaptıktan sonra objeyi direkt ödünç alamasın. İstediği tarih aralığını belirterek rezerve etsin. Ödünç verme işlemi kullanıcı objeyi teslim
      almaya geldiğinde görevli tarafından sisteme girilsin.
    - Anasayfada tüm objeler tek sayfada olmasın kategorilere ayrılsın
    - Objelerin durumu giriş yapan kullanıcılar tarafından görülebilsin. Kimin tarafından hangi tarihler arasında rezerve edilmiş, ödünç alınmış ve ne zaman teslim
      edecek gibisinden.
    - Kullanıcı istediği objeyi arama çubuğu ile aratarak bulabilsin
    - Objeler sadece modal yapısı olarak değil aynı zamanda tablo halinde görüntülenebilsin

# # 03.12.2021

* İstek doğrultusunda veri tabanına rezerve edilen objelerin tutulabilmesi için yeni tablo eklendi, gerekli ilişkiler kuruldu ve objeler için rezerve işleminin takip edilebilmesi için obje tablosuna statü özelliği eklendi

# # 04.12.2021

* Anasayfada objeler için modal yapısı entegre edildi. Kullanıcı giriş yaptıktan sonra objelerin detaylı bilgisini ve durumunu görüntüleyebiliyor. Eğer obje mevcut ise ve kimse tarafından rezerve edilmemişse girilen tarihler arasında rezerve edilebiliyor. Rezerve edilen obje görevli kişinin sistemine düşüyor.
* Rezerve işlemi için gerekli kodlamalar yapıldı

# # 06.12.2021

* Kullanıcı paneline rezerve edilmiş ve ödünç alınmış objeler bölümü eklendi. Rezerve edilen objeyi kullanıcı isterse geri bırakabiliyor veya sistem görevlisi işlemi onaylamayarak objeyi serbest bırakabiliyor.
* Rezerve işlemi için kontrol mekanizması eklendi. Geçersiz tarih aralıkları için rezerve işlemi gerçekleştirilemiyor.
* Admin panelinde rezerve edilen objelerin ödünç verilebilmesi için arayüz tasarlandı, tablo eklendi ve gerekli kodlamalar yapıldı.

# # 07.12.2021

* Rezerve edilen ve ödünç alınan objelerin bilgileri modal yapısı içerisine dahil edildi ve görüntüleri ayarlandı.

# # 09.12.2021

* Anasayfada objelerin görüntüsünün değiştirilebilimesi için buton eklendi. Artık objeleri sadece resimli değil aynı zamanda tablo halinde görüntüleyebiliyoruz.
* Gerekli kodlamalar anasayfa içerisine dahil edildi

# # 12.12.2021

* Objeler kategorilere ayrıldı. Menüye kategori kısmı eklendi ve istenen kategori seçilerek istenen biçimde gösterilebiliyor (Modal veya tablo halinde).
* Anasayfa ikonu eklendi kullanıcı üstüne tıklayarak kategoriler bölümünden anasayfaya geçiş yapabiliyor. Tasarımlar düzenlendi.

# # 15.12.2021

* Site içinde bazı sayfalara yetkisi olmayanlar için erişim kısıtlaması getirildi
* Admin için ayrı login sayfası oluşturuldu

# # 17.12.2021

* Tuba hocamız taleplerinin yazdığı onay formunu imzalayıp tarafımıza teslim etti

# # 18.12.2021

* Anasayfada bulunan contact form için admin paneline yeni bölüm eklendi. Böylelikle gelen mesaj formlarını görevli görüntüleyebilecek ve silebilecek.
* Veri tabanında EMPLOYEE_TABLE silinerek yerine iki farklı admin tipi olan ADMIN_TABLE eklendi ve ilişkileri düzenlendi. Admin tipleri olarak biri görevli(ödünç verme yetkisine sahip), diğeri ise normal tüm yetkinliklere sahip admin(ek olarak ayarlar bölümüne erişerek yeni görevli veya admin ekleyebiliyor)

# # 20.12.2021

* Admin paneline settings bölümü altında yeni admin, görevli ekleme ve güncelleme kısmı eklendi sadece Admin yetkisine sahip kişiler erişebiliyor.
* LogIn ve Register işlemlerine uyarı sistemi eklendi.(LogIn için "şifre veya kullanıcı ismi hatalıdır." || Register için "kullanıcı ismi mevcuttur başka bir isim seçiniz." "Email mevcuttur başka bir email seçiniz." gibi)

# # 22.12.2021

* Hatalı sayfa erişimlerinde 404 aradığınız sayfa mevcut değildir uyarısı ayarlandı. 
* Erişim yetkisi olmadığı halde mevcut bir sayfaya erişmeye çalışan kişiler için kimlik doğrulama yönlendirmesi ayarlandı. Yani yetkisi yok ise login sayfasına yönlendirilecek.

# # 24.12.2021

* Rezervasyon işleminde tarih seçme işlemi güncellendi. Başlangıç tarihi minimum "Bugün", bitiş tarihi minimum 1 gün sonra seçilebilir.
* My profile kısmında mevcut olmayan kişilere mesaj gönderme işleminde uyarı eklendi, hata giderildi mesaj gönderme ve mesajlarım bölümüne erişim açıldı 

# # 26.12.2021

* Admin panelinde bulunan tabloları excel formatına çevirme özelliği eklendi
* Anasayfa için max. gösterilecek obje sayısı ayarlandı, obje, üye, ve ödünç alınan objeler için arama çubuğu eklendi ve güncellendi

# # 28.12.2021

* Ödünç alınan objeler ve rezerve edilen objelere de arama çubuğu eklendi, pagedlist ayarları yapıldı

# # 30.12.2021

* Kodlara yorum eklendi, açıklamalar eklendi

# # 31.12.2021

* Projemizin müşterisi Tuba Hocamız ile görüşme yapıldı, istediği talep ve özellikler uygulamalı olarak gösterilerek projenin son hali hakkında bilgi verildi

# # 02.12.2021

* Projenin özet dokümantasyonu ve kullanım kılavuzu hazırlandı son haline getirildi
* Grup içi son değerlendirmeler yapıldı ve görüşüldü

# # 06.01.2022

* Projenin tanıtım sunumu yapılarak Önder Tombuş hocamıza teslim edilecek
