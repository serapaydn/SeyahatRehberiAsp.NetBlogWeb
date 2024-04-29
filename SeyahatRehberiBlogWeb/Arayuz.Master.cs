using SeyahatRehberiBlogWeb.AdminPaneli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb
{
    public partial class Arayuz : System.Web.UI.MasterPage
    {
        VeriModeli db = new VeriModeli();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uye"] != null)
            {
                Uye u = Session["uye"] as Uye;
                pnl_girisvar.Visible = true;
                pnl_girisyok.Visible = false;
                ltrl_uye.Text = u.KullaniciAdi;
            }
            else
            {
                pnl_girisvar.Visible = false;
                pnl_girisyok.Visible = true;
            }

            rp_kategoriler.DataSource = db.TumKategorileriGetir(true);
            rp_kategoriler.DataBind();
        }

        protected void lbtn_cikis_Click(object sender, EventArgs e)
        {
            Session["uye"] = null;
            Response.Redirect("Default.aspx");
        }

        protected void btnAra_Click(object sender, EventArgs e)
        {
            string anahtarKelime = tb_Arama.Text.Trim();

            if (!string.IsNullOrEmpty(anahtarKelime))
            {
                List<Makale> bulunanMakaleler = db.MakaleAra(anahtarKelime);

                if (bulunanMakaleler.Count > 0)
                {
                    string aramaSonucu = "<div class='makaleler'>";

                    foreach (Makale makale in bulunanMakaleler)
                    {
                        aramaSonucu += "<div class='makale'>";
                        aramaSonucu += $"<div class='resim'><img src='Resimler/MakaleResimleri/{makale.KapakResim}' /></div>";
                        aramaSonucu += $"<div class='baslik'><h2><a href='MakaleIcerik.aspx?MakaleID={makale.ID}'>{makale.Baslik}</a></h2></div>";
                        aramaSonucu += $"<div class='bilgi'><label>Kategori:</label>{makale.Kategori} <span style='font-weight: bold; font-size: 25pt; color: dimgray; padding-left: 5px; padding-right: 5px;'>.</span>";
                        aramaSonucu += $"<label>Yazar:</label>{makale.Yazar} <span style='font-weight: bold; font-size: 25pt; color: dimgray; padding-left: 5px; padding-right: 5px;'>.</span>";
                        aramaSonucu += $"<label>Tarih:</label>{makale.TarihStr} <span style='font-weight: bold; font-size: 25pt; color: dimgray; padding-left: 5px; padding-right: 5px;'>.</span>";
                        aramaSonucu += $"<label>Konum:</label>{makale.Konum}</div>";
                        aramaSonucu += $"<div class='ozet'>{makale.Ozet}</div>";
                        aramaSonucu += $"<div class='devami'><a href='MakaleIcerik.aspx?MakaleID={makale.ID}'><label>Devamını Oku</label></a></div>";
                        aramaSonucu += "</div>";
                    }

                    aramaSonucu += "</div>";
                    lblAramaSonuc.Text = aramaSonucu;
                    pnlAramaSonuc.Visible = true;
                }
                else
                {
                    lblAramaSonuc.Text = "<div class='AramaSonucu'>Aranan kelimeye uygun makale bulunamadı.</div>";
                    pnlAramaSonuc.Visible = true;
                }
            }
            else
            {
                lblAramaSonuc.Text = "<div class='AramaSonucu'>Lütfen aramak istediğiniz kelimeyi girin.</div>";
                pnlAramaSonuc.Visible = true;
            }

            ContentPlaceHolder1.Visible = false;
        }
    }
}