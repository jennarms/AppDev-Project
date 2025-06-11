using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Data;
using MetroLayag.Models;

namespace MetroLayag.Pages
{
    public class EditPassengerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditPassengerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Passenger Passenger { get; set; }

        public IActionResult OnGet(int id)
        {
            Passenger = _context.Passengers.Find(id);
            if (Passenger == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Passengers.Update(Passenger);
            _context.SaveChanges();
            return RedirectToPage("/Booking");
        }
    }
}
