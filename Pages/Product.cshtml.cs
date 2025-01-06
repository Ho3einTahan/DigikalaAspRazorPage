using Microsoft.AspNetCore.Mvc.RazorPages;
using digikala_netCore.Data;
using digikala_netCore.Models;

public class ProductModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ProductModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Product> Products { get; set; }

    public void OnGet()
    {
        // Example for retrieving data from the database
        Products = _context.Products.ToList();
    }
}
