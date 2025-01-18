using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt
{

    public class TransampleorderembellishmentService : ITransampleorderembellishmentService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TransampleorderembellishmentService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
     
        }

        public async Task<bool> SaveAsync(tran_sample_order_embellishment_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_sample_order_embellishment_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_sample_order_embellishment_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_sample_order_embellishment_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_sample_order_embellishment_entity>> GetAllAsync()
        {
            List<tran_sample_order_embellishment_entity> list = new List<tran_sample_order_embellishment_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_sample_order_embellishment_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_sample_order_embellishment_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_sample_order_embellishment_entity>> GetAsync(long Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_sample_order_embellishment m   where m.tran_sample_order_embellishment_id=@Id";

                    var dataList = connection.Query<tran_sample_order_embellishment_entity>(query,
                        new { @Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_sample_order_embellishment_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

