using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="Укажите имя пользователя")]
        [MaxLength(20, ErrorMessage = "Имя пользователя должно быть меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя пользователя должно быть больше 3 символов")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Придумайте пароль")]
        [MinLength(6,ErrorMessage = "Пароль должен быть больше или равен 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Подтвердите пароль")]
        [Compare("Password",ErrorMessage ="Пароль не совпадает")]
        public string PasswordConfirm { get; set; }
    }
}
