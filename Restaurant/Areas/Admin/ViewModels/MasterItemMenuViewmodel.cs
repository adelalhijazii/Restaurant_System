using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class MasterItemMenuViewmodel : BaseEntity
    {
        public int MasterItemMenuId { get; set; }

        public int MasterCategoryMenuId { get; set; }

        public string MasterItemMenuTitle { get; set; }

        public string MasterItemMenuBreef { get; set; }

        public string MasterItemMenuDesc { get; set; }

        public decimal MasterItemMenuPrice { get; set; }

        public string MasterItemMenuImageUrl { get; set; }

        public DateTime MasterItemMenuDate { get; set; }

        public MasterCategoryMenu MasterCategoryMenu { get; set; }

        public IFormFile MasterItemMenuFile { get; set; }

        public List<MasterCategoryMenu> ListMasterCategoryMenu { get; set; }
    }
}
