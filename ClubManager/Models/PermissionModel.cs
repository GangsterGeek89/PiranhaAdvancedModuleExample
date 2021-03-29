using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubManager.Models
{
    public class PermissionModel
    {
        public ClubPagePermissions ClubPages { get; } = new ClubPagePermissions();

        public class ClubPagePermissions
        {
            public bool Add { get; set; }
            public bool Delete { get; set; }
            public bool Edit { get; set; }
            public bool Preview { get; set; }
            public bool Publish { get; set; }
            public bool Save { get; set; }
        }
    }
}
