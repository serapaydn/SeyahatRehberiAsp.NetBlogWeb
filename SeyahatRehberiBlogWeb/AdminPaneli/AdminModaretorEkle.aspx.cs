using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb.AdminPaneli
{
    public partial class AdminModaretorEkle : System.Web.UI.Page
    {
        VeriModeli db = new VeriModeli();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtn_ekle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_isim.Text) ||
                string.IsNullOrEmpty(tb_soyisim.Text) ||
                string.IsNullOrEmpty(tb_kullaniciadi.Text) ||
                string.IsNullOrEmpty(tb_email.Text) ||
                string.IsNullOrEmpty(tb_sifre.Text))
            {
                pnl_basarisiz.Visible = true;
                lbl_hatamesaj.Text = "Lütfen tüm alanları doldurunuz!";
            }
            else
            {
                try
                {
                    int yoneticiTurID = Convert.ToInt32(rb_YoneticiTur_ID.SelectedValue);
                    string isim = tb_isim.Text;
                    string soyisim = tb_soyisim.Text;
                    string kullaniciAdi = tb_kullaniciadi.Text;
                    string email = tb_email.Text;
                    string sifre = tb_sifre.Text;
                    bool durum = cb_durum.Checked;

                    Yonetici yonetici = new Yonetici()
                    {
                        YoneticiTur_ID = yoneticiTurID,
                        Isim = isim,
                        Soyisim = soyisim,
                        KullaniciAdi = kullaniciAdi,
                        Email = email,
                        Sifre = sifre,
                        Durum = durum
                    };

                    if (db.YoneticiEkle(yonetici))
                    {
                        pnl_basarisiz.Visible = false;
                        pnl_basarili.Visible = true;
                    }
                    else
                    {
                        pnl_basarisiz.Visible = true;
                        lbl_hatamesaj.Text = "Yönetici ekleme işlemi başarısız!";
                    }
                }
                catch (Exception ex)
                {
                    pnl_basarisiz.Visible = true;
                    lbl_hatamesaj.Text = "Bir hata oluştu: " + ex.Message;
                }
            }
        }

    }


}


