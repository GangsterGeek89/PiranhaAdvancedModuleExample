
using Piranha;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System;
using System.Threading.Tasks;

namespace ClubManager.Models.Content
{
    [ContentGroup(Title = "Clubs", Icon = "fas fa-hammer")]
    public abstract class Club<T> : Content<T>
    where T : Club<T>
    {
        [Region]
        public ClubInfoRegion ClubInfo { get; set; }

        public class ClubInfoRegion
        {
            [Field(Title = "Club Editor")]
            public DataSelectField<ClubEditor> User { get; set; }

            [Field(Title = "Club Main Page")]
            public PageField ClubMainPage { get; set; }
        }
    }
}
