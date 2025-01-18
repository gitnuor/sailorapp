using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Domain.DTO;
using Postgrest;
using static Postgrest.QueryOptions;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Newtonsoft.Json.Linq;
using static Postgrest.QueryOptions;
using Postgrest;

using Npgsql;
using Dapper.Contrib.Extensions;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using NUnit.Framework.Internal.Execution;
using Serilog.Context;
using Serilog;

namespace EPYSLSAILORAPP.Infrastructure.Services.BusinessPlanning
{

    public class TranrangeplanService : ITranrangeplanService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public TranrangeplanService(IConfiguration configuration, IMapper mapper, ILogger logger)
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_range_plan_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_range_plan_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    connection.Insert(objEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<tran_range_plan_entity>> GetAllByRangePlanIDAsync(long RangePlanID)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_range_plan m   where m.range_plan_id=@Id";

                    var dataList = connection.Query<tran_range_plan_entity>(query,
                        new { Id = RangePlanID }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_range_plan_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> SaveMasterDetailsAsync(tran_range_plan_DTO entity)
        {
            try
            {
                var objtran_range_plan = _mapper.Map<tran_range_plan_entity>(entity);


                if (objtran_range_plan.range_plan_id == null)
                {

                    var objtran_range_plan_event = _mapper.Map<tran_range_plan_events_entity>(entity.Event_Detail);

                    objtran_range_plan_event.added_by = entity.added_by;
                    objtran_range_plan_event.date_added = DateTime.Now;

                    objtran_range_plan_event.total_cpu_value = entity.DetList.Sum(p => p.cpu_value);
                    objtran_range_plan_event.total_mrp_value = entity.DetList.Sum(p => p.mrp_value);
                    objtran_range_plan_event.total_range_plan_quantity = entity.DetList.Sum(p => p.range_plan_quantity);
                    entity.range_plan_details_list_json = JArray.Parse(JsonConvert.SerializeObject(entity.DetList));

                    using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                var yearCommand = new NpgsqlCommand("tran_range_insert", connection);
                                yearCommand.CommandType = CommandType.StoredProcedure;

                                yearCommand.Parameters.AddWithValue("p_tran_bp_year_id", entity.tran_bp_year_id);
                                yearCommand.Parameters.AddWithValue("p_added_by", entity.added_by);
                                yearCommand.Parameters.AddWithValue("p_range_plan_details_list_json", entity.range_plan_details_list_json.ToString());
                                yearCommand.Parameters.AddWithValue("p_tran_bp_event_id", entity.Event_Detail.tran_bp_event_id);
                                yearCommand.Parameters.AddWithValue("p_total_mrp_value", entity.DetList.Sum(p => p.mrp_value));
                                yearCommand.Parameters.AddWithValue("p_total_cpu_value", entity.DetList.Sum(p => p.cpu_value));
                                yearCommand.Parameters.AddWithValue("p_total_range_plan_quantity", entity.DetList.Sum(p => p.range_plan_quantity));
                                yearCommand.Parameters.AddWithValue("p_is_event_finalized", objtran_range_plan_event.is_finalized);
                                yearCommand.Parameters.AddWithValue("p_remarks", entity.remarks);

                                yearCommand.Parameters[2].NpgsqlDbType = NpgsqlDbType.Text;
                                yearCommand.Parameters[8].NpgsqlDbType = NpgsqlDbType.Text;

                                yearCommand.ExecuteNonQuery();

                                transaction.Commit();

                                return true;
                            }
                            catch (Exception ex)
                            {
                                using (LogContext.PushProperty("SpecialLogType", true))
                                {
                                    _logger.Error(ex.ToString());
                                }

                                transaction.Rollback();
                                return false;
                            }
                        }
                    }


                }
                else
                {
                    var event_ind = 0;

                    objtran_range_plan.updated_by = entity.added_by;

                    var objtran_range_plan_event = _mapper.Map<tran_range_plan_events_entity>(entity.Event_Detail);

                    objtran_range_plan_event.updated_by = entity.added_by;

                    objtran_range_plan_event.total_cpu_value = entity.DetList.Where(p => p.range_plan_id == objtran_range_plan.range_plan_id
                    && p.tran_bp_event_id == objtran_range_plan_event.tran_bp_event_id).Sum(p => p.cpu_value);
                    objtran_range_plan_event.total_mrp_value = entity.DetList.Where(p => p.range_plan_id == objtran_range_plan.range_plan_id
                    && p.tran_bp_event_id == objtran_range_plan_event.tran_bp_event_id).Sum(p => p.mrp_value);
                    objtran_range_plan_event.total_range_plan_quantity = entity.DetList.Where(p => p.range_plan_id == objtran_range_plan.range_plan_id
                    && p.tran_bp_event_id == objtran_range_plan_event.tran_bp_event_id).Sum(p => p.range_plan_quantity);

                    entity.range_plan_details_list_json = JArray.Parse(JsonConvert.SerializeObject(entity.DetList));
                    using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                var yearCommand = new NpgsqlCommand("tran_range_update", connection);
                                yearCommand.CommandType = CommandType.StoredProcedure;

                                yearCommand.Parameters.AddWithValue("p_range_plan_id", entity.range_plan_id);
                                yearCommand.Parameters.AddWithValue("p_updated_by", entity.added_by);
                                yearCommand.Parameters.AddWithValue("p_range_plan_details_list_update", JArray.Parse(JsonConvert.SerializeObject(entity.DetList.Where(p => p.range_plan_detail_id != null).ToList())).ToString());
                                yearCommand.Parameters.AddWithValue("p_range_plan_details_list_insert", JArray.Parse(JsonConvert.SerializeObject(entity.DetList.Where(p => p.range_plan_detail_id == null).ToList())).ToString());
                                yearCommand.Parameters.AddWithValue("p_range_plan_details_list", entity.range_plan_details_list_json.ToString());
                                yearCommand.Parameters.AddWithValue("p_tran_range_plan_event_id", entity.Event_Detail.tran_range_plan_event_id == null ? DBNull.Value : entity.Event_Detail.tran_range_plan_event_id);
                                yearCommand.Parameters.AddWithValue("p_total_mrp_value", objtran_range_plan_event.total_mrp_value);
                                yearCommand.Parameters.AddWithValue("p_total_cpu_value", objtran_range_plan_event.total_cpu_value);
                                yearCommand.Parameters.AddWithValue("p_is_plan_submitted", entity.is_submitted);
                                yearCommand.Parameters.AddWithValue("p_is_event_finalized", objtran_range_plan_event.is_finalized);
                                yearCommand.Parameters.AddWithValue("p_total_range_plan_quantity", objtran_range_plan_event.total_range_plan_quantity);
                                yearCommand.Parameters.AddWithValue("p_remarks", entity.remarks);
                                yearCommand.Parameters.AddWithValue("p_tran_bp_event_id", objtran_range_plan_event.tran_bp_event_id);

                                yearCommand.Parameters[2].NpgsqlDbType = NpgsqlDbType.Text;
                                yearCommand.Parameters[3].NpgsqlDbType = NpgsqlDbType.Text;
                                yearCommand.Parameters[4].NpgsqlDbType = NpgsqlDbType.Text;
                                yearCommand.Parameters[11].NpgsqlDbType = NpgsqlDbType.Text;
                                yearCommand.Parameters[5].NpgsqlDbType = NpgsqlDbType.Bigint;

                                yearCommand.ExecuteNonQuery();

                                transaction.Commit();

                                return true;
                            }
                            catch (Exception ex)
                            {
                                // Handle exception, log, and optionally rollback the transaction
                                using (LogContext.PushProperty("SpecialLogType", true))
                                {
                                    _logger.Error(ex.ToString());
                                }

                                transaction.Rollback();
                                return false;
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> UpdateAsync(tran_range_plan_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_range_plan_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    connection.Update(objEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<tran_range_plan_entity>> GetAllAsync()
        {
            List<tran_range_plan_entity> list = new List<tran_range_plan_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_range_plan_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_range_plan_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<List<tran_range_plan_entity>> GetAsync(long Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_range_plan m   where m.range_plan_id=@Id";

                    var dataList = connection.Query<tran_range_plan_entity>(query,
                        new { Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_range_plan_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }

}

