using Restaurant.Models;

namespace Restaurant.ViewModels
{
    public class HomeViewModel
    {
        public IList<MasterCategoryMenu> ListMasterCategorryMenu { get; set; }

        public List<MasterContactUsInformation> ListMasterContactUsInformation { get; set; }

        public IList<MasterItemMenu> ListMasterItemMenu { get; set; }

        public MasterItemMenu ItemMenu { get; set; }

        public List<MasterMenu> ListMasterMenu { get; set; }

        public MasterOffer MasterOffer { get; set; }

        public List<MasterPartner> ListMasterPartner { get; set; }

        public List<MasterService> ListMasterService { get; set; }

        public List<MasterSlider> ListMasterSlider { get; set; }

        public List<MasterSocialMedium> ListMasterSocialMedium { get; set; }

        public List<MasterWorkingHour> ListMasterWorkingHour { get; set; }

        public SystemSetting SystemSetting { get; set; }

        public TransactionBookTable TransactionBookTable { get; set; }

        public TransactionContactU TransactionContactU { get; set; }

        public TransactionNewsletter TransactionNewsletter { get; set; }

        public List<WhatPeopleSay> ListWhatPeopleSay { get; set; }


        public IList<MasterContactUsInformation> ContactUsFooterList { get; set; }
        public IList<MasterContactUsInformation> ContactUsList { get; set; }


    }
}
