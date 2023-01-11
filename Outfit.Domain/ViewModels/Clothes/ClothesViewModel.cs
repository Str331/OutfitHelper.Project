using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain.ViewModels.Clothes
{
    public class ClothesViewModel
    {
        public int Id { get; set; }

        [Display(Name="Название")]
        [Required(ErrorMessage ="Введите название")]
        [MinLength(2,ErrorMessage ="минимальная длинна 2 символа")]
        public string? Name { get; set; }  //название обьекта

        [Display(Name ="Описание")]
        [MinLength(50,ErrorMessage ="Минимальная длинна 50 символов")]
        public string? Description { get; set; }  //описание

        [Display(Name ="Стоимость")]
        public decimal Price { get; set; }   //цена образа

        public DateTime DateOfAdd { get; set; }  //дата добавления

        [Display(Name ="Тип образа")]
        [Required(ErrorMessage ="Выберите тип")]
        public string outfitType { get; set; }  //тип образа

        public IFormFile Avatar { get; set; }

        public byte[]? Image { get; set; }   // изображение
    }
}
