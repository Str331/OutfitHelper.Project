using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outfit.Domain
{//перечисление статусных кодов
    public enum StatusCode
    {
        UserNotFound = 0,
        UserAlreadyExists = 1,

        ClothesNotFound = 10,

        ContentNotFound = 20,

        OK = 200,
        InternalServerError = 500
    }
}
