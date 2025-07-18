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
    public class MasterPartnerController : Controller
    {
        public IRepository<MasterPartner> MasterPartner { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Hosting { get; }
        public UserManager<AppUser> UserManager { get; }

        public MasterPartnerController(IRepository<MasterPartner> _masterPartner, Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting, UserManager<AppUser> _userManager)
        {
            MasterPartner = _masterPartner;
            Hosting = _hosting;
            UserManager = _userManager;
        }
        // GET: MasterPartnerController
        public ActionResult Index()
        {
            var data = MasterPartner.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = MasterPartner.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            MasterPartner.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterPartnerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterPartnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterPartnerViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View();
                }
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterPartnerFile);

                var data = new MasterPartner
                {
                    MasterPartnerId = collection.MasterPartnerId,
                    MasterPartnerName = collection.MasterPartnerName,
                    MasterPartnerLogoImageUrl = ImageName,
                    MasterPartnerWebsiteUrl = collection.MasterPartnerWebsiteUrl,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                MasterPartner.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterPartnerController/Edit
        public ActionResult Edit(int id)
        {
            var data = MasterPartner.Find(id);
            MasterPartnerViewModel masterPartnerViewModel = new MasterPartnerViewModel();
            masterPartnerViewModel.MasterPartnerId = data.MasterPartnerId;
            masterPartnerViewModel.MasterPartnerName = data.MasterPartnerName;
            masterPartnerViewModel.MasterPartnerLogoImageUrl = data.MasterPartnerLogoImageUrl;
            masterPartnerViewModel.MasterPartnerWebsiteUrl = data.MasterPartnerWebsiteUrl;
            masterPartnerViewModel.CreateUser = data.CreateUser;
            masterPartnerViewModel.CreateDate = data.CreateDate;
            return View(masterPartnerViewModel);
        }

        // POST: MasterPartnerController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MasterPartnerViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterPartnerFile);

                var data = new MasterPartner
                {
                    MasterPartnerId = collection.MasterPartnerId,
                    MasterPartnerName = collection.MasterPartnerName,
                    MasterPartnerLogoImageUrl = (ImageName != "") ? ImageName : collection.MasterPartnerLogoImageUrl,
                    MasterPartnerWebsiteUrl = collection.MasterPartnerWebsiteUrl,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                MasterPartner.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterPartnerController/Delete
        public ActionResult Delete(int Delete)
        {
            MasterPartner.Delete(Delete, new Models.MasterPartner { EditUser = User.Identity.Name, EditDate = DateTime.Now });
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
