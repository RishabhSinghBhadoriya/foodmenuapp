using Menu.Data;
using foodmenuapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Menu.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuContext _context;

        public MenuController(MenuContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var dishes = from d in _context.Dishes
                         select d;
            if (!string.IsNullOrEmpty(searchString))
            {
                dishes = dishes.Where(d => d.Name.Contains(searchString));
                return View(await dishes.ToListAsync());
            }
            return View(await dishes.ToListAsync());
        }

        [HttpGet]
        public IActionResult AddDish()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddToPage(string dishName, string imageUrl, double price)
        {
            var newDish = new Dish
            {
                Name = dishName,
                Price = price,
                ImageUrl = imageUrl
            };
            // Add the new dish to the database
            _context.Dishes.Add(newDish);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Redirect to the Index action after adding the dish
        }

        [Route("Details/")]
        public async Task<IActionResult> Details(int? id)
        {
            var dish = await _context.Dishes
                .Include(di => di.DishIngredient)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }
    }
}
