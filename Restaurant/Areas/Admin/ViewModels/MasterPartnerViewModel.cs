using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class MasterPartnerViewModel : BaseEntity
    {
        public int MasterPartnerId { get; set; }

        public string MasterPartnerName { get; set; }

        public string MasterPartnerLogoImageUrl { get; set; }

        public string MasterPartnerWebsiteUrl { get; set; }

        public IFormFile MasterPartnerFile { get; set; }
    }
}
