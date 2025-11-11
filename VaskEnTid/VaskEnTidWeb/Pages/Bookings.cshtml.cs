using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;

namespace VaskEnTidWeb.Pages
{
    public class BookingModel : PageModel
    {

        BookingService _bookingService;
        MachineService _machineService;
        public List<Booking> Bookings {  get; set; }
        public List<VaskEnTidLib.Model.Machine> Machines {  get; set; }

        [BindProperty]
        public int TempBookingID { get; set; }

        public BookingModel(BookingService bs, MachineService ms)
        {
            _bookingService = bs;
            _machineService = ms;
            Bookings = _bookingService.GetAll();
            Machines = _machineService.GetAll();
        }

        public void OnGet()
        {
        }

        public void OnPostCreate()
        {

        }
        public void OnPost() { }
        public IActionResult OnPostShow()
        {
            return RedirectToPage("/Blog", new { BookingID = TempBookingID });
        }

    }

}
