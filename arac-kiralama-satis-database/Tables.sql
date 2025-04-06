-- Şubeler
CREATE TABLE Subeler (
    SubeID INT AUTO_INCREMENT PRIMARY KEY,
    SubeAdi VARCHAR(100) NOT NULL,
    Adres VARCHAR(255) NOT NULL,
    UlkeKodu VARCHAR(4) NOT NULL DEFAULT '+90',
    TelefonNo VARCHAR(10) NOT NULL,
    Email VARCHAR(100),
    SehirPlaka VARCHAR(2) NOT NULL,
    AktifMi BOOLEAN DEFAULT TRUE,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Roller
CREATE TABLE Roller (
    RolID INT AUTO_INCREMENT PRIMARY KEY,
    RolAdi VARCHAR(50) NOT NULL,
    Aciklama VARCHAR(255),
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Araç Durumları
CREATE TABLE AracDurumlari (
    DurumID INT AUTO_INCREMENT PRIMARY KEY,
    DurumAdi VARCHAR(50) NOT NULL DEFAULT "Müsait",
    Aciklama VARCHAR(255),
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Araç Sınıfları
CREATE TABLE AracSiniflari (
    AracSinifID INT AUTO_INCREMENT PRIMARY KEY,
    SinifAdi VARCHAR(30) NOT NULL UNIQUE,
    Aciklama VARCHAR(255),
    OrtalamaKiraFiyati DECIMAL(10, 2),
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Kira Fiyatları
CREATE TABLE KiraFiyatlari (
    KiraFiyatID INT AUTO_INCREMENT PRIMARY KEY,
    AracSinifID INT NOT NULL,
    KiralamaTipi ENUM('Saatlik', 'Günlük', 'Haftalık', 'Aylık') NOT NULL,
    Fiyat DECIMAL(10, 2) NOT NULL,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (AracSinifID) REFERENCES AracSiniflari(AracSinifID)
);

-- Araçlar
CREATE TABLE Araclar (
    AracID INT AUTO_INCREMENT PRIMARY KEY,
    Plaka VARCHAR(15) NOT NULL UNIQUE,
    Marka VARCHAR(50) NOT NULL,
    Model VARCHAR(50) NOT NULL,
    YapimYili INT NOT NULL,
    MotorNo VARCHAR(50),
    SasiNo VARCHAR(50) NOT NULL UNIQUE,
    Renk VARCHAR(30),
    Kilometre INT NOT NULL DEFAULT 0,
    YakitTipi VARCHAR(20) NOT NULL,
    VitesTipi VARCHAR(20) NOT NULL,
    DurumID INT NOT NULL,
    AlinmaTarihi DATE,
    AlisFiyati DECIMAL(12, 2),
    SatisFiyati DECIMAL(12, 2),
    SubeID INT,
    AracSinifID INT,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    GuncellenmeTarihi DATETIME ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (DurumID) REFERENCES AracDurumlari(DurumID),
    FOREIGN KEY (SubeID) REFERENCES Subeler(SubeID),
    FOREIGN KEY (AracSinifID) REFERENCES AracSiniflari(AracSinifID)
);

-- Müşteriler
CREATE TABLE Musteriler (
    MusteriID INT AUTO_INCREMENT PRIMARY KEY,
    Ad VARCHAR(50) NOT NULL,
    Soyad VARCHAR(50) NOT NULL,
    TC VARCHAR(11) UNIQUE,
    DogumTarihi DATE,
    EhliyetNo VARCHAR(20),
    EhliyetSinifi VARCHAR(10),
    EhliyetTarihi DATE,
    UlkeKodu VARCHAR(4) NOT NULL DEFAULT '+90',
    TelefonNo VARCHAR(10) NOT NULL,
    Email VARCHAR(100),
    Adres VARCHAR(255),
    KayitTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    MusaitlikDurumu BOOLEAN DEFAULT TRUE,
    MusteriTipi VARCHAR(20) DEFAULT 'Bireysel',
    GuncellenmeTarihi DATETIME ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_tc (TC),
    INDEX idx_telefon (TelefonNo)
);

-- Kullanıcılar
CREATE TABLE Kullanicilar (
    KullaniciID INT AUTO_INCREMENT PRIMARY KEY,
    Ad VARCHAR(50) NOT NULL,
    Soyad VARCHAR(50) NOT NULL,
    KullaniciAdi VARCHAR(50) NOT NULL UNIQUE,
    Sifre VARCHAR(255) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    UlkeKodu VARCHAR(4) NOT NULL DEFAULT '+90',
    TelefonNo VARCHAR(10) NOT NULL,
    RolID INT NOT NULL,
    SubeID INT,
    SonGirisTarihi DATETIME,
    Durum BOOLEAN DEFAULT TRUE,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    GuncellenmeTarihi DATETIME ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (RolID) REFERENCES Roller(RolID),
    FOREIGN KEY (SubeID) REFERENCES Subeler(SubeID)
);

-- Sözleşmeler
CREATE TABLE Sozlesmeler (
    SozlesmeID INT AUTO_INCREMENT PRIMARY KEY,
    SozlesmeTipi VARCHAR(20) NOT NULL,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    SozlesmeMetni TEXT,
    SozlesmeDosyaYolu VARCHAR(255)
);

-- Kiralamalar
CREATE TABLE Kiralamalar (
    KiralamaID INT AUTO_INCREMENT PRIMARY KEY,
    MusteriID INT NOT NULL,
    AracID INT NOT NULL,
    BaslangicTarihi DATETIME NOT NULL,
    BitisTarihi DATETIME NOT NULL,
    TeslimTarihi DATETIME,
    BaslangicKm INT NOT NULL,
    BitisKm INT,
    KiralamaTutari DECIMAL(12, 2) NOT NULL,
    DepozitTutari DECIMAL(12, 2),
    OdemeTipi VARCHAR(20) NOT NULL,
    KiralamaNotuID INT,
    SozlesmeID INT,
    KullaniciID INT NOT NULL,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    GuncellenmeTarihi DATETIME ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (MusteriID) REFERENCES Musteriler(MusteriID),
    FOREIGN KEY (AracID) REFERENCES Araclar(AracID),
    FOREIGN KEY (SozlesmeID) REFERENCES Sozlesmeler(SozlesmeID),
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID),
    INDEX idx_tarih (BaslangicTarihi, BitisTarihi)
);

-- Kiralama Notu
CREATE TABLE KiralamaNotu (
    KiralamaNotuID INT AUTO_INCREMENT PRIMARY KEY,
    KiralamaID INT NOT NULL,
    NotMetni TEXT NOT NULL,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    KullaniciID INT NOT NULL,
    FOREIGN KEY (KiralamaID) REFERENCES Kiralamalar(KiralamaID),
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID)
);

-- Bağımlı FK güncellemesi
ALTER TABLE Kiralamalar
ADD CONSTRAINT fk_kiralamalar_not
FOREIGN KEY (KiralamaNotuID) REFERENCES KiralamaNotu(KiralamaNotuID);

-- Satışlar
CREATE TABLE Satislar (
    SatisID INT AUTO_INCREMENT PRIMARY KEY,
    MusteriID INT NOT NULL,
    AracID INT NOT NULL,
    SatisTarihi DATETIME NOT NULL,
    SatisTutari DECIMAL(12, 2) NOT NULL,
    OdemeTipi VARCHAR(20) NOT NULL,
    TaksitSayisi INT DEFAULT 0,
    NoterTarihi DATE,
    SozlesmeID INT,
    KullaniciID INT NOT NULL,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    GuncellenmeTarihi DATETIME ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (MusteriID) REFERENCES Musteriler(MusteriID),
    FOREIGN KEY (AracID) REFERENCES Araclar(AracID),
    FOREIGN KEY (SozlesmeID) REFERENCES Sozlesmeler(SozlesmeID),
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID)
);

-- Bankalar
CREATE TABLE Bankalar (
    BankaID INT AUTO_INCREMENT PRIMARY KEY,
    BankaAdi VARCHAR(100) NOT NULL,
    Aciklama VARCHAR(255)
);

-- Ödemeler
CREATE TABLE Odemeler (
    OdemeID INT AUTO_INCREMENT PRIMARY KEY,
    IslemTipi VARCHAR(20) NOT NULL,
    IslemID INT NOT NULL,
    MusteriID INT NOT NULL,
    Tutar DECIMAL(12, 2) NOT NULL,
    OdemeTarihi DATETIME NOT NULL,
    OdemeTipi VARCHAR(20) NOT NULL,
    BankaID INT,
    OdemeNot VARCHAR(255),
    KullaniciID INT NOT NULL,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (MusteriID) REFERENCES Musteriler(MusteriID),
    FOREIGN KEY (BankaID) REFERENCES Bankalar(BankaID),
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID),
    INDEX idx_islem (IslemTipi, IslemID)
);

