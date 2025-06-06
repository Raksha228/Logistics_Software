using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic
{
    /// <summary>
    /// Класс, представляющий финансовую транзакцию в системе.
    /// Содержит информацию о платежах, налогах, скидках и деталях операции.
    /// </summary>
    public class Transaction
    {
        private decimal tax;
        private decimal discount;
        private decimal paidAmount;

        /// <summary>
        /// Уникальный идентификатор транзакции.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тип транзакции (например: "Продажа", "Возврат", "Пополнение").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// ID дилера или клиента, связанного с транзакцией.
        /// </summary>
        public int DealerCustomerId { get; set; }

        /// <summary>
        /// Описание транзакции (дополнительные сведения о операции).
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Общая сумма транзакции (включая налоги и скидки).
        /// </summary>
        public decimal GrandTotal { get; set; }

        /// <summary>
        /// Дата и время совершения транзакции.
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Сумма налога для транзакции.
        /// Значение должно быть положительным числом.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается при попытке установить отрицательное или нулевое значение.</exception>
        public decimal Tax
        {
            get => this.tax;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(@"Invalid tax!");
                }

                this.tax = value;
            }
        }

        /// <summary>
        /// Сумма скидки по транзакции.
        /// Значение должно быть положительным числом.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается при попытке установить отрицательное или нулевое значение.</exception>
        public decimal Discount
        {
            get => this.discount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(@"Invalid discount!");
                }

                this.discount = value;
            }
        }

        /// <summary>
        /// Сумма, фактически оплаченная по транзакции.
        /// Значение должно быть положительным числом.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается при попытке установить отрицательное или нулевое значение.</exception>
        public decimal PaidAmount
        {
            get => this.paidAmount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(@"Invalid Paid Amount!");
                }

                this.paidAmount = value;
            }
        }

        /// <summary>
        /// Сумма к возврату (если оплата превышает общую сумму).
        /// </summary>
        public decimal ReturnAmount { get; set; }

        /// <summary>
        /// ID пользователя, создавшего транзакцию.
        /// </summary>
        public int AddedBy { get; set; }

        /// <summary>
        /// Имя пользователя, создавшего транзакцию.
        /// </summary>
        public string AddedByName { get; set; }

        /// <summary>
        /// Детализированные данные о транзакции (список товаров/услуг).
        /// </summary>
        public DataTable TransactionDetails { get; set; }
    }
}