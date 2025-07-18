using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class MasterSliderViewModel : BaseEntity
    {
        public int MasterSliderId { get; set; }

        public string MasterSliderTitle { get; set; }

        public string MasterSliderBreef { get; set; }

        public string MasterSliderDesc { get; set; }

        public string MasterSliderUrl { get; set; }

        public IFormFile MasterSliderFile { get; set; }
    }
}
