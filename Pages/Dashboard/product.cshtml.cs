using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using digikala_netCore.Data;
using digikala_netCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace digikala_netCore.Pages.Product
{
    [Authorize(Roles ="Admin")]
    public class ProductManageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProductManageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ProductModel> Products { get; set; }

        public void OnGet()
        {
            Products = _context.Products.ToList();
        }

        public IActionResult OnPostDelete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToPage("/Product/Manage");
        }
    }
}
