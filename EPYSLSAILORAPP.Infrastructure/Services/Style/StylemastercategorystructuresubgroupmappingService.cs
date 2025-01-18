
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using static Postgrest.QueryOptions;
using Postgrest;
using Microsoft.Extensions.Logging;
using Npgsql;
using Dapper.Contrib.Extensions;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using NUnit.Framework.Internal.Execution;
using Npgsql;
using NpgsqlTypes;
using System.Data;
namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class StylemastercategorystructuresubgroupmappingService : IStylemastercategorystructuresubgroupmappingService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public StylemastercategorystructuresubgroupmappingService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync( style_master_category_structure_subgroup_mapping_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<style_master_category_structure_subgroup_mapping_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(style_master_category_structure_subgroup_mapping_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<style_master_category_structure_subgroup_mapping_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<style_master_category_structure_subgroup_mapping_entity>> GetAllAsync()
        {
            List<style_master_category_structure_subgroup_mapping_entity> list = new List<style_master_category_structure_subgroup_mapping_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<style_master_category_structure_subgroup_mapping_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<style_master_category_structure_subgroup_mapping_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<style_master_category_structure_subgroup_mapping_entity>> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM style_master_category_structure_subgroup_mapping m   where m.master_category_structure_subgroup_mapping_id=@Id";

                    var dataList = connection.Query<style_master_category_structure_subgroup_mapping_entity>(query,
                        new { @Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<style_master_category_structure_subgroup_mapping_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

