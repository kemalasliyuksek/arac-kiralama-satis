-- Şubeler
INSERT INTO Subeler (SubeAdi, Adres, UlkeKodu, TelefonNo, Email, SehirPlaka, AktifMi)
VALUES
('Bursa Merkez Şube', 'Atatürk Caddesi No:12, Osmangazi, Bursa', '+90', '5161161616', 'bursa@arackiralamasatis.com', '16', TRUE),
('İstanbul Beşiktaş Şube', 'Barbaros Bulvarı No:45, Beşiktaş, İstanbul', '+90', '5343343434', 'istanbul@arackiralamasatis.com', '34', TRUE),
('İzmir Alsancak Şube', 'Kıbrıs Şehitleri Caddesi No:22, Konak, İzmir', '+90', '5353353535', 'izmir@arackiralamasatis.com', '35', TRUE);


-- Roller
INSERT INTO Roller (RolAdi, Aciklama) VALUES
('Yönetici', 'Sistemde tam yetkiye sahip kullanıcı. Kullanıcıları, araçları, şubeleri ve personelleri yönetebilir.'),
('Şube Müdürü', 'Kendi şubesindeki işlemleri ve personeli yönetebilir.'),
('Personel', 'Araç işlemleri, rezervasyon ve kiralama süreçlerini yürütür.'),
('Müşteri', 'Araçları kiralayabilen veya satın alabilen sistem kullanıcısı.'),
('Teknisyen', 'Servisteki araçların durumu ile ilgilenir, teknik kayıtları günceller.'),
('Bakım Personeli', 'Araçların kiralama ve satış öncesi/sonrası bakımını (temizlik, genel kontrol vb.) yapar.'),
('Misafir', 'Sistemi sınırlı olarak görüntüleyebilen kullanıcı, hesap oluşturabilir.');


-- Araç Durumları
INSERT INTO AracDurumlari (DurumAdi, Aciklama) VALUES
('Müsait', 'Araç kiralama veya satış için uygundur.'),
('Satılık', 'Araç satışa sunulmuştur.'),
('Satıldı', 'Araç satılmıştır, tekrar kullanılamaz.'),
('Kiralık', 'Araç kiralama için uygundur.'),
('Kirada', 'Araç şu anda bir müşteride kiradadır.'),
('Rezerv Edildi', 'Araç bir müşteri tarafından rezerve edilmiştir.'),
('Rezervasyon Süresi Doldu', 'Rezervasyon yapılmış ancak teslim alınmamıştır.'),
('Serviste', 'Araç bakım veya onarım için servistedir.'),
('Arızalı', 'Araçta arıza var, kullanılamaz durumda.'),
('Bakımda', 'Araç temizleniyor, kısa süreliğine kullanılamaz.'),
('Transferde', 'Araç bir şubeden diğerine transfer ediliyor.'),
('İade Bekleniyor', 'Kiralanan araç henüz teslim edilmedi.');


