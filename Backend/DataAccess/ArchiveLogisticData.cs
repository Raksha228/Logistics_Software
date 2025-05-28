using Backend.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
////using System.Windows.Forms;
using Backend.Interfaces;

namespace Backend.DataAccess
{
    public class ArchiveLogisticData
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Визуализация на всички логистични операции
        public DataTable DisplayAllLogistics()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                String sql = "SELECT * FROM table_logistic_archive";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                ////MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        //Добавяне
        public bool Insert(Logistic logistic)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO table_logistic_archive (employee, first_name_employee, last_name_employee, address, contact, date, description, price, added_date, added_by, added_by_name) VALUES (@employee, @first_name_employee, @last_name_employee, @address, @contact, @date, @description, @price, @added_date, @added_by, @added_by_name)";

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
                ////MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        //Визуализация на логистични операции по дата
        public DataTable DisplayLogisticnByDate(string date)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_logistic_archive WHERE date='" + date + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                ////MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
    }
}
