using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChartsApp
{
    class Model
    {
        public string Year { get; set; }
        public string BTC { get; set; }

        public Model(string year, string Btc)
        {
            Year = year;
            BTC = Btc;
        }
    }
}
