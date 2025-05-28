using Backend.BusinesLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using Backend.Interfaces;
namespace Backend.DataAccess
{
    public class UserData:User,ICrudUser
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Вземане на данни
        public DataTable Select()
        {
            //Връзка с базата данни
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Задържане на данните в таблица
            DataTable dt = new DataTable();
            try
            {
                //query за вземане на данните от таблицата с потребителите
                String sql = "SELECT * FROM table_users";

                //Изпълнение на query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Получаване на данните в адаптер
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Отваряне на връзката
                conn.Open();

                //Попълване на данните в таблицата
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Грешка
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                //Затваряне на връзката
                conn.Close();
            }
            //Връщане на данните
            return dt;


        }

        //Добавяне на данни
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

                //Ако query-то е изпълнено ще върне стойност по-голяма от 0 ако не е изпълнено ще върне стойност по малка от 0
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
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Редактиране на данни
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
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Изтриване на данни
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
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Търсене на данни
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
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Вземане на id от влезналия потребител
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
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return user;
        }

        //Търсене на потребител за логистиката
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

                //Ако успешно имам намерени данни ние ги запазваме в обекта от business logic 
                if (dt.Rows.Count > 0)
                {
                    user.FirstName = dt.Rows[0]["first_name"].ToString();
                    user.LastName = dt.Rows[0]["last_name"].ToString();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return user;
        }
    }
}
