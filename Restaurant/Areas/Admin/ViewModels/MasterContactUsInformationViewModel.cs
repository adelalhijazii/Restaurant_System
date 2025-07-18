using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class MasterContactUsInformationViewModel : BaseEntity
    {
        public int MasterContactUsInformationId { get; set; }

        public string MasterContactUsInformationIdesc { get; set; }

        public string MasterContactUsInformationImageUrl { get; set; }

        public string MasterContactUsInformationRedirect { get; set; }

        public IFormFile MasterContactUsInformationFile { get; set; }
    }
}
