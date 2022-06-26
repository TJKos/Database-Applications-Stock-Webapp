using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDProjekt.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<StockInfoDB> StockInfo { get; set; }
        public virtual ICollection<StockInfo_ApplicationUser> StockInfo_ApplicationUser { get; set; }

    }
}
