using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;

namespace VaskEnTidWeb.Pages
{
    public class MachineModel : PageModel
    {

        [BindProperty]
        public int MachineID { get; set; }
        [BindProperty]
        public bool Edit { get; set; }
        [BindProperty]
        public Machine SpecificMachine { get; set; }
        [BindProperty]
        public int TempID { get; set; }
        [BindProperty]
        public Status TempStatus { get; set; }
        [BindProperty]
        public double TempCost { get; set; }
        

        MachineService _MachineService;

        public MachineModel(MachineService ds) 
        {
            _MachineService = ds;
            
        }

        public void OnGet(int MachineID)
        {
            SpecificMachine = _MachineService.GetByID(MachineID);
            TempID = MachineID;
        }
        public void OnPost()
        {

        }
        public void OnPostEdit()
        {
            Debug.WriteLine("Temp Machine ID: " + TempID);
            Edit = true;
            SpecificMachine = _MachineService.GetByID(TempID);
        }
        public IActionResult OnPostSave()
        {
            SpecificMachine = _MachineService.GetByID(TempID);
            //SpecificMachine.Roadname=TempRoadName;
            //SpecificMachine.City=TempCity;
            //SpecificMachine.Region=TempRegion;
            //SpecificMachine.Door=TempDoor;
            //SpecificMachine.Country=TempCountry;
            _MachineService.Update(SpecificMachine.MachineID, TempStatus, TempCost);
            Edit = false;
            return RedirectToPage("/Machine", new { MachineID = TempID });
        }
        public IActionResult OnPostDelete()
        {
            Console.WriteLine($"You pressed Delete {TempID}");
            _MachineService.DeleteByID(TempID);
            return RedirectToPage("/Machines");
        }
    }
}
