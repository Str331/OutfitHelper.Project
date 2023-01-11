using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain
{ 
    public enum Role
    {
        [Display(Name ="Пользователь")]
        User =0,

        [Display(Name ="Модератор")]
        Moderator = 1,

        [Display(Name ="Администратор")]
        Admin =2,
    }
}
