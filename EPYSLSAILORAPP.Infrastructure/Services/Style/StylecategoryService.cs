
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class StylecategoryService : IStylecategoryService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public StylecategoryService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync( style_category_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<style_category_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(style_category_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<style_category_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<style_category_entity>> GetAllAsync()
        {
            List<style_category_entity> list = new List<style_category_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<style_category_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<style_category_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<style_category_entity>> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM style_category m   where m.style_category_id=@Id";

                    var dataList = connection.Query<style_category_entity>(query,
                        new { @Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<style_category_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

