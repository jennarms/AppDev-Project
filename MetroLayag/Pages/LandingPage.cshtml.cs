using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetroLayag.Pages
{
    public class LandingPageModel : PageModel
    {
        private readonly ILogger<LandingPageModel> _logger;

        public LandingPageModel(ILogger<LandingPageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
