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
        public bool Edit { get; set; } = false;
        [BindProperty]
        public Booking SpecificBooking { get; set; }
        [BindProperty]
        public DateTime TempDateAndTime { get; set; }
        [BindProperty]
        public int TempID { get; set; }

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
            SpecificBooking = _bookingService.GetByID(TempID);
        }
        public void OnPostDelete() 
        {

        }
        public void OnPostSave()
        {
            SpecificBooking = _bookingService.GetByID(TempID);
            SpecificBooking.DateAndTime = TempDateAndTime;
            //Console.WriteLine(SpecificBooking);
            _bookingService.Update(SpecificBooking);
            Edit = false;
        }
        
    }
}
