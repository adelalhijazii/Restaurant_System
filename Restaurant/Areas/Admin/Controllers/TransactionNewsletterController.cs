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
    public class TransactionNewsletterController : Controller
    {
        public IRepository<TransactionNewsletter> TransactionNewsletter { get; }
        public UserManager<AppUser> UserManager { get; }

        public TransactionNewsletterController(IRepository<TransactionNewsletter> _transactionNewsletter, UserManager<AppUser> _userManager)
        {
            TransactionNewsletter = _transactionNewsletter;
            UserManager = _userManager;
        }
        // GET: TransactionNewsletterController
        public ActionResult Index()
        {
            var data = TransactionNewsletter.View();
            return View(data);
        }

        // GET: TransactionNewsletterController/Edit
        public ActionResult Edit(int id)
        {
            var data = TransactionNewsletter.Find(id);
            TransactionNewsletterViewModel transactionNewsletterViewModel = new TransactionNewsletterViewModel();
            transactionNewsletterViewModel.TransactionNewsletterId = data.TransactionNewsletterId;
            transactionNewsletterViewModel.TransactionNewsletterEmail = data.TransactionNewsletterEmail;
            transactionNewsletterViewModel.CreateUser = data.CreateUser;
            transactionNewsletterViewModel.CreateDate = data.CreateDate;
            return View(transactionNewsletterViewModel);
        }

        // POST: TransactionNewsletterController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TransactionNewsletterViewModel collection)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var data = new TransactionNewsletter
                {
                    TransactionNewsletterId = collection.TransactionNewsletterId,
                    TransactionNewsletterEmail = collection.TransactionNewsletterEmail,
                    CreateUser = collection.CreateUser,
                    CreateDate = collection.CreateDate,
                    EditUser = user.Id,
                    EditDate = DateTime.Now,
                };
                TransactionNewsletter.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionNewsletterController/Delete
        public ActionResult Delete(int Delete)
        {
            TransactionNewsletter.Delete(Delete, new Models.TransactionNewsletter { EditUser = User.Identity.Name, EditDate = DateTime.Now });
            return RedirectToAction(nameof(Index));
        }
    }
}
