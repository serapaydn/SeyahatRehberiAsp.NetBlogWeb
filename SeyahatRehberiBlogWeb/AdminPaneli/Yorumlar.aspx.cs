using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb.AdminPaneli
{
    public partial class Yorumlar : System.Web.UI.Page
    {
        VeriModeli db = new VeriModeli();
        protected void Page_Load(object sender, EventArgs e)
        {
            lv_yorumlar.DataSource = db.TumYorumlariGetir();
            lv_yorumlar.DataBind();
        }

        protected void lv_yorumlar_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if(e.CommandName == "sil")
            {
                db.YorumSil(id);
            }
            if(e.CommandName == "durum")
            {
                db.YorumDurumDegistir(id);
            }

            lv_yorumlar.DataSource = db.TumYorumlariGetir();
            lv_yorumlar.DataBind();
        }
    }
}