
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Web.Core.Frame.Helpers;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranFabricAllocationReqService : ITranFabricAllocationReqService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranFabricAllocationReqService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;

        }

        public async Task<bool> SaveAsync(tran_fabric_allocation_req_DTO entity)
        {
            try
            {

                if (entity.List_Det.Count > 0)
                {
                    for (var index = 0; index < entity.List_Det.Count; index++)
                    {

                        entity.List_Det[index].date_added = entity.date_added;
                        entity.List_Det[index].added_by = entity.added_by;

                        entity.List_Det[index].item_master = entity.List_Det[index].item_master;

                        entity.List_Det[index].measurement_unit_detail = entity.List_Det[index].measurement_unit_detail;

                        if (!string.IsNullOrEmpty(entity.List_Det[index].tech_pack))
                            entity.List_Det[index].tech_pack = entity.List_Det[index].tech_pack;

                        if (!string.IsNullOrEmpty(entity.List_Det[index].prev_tech_pack))
                            entity.List_Det[index].prev_tech_pack = entity.List_Det[index].prev_tech_pack;

                    }

                    entity.detl_list = JsonConvert.SerializeObject(entity.List_Det);

                    return await tran_fabric_allocation_req_insert_sp(entity);

                }

                return false;


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(tran_fabric_allocation_req_DTO entity)
        {
            try
            {

                if (entity.List_Det.Count > 0)
                {
                    for (var index = 0; index < entity.List_Det.Count; index++)
                    {
                        entity.List_Det[index].date_updated = entity.date_added;
                        entity.List_Det[index].updated_by = entity.added_by;
                        entity.List_Det[index].date_added = entity.date_added;
                        entity.List_Det[index].added_by = entity.added_by;

                        entity.List_Det[index].measurement_unit_detail = entity.List_Det[index].measurement_unit_detail;

                        if (!string.IsNullOrEmpty(entity.List_Det[index].prev_tech_pack))
                            entity.List_Det[index].prev_tech_pack = entity.List_Det[index].prev_tech_pack;

                        if (!string.IsNullOrEmpty(entity.List_Det[index].tech_pack))
                            entity.List_Det[index].tech_pack = entity.List_Det[index].tech_pack;
                    }

                }

                entity.detl_list = JArray.Parse(JsonConvert.SerializeObject(entity.List_Det)).ToString();

                return await tran_fabric_allocation_req_update_sp(entity);


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_fabric_allocation_req_entity>> GetAllAsync()
        {
            List<tran_fabric_allocation_req_entity> list = new List<tran_fabric_allocation_req_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_fabric_allocation_req_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_fabric_allocation_req_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_fabric_allocation_req_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_fabric_allocation_req m
                                           where
                                                m.fiscal_year_id=@fiscal_year_id and m.event_id=@event_id
                                                and COALESCE(m.is_transfer, 0) =@is_transfer         
                                                and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.allocation_number ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_fabric_allocation_req_entity>(query,
                        new
                        {
                            fiscal_year_id=param.fiscal_year_id,
                            event_id=param.event_id,
                            is_transfer=param.FirstFilterID,
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        
        public async Task<bool> ApproveRejectAsync(tran_fabric_allocation_req_DTO entity)
        {
           
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"update tran_fabric_allocation_req set is_approved=@is_approved," +
                        $"approved_by=@approved_by,approved_date=@approved_date,is_submitted=@is_submitted where tran_fabric_allocation_req_id=@tran_fabric_allocation_req_id";

                    connection.Execute(query,
                          new
                          {
                              is_approved = entity.is_approved,
                              approved_by=entity.approved_by,
                              approved_date=entity.approved_date,
                              is_submitted=entity.is_submitted,
                              tran_fabric_allocation_req_id =entity.tran_fabric_allocation_req_id
                          });

                    return true;
                }
                
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_fabric_allocation_req_DTO> GetSingleAsync(Int64 Id)
        {
            var objData = new rpc_proc_sp_get_data_tran_fabric_allocation_req_details_DTO();

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_fabric_allocation_req_details( @tran_fabric_allocation_req_id_filter)";

                    objData = connection.Query<rpc_proc_sp_get_data_tran_fabric_allocation_req_details_DTO>(query,
                          new
                          {
                              tran_fabric_allocation_req_id_filter = Id
                          }
                         ).AsList().FirstOrDefault();
                }

                var ret = JsonConvert.DeserializeObject<tran_fabric_allocation_req_DTO>(JsonConvert.SerializeObject(objData));

                var ret_det = JsonConvert.DeserializeObject<List<tran_fabric_allocation_req_det_DTO>>(clsUtil.CleanJsonString(objData.detl_list));

                foreach (var obj in ret_det)
                {
                    obj.prev_tech_pack = obj.prev_tech_pack;
                    obj.tech_pack = obj.tech_pack;

                    obj.measurement_unit_detail = obj.measurement_unit_detail;
                    obj.item_master = obj.item_master;

                }

                ret.List_Det = ret_det;

                return ret;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> DeleteAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new tran_fabric_allocation_req_entity { tran_fabric_allocation_req_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<bool> tran_fabric_allocation_req_insert_sp(tran_fabric_allocation_req_DTO objtran_fabric_allocation_req)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_fabric_allocation_req_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_allocation_date", NpgsqlDbType.Date, objtran_fabric_allocation_req.allocation_date == null ? DBNull.Value : objtran_fabric_allocation_req.allocation_date);

                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_fabric_allocation_req.updated_by == null ? DBNull.Value : objtran_fabric_allocation_req.updated_by);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_fabric_allocation_req.added_by == null ? DBNull.Value : objtran_fabric_allocation_req.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_fabric_allocation_req.date_added == null ? DBNull.Value : objtran_fabric_allocation_req.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_fabric_allocation_req.date_updated == null ? DBNull.Value : objtran_fabric_allocation_req.date_updated);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_fabric_allocation_req.is_submitted == null ? DBNull.Value : objtran_fabric_allocation_req.is_submitted);
                        Command.Parameters.AddWithValue("in_is_transfer", NpgsqlDbType.Bigint, objtran_fabric_allocation_req.is_transfer == null ? DBNull.Value : objtran_fabric_allocation_req.is_transfer);

                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_fabric_allocation_req.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_fabric_allocation_req.event_id);


                        Command.Parameters.AddWithValue("in_allocation_number", NpgsqlDbType.Text, objtran_fabric_allocation_req.allocation_number == null ? DBNull.Value : objtran_fabric_allocation_req.allocation_number);

                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_fabric_allocation_req.remarks == null ? DBNull.Value : objtran_fabric_allocation_req.remarks);
                        Command.Parameters.AddWithValue("in_detl_list", NpgsqlDbType.Text, objtran_fabric_allocation_req.detl_list == null ? DBNull.Value : objtran_fabric_allocation_req.detl_list);

                        Command.ExecuteNonQuery();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public async Task<bool> tran_fabric_allocation_req_update_sp(tran_fabric_allocation_req_DTO objtran_fabric_allocation_req)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_fabric_allocation_req_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_fabric_allocation_req_id", NpgsqlDbType.Bigint, objtran_fabric_allocation_req.tran_fabric_allocation_req_id == null ? DBNull.Value : objtran_fabric_allocation_req.tran_fabric_allocation_req_id);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_fabric_allocation_req.is_submitted == null ? DBNull.Value : objtran_fabric_allocation_req.is_submitted);

                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_fabric_allocation_req.updated_by == null ? DBNull.Value : objtran_fabric_allocation_req.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_fabric_allocation_req.date_updated == null ? DBNull.Value : objtran_fabric_allocation_req.date_updated);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_fabric_allocation_req.remarks == null ? DBNull.Value : objtran_fabric_allocation_req.remarks);
                        
                        Command.Parameters.AddWithValue("in_detl_list", NpgsqlDbType.Text, objtran_fabric_allocation_req.detl_list == null ? DBNull.Value : objtran_fabric_allocation_req.detl_list);

                        Command.ExecuteNonQuery();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }

}

