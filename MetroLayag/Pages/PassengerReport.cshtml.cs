using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Models;
using MetroLayag.Data;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList.Extensions;

namespace MetroLayag.Pages
{
    public class PassengerReportModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PassengerReportModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedStation { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSuccessful { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageCanceled { get; set; } = 1;

        public List<string> Stations { get; set; } = new()
        {
            "Escolta", "Lawton", "Quinta", "PUP", "Sta. Ana", "Lambingan", "Valenzuela",
            "Hulo", "Guadalupe", "Maybunga", "San Joaquin", "Kalawaan", "Pinagbuhatan"
        };

        public IPagedList<Passenger> PagedSuccessfulPassengers { get; set; }
        public IPagedList<Passenger> PagedCanceledPassengers { get; set; }

        public IActionResult OnGet()
        {
            var isLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";
            var role = HttpContext.Session.GetString("Role");

            if (!isLoggedIn || (role != "MainAdmin" && role != "StationAdmin"))
            {
                return RedirectToPage("/AccessDenied");
            }

            var passengers = _context.Passengers.AsQueryable();

            if (FilterDate.HasValue)
            {
                var date = FilterDate.Value.Date;
                passengers = passengers.Where(p => p.BookingDate.Date == date);
            }

            if (!string.IsNullOrEmpty(SelectedStation))
            {
                passengers = passengers.Where(p =>
                    p.StartingStation == SelectedStation || p.Destination == SelectedStation);
            }

            PagedSuccessfulPassengers = passengers
                .Where(p => p.HasDisembarked && !p.IsCanceled)
                .OrderByDescending(p => p.BookingDate)
                .ToPagedList(PageSuccessful, 10);

            PagedCanceledPassengers = passengers
                .Where(p => p.IsCanceled)
                .OrderByDescending(p => p.BookingDate)
                .ToPagedList(PageCanceled, 10);

            return Page();
        }
    }
}
