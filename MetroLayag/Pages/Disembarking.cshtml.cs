using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Data;
using MetroLayag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MetroLayag.Pages
{
    public class DisembarkingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DisembarkingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Passenger> PassengerList { get; set; }
        public List<Passenger> DisembarkedList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDate { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToPage("/Login");

            var query = _context.Passengers
                .Where(p => !p.HasDisembarked && !p.IsCanceled);

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                query = query.Where(p =>
                    p.FullName.Contains(SearchTerm) ||
                    p.Destination.Contains(SearchTerm));
            }

            if (FilterDate.HasValue)
            {
                var date = FilterDate.Value.Date;
                query = query.Where(p => p.BookingDate.Date == date);
            }

            PassengerList = query.OrderByDescending(p => p.BookingDate).ToList();

            var disembarkedQuery = _context.Passengers
                .Where(p => p.HasDisembarked && !p.IsCanceled);

            if (FilterDate.HasValue)
            {
                var date = FilterDate.Value.Date;
                disembarkedQuery = disembarkedQuery.Where(p => p.BookingDate.Date == date);
            }

            DisembarkedList = disembarkedQuery.OrderByDescending(p => p.BookingDate).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostMarkDisembarkedAsync(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger != null)
            {
                passenger.HasDisembarked = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
