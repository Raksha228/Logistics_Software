using Backend.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalLogisticsController : ControllerBase
    {
        private readonly string _connectionString;

        public PersonalLogisticsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/PersonalLogistics/ByUsername?username=...
        [HttpGet("ByUsername")]
        public IActionResult GetByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM table_logistic WHERE employee=@username",
                    connection);

                command.Parameters.AddWithValue("@username", username);

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }

        // GET: api/PersonalLogistics/ByDate?date=...&username=...
        [HttpGet("ByDate")]
        public IActionResult GetByDate(string date, string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM table_logistic WHERE date=@date AND employee=@username",
                    connection);

                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@username", username);

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }
    }
}
