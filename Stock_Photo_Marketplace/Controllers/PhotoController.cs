using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_Photo_Marketplace.Data; // Adjust the namespace based on your project
using Stock_Photo_Marketplace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stock_Photo_Marketplace.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhotoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch photos including their categories
            var photos = await _context.Photos.Include(p => p.Category).ToListAsync();
            return View(photos);
        }
    }
}
