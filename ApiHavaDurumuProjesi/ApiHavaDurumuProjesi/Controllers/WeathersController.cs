using ApiHavaDurumuProjesi.Context; // Veritabanı bağlantı sınıfının bulunduğu namespace'i içeri aktarır.
using ApiHavaDurumuProjesi.Entities; // City entity sınıfının bulunduğu namespace'i içeri aktarır.
using Microsoft.AspNetCore.Mvc; // ASP.NET Core MVC kütüphanelerini (ControllerBase, IActionResult, HTTP Attributeları vb.) içeri aktarır.

namespace ApiHavaDurumuProjesi.Controllers // Bu controller sınıfının ait olduğu namespace'i tanımlar.
{
    [Route("api/[controller]")] // Bu controller'a yapılacak isteklerin temel yolunu belirler. "[controller]" kısmına sınıf adı (Weathers) gelir. Yani: /api/Weathers
    [ApiController] // Bu sınıfın bir API controller olduğunu belirtir ve API'ye özgü davranışları (örneğin, otomatik model doğrulama) etkinleştirir.
    public class WeathersController : ControllerBase // Controller sınıfını tanımlar ve API controller'ları için temel işlevsellik sağlayan ControllerBase sınıfından miras alır.
    {
        WeatherContext context = new WeatherContext(); // Veritabanı işlemleri için WeatherContext sınıfından bir nesne oluşturur. (Not: Dependency Injection daha iyi bir pratiktir)

        [HttpGet] // Bu metodun HTTP GET isteklerini karşılayacağını belirtir. (Varsayılan rota: /api/Weathers)
        public IActionResult WeatherCityList() // Tüm şehirleri listeleyen action metodu.
        {
            var values = context.Cities.ToList(); // Veritabanındaki tüm City kayıtlarını çeker ve bir listeye dönüştürür.
            return Ok(values); // HTTP 200 OK durum kodu ile birlikte şehir listesini (values) yanıt olarak döner.
        }

        [HttpPost] // Bu metodun HTTP POST isteklerini karşılayacağını belirtir. (Varsayılan rota: /api/Weathers)
        public IActionResult CreateWeatherCity(City city) // Yeni bir şehir ekleyen action metodu. 'city' verisi isteğin gövdesinden (request body) alınır.
        {
            context.Cities.Add(city); // Gelen 'city' nesnesini veritabanına eklenmek üzere contexte ekler (henüz veritabanına yazılmaz).
            context.SaveChanges(); // Context üzerinde yapılan değişiklikleri (bu durumda ekleme işlemini) veritabanına kaydeder.
            return Ok("Şehir Eklendi"); // HTTP 200 OK durum kodu ile birlikte başarı mesajını yanıt olarak döner.
        }

        [HttpDelete] // Bu metodun HTTP DELETE isteklerini karşılayacağını belirtir. (Not: Genellikle ID route parametresi ile kullanılır: [HttpDelete("{id}")])
        public IActionResult DeleteWeatherCity(int id) // Belirtilen ID'ye sahip şehri silen action metodu. 'id' genellikle query string veya route'dan alınır.
        {
            var value = context.Cities.Find(id); // Verilen 'id' değerine sahip şehri veritabanında bulur (Primary Key'e göre arama yapar).
            // Eğer 'value' null ise şehir bulunamamıştır, bu durumda hata oluşabilir. Kontrol eklemek iyi bir pratiktir.
            context.Cities.Remove(value); // Bulunan 'value' (şehir) nesnesini veritabanından silinmek üzere işaretler.
            context.SaveChanges(); // Context üzerinde yapılan değişiklikleri (silme işlemini) veritabanına kaydeder.
            return Ok("Şehir Silindi"); // HTTP 200 OK durum kodu ile birlikte başarı mesajını yanıt olarak döner.
        }

