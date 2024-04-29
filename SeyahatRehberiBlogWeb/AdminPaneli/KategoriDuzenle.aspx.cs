using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace SeyahatRehberiBlogWeb.AdminPaneli
{
    public partial class KategoriDuzenle : System.Web.UI.Page
    {
        VeriModeli db = new VeriModeli();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtn_kategoriekle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_isim.Text))
            {
                Kategori kat = new Kategori();
                kat.Isim = tb_isim.Text;
                kat.Aciklama = tb_aciklama.Text;
                kat.Durum = cb_durum.Checked;
                if (db.KategoriEkle(kat))
                {
                    pnl_basarilipanel.Visible = true;
                    pnl_basarisizpanel.Visible = false;
                }
                else
                {
                    pnl_basarilipanel.Visible = false;
                    pnl_basarisizpanel.Visible = true;
                    lbl_hatamesaj.Text = "Kategori eklenirken bir hata oluştu";
                }

            }
            else
            {
                pnl_basarilipanel.Visible = false;
                pnl_basarisizpanel.Visible = true;
                lbl_hatamesaj.Text = "Kategori adı boş bırakılamaz";
            }
        }
    }
}