using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubManager.Areas.Manager.Pages
{
    [Authorize(Policy = Permissions.ClubEditor)]
    public class ClubPageListViewModel : PageModel
    {
        
    }
}
