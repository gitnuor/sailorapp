
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;

using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Domain.DTO;
using static Postgrest.Constants;
using EPYSLSAILORAPP.Domain.RPC;
using Postgrest;
using static Postgrest.QueryOptions;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Newtonsoft.Json.Linq;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class ShopFloorReturnService : IShopFloorReturnService
    {

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public ShopFloorReturnService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;

        }


        public async Task<bool> UpdateAsync(tran_shop_floor_return_entity entity, List<tran_shop_floor_return_details_entity> details)
        {
            try
            {


                try
                {


                    foreach (tran_shop_floor_return_details_entity item in details)
                    {
                        item.tran_mcd_requisition_issue_id = (long)entity.tran_mcd_requisition_issue_id;


                    }

                    entity.shop_floor_return_details_json = JArray.Parse(JsonConvert.SerializeObject(details)).ToString();

                    await tran_shop_floor_return_update_sp(entity);


                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> AckonwledgementUpdate(tran_shop_floor_return_entity entity)
        {
            try
            {

                await tran_shop_floor_return_update_sp(entity);
                return true;


            }
            catch (Exception ex)
            {

                throw (ex);
            }

        }


        public async Task<List<tran_shop_floor_return_DTO>> GetAllAsync()
        {
            List<tran_shop_floor_return_entity> list = new List<tran_shop_floor_return_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_shop_floor_return_DTO>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_shop_floor_return_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_shop_floor_return_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_shop_floor_return m
                                           where  
                                                     m.fiscal_year_id=@fiscal_year_id and m.event_id=@event_id
                                                     and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.return_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_shop_floor_return_DTO>(query,
                        new
                        {
                            fiscal_year_id = param.fiscal_year_id,
                            event_id = param.event_id,
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

        public async Task<List<tran_shop_floor_return_DTO>> GetAllPagedDataForSelect2(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_shop_floor_return m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.return_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_shop_floor_return_DTO>(query,
                        new
                        {
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

        public async Task<tran_shop_floor_return_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = @"SELECT m.*,
                                    (select jsonb_agg(
                                        jsonb_build_object(
                                'tran_shop_floor_return_details_id', sipsc.tran_shop_floor_return_details_id,
                                'tran_shop_floor_return_id', sipsc.tran_shop_floor_return_id,
                                'tran_mcd_requisition_issue_id', sipsc.tran_mcd_requisition_issue_id,
                                'item_id', sipsc.item_id,
                                'issue_quantity', sipsc.issue_quantity,
                                'return_quantity', sipsc.return_quantity,
                                'measurement_unit_id', sipsc.measurement_unit_id,
                                'measurement_unit_name', sipsc.measurement_unit_name,
                                'remarks', sipsc.remarks



                                                           )
                                                   )
                                        from tran_shop_floor_return_details sipsc
                                        where sipsc.tran_shop_floor_return_id = m.tran_shop_floor_return_id)
                                    as tran_shop_floor_return_details_json
                                    FROM tran_shop_floor_return m
                                    where m.tran_shop_floor_return_id =@Id";

                        var objdata = connection.Query<tran_shop_floor_return_DTO>(query,
                            new { Id = Id }).ToList().FirstOrDefault();

                        var objRet = JsonConvert.DeserializeObject<tran_shop_floor_return_DTO>(JsonConvert.SerializeObject(objdata));

                        if (objRet != null)
                        {
                            objRet.details = JsonConvert.DeserializeObject<List<tran_shop_floor_return_details_DTO>>(JsonConvert.SerializeObject(objdata.shop_floor_return_details_json));
                        }

                        return objRet;//
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }


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

                    var objToDelete = new tran_shop_floor_return_entity { tran_shop_floor_return_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> SaveAsync(tran_shop_floor_return_DTO objtran_shop_floor_return)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_shop_floor_return_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_requisition_slip_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.requisition_slip_id == null ? DBNull.Value : objtran_shop_floor_return.requisition_slip_id);
                        Command.Parameters.AddWithValue("in_tran_mcd_requisition_issue_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.tran_mcd_requisition_issue_id == null ? DBNull.Value : objtran_shop_floor_return.tran_mcd_requisition_issue_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.techpack_id == null ? DBNull.Value : objtran_shop_floor_return.techpack_id);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_shop_floor_return.is_submitted == null ? DBNull.Value : objtran_shop_floor_return.is_submitted);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.event_id == null ? DBNull.Value : objtran_shop_floor_return.event_id);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.fiscal_year_id == null ? DBNull.Value : objtran_shop_floor_return.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_shop_floor_return.added_by == null ? DBNull.Value : objtran_shop_floor_return.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_shop_floor_return.date_added == null ? DBNull.Value : objtran_shop_floor_return.date_added);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_shop_floor_return.remarks == null ? DBNull.Value : objtran_shop_floor_return.remarks);

                        Command.Parameters.AddWithValue("in_shop_floor_return_details_json", NpgsqlDbType.Text, objtran_shop_floor_return.shop_floor_return_details_json.Count < 1 ? DBNull.Value : objtran_shop_floor_return.shop_floor_return_details_json.ToString());


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

        public async Task<bool> tran_shop_floor_return_update_sp(tran_shop_floor_return_entity objtran_shop_floor_return)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_shop_floor_return_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;


                        Command.Parameters.AddWithValue("in_tran_shop_floor_return_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.tran_shop_floor_return_id == null ? DBNull.Value : objtran_shop_floor_return.tran_shop_floor_return_id);

                        Command.Parameters.AddWithValue("in_return_no", NpgsqlDbType.Text, objtran_shop_floor_return.return_no == null ? DBNull.Value : objtran_shop_floor_return.return_no);
                        Command.Parameters.AddWithValue("in_requisition_slip_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.requisition_slip_id == null ? DBNull.Value : objtran_shop_floor_return.requisition_slip_id);
                        Command.Parameters.AddWithValue("in_tran_mcd_requisition_issue_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.tran_mcd_requisition_issue_id == null ? DBNull.Value : objtran_shop_floor_return.tran_mcd_requisition_issue_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.techpack_id == null ? DBNull.Value : objtran_shop_floor_return.techpack_id);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_shop_floor_return.remarks == null ? DBNull.Value : objtran_shop_floor_return.remarks);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_shop_floor_return.is_submitted == null ? DBNull.Value : objtran_shop_floor_return.is_submitted);
                        Command.Parameters.AddWithValue("in_acknowledged_by", NpgsqlDbType.Bigint, objtran_shop_floor_return.acknowledged_by == null ? DBNull.Value : objtran_shop_floor_return.acknowledged_by);
                        Command.Parameters.AddWithValue("in_acknowledged_date", NpgsqlDbType.Date, objtran_shop_floor_return.acknowledged_date == null ? DBNull.Value : objtran_shop_floor_return.acknowledged_date);
                        Command.Parameters.AddWithValue("in_acknowledged_remarks", NpgsqlDbType.Text, objtran_shop_floor_return.acknowledged_remarks == null ? DBNull.Value : objtran_shop_floor_return.acknowledged_remarks);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.gen_item_structure_group_id == null ? DBNull.Value : objtran_shop_floor_return.gen_item_structure_group_id);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_sub_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.gen_item_structure_group_sub_id == null ? DBNull.Value : objtran_shop_floor_return.gen_item_structure_group_sub_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.event_id == null ? DBNull.Value : objtran_shop_floor_return.event_id);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_shop_floor_return.fiscal_year_id == null ? DBNull.Value : objtran_shop_floor_return.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_shop_floor_return.added_by == null ? DBNull.Value : objtran_shop_floor_return.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_shop_floor_return.date_added == null ? DBNull.Value : objtran_shop_floor_return.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_shop_floor_return.updated_by == null ? DBNull.Value : objtran_shop_floor_return.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_shop_floor_return.date_updated == null ? DBNull.Value : objtran_shop_floor_return.date_updated);
                        Command.Parameters.AddWithValue("in_is_acknowledged", NpgsqlDbType.Bigint, objtran_shop_floor_return.is_acknowledged == null ? DBNull.Value : objtran_shop_floor_return.is_acknowledged);
                        Command.Parameters.AddWithValue("in_shop_floor_return_details_json", NpgsqlDbType.Text, objtran_shop_floor_return.shop_floor_return_details_json == null ? DBNull.Value : objtran_shop_floor_return.shop_floor_return_details_json);


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

        public async Task<List<rpc_tran_shop_floor_return_DTO>> GetAllJoined_ShopFloorReturnAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_shop_floor_return_draft( @row_index,@page_size,@p_event_id,@p_fiscal_year_id)";

                    var dataList = connection.Query<rpc_tran_shop_floor_return_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              p_event_id = event_id,
                              p_fiscal_year_id = fiscal_year_id
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_shop_floor_return_DTO>> GetRejectedShopFloorReturnData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id)
        {


            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_shop_floor_return_rejected( @row_index,@page_size,@p_event_id,@p_fiscal_year_id)";

                    var dataList = connection.Query<rpc_tran_shop_floor_return_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              p_event_id = event_id,
                              p_fiscal_year_id = fiscal_year_id
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_shop_floor_return_DTO>> GetApprovedShopFloorReturnData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, string search)
        {


            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_shop_floor_return_approved( @row_index,@page_size,@p_event_id,@p_fiscal_year_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_shop_floor_return_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              p_event_id = event_id,
                              p_fiscal_year_id = fiscal_year_id,
                              search_text=search
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<List<rpc_tran_shop_floor_return_DTO>> GetSubmittedShopFloorReturnData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, string search)
        {



            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_shop_floor_return_submitted( @row_index,@page_size,@p_event_id,@p_fiscal_year_id, @search_text)";

                    var dataList = connection.Query<rpc_tran_shop_floor_return_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              p_event_id = event_id,
                              p_fiscal_year_id = fiscal_year_id,
                              search_text=search
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<rpc_proc_sp_get_mcd_requisition_issue_DTO> Get_mcd_requisition_issueAsync(long? p_issue_id)
        {



            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_requisition_issue_for_floor_return( @p_issue_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_mcd_requisition_issue_DTO>(query,
                          new { p_issue_id = p_issue_id }
                         ).AsList().FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_shop_floor_return_DTO> GetSingleAsyncByReturnId(long? tran_shop_floor_return_id)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_floor_return_by_return_id( @p_return_id)";

                    var dataList = connection.Query<tran_shop_floor_return_DTO>(query,
                          new { p_return_id = tran_shop_floor_return_id }
                         ).AsList().FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }


        public async Task<bool> SendForApproval(long in_tran_shop_floor_return_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"update tran_shop_floor_return
                                     set is_submitted=2
                                     where tran_shop_floor_return_id=@in_tran_shop_floor_return_id";

                    connection.Execute(query,
                        new
                        {

                            in_tran_shop_floor_return_id = in_tran_shop_floor_return_id
                        });

                    return true;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<bool> Ackonwledge(tran_shop_floor_return_DTO request)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"update tran_shop_floor_return
                                     set is_acknowledged=1,acknowledged_date=@in_acknowledged_date,acknowledged_by=@in_acknowledged_by
                                     where tran_shop_floor_return_id=@in_tran_shop_floor_return_id";

                    connection.Execute(query,
                        new
                        {
                            in_acknowledged_date = request.acknowledged_date,
                            in_acknowledged_by = request.acknowledged_by,
                            in_tran_shop_floor_return_id = request.tran_shop_floor_return_id
                        });

                    return true;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

}

