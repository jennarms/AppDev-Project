using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Models;
using System.Collections.Generic;
using System.Linq;
using MetroLayag.Data;

namespace MetroLayag.Pages
{
    public class PassengerReportModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PassengerReportModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Passenger> SuccessfulPassengers { get; set; } = new();
        public List<Passenger> CanceledPassengers { get; set; } = new();

        public void OnGet()
        {
            // Passengers who completed their trip (disembarked)
            SuccessfulPassengers = _context.Passengers
                .Where(p => p.HasDisembarked && !p.IsCanceled)
                .ToList();

            // Passengers who canceled
            CanceledPassengers = _context.Passengers
                .Where(p => p.IsCanceled)
                .ToList();
        }
    }
}
