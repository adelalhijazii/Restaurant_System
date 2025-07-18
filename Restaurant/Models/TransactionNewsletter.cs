using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class TransactionNewsletter : BaseEntity
    {
        public int TransactionNewsletterId { get; set; }

        public string TransactionNewsletterEmail { get; set; }
    }
}
