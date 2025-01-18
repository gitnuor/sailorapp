
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

    public class TrantechpackembellishmentinfoService : ITrantechpackembellishmentinfoService
    {

        private readonly IConfiguration _configuration;
     
        private readonly IMapper _mapper;

        public TrantechpackembellishmentinfoService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
            
}

        public async Task<bool> SaveAsync( tran_tech_pack_embellishment_info_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_tech_pack_embellishment_info_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_tech_pack_embellishment_info_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_tech_pack_embellishment_info_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_tech_pack_embellishment_info_entity>> GetAllAsync()
        {
            List<tran_tech_pack_embellishment_info_entity> list = new List<tran_tech_pack_embellishment_info_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_tech_pack_embellishment_info_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_info_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_tech_pack_embellishment_info_entity>> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_tech_pack_embellishment_info m   where m.tran_tech_pack_embellishment_info_id=@Id";

                    var dataList = connection.Query<tran_tech_pack_embellishment_info_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_tech_pack_embellishment_info_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

