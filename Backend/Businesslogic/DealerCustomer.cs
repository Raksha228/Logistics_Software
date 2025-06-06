using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend.BusinessLogic
{
    /// <summary>
    /// Представляет информацию о клиенте-дилере.
    /// </summary>
    public class DealerCustomer
    {
        private string name;
        private string email;
        private string contact;

        /// <summary>
        /// Получает или задаёт идентификатор клиента-дилера.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задаёт тип клиента-дилера.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Получает или задаёт имя клиента-дилера.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если имя менее 3 символов.</exception>
        public string Name
        {
            get => this.name;
            set
            {
                // Проверка имени
                if (value.Length <= 2)
                {
                    throw new ArgumentException("Некорректное имя!");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Получает или задаёт электронную почту клиента-дилера.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если электронная почта некорректна.</exception>
        public string Email
        {
            get => this.email;
            set
            {
                // Проверка электронной почты
                Regex regex = new Regex(@"^([\w-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([\w-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    this.email = value;
                }
                else
                {
                    throw new ArgumentException("Некорректный адрес электронной почты!");
                }
            }
        }

        /// <summary>
        /// Получает или задаёт контактный номер клиента-дилера.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если контакт меньше 10 символов.</exception>
        public string Contact
        {
            get => this.contact;
            set
            {
                // Проверка контакта
                if (value.Length <= 9)
                {
                    throw new ArgumentException("Некорректный контактный номер!");
                }

                this.contact = value;
            }
        }

        /// <summary>
        /// Получает или задаёт адрес клиента-дилера.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Получает или задаёт дату добавления клиента-дилера.
        /// </summary>
        public DateTime AddedDate { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор пользователя, добавившего информацию о клиенте-дилере.
        /// </summary>
        public int AddedBy { get; set; }

        /// <summary>
        /// Получает или задаёт имя пользователя, добавившего информацию о клиенте-дилере.
        /// </summary>
        public string AddedByName { get; set; }
    }
}