-- Araçlar
INSERT INTO Araclar (
    Plaka, Marka, Model, YapimYili, MotorNo, SasiNo, Renk, Kilometre,
    YakitTipi, VitesTipi, DurumID, AlinmaTarihi, AlisFiyati, SatisFiyati,
    SubeID, AracSinifID
) VALUES
-- Ekonomik Sınıf (AracSinifID = 1)
('34ABC001', 'Fiat', 'Egea', 2022, 'MTR1001', 'SASI1001', 'Beyaz', 12000, 'Dizel', 'Manuel', 1, '2022-04-01', 200000.00, NULL, 1, 1),
('16DEF002', 'Renault', 'Clio', 2021, 'MTR1002', 'SASI1002', 'Gri', 18000, 'Benzin', 'Otomatik', 1, '2021-08-15', 185000.00, NULL, 2, 1),
('35GHI003', 'Hyundai', 'i20', 2023, 'MTR1003', 'SASI1003', 'Mavi', 8000, 'Benzin', 'Manuel', 1, '2023-01-10', 210000.00, NULL, 3, 1),
-- Orta Sınıf (AracSinifID = 2)
('34JKL004', 'Toyota', 'Corolla', 2022, 'MTR1004', 'SASI1004', 'Siyah', 14000, 'Hibrit', 'Otomatik', 1, '2022-06-05', 275000.00, NULL, 1, 2),
('16MNO005', 'Ford', 'Focus', 2021, 'MTR1005', 'SASI1005', 'Kırmızı', 20000, 'Benzin', 'Manuel', 1, '2021-10-20', 240000.00, NULL, 2, 2),
('35PQR006', 'Peugeot', '301', 2023, 'MTR1006', 'SASI1006', 'Gümüş', 6000, 'Dizel', 'Otomatik', 1, '2023-02-18', 260000.00, NULL, 3, 2),
('34STU007', 'Honda', 'Civic', 2022, 'MTR1007', 'SASI1007', 'Lacivert', 10000, 'Benzin', 'Otomatik', 1, '2022-03-12', 280000.00, NULL, 1, 2),
-- Lüks Sınıf (AracSinifID = 3)
('34VWX008', 'BMW', '320i', 2023, 'MTR1008', 'SASI1008', 'Siyah', 5000, 'Benzin', 'Otomatik', 1, '2023-06-01', 900000.00, NULL, 1, 3),
('16YZA009', 'Mercedes', 'C200', 2022, 'MTR1009', 'SASI1009', 'Beyaz', 9000, 'Dizel', 'Otomatik', 1, '2022-09-10', 950000.00, NULL, 2, 3),
('35BCD010', 'Audi', 'A4', 2023, 'MTR1010', 'SASI1010', 'Gri', 7000, 'Benzin', 'Otomatik', 1, '2023-04-25', 920000.00, NULL, 3, 3);


-- Araç Sınıfları
INSERT INTO AracSiniflari (SinifAdi, Aciklama, OrtalamaKiraFiyati) VALUES
('Ekonomik', 'Uygun fiyatlı, az yakan küçük araçlar.', 1000.00),
('Orta', 'Konforlu, geniş aile araçları.', 1500.00),
('Lüks', 'Yüksek donanımlı, premium sınıf araçlar.', 2500.00);


-- Kira Fiyatları
-- Ekonomik
INSERT INTO KiraFiyatlari (AracSinifID, KiralamaTipi, Fiyat) VALUES
(1, 'Saatlik', 100.00),
(1, 'Günlük', 1000.00),
(1, 'Haftalık', 5000.00),
(1, 'Aylık', 20000.00);
-- Orta
INSERT INTO KiraFiyatlari (AracSinifID, KiralamaTipi, Fiyat) VALUES
(2, 'Saatlik', 150.00),
(2, 'Günlük', 1500.00),
(2, 'Haftalık', 7500.00),
(2, 'Aylık', 30000.00);
-- Lüks
INSERT INTO KiraFiyatlari (AracSinifID, KiralamaTipi, Fiyat) VALUES
(3, 'Saatlik', 250.00),
(3, 'Günlük', 2500.00),
(3, 'Haftalık', 12500.00),
(3, 'Aylık', 50000.00);


