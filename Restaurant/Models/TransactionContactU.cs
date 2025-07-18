using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class TransactionContactU : BaseEntity
    {
        public int TransactionContactUId { get; set; }

        public string TransactionContactUFullName { get; set; }

        public string TransactionContactUEmail { get; set; }

        public string TransactionContactUSubject { get; set; }

        public string TransactionContactUMessage { get; set; }
    }
}
