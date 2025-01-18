using AutoMapper;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.TranTables;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ServiceStack;

namespace EPYSLSAILORAPP.Infrastructure.Services.GenServices
{
    public class Tran_BP_EventMonthOutletService : ITran_BP_EventMonthOutletService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public Tran_BP_EventMonthOutletService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
       }

        public async Task<List<tran_bp_event_month_outlet_dto>> get_tran_bp_event_month_outletService_GetAll(Int64? tran_bp_event_monthid)
        {
            try
            {
               
                List<tran_bp_event_month_outlet_dto> retList = new List<tran_bp_event_month_outlet_dto>();

                if (tran_bp_event_monthid.HasValue)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = $"SELECT * FROM tran_bp_event_month_outlet where tran_bp_event_month_id=@tran_bp_event_month_id";

                        var dataList = connection.Query<tran_bp_event_month_outlet_dto>(query,
                              new { tran_bp_event_monthid }
                             ).AsList();

                        return dataList;
                    }
                }
                else
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = $"SELECT * FROM tran_bp_event_month_outlet";

                        var dataList = connection.Query<tran_bp_event_month_outlet_dto>(query).AsList();

                        return dataList;

                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<rpc_tran_bp_event_month_outlet>> get_tran_bp_event_month_outletService_GetByTran_event_MonthIDs(Int64 fiscal_year_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_tran_bp_event_month_outlet( @filter_fiscal_year_id)";
                    var dataList = connection.Query<rpc_tran_bp_event_month_outlet>(
                            query,
                            new { filter_fiscal_year_id = fiscal_year_id },
                            buffered: true, // Disable buffering to prevent Npgsql from attempting to load all data into memory
                            commandTimeout: 300  // Set a command timeout (in seconds) if the query might take longer to execute
                        ).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
