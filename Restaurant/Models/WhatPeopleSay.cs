namespace Restaurant.Models
{
    public class WhatPeopleSay : BaseEntity
    {
        public int WhatPeopleSayId { get; set; }

        public string WhatPeopleSayTitle { get; set; }

        public string WhatPeopleSayDesc { get; set; }

        public string WhatPeopleSayImageUrl { get; set; }
    }
}
