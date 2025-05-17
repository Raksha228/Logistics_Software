using Backend.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Interfaces;
using Backend.Utils;

namespace Backend.DataAccess
{
    public class TransactionDetailsData:TransactionDetails,ITransactionDetail
    {
        //static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        static string myconnstrng = AppConfiguration.ConnectionString;

        //Добавьте детали транзакции
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

                //Есть ли затронутая или измененная строка при успешном добавлении данных
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
    }
}
