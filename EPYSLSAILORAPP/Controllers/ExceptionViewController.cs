using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Application.Interface.Common;
using MongoDB.Driver;
using MongoDB.Bson;

namespace EPYSLSAILORAPP.Controllers.DesignManagement
{
    [RequestFormLimits(MultipartBodyLengthLimit = 2000000000)]
    [RequestSizeLimit(2000000000)]

    public class ExceptionViewController : BaseController
    {
        private readonly ILogger<ExceptionViewController> _logger;
        private readonly IConfiguration _configuration;
        
       
        private readonly MongoDbContext _context;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _contextAccessor;


        private IRedisService<rpc_range_plan_getfor_createupdate_DTO> _redisRangePlanService;
      

        public ExceptionViewController(
            IMapper mapper, ILogger<ExceptionViewController> logger, IHttpContextAccessor contextAccessor,
            IConfiguration configuration
           )
        {
            
           
            _mapper = mapper;
            _logger = logger;
          
            _contextAccessor = contextAccessor;
           
            _logger.LogInformation("Hello from inside ExceptionView !");
            _configuration = configuration;
            
            _redisRangePlanService = new RedisService<rpc_range_plan_getfor_createupdate_DTO>(_configuration);

          
            
            _context = new MongoDbContext("mongodb://localhost:27017", "log_sailor_project");
        }


       

  

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetLogs(int draw, int start, int length)
        {
            int page = (start / length) + 1;
            long TotalCount = 0;
            var logs = _context.GetPagedLogs(page, length, out TotalCount);

            var result = new
            {
                draw = draw,
                recordsTotal = logs.Count,
                recordsFiltered = TotalCount,
                data = logs.Select(log => new
                {
                    timestamp = log.Timestamp,
                    level = log.Level,
                    RequestPath = JsonConvert.SerializeObject(log.Properties.Elements),
                    //SourceContext = log.Properties.SourceContext,
                    messageTemplate = log.MessageTemplate
                   
                }).ToList()
            };

            return Json(result);
        }

    }


    public class LogEntry
    {
        public ObjectId Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string MessageTemplate { get; set; }
        public string RenderedMessage { get; set; }
        public BsonDocument Properties { get; set; }

        public string UtcTimestamp { get; set; }
    }
    //public class LogEntry
    //{
    //    public ObjectId Id { get; set; }
    //    public DateTime Timestamp { get; set; }
    //    public string Level { get; set; }
    //    public string MessageTemplate { get; set; }
    //    public string RenderedMessage { get; set; }
    //    public LogEntryProperties Properties { get; set; }
    //    public string UtcTimestamp { get; set; }
    //}
    public class LogEntryProperties
    {
        public string SourceContext { get; set; }
        public bool SpecialLogType { get; set; }
        public string RequestId { get; set; }
        public string RequestPath { get; set; }
        // Add more properties as needed
    }


    public class MongoDbContext
    {
        private readonly IMongoCollection<LogEntry> _logEntries;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _logEntries = database.GetCollection<LogEntry>("db_custom_log");
        }

        public List<LogEntry> GetPagedLogs(int page, int pageSize, out long TotalCount)
        {
            var filter = Builders<LogEntry>.Filter.Empty;

           // var sort = Builders<LogEntry>.Sort.Descending(entry => entry.Timestamp);

            var logs = _logEntries.Find(filter)
                //.Sort(sort)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .SortByDescending(entry => entry.Timestamp)
                .ToList();
             TotalCount = _logEntries.EstimatedDocumentCount();
        
            return logs;
        }
    }

}
