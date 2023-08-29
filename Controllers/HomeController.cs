using BlogWebApp.Data;
using BlogWebApp.Models;
using BlogWebApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly AppsDbContext _db;
        private readonly IRepository _repo;
        public HomeController(ILogger<HomeController> logger,IRepository repository)
        {
            _logger = logger;
          
            _repo = repository;
        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return View(new Post());
            }
            else {
            var post= _repo.GetPost((int)(id));
            return View(post);
                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.Id > 0)
            {
                _repo.UpdatePost(post);
            }
            else
            {
                _repo.AddPost(post);
            }
            if (await _repo.SaveChangesAsync())
            {

                return RedirectToAction("Index");
            }
            else
            {
                return View(post);
            }
        }





        public async Task<IActionResult> Delete(int id)
        {
            _repo.DeletePost(id);

            if (await _repo.SaveChangesAsync())
            {


              return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
           

        }


        public IActionResult Privacy()
        {
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}