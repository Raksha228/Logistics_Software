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
    /// Предоставляет методы для взаимодействия с архивом логистических операций:
    /// добавление новых записей и получение архивных данных.
    /// </summary>
    public class ArchiveLogisticData
    {
        /// <summary>
        /// Строка подключения к базе данных, полученная из конфигурационного файла приложения.
        /// </summary>
        private static readonly string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        /// <summary>
        /// Получает все архивные записи логистических операций из базы данных.
        /// </summary>
        /// <returns>
        /// Объект <see cref="DataTable"/>, содержащий все записи архива логистики.
        /// </returns>
        /// <remarks>
        /// Каждая строка полученного <see cref="DataTable"/> соответствует строке из таблицы table_logistic_archive.
        /// В случае возникновения ошибки пользователю будет показано окно с сообщением об ошибке.
        /// </remarks>
        public DataTable DisplayAllLogistics()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_logistic_archive";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // В случае ошибки выводится сообщение пользователю
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        /// <summary>
        /// Добавляет новую запись логистической операции в архив.
        /// </summary>
        /// <param name="logistic">Экземпляр <see cref="Logistic"/>, содержащий данные операции.</param>
        /// <returns>
        /// true, если добавление прошло успешно; иначе false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если параметр <paramref name="logistic"/> равен null.
        /// </exception>
        /// <remarks>
        /// Свойства объекта <paramref name="logistic"/> сопоставляются с соответствующими столбцами таблицы архива.
        /// В случае ошибки пользователю будет показано окно с сообщением.
        /// </remarks>
        public bool Insert(Logistic logistic)
        {
            if (logistic == null)
                throw new ArgumentNullException(nameof(logistic));

            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "INSERT INTO table_logistic_archive (employee, first_name_employee, last_name_employee, address, contact, date, description, price, added_date, added_by, added_by_name) VALUES (@employee, @first_name_employee, @last_name_employee, @address, @contact, @date, @description, @price, @added_date, @added_by, @added_by_name)";

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
                // В случае ошибки выводится сообщение пользователю
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        /// <summary>
        /// Получает логистические операции по заданной дате.
        /// </summary>
        /// <param name="date">
        /// Дата в формате строки (например, "2024-06-01"), соответствующая хранимому в базе формату.
        /// </param>
        /// <returns>
        /// Объект <see cref="DataTable"/>, содержащий найденные записи логистики по указанной дате.
        /// </returns>
        /// <remarks>
        /// Фильтрация производится по совпадению значения поля "date" в таблице архива.
        /// В случае ошибки пользователю будет показано сообщение.
        /// </remarks>
        public DataTable DisplayLogisticnByDate(string date)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_logistic_archive WHERE date=@date";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // В случае ошибки выводится сообщение пользователю
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