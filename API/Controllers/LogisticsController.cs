using Backend.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogisticsController : ControllerBase
    {
        private readonly string _connectionString;

        public LogisticsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/Logistics
        [HttpGet]
        public IActionResult GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM table_logistic", connection);
                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();

                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }

        // GET: api/Logistics/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM table_logistic WHERE id=@id", connection);
                command.Parameters.AddWithValue("@id", id);

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                    return NotFound();

                return Ok(dataTable);
            }
        }

        // POST: api/Logistics
        [HttpPost]
        public IActionResult Create([FromBody] Logistic logistic)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO table_logistic (employee, first_name_employee, last_name_employee, address, contact, date, description, price, added_date, added_by, added_by_name) " +
                    "VALUES (@employee, @first_name_employee, @last_name_employee, @address, @contact, @date, @description, @price, @added_date, @added_by, @added_by_name); " +
                    "SELECT SCOPE_IDENTITY();", connection);

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
                var id = command.ExecuteScalar();

                return Ok(new { Id = id });
            }
        }

        // PUT: api/Logistics/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Logistic logistic)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE table_logistic SET employee=@employee, first_name_employee=@first_name_employee, " +
                    "last_name_employee=@last_name_employee, address=@address, contact=@contact, date=@date, " +
                    "description=@description, price=@price, added_date=@added_date, added_by=@added_by, " +
                    "added_by_name=@added_by_name WHERE id=@id", connection);
                command.Parameters.AddWithValue("@id", id);
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

                return rowsAffected > 0 ? Ok() : NotFound();
            }
        }

        // DELETE: api/Logistics/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM table_logistic WHERE id=@id", connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : NotFound();
            }
        }

        // GET: api/Logistics/Search?keywords=...
        [HttpGet("Search")]
        public IActionResult Search(string keywords)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM table_logistic WHERE id LIKE @keywords OR employee LIKE @keywords OR date LIKE @keywords",
                    connection);

                command.Parameters.AddWithValue("@keywords", $"%{keywords}%");

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }
    }
}
