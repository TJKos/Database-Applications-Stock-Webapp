using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProjekt.Shared.Models
{
    public class StockInfo
    {
        public int IdStockInfo { get; set; }
        public string Name { get; set; }
        public string Ticker { get; set; }
        public string Locale { get; set; }
        public string Market { get; set; }
        public string Phone_Number { get; set; }
        public string Homepage_Url { get; set; }
        public string Description { get; set; }
        public string Sic_Description { get; set; }
        public string Primary_Exchange { get; set; }
#nullable enable
        public string? Logo_Url { get; set; }
        public string? Icon_Url { get; set; }
#nullable disable
    }
}