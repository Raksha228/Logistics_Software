using Backend.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Backend.Interfaces;

namespace Backend.DataAccess
{
    /// <summary>
    /// Класс <c>ProductData</c> реализует доступ к данным о продуктах, а также
    /// позволяет выполнять операции CRUD и работать с количеством продукции.
    /// </summary>
    /// <remarks>
    /// Этот класс наследуется от <see cref="Product"/> и реализует интерфейсы <see cref="ICrudProduct"/> и <see cref="IQuantityProduct"/>.
    /// В качестве источника строки подключения используется конфигурационный файл приложения.
    /// </remarks>
    public class ProductData : Product, ICrudProduct, IQuantityProduct
    {
        /// <summary>
        /// Строка подключения к базе данных, полученная из конфигурационного файла приложения.
        /// </summary>
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        /// <summary>
        /// Выбирает и возвращает все записи по продуктам из таблицы <c>table_products</c>.
        /// </summary>
        /// <returns>
        /// Объект <see cref="DataTable"/>, содержащий всю информацию о продуктах.
        /// </returns>
        public DataTable Select()
        {
            //Установка соединения с базой данных
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Создайте временную таблицу для хранения данных
            DataTable dt = new DataTable();

            try
            {
                //Создайте запрос, который берет все данные из таблицы
                String sql = "SELECT * FROM table_products";

                //Создайте команду для выполнения запроса
                SqlCommand cmd = new SqlCommand(sql, conn);

                //создайте адаптер для хранения информации и заполните таблицу
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        /// <summary>
        /// Вставляет новый продукт в таблицу <c>table_products</c>.
        /// </summary>
        /// <param name="product">Объект <see cref="Product"/>, содержащий информацию о добавляемом продукте.</param>
        /// <returns>
        /// <c>true</c>, если вставка выполнена успешно; в противном случае <c>false</c>.
        /// </returns>
        /// <example>
        /// <code>
        /// ProductData p = new ProductData();
        /// bool success = p.Insert(new Product { Name="Шуруп", Category="Крепеж", ... });
        /// </code>
        /// </example>
        public bool Insert(Product product)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO table_products (name, category, special_number, description, rate, qty, added_date, added_by, added_by_name) VALUES (@name, @category, @special_number, @description, @rate, @qty, @added_date, @added_by, @added_by_name)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@category", product.Category);
                cmd.Parameters.AddWithValue("@special_number", product.SpecialNumber);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@rate", product.Rate);
                cmd.Parameters.AddWithValue("@qty", product.Quantity);
                cmd.Parameters.AddWithValue("@added_date", product.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", product.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", product.AddedByName);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        /// <summary>
        /// Редактирует данные существующего продукта на основании его идентификатора.
        /// </summary>
        /// <param name="product">Объект <see cref="Product"/>, содержащий обновлённые данные продукта.</param>
        /// <returns>
        /// <c>true</c>, если обновление выполнено успешно; иначе <c>false</c>.
        /// </returns>
        public bool Update(Product product)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE table_products SET name=@name, category=@category, special_number=@special_number, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by, added_by_name=@added_by_name WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@category", product.Category);
                cmd.Parameters.AddWithValue("@special_number", product.SpecialNumber);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@rate", product.Rate);
                cmd.Parameters.AddWithValue("@qty", product.Quantity);
                cmd.Parameters.AddWithValue("@added_date", product.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", product.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", product.AddedByName);
                cmd.Parameters.AddWithValue("@id", product.Id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        /// <summary>
        /// Удаляет продукт из базы данных по идентификатору.
        /// </summary>
        /// <param name="product">Объект <see cref="Product"/>, для которого требуется удалить запись (используется свойство <c>Id</c>).</param>
        /// <returns>
        /// <c>true</c>, если удаление успешно; иначе <c>false</c>.
        /// </returns>
        public bool Delete(Product product)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM table_products WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", product.Id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        /// <summary>
        /// Выполняет поиск продуктов по ключевым словам в полях "special_number", "name" или "category".
        /// </summary>
        /// <param name="keywords">Ключевые слова для поиска.</param>
        /// <returns>
        /// Объект <see cref="DataTable"/>, содержащий подходящие записи.
        /// </returns>
        /// <example>
        /// <code>
        /// DataTable result = productData.Search("саморез");
        /// </code>
        /// </example>
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_products WHERE special_number LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        /// <summary>
        /// Получает информацию о продукте для сделки по его ключу (имя или спец. номер).
        /// </summary>
        /// <param name="keyword">Ключ для поиска (имя или специальный номер продукта).</param>
        /// <returns>
        /// Объект <see cref="Product"/>, заполненный найденными данными (или пустой, если не найдено).
        /// </returns>
        public Product GetProductsForTransaction(string keyword)
        {
            Product product = new Product();

            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT name, special_number, rate, qty FROM table_products WHERE special_number LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    product.Name = dt.Rows[0]["name"].ToString();
                    product.SpecialNumber = dt.Rows[0]["special_number"].ToString();
                    product.Rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    product.Quantity = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return product;
        }

        /// <summary>
        /// Получает идентификатор продукта на основе его имени.
        /// </summary>
        /// <param name="productName">Имя продукта.</param>
        /// <returns>
        /// Объект <see cref="Product"/> c заполненным свойством <c>Id</c>. Если продукт не найден, Id будет равен 0.
        /// </returns>
        public Product GetProductIDFromName(string productName)
        {
            Product product = new Product();

            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id FROM table_products WHERE name='" + productName + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    product.Id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return product;
        }

        /// <summary>
        /// Получает текущее количество конкретного продукта по его идентификатору.
        /// </summary>
        /// <param name="productID">Идентификатор продукта.</param>
        /// <returns>
        /// Числовое значение количества продукта (тип <see cref="decimal"/>).
        /// </returns>
        public decimal GetProductQty(int productID)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            decimal qty = 0;

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT qty FROM table_products WHERE id = " + productID;

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return qty;
        }

        /// <summary>
        /// Обновляет значение количества конкретного продукта.
        /// </summary>
        /// <param name="productID">Идентификатор продукта.</param>
        /// <param name="quantity">Новое значение количества.</param>
        /// <returns>
        /// <c>true</c>, если количество обновлено успешно; иначе <c>false</c>.
        /// </returns>
        public bool UpdateQuantity(int productID, decimal quantity)
        {
            bool success = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE table_products SET qty=@qty WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@qty", quantity);
                cmd.Parameters.AddWithValue("@id", productID);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }

        /// <summary>
        /// Увеличивает количество продукта на заданное значение.
        /// </summary>
        /// <param name="productID">Идентификатор продукта.</param>
        /// <param name="increaseQuantity">Величина, на которую нужно увеличить количество.</param>
        /// <returns>
        /// <c>true</c>, если количество успешно увеличено; иначе <c>false</c>.
        /// </returns>
        public bool IncreaseProduct(int productID, decimal increaseQuantity)
        {
            bool success = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Принимая текущее количество
                decimal currentQuantity = GetProductQty(productID);

                //Увеличить текущее количество за счет того, что мы уже купили
                decimal newQuantity = currentQuantity + increaseQuantity;

                //Изменение количества
                success = UpdateQuantity(productID, newQuantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        /// <summary>
        /// Уменьшает количество продукта на заданную величину.
        /// </summary>
        /// <param name="productID">Идентификатор продукта.</param>
        /// <param name="decreaseQuantity">Величина, на которую нужно уменьшить количество.</param>
        /// <returns>
        /// <c>true</c>, если количество успешно уменьшено; иначе <c>false</c>.
        /// </returns>
        public bool DecreaseProduct(int productID, decimal decreaseQuantity)
        {
            bool success = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                decimal currentQuantity = GetProductQty(productID);

                decimal newQuantity = currentQuantity - decreaseQuantity;

                success = UpdateQuantity(productID, newQuantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        /// <summary>
        /// Получает и отображает все продукты, относящиеся к указанной категории.
        /// </summary>
        /// <param name="category">Название категории продуктов.</param>
        /// <returns>
        /// Объект <see cref="DataTable"/>, содержащий продукты выбранной категории.
        /// </returns>
        public DataTable DisplayProductsByCategory(string category)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_products WHERE category='" + category + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
    }
}