using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Введите имя пользователя")]
        [MaxLength(20,ErrorMessage ="Имя пользователя должно быть меньше 20 символов")]
        [MinLength(3,ErrorMessage ="Имя пользователя должно быть больше 3 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name ="Пароль")]
        public string Password { get; set; }
    }
}
