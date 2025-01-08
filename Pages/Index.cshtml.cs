using digikala_netCore.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using digikala_netCore.Models;

namespace digikala_netCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ProductModel> Products { get; set; }

        public void OnGet()
        {
            Products = _context.Products.ToList();
        }
    }
}
