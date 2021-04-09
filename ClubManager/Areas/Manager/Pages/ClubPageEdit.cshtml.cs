using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubManager.Areas.Manager.Pages
{
    [Authorize(Policy = Permissions.ClubEditorEdit)]
    public class ClubPageEditModel : PageModel
    {
        
    }
}
