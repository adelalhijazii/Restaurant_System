using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class MasterSocialMediumViewModel : BaseEntity
    {
        public int MasterSocialMediumId { get; set; }

        public string MasterSocialMediumImageUrl { get; set; }

        public string MasterSocialMediumUrl { get; set; }

        public IFormFile MasterSocialMediumFile { get; set; }
    }
}
