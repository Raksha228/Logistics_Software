using Backend.BusinessLogic;
using Backend.Interfaces;
using Backend.Utils;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.DataAccess
{
    public class ProductData : Product, ICrudProduct, IQuantityProduct
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ProductData()
        {
            _httpClient = new HttpClient();
            _apiBaseUrl = AppConfiguration.ApiBaseUrl;
        }

        // Получение всех продуктов
        public DataTable Select()
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products";
                var response = _httpClient.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<DataTable>(json);
                }

                return new DataTable();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        // Добавление нового продукта
        public bool Insert(Product product)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products";
                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _httpClient.PostAsync(apiUrl, content).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Обновление продукта
        public bool Update(Product product)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products/{product.Id}";
                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _httpClient.PutAsync(apiUrl, content).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Удаление продукта
        public bool Delete(Product product)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products/{product.Id}";
                var response = _httpClient.DeleteAsync(apiUrl).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Поиск продуктов
        public DataTable Search(string keywords)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products/Search?keywords={Uri.EscapeDataString(keywords)}";
                var response = _httpClient.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<DataTable>(json);
                }

                return new DataTable();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        // Поиск продукта для транзакции
        public Product GetProductsForTransaction(string keyword)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products/ForTransaction?keyword={Uri.EscapeDataString(keyword)}";
                var response = _httpClient.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<Product>(json);
                }
                return new Product();
            }
            catch (Exception)
            {
                return new Product();
            }
        }

        // Получение ID продукта по имени
        public Product GetProductIDFromName(string productName)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products/GetIdFromName?name={Uri.EscapeDataString(productName)}";
                var response = _httpClient.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<Product>(json);
                }

                return new Product();
            }
            catch (Exception)
            {
                return new Product();
            }
        }

        // Получение количества продукта
        public decimal GetProductQty(int productID)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products/{productID}/Quantity";
                var response = _httpClient.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<decimal>(json);
                }

                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // Обновление количества продукта
        public bool UpdateQuantity(int productID, decimal quantity)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products/{productID}/Quantity";
                var content = new StringContent(quantity.ToString(), Encoding.UTF8, "application/json");

                var response = _httpClient.PutAsync(apiUrl, content).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Увеличение количества продукта
        public bool IncreaseProduct(int productID, decimal increaseQuantity)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products/{productID}/Increase?amount={increaseQuantity}";
                var response = _httpClient.PostAsync(apiUrl, null).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Уменьшение количества продукта
        public bool DecreaseProduct(int productID, decimal decreaseQuantity)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products/{productID}/Decrease?amount={decreaseQuantity}";
                var response = _httpClient.PostAsync(apiUrl, null).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Отображение продуктов по категории
        public DataTable DisplayProductsByCategory(string category)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/Products/ByCategory?category={Uri.EscapeDataString(category)}";
                var response = _httpClient.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<DataTable>(json);
                }

                return new DataTable();
            }
            catch (Exception)
            {
                return new DataTable();
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

//namespace Backend.DataAccess
//{
//    public class ProductData:Product,ICrudProduct,IQuantityProduct
//    {
//        //static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
//        static string myconnstrng = AppConfiguration.ConnectionString;

//        //Сбор данных
//        public DataTable Select()
//        {
//            //Установка соединения с базой данных
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            //Создайте временную таблицу для хранения данных
//            DataTable dt = new DataTable();

//            try
//            {
//                //Создайте запрос, который берет все данные из таблицы
//                String sql = "SELECT * FROM table_products";

//                //Создайте команду для выполнения запроса
//                SqlCommand cmd = new SqlCommand(sql, conn);

//                //создайте адаптер для хранения информации и заполните таблицу
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

//        //Ввод данных в базу данных
//        public bool Insert(Product product)
//        {
//            bool isSuccess = false;

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            try
//            {
//                String sql = "INSERT INTO table_products (name, category, special_number, description, rate, qty, added_date, added_by, added_by_name) VALUES (@name, @category, @special_number, @description, @rate, @qty, @added_date, @added_by, @added_by_name)";

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                cmd.Parameters.AddWithValue("@name", product.Name);
//                cmd.Parameters.AddWithValue("@category", product.Category);
//                cmd.Parameters.AddWithValue("@special_number", product.SpecialNumber);
//                cmd.Parameters.AddWithValue("@description", product.Description);
//                cmd.Parameters.AddWithValue("@rate", product.Rate);
//                cmd.Parameters.AddWithValue("@qty", product.Quantity);
//                cmd.Parameters.AddWithValue("@added_date", product.AddedDate);
//                cmd.Parameters.AddWithValue("@added_by", product.AddedBy);
//                cmd.Parameters.AddWithValue("@added_by_name", product.AddedByName);

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
//        public bool Update(Product product)
//        {
//            bool isSuccess = false;

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            try
//            {
//                String sql = "UPDATE table_products SET name=@name, category=@category, special_number=@special_number, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by, added_by_name=@added_by_name WHERE id=@id";

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                cmd.Parameters.AddWithValue("@name", product.Name);
//                cmd.Parameters.AddWithValue("@category", product.Category);
//                cmd.Parameters.AddWithValue("@special_number", product.SpecialNumber);
//                cmd.Parameters.AddWithValue("@description", product.Description);
//                cmd.Parameters.AddWithValue("@rate", product.Rate);
//                cmd.Parameters.AddWithValue("@qty", product.Quantity);
//                cmd.Parameters.AddWithValue("@added_date", product.AddedDate);
//                cmd.Parameters.AddWithValue("@added_by", product.AddedBy);
//                cmd.Parameters.AddWithValue("@added_by_name", product.AddedByName);
//                cmd.Parameters.AddWithValue("@id", product.Id);

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
//        public bool Delete(Product product)
//        {
//            bool isSuccess = false;

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            try
//            {
//                String sql = "DELETE FROM table_products WHERE id=@id";

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                cmd.Parameters.AddWithValue("@id", product.Id);

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
//        public DataTable Search(string keywords)
//        {
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            DataTable dt = new DataTable();

//            try
//            {
//                string sql = "SELECT * FROM table_products WHERE special_number LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%'";

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

//        //Поиск продуктов для сделок
//        public Product GetProductsForTransaction(string keyword)
//        {
//            Product product = new Product();

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            DataTable dt = new DataTable();

//            try
//            {
//                string sql = "SELECT name, special_number, rate, qty FROM table_products WHERE special_number LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

//                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

//                conn.Open();

//                adapter.Fill(dt);

//                if (dt.Rows.Count > 0)
//                {
//                    product.Name = dt.Rows[0]["name"].ToString();
//                    product.SpecialNumber = dt.Rows[0]["special_number"].ToString();
//                    product.Rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
//                    product.Quantity = decimal.Parse(dt.Rows[0]["qty"].ToString());
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

//            return product;
//        }

//        // Получение идентификатора
//        public Product GetProductIDFromName(string productName)
//        {
//            Product product = new Product();

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            DataTable dt = new DataTable();

//            try
//            {
//                string sql = "SELECT id FROM table_products WHERE name='" + productName + "'";

//                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

//                conn.Open();

//                adapter.Fill(dt);

//                if (dt.Rows.Count > 0)
//                {
//                    product.Id = int.Parse(dt.Rows[0]["id"].ToString());
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

//            return product;
//        }

//        //Взяв мгновенное количество продуктов
//        public decimal GetProductQty(int productID)
//        {
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            decimal qty = 0;

//            DataTable dt = new DataTable();

//            try
//            {
//                string sql = "SELECT qty FROM table_products WHERE id = " + productID;

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

//                conn.Open();

//                adapter.Fill(dt);

//                if (dt.Rows.Count > 0)
//                {
//                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
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

//            return qty;
//        }

//        //Изменение количества
//        public bool UpdateQuantity(int productID, decimal quantity)
//        {
//            bool success = false;

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            try
//            {
//                string sql = "UPDATE table_products SET qty=@qty WHERE id=@id";

//                SqlCommand cmd = new SqlCommand(sql, conn);

//                cmd.Parameters.AddWithValue("@qty", quantity);
//                cmd.Parameters.AddWithValue("@id", productID);

//                conn.Open();

//                int rows = cmd.ExecuteNonQuery();
//                if (rows > 0)
//                {
//                    success = true;
//                }
//                else
//                {
//                    success = false;
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

//            return success;
//        }

//        //Увеличение количества
//        public bool IncreaseProduct(int productID, decimal increaseQuantity)
//        {
//            bool success = false;

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            try
//            {
//                //Вземане на текущото количество
//                decimal currentQuantity = GetProductQty(productID);

//                //Увеличаване на текущото количество с това което сме купили
//                decimal newQuantity = currentQuantity + increaseQuantity;

//                //Промяна на количеството
//                success = UpdateQuantity(productID, newQuantity);
//            }
//            catch (Exception ex)
//            {
//                //MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                conn.Close();
//            }
//            return success;
//        }

//        //Уменьшение количества
//        public bool DecreaseProduct(int productID, decimal decreaseQuantity)
//        {
//            bool success = false;

//            SqlConnection conn = new SqlConnection(myconnstrng);

//            try
//            {
//                decimal currentQuantity = GetProductQty(productID);

//                decimal newQuantity = currentQuantity - decreaseQuantity;

//                success = UpdateQuantity(productID, newQuantity);
//            }
//            catch (Exception ex)
//            {
//                //MessageBox.Show(ex.Message);
//            }
//            finally
//            {
//                conn.Close();
//            }
//            return success;
//        }

//        //Визуализация продуктов по категориям
//        public DataTable DisplayProductsByCategory(string category)
//        {
//            SqlConnection conn = new SqlConnection(myconnstrng);

//            DataTable dt = new DataTable();

//            try
//            {
//                string sql = "SELECT * FROM table_products WHERE category='" + category + "'";

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
