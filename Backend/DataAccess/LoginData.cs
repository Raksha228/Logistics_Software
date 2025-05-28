using Backend.BusinessLogic;
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
    public class LoginData
    {
        //Връзка с базата данни
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Метод за влизане в системата
        public bool loginCheck(Login login)
        {
            bool isSuccess = false;

            //Свързванне с базата данни
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Създаване на query за взимане на данни от базата данни
                string sql = "SELECT * FROM table_users WHERE username=@username AND password=@password AND user_type=@user_type";

               

                //Вземане на стойностите с изпълнението на query-то
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", login.Username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", login.Password ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@user_type", login.UserType ?? (object)DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                //Проверка дали имаме редове в таблицата
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
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
    }
}
