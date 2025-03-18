# MicroX

MicroX, .NET Core tabanlı bir mikro servis mimarisi uygulamasıdır. Yüksek performans, esneklik ve ölçeklenebilirlik sağlamak için modern teknolojiler ve mimari desenler kullanılmıştır.

## Kullanılan Teknolojiler

- **.NET Core Microservices** - Mikro servis mimarisi
- **Ocelot** - API Gateway yönetimi
- **CQRS & MediatR** - Komut ve sorgu ayrımı
- **Redis Cluster & Sentinel** - Önbellekleme ve yüksek erişilebilirlik
- **RabbitMQ & Kafka** - Mesaj kuyruk yönetimi
- **AutoMapper** - Nesne dönüşümü
- **Entity Framework Core** - Veritabanı işlemleri

## Kurulum

1. **Depoyu klonlayın:**
   ```sh
   git clone https://github.com/supereffect/MicroX.git
   cd MicroX
   ```
2. **Gerekli bağımlılıkları yükleyin:**
   ```sh
   dotnet restore
   ```
3. **Docker kullanarak servisi başlatın:**
   ```sh
   docker-compose up -d
   ```

## Kullanım

- Servisleri ayağa kaldırdıktan sonra API Gateway üzerinden erişim sağlayabilirsiniz.
- CQRS ve MediatR yapısı sayesinde sorgu ve komutlar ayrımıyla yönetilir.
- RabbitMQ ve Kafka kullanarak mesajlaşma sistemini entegre edebilirsiniz.
- Redis Cluster ile önbellekleme ve Sentinel ile hata toleransı sağlanabilir.

## Katkıda Bulunma

1. Depoyu forklayın.
2. Yeni bir branch oluşturun: `git checkout -b yeni-ozellik`
3. Değişikliklerinizi yapın ve commit atın: `git commit -m 'Yeni özellik eklendi'`
4. Değişikliklerinizi push edin: `git push origin yeni-ozellik`
5. Bir Pull Request oluşturun.

## Lisans

Bu proje MIT Lisansı altında lisanslanmıştır. Daha fazla bilgi için [LICENSE](LICENSE) dosyasına bakın.

---
Daha fazla bilgi için lütfen [MicroX](https://github.com/supereffect/MicroX) GitHub deposunu ziyaret edin.



![if you see this cat, you are most luckiest person in this world](https://m.media-amazon.com/images/I/71+KyPYHyrL.jpg)
