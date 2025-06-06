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
    /// Класс доступа к данным для работы с логистикой.
    /// Предоставляет функционал для обработки CRUD-операций (создание, чтение, обновление, удаление) над записями логистики,
    /// а также поиск по таблице логистики и выбор продукции для логистики.
    /// Наследуется от <see cref="Logistic"/> и реализует интерфейс <see cref="ICrudLogistic"/>.
    /// </summary>
    public class LogisticData : Logistic, ICrudLogistic
    {
        /// <summary>
        /// Строка подключения к базе данных, полученная из файла конфигурации приложения.
        /// </summary>
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        /// <summary>
        /// Получает все записи по логистике из базы данных.
        /// </summary>
        /// <returns>
        /// <see cref="DataTable"/>, содержащий все строки из таблицы логистики.
        /// </returns>
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                String sql = "SELECT * FROM table_logistic";
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
        /// Добавляет новую запись по логистике в БД.
        /// </summary>
        /// <param name="logistic">
        /// Объект <see cref="Logistic"/>, содержащий информацию для записи.
        /// </param>
        /// <returns>
        /// <c>true</c> — если запись успешно добавлена, иначе <c>false</c>.
        /// </returns>
        public bool Insert(Logistic logistic)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO table_logistic (employee, first_name_employee, last_name_employee, address, contact, date, description, price, added_date, added_by, added_by_name) " +
                    "VALUES (@employee, @first_name_employee, @last_name_employee, @address, @contact, @date, @description, @price, @added_date, @added_by, @added_by_name)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@employee", logistic.Empleyee);
                cmd.Parameters.AddWithValue("@first_name_employee", logistic.FirstNameEmployee);
                cmd.Parameters.AddWithValue("@last_name_employee", logistic.LastNameEmployee);
                cmd.Parameters.AddWithValue("@address", logistic.Address);
                cmd.Parameters.AddWithValue("@contact", logistic.Contact);
                cmd.Parameters.AddWithValue("@date", logistic.Date);
                cmd.Parameters.AddWithValue("@description", logistic.Description);
                cmd.Parameters.AddWithValue("@price", logistic.Price);
                cmd.Parameters.AddWithValue("@added_date", logistic.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", logistic.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", logistic.AddedByName);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                isSuccess = rows > 0;
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
        /// Обновляет существующую запись по логистике в базе данных.
        /// </summary>
        /// <param name="logistic">
        /// Объект <see cref="Logistic"/>, содержащий обновлённые данные записи.
        /// </param>
        /// <returns>
        /// <c>true</c>, если обновление прошло успешно; <c>false</c> — в случае сбоя или отсутствия изменений.
        /// </returns>
        public bool Update(Logistic logistic)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE table_logistic SET employee=@employee, first_name_employee=@first_name_employee, last_name_employee=@last_name_employee, address=@address, contact=@contact, date=@date, description=@description, price=@price, added_date=@added_date, added_by=@added_by, added_by_name=@added_by_name WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@employee", logistic.Empleyee);
                cmd.Parameters.AddWithValue("@first_name_employee", logistic.FirstNameEmployee);
                cmd.Parameters.AddWithValue("@last_name_employee", logistic.LastNameEmployee);
                cmd.Parameters.AddWithValue("@address", logistic.Address);
                cmd.Parameters.AddWithValue("@contact", logistic.Contact);
                cmd.Parameters.AddWithValue("@date", logistic.Date);
                cmd.Parameters.AddWithValue("@description", logistic.Description);
                cmd.Parameters.AddWithValue("@price", logistic.Price);
                cmd.Parameters.AddWithValue("@added_date", logistic.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", logistic.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", logistic.AddedByName);
                cmd.Parameters.AddWithValue("@id", logistic.Id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                isSuccess = rows > 0;
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
        /// Удаляет запись логистики по идентификатору.
        /// </summary>
        /// <param name="logistic">
        /// Объект <see cref="Logistic"/>, у которого заполнено свойство <c>Id</c> для удаления.
        /// </param>
        /// <returns>
        /// <c>true</c>, если удаление прошло успешно; иначе <c>false</c>.
        /// </returns>
        public bool Delete(Logistic logistic)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM table_logistic WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", logistic.Id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                isSuccess = rows > 0;
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
        /// Выполняет поиск записей логистики по заданному ключевому слову.
        /// Осуществляет поиск в полях <c>id</c>, <c>employee</c> и <c>date</c>.
        /// </summary>
        /// <param name="keywords">
        /// Ключевое слово для поиска среди записей.
        /// </param>
        /// <returns>
        /// <see cref="DataTable"/> с найденными по критерию строками.
        /// </returns>
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_logistic WHERE id LIKE '%" + keywords + "%' OR employee LIKE '%" + keywords + "%' OR date LIKE '%" + keywords + "%'";
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
        /// Находит продукцию для логистических операций по ключевому слову.
        /// Используется для получения сведений о продукте для последующей работы в рамках логистики.
        /// </summary>
        /// <param name="keyword">
        /// Ключевое слово для поиска в названиях или специальных номерах продукции.
        /// </param>
        /// <returns>
        /// Объект <see cref="Product"/> с найденной продукцией или с пустыми полями, если ничего не найдено.
        /// </returns>
        public Product GetProductsForLogistic(string keyword)
        {
            Product product = new Product();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id, name, special_number, rate, qty FROM table_products WHERE name LIKE '%" + keyword + "%' OR special_number LIKE '%" + keyword + "%'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    product.Id = int.Parse(dt.Rows[0]["id"].ToString());
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
    }
}