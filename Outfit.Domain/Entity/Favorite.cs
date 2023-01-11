using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain.Entity
{
    public class Favorite
    {
        public long Id { get; set; }

        public User User { get; set; }

        public long UserId { get; set; }

        public List<Added> Addeds { get; set; } 
    }
}
