using System;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Backend.BusinessLogic;
using Backend.Interfaces;
using Newtonsoft.Json;

namespace Backend.DataAccess
{
    public class DealerCustomerData : DealerCustomer, ICrudDealerCustomer
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5000/api/dealerCustomers"; // URL вашего API

        public DealerCustomerData()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        // Получение всех дилеров/клиентов
        public DataTable Select()
        {
            try
            {
                var response = _httpClient.GetAsync("").Result;
                response.EnsureSuccessStatusCode();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<DataTable>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении дилеров/клиентов: {ex.Message}");
                return new DataTable();
            }
        }

        // Добавление нового дилера/клиента
        public bool Insert(DealerCustomer dealerAndCustomer)
        {
            try
            {
                var response = _httpClient.PostAsJsonAsync("", dealerAndCustomer).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении дилера/клиента: {ex.Message}");
                return false;
            }
        }

        // Обновление данных дилера/клиента
        public bool Update(DealerCustomer dealerAndCustomer)
        {
            try
            {
                var response = _httpClient.PutAsJsonAsync($"{dealerAndCustomer.Id}", dealerAndCustomer).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении дилера/клиента: {ex.Message}");
                return false;
            }
        }

        // Удаление дилера/клиента
        public bool Delete(DealerCustomer dealerAndCustomer)
        {
            try
            {
                var response = _httpClient.DeleteAsync($"{dealerAndCustomer.Id}").Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении дилера/клиента: {ex.Message}");
                return false;
            }
        }

        // Поиск дилеров/клиентов
        public DataTable Search(string keyword)
        {
            try
            {
                var response = _httpClient.GetAsync($"/search?keyword={keyword}").Result;
                response.EnsureSuccessStatusCode();

                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<DataTable>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске дилеров/клиентов: {ex.Message}");
                return new DataTable();
            }
        }

        // Поиск дилера/клиента для транзакции
        public DealerCustomer SearchDealerCustomerForTransaction(string keyword)
        {
            try
            {
                var response = _httpClient.GetAsync($"/searchForTransaction?keyword={keyword}").Result;
                response.EnsureSuccessStatusCode();

                return response.Content.ReadFromJsonAsync<DealerCustomer>().Result ?? new DealerCustomer();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске дилера/клиента для транзакции: {ex.Message}");
                return new DealerCustomer();
            }
        }

        // Получение ID по имени
        public DealerCustomer GetDeaCustIDFromName(string name)
        {
            try
            {
                var response = _httpClient.GetAsync($"/getIdFromName?name={name}").Result;
                response.EnsureSuccessStatusCode();

                return response.Content.ReadFromJsonAsync<DealerCustomer>().Result ?? new DealerCustomer();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении ID по имени: {ex.Message}");
                return new DealerCustomer();
            }
        }
    }
}
















































//using Backend.BusinessLogic;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Backend.Interfaces;
//using Backend.Utils;
////using System.Windows.Forms;

//namespace Backend.DataAccess
//{
//    public class DealerCustomerData:DealerCustomer,ICrudDealerCustomer
//    {
//        //static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
//        static string myconnstrng = AppConfiguration.ConnectionString;

//        //Сбор данных
//        public DataTable Select()
//        {
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            DataTable dt = new DataTable();

//            try
//            {
//                string sql = "SELECT * FROM table_dealer_customer";

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

//        //Ввод данных
//        public bool Insert(DealerCustomer dealerAndCustomer)
//        {
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            bool isSuccess = false;

