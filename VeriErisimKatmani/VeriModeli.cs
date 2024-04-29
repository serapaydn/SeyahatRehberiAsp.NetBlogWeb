using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace VeriErisimKatmani
{
    public class VeriModeli
    {

        SqlConnection baglanti; SqlCommand komut;
        public VeriModeli()
        {
            baglanti = new SqlConnection(BaglantiYollari.AnaBaglantiYolu);
            komut = baglanti.CreateCommand();
        }

        #region Yönetici Metotları
        public int YoneticiSayisiGetir()
        {
            SqlCommand komut = new SqlCommand();

            try
            {
                komut.CommandText = "SELECT COUNT(*) FROM Yoneticiler";
                komut.Parameters.Clear();
                komut.Connection = baglanti;

                baglanti.Open();
                return (int)komut.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
            finally
            {
                if (baglanti.State != ConnectionState.Closed)
                    baglanti.Close();
            }
        }
        public int MakaleSayisiGetir()
        {
            SqlCommand komut = new SqlCommand();

            try
            {
                komut.CommandText = "SELECT COUNT(*) FROM Makaleler";
                komut.Parameters.Clear();
                komut.Connection = baglanti;

                baglanti.Open();
                return (int)komut.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public int UyeSayisiGetir()
        {
            SqlCommand komut = new SqlCommand();

            try
            {
                komut.CommandText = "SELECT COUNT(*) FROM Uyeler";
                komut.Parameters.Clear();
                komut.Connection = baglanti;

                baglanti.Open();
                return (int)komut.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public int YorumSayisiGetir()
        {
            SqlCommand komut = new SqlCommand();

            try
            {
                komut.CommandText = "SELECT COUNT(*) FROM Yorumlar";
                komut.Parameters.Clear();
                komut.Connection = baglanti;

                baglanti.Open();
                return (int)komut.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public List<Yonetici> TumYoneticileriGetir()
        {
            List<Yonetici> yoneticiler = new List<Yonetici>();

            try
            {
                komut.CommandText = "SELECT ID, YoneticiTur_ID, Isim, Soyisim, KullaniciAdi, Email, Sifre, Durum FROM Yoneticiler";
                komut.Parameters.Clear();
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Yonetici yonetici = new Yonetici();
                    yonetici.ID = okuyucu.GetInt32(0);
                    yonetici.YoneticiTur_ID = okuyucu.GetInt32(1);
                    yonetici.Isim = okuyucu.GetString(2);
                    yonetici.Soyisim = okuyucu.GetString(3);
                    yonetici.KullaniciAdi = okuyucu.GetString(4);
                    yonetici.Email = okuyucu.GetString(5);
                    yonetici.Sifre = okuyucu.GetString(6);
                    yonetici.Durum = okuyucu.GetBoolean(7);
                    yoneticiler.Add(yonetici);
                }
                return yoneticiler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public void YoneticiSil(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM Yoneticiler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void YoneticiDurumDegistir(int id)
        {
            try
            {
                komut.CommandText = "SELECT Durum FROM Yoneticiler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                bool durum = Convert.ToBoolean(komut.ExecuteScalar());

                komut.CommandText = "UPDATE Yoneticiler SET Durum=@durum WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                komut.Parameters.AddWithValue("@durum", !durum);
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }
        public Yonetici YoneticiGiris(string kullaniciAdi, string sifre)
        {
            try
            {
                Yonetici y = new Yonetici();
                komut.CommandText = "SELECT Y.ID, Y.YoneticiTur_ID, YT.Isim, Y.Isim, Y.Soyisim,Y.KullaniciAdi,Y.Email, Y.Sifre, Y.Durum FROM Yoneticiler AS Y JOIN YoneticiTurleri AS YT ON Y.YoneticiTur_ID = YT.ID WHERE Y.KullaniciAdi = @kadi AND Y.Sifre = @sifre";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@kadi", kullaniciAdi);
                komut.Parameters.AddWithValue("@sifre", sifre);

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();

                while (okuyucu.Read())
                {
                    y.ID = okuyucu.GetInt32(0);
                    y.YoneticiTur_ID = okuyucu.GetInt32(1);
                    y.YoneticiTur = okuyucu.GetString(2);
                    y.Isim = okuyucu.GetString(3);
                    y.Soyisim = okuyucu.GetString(4);
                    y.KullaniciAdi = okuyucu.GetString(5);
                    y.Email = okuyucu.GetString(6);
                    y.Sifre = okuyucu.GetString(7);
                    y.Durum = okuyucu.GetBoolean(8);
                }
                return y;
            }
            catch
            {
                return null;
            }

        }
        public bool YoneticiEkle(Yonetici yonetici)
        {
            try
            {
                komut.CommandText = "INSERT INTO Yoneticiler(YoneticiTur_ID, Isim, Soyisim, KullaniciAdi, Email, Sifre, Durum) VALUES(@yoneticiTurID, @isim, @soyisim, @kullaniciAdi, @email, @sifre, @durum)";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@yoneticiTurID", yonetici.YoneticiTur_ID);
                komut.Parameters.AddWithValue("@isim", yonetici.Isim);
                komut.Parameters.AddWithValue("@soyisim", yonetici.Soyisim);
                komut.Parameters.AddWithValue("@kullaniciAdi", yonetici.KullaniciAdi);
                komut.Parameters.AddWithValue("@email", yonetici.Email);
                komut.Parameters.AddWithValue("@sifre", yonetici.Sifre);
                komut.Parameters.AddWithValue("@durum", yonetici.Durum);
                baglanti.Open();
                komut.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public Yonetici YoneticiBilgisiGetir(int yoneticiID)
        {
            try
            {
                komut.CommandText = "SELECT ID, Isim, Soyisim, KullaniciAdi, Email, Durum, Sifre FROM Yoneticiler WHERE ID = @yoneticiID";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@yoneticiID", yoneticiID);

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                Yonetici yonetici = null;
                if (okuyucu.Read())
                {
                    yonetici = new Yonetici();
                    yonetici.ID = okuyucu.GetInt32(0);
                    yonetici.Isim = okuyucu.GetString(1);
                    yonetici.Soyisim = okuyucu.GetString(2);
                    yonetici.KullaniciAdi = okuyucu.GetString(3);
                    yonetici.Email = okuyucu.GetString(4);
                    yonetici.Durum = okuyucu.GetBoolean(5);
                    yonetici.Sifre = okuyucu.GetString(6);
                }
                return yonetici;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public bool YoneticiProfilGuncelle(int yoneticiID, string isim, string soyisim, string kullaniciAdi, string email, string yeniSifre)
        {
            try
            {
                Yonetici yonetici = YoneticiBilgisiGetir(yoneticiID);
                if (yonetici == null)
                    return false;

                komut.CommandText = "UPDATE Yoneticiler SET Isim = @isim, Soyisim = @soyisim, KullaniciAdi = @kullaniciAdi, Email = @email, Sifre = @yeniSifre WHERE ID = @yoneticiID";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@isim", isim);
                komut.Parameters.AddWithValue("@soyisim", soyisim);
                komut.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                komut.Parameters.AddWithValue("@email", email);
                komut.Parameters.AddWithValue("@yeniSifre", yeniSifre);
                komut.Parameters.AddWithValue("@yoneticiID", yoneticiID);

                baglanti.Open();
                int etkilenenSatirSayisi = komut.ExecuteNonQuery();

                if (etkilenenSatirSayisi == 0)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }


        #endregion

        #region Kategori Metodları
        public bool KategoriEkle(Kategori kat)
        {
            try
            {
                komut.CommandText = "INSERT INTO Kategoriler(Isim, Aciklama, Durum) VALUES(@isim,@aciklama,@durum)";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@isim", kat.Isim);
                komut.Parameters.AddWithValue("@aciklama", kat.Aciklama);
                komut.Parameters.AddWithValue("@durum", kat.Durum);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public List<Kategori> TumKategorileriGetir()
        {
            List<Kategori> kategoriler = new List<Kategori>();

            try
            {
                komut.CommandText = "SELECT ID, Isim, Aciklama, Durum FROM Kategoriler";
                komut.Parameters.Clear();
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Kategori kat = new Kategori();
                    kat.ID = okuyucu.GetInt32(0);
                    kat.Isim = okuyucu.GetString(1);
                    kat.Aciklama = okuyucu.GetString(2);
                    kat.Durum = okuyucu.GetBoolean(3);
                    kategoriler.Add(kat);
                }
                return kategoriler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public List<Kategori> TumKategorileriGetir(bool durum)
        {
            List<Kategori> kategoriler = new List<Kategori>();

            try
            {
                komut.CommandText = "SELECT ID, Isim, Aciklama, Durum FROM Kategoriler WHERE Durum=@d";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@d", durum);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Kategori kat = new Kategori();
                    kat.ID = okuyucu.GetInt32(0);
                    kat.Isim = okuyucu.GetString(1);
                    kat.Aciklama = okuyucu.GetString(2);
                    kat.Durum = okuyucu.GetBoolean(3);
                    kategoriler.Add(kat);
                }
                return kategoriler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public Kategori KategoriGetir(int id)
        {
            try
            {
                komut.CommandText = "SELECT ID, Isim, Aciklama, Durum FROM Kategoriler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                Kategori kat = new Kategori();
                while (okuyucu.Read())
                {
                    kat.ID = okuyucu.GetInt32(0);
                    kat.Isim = okuyucu.GetString(1);
                    kat.Aciklama = okuyucu.GetString(2);
                    kat.Durum = okuyucu.GetBoolean(3);
                }
                return kat;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool KategoriDuzenle(Kategori kat)
        {
            try
            {
                komut.CommandText = "UPDATE Kategoriler SET Isim=@isim, Aciklama=@aciklama, Durum=@durum WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", kat.ID);
                komut.Parameters.AddWithValue("@isim", kat.Isim);
                komut.Parameters.AddWithValue("@aciklama", kat.Aciklama);
                komut.Parameters.AddWithValue("@durum", kat.Durum);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void KategoriSil(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM Kategoriler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void KategoriDurumDegistir(int id)
        {
            try
            {
                komut.CommandText = "SELECT Durum FROM Kategoriler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                bool durum = Convert.ToBoolean(komut.ExecuteScalar());

                komut.CommandText = "UPDATE Kategoriler SET Durum=@durum WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                komut.Parameters.AddWithValue("@durum", !durum);
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }
        #endregion

        #region Makale Metotları
      
        public List<Makale> MakaleAra(string anahtarKelime)
        {
            List<Makale> bulunanMakaleler = new List<Makale>();

            try
            {
                komut.CommandText = "SELECT ID, Kategori_ID, Yazar_ID, Baslik, Ozet, Icerik, KapakResim, Tarih, GoruntulemeSayi, Konum, Durum " +
                                     "FROM Makaleler " +
                                     "WHERE Baslik LIKE @AnahtarKelime OR Konum LIKE @AnahtarKelime " +
                                     "ORDER BY CASE WHEN Baslik LIKE @AnahtarKelime THEN 1 ELSE 2 END, Tarih DESC";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@AnahtarKelime", "%" + anahtarKelime + "%");

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();

                while (okuyucu.Read())
                {
                    Makale makale = new Makale();
                    makale.ID = okuyucu.GetInt32(0);
                    makale.Kategori_ID = okuyucu.GetInt32(1);
                    makale.Yazar_ID = okuyucu.GetInt32(2);
                    makale.Baslik = okuyucu.GetString(3);
                    makale.Ozet = okuyucu.GetString(4);
                    makale.Icerik = okuyucu.GetString(5);
                    makale.KapakResim = okuyucu.GetString(6);
                    makale.Tarih = okuyucu.GetDateTime(7);
                    makale.GoruntulemeSayi = okuyucu.GetInt32(8);
                    makale.Konum = okuyucu.GetString(9);
                    makale.Durum = okuyucu.GetBoolean(10);

                    bulunanMakaleler.Add(makale);
                }
            }
            finally
            {
                baglanti.Close();
            }

            return bulunanMakaleler;
        }
        public bool MakaleEkle(Makale mak)
        {
            try
            {
                komut.CommandText = "INSERT INTO Makaleler(Kategori_ID, Yazar_ID, Baslik, Ozet, Icerik, KapakResim, Tarih, GoruntulemeSayi, Konum, Durum) VALUES(@kategori_ID, @yazar_ID, @baslik, @ozet, @icerik, @kapakResim, @tarih, @goruntulemeSayi, @konum, @durum)";
                komut.Parameters.AddWithValue("@kategori_ID", mak.Kategori_ID);
                komut.Parameters.AddWithValue("@yazar_ID", mak.Yazar_ID);
                komut.Parameters.AddWithValue("@baslik", mak.Baslik);
                komut.Parameters.AddWithValue("@ozet", mak.Ozet);
                komut.Parameters.AddWithValue("@icerik", mak.Icerik);
                komut.Parameters.AddWithValue("@kapakResim", mak.KapakResim);
                komut.Parameters.AddWithValue("@tarih", mak.Tarih);
                komut.Parameters.AddWithValue("@goruntulemeSayi", mak.GoruntulemeSayi);
                komut.Parameters.AddWithValue("@konum", mak.Konum);
                komut.Parameters.AddWithValue("@durum", mak.Durum);

                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public List<Makale> MakaleListele()
        {
            try
            {
                List<Makale> makaleler = new List<Makale>();
                komut.CommandText = "SELECT M.ID, M.Kategori_ID, K.Isim, M.Yazar_ID, Y.KullaniciAdi, M.Baslik, M.Ozet, M.Icerik, M.KapakResim, M.Tarih, M.GoruntulemeSayi, M.Konum, M.Durum FROM Makaleler AS M JOIN Kategoriler AS K ON M.Kategori_ID = K.ID JOIN Yoneticiler AS Y ON M.Yazar_ID = Y.ID ORDER BY M.Tarih DESC";
                komut.Parameters.Clear();
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Makale mak = new Makale();
                    mak.ID = okuyucu.GetInt32(0);
                    mak.Kategori_ID = okuyucu.GetInt32(1);
                    mak.Kategori = okuyucu.GetString(2);
                    mak.Yazar_ID = okuyucu.GetInt32(3);
                    mak.Yazar = okuyucu.GetString(4);
                    mak.Baslik = okuyucu.GetString(5);
                    mak.Ozet = okuyucu.GetString(6);
                    mak.Icerik = okuyucu.GetString(7);
                    mak.KapakResim = okuyucu.GetString(8);
                    mak.Tarih = okuyucu.GetDateTime(9);
                    mak.TarihStr = mak.Tarih.ToShortDateString();
                    mak.GoruntulemeSayi = okuyucu.GetInt32(10);
                    mak.Konum = okuyucu.GetString(11);
                    mak.Durum = okuyucu.GetBoolean(12);
                    makaleler.Add(mak);
                }
                return makaleler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public List<Makale> MakaleListele(bool durum)
        {
            try
            {
                List<Makale> makaleler = new List<Makale>();
                komut.CommandText = "SELECT M.ID, M.Kategori_ID, K.Isim, M.Yazar_ID, Y.KullaniciAdi, M.Baslik, M.Ozet, M.Icerik, M.KapakResim, M.Tarih, M.GoruntulemeSayi, M.Konum, M.Durum FROM Makaleler AS M JOIN Kategoriler AS K ON M.Kategori_ID = K.ID JOIN Yoneticiler AS Y ON M.Yazar_ID = Y.ID WHERE M.Durum = @d";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@d", durum);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Makale mak = new Makale();
                    mak.ID = okuyucu.GetInt32(0);
                    mak.Kategori_ID = okuyucu.GetInt32(1);
                    mak.Kategori = okuyucu.GetString(2);
                    mak.Yazar_ID = okuyucu.GetInt32(3);
                    mak.Yazar = okuyucu.GetString(4);
                    mak.Baslik = okuyucu.GetString(5);
                    mak.Ozet = okuyucu.GetString(6);
                    mak.Icerik = okuyucu.GetString(7);
                    mak.KapakResim = okuyucu.GetString(8);
                    mak.Tarih = okuyucu.GetDateTime(9);
                    mak.TarihStr = mak.Tarih.ToShortDateString();
                    mak.GoruntulemeSayi = okuyucu.GetInt32(10);
                    mak.Konum = okuyucu.GetString(11);
                    mak.Durum = okuyucu.GetBoolean(12);

                    makaleler.Add(mak);
                }
                return makaleler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public List<Makale> MakaleListele(int KategoriID)
        {
            try
            {
                List<Makale> makaleler = new List<Makale>();
                komut.CommandText = "SELECT M.ID, M.Kategori_ID, K.Isim, M.Yazar_ID, Y.KullaniciAdi, M.Baslik, M.Ozet, M.Icerik, M.KapakResim, M.Tarih, M.GoruntulemeSayi, M.Konum, M.Durum FROM Makaleler AS M JOIN Kategoriler AS K ON M.Kategori_ID = K.ID JOIN Yoneticiler AS Y ON M.Yazar_ID = Y.ID WHERE M.Durum = 1 AND M.Kategori_ID = @kid";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@kid", KategoriID);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Makale mak = new Makale();
                    mak.ID = okuyucu.GetInt32(0);
                    mak.Kategori_ID = okuyucu.GetInt32(1);
                    mak.Kategori = okuyucu.GetString(2);
                    mak.Yazar_ID = okuyucu.GetInt32(3);
                    mak.Yazar = okuyucu.GetString(4);
                    mak.Baslik = okuyucu.GetString(5);
                    mak.Ozet = okuyucu.GetString(6);
                    mak.Icerik = okuyucu.GetString(7);
                    mak.KapakResim = okuyucu.GetString(8);
                    mak.Tarih = okuyucu.GetDateTime(9);
                    mak.TarihStr = mak.Tarih.ToShortDateString();
                    mak.GoruntulemeSayi = okuyucu.GetInt32(10);
                    mak.Konum = okuyucu.GetString(11);
                    mak.Durum = okuyucu.GetBoolean(12);

                    makaleler.Add(mak);
                }
                return makaleler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public Makale MakaleGetir(int id)
        {
            try
            {
                komut.CommandText = "SELECT M.ID, M.Kategori_ID, K.Isim, M.Yazar_ID, Y.KullaniciAdi, M.Baslik, M.Ozet, M.Icerik, M.KapakResim, M.Tarih, M.GoruntulemeSayi, M.Konum, M.Durum FROM Makaleler AS M JOIN Kategoriler AS K ON M.Kategori_ID = K.ID JOIN Yoneticiler AS Y ON M.Yazar_ID = Y.ID WHERE M.ID = @id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                Makale mak = new Makale();
                while (okuyucu.Read())
                {
                    mak.ID = okuyucu.GetInt32(0);
                    mak.Kategori_ID = okuyucu.GetInt32(1);
                    mak.Kategori = okuyucu.GetString(2);
                    mak.Yazar_ID = okuyucu.GetInt32(3);
                    mak.Yazar = okuyucu.GetString(4);
                    mak.Baslik = okuyucu.GetString(5);
                    mak.Ozet = okuyucu.GetString(6);
                    mak.Icerik = okuyucu.GetString(7);
                    mak.KapakResim = okuyucu.GetString(8);
                    mak.Tarih = okuyucu.GetDateTime(9);
                    mak.GoruntulemeSayi = okuyucu.GetInt32(10);
                    mak.Konum = okuyucu.GetString(11);
                    mak.Durum = okuyucu.GetBoolean(12);

                }
                return mak;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public List<Makale> TumMakaleleriGetir()
        {
            List<Makale> makaleler = new List<Makale>();

            try
            {
                komut.CommandText = "SELECT ID, Kategori_ID, Kategori, Yazar_ID, Yazar, Baslik, Ozet, Icerik, KapakResim, Tarih, TarihStr, GoruntulemeSayi, Konum, Durum FROM Makaleler";
                komut.Parameters.Clear();
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Makale mak = new Makale();
                    mak.ID = okuyucu.GetInt32(0);
                    mak.Kategori_ID = okuyucu.GetInt32(1);
                    mak.Kategori = okuyucu.GetString(2);
                    mak.Yazar_ID = okuyucu.GetInt32(3);
                    mak.Yazar = okuyucu.GetString(4);
                    mak.Baslik = okuyucu.GetString(5);
                    mak.Ozet = okuyucu.GetString(6);
                    mak.Icerik = okuyucu.GetString(7);
                    mak.KapakResim = okuyucu.GetString(8);
                    mak.Tarih = okuyucu.GetDateTime(9);
                    mak.TarihStr = okuyucu.GetString(10);
                    mak.GoruntulemeSayi = okuyucu.GetInt32(11);
                    mak.Konum = okuyucu.GetString(12);
                    mak.Durum = okuyucu.GetBoolean(13);

                    makaleler.Add(mak);
                }
                return makaleler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public bool MakaleDuzenle(Makale mak)
        {
            try
            {
                komut.CommandText = "UPDATE Makaleler SET Kategori_ID = @kategori_ID, Baslik = @baslik, Ozet = @ozet, Icerik = @icerik, KapakResim = @kapakresim,Konum = @konum, Durum = @durum  WHERE ID = @id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", mak.ID);
                komut.Parameters.AddWithValue("@kategori_ID", mak.Kategori_ID);
                komut.Parameters.AddWithValue("@baslik", mak.Baslik);
                komut.Parameters.AddWithValue("@ozet", mak.Ozet);
                komut.Parameters.AddWithValue("@icerik", mak.Icerik);
                komut.Parameters.AddWithValue("@kapakResim", mak.KapakResim);
                komut.Parameters.AddWithValue("@konum", mak.Konum);
                komut.Parameters.AddWithValue("@durum", mak.Durum);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public void MakaleSil(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM Makaleler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void MakaleDurumDegistir(int id)
        {
            try
            {
                komut.CommandText = "SELECT Durum FROM Makaleler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                bool durum = Convert.ToBoolean(komut.ExecuteScalar());

                komut.CommandText = "UPDATE Makaleler SET Durum=@durum WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                komut.Parameters.AddWithValue("@durum", !durum);
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }
        #endregion

        #region Uye Metotları
        public List<Uye> TumUyeleriGetir()
        {
            List<Uye> uyeler = new List<Uye>();

            try
            {
                komut.CommandText = "SELECT ID, Isim,Soyisim,KullaniciAdi,Email,UyelikTarihi, Durum FROM Uyeler";
                komut.Parameters.Clear();
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Uye u = new Uye();
                    u.ID = okuyucu.GetInt32(0);
                    u.Isim = okuyucu.GetString(1);
                    u.Soyisim = okuyucu.GetString(2);
                    u.KullaniciAdi = okuyucu.GetString(3);
                    u.Email = okuyucu.GetString(4);
                    u.UyelikTarihi = okuyucu.GetDateTime(5);
                    u.Durum = okuyucu.GetBoolean(6);
                    uyeler.Add(u);
                }
                return uyeler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public Uye UyeGetir(int id)
        {
            try
            {
                komut.CommandText = "SELECT ID, Isim,Soyisim,KullaniciAdi,Email,UyelikTarihi, Durum FROM Uyeler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                Uye u = new Uye();
                while (okuyucu.Read())
                {
                    u.ID = okuyucu.GetInt32(0);
                    u.Isim = okuyucu.GetString(1);
                    u.Soyisim = okuyucu.GetString(2);
                    u.KullaniciAdi = okuyucu.GetString(3);
                    u.Email = okuyucu.GetString(4);
                    u.UyelikTarihi = okuyucu.GetDateTime(5);
                    u.Durum = okuyucu.GetBoolean(6);
                }
                return u;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public Uye UyeGetir(string kullaniciAdi)
        {
            try
            {
                komut.CommandText = "SELECT ID, Isim, Soyisim, Email, UyelikTarihi, Durum FROM Uyeler WHERE KullaniciAdi = @kullaniciAdi";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                Uye u = null;
                while (okuyucu.Read())
                {
                    u = new Uye();
                    u.ID = okuyucu.GetInt32(0);
                    u.Isim = okuyucu.GetString(1);
                    u.Soyisim = okuyucu.GetString(2);
                    u.KullaniciAdi = kullaniciAdi;
                    u.Email = okuyucu.GetString(3);
                    u.UyelikTarihi = okuyucu.GetDateTime(4);
                    u.Durum = okuyucu.GetBoolean(5);
                }
                return u;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool UyeDuzenle(Uye u)
        {
            try
            {
                komut.CommandText = "UPDATE Uyeler SET Isim=@isim,Soyisim=@soyisim,KullaniciAdi=@kadi,Email=@email,UyelikTarihi=@uyet, Durum=@durum WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", u.ID);
                komut.Parameters.AddWithValue("@isim", u.Isim);
                komut.Parameters.AddWithValue("@Soyisim", u.Soyisim);
                komut.Parameters.AddWithValue("@kadi", u.KullaniciAdi);
                komut.Parameters.AddWithValue("@email", u.Email);
                komut.Parameters.AddWithValue("@uyet", u.UyelikTarihi);
                komut.Parameters.AddWithValue("@durum", u.Durum);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void UyeSil(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM Uyeler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void UyeDurumDegistir(int id)
        {
            try
            {
                komut.CommandText = "SELECT Durum FROM Uyeler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                bool durum = Convert.ToBoolean(komut.ExecuteScalar());

                komut.CommandText = "UPDATE Uyeler SET Durum=@durum WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                komut.Parameters.AddWithValue("@durum", !durum);
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }
        public Uye UyeGiris(string email, string sifre)
        {
            try
            {
                Uye u = new Uye();
                komut.CommandText = "SELECT * FROM Uyeler WHERE Email = @e AND Sifre = @s";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@e", email);
                komut.Parameters.AddWithValue("@s", sifre);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    u.ID = okuyucu.GetInt32(0);
                    u.Isim = okuyucu.GetString(1);
                    u.Soyisim = okuyucu.GetString(2);
                    u.KullaniciAdi = okuyucu.GetString(3);
                    u.Email = okuyucu.GetString(4);
                    u.Sifre = okuyucu.GetString(5);
                    u.UyelikTarihi = okuyucu.GetDateTime(6);
                    u.Durum = okuyucu.GetBoolean(7);
                }
                return u;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public bool UyeOl(Uye u)
        {
            try
            {
                komut.CommandText = "INSERT INTO Uyeler(Isim, Soyisim, KullaniciAdi, Email, Sifre, UyelikTarihi, Durum) VALUES(@isim, @soyisim, @kadi, @email, @sifre, @uyetarih, @durum)";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@isim", u.Isim);
                komut.Parameters.AddWithValue("@soyisim", u.Soyisim);
                komut.Parameters.AddWithValue("@kadi", u.KullaniciAdi);
                komut.Parameters.AddWithValue("@email", u.Email);
                komut.Parameters.AddWithValue("@sifre", u.Sifre);
                komut.Parameters.AddWithValue("@uyetarih", u.UyelikTarihi);
                komut.Parameters.AddWithValue("@durum", u.Durum);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public Uye UyeBilgisiGetir(int uyeID)
        {
            try
            {
                komut.CommandText = "SELECT ID, Isim, Soyisim, KullaniciAdi, Email, UyelikTarihi, Durum, Sifre FROM Uyeler WHERE ID = @uyeID";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@uyeID", uyeID);

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                Uye uye = null;
                if (okuyucu.Read())
                {
                    uye = new Uye();
                    uye.ID = okuyucu.GetInt32(0);
                    uye.Isim = okuyucu.GetString(1);
                    uye.Soyisim = okuyucu.GetString(2);
                    uye.KullaniciAdi = okuyucu.GetString(3);
                    uye.Email = okuyucu.GetString(4);
                    uye.UyelikTarihi = okuyucu.GetDateTime(5);
                    uye.Durum = okuyucu.GetBoolean(6);
                    uye.Sifre = okuyucu.GetString(7);
                }
                return uye;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public bool UyeSifreGuncelle(int uyeID, string yeniSifre)
        {
            try
            {
                Uye uye = UyeBilgisiGetir(uyeID);
                if (uye == null)
                    return false;

                komut.CommandText = "UPDATE Uyeler SET Sifre = @yeniSifre WHERE ID = @uyeID AND Sifre = @eskiSifre";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@yeniSifre", yeniSifre);
                komut.Parameters.AddWithValue("@uyeID", uyeID);
                komut.Parameters.AddWithValue("@eskiSifre", uye.Sifre);

                baglanti.Open();
                int etkilenenSatirSayisi = komut.ExecuteNonQuery();

                if (etkilenenSatirSayisi == 0)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public Uye UyeMailGetir(string email)
        {
            try
            {
                komut.CommandText = "SELECT ID, Isim, Soyisim, KullaniciAdi, UyelikTarihi, Durum FROM Uyeler WHERE Email = @email";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@email", email);

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                Uye uye = null;
                if (okuyucu.Read())
                {
                    uye = new Uye();
                    uye.ID = okuyucu.GetInt32(0);
                    uye.Isim = okuyucu.GetString(1);
                    uye.Soyisim = okuyucu.GetString(2);
                    uye.KullaniciAdi = okuyucu.GetString(3);
                    uye.Email = email;
                    uye.UyelikTarihi = okuyucu.GetDateTime(4);
                    uye.Durum = okuyucu.GetBoolean(5);
                }
                return uye;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool UyeProfilGuncelle(int uyeID, string isim, string soyisim, string kullaniciAdi, string email, string yeniSifre)
        {
            try
            {
                Uye uye = UyeBilgisiGetir(uyeID);
                if (uye == null)
                    return false;

                komut.CommandText = "UPDATE Uyeler SET Isim = @isim, Soyisim = @soyisim, KullaniciAdi = @kullaniciAdi, Email = @email, Sifre = @yeniSifre WHERE ID = @uyeID";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@isim", isim);
                komut.Parameters.AddWithValue("@soyisim", soyisim);
                komut.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                komut.Parameters.AddWithValue("@email", email);
                komut.Parameters.AddWithValue("@yeniSifre", yeniSifre);
                komut.Parameters.AddWithValue("@uyeID", uyeID);

                baglanti.Open();
                int etkilenenSatirSayisi = komut.ExecuteNonQuery();

                if (etkilenenSatirSayisi == 0)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }
        #endregion

        #region Yorum Metotları
        public bool YorumEkle(Yorum yorum)
        {
            try
            {
                komut.CommandText = "INSERT INTO Yorumlar (Makale_ID, Uye_ID, Icerik, TarihveSaat, Durum) VALUES (@makaleID, @uyeID, @icerik, @tarihveSaat, @durum)";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@makaleID", yorum.Makale_ID);
                komut.Parameters.AddWithValue("@uyeID", yorum.Uye_ID);
                komut.Parameters.AddWithValue("@icerik", yorum.Icerik);
                komut.Parameters.AddWithValue("@tarihveSaat", yorum.TarihveSaat);
                komut.Parameters.AddWithValue("@durum", yorum.Durum);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public List<Yorum> TumYorumlariGetir()
        {
            List<Yorum> yorumlar = new List<Yorum>();

            try
            {
                komut.CommandText = "SELECT ID, Makale_ID, Uye_ID, Icerik, TarihveSaat, Durum FROM Yorumlar";
                komut.Parameters.Clear();
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Yorum yorum = new Yorum();
                    yorum.ID = okuyucu.GetInt32(0);
                    yorum.Makale_ID = okuyucu.GetInt32(1);
                    yorum.Uye_ID = okuyucu.GetInt32(2);
                    yorum.Icerik = okuyucu.GetString(3);
                    yorum.TarihveSaat = okuyucu.GetDateTime(4);
                    yorum.Durum = okuyucu.GetBoolean(5);
                    yorumlar.Add(yorum);
                }
                return yorumlar;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public void YorumSil(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM Yorumlar WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void YorumDurumDegistir(int id)
        {
            try
            {
                komut.CommandText = "SELECT Durum FROM Yorumlar WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                bool durum = Convert.ToBoolean(komut.ExecuteScalar());

                komut.CommandText = "UPDATE Yorumlar SET Durum=@durum WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                komut.Parameters.AddWithValue("@durum", !durum);
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }
        public bool YorumDuzenle(Yorum yorum)
        {
            try
            {
                komut.CommandText = "UPDATE Yorumlar SET Makale_ID=@makaleID, Uye_ID=@uyeID, Icerik=@icerik, TarihveSaat=@tarihveSaat, Durum=@durum WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", yorum.ID);
                komut.Parameters.AddWithValue("@makaleID", yorum.Makale_ID);
                komut.Parameters.AddWithValue("@uyeID", yorum.Uye_ID);
                komut.Parameters.AddWithValue("@icerik", yorum.Icerik);
                komut.Parameters.AddWithValue("@tarihveSaat", yorum.TarihveSaat);
                komut.Parameters.AddWithValue("@durum", yorum.Durum);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public List<Yorum> YorumlariGetir(int makaleID)
        {
            List<Yorum> yorumlar = new List<Yorum>();
            try
            {
                komut.CommandText = "SELECT y.ID, y.Makale_ID, y.Uye_ID, y.Icerik, y.TarihveSaat, y.Durum, u.Isim, u.Soyisim FROM Yorumlar y INNER JOIN Uyeler u ON y.Uye_ID = u.ID WHERE y.Makale_ID = @makaleID";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@makaleID", makaleID);

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Yorum yorum = new Yorum();
                    yorum.ID = okuyucu.GetInt32(0);
                    yorum.Makale_ID = okuyucu.GetInt32(1);
                    yorum.Uye_ID = okuyucu.GetInt32(2);
                    yorum.Icerik = okuyucu.GetString(3);
                    yorum.TarihveSaat = okuyucu.GetDateTime(4);
                    yorum.Durum = okuyucu.GetBoolean(5);
                    yorum.UyeIsim = okuyucu.GetString(6) + " " + okuyucu.GetString(7);
                    yorumlar.Add(yorum);
                }
                return yorumlar;
            }
            catch
            {
                return yorumlar;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public List<Yorum> UyeninYorumlariniGetir(int uyeID)
        {
            List<Yorum> uyeninYorumlari = new List<Yorum>();

            try
            {
                komut.CommandText = "SELECT ID, Makale_ID, Uye_ID, Icerik, TarihveSaat, Durum FROM Yorumlar WHERE Uye_ID = @UyeID";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@UyeID", uyeID);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Yorum yorum = new Yorum();
                    yorum.ID = okuyucu.GetInt32(0);
                    yorum.Makale_ID = okuyucu.GetInt32(1);
                    yorum.Uye_ID = okuyucu.GetInt32(2);
                    yorum.Icerik = okuyucu.GetString(3);
                    yorum.TarihveSaat = okuyucu.GetDateTime(4);
                    yorum.Durum = okuyucu.GetBoolean(5);
                    uyeninYorumlari.Add(yorum);
                }
                return uyeninYorumlari;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        #endregion



    }


}


