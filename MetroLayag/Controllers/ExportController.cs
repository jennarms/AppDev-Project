using Microsoft.AspNetCore.Mvc;
using MetroLayag.Data;
using MetroLayag.Models;
using Rotativa.AspNetCore;
using System;
using System.Linq;

namespace MetroLayag.Controllers
{
    [Route("Export")]
    public class ExportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Export/SuccessfulPdf
        [HttpGet("SuccessfulPdf")]
        public IActionResult SuccessfulPdf(DateTime? filterDate, string? selectedStation)
        {
            var passengers = _context.Passengers
                .Where(p => p.HasDisembarked && !p.IsCanceled);

            if (filterDate.HasValue)
            {
                passengers = passengers.Where(p => p.BookingDate.Date == filterDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(selectedStation))
            {
                passengers = passengers.Where(p =>
                    p.StartingStation == selectedStation || p.Destination == selectedStation);
            }

            string fileName = GenerateFileName("SuccessfulPassengers", filterDate, selectedStation);

            return new ViewAsPdf("PassengerPdf", passengers.ToList())
            {
                FileName = fileName
            };
        }

        // GET: /Export/CanceledPdf
        [HttpGet("CanceledPdf")]
        public IActionResult CanceledPdf(DateTime? filterDate, string? selectedStation)
        {
            var passengers = _context.Passengers
                .Where(p => p.IsCanceled);

            if (filterDate.HasValue)
            {
                passengers = passengers.Where(p => p.BookingDate.Date == filterDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(selectedStation))
            {
                passengers = passengers.Where(p =>
                    p.StartingStation == selectedStation || p.Destination == selectedStation);
            }

            string fileName = GenerateFileName("CanceledPassengers", filterDate, selectedStation);

            return new ViewAsPdf("PassengerPdf", passengers.ToList())
            {
                FileName = fileName
            };
        }

        private string GenerateFileName(string baseName, DateTime? date, string? station)
        {
            var fileName = baseName;

            if (date.HasValue)
            {
                fileName += $"_{date.Value:yyyy-MM-dd}";
            }

            if (!string.IsNullOrEmpty(station))
            {
                // Sanitize the station name for filenames
                var safeStation = station.Replace(" ", "_").Replace("/", "-");
                fileName += $"_{safeStation}";
            }

            return fileName + ".pdf";
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Content("ExportController is working!");
        }
    }
}
