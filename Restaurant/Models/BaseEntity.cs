using System.Security.Cryptography.X509Certificates;

namespace Restaurant.Models
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public string CreateUser { get; set; }

        public DateTime? CreateDate { get; set; }

        public string EditUser { get; set; }

        public DateTime? EditDate { get; set; }
    }
}