//            try
//            {
//                string sql = "INSERT INTO table_dealer_customer (type, name, email, contact, address, added_date, added_by, added_by_name) VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by, @added_by_name)";

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                cmd.Parameters.AddWithValue("@type", dealerAndCustomer.Type);
//                cmd.Parameters.AddWithValue("@name", dealerAndCustomer.Name);
//                cmd.Parameters.AddWithValue("@email", dealerAndCustomer.Email);
//                cmd.Parameters.AddWithValue("@contact", dealerAndCustomer.Contact);
//                cmd.Parameters.AddWithValue("@address", dealerAndCustomer.Address);
//                cmd.Parameters.AddWithValue("@added_date", dealerAndCustomer.AddedDate);
//                cmd.Parameters.AddWithValue("@added_by", dealerAndCustomer.AddedBy);
//                cmd.Parameters.AddWithValue("@added_by_name", dealerAndCustomer.AddedByName);

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

//        //Редактировать данные
//        public bool Update(DealerCustomer dealerAndCustomer)
//        {
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            bool isSuccess = false;

//            try
//            {
//                string sql = "UPDATE table_dealer_customer SET type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by, added_by_name=@added_by_name WHERE id=@id";

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                cmd.Parameters.AddWithValue("@type", dealerAndCustomer.Type);
//                cmd.Parameters.AddWithValue("@name", dealerAndCustomer.Name);
//                cmd.Parameters.AddWithValue("@email", dealerAndCustomer.Email);
//                cmd.Parameters.AddWithValue("@contact", dealerAndCustomer.Contact);
//                cmd.Parameters.AddWithValue("@address", dealerAndCustomer.Address);
//                cmd.Parameters.AddWithValue("@added_date", dealerAndCustomer.AddedDate);
//                cmd.Parameters.AddWithValue("@added_by", dealerAndCustomer.AddedBy);
//                cmd.Parameters.AddWithValue("@added_by_name", dealerAndCustomer.AddedByName);
//                cmd.Parameters.AddWithValue("@id", dealerAndCustomer.Id);

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

//        //Удаление данных
//        public bool Delete(DealerCustomer dealerAndCustomer)
//        {
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            bool isSuccess = false;

//            try
//            {
//                string sql = "DELETE FROM table_dealer_customer WHERE id=@id";

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                cmd.Parameters.AddWithValue("@id", dealerAndCustomer.Id);

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

//        //Данные поиска
//        public DataTable Search(string keyword)
//        {
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            DataTable dt = new DataTable();

//            try
//            {
//                //Write the Query to Search Dealer or Customer Based in id, type and name
//                string sql = "SELECT * FROM table_dealer_customer WHERE id LIKE '%" + keyword + "%' OR type LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

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

//        //Поиск в данных
//        public DealerCustomer SearchDealerCustomerForTransaction(string keyword)
//        {
//            DealerCustomer dealerCustomer = new DealerCustomer();

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            DataTable dt = new DataTable();

//            try
//            {
//                string sql = "SELECT type, name, email, contact, address from table_dealer_customer WHERE id LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

//                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

//                conn.Open();

//                adapter.Fill(dt);

//                // Если мы успешно нашли данные, мы сохраняем их в объекте из business logic 
//                if (dt.Rows.Count > 0)
//                {
//                    dealerCustomer.Type = dt.Rows[0]["type"].ToString();
//                    dealerCustomer.Name = dt.Rows[0]["name"].ToString();
//                    dealerCustomer.Email = dt.Rows[0]["email"].ToString();
//                    dealerCustomer.Contact = dt.Rows[0]["contact"].ToString();
//                    dealerCustomer.Address = dt.Rows[0]["address"].ToString();
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

//            return dealerCustomer;
//        }

//        //Получение идентификатора из имени 
//        public DealerCustomer GetDeaCustIDFromName(string Name)
//        {
//            DealerCustomer dealerCustomer = new DealerCustomer();

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            DataTable dt = new DataTable();

//            try
//            {
//                string sql = "SELECT id FROM table_dealer_customer WHERE name='" + Name + "'";

//                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

//                conn.Open();

//                adapter.Fill(dt);

//                if (dt.Rows.Count > 0)
//                {
//                    dealerCustomer.Id = int.Parse(dt.Rows[0]["id"].ToString());
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

//            return dealerCustomer;
//        }

//    }
//}
