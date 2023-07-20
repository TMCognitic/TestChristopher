using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using TestChristopher.Models;

namespace TestChristopher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbConnection _dbConnection;

        public HomeController(ILogger<HomeController> logger, DbConnection dbConnection)
        {
            _logger = logger;
            _dbConnection = dbConnection;
        }

        public IActionResult Index()
        {
            List<Comment> comments = new List<Comment>();

            using(_dbConnection)
            {
                using(DbCommand command = _dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT C.Id, C.Content, J.Id AS PostId, J.Content AS PostContent FROM Comment AS C JOIN Post AS J ON C.PostId = J.Id";
                    _dbConnection.Open();
                    using (DbDataReader  reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comments.Add(new Comment((int)reader["Id"], (string)reader["Content"], (int)reader["PostId"], (string)reader["PostContent"]));
                        }
                    }
                }
            }

            return View(comments.GroupBy(c => c.PostInfo));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}