-- Servisler
CREATE TABLE Servisler (
    ServisID INT AUTO_INCREMENT PRIMARY KEY,
    ServisAdi VARCHAR(100) NOT NULL,
    Adres VARCHAR(255),
    UlkeKodu VARCHAR(4) NOT NULL DEFAULT '+90',
    TelefonNo VARCHAR(10) NOT NULL,
    Email VARCHAR(100),
    YetkiliKisi VARCHAR(100)
);

-- Bakımlar
CREATE TABLE Bakimlar (
    BakimID INT AUTO_INCREMENT PRIMARY KEY,
    AracID INT NOT NULL,
    BaslamaTarihi DATETIME NOT NULL,
    BitisTarihi DATETIME,
    BakimTipi VARCHAR(50) NOT NULL,
    BakimNotu TEXT,
    Maliyet DECIMAL(10, 2) NOT NULL,
    ServisID INT,
    KullaniciID INT NOT NULL,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    GuncellenmeTarihi DATETIME ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (AracID) REFERENCES Araclar(AracID),
    FOREIGN KEY (ServisID) REFERENCES Servisler(ServisID),
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID)
);

-- Belgeler
CREATE TABLE Belgeler (
    BelgeID INT AUTO_INCREMENT PRIMARY KEY,
    IslemTipi VARCHAR(20) NOT NULL,
    IslemID INT NOT NULL,
    BelgeTipi VARCHAR(50) NOT NULL,
    DosyaYolu VARCHAR(255) NOT NULL,
    YuklemeTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    KullaniciID INT NOT NULL,
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID),
    INDEX idx_islem (IslemTipi, IslemID)
);

