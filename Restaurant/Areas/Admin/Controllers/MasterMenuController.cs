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
    public class MasterMenuController : Controller
    {
        public IRepository<MasterMenu> MasterMenu { get; }
        public UserManager<AppUser> UserManager { get; }

        public MasterMenuController(IRepository<MasterMenu> _masterMenu, UserManager<AppUser> _userManager)
        {
            MasterMenu = _masterMenu;
            UserManager = _userManager;
        }
        // GET: MasterMenuController
        public ActionResult Index()
        {
            var data = MasterMenu.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = MasterMenu.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            MasterMenu.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterMenuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterMenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterMenuViewmodel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new MasterMenu
                {
                    MasterMenuId = collection.MasterMenuId,
                    MasterMenuName = collection.MasterMenuName,
                    MasterMenuUrl = collection.MasterMenuUrl,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                MasterMenu.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterMenuController/Edit
        public ActionResult Edit(int id)
        {
            var data = MasterMenu.Find(id);
            MasterMenuViewmodel masterMenuViewModel = new MasterMenuViewmodel();
            masterMenuViewModel.MasterMenuId = data.MasterMenuId;
            masterMenuViewModel.MasterMenuName = data.MasterMenuName;
            masterMenuViewModel.MasterMenuUrl = data.MasterMenuUrl;
            masterMenuViewModel.CreateUser = data.CreateUser;
            masterMenuViewModel.CreateDate = data.CreateDate;
            return View(masterMenuViewModel);
        }

        // POST: MasterMenuController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MasterMenuViewmodel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new MasterMenu
                {
                    MasterMenuId = collection.MasterMenuId,
                    MasterMenuName = collection.MasterMenuName,
                    MasterMenuUrl = collection.MasterMenuUrl,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                MasterMenu.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterMenuController/Delete
        public ActionResult Delete(int Delete)
        {
            MasterMenu.Delete(Delete, new Models.MasterMenu { EditUser = User.Identity.Name, EditDate = DateTime.Now });
            return RedirectToAction(nameof(Index));
        }
    }
}
