using digikala_netCore.Data;
using digikala_netCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.Intrinsics.X86;
using System;

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

            // Save product details to database (mocked here)
            // SaveToDatabase(Name, Description, Price, "/uploads/" + Image.FileName);
    
            var product = new Product
            {
                Name = Name,
                Description = Description,
                Price = Price,
                ImageUrl = "/uploads/" + Image.FileName,
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Product");
        }

        ErrorMessage = "Image upload failed. Please try again.";
        return Page();
    }
}
