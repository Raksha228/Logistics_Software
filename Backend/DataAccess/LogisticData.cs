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
    public class LogisticData : Logistic, ICrudLogistic
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Получение данных
        public DataTable Select()
        {
            //Установка соединения с базой данных
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Создайте временную таблицу для хранения данных
            DataTable dt = new DataTable();

            try
            {
                //Создайте запрос, который берет все данные из таблицы
                String sql = "SELECT * FROM table_logistic";

                //Создайте команду для выполнения запроса
                SqlCommand cmd = new SqlCommand(sql, conn);

                //создайте адаптер для хранения информации и заполните таблицу
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

        //Вставка данных
        public bool Insert(Logistic logistic)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO table_logistic (employee, first_name_employee, last_name_employee, address, contact, date, description, price, added_date, added_by, added_by_name) VALUES (@employee, @first_name_employee, @last_name_employee, @address, @contact, @date, @description, @price, @added_date, @added_by, @added_by_name)";

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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        //Редактировать данные
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

        //Получение продукции для логистики
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
