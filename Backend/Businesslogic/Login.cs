using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic
{
    /// <summary>
    /// Класс, представляющий данные для входа пользователя.
    /// Используется для передачи логина, пароля и типа пользователя.
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя (в открытом виде, без хеширования).
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Тип пользователя (например: "Admin", "User", "Guest").
        /// Определяет уровень доступа в системе.
        /// </summary>
        public string UserType { get; set; }
    }
}
