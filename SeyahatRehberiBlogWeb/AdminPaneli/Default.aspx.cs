using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb.AdminPaneli
{
    public partial class Default : System.Web.UI.Page
    {
        VeriModeli db = new VeriModeli();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int makaleSayisi = db.MakaleSayisiGetir();
                int uyeSayisi = db.UyeSayisiGetir();
                int yorumSayisi = db.YorumSayisiGetir();
                int yoneticiSayisi = db.YoneticiSayisiGetir();

                lblMakaleSayisi.Text = makaleSayisi.ToString();
                lblUyeSayisi.Text = uyeSayisi.ToString();
                lblYorumSayisi.Text = yorumSayisi.ToString();
                lblYoneticiSayisi.Text = yoneticiSayisi.ToString();
            }
        }
    }
}