CREATE DATABASE SeyahatRehberimBlog_DB
GO
USE SeyahatRehberimBlog_DB
GO
CREATE TABLE YoneticiTurleri
(
    ID int IDENTITY(1,1),
    Isim nvarchar(50),
    CONSTRAINT pk_yoneticiTur PRIMARY KEY(ID)
)
GO
INSERT INTO YoneticiTurleri(Isim) VALUES('Admin')
INSERT INTO YoneticiTurleri(Isim) VALUES('Moderatör')
GO
CREATE TABLE Yoneticiler
(
    ID int IDENTITY(1,1),
    YoneticiTur_ID int,
    Isim nvarchar(75),
    Soyisim nvarchar(75),
    KullaniciAdi nvarchar(20),
    Email nvarchar(170),
    Sifre nvarchar(20),
    Durum bit,
    CONSTRAINT pk_Yonetici PRIMARY KEY(ID),
    CONSTRAINT fk_YoneticiYoneticiTur FOREIGN KEY(YoneticiTur_ID) REFERENCES YoneticiTurleri(ID)
)
GO
INSERT INTO Yoneticiler (YoneticiTur_ID, Isim, Soyisim, KullaniciAdi, Email, Sifre, Durum)
VALUES(1, 'Serap', 'Aydýn', 'SeyahatRehberi', 'serapaydn461@gmail.com', '1234', 1)
GO
CREATE TABLE Uyeler
(
    ID int IDENTITY(1,1),
    Isim nvarchar(75),
    Soyisim nvarchar(75),
    KullaniciAdi nvarchar(20),
    Email nvarchar(170),
    Sifre nvarchar(20),
    UyelikTarihi datetime,
    Durum bit,
    CONSTRAINT pk_uye PRIMARY KEY(ID)
)
GO
INSERT INTO Uyeler (Isim, Soyisim, KullaniciAdi, Email, Sifre, UyelikTarihi, Durum)
VALUES('Uras Ege', 'Aydýn', 'urasege1', 'urasege11@gmail.com', '1234', '2024-03-27', 1)
GO
CREATE TABLE Kategoriler
(
    ID int IDENTITY(1,1),
    Isim nvarchar(50) NOT NULL,
    Aciklama nvarchar(500),
    Durum bit,
    CONSTRAINT pk_Kategori PRIMARY KEY(ID)
)
GO
CREATE TABLE Makaleler
(
    ID int IDENTITY(1,1),
    Kategori_ID int,
    Yazar_ID int,
    Baslik nvarchar(150),
    Ozet nvarchar(500),
    Icerik ntext,
    KapakResim nvarchar(50),
    Tarih datetime,
    GoruntulemeSayi int,
    Konum nvarchar(100),
    Durum bit,
    CONSTRAINT pk_Makale PRIMARY KEY(ID),
    CONSTRAINT fk_MakaleKategori FOREIGN KEY(Kategori_ID) REFERENCES Kategoriler(ID),  
	CONSTRAINT fk_MakaleYonetici FOREIGN KEY(Yazar_ID) REFERENCES Yoneticiler(ID) 
)
GO
CREATE TABLE Yorumlar
(
    ID int IDENTITY(1,1),
    Makale_ID int,
    Uye_ID int,
    Icerik nvarchar(500),
    TarihveSaat datetime,
    Durum bit,
    UyeIsim nvarchar(75),
    CONSTRAINT pk_Yorum PRIMARY KEY(ID),
    CONSTRAINT fk_YorumMakale FOREIGN KEY(Makale_ID) REFERENCES Makaleler(ID),
    CONSTRAINT fk_YorumUye FOREIGN KEY(Uye_ID) REFERENCES Uyeler(ID)
)