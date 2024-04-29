using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb
{
    public partial class Makaleİcerik : System.Web.UI.Page
    {
        VeriModeli db = new VeriModeli();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count != 0)
            {
                int id = Convert.ToInt32(Request.QueryString["MakaleID"]);
                Makale m = db.MakaleGetir(id);
                ltrl_baslik.Text = m.Baslik;
                ltrl_Icerik.Text = m.Icerik;
                ltrl_kategori.Text = m.Kategori;
                ltrl_Tarih.Text = m.TarihStr;
                ltrl_Yazar.Text = m.Yazar; 
                ltrl_Konum.Text = m.Konum;
                img_resim.ImageUrl = "Resimler/MakaleResimleri/" + m.KapakResim;

                rp_yorumlar.DataSource = db.YorumlariGetir(id);
                rp_yorumlar.DataBind();

                if (Session["uye"] != null)
                {
                    pnl_girisvar.Visible = true;
                    pnl_girisYok.Visible = false;
                }
                else
                {
                    pnl_girisvar.Visible = false;
                    pnl_girisYok.Visible = true;
                }

            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void lbtn_ekle_Click(object sender, EventArgs e)
        {
            if (Session["uye"] != null)
            {
                try
                {
                    int makaleID = Convert.ToInt32(Request.QueryString["MakaleID"]);
                    Uye u = Session["uye"] as Uye;
                    if (u != null)
                    {
                        int uyeID = u.ID;
                        string icerik = tb_yorum.Text;

                        
                        Yorum yeniYorum = new Yorum();
                        yeniYorum.Makale_ID = makaleID;
                        yeniYorum.Uye_ID = uyeID;
                        yeniYorum.Icerik = icerik;
                        yeniYorum.TarihveSaat = DateTime.Now;
                        yeniYorum.Durum = true;

                       
                        string uyeIsim = u.Isim + " " + u.Soyisim;
                        yeniYorum.UyeIsim = uyeIsim;

                        VeriModeli db = new VeriModeli();
                        bool eklemeBasarili = db.YorumEkle(yeniYorum);

                        if (eklemeBasarili)
                        {
                            Response.Redirect(Request.RawUrl);
                        }
                        else
                        {
                            pnl_basarisiz.Visible = true;
                            lbl_hatamesaj.Text = "Yorum eklenirken bir hata oluştu. Lütfen tekrar deneyin.";
                        }
                    }
                    else
                    {
                        pnl_basarisiz.Visible = true;
                        lbl_hatamesaj.Text = "Kullanıcı bilgileri alınamadı.";
                    }
                }
                catch (Exception ex)
                {
                    pnl_basarisiz.Visible = true;
                    lbl_hatamesaj.Text = "Bir hata oluştu: " + ex.Message;
                }
            }
            else
            {
                pnl_basarisiz.Visible = true;
                lbl_hatamesaj.Text = "Yorum yapabilmek için giriş yapmalısınız.";
            }
        }
    }
}