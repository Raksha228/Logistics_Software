using Backend.BusinessLogic;
using Backend.Interfaces;
using Backend.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DataAccess
{
    public class CategoryData : Category,ICrudCategory
    {
        //public string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        public string myconnstrng = AppConfiguration.ConnectionString;
        //Получение данных
        public DataTable Select()
        {
            //Установите связь
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //Создайте запрос, чтобы получить все данные из базы данных
                string sql = "SELECT * FROM table_categories";

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

        //Создайте новую категорию
        public bool Insert(Category category)
        {
            bool isSucces = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Создайте запрос
                string sql = "INSERT INTO table_categories (title, description, added_date, added_by, added_by_name) VALUES (@title, @description, @added_date, @added_by, @added_by_name)";

                //Creating SQL Command to pass values in our query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing Values through parameter
                cmd.Parameters.AddWithValue("@title", category.Title);
                cmd.Parameters.AddWithValue("@description", category.Description);
                cmd.Parameters.AddWithValue("@added_date", category.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", category.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", category.AddedByName);

                conn.Open();

                //Получение количества заполненных строк
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    isSucces = true;
                }
                else
                {
                    isSucces = false;
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

            return isSucces;
        }

        //Редактирование данных
        public bool Update(Category category)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE table_categories SET title=@title, description=@description, added_date=@added_date, added_by=@added_by, added_by_name=@added_by_name WHERE id=@id";

                //Команда SQL для выполнения запроса
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@title", category.Title);
                cmd.Parameters.AddWithValue("@description", category.Description);
                cmd.Parameters.AddWithValue("@added_date", category.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", category.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", category.AddedByName);
                cmd.Parameters.AddWithValue("@id", category.Id);

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

        //Удаление данных
        public bool Delete(Category category)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM table_categories WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", category.Id);

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

        //Данные поиска
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                String sql = "SELECT * FROM table_categories WHERE id LIKE '%" + keywords + "%' OR title LIKE '%" + keywords + "%' OR description LIKE '%" + keywords + "%'";

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
