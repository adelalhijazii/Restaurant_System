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
    public class MasterSocialMediumController : Controller
    {
        public IRepository<MasterSocialMedium> MasterSocialMedium { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Hosting { get; }
        public UserManager<AppUser> UserManager { get; }

        public MasterSocialMediumController(IRepository<MasterSocialMedium> _masterSocialMedium, Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting, UserManager<AppUser> _userManager)
        {
            MasterSocialMedium = _masterSocialMedium;
            Hosting = _hosting;
            UserManager = _userManager;
        }
        // GET: MasterSocialMediumController
        public ActionResult Index()
        {
            var data = MasterSocialMedium.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = MasterSocialMedium.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            MasterSocialMedium.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterSocialMediumController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterSocialMediumController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterSocialMediumViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View();
                }
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterSocialMediumFile);
                var data = new MasterSocialMedium
                {
                    MasterSocialMediumId = collection.MasterSocialMediumId,
                    MasterSocialMediumImageUrl = ImageName,
                    MasterSocialMediumUrl = collection.MasterSocialMediumUrl,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                MasterSocialMedium.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterSocialMediumController/Edit
        public ActionResult Edit(int id)
        {
            var data = MasterSocialMedium.Find(id);
            MasterSocialMediumViewModel masterSocialMediumViewModel = new MasterSocialMediumViewModel();
            masterSocialMediumViewModel.MasterSocialMediumId = data.MasterSocialMediumId;
            masterSocialMediumViewModel.MasterSocialMediumImageUrl = data.MasterSocialMediumImageUrl;
            masterSocialMediumViewModel.MasterSocialMediumUrl = data.MasterSocialMediumUrl;
            masterSocialMediumViewModel.CreateUser = data.CreateUser;
            masterSocialMediumViewModel.CreateDate = data.CreateDate;
            return View(masterSocialMediumViewModel);
        }

        // POST: MasterSocialMediumController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MasterSocialMediumViewModel collection)
        {
            try
            {

                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterSocialMediumFile);
                var data = new MasterSocialMedium
                {
                    MasterSocialMediumId = collection.MasterSocialMediumId,
                    MasterSocialMediumImageUrl = (ImageName != "") ? ImageName : collection.MasterSocialMediumImageUrl,
                    MasterSocialMediumUrl = collection.MasterSocialMediumUrl,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                MasterSocialMedium.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterSocialMediumController/Delete
        public ActionResult Delete(int Delete)
        {
            MasterSocialMedium.Delete(Delete, new Models.MasterSocialMedium { EditUser = User.Identity.Name, EditDate = DateTime.Now });
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
