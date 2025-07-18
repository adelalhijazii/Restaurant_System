using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class WhatPeopleSayViewModel : BaseEntity
    {
        public int WhatPeopleSayId { get; set; }

        public string WhatPeopleSayTitle { get; set; }

        public string WhatPeopleSayDesc { get; set; }

        public string WhatPeopleSayImageUrl { get; set; }

        public IFormFile WhatPeopleSayFile { get; set; }
    }
}
