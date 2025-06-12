using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Models;
using MetroLayag.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;


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

        public List<string> Stations { get; set; } = new()
        {
            "Escolta", "Lawton", "Quinta", "PUP", "Sta. Ana", "Lambingan", "Valenzuela",
            "Hulo", "Guadalupe", "Maybunga", "San Joaquin", "Kalawaan", "Pinagbuhatan"
        };

        public List<Passenger> SuccessfulPassengers { get; set; } = new();
        public List<Passenger> CanceledPassengers { get; set; } = new();

        public void OnGet()
        {
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

            SuccessfulPassengers = passengers
                .Where(p => p.HasDisembarked && !p.IsCanceled)
                .ToList();

            CanceledPassengers = passengers
                .Where(p => p.IsCanceled)
                .ToList();
        }
    }
}