using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic
{
    /// <summary>
    /// Класс, представляющий товар в системе.
    /// Содержит информацию о названии, категории, цене и количестве.
    /// </summary>
    public class Product
    {
        private decimal rate;
        private decimal quantity;

        /// <summary>
        /// Уникальный идентификатор товара.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название товара.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Категория товара (например: "Электроника", "Одежда").
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Артикул или специальный номер товара.
        /// </summary>
        public string SpecialNumber { get; set; }

        /// <summary>
        /// Описание товара.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена товара.
        /// При установке значения выполняется проверка на отрицательное значение.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если цена отрицательная.</exception>
        public decimal Rate
        {
            get => this.rate;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(@"Invalid price!");
                }

                this.rate = value;
            }
        }

        /// <summary>
        /// Количество товара на складе.
        /// При установке значения выполняется проверка на отрицательное значение.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если количество отрицательное.</exception>
        public decimal Quantity
        {
            get => this.quantity;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(@"Invalid quantity!");
                }

                this.quantity = value;
            }
        }

        /// <summary>
        /// Дата добавления товара в систему.
        /// </summary>
        public DateTime AddedDate { get; set; }

        /// <summary>
        /// ID пользователя, добавившего товар.
        /// </summary>
        public int AddedBy { get; set; }

        /// <summary>
        /// Имя пользователя, добавившего товар.
        /// </summary>
        public string AddedByName { get; set; }
    }
}