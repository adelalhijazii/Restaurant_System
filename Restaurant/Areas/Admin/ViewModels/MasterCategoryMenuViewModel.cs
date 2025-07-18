using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class MasterCategoryMenuViewModel : BaseEntity
    {
        public int MasterCategoryMenuId { get; set; }

        public string MasterCategoryMenuName { get; set; }
    }
}
