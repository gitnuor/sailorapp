using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Application.Interface.Common;
using MongoDB.Driver;
using MongoDB.Bson;
using Npgsql;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EPYSLSAILORAPP.Controllers.DesignManagement
{
    [RequestFormLimits(MultipartBodyLengthLimit = 2000000000)]
    [RequestSizeLimit(2000000000)]

    public class QueryEditorController : BaseController
    {
        private readonly ILogger<QueryEditorController> _logger;
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _contextAccessor;



        public QueryEditorController(
            IMapper mapper, ILogger<QueryEditorController> logger, IHttpContextAccessor contextAccessor,
            IConfiguration configuration
           )
        {

            _mapper = mapper;
            _logger = logger;

            _contextAccessor = contextAccessor;

            _logger.LogInformation("Hello from inside ExceptionView !");
            _configuration = configuration;



        }

        [HttpGet]
        public async Task<IActionResult> GetTableDetails(string tablename)
        {
            ViewBag.Table = tablename;

            var dtcolumn = GetTableDetailsList(tablename);
            ViewBag.TableDetails = dtcolumn;

            ViewBag.TableDet = JsonConvert.SerializeObject(dtcolumn);
            ViewBag.TableRelationDetails =  JsonConvert.SerializeObject( GetTableRelationList(tablename), Formatting.Indented);

            return PartialView("~/Views/QueryEditor/_table_columns.cshtml");

        }

        public IActionResult Index()
        {
            ViewBag.Tables = GetTableList();
            return View();
        }

        public DataTable GetTableRelationList(string TableName)
        {
            var dtForeignKeys = new DataTable();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {

                string query2 = @"SELECT
                                conrelid::regclass::text AS referencing_table,
                                a.attname AS referencing_column,
                                confrelid::regclass::text AS referenced_table,
                                b.attname AS referenced_column
                            FROM
                                pg_constraint AS c
                            JOIN
                                pg_attribute AS a ON a.attnum = ANY(c.conkey) AND a.attrelid = c.conrelid
                            JOIN
                                pg_attribute AS b ON b.attnum = ANY(c.confkey) AND b.attrelid = c.confrelid
                            WHERE
                                confrelid::regclass::text = '" + TableName + @"';";

                // connection.Open();

                using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query2, connection))
                {
                    dataAdapter.Fill(dtForeignKeys);
                }
            }

            return dtForeignKeys;
        }

        public DataTable GetTableDetailsList(string TableName)
        {
            DataTable dtColumns = new DataTable();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {

                try
                {
                    string query = @"SELECT '" + TableName + @"' as table_name, col.column_name,col.is_nullable,col.data_type,col.is_identity,
                                    col.ordinal_position,true as ChkSelect FROM information_schema.columns col
                                    WHERE table_name='" + TableName + "' order by col.ordinal_position";

                    connection.Open();

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dtColumns);
                    }

                }
                catch (Exception ex)
                {
                }

               
            }

            return dtColumns;
        }

        public DataTable GetTableList()
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                DataTable dataTable = new DataTable();

                try
                {
                    string query = @"select table_name from information_schema.tables where table_schema='public' order by table_name";

                    connection.Open();

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, connection))
                    {
                        dataAdapter.Fill(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return dataTable; 

            }
        }

    }

}
