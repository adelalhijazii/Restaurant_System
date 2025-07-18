using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Areas.Admin.ViewModels;
using Restaurant.Models;
using Restaurant.Models.Repository;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MasterOfferController : Controller
    {
        public IRepository<MasterOffer> MasterOffer { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Hosting { get; }
        public UserManager<AppUser> UserManager { get; }

        public MasterOfferController(IRepository<MasterOffer> _masterOffer, Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting, UserManager<AppUser> _userManager)
        {
            MasterOffer = _masterOffer;
            Hosting = _hosting;
            UserManager = _userManager;
        }
        // GET: MasterOfferController
        public ActionResult Index()
        {
            var data = MasterOffer.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = MasterOffer.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            MasterOffer.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterOfferController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterOfferController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterOfferViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View();
                }
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterOfferFile);

                var data = new MasterOffer
                {
                    MasterOfferId = collection.MasterOfferId,
                    MasterOfferTitle = collection.MasterOfferTitle,
                    MasterOfferBreef = collection.MasterOfferBreef,
                    MasterOfferDesc = collection.MasterOfferDesc,
                    MasterOfferImageUrl = ImageName,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                MasterOffer.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterOfferController/Edit
        public ActionResult Edit(int id)
        {
            var data = MasterOffer.Find(id);
            MasterOfferViewModel masterOfferViewModel = new MasterOfferViewModel();
            masterOfferViewModel.MasterOfferId = data.MasterOfferId;
            masterOfferViewModel.MasterOfferTitle = data.MasterOfferTitle;
            masterOfferViewModel.MasterOfferBreef = data.MasterOfferBreef;
            masterOfferViewModel.MasterOfferDesc = data.MasterOfferDesc;
            masterOfferViewModel.MasterOfferImageUrl = data.MasterOfferImageUrl;
            masterOfferViewModel.CreateUser = data.CreateUser;
            masterOfferViewModel.CreateDate = data.CreateDate;
            return View(masterOfferViewModel);
        }

        // POST: MasterOfferController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MasterOfferViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterOfferFile);

                var data = new MasterOffer
                {
                    MasterOfferId = collection.MasterOfferId,
                    MasterOfferTitle = collection.MasterOfferTitle,
                    MasterOfferBreef = collection.MasterOfferBreef,
                    MasterOfferDesc = collection.MasterOfferDesc,
                    MasterOfferImageUrl = (ImageName != "") ? ImageName : collection.MasterOfferImageUrl,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                MasterOffer.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterOfferController/Delete
        public ActionResult Delete(int Delete)
        {
            MasterOffer.Delete(Delete, new Models.MasterOffer { EditUser = User.Identity.Name, EditDate = DateTime.Now });
            return RedirectToAction(nameof(Index));
        }

        string UploadFile(IFormFile File)
        {
            string fileName = "";
            if (File != null)
            {
                string pathFile = Path.Combine(Hosting.WebRootPath, "photos");
                FileInfo fileInfo = new FileInfo(File.FileName);
                fileName = "Image_" + Guid.NewGuid() + fileInfo.Extension;
                string fullPath = Path.Combine(pathFile, fileName);
                File.CopyTo(new FileStream(fullPath, FileMode.Create));
            }
            return fileName;
        }
    }
}
