using Backend.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly string _connectionString;

        public ProductsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM table_products", connection);
                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();

                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM table_products WHERE id=@id", connection);
                command.Parameters.AddWithValue("@id", id);

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                    return NotFound();

                return Ok(dataTable);
            }
        }

        // POST: api/Products
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO table_products (name, category, special_number, description, rate, qty, added_date, added_by, added_by_name) " +
                    "VALUES (@name, @category, @special_number, @description, @rate, @qty, @added_date, @added_by, @added_by_name); " +
                    "SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@category", product.Category);
                command.Parameters.AddWithValue("@special_number", product.SpecialNumber);
                command.Parameters.AddWithValue("@description", product.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@rate", product.Rate);
                command.Parameters.AddWithValue("@qty", product.Quantity);
                command.Parameters.AddWithValue("@added_date", product.AddedDate);
                command.Parameters.AddWithValue("@added_by", product.AddedBy);
                command.Parameters.AddWithValue("@added_by_name", product.AddedByName ?? (object)DBNull.Value);

                connection.Open();
                var id = command.ExecuteScalar();

                return Ok(new { Id = id });
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE table_products SET name=@name, category=@category, special_number=@special_number, " +
                    "description=@description, rate=@rate, qty=@qty, added_date=@added_date, " +
                    "added_by=@added_by, added_by_name=@added_by_name WHERE id=@id", connection);

                command.Parameters.AddWithValue("@id", id);

                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@category", product.Category);
                command.Parameters.AddWithValue("@special_number", product.SpecialNumber);
                command.Parameters.AddWithValue("@description", product.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@rate", product.Rate);
                command.Parameters.AddWithValue("@qty", product.Quantity);
                command.Parameters.AddWithValue("@added_date", product.AddedDate);
                command.Parameters.AddWithValue("@added_by", product.AddedBy);
                command.Parameters.AddWithValue("@added_by_name", product.AddedByName ?? (object)DBNull.Value);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : NotFound();
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM table_products WHERE id=@id", connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : NotFound();
            }
        }

        // GET: api/Products/Search?keywords=...
        [HttpGet("Search")]
        public IActionResult Search(string keywords)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM table_products WHERE special_number LIKE @keywords OR name LIKE @keywords OR category LIKE @keywords",
                    connection);

                command.Parameters.AddWithValue("@keywords", $"%{keywords}%");

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }

        // GET: api/Products/ForTransaction?keyword=...
        [HttpGet("ForTransaction")]
        public IActionResult GetForTransaction(string keyword)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT name, special_number, rate, qty FROM table_products " +
                    "WHERE special_number LIKE @keyword OR name LIKE @keyword",
                    connection);

                command.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                    return NotFound();

                var product = new Product
                {
                    Name = dataTable.Rows[0]["name"].ToString(),
                    SpecialNumber = dataTable.Rows[0]["special_number"].ToString(),
                    Rate = Convert.ToDecimal(dataTable.Rows[0]["rate"]),
                    Quantity = Convert.ToDecimal(dataTable.Rows[0]["qty"])
                };

                return Ok(product);
            }
        }

        // GET: api/Products/GetIdFromName?name=...
        [HttpGet("GetIdFromName")]
        public IActionResult GetIdFromName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT id FROM table_products WHERE name=@name",
                    connection);

                command.Parameters.AddWithValue("@name", name);

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                    return NotFound();

                var product = new Product
                {
                    Id = Convert.ToInt32(dataTable.Rows[0]["id"])
                };

                return Ok(product);
            }
        }

        // GET: api/Products/5/Quantity
        [HttpGet("{id}/Quantity")]
        public IActionResult GetQuantity(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT qty FROM table_products WHERE id=@id",
                    connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                var result = command.ExecuteScalar();

                if (result == null)
                    return NotFound();

                return Ok(Convert.ToDecimal(result));
            }
        }

        // PUT: api/Products/5/Quantity
        [HttpPut("{id}/Quantity")]
        public IActionResult UpdateQuantity(int id, [FromBody] decimal quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE table_products SET qty=@qty WHERE id=@id",
                    connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@qty", quantity);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : NotFound();
            }
        }

        // POST: api/Products/5/Increase?amount=...
        [HttpPost("{id}/Increase")]
        public IActionResult IncreaseQuantity(int id, decimal amount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE table_products SET qty = qty + @amount WHERE id=@id",
                    connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@amount", amount);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : NotFound();
            }
        }

        // POST: api/Products/5/Decrease?amount=...
        [HttpPost("{id}/Decrease")]
        public IActionResult DecreaseQuantity(int id, decimal amount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE table_products SET qty = qty - @amount WHERE id=@id AND qty >= @amount",
                    connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@amount", amount);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0 ? Ok() : BadRequest("Not enough quantity available");
            }
        }

        // GET: api/Products/ByCategory?category=...
        [HttpGet("ByCategory")]
        public IActionResult GetByCategory(string category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM table_products WHERE category=@category",
                    connection);

                command.Parameters.AddWithValue("@category", category);

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                return Ok(dataTable);
            }
        }
    }
}
