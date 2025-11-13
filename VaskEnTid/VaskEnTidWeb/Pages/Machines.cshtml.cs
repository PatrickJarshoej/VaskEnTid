using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.PortableExecutable;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;

namespace VaskEnTidWeb.Pages
{
    public class MachinesModel : PageModel
    {

        MachineService _machineService;
        public List<Booking> Bookings {  get; set; }
        public List<VaskEnTidLib.Model.Machine> Machines {  get; set; }

        [BindProperty]
        public int TempMachineID { get; set; }

        public MachinesModel(MachineService ms)
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
            return RedirectToPage("/Machine", new { MachineID = TempMachineID });
        }

    }

}