-- Müşteriler
INSERT INTO Musteriler (
    Ad, Soyad, TC, DogumTarihi, EhliyetNo, EhliyetSinifi, EhliyetTarihi,
    UlkeKodu, TelefonNo, Email, Adres, MusaitlikDurumu, MusteriTipi
) VALUES
('Ahmet', 'Yılmaz', '12345678901', '1990-05-15', 'EHL001', 'B', '2010-07-20', '+90', '5321112233', 'ahmet.yilmaz@example.com', 'Beşiktaş, İstanbul', TRUE, 'Bireysel'),
('Ayşe', 'Demir', '23456789012', '1988-03-10', 'EHL002', 'B', '2009-06-18', '+90', '5332223344', 'ayse.demir@example.com', 'Osmangazi, Bursa', TRUE, 'Bireysel'),
('Mehmet', 'Kaya', '34567890123', '1995-11-22', 'EHL003', 'B', '2014-09-10', '+90', '5343334455', 'mehmet.kaya@example.com', 'Karşıyaka, İzmir', TRUE, 'Bireysel'),
('Elif', 'Çetin', '45678901234', '1992-07-05', 'EHL004', 'B', '2011-05-12', '+90', '5354445566', 'elif.cetin@example.com', 'Çankaya, Ankara', TRUE, 'Bireysel'),
('Burak', 'Aydın', '56789012345', '1998-01-30', 'EHL005', 'B', '2017-03-22', '+90', '5365556677', 'burak.aydin@example.com', 'Nilüfer, Bursa', TRUE, 'Bireysel'),
('Zeynep', 'Koç', '67890123456', '1993-09-17', 'EHL006', 'B', '2012-10-15', '+90', '5376667788', 'zeynep.koc@example.com', 'Konak, İzmir', TRUE, 'Bireysel'),
('Emre', 'Yıldız', '78901234567', '1985-12-03', 'EHL007', 'B', '2006-01-05', '+90', '5387778899', 'emre.yildiz@example.com', 'Kadıköy, İstanbul', TRUE, 'Kurumsal'),
('Fatma', 'Arslan', '89012345678', '1991-06-09', 'EHL008', 'B', '2010-08-30', '+90', '5398889900', 'fatma.arslan@example.com', 'Bornova, İzmir', TRUE, 'Bireysel'),
('Salih', 'Taş', '90123456789', '1996-02-14', 'EHL009', 'B', '2015-04-25', '+90', '5309990011', 'salih.tas@example.com', 'Yıldırım, Bursa', TRUE, 'Kurumsal'),
('Melis', 'Aksoy', '01234567890', '1994-08-27', 'EHL010', 'B', '2013-11-11', '+90', '5310001122', 'melis.aksoy@example.com', 'Ataşehir, İstanbul', TRUE, 'Bireysel');


-- Kullanıcılar
INSERT INTO Kullanicilar (
    Ad, Soyad, KullaniciAdi, Sifre, Email, UlkeKodu, TelefonNo, RolID, SubeID, SonGirisTarihi, Durum
)VALUES
-- Yönetici
('Kemal', 'Aslıyüksek', 'kasliyuksek', '123456', 'admin@arackiralamasatis.com', '+90', '5301112233', 1, NULL, NOW(), TRUE),
-- Şube Müdürleri
('Nur', 'Kumbasar', 'nkumbasar', '123456', 'nur.kumbasar@arackiralamasatis.com', '+90', '5312223344', 2, 3, NOW(), TRUE),
('Salih', 'Zenginal', 'szenginal', '123456', 'salih.zenginal@arackiralamasatis.com', '+90', '5323334455', 2, 1, NOW(), TRUE),
('Ensar', 'Bayraktar', 'ebayraktar', '123456', 'ensar.bayraktar@arackiralamasatis.com', '+90', '5334445566', 2, 2, NOW(), TRUE),
-- Genel Personel
('Ali', 'Aslıyüksek', 'aasliyuksek', '123456', 'ali.asliyuksek@arackiralamasatis.com', '+90', '5345556677', 3, 1, NOW(), TRUE),
('Ayşe', 'Sönmez', 'asonmez', '123456', 'ayse.sonmez@arackiralamasatis.com', '+90', '5356667788', 3, 2, NOW(), TRUE),
('Murat', 'Bucan', 'mbucan', '123456', 'murat.bucan@arackiralamasatis.com', '+90', '5356667788', 3, 2, NOW(), TRUE),
-- Teknisyen
('Taha', 'Uluşahin', 'tulusahin', '123456', 'taha.ulusahin@arackiralamasatis.com', '+90', '5389990011', 5, 2, NOW(), TRUE),
-- Bakım Personeli
('Fatih', 'Turan', 'fturan', '123456', 'fatih.turan@arackiralamasatis.com', '+90', '5367778899', 6, 1, NOW(), TRUE),
('Gül', 'Koç', 'gkoc', '123456', 'gul.koc@arackiralamasatis.com', '+90', '5378889900', 6, 3, NOW(), TRUE);
