using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;
using System.Diagnostics;

namespace VaskEnTidWeb.Pages
{
    public class BookingsModel : PageModel
    {

        BookingService _bookingService;
        MachineService _machineService;
        public List<Booking> Bookings {  get; set; }
        public List<VaskEnTidLib.Model.Machine> Machines {  get; set; }

        [BindProperty]
        public int TempBookingID { get; set; }
        [BindProperty]
        public DateTime DateAndTime { get; set; }
        [BindProperty]
        public int DomicileID { get; set; }
        [BindProperty]
        public List<int> MachineIDs { get; set; }


        public BookingsModel(BookingService bs, MachineService ms)
        {
            _bookingService = bs;
            _machineService = ms;
            Bookings = _bookingService.GetAll();
            Machines = _machineService.GetAll();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostCreate()
        {
            //Debug.Write("Date and Time: " + DateAndTime + ", Domicile ID: " + DomicileID + ", Machine IDs: ");
            //foreach(var id in MachineIDs)
            //{
            //    Debug.Write(id);
            //}
            //Debug.WriteLine(" ");
            _bookingService.Create(DateAndTime, DomicileID, MachineIDs);
            return RedirectToPage("/Index");
        }
        public void OnPost() { }
        public IActionResult OnPostShow()
        {
            Debug.WriteLine("Temp Booking ID: " + TempBookingID);
            return RedirectToPage("/Booking", new { BookingID = TempBookingID });
        }

    }

}
