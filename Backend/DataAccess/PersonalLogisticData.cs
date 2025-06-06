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
    /// Класс <c>PersonalLogisticData</c> предоставляет методы для доступа к данным логистики
    /// конкретного сотрудника, реализует бизнес-логику по отображению информации по пользователям и датам.
    /// </summary>
    /// <remarks>
    /// Класс наследуется от <see cref="Logistic"/> и реализует интерфейс <see cref="IPersonalLogistic"/>.
    /// Использует строку подключения из файла конфигурации приложения.
    /// </remarks>
    public class PersonalLogisticData : Logistic, IPersonalLogistic
    {
        /// <summary>
        /// Строка подключения к базе данных, полученная из конфигурации приложения.
        /// </summary>
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        /// <summary>
        /// Выполняет выборку и отображает записи логистики,
        /// относящиеся к пользователю, вошедшему в систему.
        /// </summary>
        /// <param name="username">Имя пользователя, по которому производится фильтрация данных.</param>
        /// <returns>
        /// Объект <see cref="DataTable"/> с данными логистики, соответствующими заданному пользователю.
        /// </returns>
        /// <example>
        /// <code>
        /// PersonalLogisticData personalData = new PersonalLogisticData();
        /// DataTable table = personalData.DisplayLogisticByUsername("ivanov");
        /// </code>
        /// </example>
        public DataTable DisplayLogisticByUsername(string username)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_logistic WHERE employee='" + username + "'";

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
        /// Выполняет выборку и отображает записи логистики по определённой дате и пользователю.
        /// </summary>
        /// <param name="date">Дата, по которой производится фильтрация данных (в формате строки).</param>
        /// <param name="username">Имя пользователя, по которому производится дополнительная фильтрация данных.</param>
        /// <returns>
        /// Объект <see cref="DataTable"/> с данными логистики, соответствующими заданной дате и пользователю.
        /// </returns>
        /// <example>
        /// <code>
        /// PersonalLogisticData personalData = new PersonalLogisticData();
        /// DataTable table = personalData.DisplayLogisticnByDate("2024-06-19", "ivanov");
        /// </code>
        /// </example>
        public DataTable DisplayLogisticnByDate(string date, string username)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_logistic WHERE date='" + date + "' AND employee='" + username + "'";

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