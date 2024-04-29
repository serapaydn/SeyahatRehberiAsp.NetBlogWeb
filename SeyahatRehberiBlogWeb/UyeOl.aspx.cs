using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb
{
    public partial class UyeOl : System.Web.UI.Page
    {
        VeriModeli db = new VeriModeli();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_tikla_Click(object sender, EventArgs e)
        {
            Uye u = new Uye();
            u.Isim = tb_isim.Text;
            u.Soyisim = tb_soyisim.Text;
            u.KullaniciAdi = tb_kullanici.Text;
            u.Email = tb_mail.Text;
            u.Sifre = tb_sifre.Text;
            u.Durum = cb_durum.Checked;
            u.UyelikTarihi = DateTime.Now;

            if (string.IsNullOrEmpty(tb_mail.Text))
            {
                pnl_basarisiz.Visible = true;
                lbl_hatamesaj.Text = "Lütfen e-posta adresinizi girin";
                return;
            }

            if (!tb_mail.Text.Contains("@") || !tb_mail.Text.EndsWith(".com"))
            {
                pnl_basarisiz.Visible = true;
                lbl_hatamesaj.Text = "Geçersiz e-posta adresi";
                return;
            }

            if (string.IsNullOrEmpty(tb_isim.Text) ||
                string.IsNullOrEmpty(tb_soyisim.Text) ||
                string.IsNullOrEmpty(tb_kullanici.Text) ||
                string.IsNullOrEmpty(tb_sifre.Text))
            {
                pnl_basarisiz.Visible = true;
                lbl_hatamesaj.Text = "Lütfen tüm alanları doldurun";
                return;
            }

            if (db.UyeOl(u))
            {
                pnl_basarili.Visible = true;
                pnl_basarisiz.Visible = false;
            }
            else
            {
                pnl_basarili.Visible = false;
                pnl_basarisiz.Visible = true;
                lbl_hatamesaj.Text = "Üye eklenirken bir hata oluştu";
            }
        }
    }

}

