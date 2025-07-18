using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Models.Repository;
using Restaurant.ViewModels;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IRepository<MasterCategoryMenu> _masterCategoryMenu, IRepository<MasterContactUsInformation> _masterContactUsInformation,
            IRepository<MasterItemMenu> _masterItemMenu, IRepository<MasterMenu> _masterMenu, IRepository<MasterOffer> _masterOffer,
            IRepository<MasterPartner> _masterPartner, IRepository<MasterService> _masterService, IRepository<MasterSlider> _masterSlider,
            IRepository<MasterSocialMedium> _masterSocialMedium, IRepository<MasterWorkingHour> _masterWorkingHour, IRepository<SystemSetting> _systemSetting,
            IRepository<TransactionBookTable> _transactionBookTable, IRepository<TransactionContactU> _transactionContactU, IRepository<TransactionNewsletter> _transactionNewsletter,
            IRepository<WhatPeopleSay> _whatPeopleSay)
        {
            MasterCategoryMenu = _masterCategoryMenu;
            MasterContactUsInformation = _masterContactUsInformation;
            MasterItemMenu = _masterItemMenu;
            MasterMenu = _masterMenu;
            MasterOffer = _masterOffer;
            MasterPartner = _masterPartner;
            MasterService = _masterService;
            MasterSlider = _masterSlider;
            MasterSocialMedium = _masterSocialMedium;
            MasterWorkingHour = _masterWorkingHour;
            SystemSetting = _systemSetting;
            TransactionBookTable = _transactionBookTable;
            TransactionContactU = _transactionContactU;
            TransactionNewsletter = _transactionNewsletter;
            WhatPeopleSay = _whatPeopleSay;
        }

        public IRepository<MasterCategoryMenu> MasterCategoryMenu { get; }
        public IRepository<MasterContactUsInformation> MasterContactUsInformation { get; }
        public IRepository<MasterItemMenu> MasterItemMenu { get; }
        public IRepository<MasterMenu> MasterMenu { get; }
        public IRepository<MasterOffer> MasterOffer { get; }
        public IRepository<MasterPartner> MasterPartner { get; }
        public IRepository<MasterService> MasterService { get; }
        public IRepository<MasterSlider> MasterSlider { get; }
        public IRepository<MasterSocialMedium> MasterSocialMedium { get; }
        public IRepository<MasterWorkingHour> MasterWorkingHour { get; }
        public IRepository<SystemSetting> SystemSetting { get; }
        public IRepository<TransactionBookTable> TransactionBookTable { get; }
        public IRepository<TransactionContactU> TransactionContactU { get; }
        public IRepository<TransactionNewsletter> TransactionNewsletter { get; }
        public IRepository<WhatPeopleSay> WhatPeopleSay { get; }

        public IActionResult Index()
        {
            var obj = new HomeViewModel
            {
                ListMasterContactUsInformation = MasterContactUsInformation.ViewFormClient().ToList(),
                ListMasterMenu = MasterMenu.ViewFormClient().ToList(),
                MasterOffer = MasterOffer.Find(1),
                ListMasterPartner = MasterPartner.ViewFormClient().ToList(),
                ListMasterSlider = MasterSlider.ViewFormClient().ToList(),
                ListMasterSocialMedium = MasterSocialMedium.ViewFormClient().ToList(),
                ListMasterWorkingHour = MasterWorkingHour.ViewFormClient().ToList(),
                SystemSetting = SystemSetting.Find(1),
                ListMasterItemMenu = MasterItemMenu.ViewFormClient(),
                ListWhatPeopleSay = WhatPeopleSay.ViewFormClient().ToList(),
                ContactUsFooterList= MasterContactUsInformation.ViewFormClient().Where(x=>x.MasterContactUsInformationRedirect== "footer").ToList()
            };
            return View(obj);
        }

        public IActionResult About()
        {
            var obj = new HomeViewModel
            {
                ListMasterMenu = MasterMenu.ViewFormClient().ToList(),
                ListMasterService = MasterService.ViewFormClient().ToList(),
                ListMasterSocialMedium = MasterSocialMedium.ViewFormClient().ToList(),
                ListMasterWorkingHour = MasterWorkingHour.ViewFormClient().ToList(),
                SystemSetting = SystemSetting.Find(1),
                ContactUsFooterList = MasterContactUsInformation.ViewFormClient().Where(x => x.MasterContactUsInformationRedirect == "footer").ToList()

            };
            return View(obj);
        }

        public IActionResult Menu()
        {
            var obj = new HomeViewModel
            {
                ListMasterCategorryMenu = MasterCategoryMenu.ViewFormClient(),
                ListMasterItemMenu = MasterItemMenu.ViewFormClient(),
                ListMasterMenu = MasterMenu.ViewFormClient().ToList(),
                ListMasterPartner = MasterPartner.ViewFormClient().ToList(),
                ListMasterSocialMedium = MasterSocialMedium.ViewFormClient().ToList(),
                ListMasterWorkingHour = MasterWorkingHour.ViewFormClient().ToList(),
                SystemSetting = SystemSetting.Find(1),
                ContactUsFooterList = MasterContactUsInformation.ViewFormClient().Where(x => x.MasterContactUsInformationRedirect == "footer").ToList()

            };
            return View(obj);
        }

        public IActionResult ContactUs()
        {
            var obj = new HomeViewModel
            {
                ListMasterMenu = MasterMenu.ViewFormClient().ToList(),
                ListMasterSocialMedium = MasterSocialMedium.ViewFormClient().ToList(),
                ListMasterWorkingHour = MasterWorkingHour.ViewFormClient().ToList(),
                SystemSetting = SystemSetting.Find(1),
                ContactUsFooterList = MasterContactUsInformation.ViewFormClient().Where(x => x.MasterContactUsInformationRedirect == "footer").ToList(),
                ContactUsList = MasterContactUsInformation.ViewFormClient().Where(x => x.MasterContactUsInformationRedirect == "Contact").ToList()


            };
            return View(obj);
        }

        public IActionResult ProductDetails(int idDetails)
        {
            HomeViewModel obj = new HomeViewModel();

            obj.ListMasterMenu = MasterMenu.ViewFormClient().ToList();
            obj.SystemSetting = SystemSetting.Find(5);
            obj.ListMasterSocialMedium = MasterSocialMedium.ViewFormClient().ToList();
            obj.ItemMenu = MasterItemMenu.Find(idDetails);
            obj.ListMasterContactUsInformation = MasterContactUsInformation.ViewFormClient().ToList();
            obj.ListMasterWorkingHour = MasterWorkingHour.ViewFormClient().ToList();
            obj.ContactUsFooterList = MasterContactUsInformation.ViewFormClient().Where(x => x.MasterContactUsInformationRedirect == "footer").ToList();
            obj.ListMasterItemMenu = MasterItemMenu.ViewFormClient().ToList();
            return View(obj);
        }

        [HttpPost]
        public IActionResult BookTable(HomeViewModel data)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.message = "Failed";
                return RedirectToAction("Index", "Home");
            }

            TransactionBookTable.Add(data.TransactionBookTable);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult ContactU(HomeViewModel data)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.message = "Failed";
                return RedirectToAction("ContactU", "Home");
            }

            TransactionContactU.Add(data.TransactionContactU);
            return RedirectToAction("ContactUs", "Home");
        }

        [HttpPost]
        public IActionResult NewsLetter(HomeViewModel data)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.message = "Failed";
                return RedirectToAction("Index", "Home");
            }

            TransactionNewsletter.Add(data.TransactionNewsletter);
            return RedirectToAction("Index", "Home");
        }
    }
}
