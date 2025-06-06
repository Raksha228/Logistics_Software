using Backend.BusinesLogic;
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
    /// Класс для работы с данными пользователей.
    /// </summary>
    public class UserData : User, ICrudUser
    {
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        /// <summary>
        /// Получает данные всех пользователей из базы данных.
        /// </summary>
        /// <returns>Возвращает <see cref="DataTable"/> с данными о пользователях.</returns>
        public DataTable Select()
        {
            // Подключение к базе данных
            SqlConnection conn = new SqlConnection(myconnstrng);
            // Сохранение данных в таблице
            DataTable dt = new DataTable();
            try
            {
                // Запрос для получения данных из таблицы пользователей
                string sql = "SELECT * FROM table_users";

                // Выполняем запрос
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Получаем данные в адаптер
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Открываем соединение
                conn.Open();
                // Заполняем данные в таблице
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Ошибка
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Закрытие соединения
                conn.Close();
            }
            // Возвращаем данные
            return dt;
        }

        /// <summary>
        /// Добавляет нового пользователя в базу данных.
        /// </summary>
        /// <param name="user">Объект типа User, содержащий информацию о пользователе.</param>
        /// <returns>Возвращает true, если пользователь был успешно добавлен; в противном случае — false.</returns>
        public bool Insert(User user)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "INSERT INTO table_users (first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by, added_by_name) VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by, @added_by_name)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", user.FirstName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@last_name", user.LastName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@email", user.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@username", user.Username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", user.Password ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@contact", user.Contact ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@address", user.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@gender", user.Gender ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@user_type", user.UserType ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@added_date", user.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", user.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", user.AddedByName);

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
        /// Обновляет данные существующего пользователя в базе данных.
        /// </summary>
        /// <param name="user">Объект типа User, содержащий обновленную информацию о пользователе.</param>
        /// <returns>Возвращает true, если данные пользователя были успешно обновлены; в противном случае — false.</returns>
        public bool Update(User user)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE table_users SET first_name=@first_name, last_name=@last_name, email=@email, username=@username, password=@password, contact=@contact, address=@address, gender=@gender, user_type=@user_type, added_date=@added_date, added_by=@added_by, added_by_name=@added_by_name WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                cmd.Parameters.AddWithValue("@last_name", user.LastName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@contact", user.Contact);
                cmd.Parameters.AddWithValue("@address", user.Address);
                cmd.Parameters.AddWithValue("@gender", user.Gender);
                cmd.Parameters.AddWithValue("@user_type", user.UserType);
                cmd.Parameters.AddWithValue("@added_date", user.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", user.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", user.AddedByName);
                cmd.Parameters.AddWithValue("@id", user.Id);

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
        /// Удаляет пользователя из базы данных.
        /// </summary>
        /// <param name="user">Объект типа User, представляющий пользователя, который нужно удалить.</param>
        /// <returns>Возвращает true, если пользователь был успешно удален; в противном случае — false.</returns>
        public bool Delete(User user)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM table_users WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", user.Id);

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
        /// Ищет пользователей в базе данных по заданным ключевым словам.
        /// </summary>
        /// <param name="keywords">Строка с ключевыми словами для поиска.</param>
        /// <returns>Возвращает <see cref="DataTable"/> с данными пользователей, соответствующих критериям поиска.</returns>
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM table_users WHERE id LIKE '%" + keywords + "%' OR first_name LIKE '%" + keywords + "%' OR last_name LIKE '%" + keywords + "%' OR username LIKE '%" + keywords + "%'";

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
        /// Получает идентификатор пользователя по его имени пользователя.
        /// </summary>
        /// <param name="username">Имя пользователя, для которого требуется получить идентификатор.</param>
        /// <returns>Возвращает объект <see cref="User"/> с идентификатором пользователя.</returns>
        public User GetIDFromUsername(string username)
        {
            User user = new User();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id FROM table_users WHERE username='" + username + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();

                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    user.Id = int.Parse(dt.Rows[0]["id"].ToString());
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
            return user;
        }

        /// <summary>
        /// Ищет пользователей по логистике по заданному ключевому слову.
        /// </summary>
        /// <param name="keyword">Ключевое слово для поиска.</param>
        /// <returns>Возвращает объект <see cref="User"/> с данными найденного пользователя.</returns>
        public User SearchUserForLogistic(string keyword)
        {
            User user = new User();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT first_name, last_name from table_users WHERE username LIKE '%" + keyword + "%'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();

                adapter.Fill(dt);

                // Если успешно найдены данные, сохраняем их в объекте
                if (dt.Rows.Count > 0)
                {
                    user.FirstName = dt.Rows[0]["first_name"].ToString();
                    user.LastName = dt.Rows[0]["last_name"].ToString();
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
            return user;
        }
    }
}