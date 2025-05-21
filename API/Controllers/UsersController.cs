using Backend.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string _connectionString;

        public UsersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM table_users WHERE username=@username AND password=@password AND user_type=@user_type",
                    connection);

                command.Parameters.AddWithValue("@username", login.Username ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@password", login.Password ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@user_type", login.UserType ?? (object)DBNull.Value);

                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();

                adapter.Fill(dataTable);

                return Ok(dataTable.Rows.Count > 0);
            }
        }
    }
}