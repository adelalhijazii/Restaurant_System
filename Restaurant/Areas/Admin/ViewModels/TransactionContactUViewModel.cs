using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class TransactionContactUViewModel : BaseEntity
    {
        public int TransactionContactUId { get; set; }

        public string TransactionContactUFullName { get; set; }

        public string TransactionContactUEmail { get; set; }

        public string TransactionContactUSubject { get; set; }

        public string TransactionContactUMessage { get; set; }
    }
}
