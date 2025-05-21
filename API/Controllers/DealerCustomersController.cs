using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Backend.BusinessLogic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerCustomersController : ControllerBase
    {
        private readonly string _connectionString;

        public DealerCustomersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/dealerCustomers
        [HttpGet]
        public IActionResult GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM table_dealer_customer", connection);
                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();

                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }

        // POST: api/dealerCustomers
        [HttpPost]
        public IActionResult Create([FromBody] DealerCustomer dealerCustomer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO table_dealer_customer (type, name, email, contact, address, added_date, added_by, added_by_name) " +
                    "VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by, @added_by_name)",
                    connection);

                command.Parameters.AddWithValue("@type", dealerCustomer.Type);
                command.Parameters.AddWithValue("@name", dealerCustomer.Name);
                command.Parameters.AddWithValue("@email", dealerCustomer.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@contact", dealerCustomer.Contact ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@address", dealerCustomer.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@added_date", dealerCustomer.AddedDate);
                command.Parameters.AddWithValue("@added_by", dealerCustomer.AddedBy);
                command.Parameters.AddWithValue("@added_by_name", dealerCustomer.AddedByName ?? (object)DBNull.Value);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
        }

        // PUT: api/dealerCustomers/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DealerCustomer dealerCustomer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE table_dealer_customer SET type=@type, name=@name, email=@email, " +
                    "contact=@contact, address=@address, added_date=@added_date, " +
                    "added_by=@added_by, added_by_name=@added_by_name WHERE id=@id",
                    connection);

                command.Parameters.AddWithValue("@type", dealerCustomer.Type);
                command.Parameters.AddWithValue("@name", dealerCustomer.Name);
                command.Parameters.AddWithValue("@email", dealerCustomer.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@contact", dealerCustomer.Contact ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@address", dealerCustomer.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@added_date", dealerCustomer.AddedDate);
                command.Parameters.AddWithValue("@added_by", dealerCustomer.AddedBy);
                command.Parameters.AddWithValue("@added_by_name", dealerCustomer.AddedByName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
        }

        // DELETE: api/dealerCustomers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM table_dealer_customer WHERE id=@id", connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
        }

        // GET: api/dealerCustomers/search?keyword=...
        [HttpGet("search")]
        public IActionResult Search(string keyword)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM table_dealer_customer WHERE id LIKE @keyword OR type LIKE @keyword OR name LIKE @keyword",
                    connection);

                command.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();

                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }

        // GET: api/dealerCustomers/searchForTransaction?keyword=...
        [HttpGet("searchForTransaction")]
        public IActionResult SearchForTransaction(string keyword)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT type, name, email, contact, address FROM table_dealer_customer " +
                    "WHERE id LIKE @keyword OR name LIKE @keyword",
                    connection);

                command.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                    return NotFound();

                var dealerCustomer = new DealerCustomer
                {
                    Type = dataTable.Rows[0]["type"].ToString(),
                    Name = dataTable.Rows[0]["name"].ToString(),
                    Email = dataTable.Rows[0]["email"].ToString(),
                    Contact = dataTable.Rows[0]["contact"].ToString(),
                    Address = dataTable.Rows[0]["address"].ToString()
                };

                return Ok(dealerCustomer);
            }
        }

        // GET: api/dealerCustomers/getIdFromName?name=...
        [HttpGet("getIdFromName")]
        public IActionResult GetIdFromName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT id FROM table_dealer_customer WHERE name=@name",
                    connection);

                command.Parameters.AddWithValue("@name", name);

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                    return NotFound();

                var dealerCustomer = new DealerCustomer
                {
                    Id = Convert.ToInt32(dataTable.Rows[0]["id"])
                };

                return Ok(dealerCustomer);
            }
        }
    }
}