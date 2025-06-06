using Backend.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Backend.Interfaces;

namespace Backend.DataAccess
{
    /// <summary>
    /// Класс для работы с деталями транзакций.
    /// </summary>
    public class TransactionDetailsData : TransactionDetails, ITransactionDetail
    {
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        /// <summary>
        /// Добавляет детали транзакции в базу данных.
        /// </summary>
        /// <param name="transactionDetail">Объект типа TransactionDetails, содержащий детали транзакции.</param>
        /// <returns>Возвращает true, если детали транзакции были успешно добавлены; в противном случае — false.</returns>
        public bool InsertTransactionDetail(TransactionDetails transactionDetail)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "INSERT INTO table_transactions_detail (product_id, rate, qty, total, dea_cust_id, added_date, added_by, added_by_name) VALUES (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by, @added_by_name)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@product_id", transactionDetail.ProductId);
                cmd.Parameters.AddWithValue("@rate", transactionDetail.Rate);
                cmd.Parameters.AddWithValue("@qty", transactionDetail.Quantity);
                cmd.Parameters.AddWithValue("@total", transactionDetail.Total);
                cmd.Parameters.AddWithValue("@dea_cust_id", transactionDetail.DealerCustomerId);
                cmd.Parameters.AddWithValue("@added_date", transactionDetail.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", transactionDetail.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", transactionDetail.AddedByName);

                conn.Open();

                // Выполнение команды и получение количества затронутых строк
                int rows = cmd.ExecuteNonQuery();

                // Если хотя бы одна строка была добавлена успешно
                isSuccess = rows > 0;
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
    }
}