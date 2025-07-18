using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class MasterWorkingHourViewModel : BaseEntity
    {
        public int MasterWorkingHourId { get; set; }

        public string MasterWorkingHourIdName { get; set; }

        public string MasterWorkingHourIdTimeFormTo { get; set; }
    }
}
