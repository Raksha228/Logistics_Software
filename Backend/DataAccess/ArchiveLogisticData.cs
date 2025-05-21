using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Backend.BusinessLogic;
using Newtonsoft.Json;

namespace Backend.DataAccess
{
    public class ArchiveLogisticData
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5000/api/archiveLogistics"; // URL вашего API

        public ArchiveLogisticData()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        // Получение всех логистических операций
        public DataTable DisplayAllLogistics()
        {
            try
            {
                var response = _httpClient.GetAsync("").Result;
                response.EnsureSuccessStatusCode();

                var json = response.Content.ReadAsStringAsync().Result;
                var dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                return dataTable;
            }
            catch (Exception ex)
            {
                // В WPF лучше использовать механизмы логирования вместо MessageBox
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
                return new DataTable();
            }
        }

        // Добавление логистической операции
        public bool Insert(Logistic logistic)
        {
            try
            {
                var response = _httpClient.PostAsJsonAsync("", logistic).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении данных: {ex.Message}");
                return false;
            }
        }

        // Получение логистических операций по дате
        public DataTable DisplayLogisticnByDate(string date)
        {
            try
            {
                var response = _httpClient.GetAsync($"?date={date}").Result;
                response.EnsureSuccessStatusCode();

                var json = response.Content.ReadAsStringAsync().Result;
                var dataTable = JsonConvert.DeserializeObject<DataTable>(json);

                return dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных по дате: {ex.Message}");
                return new DataTable();
            }
        }
    }
}




























//using Backend.BusinessLogic;
//using System;
//using System.Windows;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Backend.Interfaces;
//using Backend.Utils;



//namespace Backend.DataAccess
//{
//    public class ArchiveLogisticData
//    {
//        //static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
//        static string myconnstrng = AppConfiguration.ConnectionString;

//        //Визуализация всех логистических операций
//        public DataTable DisplayAllLogistics()
//        {
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            DataTable dt = new DataTable();

//            try
//            {
//                String sql = "SELECT * FROM table_logistic_archive";

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

//                conn.Open();

//                adapter.Fill(dt);
//            }
//            catch (Exception ex)
//            {
//                //MessageBox.Show(ex.Message);

//            }
//            finally
//            {
//                conn.Close();
//            }

//            return dt;
//        }

//        //Добавить
//        public bool Insert(Logistic logistic)
//        {
//            bool isSuccess = false;

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            try
//            {
//                String sql = "INSERT INTO table_logistic_archive (employee, first_name_employee, last_name_employee, address, contact, date, description, price, added_date, added_by, added_by_name) VALUES (@employee, @first_name_employee, @last_name_employee, @address, @contact, @date, @description, @price, @added_date, @added_by, @added_by_name)";

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                cmd.Parameters.AddWithValue("@employee", logistic.Empleyee);
//                cmd.Parameters.AddWithValue("@first_name_employee", logistic.FirstNameEmployee);
//                cmd.Parameters.AddWithValue("@last_name_employee", logistic.LastNameEmployee);
//                cmd.Parameters.AddWithValue("@address", logistic.Address);
//                cmd.Parameters.AddWithValue("@contact", logistic.Contact);
//                cmd.Parameters.AddWithValue("@date", logistic.Date);
//                cmd.Parameters.AddWithValue("@description", logistic.Description);
//                cmd.Parameters.AddWithValue("@price", logistic.Price);
//                cmd.Parameters.AddWithValue("@added_date", logistic.AddedDate);
//                cmd.Parameters.AddWithValue("@added_by", logistic.AddedBy);
//                cmd.Parameters.AddWithValue("@added_by_name", logistic.AddedByName);

//                conn.Open();

//                int rows = cmd.ExecuteNonQuery();

//                if (rows > 0)
//                {
//                    isSuccess = true;
//                }
//                else
//                {
//                    isSuccess = false;
//                }
//            }
//            catch (Exception ex)
//            {
//                //MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                conn.Close();
//            }

//            return isSuccess;
//        }

//        //Визуализация логистических операций по датам
//        public DataTable DisplayLogisticnByDate(string date)
//        {
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            DataTable dt = new DataTable();

//            try
//            {
//                string sql = "SELECT * FROM table_logistic_archive WHERE date='" + date + "'";

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

//                conn.Open();

//                adapter.Fill(dt);
//            }
//            catch (Exception ex)
//            {
//                //MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                conn.Close();
//            }

//            return dt;
//        }
//    }
//}
