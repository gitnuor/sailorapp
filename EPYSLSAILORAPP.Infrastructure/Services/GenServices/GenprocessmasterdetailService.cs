
using AutoMapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using static Dapper.SqlMapper;


namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenProcessMasterDetailService : IGenProcessMasterDetailService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenProcessMasterDetailService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_process_master_detail_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_process_master_detail_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_process_master_detail_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_process_master_detail_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_process_master_detail_entity>> GetAllAsync()
        {
            List<gen_process_master_detail_entity> list = new List<gen_process_master_detail_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_process_master_detail_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_process_master_detail_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_process_master_detail_entity>> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_process_master_detail m   where m.gen_process_master_detail_id=@Id";

                    var dataList = connection.Query<gen_process_master_detail_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_process_master_detail_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

    }

    
}



