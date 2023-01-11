using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage ="Укажите возраст")]
        [Range(16,99,ErrorMessage ="Возраст должен находиться в диапазоне от 16 до 99 лет")]
        public byte Age { get; set; }

        [MinLength(5,ErrorMessage ="Минимальная длина строки составляет 5 символов")]
        [MaxLength(100,ErrorMessage ="Максимальная длина строки составляет 100 символов")]
        public string Address { get; set; }

        public string UserName { get; set; }

        public string NewPassword { get; set; }
    }
}
