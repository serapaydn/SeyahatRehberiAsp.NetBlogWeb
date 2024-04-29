using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb.AdminPaneli
{
    public partial class ProfilAyarlari : System.Web.UI.Page
    {
        VeriModeli db = new VeriModeli();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Yonetici yonetici = Session["yonetici"] as Yonetici;
                if (yonetici != null)
                {
                    tb_kullaniciadi.Text = yonetici.KullaniciAdi;
                    tb_isim.Text = yonetici.Isim;
                    tb_soyisim.Text = yonetici.Soyisim;
                    tb_email.Text = yonetici.Email;
                    cb_durum.Checked = yonetici.Durum;
                }
            }
        }

        protected void lbtn_ekle_Click(object sender, EventArgs e)
        {
            string email = tb_email.Text;
            string yeniSifre = tb_sifre.Text;
            string isim = tb_isim.Text;
            string soyisim = tb_soyisim.Text;
            string kullaniciAdi = tb_kullaniciadi.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(yeniSifre) || string.IsNullOrEmpty(isim) || string.IsNullOrEmpty(soyisim) || string.IsNullOrEmpty(kullaniciAdi))
            {
                pnl_basarisiz.Visible = true;
                lbl_hatamesaj.Text = "Lütfen tüm alanları doldurun!";
                return;
            }

            Yonetici yonetici = Session["yonetici"] as Yonetici;
            if (yonetici != null)
            {
                if (db.YoneticiProfilGuncelle(yonetici.ID, isim, soyisim, kullaniciAdi, email, yeniSifre))
                {
                    pnl_basarili.Visible = true;
                    lbl_hatamesaj.Text = "Profil başarıyla güncellenmiştir";
                }
                else
                {
                    pnl_basarisiz.Visible = true;
                    lbl_hatamesaj.Text = "Profil güncelleme işlemi başarısız!";
                }
            }
            else
            {
                pnl_basarisiz.Visible = true;
                lbl_hatamesaj.Text = "Geçersiz email adresi!";
            }
        }
    }

    

}