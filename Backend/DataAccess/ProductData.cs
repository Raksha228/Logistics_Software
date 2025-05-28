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
    public class ProductData :Product,ICrudProduct,IQuantityProduct
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Взимане на данни
        public DataTable Select()
        {
            //Създаване на връзка с базата данни
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Създаване на времена таблица за задържане на данните
            DataTable dt = new DataTable();

            try
            {
                //Създаване на query което да вземе всички данни от таблицата
                String sql = "SELECT * FROM table_products";

                //Създаване на команда която да изпълне query-то
                SqlCommand cmd = new SqlCommand(sql, conn);

                //създаване на адаптер за задържане на информацията и запълване в таблицата
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

        //Вкарване на данни в базата
        public bool Insert(Product product)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO table_products (name, category, special_number, description, rate, qty, added_date, added_by, added_by_name) VALUES (@name, @category, @special_number, @description, @rate, @qty, @added_date, @added_by, @added_by_name)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@category", product.Category);
                cmd.Parameters.AddWithValue("@special_number", product.SpecialNumber);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@rate", product.Rate);
                cmd.Parameters.AddWithValue("@qty", product.Quantity);
                cmd.Parameters.AddWithValue("@added_date", product.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", product.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", product.AddedByName);

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

        //Редактиране на данни
        public bool Update(Product product)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "UPDATE table_products SET name=@name, category=@category, special_number=@special_number, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by, added_by_name=@added_by_name WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@category", product.Category);
                cmd.Parameters.AddWithValue("@special_number", product.SpecialNumber);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@rate", product.Rate);
                cmd.Parameters.AddWithValue("@qty", product.Quantity);
                cmd.Parameters.AddWithValue("@added_date", product.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", product.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", product.AddedByName);
                cmd.Parameters.AddWithValue("@id", product.Id);

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
        public bool Delete(Product product)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "DELETE FROM table_products WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", product.Id);

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
                string sql = "SELECT * FROM table_products WHERE special_number LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%'";

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

        //Търсене на продукт за транзакциите
        public Product GetProductsForTransaction(string keyword)
        {
            Product product = new Product();

            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT name, special_number, rate, qty FROM table_products WHERE special_number LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    product.Name = dt.Rows[0]["name"].ToString();
                    product.SpecialNumber = dt.Rows[0]["special_number"].ToString();
                    product.Rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    product.Quantity = decimal.Parse(dt.Rows[0]["qty"].ToString());
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

            return product;
        }

        //Вземане на id 
        public Product GetProductIDFromName(string productName)
        {
            Product product = new Product();

            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id FROM table_products WHERE name='" + productName + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    product.Id = int.Parse(dt.Rows[0]["id"].ToString());
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

            return product;
        }

        //Вземане на моментното количество продукти
        public decimal GetProductQty(int productID)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            decimal qty = 0;

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT qty FROM table_products WHERE id = " + productID;

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
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

            return qty;
        }

        //Промяна на количеството
        public bool UpdateQuantity(int productID, decimal quantity)
        {
            bool success = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE table_products SET qty=@qty WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@qty", quantity);
                cmd.Parameters.AddWithValue("@id", productID);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
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

            return success;
        }

        //Увеличаване на количеството
        public bool IncreaseProduct(int productID, decimal increaseQuantity)
        {
            bool success = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Вземане на текущото количество
                decimal currentQuantity = GetProductQty(productID);

                //Увеличаване на текущото количество с това което сме купили
                decimal newQuantity = currentQuantity + increaseQuantity;

                //Промяна на количеството
                success = UpdateQuantity(productID, newQuantity);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        //Намаляне на количеството
        public bool DecreaseProduct(int productID, decimal decreaseQuantity)
        {
            bool success = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                decimal currentQuantity = GetProductQty(productID);

                decimal newQuantity = currentQuantity - decreaseQuantity;

                success = UpdateQuantity(productID, newQuantity);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        //Визуализация на продукти според категорията
        public DataTable DisplayProductsByCategory(string category)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_products WHERE category='" + category + "'";

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
    }
}
