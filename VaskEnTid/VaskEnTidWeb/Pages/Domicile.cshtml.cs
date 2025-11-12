using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        DomicileService _domicileService;

        public DomicileModel(DomicileService ds) 
        {
            _domicileService = ds;
        }

        public void OnGet(int domicileID)
        {
            SpecificDomicile = _domicileService.GetByID(domicileID);

        }
        public void OnPost()
        {

        }
        public void OnPostEdit()
        {
            Edit = true;
        }
    }
}
