using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class MasterOffer : BaseEntity 
    {
        public int MasterOfferId { get; set; }

        public string MasterOfferTitle { get; set; }

        public string MasterOfferBreef { get; set; }

        public string MasterOfferDesc { get; set; }

        public string MasterOfferImageUrl { get; set; }
    }
}
