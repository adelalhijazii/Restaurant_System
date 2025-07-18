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
    public class MasterSliderController : Controller
    {
        public IRepository<MasterSlider> MasterSlider { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Hosting { get; }
        public UserManager<AppUser> UserManager { get; }

        public MasterSliderController(IRepository<MasterSlider> _masterSlider, Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting, UserManager<AppUser> _userManager)
        {
            MasterSlider = _masterSlider;
            Hosting = _hosting;
            UserManager = _userManager;
        }

        // GET: MasterSliderController
        public ActionResult Index()
        {
            var data = MasterSlider.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = MasterSlider.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            MasterSlider.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterSliderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterSliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterSliderViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View();
                }
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterSliderFile);
                var data = new MasterSlider
                {
                    MasterSliderId = collection.MasterSliderId,
                    MasterSliderTitle = collection.MasterSliderTitle,
                    MasterSliderBreef = collection.MasterSliderBreef,
                    MasterSliderDesc = collection.MasterSliderDesc,
                    MasterSliderUrl = ImageName,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                MasterSlider.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterSliderController/Edit
        public ActionResult Edit(int id)
        {
            var data = MasterSlider.Find(id);
            MasterSliderViewModel masterSliderViewModel = new MasterSliderViewModel();
            masterSliderViewModel.MasterSliderId = data.MasterSliderId;
            masterSliderViewModel.MasterSliderTitle = data.MasterSliderTitle;
            masterSliderViewModel.MasterSliderBreef = data.MasterSliderBreef;
            masterSliderViewModel.MasterSliderDesc = data.MasterSliderDesc;
            masterSliderViewModel.MasterSliderUrl = data.MasterSliderUrl;
            masterSliderViewModel.CreateUser = data.CreateUser;
            masterSliderViewModel.CreateDate = data.CreateDate;
            return View(masterSliderViewModel);
        }

        // POST: MasterSliderController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MasterSliderViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterSliderFile);
                var data = new MasterSlider
                {
                    MasterSliderId = collection.MasterSliderId,
                    MasterSliderTitle = collection.MasterSliderTitle,
                    MasterSliderBreef = collection.MasterSliderBreef,
                    MasterSliderDesc = collection.MasterSliderDesc,
                    MasterSliderUrl = (ImageName != "") ? ImageName : collection.MasterSliderUrl,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                MasterSlider.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterSliderController/Delete
        public ActionResult Delete(int Delete)
        {
            MasterSlider.Delete(Delete, new Models.MasterSlider { EditUser = User.Identity.Name, EditDate = DateTime.Now });
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
