using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.GetAll();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.categories.Find(id);
            var categoryFromFirst = _db.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromSingle = _db.categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.categories.Find(id);
            var categoryFromFirst = _db.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromSingle = _db.categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromFirst);
        }

        //POST
        [HttpPost]
        //[HttpPost,ActionName("Delete")] eta korle DeletePOST er nam(2 jayga tei jemon view and controller page a) only Delete korleo kaj korbe.
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");



        }
    }
}
