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
    public class TransactionContactUController : Controller
    {
        public IRepository<TransactionContactU> TransactionContactU { get; }
        public UserManager<AppUser> UserManager { get; }

        public TransactionContactUController(IRepository<TransactionContactU> _transactionContactU, UserManager<AppUser> _userManager)
        {
            TransactionContactU = _transactionContactU;
            UserManager = _userManager;
        }
        // GET: TransactionContactUController
        public ActionResult Index()
        {
            var data = TransactionContactU.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = TransactionContactU.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            TransactionContactU.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: TransactionContactUController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionContactUController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TransactionContactUViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new TransactionContactU
                {
                    TransactionContactUId = collection.TransactionContactUId,
                    TransactionContactUFullName = collection.TransactionContactUFullName,
                    TransactionContactUEmail = collection.TransactionContactUEmail,
                    TransactionContactUSubject = collection.TransactionContactUSubject,
                    TransactionContactUMessage = collection.TransactionContactUMessage,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                TransactionContactU.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionContactUController/Edit
        public ActionResult Edit(int id)
        {
            var data = TransactionContactU.Find(id);
            TransactionContactUViewModel transactionContactUViewModel = new TransactionContactUViewModel();
            transactionContactUViewModel.TransactionContactUId = data.TransactionContactUId;
            transactionContactUViewModel.TransactionContactUFullName = data.TransactionContactUFullName;
            transactionContactUViewModel.TransactionContactUEmail = data.TransactionContactUEmail;
            transactionContactUViewModel.TransactionContactUSubject = data.TransactionContactUSubject;
            transactionContactUViewModel.TransactionContactUMessage = data.TransactionContactUMessage;
            transactionContactUViewModel.CreateUser = data.CreateUser;
            transactionContactUViewModel.CreateDate = data.CreateDate;
            return View(transactionContactUViewModel);
        }

        // POST: TransactionContactUController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TransactionContactUViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new TransactionContactU
                {
                    TransactionContactUId = collection.TransactionContactUId,
                    TransactionContactUFullName = collection.TransactionContactUFullName,
                    TransactionContactUEmail = collection.TransactionContactUEmail,
                    TransactionContactUSubject = collection.TransactionContactUSubject,
                    TransactionContactUMessage = collection.TransactionContactUMessage,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                TransactionContactU.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionContactUController/Delete
        public ActionResult Delete(int Delete)
        {
            TransactionContactU.Delete(Delete, new Models.TransactionContactU { EditUser = User.Identity.Name, EditDate = DateTime.Now });
            return RedirectToAction(nameof(Index));
        }
    }
}
