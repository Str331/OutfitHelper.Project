using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain.Entity
{
    public class Clothes
    {
        public int Id { get; set; }  //Id обьекта
        public string? Name { get; set; }  //название обьекта
        public string? Description { get; set; }  //описание
        public decimal Price { get; set; }   //цена образа
        public DateTime DateOfAdd { get; set; }  //дата добавления
        public OutfitType outfitType { get; set; }  //тип образа
        public byte[]? avatar { get; set; }   //изображение
    }
}
