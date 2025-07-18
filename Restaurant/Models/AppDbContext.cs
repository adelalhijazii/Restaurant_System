using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Restaurant.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<MasterCategoryMenu> MasterCategoryMenu { get; set; }

        public DbSet<MasterContactUsInformation> MasterContactUsInformation { get; set; }

        public DbSet<MasterItemMenu> MasterItemMenu { get; set; }

        public DbSet<MasterMenu> MasterMenu { get; set; }

        public DbSet<MasterOffer> MasterOffer { get; set; }

        public DbSet<MasterPartner> MasterPartner { get; set; }

        public DbSet<MasterService> MasterService { get; set; }

        public DbSet<MasterSlider> MasterSlider { get; set; }

        public DbSet<MasterSocialMedium> MasterSocialMedium { get; set; }

        public DbSet<MasterWorkingHour> MasterWorkingHour { get; set; }

        public DbSet<SystemSetting> SystemSetting { get; set; }

        public DbSet<TransactionBookTable> TransactionBookTable { get; set; }

        public DbSet<TransactionContactU> TransactionContactU { get; set; }

        public DbSet<TransactionNewsletter> TransactionNewsletter { get; set; }

        public DbSet<WhatPeopleSay> WhatPeopleSay { get; set; }
    }
}
