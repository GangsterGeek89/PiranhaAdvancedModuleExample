using ClubManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubManager.Controllers
{
    [Area("Manager")]
    [Route("manager/api/clubs/permissions")]
    [Authorize(Policy = Permissions.ClubEditor)]
    [ApiController]
    public class ClubPermissionsApiController : Controller
    {
        private readonly IAuthorizationService _auth;

        public ClubPermissionsApiController(IAuthorizationService auth)
        {
            _auth = auth;
        }

        [HttpGet]
        [Authorize(Policy = Permissions.ClubEditor)]
        public async Task<PermissionModel> Get()
        {
            var model = new PermissionModel();

            // Page permissions
            model.ClubPages.Add = (await _auth.AuthorizeAsync(User, Permissions.ClubEditorAdd)).Succeeded;
            model.ClubPages.Delete = (await _auth.AuthorizeAsync(User, Permissions.ClubEditorDelete)).Succeeded;
            model.ClubPages.Edit = (await _auth.AuthorizeAsync(User, Permissions.ClubEditorEdit)).Succeeded;
            //model.ClubPages.Preview = (await _auth.AuthorizeAsync(User, Permissions.ClubEditorPreview)).Succeeded;
            //model.ClubPages.Publish = (await _auth.AuthorizeAsync(User, Permissions.ClubEditorPublish)).Succeeded;
            //model.ClubPages.Save = (await _auth.AuthorizeAsync(User, Permissions.ClubEditorSave)).Succeeded;

            return model;
        }
    }
}
