using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Backend.BusinessLogic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly string _connectionString;

        public CategoriesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/categories
        [HttpGet]
        public IActionResult GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM table_categories", connection);
                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();

                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }

        // POST: api/categories
        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO table_categories (title, description, added_date, added_by, added_by_name) " +
                    "VALUES (@title, @description, @added_date, @added_by, @added_by_name)", connection);

                command.Parameters.AddWithValue("@title", category.Title);
                command.Parameters.AddWithValue("@description", category.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@added_date", category.AddedDate);
                command.Parameters.AddWithValue("@added_by", category.AddedBy);
                command.Parameters.AddWithValue("@added_by_name", category.AddedByName ?? (object)DBNull.Value);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE table_categories SET title=@title, description=@description, " +
                    "added_date=@added_date, added_by=@added_by, added_by_name=@added_by_name " +
                    "WHERE id=@id", connection);

                command.Parameters.AddWithValue("@title", category.Title);
                command.Parameters.AddWithValue("@description", category.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@added_date", category.AddedDate);
                command.Parameters.AddWithValue("@added_by", category.AddedBy);
                command.Parameters.AddWithValue("@added_by_name", category.AddedByName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM table_categories WHERE id=@id", connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : BadRequest();
            }
        }

        // GET: api/categories/search?keywords=...
        [HttpGet("search")]
        public IActionResult Search(string keywords)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM table_categories WHERE id LIKE @keywords OR title LIKE @keywords OR description LIKE @keywords",
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