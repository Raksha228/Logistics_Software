using Backend.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.Utils;
namespace Backend.DataAccess
{
    public class LoginData
    {
        //Подключение к базе данных
        //static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        static string myconnstrng = AppConfiguration.ConnectionString;

        //Метод входа в систему
        public bool loginCheck(Login login)
        {
            bool isSuccess = false;

            //Подключение к базе данных
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Создайте запрос для получения данных из базы данных
                string sql = "SELECT * FROM table_users WHERE username=@username AND password=@password AND user_type=@user_type";

                //Получение значений с помощью выполнения запроса
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", login.Username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", login.Password ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@user_type", login.UserType ?? (object)DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                //Проверьте, есть ли у нас строки в таблице
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
               // MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
    }
}
