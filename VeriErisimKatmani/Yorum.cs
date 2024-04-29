using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriErisimKatmani
{
    public class Yorum
    {
        public int ID { get; set; }
        public int Makale_ID { get; set; }
        public int Uye_ID { get; set; }
        public string Icerik { get; set; }
        public DateTime TarihveSaat { get; set; }
        public bool Durum { get; set; }
        public string UyeIsim { get; set; }
    }
}
