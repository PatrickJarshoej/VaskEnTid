using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;

namespace VaskEnTidWeb.Pages
{
    public class BookingModel : PageModel
    {

        BookingService _bookingService;
        //MachineService _machineService;
        public List<Booking> Bookings {  get; set; }

        public BookingModel(BookingService bs)
        {
            _bookingService = bs;
            Bookings = _bookingService.GetAll();
        }

        public void OnGet()
        {
        }

        public void OnPostCreate()
        {

        }
        public void OnPost() { }
        
    }

}
