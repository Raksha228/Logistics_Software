using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic
{
    /// <summary>
    /// Класс, представляющий логистическую запись (перевозку, доставку или связанную операцию).
    /// Содержит информацию о сотруднике, контактах, адресе, стоимости и датах.
    /// </summary>
    public class Logistic
    {
        private string contact;

        /// <summary>
        /// Уникальный идентификатор записи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ФИО или идентификатор сотрудника, связанного с логистикой.
        /// </summary>
        public string Empleyee { get; set; }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string FirstNameEmployee { get; set; }

        /// <summary>
        /// Фамилия сотрудника.
        /// </summary>
        public string LastNameEmployee { get; set; }

        /// <summary>
        /// Контактный номер телефона или email.
        /// При установке значения выполняется проверка на минимальную длину (не менее 10 символов).
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если контакт слишком короткий (меньше 10 символов).</exception>
        public string Contact
        {
            get => this.contact;
            set
            {
                //Проверка контакта
                if (value.Length <= 9)
                {
                    throw new ArgumentException(@"Invalid contact!");
                }

                this.contact = value;
            }
        }

        /// <summary>
        /// Адрес доставки или место назначения.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Дата выполнения логистической операции (в строковом формате).
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Описание операции (например, тип груза или особые условия).
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Стоимость услуги (в денежном выражении).
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата добавления записи в систему.
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