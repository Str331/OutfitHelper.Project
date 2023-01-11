using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        public string UserName { get; set; }

        [Required(ErrorMessage ="Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name ="Пароль")]
        [MinLength(6,ErrorMessage ="Пароль должен быть больше или равен 6 символов")]
        public string NewPassword { get; set; }
    }
}
