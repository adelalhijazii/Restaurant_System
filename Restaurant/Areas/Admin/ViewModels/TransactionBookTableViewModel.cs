using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class TransactionBookTableViewModel : BaseEntity
    {
        public int TransactionBookTableId { get; set; }

        public string TransactionBookTableFullName { get; set; }

        public string TransactionBookTableEmail { get; set; }

        public string TransactionBookTableMobileNumber { get; set; }

        public DateTime TransactionBookTableDate { get; set; }
    }
}
