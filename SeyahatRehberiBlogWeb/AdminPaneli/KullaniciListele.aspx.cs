using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb.AdminPaneli
{
    public partial class KullaniciListele : System.Web.UI.Page
    {
        VeriModeli db = new VeriModeli();
        protected void Page_Load(object sender, EventArgs e)
        {
            lv_kullanicilar.DataSource = db.TumYoneticileriGetir();
            lv_kullanicilar.DataBind();
        }

        protected void lv_kullanicilar_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "sil")
            {
                db.YoneticiSil(id);
            }
            if (e.CommandName == "durum")
            {
                db.YoneticiDurumDegistir(id);
            }

            lv_kullanicilar.DataSource = db.TumYoneticileriGetir();
            lv_kullanicilar.DataBind();
        }
    }
}