using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Restaurant.Areas.Admin.ViewModels;
using Restaurant.Models;
using Restaurant.Models.Repository;
using System.Security.Claims;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MasterCategoryMenuController : Controller
    {
        public IRepository<MasterCategoryMenu> MasterCategoryMenu { get; }
        public UserManager<AppUser> UserManager { get; }

        public MasterCategoryMenuController(IRepository<MasterCategoryMenu> _masterCategoryMenu, UserManager<AppUser> _userManager)
        {
            MasterCategoryMenu = _masterCategoryMenu;
            UserManager = _userManager;
        }
        // GET: MasterCategoryMenuController
        public ActionResult Index()
        {
            var data = MasterCategoryMenu.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = MasterCategoryMenu.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            MasterCategoryMenu.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterCategoryMenuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterCategoryMenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterCategoryMenuViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new MasterCategoryMenu
                {
                    MasterCategoryMenuId = collection.MasterCategoryMenuId,
                    MasterCategoryMenuName = collection.MasterCategoryMenuName,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                MasterCategoryMenu.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterCategoryMenuController/Edit
        public ActionResult Edit(int id)
        {
            var data = MasterCategoryMenu.Find(id);
            MasterCategoryMenuViewModel masterCategoryMenuViewModel = new MasterCategoryMenuViewModel();
            masterCategoryMenuViewModel.MasterCategoryMenuId = data.MasterCategoryMenuId;
            masterCategoryMenuViewModel.MasterCategoryMenuName = data.MasterCategoryMenuName;
            masterCategoryMenuViewModel.CreateUser = data.CreateUser;
            masterCategoryMenuViewModel.CreateDate = data.CreateDate;
            return View(masterCategoryMenuViewModel);
        }

        // POST: MasterCategoryMenuController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MasterCategoryMenuViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new MasterCategoryMenu
                {
                    MasterCategoryMenuId = collection.MasterCategoryMenuId,
                    MasterCategoryMenuName = collection.MasterCategoryMenuName,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                    IsActive = true
                };
                MasterCategoryMenu.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterCategoryMenuController/Delete
        public ActionResult Delete(int Delete)
        {
            MasterCategoryMenu.Delete(Delete, new Models.MasterCategoryMenu { EditUser = User.Identity.Name, EditDate = DateTime.Now });
            return RedirectToAction(nameof(Index));
        }
    }
}
