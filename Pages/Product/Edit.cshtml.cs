using digikala_netCore.Data;
using digikala_netCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace digikala_netCore.Pages.Product
{
    [Authorize(Roles ="Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public ProductModel Product { get; set; }

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {

            Product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (Product == null)
            {
                return NotFound(); 
            }

            return Page(); 
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == Product.Id);

            if (existingProduct == null)
            {
                return NotFound(); 
            }

            existingProduct.Name = Product.Name;
            existingProduct.Price = Product.Price;
            existingProduct.Description = Product.Description;
            existingProduct.ImageUrl = Product.ImageUrl;

            _context.SaveChanges();

            return RedirectToPage("/Dashboard/Product");
        }
    }
}
