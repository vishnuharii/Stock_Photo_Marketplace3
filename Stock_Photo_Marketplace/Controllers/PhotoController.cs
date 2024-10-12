using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock_Photo_Marketplace.Models;
using Stock_Photo_Marketplace.Data; // Add this using directive

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Stock_Photo_Marketplace.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stock_Photo_Marketplace.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace Stock_Photo_Marketplace.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ApplicationDbContext _context; 
        private readonly IWebHostEnvironment _webHostEnvironment;

        //Constructor used for Initialisation
        public PhotoController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IWebHostEnvironment _env)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        //Action for Displaying Photo's in Seller's Page
        [HttpGet]
        public async Task<IActionResult> SellIndex()
        {
            var photos = await _context.Photos.Include(p => p.Category).ToListAsync();
            return View(photos);
        }

        //Action for Displaying photo's in Buyer's PhotoGallery
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var photos = await _context.Photos.Include(p => p.Category).ToListAsync();
            return View(photos);
        }

        // Action for taking category values from db to the dropdown in form
        [HttpGet]
        public IActionResult Sell()
        {
            var viewModel = new PhotoViewModel
            {
                Categories = _context.Categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryID.ToString(),
                    Text = c.CategoryName
                }).ToList()
            };
            return View(viewModel);
        }

        //Action for Creating/Uplaoding new Photo to the market
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sell(PhotoViewModel model)
        {
            string filePath = null;

            // Handle file upload
            if (model.PhotoFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PhotoFile.FileName;
                filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PhotoFile.CopyTo(fileStream);
                }

                filePath = "/images/" + uniqueFileName;
            }

            var photo = new Photo
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                UploadDate = DateTime.Now,
                CategoryID = model.CategoryID,
                FilePath = filePath
            };

            _context.Photos.Add(photo);
            _context.SaveChanges();

            // Set TempData to indicate success
            TempData["SuccessMessage"] = "Photo uploaded successfully!";

            return RedirectToAction(nameof(Index));
        }

        //Action for deleting the photo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo != null)
            {
                _context.Photos.Remove(photo); 
                await _context.SaveChangesAsync(); 
            }
            return RedirectToAction(nameof(Index));  
        }
        
        //Action for getting the details of photo we selected
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Fetch the photo by id from the database
            var photo = _context.Photos.Find(id);

            if (photo == null)
            {
                return NotFound();
            }

            var viewModel = new PhotoViewModel
            {
                PhotoID = photo.PhotoID,
                Title = photo.Title,
                Description = photo.Description,
                Price = photo.Price,
                CategoryID = photo.CategoryID,
                FilePath = photo.FilePath,  // For showing the current photo
                Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName")
            };

            return View(viewModel);  // Return the Edit.cshtml view
        }

        //Action for submitting the changes to db
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PhotoViewModel viewModel)
        {
            if (true)
            {
                // Find the photo by ID
                var photo = await _context.Photos.FindAsync(viewModel.PhotoID);

                if (photo == null)
                {
                    return NotFound();
                }

                // Update the photo properties with the values from the form
                photo.Title = viewModel.Title;
                photo.Description = viewModel.Description;
                photo.Price = viewModel.Price;
                photo.CategoryID = viewModel.CategoryID;

                // If a new photo file was uploaded, update the FilePath
                if (viewModel.PhotoFile != null)
                {
                    var fileName = Path.GetFileName(viewModel.PhotoFile.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.PhotoFile.CopyToAsync(stream);
                    }

                    photo.FilePath = "/images/" + fileName; // Update the file path in the database
                }

                // Save the changes to the database
                _context.Update(photo);
                await _context.SaveChangesAsync();

                // set a success message in TempData
                TempData["SuccessMessage"] = "Photo updated successfully!";

                return RedirectToAction(nameof(Index)); // Redirect to the gallery or homepage
            }

            // If we reach this point, something went wrong
            viewModel.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName", viewModel.CategoryID);
            return View(viewModel);
        }       

    }
}
