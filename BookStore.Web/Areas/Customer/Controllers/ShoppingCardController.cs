using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCardController : Controller
    {
        // GET: ShoppingCard
        public ActionResult Index()
        {
            return View();
        }

        // GET: ShoppingCard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingCard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingCard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingCard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
