using Microsoft.AspNetCore.Mvc;
using Npgsql;
using PostgreCRUD.Models;
using System.Diagnostics;

namespace PostgreCRUD.Controllers
{
    public class JsonInsertController : Controller
    {
        private readonly ILogger<JsonInsertController> _logger;
        // Assuming you have a configuration for the database connection
        private readonly IConfiguration _configuration;

      
        public JsonInsertController(ILogger<JsonInsertController> logger, IConfiguration configuration)
        {
            _logger = logger; 
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            

            return View();
        }
        public IActionResult Create()
        {
            var viewModel = new InsertDataViewModel
            {
                Grandparent = new Grandparent(),
                Parent = new Parent(),
                Child = new Child(),
                GreatGrandchild = new GreatGrandchild()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult InsertData(InsertDataViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Insert data into the database using raw SQL queries
                                InsertGrandparent(connection, transaction, viewModel.Grandparent);
                                InsertParent(connection, transaction, viewModel.Parent, viewModel.Grandparent.GrandparentId);
                                InsertChild(connection, transaction, viewModel.Child, viewModel.Parent.ParentId);
                                InsertGreatGrandchild(connection, transaction, viewModel.GreatGrandchild, viewModel.Child.ChildId);

                                // Commit the transaction if all steps are successful
                                transaction.Commit();

                                // Redirect to a success page or return a view
                                return RedirectToAction("Index"); 
                            }
                            catch (Exception ex)
                            {
                                // Handle exception, log, and optionally rollback the transaction
                                Console.WriteLine($"Error: {ex.Message}");
                                transaction.Rollback();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle connection-related exceptions
                    Console.WriteLine($"Connection Error: {ex.Message}");
                }
            }

            // If ModelState is not valid, return the view with validation errors
            return RedirectToAction("Index");
        }

        // Define methods to insert data into each table using raw SQL queries

        private void InsertGrandparent(NpgsqlConnection connection, NpgsqlTransaction transaction, Grandparent grandparent)
        {
            using (var command = new NpgsqlCommand("INSERT INTO grandparents (grandparent_name) VALUES (@GrandparentName) RETURNING grandparent_id", connection, transaction))
            {
                command.Parameters.AddWithValue("@GrandparentName", grandparent.GrandparentName);

                // Execute scalar to get the generated grandparent_id
                grandparent.GrandparentId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private void InsertParent(NpgsqlConnection connection, NpgsqlTransaction transaction, Parent parent, int grandparentId)
        {
            using (var command = new NpgsqlCommand("INSERT INTO parents (parent_name, grandparent_id) VALUES (@ParentName, @GrandparentId) RETURNING parent_id", connection, transaction))
            {
                command.Parameters.AddWithValue("@ParentName", parent.ParentName);
                command.Parameters.AddWithValue("@GrandparentId", grandparentId);

                // Execute scalar to get the generated parent_id
                parent.ParentId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private void InsertChild(NpgsqlConnection connection, NpgsqlTransaction transaction, Child child, int parentId)
        {
            using (var command = new NpgsqlCommand("INSERT INTO children (child_name, parent_id) VALUES (@ChildName, @ParentId) RETURNING child_id", connection, transaction))
            {
                command.Parameters.AddWithValue("@ChildName", child.ChildName);
                command.Parameters.AddWithValue("@ParentId", parentId);

                // Execute scalar to get the generated child_id
                child.ChildId = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private void InsertGreatGrandchild(NpgsqlConnection connection, NpgsqlTransaction transaction, GreatGrandchild greatGrandchild, int childId)
        {
            using (var command = new NpgsqlCommand("INSERT INTO great_grandchildren (great_grandchild_name, child_id) VALUES (@GreatGrandchildName, @ChildId) RETURNING great_grandchild_id", connection, transaction))
            {
                command.Parameters.AddWithValue("@GreatGrandchildName", greatGrandchild.GreatGrandchildName);
                command.Parameters.AddWithValue("@ChildId", childId);

                // Execute scalar to get the generated great_grandchild_id
                greatGrandchild.GreatGrandchildId = Convert.ToInt32(command.ExecuteScalar());
            }
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