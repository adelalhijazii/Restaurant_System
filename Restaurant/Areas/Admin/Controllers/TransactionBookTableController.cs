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
    public class TransactionBookTableController : Controller
    {
        public IRepository<TransactionBookTable> TransactionBookTable { get; }
        public UserManager<AppUser> UserManager { get; }

        public TransactionBookTableController(IRepository<TransactionBookTable> _transactionBookTable, UserManager<AppUser> _userManager)
        {
            TransactionBookTable = _transactionBookTable;
            UserManager = _userManager;
        }
        // GET: TransactionBookTableController
        public ActionResult Index()
        {
            var data = TransactionBookTable.View();
            return View(data);
        }

        public ActionResult Active(int id)
        {
            var data = TransactionBookTable.Find(id);
            data.EditDate = DateTime.Now;
            data.EditUser = User.Identity.Name;
            TransactionBookTable.Active(id, data);
            return RedirectToAction(nameof(Index));
        }

        // GET: TransactionBookTableController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionBookTableController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TransactionBookTableViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new TransactionBookTable
                {
                    TransactionBookTableId = collection.TransactionBookTableId,
                    TransactionBookTableFullName = collection.TransactionBookTableFullName,
                    TransactionBookTableEmail = collection.TransactionBookTableEmail,
                    TransactionBookTableMobileNumber = collection.TransactionBookTableMobileNumber,
                    TransactionBookTableDate = collection.TransactionBookTableDate,
                    CreateUser = user.Id,
                    CreateDate = DateTime.Now,
                    IsActive = true
                };
                TransactionBookTable.Add(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionBookTableController/Edit
        public ActionResult Edit(int id)
        {
            var data = TransactionBookTable.Find(id);
            TransactionBookTableViewModel transactionBookTableViewModel = new TransactionBookTableViewModel();
            transactionBookTableViewModel.TransactionBookTableId = data.TransactionBookTableId;
            transactionBookTableViewModel.TransactionBookTableFullName = data.TransactionBookTableFullName;
            transactionBookTableViewModel.TransactionBookTableEmail = data.TransactionBookTableEmail;
            transactionBookTableViewModel.TransactionBookTableMobileNumber = data.TransactionBookTableMobileNumber;
            transactionBookTableViewModel.TransactionBookTableDate = data.TransactionBookTableDate;
            transactionBookTableViewModel.CreateUser = data.CreateUser;
            transactionBookTableViewModel.CreateDate = data.CreateDate;
            return View(transactionBookTableViewModel);
        }

        // POST: TransactionBookTableController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TransactionBookTableViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new TransactionBookTable
                {
                    TransactionBookTableId = collection.TransactionBookTableId,
                    TransactionBookTableFullName = collection.TransactionBookTableFullName,
                    TransactionBookTableEmail = collection.TransactionBookTableEmail,
                    TransactionBookTableMobileNumber = collection.TransactionBookTableMobileNumber,
                    TransactionBookTableDate = collection.TransactionBookTableDate,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                TransactionBookTable.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionBookTableController/Delete
        public ActionResult Delete(int Delete)
        {
            TransactionBookTable.Delete(Delete, new Models.TransactionBookTable { EditUser = User.Identity.Name, EditDate = DateTime.Now });
            return RedirectToAction(nameof(Index));
        }
    }
}
