
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using static Postgrest.QueryOptions;
using Postgrest;
using Microsoft.Extensions.Logging;
using Npgsql;
using Dapper.Contrib.Extensions;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using NUnit.Framework.Internal.Execution;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranrangeplaneventsService : ITranrangeplaneventsService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranrangeplaneventsService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
     }

        public async Task<bool> SaveAsync( tran_range_plan_events_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_range_plan_events_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    connection.Insert(objEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<bool> UpdateAsync(tran_range_plan_events_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_range_plan_events_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    connection.Update(objEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<tran_range_plan_events_entity>> GetAllAsync()
        {
            List<tran_range_plan_events_entity> list = new List<tran_range_plan_events_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_range_plan_events_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_range_plan_events_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<tran_range_plan_events_entity> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_range_plan_events m   where m.tran_range_plan_event_id=@Id";

                    var dataList = connection.Query<tran_range_plan_events_entity>(query,
                        new { Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_range_plan_events_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
    }

}

