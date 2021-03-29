using ClubManager.Models;
using ClubManager.Models.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.AspNetCore.Identity.Data;
using Piranha.Manager;
using Piranha.Manager.Models.Content;
using Piranha.Manager.Services;
using Piranha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ClubManager.Models.ClubPageListModel;

namespace ClubManager.Controllers
{
    /// <summary>
    /// Api controller for page management.
    /// </summary>
    [Area("Manager")]
    [Route("manager/api/clubpage")]
    [Authorize(Policy = Permissions.ClubEditor)]
    [ApiController]
    public class ClubPageApiController : Controller
    {
        private readonly PageService _service;
        private readonly UserManager<User> _userManager;
        private readonly IApi _api;
        private readonly ManagerLocalizer _localizer;

        public ClubPageApiController(PageService service, UserManager<User> userManager, IApi api, ManagerLocalizer localizer)
        {
            _service = service;
            _userManager = userManager;
            _api = api;
            _localizer = localizer;
        }
        /// <summary>
        /// Gets the list model.
        /// </summary>
        /// <returns>The list model</returns>
        [Route("list")]
        [HttpGet]
        [Authorize(Policy = Permissions.ClubEditor)]
        public async Task<ClubPageListModel> List()
        {
            var ClubModel = new ClubPageListModel();

            // Get Page Types
            ClubModel.PageTypes = App.PageTypes.Select(t => new ContentTypeModel
            {
                Id = t.Id,
                Title = t.Title,
                AddUrl = "manager/page/add/"
            }).ToList();

            // Get Default Site
            var DefaultSite = (await _api.Sites.GetAllAsync()).Where(s => s.IsDefault == true).FirstOrDefault();

            // Check we have the default site
            if (DefaultSite == null)
                return ClubModel;

            // Get the logged in users id
            var id = (await _userManager.GetUserAsync(User)).Id;
            if (id == Guid.Empty)
                return ClubModel;


            // Get the club info relevant to the logged in user
            var Club = (await _api.Content.GetAllAsync<StandardClub>()).Where(x => Guid.Parse(x.ClubInfo.User.Id) == id).FirstOrDefault();
            if (Club == null)
                return ClubModel;

            // Find the club Pages
            var MainPageId = Club.ClubInfo.ClubMainPage.Id;
            if (MainPageId == Guid.Empty)
                return ClubModel;

            // Get Main Club Page
            var ClubPage = await _api.Pages.GetByIdAsync((Guid)MainPageId);
            if (ClubPage == null)
                return ClubModel;

            var sitemap = await _api.Sites.GetSitemapAsync(ClubPage.SiteId, false);
            var drafts = await _api.Pages.GetAllDraftsAsync(ClubPage.SiteId);

            var MainClubPageSiteMapItem = sitemap.Where(si => si.Id == ClubPage.Id).FirstOrDefault();

            ClubModel.MainPage = new MainClubPage
            {
                Id = MainClubPageSiteMapItem.Id,
                SiteId = ClubPage.SiteId,
                Title = MainClubPageSiteMapItem.Title,
                TypeName = MainClubPageSiteMapItem.PageTypeName,
                Published = MainClubPageSiteMapItem.Published.HasValue ? MainClubPageSiteMapItem.Published.Value.ToString("yyyy-MM-dd") : null,
                Status = drafts.Contains(MainClubPageSiteMapItem.Id) ? _localizer.General[ClubPageListModel.MainClubPage.Draft] :
                    !MainClubPageSiteMapItem.Published.HasValue ? _localizer.General[ClubPageListModel.MainClubPage.Unpublished] : "",
                EditUrl = "manager/page/edit/",
                IsExpanded = false,
                IsCopy = MainClubPageSiteMapItem.OriginalPageId.HasValue,
                IsRestricted = MainClubPageSiteMapItem.Permissions.Count > 0,
                Permalink = MainClubPageSiteMapItem.Permalink

            };

            // Get The Club Pages
            ClubModel.MainPage.Items.AddRange(MapRecursive(ClubPage.SiteId, MainClubPageSiteMapItem, 1, 0, drafts));


            return ClubModel;
        }

        private List<ClubPage> MapRecursive(Guid siteId, SitemapItem parentItem, int level, int expandedLevels, IEnumerable<Guid> drafts)
        {
            var subPages = new List<ClubPage>();

            foreach(var item in parentItem.Items)
            {
                subPages.Add(new ClubPage
                {
                    Id = item.Id,
                    SiteId = siteId,
                    Title = item.MenuTitle,
                    TypeName = item.PageTypeName,
                    Published = item.Published.HasValue ? item.Published.Value.ToString("yyyy-MM-dd") : null,
                    Status = drafts.Contains(item.Id) ? _localizer.General[ClubPageListModel.ClubPage.Draft] :
                    !item.Published.HasValue ? _localizer.General[ClubPageListModel.ClubPage.Unpublished] : "",
                    EditUrl = "manager/page/edit/",
                    IsExpanded = level < expandedLevels,
                    IsCopy = item.OriginalPageId.HasValue,
                    IsRestricted = item.Permissions.Count > 0,
                    Permalink = item.Permalink
                });
            }

            return subPages;
        }
    }
}
