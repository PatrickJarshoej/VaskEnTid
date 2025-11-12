using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;

namespace VaskEnTidWeb.Pages
{
    public class DomicileModel : PageModel
    {

        [BindProperty]
        public int domicileID { get; set; }
        [BindProperty]
        public bool Edit { get; set; }
        [BindProperty]
        public Domicile SpecificDomicile { get; set; }
        [BindProperty]
        public int TempID { get; set; }
        [BindProperty]
        public string TempRoadName { get; set; }
        [BindProperty]
        public string TempRegion { get; set; }
        [BindProperty]
        public string TempCity { get; set; }
        [BindProperty]
        public string TempDoor { get; set; }
        [BindProperty]
        public string TempCountry { get; set; }
        [BindProperty]
        public List<User> Users { get; set; }

        DomicileService _domicileService;
        UserService _userService;

        public DomicileModel(DomicileService ds, UserService us) 
        {
            _domicileService = ds;
            _userService = us;
            
        }

        public void OnGet(int domicileID)
        {
            SpecificDomicile = _domicileService.GetByID(domicileID);
            TempID = domicileID;
        }
        public void OnPost()
        {

        }
        public void OnPostEdit()
        {
            Debug.WriteLine("Temp Domicile ID: " + TempID);
            Edit = true;
            SpecificDomicile = _domicileService.GetByID(TempID);
            //Users=_userService.GetAll()
        }
        public IActionResult OnPostSave()
        {
            SpecificDomicile = _domicileService.GetByID(TempID);
            //SpecificDomicile.Roadname=TempRoadName;
            //SpecificDomicile.City=TempCity;
            //SpecificDomicile.Region=TempRegion;
            //SpecificDomicile.Door=TempDoor;
            //SpecificDomicile.Country=TempCountry;
            _domicileService.Update(SpecificDomicile.DomicileID, TempRoadName, TempDoor,TempCity,TempRegion, TempCountry);
            Edit = false;
            return RedirectToPage("/Domicile", new { DomicileID = TempID });
        }
    }
}
