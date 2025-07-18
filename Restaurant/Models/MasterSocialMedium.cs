using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class MasterSocialMedium : BaseEntity
    {
        public int MasterSocialMediumId { get; set; }

        public string MasterSocialMediumImageUrl { get; set; }

        public string MasterSocialMediumUrl { get; set; }
    }
}
