using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Data;
using MetroLayag.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

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

        public string UserStation { get; set; } = string.Empty;

        public List<string> OtherStations { get; set; } = new List<string>();

        private static readonly List<string> AllStations = new List<string>
        {
            "Escolta", "Lawton", "Quinta", "PUP", "Sta. Ana", "Lambingan",
            "Valenzuela", "Hulo", "Guadalupe", "Maybunga", "San Joaquin",
            "Kalawaan", "Pinagbuhatan"
        };

        public IActionResult OnGet(int id)
        {
            Passenger = _context.Passengers.Find(id);
            if (Passenger == null)
                return NotFound();

            var sessionStation = HttpContext.Session.GetString("Station");
            if (string.IsNullOrEmpty(sessionStation))
                return RedirectToPage("/Login");

            UserStation = sessionStation;
            OtherStations = AllStations.Where(s => s != UserStation).ToList();

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
