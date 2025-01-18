
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;
using Npgsql;
using Dapper.Contrib.Extensions;


namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranrangeplandetailssizeService : ITranrangeplandetailssizeService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranrangeplandetailssizeService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync( tran_range_plan_details_size_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_range_plan_details_size_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_range_plan_details_size_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_range_plan_details_size_entity>(JsonConvert.SerializeObject(entity));

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
        //
      

        public async Task<List<tran_range_plan_details_size_entity>> GetAllAsync()
        {
            List<tran_range_plan_details_size_entity> list = new List<tran_range_plan_details_size_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_range_plan_details_size_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_range_plan_details_size_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_range_plan_details_size_entity>> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_range_plan_details_size m   where m.range_plan_detail_size_id=@Id";

                    var dataList = connection.Query<tran_range_plan_details_size_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_range_plan_details_size_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

