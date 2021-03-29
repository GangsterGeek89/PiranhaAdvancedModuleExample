using Piranha.Manager.Models;
using Piranha.Manager.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubManager.Models
{
    public class ClubPageListModel
    {
        public class MainClubPage
        {
            public static readonly string Draft = "Draft";
            public static readonly string Unpublished = "Unpublished";

            public Guid Id { get; set; }
            public Guid SiteId { get; set; }
            public string Title { get; set; }
            public string TypeName { get; set; }
            public string Published { get; set; }
            public string Status { get; set; }
            public string EditUrl { get; set; }
            public bool IsCopy { get; set; }
            public bool IsDraft { get; set; }
            public bool IsExpanded { get; set; }
            public bool IsRestricted { get; set; }
            public string Permalink { get; set; }
            public List<ClubPage> Items { get; set; } = new List<ClubPage>();
        }

        public class ClubPage
        {
            public static readonly string Draft = "Draft";
            public static readonly string Unpublished = "Unpublished";

            public Guid Id { get; set; }
            public Guid SiteId { get; set; }
            public string Title { get; set; }
            public string TypeName { get; set; }
            public string Published { get; set; }
            public string Status { get; set; }
            public string EditUrl { get; set; }
            public bool IsCopy { get; set; }
            public bool IsDraft { get; set; }
            public bool IsExpanded { get; set; }
            public bool IsRestricted { get; set; }
            public string Permalink { get; set; }
        }

        public MainClubPage MainPage { get; set; }

        public IList<ContentTypeModel> PageTypes { get; set; } = new List<ContentTypeModel>();
        public StatusMessage Status { get; set; }
    }
}
