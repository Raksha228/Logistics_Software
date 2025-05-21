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
    public class PersonalLogisticData : Logistic, IPersonalLogistic
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public PersonalLogisticData()
        {
            _httpClient = new HttpClient();
            _apiBaseUrl = AppConfiguration.ApiBaseUrl;
        }

        // Отображение данных в соответствии с вошедшим в систему пользователем
        public DataTable DisplayLogisticByUsername(string username)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/PersonalLogistics/ByUsername?username={Uri.EscapeDataString(username)}";
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
                // В реальном приложении здесь должно быть логирование ошибок
                return new DataTable();
            }
        }

        // Отображение данных по дате
        public DataTable DisplayLogisticnByDate(string date, string username)
        {
            try
            {
                string apiUrl = $"{_apiBaseUrl}/api/PersonalLogistics/ByDate?date={Uri.EscapeDataString(date)}&username={Uri.EscapeDataString(username)}";
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

















































////using Backend.BusinessLogic;
////using System;
////using System.Collections.Generic;
////using System.Configuration;
////using System.Data;
////using System.Data.SqlClient;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using Backend.Interfaces;
////using Backend.Utils;

////namespace Backend.DataAccess
////{
////    public class PersonalLogisticData : Logistic, IPersonalLogistic
////    {

////        //static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
////        static string myconnstrng = AppConfiguration.ConnectionString;


////        //Отображение данных в соответствии с вошедшим в систему пользователем
////        public DataTable DisplayLogisticByUsername(string username)
////        {
////            SqlConnection conn = new SqlConnection(myconnstrng);

////            DataTable dt = new DataTable();

////            try
////            {
////                string sql = "SELECT * FROM table_logistic WHERE employee='" + username + "'" ;

////                SqlCommand cmd = new SqlCommand(sql, conn);

////                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

////                conn.Open();

////                adapter.Fill(dt);
////            }
////            catch (Exception ex)
////            {
////                //MessageBox.Show(ex.Message);
////            }
////            finally
////            {
////                conn.Close();
////            }

////            return dt;
////        }

////        // Отображение данных по дате
////        public DataTable DisplayLogisticnByDate(string date, string username)
////        {
////            SqlConnection conn = new SqlConnection(myconnstrng);

////            DataTable dt = new DataTable();

////            try
////            {
////                string sql = "SELECT * FROM table_logistic WHERE date='" + date + "' AND employee='" + username + "'";

////                SqlCommand cmd = new SqlCommand(sql, conn);

////                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

////                conn.Open();

////                adapter.Fill(dt);
////            }
////            catch (Exception ex)
////            {
////                //MessageBox.Show(ex.Message);
////            }
////            finally
////            {
////                conn.Close();
////            }

////            return dt;
////        }
////    }
////}
