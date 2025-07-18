using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class MasterServiceViewModel : BaseEntity
    {
        public int MasterServiceId { get; set; }

        public string MasterServiceTitle { get; set; }

        public string MasterServiceDesc { get; set; }

        public string MasterServiceImage { get; set; }

        public IFormFile MasterServiceFile { get; set; }
    }
}
