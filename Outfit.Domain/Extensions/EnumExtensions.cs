using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this System.Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttributes<DisplayAttribute>()
                ?.GetName() ?? "Неопределенный";
        }
    }
}
