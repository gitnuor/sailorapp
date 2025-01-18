using AutoMapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.Interface.Gen_Tables;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services.GenServices
{

    public class GenBusinessTypeService : IGenBusinessTypeService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenBusinessTypeService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_business_type_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_business_type_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_business_type_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_business_type_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_business_type_entity>> GetAllAsync()
        {
            List<gen_business_type_entity> list = new List<gen_business_type_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_business_type_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_business_type_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_business_type_entity>> GetAsync(long Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_business_type m   where m.gen_business_type_id=@Id";

                    var dataList = connection.Query<gen_business_type_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;
                    
                    //JsonConvert.DeserializeObject<List<gen_business_type_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
    }

}

