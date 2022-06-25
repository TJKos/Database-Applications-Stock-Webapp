using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProjekt.Shared.Models
{
    public class Stock
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Market { get; set; }
        public string Primary_Exchange { get; set; }
        public string Currency_Name { get; set; }
    }
}