using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;

using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Domain.Entity.Tran_Tables;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using Npgsql;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt
{

    public class TranprecostingitemsubcontractdetailService : ITranprecostingitemsubcontractdetailService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranprecostingitemsubcontractdetailService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
     
        }

        public async Task<bool> SaveAsync(tran_pre_costing_item_subcontract_detail_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_pre_costing_item_subcontract_detail_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_pre_costing_item_subcontract_detail_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_pre_costing_item_subcontract_detail_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_pre_costing_item_subcontract_detail_entity>> GetAllAsync()
        {
            List<tran_pre_costing_item_subcontract_detail_entity> list = new List<tran_pre_costing_item_subcontract_detail_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_pre_costing_item_subcontract_detail_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_pre_costing_item_subcontract_detail_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<tran_pre_costing_item_subcontract_detail_entity>> GetAsync(long Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_pre_costing_item_subcontract_detail m   where m.tran_pre_costing_item_subcontract_detail_id=@Id";

                    var dataList = connection.Query<tran_pre_costing_item_subcontract_detail_entity>(query,
                        new { @Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_pre_costing_item_subcontract_detail_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

