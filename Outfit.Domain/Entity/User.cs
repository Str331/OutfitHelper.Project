using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain.Entity
{
    public class User
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public Profile Profile { get; set; }
        public Favorite Favorite { get; set; }
    }
}
