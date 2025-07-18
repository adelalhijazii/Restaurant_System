using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Areas.Admin.ViewModels;
using Restaurant.Models;
using Restaurant.Models.Repository;
using System.Security.Claims;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MasterItemMenuController : Controller
    {
        private readonly UserManager<AppUser> UserManager;

        public IRepository<MasterItemMenu> MasterItemMenu { get; }
        public IRepository<MasterCategoryMenu> MasterCategoryMenu { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Hosting { get; }

        public MasterItemMenuController(IRepository<MasterItemMenu> _masterItemMenu, IRepository<MasterCategoryMenu> _masterCategoryMenu, Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting, UserManager<AppUser> _userManager)
        {
            MasterItemMenu = _masterItemMenu;
            MasterCategoryMenu = _masterCategoryMenu;
            Hosting = _hosting;
            UserManager = _userManager;
        }
        //GET: MasterItemMenuController
        public ActionResult Index()
        {
            var data = MasterItemMenu.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = MasterItemMenu.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            MasterItemMenu.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterItemMenuController/Create
        public ActionResult Create()
        {
            var data = MasterCategoryMenu.View();
            ViewBag.category = data;
            return View();
        }

        // POST: MasterItemMenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterItemMenuViewmodel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Error Data Entry..!");
                    return View();
                }
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterItemMenuFile);
                var data = new MasterItemMenu
                {
                    MasterItemMenuId = collection.MasterItemMenuId,
                    MasterCategoryMenuId = collection.MasterCategoryMenuId,
                    MasterItemMenuTitle = collection.MasterItemMenuTitle,
                    MasterItemMenuBreef = collection.MasterItemMenuBreef,
                    MasterItemMenuDesc = collection.MasterItemMenuDesc,
                    MasterItemMenuPrice = collection.MasterItemMenuPrice,
                    MasterItemMenuImageUrl = ImageName,
                    MasterItemMenuDate = collection.MasterItemMenuDate,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                MasterItemMenu.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterItemMenuController/Edit
        public ActionResult Edit(int id)
        {
            var data = MasterItemMenu.Find(id);
            var data2 = MasterCategoryMenu.View();
            ViewBag.category = data2;
            MasterItemMenuViewmodel masterItemMenuViewModel = new MasterItemMenuViewmodel();
            masterItemMenuViewModel.MasterItemMenuId = data.MasterItemMenuId;
            masterItemMenuViewModel.MasterCategoryMenuId = data.MasterCategoryMenuId;
            masterItemMenuViewModel.MasterCategoryMenu = MasterCategoryMenu.Find(data.MasterCategoryMenuId);
            masterItemMenuViewModel.MasterItemMenuTitle = data.MasterItemMenuTitle;
            masterItemMenuViewModel.MasterItemMenuBreef = data.MasterItemMenuBreef;
            masterItemMenuViewModel.MasterItemMenuDesc = data.MasterItemMenuDesc;
            masterItemMenuViewModel.MasterItemMenuPrice = data.MasterItemMenuPrice;
            masterItemMenuViewModel.MasterItemMenuImageUrl = data.MasterItemMenuImageUrl;
            masterItemMenuViewModel.MasterItemMenuDate = data.MasterItemMenuDate;
            masterItemMenuViewModel.CreateUser= data.CreateUser;
            masterItemMenuViewModel.CreateDate= data.CreateDate;
            return View(masterItemMenuViewModel);
        }

        // POST: MasterItemMenuController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MasterItemMenuViewmodel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterItemMenuFile);

                var data = new MasterItemMenu
                {
                    MasterItemMenuId = collection.MasterItemMenuId,
                    MasterCategoryMenuId = collection.MasterCategoryMenuId,
                    MasterItemMenuTitle = collection.MasterItemMenuTitle,
                    MasterItemMenuBreef = collection.MasterItemMenuBreef,
                    MasterItemMenuDesc = collection.MasterItemMenuDesc,
                    MasterItemMenuPrice = collection.MasterItemMenuPrice,
                    MasterItemMenuImageUrl = (ImageName != "") ? ImageName : collection.MasterItemMenuImageUrl,
                    MasterItemMenuDate = collection.MasterItemMenuDate,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,

                };
                MasterItemMenu.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterItemMenuController/Delete
        public ActionResult Delete(int Delete)
        {
            MasterItemMenu.Delete(Delete, new Models.MasterItemMenu { EditUser = User.Identity.Name, EditDate = DateTime.Now });
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
