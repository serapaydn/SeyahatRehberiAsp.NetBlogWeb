using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb.AdminPaneli
{
    public partial class AdminGiris : System.Web.UI.Page
    {
        VeriModeli db = new VeriModeli();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_tikla_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_kullanici.Text))
            {
                if (!string.IsNullOrEmpty(tb_sifre.Text))
                {
                    Yonetici yon = db.YoneticiGiris(tb_kullanici.Text, tb_sifre.Text);
                    if (yon != null)
                    {
                        if (yon.ID != 0)
                        {
                            if (yon.Durum)
                            {
                                Session["Yonetici"] = yon;
                                Response.Redirect("Default.aspx");
                            }
                            else
                            {
                                pnl_basarisiz.Visible = true;
                                lbl_mesaj.Text = "Hesabınız askıya alınmıştır. Yönetici ile görüşünüz!";
                            }
                        }
                        else
                        {
                            pnl_basarisiz.Visible = true;
                            lbl_mesaj.Text = "Kullanıcı bulunamadı. Bilgileri kontrol ediniz";
                        }
                    }
                    else
                    {
                        pnl_basarisiz.Visible = true;
                        lbl_mesaj.Text = "Bir hata oluştu lütfen daha sonra deneyiniz";
                    }
                }
                else
                {
                    pnl_basarisiz.Visible = true;
                    lbl_mesaj.Text = "Şifre boş bırakılamaz";
                }
            }
            else
            {
                pnl_basarisiz.Visible = true;
                lbl_mesaj.Text = "Kullanıcı adı boş bırakılamaz";
            }

        }
    
    }
}