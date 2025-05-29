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
    public class TransactionData :Transaction,ITransaction
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Добавить транзакцию
        public bool InsertTransaction(Transaction transaction, out int transactionID)
        {
            bool isSuccess = false;

            //Изначально установите идентификатор транзакции в -1
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

                //Возвращает значение первой строки и столбца после выполнения
                object executeQuery = cmd.ExecuteScalar();

                if (executeQuery != null)
                {
                    //Получение идентификатора транзакции, если она была проведена правильно
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

        //Визуализация всех операций
        public DataTable DisplayAllTransactions()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //SELECT t1.*, t2.* FROM t1, t2;
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

        //Визуализация транзакций по типам
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

        //Удалить все операции и детали
        public DataTable DeleteAllTransactions()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //SELECT t1.*, t2.* FROM t1, t2;
                //string sql = "SELECT table_transactions.*, table_transactions_detail.* FROM table_transactions, table_transactions_detail";

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
