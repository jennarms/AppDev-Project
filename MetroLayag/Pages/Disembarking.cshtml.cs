using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Data;
using MetroLayag.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using X.PagedList;
using X.PagedList.Extensions;

namespace MetroLayag.Pages
{
    public class DisembarkingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DisembarkingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IPagedList<Passenger> PassengerList { get; set; }
        public IPagedList<Passenger> DisembarkedList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ActivePage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int DisembarkedPage { get; set; } = 1;

        private const int PageSize = 10;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToPage("/Login");

            if (HttpContext.Session.GetString("UserRole") != "StationAdmin")
                return RedirectToPage("/AccessDenied");

            // Active passengers
            var activeQuery = _context.Passengers
                .Where(p => !p.HasDisembarked && !p.IsCanceled);

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                activeQuery = activeQuery.Where(p =>
                    p.FullName.Contains(SearchTerm) ||
                    p.Destination.Contains(SearchTerm));
            }

            if (FilterDate.HasValue)
            {
                var date = FilterDate.Value.Date;
                activeQuery = activeQuery.Where(p => p.BookingDate.Date == date);
            }

            PassengerList = activeQuery
                .OrderByDescending(p => p.BookingDate)
                .ToPagedList(ActivePage, PageSize);

            // Disembarked passengers
            var disembarkedQuery = _context.Passengers
                .Where(p => p.HasDisembarked && !p.IsCanceled);

            if (FilterDate.HasValue)
            {
                var date = FilterDate.Value.Date;
                disembarkedQuery = disembarkedQuery.Where(p => p.BookingDate.Date == date);
            }

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                disembarkedQuery = disembarkedQuery.Where(p =>
                    p.FullName.Contains(SearchTerm) ||
                    p.Destination.Contains(SearchTerm));
            }

            DisembarkedList = disembarkedQuery
                .OrderByDescending(p => p.BookingDate)
                .ToPagedList(DisembarkedPage, PageSize);

            return Page();
        }

        public async Task<IActionResult> OnPostMarkDisembarkedAsync(int id)
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToPage("/Login");

            if (HttpContext.Session.GetString("UserRole") != "StationAdmin")
                return RedirectToPage("/AccessDenied");

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
