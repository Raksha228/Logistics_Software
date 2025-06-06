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
    /// Класс для работы с аутентификацией пользователей.
    /// Предоставляет методы для проверки учётных данных пользователя в базе данных.
    /// </summary>
    public class LoginData
    {
        /// <summary>
        /// Строка подключения к базе данных, получаемая из файла конфигурации приложения.
        /// </summary>
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        /// <summary>
        /// Проверяет корректность учётных данных пользователя для входа в систему.
        /// Запрос обращается к таблице пользователей и сверяет данные по имени пользователя, паролю и типу пользователя.
        /// </summary>
        /// <param name="login">
        /// Объект <see cref="Login"/>, содержащий имя пользователя (<c>Username</c>), пароль (<c>Password</c>) и тип пользователя (<c>UserType</c>) для проверки.
        /// </param>
        /// <returns>
        /// <c>true</c> если в таблице найден пользователь с указанными данными, иначе <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Если учётная запись не найдена либо возникает ошибка при обработке запроса, метод возвращает <c>false</c>.
        /// </remarks>
        public bool loginCheck(Login login)
        {
            bool isSuccess = false;

            // Подключение к базе данных
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                // Создать SQL-запрос для проверки существования пользователя
                string sql = "SELECT * FROM table_users WHERE username=@username AND password=@password AND user_type=@user_type";

                // Подготовить команду с параметрами
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", login.Username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", login.Password ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@user_type", login.UserType ?? (object)DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                // Проверка наличия пользователя с такими учётными данными
                if (dt.Rows.Count > 0)
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
    }
}