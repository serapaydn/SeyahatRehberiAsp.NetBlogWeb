using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb.AdminPaneli
{
    public partial class AdminPanel : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Yonetici"] == null)
            {
                Response.Redirect("AdminGiris.aspx");
            }
            else
            {
                Yonetici yon = (Yonetici)Session["Yonetici"];
                lbl_kullanici.Text = yon.KullaniciAdi + "(" + yon.YoneticiTur + ")";
            }
        }

        protected void lbtn_cikis_Click(object sender, EventArgs e)
        {

            Session["Yonetici"] = null;
            Response.Redirect("AdminGiris.aspx");
        }
    }
}