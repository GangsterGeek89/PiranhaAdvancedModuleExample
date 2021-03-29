using Microsoft.EntityFrameworkCore;
using Piranha.AspNetCore.Identity;
using Piranha.Extend.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubManager.Models
{
    public class ClubEditor
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        static async Task<ClubEditor> GetById(string id, IDb db)
        {
            var users = await db.Users.ToListAsync();
            var user =  users.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();
            if(user != null)
            {
                return new ClubEditor
                {
                    Id = user.Id,
                    Username = user.UserName
                };
            }

            return new ClubEditor { Id = Guid.Empty, Username = "Not Found"};
        }

        static async Task<IEnumerable<DataSelectFieldItem>> GetList(IDb db)
        {
            return (await db.Users.OrderBy(u => u.UserName).ToListAsync()).Select(p => new DataSelectFieldItem
            {
                Id = p.Id.ToString(),
                Name = p.UserName
            });
        }
    }
}
