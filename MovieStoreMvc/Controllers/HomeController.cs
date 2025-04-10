using Microsoft.AspNetCore.Mvc;
using BookStoreMvc.Repositories.Abstract;

namespace BookStoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _BookService;
        public HomeController(IBookService BookService)
        {
            _BookService = BookService;
        }
        public IActionResult Index(string term="", int currentPage = 1)
        {
            var Books = _BookService.List(term,true,currentPage);
            return View(Books);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult BookDetail(int BookId)
        {
            var Book = _BookService.GetById(BookId);
            return View(Book);
        }

    }
}
