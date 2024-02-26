using Booky.Data;
using Booky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Booky.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Book> books = _context.Books.Include("Category").ToList();
            return View(books);
        }
        //CREATE
        //==========================================================================================
        public IActionResult Create()
        {
            BookVM bookVM = new BookVM()
            {
                Categories = _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
            };


            return View(bookVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookVM bookVM)
        {



            var book = new Book()
            {
                Author = bookVM.Author,
                Title = bookVM.Title,
                Description = bookVM.Description,
                CategoryId = bookVM.CategoryId,


            };
            _context.Books.Add(book);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        //EDIT
        //==========================================================================================
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var book = _context.Books.FirstOrDefault(b => b.Id == Id);
            if (book == null)
            {
                return NotFound();
            }

            var categories = _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            var bookVM = new BookVM
            {
                Id = book.Id,
                Author = book.Author,
                Title = book.Title,
                Description = book.Description,
                CategoryId = book.CategoryId,
                Categories = categories
            };

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookVM bookvm)
        {
            if (!ModelState.IsValid)
            {
                // يوجد بيانات غير صحيحة، قم بإعادة عرض النموذج
                bookvm.Categories = _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
                return View(bookvm);
            }

            var book = _context.Books.FirstOrDefault(b => b.Id == bookvm.Id);
            if (book == null)
            {
                return NotFound();
            }

            // تحديث الكتاب بالبيانات المدخلة
            book.Author = bookvm.Author;
            book.Title = bookvm.Title;
            book.Description = bookvm.Description;
            book.CategoryId = bookvm.CategoryId;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookVM = new BookVM()
            {
                Author = book.Author,
                Title = book.Title,
                Description = book.Description,
                CategoryId = book.CategoryId,
                Categories = _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, }).ToList(),

            };
            
            return View( bookVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       public IActionResult Delete(BookVM bookvm)
        {
            var book = _context.Books.Find(bookvm.Id);
            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
