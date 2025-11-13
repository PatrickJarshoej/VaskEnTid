using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;
using System.Drawing;
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
        [BindProperty]
        public VaskEnTidLib.Model.Type TempType { get; set; }
        [BindProperty]
        public int TempTypeNumber { get; set; }

        public MachinesModel(MachineService ms)
        {
            _machineService = ms;
            Machines = _machineService.GetAll();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostCreate()
        {
            _machineService.Create(TempType, TempTypeNumber);
            return RedirectToPage("/Machines");
        }
        public void OnPost() { }
        public IActionResult OnPostShow()
        {
            return RedirectToPage("/Machine", new { MachineID = TempMachineID });
        }

    }

}
