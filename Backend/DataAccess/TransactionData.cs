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
    /// Класс <c>TransactionData</c> реализует доступ к данным о транзакциях, а также методы
    /// для добавления, удаления и отображения информации о транзакциях в системе.
    /// </summary>
    /// <remarks>
    /// Класс наследуется от <see cref="Transaction"/> и реализует интерфейс <see cref="ITransaction"/>.
    /// Обеспечивает работу с таблицей <c>table_transactions</c> базы данных.
    /// </remarks>
    public class TransactionData : Transaction, ITransaction
    {
        /// <summary>
        /// Строка подключения к базе данных,
        /// полученная из файла конфигурации приложения.
        /// </summary>
        private static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        /// <summary>
        /// Добавляет новую транзакцию в базу данных.
        /// </summary>
        /// <param name="transaction">Объект <see cref="Transaction"/>, содержащий все необходимые параметры транзакции.</param>
        /// <param name="transactionID">Выходной параметр, в который помещается идентификатор вставленной транзакции.</param>
        /// <returns>
        /// <c>true</c>, если транзакция успешно добавлена и получен идентификатор; <c>false</c> в противном случае.
        /// </returns>
        /// <example>
        /// <code>
        /// TransactionData td = new TransactionData();
        /// Transaction tr = new Transaction { /* инициализация */ };
        /// int trId;
        /// bool result = td.InsertTransaction(tr, out trId);
        /// </code>
        /// </example>
        public bool InsertTransaction(Transaction transaction, out int transactionID)
        {
            bool isSuccess = false;

            //Изначально устанавливаем идентификатор транзакции в -1
            transactionID = -1;

            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "INSERT INTO table_transactions (type, dea_cust_id, description, grandTotal, transaction_date, tax, discount, paid_amount, return_amount, added_by, added_by_name) VALUES (@type, @dea_cust_id, @description, @grandTotal, @transaction_date, @tax, @discount, @paid_amount, @return_amount, @added_by, @added_by_name); SELECT @@IDENTITY;";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", transaction.Type);
                cmd.Parameters.AddWithValue("@dea_cust_id", transaction.DealerCustomerId);
                cmd.Parameters.AddWithValue("@description", transaction.Description);
                cmd.Parameters.AddWithValue("@grandTotal", transaction.GrandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", transaction.TransactionDate);
                cmd.Parameters.AddWithValue("@tax", transaction.Tax);
                cmd.Parameters.AddWithValue("@discount", transaction.Discount);
                cmd.Parameters.AddWithValue("@paid_amount", transaction.PaidAmount);
                cmd.Parameters.AddWithValue("@return_amount", transaction.ReturnAmount);
                cmd.Parameters.AddWithValue("@added_by", transaction.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", transaction.AddedByName);

                conn.Open();

                //Возвращает значение идентификатора вставленной строки
                object executeQuery = cmd.ExecuteScalar();

                if (executeQuery != null)
                {
                    //Получение идентификатора транзакции, если операция проведена удачно
                    transactionID = int.Parse(executeQuery.ToString());
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
        /// Возвращает таблицу, содержащую все совершённые транзакции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="DataTable"/> со всеми записями из таблицы транзакций.
        /// </returns>
        /// <example>
        /// <code>
        /// DataTable dt = new TransactionData().DisplayAllTransactions();
        /// </code>
        /// </example>
        public DataTable DisplayAllTransactions()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_transactions";
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
        /// Возвращает таблицу транзакций, фильтрованных по определённому типу (например, "Покупка" или "Продажа").
        /// </summary>
        /// <param name="type">Тип транзакции, по которому выполнять фильтрацию.</param>
        /// <returns>
        /// Объект <see cref="DataTable"/>, содержащий транзакции только указанного типа.
        /// </returns>
        /// <example>
        /// <code>
        /// DataTable onlySales = new TransactionData().DisplayTransactionByType("Продажа");
        /// </code>
        /// </example>
        public DataTable DisplayTransactionByType(string type)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM table_transactions WHERE type='" + type + "'";
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
        /// Удаляет все транзакции из таблицы <c>table_transactions</c>.
        /// </summary>
        /// <remarks>
        /// Используйте с осторожностью: действие необратимо и приведёт к потере всех данных о транзакциях.
        /// </remarks>
        /// <returns>
        /// Объект <see cref="DataTable"/> (обычно пустой после удаления).
        /// </returns>
        public DataTable DeleteAllTransactions()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "DELETE FROM table_transactions";
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
    }
}