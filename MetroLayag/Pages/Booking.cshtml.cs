using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Models;
using MetroLayag.Data;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using X.PagedList.Extensions;

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

        public IPagedList<Passenger> PassengerList { get; set; } = new PagedList<Passenger>(new List<Passenger>(), 1, 10);
        public IPagedList<Passenger> CanceledList { get; set; } = new PagedList<Passenger>(new List<Passenger>(), 1, 10);

        [BindProperty(SupportsGet = true)]
        public string? SearchTermBooked { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTermCanceled { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDateBooked { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDateCanceled { get; set; }

        [BindProperty(SupportsGet = true)]
        public int BookedPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int CanceledPage { get; set; } = 1;

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToPage("/Login");

            if (HttpContext.Session.GetString("UserRole") != "StationAdmin")
                return RedirectToPage("/AccessDenied");

            LoadPassengerLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToPage("/Login");

            if (HttpContext.Session.GetString("UserRole") != "StationAdmin")
                return RedirectToPage("/AccessDenied");

            if (!ModelState.IsValid)
            {
                LoadPassengerLists(); // Show validation errors with current data
                return Page();
            }

            Passenger.BookingDate = DateTime.Now;
            _context.Passengers.Add(Passenger);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        private void LoadPassengerLists()
        {
            const int pageSize = 10;

            // Active passengers
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

            PassengerList = activeQuery
                .OrderByDescending(p => p.BookingDate)
                .ToPagedList(BookedPage, pageSize);

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

            CanceledList = canceledQuery
                .OrderByDescending(p => p.BookingDate)
                .ToPagedList(CanceledPage, pageSize);
        }
    }
}
