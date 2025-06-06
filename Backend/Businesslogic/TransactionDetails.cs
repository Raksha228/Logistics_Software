using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic
{
    /// <summary>
    /// Класс, представляющий детали транзакции.
    /// Содержит информацию о товарах, их количестве, ценах и участниках сделки.
    /// </summary>
    public class TransactionDetails
    {
        /// <summary>
        /// Уникальный идентификатор детали транзакции.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор товара, связанного с транзакцией.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Цена единицы товара на момент совершения транзакции.
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Количество товара в транзакции.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Общая стоимость по позиции (Rate * Quantity).
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Идентификатор дилера или клиента, связанного с данной позицией транзакции.
        /// </summary>
        public int DealerCustomerId { get; set; }

        /// <summary>
        /// Дата и время добавления позиции в транзакцию.
        /// </summary>
        public DateTime AddedDate { get; set; }

        /// <summary>
        /// Идентификатор пользователя, добавившего позицию в транзакцию.
        /// </summary>
        public int AddedBy { get; set; }

        /// <summary>
        /// Имя пользователя, добавившего позицию в транзакцию.
        /// </summary>
        public string AddedByName { get; set; }
    }
}