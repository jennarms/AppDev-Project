using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Data;
using MetroLayag.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MetroLayag.Pages
{
    public class ManageStationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ManageStationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<User> Users { get; set; }

        [BindProperty]
        public User EditUser { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToPage("/Login");

            var role = HttpContext.Session.GetString("Role");
            if (role != "MainAdmin")
                return RedirectToPage("/AccessDenied");

            Users = _context.Users
                .Where(u => u.Role == "StationAdmin")
                .OrderBy(u => u.Station)
                .ToList();

            return Page();
        }

        public IActionResult OnPostEdit()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToPage("/Login");

            var role = HttpContext.Session.GetString("Role");
            if (role != "MainAdmin")
                return RedirectToPage("/AccessDenied");

            var user = _context.Users.FirstOrDefault(u => u.Id == EditUser.Id);
            if (user == null) return RedirectToPage();

            user.Username = EditUser.Username;

            if (!string.IsNullOrWhiteSpace(EditUser.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(EditUser.Password);
            }

            _context.SaveChanges();
            return RedirectToPage();
        }
    }
}
