using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend.BusinesLogic
{
    /// <summary>
    /// Класс, представляющий пользователя системы.
    /// Содержит информацию о пользователе и методы валидации данных.
    /// </summary>
    public class User
    {
        private string firstName;
        private string lastName;
        private string email;
        private string username;
        private string password;

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// Должно содержать более 3 символов.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если имя короче 4 символов.</exception>
        public string FirstName
        {
            get => this.firstName;
            set
            {
                if (value.Length <= 3)
                {
                    throw new ArgumentException("Invalid first name!");
                }

                this.firstName = value;
            }
        }

        /// <summary>
        /// Фамилия пользователя.
        /// Должна содержать более 3 символов.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если фамилия короче 4 символов.</exception>
        public string LastName
        {
            get => this.lastName;
            set
            {
                if (value.Length <= 3)
                {
                    throw new ArgumentException("Invalid last name!");
                }

                this.lastName = value;
            }
        }

        /// <summary>
        /// Электронная почта пользователя.
        /// Проверяется на соответствие формату email.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается при неверном формате email.</exception>
        public string Email
        {
            get => this.email;
            set
            {
                Regex regex = new Regex(@"^([\w-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([\w-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    this.email = value;
                }
                else
                {
                    throw new ArgumentException(@"Invalid email!");
                }
            }
        }

        /// <summary>
        /// Логин пользователя.
        /// Должен содержать не менее 3 символов.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если логин короче 3 символов.</exception>
        public string Username
        {
            get => this.username;
            set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException("Invalid username!");
                }

                this.username = value;
            }
        }

        /// <summary>
        /// Пароль пользователя.
        /// Должен содержать не менее 5 символов.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если пароль короче 5 символов.</exception>
        public string Password
        {
            get => this.password;
            set
            {
                if (value.Length < 5)
                {
                    throw new ArgumentException("Invalid password!");
                }

                this.password = value;
            }
        }

        /// <summary>
        /// Адрес пользователя.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Контактный телефон пользователя.
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// Пол пользователя.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Тип пользователя (например: "Admin", "User").
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// Дата добавления пользователя.
        /// </summary>
        public DateTime AddedDate { get; set; }

        /// <summary>
        /// ID пользователя, добавившего запись.
        /// </summary>
        public int AddedBy { get; set; }

        /// <summary>
        /// Имя пользователя, добавившего запись.
        /// </summary>
        public string AddedByName { get; set; }
    }
}