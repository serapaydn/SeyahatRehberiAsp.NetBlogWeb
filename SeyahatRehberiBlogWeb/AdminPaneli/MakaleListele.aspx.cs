using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb.AdminPaneli
{
    public partial class MakaleListele : System.Web.UI.Page
    {
        VeriModeli db = new VeriModeli();
        protected void Page_Load(object sender, EventArgs e)
        {
            lv_makaleler.DataSource = db.MakaleListele();
            lv_makaleler.DataBind();
        }

        protected void lv_makaleler_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "sil")
            {
                db.MakaleSil(id);
            }
            if (e.CommandName == "durum")
            {
                db.MakaleDurumDegistir(id);
            }

            lv_makaleler.DataSource = db.MakaleListele();
            lv_makaleler.DataBind();
        }
    }
}