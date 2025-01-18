using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO.TranTables;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity.Tran_Tables;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;

namespace EPYSLSAILORAPP.Infrastructure.Services.GenServices
{
    public class Tran_BP_EventMonthService : ITran_BP_EventMonthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public Tran_BP_EventMonthService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }


        public async Task<List<tran_bp_event_month_dto>> get_tran_BP_EventMonthService_GetAll(Int64? tran_bp_event_id)
        {
            try
            {
                List<tran_bp_event_month_dto> retList = new List<tran_bp_event_month_dto>();

                if (tran_bp_event_id.HasValue)
                {
                    try
                    {
                        using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                        {
                            connection.Open();

                            string query = @"SELECT m.*  FROM tran_bp_event_month m   
                            where m.tran_bp_event_id=@tran_bp_event_id";

                            var dataList = connection.Query<tran_bp_event_month>(query,
                                new { tran_bp_event_id = tran_bp_event_id }).ToList();

                            return JsonConvert.DeserializeObject<List<tran_bp_event_month_dto>>(JsonConvert.SerializeObject(dataList));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
                else
                {
                    try
                    {
                        using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                        {
                            connection.Open();

                            var dataList = connection.GetAll<tran_bp_event_month>().ToList();

                            return JsonConvert.DeserializeObject<List<tran_bp_event_month_dto>>(JsonConvert.SerializeObject(dataList));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