        [HttpPut] // Bu metodun HTTP PUT isteklerini karşılayacağını belirtir. (Not: Genellikle ID route parametresi ile kullanılır: [HttpPut("{id}")])
        public IActionResult UpdateWeatherCity(City city) // Var olan bir şehri güncelleyen action metodu. 'city' verisi isteğin gövdesinden alınır ve güncellenecek şehrin ID'sini içermelidir.
        {
            var value = context.Cities.Find(city.CityId); // Güncellenecek şehrin ID'si ile veritabanındaki mevcut kaydı bulur.
            // Eğer 'value' null ise şehir bulunamamıştır, bu durumda hata oluşabilir. Kontrol eklemek iyi bir pratiktir.
            value.CityName = city.CityName; // Bulunan kaydın 'CityName' özelliğini gelen 'city' nesnesindeki değerle günceller.
            value.Detail = city.Detail; // Bulunan kaydın 'Detail' özelliğini gelen 'city' nesnesindeki değerle günceller.
            value.Temp = city.Temp; // Bulunan kaydın 'Temp' özelliğini gelen 'city' nesnesindeki değerle günceller.
            value.Country = city.Country; // Bulunan kaydın 'Country' özelliğini gelen 'city' nesnesindeki değerle günceller.
            context.SaveChanges(); // Context üzerinde yapılan değişiklikleri (güncelleme işlemlerini) veritabanına kaydeder.
            return Ok("Şehir Güncellendi"); // HTTP 200 OK durum kodu ile birlikte başarı mesajını yanıt olarak döner.
        }

        [HttpGet("GetByIdWeatherCity")] // Bu metodun HTTP GET isteklerini "/api/Weathers/GetByIdWeatherCity" yoluna karşılayacağını belirtir. ID genellikle query string ile alınır (?id=5).
        public IActionResult GetByIdWeatherCity(int id) // Belirtilen ID'ye sahip şehri getiren action metodu.
        {
            var value = context.Cities.Find(id); // Verilen 'id' değerine sahip şehri veritabanında bulur.
            return Ok(value); // HTTP 200 OK durum kodu ile birlikte bulunan şehir nesnesini (veya bulunamazsa null) yanıt olarak döner.
        }

        [HttpGet("TotalCityCount")] // Bu metodun HTTP GET isteklerini "/api/Weathers/TotalCityCount" yoluna karşılayacağını belirtir.
        public IActionResult TotalCityCount() // Veritabanındaki toplam şehir sayısını getiren action metodu.
        {
            var value = context.Cities.Count(); // Veritabanındaki 'Cities' tablosundaki toplam kayıt sayısını hesaplar.
            return Ok(value); // HTTP 200 OK durum kodu ile birlikte toplam şehir sayısını yanıt olarak döner.
        }

        [HttpGet("MaxTempCityName")] // Bu metodun HTTP GET isteklerini "/api/Weathers/MaxTempCityName" yoluna karşılayacağını belirtir.
        public IActionResult MaxTempCityName() // En yüksek sıcaklığa sahip şehrin adını getiren action metodu.
        {
            var value = context.Cities // Şehirler tablosu üzerinde sorgu başlatır.
                                .OrderByDescending(x => x.Temp) // Şehirleri sıcaklık (Temp) değerine göre azalan sırada (en yüksek önce) sıralar.
                                .Select(y => y.CityName) // Sıralanmış sonuçlardan sadece şehir adlarını (CityName) seçer.
                                .FirstOrDefault(); // Sıralı listedeki ilk şehir adını alır (yani en yüksek sıcaklığa sahip olanı). Eğer tablo boşsa null döner.
            return Ok(value); // HTTP 200 OK durum kodu ile birlikte en yüksek sıcaklığa sahip şehir adını yanıt olarak döner.
        }

        [HttpGet("MinTempCityName")] // Bu metodun HTTP GET isteklerini "/api/Weathers/MinTempCityName" yoluna karşılayacağını belirtir.
        public IActionResult MinTempCityName() // En düşük sıcaklığa sahip şehrin adını getiren action metodu.
        {
            var value = context.Cities // Şehirler tablosu üzerinde sorgu başlatır.
                               .OrderBy(x => x.Temp) // Şehirleri sıcaklık (Temp) değerine göre artan sırada (en düşük önce) sıralar.
                               .Select(y => y.CityName) // Sıralanmış sonuçlardan sadece şehir adlarını (CityName) seçer.
                               .FirstOrDefault(); // Sıralı listedeki ilk şehir adını alır (yani en düşük sıcaklığa sahip olanı). Eğer tablo boşsa null döner.
            return Ok(value); // HTTP 200 OK durum kodu ile birlikte en düşük sıcaklığa sahip şehir adını yanıt olarak döner.
        }
    }
}