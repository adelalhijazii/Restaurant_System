using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class MasterMenuViewmodel : BaseEntity
    {
        public int MasterMenuId { get; set; }

        public string MasterMenuName { get; set; } = null!;

        public string MasterMenuUrl { get; set; } = null!;
    }
}
