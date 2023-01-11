using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Outfit.Domain.ViewModels.Added
{
    public class CreateAddedViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime DateOfAdd { get; set; }

        public long ClothesId { get; set; }

        public string Login { get; set; }
    }
}
