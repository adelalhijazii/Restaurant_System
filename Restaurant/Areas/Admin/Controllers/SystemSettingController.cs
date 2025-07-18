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
    public class SystemSettingController : Controller
    {
        public IRepository<SystemSetting> SystemSetting { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Hosting { get; }
        public UserManager<AppUser> UserManager { get; }

        public SystemSettingController(IRepository<SystemSetting> _systemSetting, Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting, UserManager<AppUser> _userManager)
        {
            SystemSetting = _systemSetting;
            Hosting = _hosting;
            UserManager = _userManager;
        }
        // GET: SystemSettingController
        public ActionResult Index()
        {
            var data = SystemSetting.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = SystemSetting.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            SystemSetting.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: SystemSettingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SystemSettingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SystemSettingViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Error Data Entry..!");
                    return View();
                }
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName1 = UploadFile(collection.SystemSettingFile1);
                string ImageName2 = UploadFile(collection.SystemSettingFile2);
                string ImageName3 = UploadFile(collection.SystemSettingFile3);
                var data = new SystemSetting
                {
                    SystemSettingId = collection.SystemSettingId,
                    SystemSettingLogoImageUrl = ImageName1,
                    SystemSettingLogoImageUrl2 = ImageName2,
                    SystemSettingCopyright = collection.SystemSettingCopyright,
                    SystemSettingWelcomeNoteTitle = collection.SystemSettingWelcomeNoteTitle,
                    SystemSettingWelcomeNoteBreef = collection.SystemSettingWelcomeNoteBreef,
                    SystemSettingWelcomeNoteDesc = collection.SystemSettingWelcomeNoteDesc,
                    SystemSettingWelcomeNoteUrl = collection.SystemSettingWelcomeNoteUrl,
                    SystemSettingWelcomeNoteImageUrl = ImageName3,
                    SystemSettingMapLocation = collection.SystemSettingMapLocation,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                SystemSetting.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SystemSettingController/Edit
        public ActionResult Edit(int id)
        {
            var data = SystemSetting.Find(id);
            SystemSettingViewModel systemSettingViewModel = new SystemSettingViewModel();
            systemSettingViewModel.SystemSettingId = data.SystemSettingId;
            systemSettingViewModel.SystemSettingLogoImageUrl = data.SystemSettingLogoImageUrl;
            systemSettingViewModel.SystemSettingLogoImageUrl2 = data.SystemSettingLogoImageUrl2;
            systemSettingViewModel.SystemSettingCopyright = data.SystemSettingCopyright;
            systemSettingViewModel.SystemSettingWelcomeNoteTitle = data.SystemSettingWelcomeNoteTitle;
            systemSettingViewModel.SystemSettingWelcomeNoteBreef = data.SystemSettingWelcomeNoteBreef;
            systemSettingViewModel.SystemSettingWelcomeNoteDesc = data.SystemSettingWelcomeNoteDesc;
            systemSettingViewModel.SystemSettingWelcomeNoteUrl = data.SystemSettingWelcomeNoteUrl;
            systemSettingViewModel.SystemSettingWelcomeNoteImageUrl = data.SystemSettingWelcomeNoteImageUrl;
            systemSettingViewModel.SystemSettingMapLocation = data.SystemSettingMapLocation;
            systemSettingViewModel.CreateUser = data.CreateUser;
            systemSettingViewModel.CreateDate = data.CreateDate;
            return View(systemSettingViewModel);
        }

        // POST: SystemSettingController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SystemSettingViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName1 = UploadFile(collection.SystemSettingFile1);
                string ImageName2 = UploadFile(collection.SystemSettingFile2);
                string ImageName3 = UploadFile(collection.SystemSettingFile3);
                var data = new SystemSetting
                {
                    SystemSettingId = collection.SystemSettingId,
                    SystemSettingLogoImageUrl = (ImageName1 != "") ? ImageName1 : collection.SystemSettingLogoImageUrl,
                    SystemSettingLogoImageUrl2 = (ImageName2 != "") ? ImageName2 : collection.SystemSettingLogoImageUrl2,
                    SystemSettingCopyright = collection.SystemSettingCopyright,
                    SystemSettingWelcomeNoteTitle = collection.SystemSettingWelcomeNoteTitle,
                    SystemSettingWelcomeNoteBreef = collection.SystemSettingWelcomeNoteBreef,
                    SystemSettingWelcomeNoteDesc = collection.SystemSettingWelcomeNoteDesc,
                    SystemSettingWelcomeNoteUrl = collection.SystemSettingWelcomeNoteUrl,
                    SystemSettingWelcomeNoteImageUrl = (ImageName3 != "") ? ImageName3 : collection.SystemSettingWelcomeNoteImageUrl,
                    SystemSettingMapLocation = collection.SystemSettingMapLocation,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                SystemSetting.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SystemSettingController/Delete
        public ActionResult Delete(int Delete)
        {
            SystemSetting.Delete(Delete, new Models.SystemSetting { EditUser = User.Identity.Name, EditDate = DateTime.Now });
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
