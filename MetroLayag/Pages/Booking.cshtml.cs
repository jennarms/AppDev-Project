using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Models;
using MetroLayag.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

namespace MetroLayag.Pages
{
    public class BookingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BookingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Passenger Passenger { get; set; }

        public List<Passenger> PassengerList { get; set; } = new();
        public List<Passenger> CanceledList { get; set; } = new();

        // ✅ Do NOT bind these on POST
        [BindProperty(SupportsGet = true)]
        public string? SearchTermBooked { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTermCanceled { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDateBooked { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDateCanceled { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToPage("/Login");

            LoadPassengerLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("🚨 OnPostAsync reached");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ Model is invalid");

                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"❌ Field: {state.Key} - Error: {error.ErrorMessage}");
                    }
                }

                LoadPassengerLists(); // Keep UI consistent
                return Page();
            }

            Passenger.BookingDate = DateTime.Now;
            _context.Passengers.Add(Passenger);
            await _context.SaveChangesAsync();

            Console.WriteLine("✅ Passenger saved");

            return RedirectToPage();
        }

        private void LoadPassengerLists()
        {
            // Active bookings
            var activeQuery = _context.Passengers
                .Where(p => !p.HasDisembarked && !p.IsCanceled);

            if (!string.IsNullOrWhiteSpace(SearchTermBooked))
            {
                activeQuery = activeQuery.Where(p =>
                    p.FullName.Contains(SearchTermBooked) ||
                    p.Destination.Contains(SearchTermBooked));
            }

            if (FilterDateBooked.HasValue)
            {
                var date = FilterDateBooked.Value.Date;
                activeQuery = activeQuery.Where(p => p.BookingDate.Date == date);
            }

            PassengerList = activeQuery.OrderByDescending(p => p.BookingDate).ToList();

            // Canceled passengers
            var canceledQuery = _context.Passengers.Where(p => p.IsCanceled);

            if (!string.IsNullOrWhiteSpace(SearchTermCanceled))
            {
                canceledQuery = canceledQuery.Where(p =>
                    p.FullName.Contains(SearchTermCanceled) ||
                    p.Destination.Contains(SearchTermCanceled));
            }

            if (FilterDateCanceled.HasValue)
            {
                var date = FilterDateCanceled.Value.Date;
                canceledQuery = canceledQuery.Where(p => p.BookingDate.Date == date);
            }

            CanceledList = canceledQuery.OrderByDescending(p => p.BookingDate).ToList();
        }
    }
}
