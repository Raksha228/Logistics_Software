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
    /// <summary>
    /// Класс для доступа к данным дилеров и клиентов.
    /// Предоставляет методы для выполнения основных операций с таблицей "table_dealer_customer" в базе данных,
    /// включая выборку, добавление, изменение, удаление и поиск данных.
    /// Наследуется от <see cref="DealerCustomer"/> и реализует интерфейс <see cref="ICrudDealerCustomer"/>.
    /// </summary>
    public class DealerCustomerData : DealerCustomer, ICrudDealerCustomer
    {
        /// <summary>
        /// Строка подключения к базе данных, получаемая из файла конфигурации приложения.
        /// </summary>
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        /// <summary>
        /// Получает все записи дилеров и клиентов из базы данных.
        /// </summary>
        /// <returns>
        /// Объект <see cref="DataTable"/>, содержащий все строки из таблицы дилеров и клиентов.
        /// </returns>
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_dealer_customer";

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

        /// <summary>
        /// Добавляет новую запись дилера или клиента в таблицу базы данных.
        /// </summary>
        /// <param name="dealerAndCustomer">
        /// Объект <see cref="DealerCustomer"/>, содержащий информацию о новом дилере или клиенте.
        /// </param>
        /// <returns>
        /// <c>true</c> если запись успешно добавлена, иначе <c>false</c>.
        /// </returns>
        public bool Insert(DealerCustomer dealerAndCustomer)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            bool isSuccess = false;

            try
            {
                string sql = "INSERT INTO table_dealer_customer (type, name, email, contact, address, added_date, added_by, added_by_name) VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by, @added_by_name)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", dealerAndCustomer.Type);
                cmd.Parameters.AddWithValue("@name", dealerAndCustomer.Name);
                cmd.Parameters.AddWithValue("@email", dealerAndCustomer.Email);
                cmd.Parameters.AddWithValue("@contact", dealerAndCustomer.Contact);
                cmd.Parameters.AddWithValue("@address", dealerAndCustomer.Address);
                cmd.Parameters.AddWithValue("@added_date", dealerAndCustomer.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", dealerAndCustomer.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", dealerAndCustomer.AddedByName);

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

        /// <summary>
        /// Обновляет существующую запись дилера или клиента в таблице базы данных.
        /// </summary>
        /// <param name="dealerAndCustomer">
        /// Объект <see cref="DealerCustomer"/>, содержащий обновлённую информацию.
        /// </param>
        /// <returns>
        /// <c>true</c> если запись успешно обновлена, иначе <c>false</c>.
        /// </returns>
        public bool Update(DealerCustomer dealerAndCustomer)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            bool isSuccess = false;

            try
            {
                string sql = "UPDATE table_dealer_customer SET type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by, added_by_name=@added_by_name WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", dealerAndCustomer.Type);
                cmd.Parameters.AddWithValue("@name", dealerAndCustomer.Name);
                cmd.Parameters.AddWithValue("@email", dealerAndCustomer.Email);
                cmd.Parameters.AddWithValue("@contact", dealerAndCustomer.Contact);
                cmd.Parameters.AddWithValue("@address", dealerAndCustomer.Address);
                cmd.Parameters.AddWithValue("@added_date", dealerAndCustomer.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", dealerAndCustomer.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", dealerAndCustomer.AddedByName);
                cmd.Parameters.AddWithValue("@id", dealerAndCustomer.Id);

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

        /// <summary>
        /// Удаляет запись дилера или клиента из таблицы базы данных по его идентификатору.
        /// </summary>
        /// <param name="dealerAndCustomer">
        /// Объект <see cref="DealerCustomer"/>, содержащий идентификатор записи для удаления.
        /// </param>
        /// <returns>
        /// <c>true</c> если запись успешно удалена, иначе <c>false</c>.
        /// </returns>
        public bool Delete(DealerCustomer dealerAndCustomer)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            bool isSuccess = false;

            try
            {
                string sql = "DELETE FROM table_dealer_customer WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", dealerAndCustomer.Id);

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

        /// <summary>
        /// Выполняет поиск записей дилеров и клиентов по ключевому слову.
        /// Ключевое слово ищется в полях "id", "type" и "name".
        /// </summary>
        /// <param name="keyword">Строка поиска, по которой происходит фильтрация записей.</param>
        /// <returns>
        /// Объект <see cref="DataTable"/>, содержащий найденные записи.
        /// </returns>
        public DataTable Search(string keyword)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //Write the Query to Search Dealer or Customer Based in id, type and name
                string sql = "SELECT * FROM table_dealer_customer WHERE id LIKE '%" + keyword + "%' OR type LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

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

        /// <summary>
        /// Выполняет расширенный поиск дилера или клиента по ключевому слову для проведения транзакции.
        /// Ключевое слово ищется в полях "id" и "name".
        /// Возвращает объект <see cref="DealerCustomer"/> с найденными данными.
        /// </summary>
        /// <param name="keyword">Строка поиска по id или имени.</param>
        /// <returns>
        /// Объект <see cref="DealerCustomer"/>, содержащий сведения о найденном дилере или клиенте.
        /// Если ничего не найдено, возвращается объект с пустыми значениями полей.
        /// </returns>
        public DealerCustomer SearchDealerCustomerForTransaction(string keyword)
        {
            DealerCustomer dealerCustomer = new DealerCustomer();

            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT type, name, email, contact, address from table_dealer_customer WHERE id LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                adapter.Fill(dt);

                //Если я успешно нашел данные, мы сохраняем их в объекте из бизнес-логики 
                if (dt.Rows.Count > 0)
                {
                    dealerCustomer.Type = dt.Rows[0]["type"].ToString();
                    dealerCustomer.Name = dt.Rows[0]["name"].ToString();
                    dealerCustomer.Email = dt.Rows[0]["email"].ToString();
                    dealerCustomer.Contact = dt.Rows[0]["contact"].ToString();
                    dealerCustomer.Address = dt.Rows[0]["address"].ToString();
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

            return dealerCustomer;
        }

        /// <summary>
        /// Получает идентификатор дилера или клиента по его имени.
        /// </summary>
        /// <param name="Name">Имя дилера или клиента, по которому выполняется поиск идентификатора.</param>
        /// <returns>
        /// Объект <see cref="DealerCustomer"/>, содержащий найденный идентификатор в свойстве <c>Id</c>.
        /// Если запись не найдена, Id будет равен 0.
        /// </returns>
        public DealerCustomer GetDeaCustIDFromName(string Name)
        {
            DealerCustomer dealerCustomer = new DealerCustomer();

            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id FROM table_dealer_customer WHERE name='" + Name + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                conn.Open();

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dealerCustomer.Id = int.Parse(dt.Rows[0]["id"].ToString());
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

            return dealerCustomer;
        }

    }
}