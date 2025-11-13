using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;

namespace VaskEnTidWeb.Pages
{
    public class DomicilesModel : PageModel
    {

        DomicileService _domicileService;
        public List<Domicile> Domiciles {  get; set; }
        [BindProperty]
        public int TempDomicileID { get; set; }
        [BindProperty]
        public int PostalCode { get; set; }
        [BindProperty]
        public string RoadName{ get; set; }
        [BindProperty]
        public string Door { get; set; }
        [BindProperty]
        public string Floor { get; set; }
        [BindProperty]
        public string City { get; set; }
        [BindProperty]
        public string Region { get; set; }
        [BindProperty]
        public string Country { get; set; }
        public DomicilesModel(DomicileService ds)
        {
            _domicileService = ds;
            Domiciles = _domicileService.GetAll();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostCreate()
        {
            _domicileService.Create(RoadName, PostalCode, Floor, Door, City, Region, Country);
            return RedirectToPage("/Domiciles");
        }
        public void OnPost() { }
        public IActionResult OnPostShow()
        {
            Debug.WriteLine("Temp Domicile ID: " + TempDomicileID);
            return RedirectToPage("/Domicile", new { DomicileID = TempDomicileID });
        }

    }

}
