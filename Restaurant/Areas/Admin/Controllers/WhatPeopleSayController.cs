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
    public class WhatPeopleSayController : Controller
    {
        public IRepository<WhatPeopleSay> WhatPeopleSay { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Hosting { get; }
        public UserManager<AppUser> UserManager { get; }

        public WhatPeopleSayController(IRepository<WhatPeopleSay> _whatPeopleSay, Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting, UserManager<AppUser> _userManager)
        {
            WhatPeopleSay = _whatPeopleSay;
            Hosting = _hosting;
            UserManager = _userManager;
        }
        // GET: WhatPeopleSayController
        public ActionResult Index()
        {
            var data = WhatPeopleSay.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = WhatPeopleSay.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            WhatPeopleSay.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: WhatPeopleSayController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WhatPeopleSayController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WhatPeopleSayViewModel collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View();
                }
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.WhatPeopleSayFile);

                var data = new WhatPeopleSay
                {
                    WhatPeopleSayId = collection.WhatPeopleSayId,
                    WhatPeopleSayTitle = collection.WhatPeopleSayTitle,
                    WhatPeopleSayDesc = collection.WhatPeopleSayDesc,
                    WhatPeopleSayImageUrl = ImageName,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                WhatPeopleSay.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WhatPeopleSayController/Edit
        public ActionResult Edit(int id)
        {
            var data = WhatPeopleSay.Find(id);
            WhatPeopleSayViewModel whatPeopleSayViewModel = new WhatPeopleSayViewModel();
            whatPeopleSayViewModel.WhatPeopleSayId = data.WhatPeopleSayId;
            whatPeopleSayViewModel.WhatPeopleSayTitle = data.WhatPeopleSayTitle;
            whatPeopleSayViewModel.WhatPeopleSayDesc = data.WhatPeopleSayDesc;
            whatPeopleSayViewModel.WhatPeopleSayImageUrl = data.WhatPeopleSayImageUrl;
            whatPeopleSayViewModel.CreateUser = data.CreateUser;
            whatPeopleSayViewModel.CreateDate = data.CreateDate;
            return View(whatPeopleSayViewModel);
        }

        // POST: WhatPeopleSayController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, WhatPeopleSayViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                string ImageName = UploadFile(collection.WhatPeopleSayFile);

                var data = new WhatPeopleSay
                {
                    WhatPeopleSayId = collection.WhatPeopleSayId,
                    WhatPeopleSayTitle = collection.WhatPeopleSayTitle,
                    WhatPeopleSayDesc = collection.WhatPeopleSayDesc,
                    WhatPeopleSayImageUrl = (ImageName != "") ? ImageName : collection.WhatPeopleSayImageUrl,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                WhatPeopleSay.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WhatPeopleSayController/Delete
        public ActionResult Delete(int Delete)
        {
            WhatPeopleSay.Delete(Delete, new Models.WhatPeopleSay { EditUser = User.Identity.Name, EditDate = DateTime.Now });
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
