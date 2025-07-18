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
    public class MasterWorkingHourController : Controller
    {
        public IRepository<MasterWorkingHour> MasterWorkingHour { get; }
        public UserManager<AppUser> UserManager { get; }

        public MasterWorkingHourController(IRepository<MasterWorkingHour> _masterWorkingHour, UserManager<AppUser> _userManager)
        {
            MasterWorkingHour = _masterWorkingHour;
            UserManager = _userManager;
        }
        // GET: MasterWorkingHourController
        public ActionResult Index()
        {
            var data = MasterWorkingHour.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = MasterWorkingHour.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            MasterWorkingHour.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: MasterWorkingHourController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterWorkingHourController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MasterWorkingHourViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new MasterWorkingHour
                {
                    MasterWorkingHourId = collection.MasterWorkingHourId,
                    MasterWorkingHourIdName = collection.MasterWorkingHourIdName,
                    MasterWorkingHourIdTimeFormTo = collection.MasterWorkingHourIdTimeFormTo,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                MasterWorkingHour.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterWorkingHourController/Edit
        public ActionResult Edit(int id)
        {
            var data = MasterWorkingHour.Find(id);
            MasterWorkingHourViewModel masterWorkingHourViewModel = new MasterWorkingHourViewModel();
            masterWorkingHourViewModel.MasterWorkingHourId = data.MasterWorkingHourId;
            masterWorkingHourViewModel.MasterWorkingHourIdName = data.MasterWorkingHourIdName;
            masterWorkingHourViewModel.MasterWorkingHourIdTimeFormTo = data.MasterWorkingHourIdTimeFormTo;
            masterWorkingHourViewModel.CreateUser = data.CreateUser;
            masterWorkingHourViewModel.CreateDate = data.CreateDate;
            return View(masterWorkingHourViewModel);
        }

        // POST: MasterWorkingHourController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MasterWorkingHourViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new MasterWorkingHour
                {
                    MasterWorkingHourId = collection.MasterWorkingHourId,
                    MasterWorkingHourIdName = collection.MasterWorkingHourIdName,
                    MasterWorkingHourIdTimeFormTo = collection.MasterWorkingHourIdTimeFormTo,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                MasterWorkingHour.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterWorkingHourController/Delete
        public ActionResult Delete(int Delete)
        {
            MasterWorkingHour.Delete(Delete, new Models.MasterWorkingHour { EditUser = User.Identity.Name, EditDate = DateTime.Now });
            return RedirectToAction(nameof(Index));
        }
    }
}
