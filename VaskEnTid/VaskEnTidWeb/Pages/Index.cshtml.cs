using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaskEnTidLib.Service;
using VaskEnTidLib.Model;
using System.Diagnostics;

namespace VaskEnTidWeb.Pages
{
    public class IndexModel : PageModel
    {
        UserService _userService;

        [BindProperty]
        public int Userid { get; set; }
        [BindProperty]
        public string Pass { get; set; }
        [BindProperty]
        public bool IsLoggedIn { get; set; } = false;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, UserService bs)
        {
            _logger = logger;
            _userService = bs;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            Debug.WriteLine("Du kører den forkerte on post");
        }
        public IActionResult OnPostLogIn()
        {
            User user = _userService.CheckPassword(Userid, Pass);
            if (user.UserID == 0)
            {
                Debug.WriteLine("Error in Username or password");
                IsLoggedIn = false;
                Debug.WriteLine($"{user.FirstName} is logged in? {IsLoggedIn}");
                return RedirectToPage("/Index");
            }
            else
            {
                IsLoggedIn = true;
                Debug.WriteLine($"{user.FirstName} is logged in? {IsLoggedIn}");
                return RedirectToPage("/Profile", user);
            }
        }
    }
}
