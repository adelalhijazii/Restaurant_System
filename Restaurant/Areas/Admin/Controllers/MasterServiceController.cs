using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Areas.Admin.ViewModels;
using Restaurant.Models;
using Restaurant.Models.Repository;


namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MasterServiceController : Controller
    {
        public IRepository<MasterService> MasterService { get; }

        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Hosting { get; }
        public UserManager<AppUser> UserManager { get; }

        public MasterServiceController(IRepository<MasterService> _masterService, Microsoft.AspNetCore.Hosting.IHostingEnvironment   _hosting, UserManager<AppUser> _userManager)
        {
            MasterService = _masterService;
            Hosting = _hosting;
            UserManager = _userManager;
        }
        // GET: MasterServiceController
        public ActionResult Index()
        {
            var data = MasterService.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = MasterService.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            MasterService.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterServiceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterServiceViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View();
                }
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterServiceFile);

                var data = new MasterService
                {
                    MasterServiceId = collection.MasterServiceId,
                    MasterServiceTitle = collection.MasterServiceTitle,
                    MasterServiceDesc = collection.MasterServiceDesc,
                    MasterServiceImage = ImageName,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                MasterService.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterServiceController/Edit
        public ActionResult Edit(int id)
        {
            var data = MasterService.Find(id);
            MasterServiceViewModel masterServiceViewModel = new MasterServiceViewModel();
            masterServiceViewModel.MasterServiceId = data.MasterServiceId;
            masterServiceViewModel.MasterServiceTitle = data.MasterServiceTitle;
            masterServiceViewModel.MasterServiceDesc = data.MasterServiceDesc;
            masterServiceViewModel.MasterServiceImage = data.MasterServiceImage;
            masterServiceViewModel.CreateUser = data.CreateUser;
            masterServiceViewModel.CreateDate = data.CreateDate;
            return View(masterServiceViewModel);
        }

        // POST: MasterServiceController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MasterServiceViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterServiceFile);

                var data = new MasterService
                {
                    MasterServiceId = collection.MasterServiceId,
                    MasterServiceTitle = collection.MasterServiceTitle,
                    MasterServiceDesc = collection.MasterServiceDesc,
                    MasterServiceImage = (ImageName != "") ? ImageName : collection.MasterServiceImage,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                MasterService.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterServiceController/Delete
        public ActionResult Delete(int Delete)
        {
            MasterService.Delete(Delete, new Models.MasterService { EditUser = User.Identity.Name, EditDate = DateTime.Now });
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
