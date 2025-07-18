using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class MasterMenu : BaseEntity
    {
        public int MasterMenuId { get; set; }

        public string MasterMenuName { get; set; }

        public string MasterMenuUrl { get; set; }
    }
}
