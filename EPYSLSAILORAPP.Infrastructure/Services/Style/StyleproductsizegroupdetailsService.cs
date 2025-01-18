
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using Postgrest;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{
    public class StyleproductsizegroupdetailsService : IStyleproductsizegroupdetailsService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public StyleproductsizegroupdetailsService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
         }

        public async Task<bool> SaveAsync( style_product_size_group_details_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<style_product_size_group_details_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(style_product_size_group_details_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<style_product_size_group_details_entity>(JsonConvert.SerializeObject(entity));

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

      

        public async Task<List<style_product_size_group_details_entity>> GetAllAsync()
        {
            List<style_product_size_group_details_entity> list = new List<style_product_size_group_details_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<style_product_size_group_details_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<style_product_size_group_details_entity>> GetAsync(Int64? Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM style_product_size_group_details m   where m.style_product_size_group_id=@Id";

                    var dataList = connection.Query<style_product_size_group_details_entity>(query,
                        new { @Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<style_product_size_group_details_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
    }

}

