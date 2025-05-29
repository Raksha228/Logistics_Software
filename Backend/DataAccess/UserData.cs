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
    public class UserData:User,ICrudUser
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Сбор данных
        public DataTable Select()
        {
            //Подключение к базе данных
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Сохранение данных в таблице
            DataTable dt = new DataTable();
            try
            {
                //Запрос для получения данных из таблицы пользователей
                String sql = "SELECT * FROM table_users";

                //Выполните запрос
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Получение данных в адаптер
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Открытие ссылки
                conn.Open();

                //Заполните данные в таблице
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Ошибка
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Закрытие соединения
                conn.Close();
            }
            //Верните данные
            return dt;


        }

        //Добавить данные
        public bool Insert(User user)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO table_users (first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by, added_by_name) VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by, @added_by_name)";

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

                //Если запрос будет выполнен, он вернет значение больше 0, если не будет выполнен, вернет значение меньше 0

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

        //Редактировать данные
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

        //Удаление данных
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

        //Данные поиска
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM table_users WHERE id LIKE '%" + keywords + "%' OR first_name LIKE '%" + keywords + "%' OR last_name LIKE '%" + keywords + "%' OR username LIKE '%" + keywords + "%'";

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

        //Получение идентификатора от вошедшего пользователя
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

        //Поиск пользователей по логистике
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

                //Если я успешно нашел данные, мы сохраняем их в объекте из бизнес-логики
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
