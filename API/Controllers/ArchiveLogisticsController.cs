using Backend.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchiveLogisticsController : ControllerBase
    {
        private readonly string _connectionString;

        public ArchiveLogisticsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/ArchiveLogistics
        [HttpGet]
        public IActionResult GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM table_logistic_archive", connection);
                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();

                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }

        // POST: api/ArchiveLogistics
        [HttpPost]
        public IActionResult Insert([FromBody] Logistic logistic)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO table_logistic_archive (employee, first_name_employee, last_name_employee, " +
                    "address, contact, date, description, price, added_date, added_by, added_by_name) " +
                    "VALUES (@employee, @first_name_employee, @last_name_employee, @address, @contact, " +
                    "@date, @description, @price, @added_date, @added_by, @added_by_name)", connection);

                command.Parameters.AddWithValue("@employee", logistic.Empleyee);
                command.Parameters.AddWithValue("@first_name_employee", logistic.FirstNameEmployee);
                command.Parameters.AddWithValue("@last_name_employee", logistic.LastNameEmployee);
                command.Parameters.AddWithValue("@address", logistic.Address);
                command.Parameters.AddWithValue("@contact", logistic.Contact);
                command.Parameters.AddWithValue("@date", logistic.Date);
                command.Parameters.AddWithValue("@description", logistic.Description);
                command.Parameters.AddWithValue("@price", logistic.Price);
                command.Parameters.AddWithValue("@added_date", logistic.AddedDate);
                command.Parameters.AddWithValue("@added_by", logistic.AddedBy);
                command.Parameters.AddWithValue("@added_by_name", logistic.AddedByName);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
        }

        // GET: api/ArchiveLogistics?date=...
        [HttpGet]
        public IActionResult GetByDate(string date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM table_logistic_archive WHERE date=@date", connection);
                command.Parameters.AddWithValue("@date", date);

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();

                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }
    }
}