using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
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

        public DomicilesModel(DomicileService ds)
        {
            _domicileService = ds;
            Domiciles = _domicileService.GetAll();
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
            Debug.WriteLine("Temp Domicile ID: " + TempDomicileID);
            return RedirectToPage("/Domicile", new { DomicileID = TempDomicileID });
        }

    }

}
