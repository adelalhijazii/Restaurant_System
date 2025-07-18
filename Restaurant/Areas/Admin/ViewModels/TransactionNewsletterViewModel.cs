using Restaurant.Models;

namespace Restaurant.Areas.Admin.ViewModels
{
    public class TransactionNewsletterViewModel : BaseEntity
    {
        public int TransactionNewsletterId { get; set; }

        public string TransactionNewsletterEmail { get; set; }
    }
}
