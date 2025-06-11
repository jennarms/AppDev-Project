using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Data;
using MetroLayag.Models;
using System.Threading.Tasks;

namespace MetroLayag.Pages
{
    public class CancelPassengerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CancelPassengerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null)
                return NotFound();

            passenger.IsCanceled = true;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Booking");
        }
    }
}
