using digikala_netCore.Data;
using digikala_netCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace digikala_netCore.Pages.Product
{
    [Authorize(Roles ="Admin")]
    public class CreateProductModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public CreateProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public decimal Price { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please correct the errors in the form.";
                return Page();
            }

            if (Image != null && Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                // Create folder if it doesn't exist
                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, Image.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                var product = new ProductModel
                {
                    Name = Name,
                    Description = Description,
                    Price = Price,
                    ImageUrl = "/uploads/" + Image.FileName,
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return RedirectToPage("/Index");
            }

            ErrorMessage = "Image upload failed. Please try again.";
            return Page();
        }
    }

}
