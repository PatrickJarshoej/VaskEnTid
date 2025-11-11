using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaskEnTidLib.Model;
using VaskEnTidLib.Service;

namespace VaskEnTidWeb.Pages
{
    public class DomicileModel : PageModel
    {

        DomicileService _domicileService;
        public List<Domicile> Domiciles {  get; set; }
        [BindProperty]
        public int TempDomicileID { get; set; }

        public DomicileModel(DomicileService ds)
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
            return RedirectToPage("/Blog", new { DomicileID = TempDomicileID });
        }

    }

}
