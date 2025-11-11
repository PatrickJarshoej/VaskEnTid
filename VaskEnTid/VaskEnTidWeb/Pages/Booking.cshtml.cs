using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;

namespace VaskEnTidWeb.Pages
{
    public class BookingModel : PageModel
    {

        [BindProperty]
        public int BookingID { get; set; }
        [BindProperty]
        public bool Edit { get; set; }
        [BindProperty]
        public Booking SpecificBooking { get; set; }

        BookingService _bookingService;

        public BookingModel(BookingService bs) 
        {
            _bookingService = bs;
        }

        public void OnGet(int bookingID)
        {
            SpecificBooking = _bookingService.GetByID(bookingID);

        }
        public void OnPost()
        {

        }
        public void OnPostEdit()
        {
            Edit = true;
        }
    }
}
