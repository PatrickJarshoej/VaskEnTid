using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;

namespace VaskEnTidWeb.Pages
{
    public class ProfileModel : PageModel
    {
        public UserService _userService;
        public BookingService _bookingService;

        [BindProperty]
        public User UserMe { get; set; } = new();
        [BindProperty]
        public List<User> Users { get; set; } = new List<User>();
        [BindProperty]
        public Booking Bookings { get; set; } = new();
        [BindProperty]
        public string CreateFirstName { get; set; } = "John";
        [BindProperty]
        public string CreateLastName { get; set; } = "Doe";
        [BindProperty]
        public string CreatePhone { get; set; } = "Not a number";
        [BindProperty]
        public string CreateEmail { get; set; } = "Not@Mail.Fix";
        [BindProperty]
        public string CreatePassword { get; set; } = "1234";
        [BindProperty]
        public bool CreateAdmin { get; set; } = false;

        private readonly ILogger<ProfileModel> _logger;



        public ProfileModel(ILogger<ProfileModel> logger, UserService bs, BookingService ebs)
        {
            _logger = logger;
            _userService = bs;
            _bookingService = ebs;
        }

        public void OnGet(User loggedInUser)
        {
            UserMe = loggedInUser;
            if (UserMe.UserID == 0)
            {
                RedirectToPage("/Log-in");
            }
            else
            {
                Bookings = _bookingService.GetByDomicileID(UserMe.DomicileID[0]);
                Users = _userService.GetAll();
            }
        }
        public IActionResult OnPostCreateUser()
        {
            _userService.CreateUser(CreateFirstName, CreateLastName, CreateEmail, CreatePhone, CreatePassword,CreateAdmin);
            return RedirectToPage("/Profile", UserMe);
        }
    }

}
