using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class MasterOfferViewModel : BaseEntity
    {
        public int MasterOfferId { get; set; }

        public string MasterOfferTitle { get; set; }

        public string MasterOfferBreef { get; set; }

        public string MasterOfferDesc { get; set; }

        public string MasterOfferImageUrl { get; set; }

        public IFormFile MasterOfferFile { get; set; }
    }
}