-- Loglar
CREATE TABLE Loglar (
    LogID INT AUTO_INCREMENT PRIMARY KEY,
    KullaniciID INT,
    IslemTipi VARCHAR(50) NOT NULL,
    IslemDetayi TEXT NOT NULL,
    IPAdresi VARCHAR(50) NOT NULL,
    IslemTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID)
);


-- Hata logları için tablo oluşturma script'i
CREATE TABLE IF NOT EXISTS `HataLoglar` (
  `HataID` VARCHAR(50) NOT NULL PRIMARY KEY COMMENT 'Benzersiz hata tanımlayıcısı',
  `Tarih` DATETIME NOT NULL COMMENT 'Hatanın oluştuğu tarih ve saat',
  `Seviye` VARCHAR(20) NOT NULL COMMENT 'Hata seviyesi (Critical, Error, Warning, Info)',
  `Kaynak` VARCHAR(20) NOT NULL COMMENT 'Hatanın kaynağı (Database, UI, Business, vb.)',
  `Mesaj` TEXT NOT NULL COMMENT 'Hata mesajı',
  `Baglam` TEXT NULL COMMENT 'Hatanın oluştuğu bağlam bilgisi',
  `StackTrace` TEXT NULL COMMENT 'Stack trace bilgisi',
  `IcHata` TEXT NULL COMMENT 'İç hata mesajı (varsa)',
  `CagiranMetod` VARCHAR(255) NULL COMMENT 'Hatanın oluştuğu metod',
  `CagiranDosya` VARCHAR(255) NULL COMMENT 'Hatanın oluştuğu dosya',
  `CagiranSatir` INT NULL COMMENT 'Hatanın oluştuğu satır numarası',
  `KullaniciID` INT NULL COMMENT 'Hatanın oluştuğu anda aktif olan kullanıcı ID',
  `KullaniciAdi` VARCHAR(100) NULL COMMENT 'Hatanın oluştuğu anda aktif olan kullanıcı adı',
  `IPAdresi` VARCHAR(50) NULL COMMENT 'Kullanıcının IP adresi',
  `EkBilgiler` TEXT NULL COMMENT 'JSON formatında ek bilgiler',
  `KayitZamani` TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Loglamanın yapıldığı zaman'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Uygulama hata loglarını saklar';

-- Hatalar üzerinde aramaları hızlandırmak için indeksler
CREATE INDEX `IX_HataLoglar_Tarih` ON `HataLoglar` (`Tarih`);
CREATE INDEX `IX_HataLoglar_Seviye` ON `HataLoglar` (`Seviye`);
CREATE INDEX `IX_HataLoglar_Kaynak` ON `HataLoglar` (`Kaynak`);
CREATE INDEX `IX_HataLoglar_KullaniciID` ON `HataLoglar` (`KullaniciID`);

-- Hata log işlemlerini yönetebilmek için bir temizleme procedürü
DELIMITER //
CREATE PROCEDURE `sp_HataLogTemizle` (
    IN p_gunSayisi INT
)
BEGIN
    -- Belirtilen gün sayısından eski kayıtları sil
    DELETE FROM `HataLoglar` 
    WHERE `Tarih` < DATE_SUB(NOW(), INTERVAL p_gunSayisi DAY);
    
    -- Silinen kayıt sayısını döndür
    SELECT ROW_COUNT() AS 'SilinenKayitSayisi';
END //
DELIMITER ;

-- Kullanım örneği: CALL sp_HataLogTemizle(30); -- 30 günden eski hata kayıtlarını temizler


