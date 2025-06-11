using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Data;

namespace MetroLayag.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public int TotalBooked { get; set; }
        public int TotalDisembarked { get; set; }
        public int TotalCanceled { get; set; }


        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToPage("/Login");
            }

            TotalBooked = _context.Passengers.Count(p => !p.HasDisembarked && !p.IsCanceled);
            TotalDisembarked = _context.Passengers.Count(p => p.HasDisembarked);
            TotalCanceled = _context.Passengers.Count(p => p.IsCanceled);
            return Page();
        }
    }
}
