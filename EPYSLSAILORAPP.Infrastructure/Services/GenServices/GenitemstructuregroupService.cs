
using AutoMapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenItemStructureGroupService : IGenItemStructureGroupService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenItemStructureGroupService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync( gen_item_structure_group_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_item_structure_group_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_item_structure_group_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_item_structure_group_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_item_structure_group_entity>> GetAllAsync()
        {
            List<gen_item_structure_group_entity> list = new List<gen_item_structure_group_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_item_structure_group_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_item_structure_group_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_item_structure_group_DTO>> GetAllAccessoriesGroups()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_item_structure_group m   
                            where m.item_structure_group_id!="+ Convert.ToInt64(Enum_gen_item_structure_group.Fabric).ToString();

                    var dataList = connection.Query<gen_item_structure_group_entity>(query).ToList();

                    return JsonConvert.DeserializeObject<List<gen_item_structure_group_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<List<gen_item_structure_group_entity>> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_item_structure_group m   where m.item_structure_group_id=@Id";

                    var dataList = connection.Query<gen_item_structure_group_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_item_structure_group_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

