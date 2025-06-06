using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic
{
    /// <summary>
    /// Представляет категорию товара.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Получает или задаёт идентификатор категории.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задаёт название категории.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Получает или задаёт описание категории.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Получает или задаёт дату добавления категории.
        /// </summary>
        public DateTime AddedDate { get; set; }

        /// <summary>
        /// Получает или задаёт идентификатор пользователя, добавившего категорию.
        /// </summary>
        public int AddedBy { get; set; }

        /// <summary>
        /// Получает или задаёт имя пользователя, добавившего категорию.
        /// </summary>
        public string AddedByName { get; set; }
    }
}