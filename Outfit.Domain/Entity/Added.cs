using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain.Entity
{
    public class Added
    {
        public long Id { get; set; }

        public long? ClothesId { get; set; }

        public DateTime DateofAdd { get; set; }

        public long? FavoriteId { get; set; }

        public virtual Favorite Favorite { get; set; }
    }
}
