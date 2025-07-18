using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class MasterContactUsInformation : BaseEntity
    {
        public int MasterContactUsInformationId { get; set; }

        public string MasterContactUsInformationIdesc { get; set; }

        public string MasterContactUsInformationImageUrl { get; set; }

        public string MasterContactUsInformationRedirect { get; set; }
    }
}
