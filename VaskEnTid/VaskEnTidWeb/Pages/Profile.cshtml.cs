using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaskEnTidLib.Service;
using VaskEnTidLib.Service;
using VaskEnTidLib.Model;
using System.Diagnostics;

namespace VaskEnTidWeb.Pages
{
    public class ProfileModel : PageModel
    {
        public UserService _userService;
        public BookingService _bookingService;

        [BindProperty]
        public User user { get; set; } = new();
        [BindProperty]
        public List<Booking> bookings { get; set; } = new();

        private readonly ILogger<ProfileModel> _logger;



        public ProfileModel(ILogger<ProfileModel> logger, UserService bs, BookingService ebs)
        {
            _logger = logger;
            _userService = bs;
            _bookingService = ebs;
        }

        public void OnGet(User loggedInUser)
        {
            user = loggedInUser;
            if (user.UserID == 0)
            {
                RedirectToPage("/Log-in");
            }
            Console.WriteLine(user.DomicileID[0]);
            Console.WriteLine(user.DomicileID.Count());

            int i = 0;
            foreach (int domicileID in user.DomicileID)
            {
                Console.WriteLine(user.DomicileID[0]);
                bookings[i] = _bookingService.GetByDomicileID(user.DomicileID[i]);
                i++;
            }
        }
    }

}
