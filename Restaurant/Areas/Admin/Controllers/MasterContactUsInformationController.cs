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
    public class MasterContactUsInformationController : Controller
    {

        public IRepository<MasterContactUsInformation> MasterContactUsInformation { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Hosting { get; }
        public UserManager<AppUser> UserManager { get; }

        public MasterContactUsInformationController(IRepository<MasterContactUsInformation> _masterContactUsInformation, Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting, UserManager<AppUser> _userManager)
        {
            MasterContactUsInformation = _masterContactUsInformation;
            Hosting = _hosting;
            UserManager = _userManager;
        }
        // GET: MasterContactUsInformationController
        public ActionResult Index()
        {
            var data = MasterContactUsInformation.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = MasterContactUsInformation.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            MasterContactUsInformation.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterContactUsInformationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterContactUsInformationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterContactUsInformationViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View();
                }
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterContactUsInformationFile);

                var data = new MasterContactUsInformation
                {
                    MasterContactUsInformationId = collection.MasterContactUsInformationId,
                    MasterContactUsInformationIdesc = collection.MasterContactUsInformationIdesc,
                    MasterContactUsInformationImageUrl = ImageName,
                    MasterContactUsInformationRedirect = collection.MasterContactUsInformationRedirect,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                MasterContactUsInformation.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterContactUsInformationController/Edit
        public ActionResult Edit(int id)
        {
            var data = MasterContactUsInformation.Find(id);
            MasterContactUsInformationViewModel masterContactUsInformationViewModel = new MasterContactUsInformationViewModel();
            masterContactUsInformationViewModel.MasterContactUsInformationId = data.MasterContactUsInformationId;
            masterContactUsInformationViewModel.MasterContactUsInformationIdesc = data.MasterContactUsInformationIdesc;
            masterContactUsInformationViewModel.MasterContactUsInformationImageUrl = data.MasterContactUsInformationImageUrl;
            masterContactUsInformationViewModel.MasterContactUsInformationRedirect = data.MasterContactUsInformationRedirect;
            masterContactUsInformationViewModel.CreateUser = data.CreateUser;
            masterContactUsInformationViewModel.CreateDate = data.CreateDate;
            return View(masterContactUsInformationViewModel);
        }

        // POST: MasterContactUsInformationController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MasterContactUsInformationViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.MasterContactUsInformationFile);

                var data = new MasterContactUsInformation
                {
                    MasterContactUsInformationId = collection.MasterContactUsInformationId,
                    MasterContactUsInformationIdesc = collection.MasterContactUsInformationIdesc,
                    MasterContactUsInformationImageUrl = (ImageName != "") ? ImageName : collection.MasterContactUsInformationImageUrl,
                    MasterContactUsInformationRedirect = collection.MasterContactUsInformationRedirect,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                MasterContactUsInformation.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterContactUsInformationController/Delete
        public ActionResult Delete(int Delete)
        {
            MasterContactUsInformation.Delete(Delete, new Models.MasterContactUsInformation { EditUser = User.Identity.Name, EditDate = DateTime.Now });
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
