using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProjekt.Shared.Models
{
    public class Stock
    {
        public int IdStock { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Market { get; set; }
        public string Primary_Exchange { get; set; }
    }
}