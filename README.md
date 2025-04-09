# ApiHavaDurumuProjesi ☁️☀️

Bu proje, .NET 6 üzerinde C# ile geliştirilmiş, **ASP.NET Core Web API** kullanarak basit bir şehir hava durumu bilgisi yönetim sistemidir. Veritabanı işlemleri için **Entity Framework Core (Code First)** yaklaşımı ve **SQL Server** kullanılmıştır.

## 🚀 Genel Bakış

API, şehirlerin adını, ülkesini, sıcaklık değerini ve kısa bir açıklamasını depolamak ve yönetmek için temel CRUD (Create, Read, Update, Delete) operasyonlarını sunar. Ayrıca, bazı temel istatistiksel sorgular için de endpoint'ler içerir. API'nin test edilmesi ve belgelendirilmesi için **Swagger (OpenAPI)** entegrasyonu mevcuttur.

## ✨ Özellikler ve Endpoints

*   **EF Core Code First:** Veritabanı şeması, `Entities/City.cs` sınıfı ve `Context/WeatherContext.cs` üzerinden kod tabanlı olarak oluşturulur.
*   **EF Core Migrations:** Veritabanı şema değişikliklerini yönetmek için kullanılır (`Migrations` klasörü).
*   **Swagger Entegrasyonu:** API endpoint'lerinin otomatik olarak belgelendirilmesi ve test edilmesi için Swagger UI mevcuttur.
*   **Temel CRUD Operasyonları (`/api/Weathers`):**
    *   `GET /`: Tüm şehirlerin listesini döner.
    *   `POST /`: Yeni bir şehir ekler (Request body'den `City` nesnesi alır).
    *   `DELETE ?id={id}`: Belirtilen ID'ye sahip şehri siler. *(Not: Daha standart bir RESTful yaklaşım `DELETE /{id}` şeklinde olurdu)*.
    *   `PUT /`: Mevcut bir şehri günceller (Request body'den `City` nesnesi alır, `CityId` içermelidir). *(Not: Daha standart bir RESTful yaklaşım `PUT /{id}` şeklinde olurdu)*.
*   **Spesifik Sorgu Endpoints (`/api/Weathers/...`):**
    *   `GET /GetByIdWeatherCity?id={id}`: Belirtilen ID'ye sahip şehri getirir.
    *   `GET /TotalCityCount`: Veritabanındaki toplam şehir sayısını döner (`Count()`).
    *   `GET /MaxTempCityName`: En yüksek sıcaklığa sahip şehrin adını döner (`OrderByDescending()`, `FirstOrDefault()`).
    *   `GET /MinTempCityName`: En düşük sıcaklığa sahip şehrin adını döner (`OrderBy()`, `FirstOrDefault()`).

## 🛠️ Kullanılan Teknolojiler

*   **Programlama Dili:** C#
*   **Framework:** .NET 6
*   **Platform:** ASP.NET Core Web API
*   **Veri Erişimi:** Entity Framework Core 6 (Code First)
*   **Veritabanı:** Microsoft SQL Server
*   **API Dokümantasyonu:** Swashbuckle.AspNetCore (Swagger)

## 💾 Kurulum ve Çalıştırma

Bu proje EF Core Code First kullandığından, veritabanı ilk migration uygulandığında otomatik olarak oluşturulur.

1.  **Gereksinimler:**
    *   .NET 6 SDK
    *   Visual Studio 2022 veya uyumlu bir IDE
    *   Microsoft SQL Server (Express, Developer veya başka bir sürüm) - Uygulamanın bağlanabileceği bir instance çalışıyor olmalı.

2.  **Projeyi Klonlama:**
    ```bash
    git clone https://github.com/kullanici-adiniz/ApiHavaDurumuProjesi.git
    ```
    *(kullanici-adiniz kısmını kendi GitHub kullanıcı adınızla değiştirin)*

3.  **Bağlantı Dizesini Ayarlama:**
    *   `ApiHavaDurumuProjesi` projesindeki `Context/WeatherContext.cs` dosyasını açın.
    *   `OnConfiguring` metodu içindeki bağlantı dizesini bulun:
        ```csharp
        optionsBuilder.UseSqlServer("Server=UMUT\\SQLEXPRESS;initial catalog=ApiHavaDurumuDb;integrated Security=true");
        ```
    *   `Server=UMUT\SQLEXPRESS` kısmını kendi SQL Server sunucu adınızla değiştirin (örn: `.` , `(localdb)\mssqllocaldb`, `YOUR_PC_NAME\SQLEXPRESS` vb.).
    *   `initial catalog=ApiHavaDurumuDb` kısmındaki veritabanı adını isterseniz değiştirebilirsiniz. Bu veritabanı SQL Server'da mevcut olmasa bile EF Core tarafından oluşturulacaktır.
    *   Eğer SQL Server'ınız Windows Authentication (integrated security=true) kullanmıyorsa, bağlantı dizesini SQL Server Authentication'a göre düzenlemeniz gerekir (User ID=...;Password=...;).

4.  **Veritabanını Oluşturma (EF Core Migrations):**
    *   Visual Studio'da `ApiHavaDurumuProjesi.sln` dosyasını açın.
    *   NuGet Paket Yöneticisi Konsolu'nu açın (`Tools -> NuGet Package Manager -> Package Manager Console`).
    *   Açılan konsolda `Default project` olarak `ApiHavaDurumuProjesi` projesinin seçili olduğundan emin olun.
    *   Aşağıdaki komutu çalıştırarak veritabanını oluşturun ve mevcut migration'ları uygulayın:
        ```powershell
        Update-Database
        ```
    *   Bu komut, `WeatherContext.cs`'deki bağlantı dizesini kullanarak SQL Server'a bağlanacak, `ApiHavaDurumuDb` veritabanını (eğer yoksa) oluşturacak ve `Migrations` klasöründeki tanımlara göre `Cities` tablosunu yaratacaktır.

5.  **Uygulamayı Çalıştırma:**
    *   Projeyi Visual Studio üzerinden başlatın (F5 veya Debug -> Start Debugging).
    *   Uygulama başladığında tarayıcınız otomatik olarak Swagger UI arayüzünü (`/swagger`) açacaktır. Buradan API endpoint'lerini test edebilirsiniz.
    *   Alternatif olarak `dotnet run` komutunu projenin kök dizininde çalıştırabilirsiniz.

## 📝 API Kullanımı

API'yi test etmek için projenin sunduğu Swagger UI arayüzünü (`/swagger` adresinden erişilebilir) veya Postman, Insomnia gibi araçları kullanabilirsiniz.

*   **Listeleme:** `GET /api/Weathers`
*   **Ekleme:** `POST /api/Weathers` (Request Body'de JSON formatında City nesnesi gönderin)
    ```json
    {
      "cityName": "İstanbul",
      "country": "Türkiye",
      "temp": 15.5,
      "detail": "Parçalı Bulutlu"
    }
    ```
*   **Silme:** `DELETE /api/Weathers?id=1` (ID'si 1 olan şehri siler)
*   **Güncelleme:** `PUT /api/Weathers` (Request Body'de güncellenmiş City nesnesini ID'si ile birlikte gönderin)
    ```json
    {
      "cityId": 1,
      "cityName": "İstanbul",
      "country": "Türkiye",
      "temp": 16.0,
      "detail": "Güneşli"
    }
    ```
