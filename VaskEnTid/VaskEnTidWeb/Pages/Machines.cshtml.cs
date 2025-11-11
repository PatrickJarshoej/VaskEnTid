using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;

namespace VaskEnTidWeb.Pages
{
    public class MachineModel : PageModel
    {

        BookingService _bookingService;
        MachineService _machineService;
        public List<Booking> Bookings {  get; set; }
        public List<VaskEnTidLib.Model.Machine> Machines {  get; set; }

        [BindProperty]
        public int TempMachineID { get; set; }

        public MachineModel(MachineService ms)
        {
            _machineService = ms;
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
            return RedirectToPage("/Blog", new { MachineID = TempMachineID });
        }

    }

}
