
using AutoMapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using static Dapper.SqlMapper;
using EPYSLSAILORAPP.Application.Interface;


namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenItemStructureGroupSubSegmentMappingService : IGenItemStructureGroupSubSegmentMappingService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenItemStructureGroupSubSegmentMappingService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync( gen_item_structure_group_sub_segment_mapping_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_item_structure_group_sub_segment_mapping_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_item_structure_group_sub_segment_mapping_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_item_structure_group_sub_segment_mapping_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_item_structure_group_sub_segment_mapping_entity>> GetAllAsync()
        {
            List<gen_item_structure_group_sub_segment_mapping_entity> list = new List<gen_item_structure_group_sub_segment_mapping_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_item_structure_group_sub_segment_mapping_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_segment_mapping_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<gen_item_structure_group_sub_segment_mapping_entity>> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_item_structure_group_sub_segment_mapping m   where m.gen_item_structure_group_sub_segment_mapping_id=@Id";

                    var dataList = connection.Query<gen_item_structure_group_sub_segment_mapping_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_segment_mapping_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

