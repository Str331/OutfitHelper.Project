using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain
{
    public enum OutfitType
    {
        [Display(Name ="Бизнес кэжуал")]
        BusinessCasualStyle = 0,
        [Display(Name = "Фешн")]
        FashionStyle = 1,
        [Display(Name = "Смарт кэжуал")]
        SmartCasualStyle = 2,
        [Display(Name = "Спортивный стиль")]
        SportStyle = 3,
        [Display(Name = "Стрит стиль")]
        StreetStyle = 4,
    }
}
