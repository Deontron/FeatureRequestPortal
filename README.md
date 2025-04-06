# FeatureRequestPortal

## Proje Hakkında

FeatureRequestPortal, bir özellik talepleri platformudur. Bu platform, kullanıcıların yazılım geliştirme süreçlerinde taleplerini oluşturmasına, oylamasına ve yöneticilerin bu talepleri onaylamasına veya reddetmesine imkan tanır. Kullanıcılar, yazılımın geliştirilmesi ve iyileştirilmesi için fikirlerini sunar, diğer kullanıcılar ise bu fikirleri oylayarak, hangi taleplerin daha fazla önem taşıdığına karar verirler.

Proje, Volosoft Staj Başvurusu kapsamında hazırlanmış ve ABP Framework kullanılarak geliştirilmiştir. ABP Framework, modüler yapısı ile projeyi daha sürdürülebilir ve kolay yönetilebilir hale getirmektedir.

Projede ayrıca EF Core ve PostgreSQL kullanılarak veritabanı işlemleri gerçekleştirilmiş ve React.js ile frontend tarafında kullanıcı etkileşimleri sağlanmıştır.

### Gereksinimler

*.NET SDK 9.0+ - Proje, .NET SDK'sı ile geliştirilmiştir. Projeyi çalıştırabilmek için bu sürüm veya üstü gereklidir.

*Node.js (v18 veya v20) - Frontend kısmında bağımlılıkları yönetmek için Node.js gereklidir.

*PostgreSQL - Veritabanı olarak PostgreSQL kullanılmıştır.


#### Kurulum

*Backend

*Projeyi klonlayın:

git clone <repository-url>
cd <project-directory>

*Backend bağımlılıklarını yükleyin:

dotnet restore
dotnet build
dotnet run

*Frontend

*Frontend bağımlılıklarını yükleyin:

npm install
npm run dev

*Veritabanı Ayarları

appsettings.json dosyasını açın ve PostgreSQL bağlantı dizesini aşağıdaki gibi güncelleyin:

"ConnectionStrings": {
  "Default": "Host=my_host;Database=my_db;Username=my_user;Password=my_password"
}

*Veritabanını oluşturun:

dotnet ef database update

*Uygulamanın Başlatılması:

Backend çalıştırıldıktan sonra, tarayıcıda https://localhost:44390 adresine gidin.

##### Ekran Görüntüleri




