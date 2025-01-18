
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
using Newtonsoft.Json.Linq;
using Npgsql;
using Dapper.Contrib.Extensions;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranMcdPurchaseReturnService : ITranMcdPurchaseReturnService
    {

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public TranMcdPurchaseReturnService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;

        }

        public async Task<bool> SaveAsync(tran_mcd_purchase_return_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_mcd_purchase_return_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_mcd_purchase_return_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_mcd_purchase_return_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_mcd_purchase_return_DTO>> GetAllAsync()
        {
            List<tran_mcd_purchase_return_entity> list = new List<tran_mcd_purchase_return_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_mcd_purchase_return_DTO>().ToList();

                    return dataList;
                    //JsonConvert.DeserializeObject<List<tran_mcd_purchase_return_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_mcd_purchase_return_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_mcd_purchase_return m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.purchase_return_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_mcd_purchase_return_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    //return dataList;
                    return JsonConvert.DeserializeObject<List<tran_mcd_purchase_return_DTO>>(JsonConvert.SerializeObject(dataList));

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_mcd_purchase_return_DTO>> GetAllPagedDataForSelect2(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_mcd_purchase_return m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.purchase_return_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_mcd_purchase_return_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    //return dataList;
                    return JsonConvert.DeserializeObject<List<tran_mcd_purchase_return_DTO>>(JsonConvert.SerializeObject(dataList));

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<tran_mcd_purchase_return_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT tmr.receive_no,gtt.transport_type as vehical_type,m.*,
            (select jsonb_agg(
                jsonb_build_object(
        'purchase_return_detail_id', sipsc.purchase_return_detail_id,
        'purchase_return_id', sipsc.purchase_return_id,
        'gen_item_master_id', sipsc.gen_item_master_id,
        'po_quantity', sipsc.po_quantity,
        'measurement_unit_detail_id', sipsc.measurement_unit_detail_id,
        'measurement_unit', sipsc.measurement_unit,
        'receive_quantity', sipsc.receive_quantity,
        'receive_unit', sipsc.receive_unit,
        'reject_quantity', sipsc.reject_quantity,
        'return_quantity', sipsc.return_quantity,
        'remarks', sipsc.remarks,
        'po_id', sipsc.po_id,
         'item_name',gim.item_name,
 'item_spec',gim.item_spec,
                'unit_detail_display',tmrd.unit_detail_display




                                                           )
                                                   )
                  from tran_mcd_purchase_return_detail sipsc
                   INNER JOIN gen_item_master gim ON
                gim.gen_item_master_id = sipsc.gen_item_master_id
                   INNER JOIN gen_measurement_unit_detail tmrd ON
                      tmrd.gen_measurement_unit_detail_id=sipsc.measurement_unit_detail_id
                  where sipsc.purchase_return_id = m.purchase_return_id) as purchase_return_detail

                FROM tran_mcd_purchase_return m
                INNER JOIN tran_mcd_receive tmr ON 
                tmr.received_id=m.received_id
                
                INNER JOIN gen_tran_transport gtt ON 
                    gtt.transport_id=m.transport_type::bigint
                
                where m.purchase_return_id = @Id";

                    var dataList = connection.Query<tran_mcd_purchase_return_DTO>(query,
                        new { Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_mcd_purchase_return_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_mcd_purchase_return_entity { purchase_return_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<(bool success, long requisition_slip_id)> tran_mcd_purchase_return_insert_sp(tran_mcd_purchase_return_entity objtran_mcd_purchase_return, List<tran_mcd_purchase_return_detail_entity> details)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_mcd_purchase_returninsertt", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_purchase_return_no", NpgsqlDbType.Text, objtran_mcd_purchase_return.purchase_return_no == null ? DBNull.Value : objtran_mcd_purchase_return.purchase_return_no);
                        Command.Parameters.AddWithValue("in_received_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.received_id == null ? DBNull.Value : objtran_mcd_purchase_return.received_id);
                        Command.Parameters.AddWithValue("in_receive_date", NpgsqlDbType.Date, objtran_mcd_purchase_return.receive_date == null ? DBNull.Value : objtran_mcd_purchase_return.receive_date);
                        Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.po_id == null ? DBNull.Value : objtran_mcd_purchase_return.po_id);
                        Command.Parameters.AddWithValue("in_po_date", NpgsqlDbType.Date, objtran_mcd_purchase_return.po_date == null ? DBNull.Value : objtran_mcd_purchase_return.po_date);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.supplier_id == null ? DBNull.Value : objtran_mcd_purchase_return.supplier_id);
                        Command.Parameters.AddWithValue("in_del_chalan_no", NpgsqlDbType.Text, objtran_mcd_purchase_return.del_chalan_no == null ? DBNull.Value : objtran_mcd_purchase_return.del_chalan_no);
                        Command.Parameters.AddWithValue("in_del_chalan_date", NpgsqlDbType.Date, objtran_mcd_purchase_return.del_chalan_date == null ? DBNull.Value : objtran_mcd_purchase_return.del_chalan_date);
                        Command.Parameters.AddWithValue("in_transport_type", NpgsqlDbType.Text, objtran_mcd_purchase_return.transport_type == null ? DBNull.Value : objtran_mcd_purchase_return.transport_type);
                        Command.Parameters.AddWithValue("in_vehical_no", NpgsqlDbType.Text, objtran_mcd_purchase_return.vehical_no == null ? DBNull.Value : objtran_mcd_purchase_return.vehical_no);
                        Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, objtran_mcd_purchase_return.driver_name == null ? DBNull.Value : objtran_mcd_purchase_return.driver_name);
                        Command.Parameters.AddWithValue("in_driver_contact_no", NpgsqlDbType.Text, objtran_mcd_purchase_return.driver_contact_no == null ? DBNull.Value : objtran_mcd_purchase_return.driver_contact_no);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.added_by == null ? DBNull.Value : objtran_mcd_purchase_return.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_mcd_purchase_return.date_added == null ? DBNull.Value : objtran_mcd_purchase_return.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.updated_by == null ? DBNull.Value : objtran_mcd_purchase_return.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_mcd_purchase_return.date_updated == null ? DBNull.Value : objtran_mcd_purchase_return.date_updated);
                        Command.Parameters.AddWithValue("in_date_approved", NpgsqlDbType.Date, objtran_mcd_purchase_return.date_approved == null ? DBNull.Value : objtran_mcd_purchase_return.date_approved);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.gen_item_structure_group_id == null ? DBNull.Value : objtran_mcd_purchase_return.gen_item_structure_group_id);
                       
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.is_submitted == null ? DBNull.Value : objtran_mcd_purchase_return.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.submitted_by == null ? DBNull.Value : objtran_mcd_purchase_return.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.is_approved == null ? DBNull.Value : objtran_mcd_purchase_return.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.approved_by == null ? DBNull.Value : objtran_mcd_purchase_return.approved_by);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.fiscal_year_id == null ? DBNull.Value : objtran_mcd_purchase_return.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.event_id == null ? DBNull.Value : objtran_mcd_purchase_return.event_id);

                        Command.Parameters.AddWithValue("in_purchase_return_detail", NpgsqlDbType.Text, JArray.Parse(JsonConvert.SerializeObject(details)).ToString() == null ? DBNull.Value : JArray.Parse(JsonConvert.SerializeObject(details)).ToString());


                        Command.ExecuteNonQuery();

                        transaction.Commit();

                        return (true, objtran_mcd_purchase_return.purchase_return_id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                        return (false, 0);
                    }
                }
            }
        }


        public async Task<(bool success, long requisition_slip_id)> tran_mcd_purchase_return_update_sp(tran_mcd_purchase_return_entity objtran_mcd_purchase_return, List<tran_mcd_purchase_return_detail_entity> details)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_mcd_purchase_returnupdate", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_purchase_return_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.purchase_return_id == null ? DBNull.Value : objtran_mcd_purchase_return.purchase_return_id);
                        Command.Parameters.AddWithValue("in_purchase_return_no", NpgsqlDbType.Text, objtran_mcd_purchase_return.purchase_return_no == null ? DBNull.Value : objtran_mcd_purchase_return.purchase_return_no);
                        Command.Parameters.AddWithValue("in_received_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.received_id == null ? DBNull.Value : objtran_mcd_purchase_return.received_id);
                        Command.Parameters.AddWithValue("in_receive_date", NpgsqlDbType.Date, objtran_mcd_purchase_return.receive_date == null ? DBNull.Value : objtran_mcd_purchase_return.receive_date);
                        Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.po_id == null ? DBNull.Value : objtran_mcd_purchase_return.po_id);
                        Command.Parameters.AddWithValue("in_po_date", NpgsqlDbType.Date, objtran_mcd_purchase_return.po_date == null ? DBNull.Value : objtran_mcd_purchase_return.po_date);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.supplier_id == null ? DBNull.Value : objtran_mcd_purchase_return.supplier_id);
                        Command.Parameters.AddWithValue("in_del_chalan_no", NpgsqlDbType.Text, objtran_mcd_purchase_return.del_chalan_no == null ? DBNull.Value : objtran_mcd_purchase_return.del_chalan_no);
                        Command.Parameters.AddWithValue("in_del_chalan_date", NpgsqlDbType.Date, objtran_mcd_purchase_return.del_chalan_date == null ? DBNull.Value : objtran_mcd_purchase_return.del_chalan_date);
                        Command.Parameters.AddWithValue("in_transport_type", NpgsqlDbType.Text, objtran_mcd_purchase_return.transport_type == null ? DBNull.Value : objtran_mcd_purchase_return.transport_type);
                        Command.Parameters.AddWithValue("in_vehical_no", NpgsqlDbType.Text, objtran_mcd_purchase_return.vehical_no == null ? DBNull.Value : objtran_mcd_purchase_return.vehical_no);
                        Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, objtran_mcd_purchase_return.driver_name == null ? DBNull.Value : objtran_mcd_purchase_return.driver_name);
                        Command.Parameters.AddWithValue("in_driver_contact_no", NpgsqlDbType.Text, objtran_mcd_purchase_return.driver_contact_no == null ? DBNull.Value : objtran_mcd_purchase_return.driver_contact_no);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.added_by == null ? DBNull.Value : objtran_mcd_purchase_return.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_mcd_purchase_return.date_added == null ? DBNull.Value : objtran_mcd_purchase_return.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.updated_by == null ? DBNull.Value : objtran_mcd_purchase_return.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_mcd_purchase_return.date_updated == null ? DBNull.Value : objtran_mcd_purchase_return.date_updated);
                        Command.Parameters.AddWithValue("in_date_approved", NpgsqlDbType.Date, objtran_mcd_purchase_return.date_approved == null ? DBNull.Value : objtran_mcd_purchase_return.date_approved);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.gen_item_structure_group_id == null ? DBNull.Value : objtran_mcd_purchase_return.gen_item_structure_group_id);
                        
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.is_submitted == null ? DBNull.Value : objtran_mcd_purchase_return.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.submitted_by == null ? DBNull.Value : objtran_mcd_purchase_return.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.is_approved == null ? DBNull.Value : objtran_mcd_purchase_return.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.approved_by == null ? DBNull.Value : objtran_mcd_purchase_return.approved_by);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.fiscal_year_id == null ? DBNull.Value : objtran_mcd_purchase_return.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_mcd_purchase_return.event_id == null ? DBNull.Value : objtran_mcd_purchase_return.event_id);


                        Command.Parameters.AddWithValue("in_purchase_return_detail", NpgsqlDbType.Text, JArray.Parse(JsonConvert.SerializeObject(details)).ToString() == null ? DBNull.Value : JArray.Parse(JsonConvert.SerializeObject(details)).ToString());

                        Command.ExecuteNonQuery();

                        transaction.Commit();

                        return (true, objtran_mcd_purchase_return.purchase_return_id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                        return (false, 0);
                    }
                }
            }
        }



        public async Task<List<tran_mcd_purchase_return_DTO>> GetAllJoined_TranMcdPurchaseReturnAsync(Int64 row_index, Int64 page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_mcd_purchase_return( @row_index,@page_size)";

                    var dataList = connection.Query<tran_mcd_purchase_return_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size
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




        //public async Task<(bool success, long requisition_slip_id)> SaveAsync(tran_mcd_purchase_return_entity entity, List<tran_mcd_purchase_return_detail_entity> details)
        //{
        //    try
        //    {
        //        await _connectionSupabse.InitializeAsync();
        //        long purchase_return_id = 0;
        //        long gen_item_master_id = 0;

        //        // var model= _mapper.Map<tran_mcd_purchase_return_DTO>(entity);

        //        entity.purchase_return_detail = JArray.Parse(JsonConvert.SerializeObject(details));

        //        var retObj = await _connectionSupabse.From<tran_mcd_purchase_return_entity>().Insert(entity,
        //                         new QueryOptions { Returning = ReturnType.Representation });

        //        if (Convert.ToInt64(retObj.Models[0].PrimaryKey.FirstOrDefault().Value) > 0)
        //        {
        //            purchase_return_id = Convert.ToInt64(retObj.Models[0].PrimaryKey.FirstOrDefault().Value);

        //        };

        //        try
        //        {
        //            foreach (tran_mcd_purchase_return_detail_entity item in details)
        //            {
        //                item.purchase_return_id = purchase_return_id;
        //            }
        //            await _connectionSupabse.From<tran_mcd_purchase_return_detail_entity>().Insert(details);

        //            return (true, purchase_return_id);
        //        }
        //        catch (Exception ex)
        //        {
        //            await _connectionSupabse.From<tran_mcd_purchase_return_entity>().Where(x => x.purchase_return_id == entity.purchase_return_id).Delete();
        //            await _connectionSupabse.From<tran_mcd_purchase_return_detail_entity>().Where(x => x.purchase_return_id == entity.purchase_return_id).Delete();
        //            return (false, 0);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, 0);
        //    }

        //}

        public async Task<bool> ProposedAsync(tran_mcd_purchase_return_DTO request)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT public.tranPurchaseReturnPropose(@p_purchase_return_id,@p_submitted_by, @p_is_submitted)";
                    // Create the parameters
                    var parameters = new
                    {
                        p_purchase_return_id = request.purchase_return_id,
                        p_submitted_by=request.added_by,
                        p_is_submitted = 2
                    };

                    int rowsAffected = await connection.ExecuteAsync(sqlCommand, parameters);

                    if (rowsAffected < 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> ApproveAsync(tran_mcd_purchase_return_DTO request)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_mcd_purchase_return
                                    SET is_approved = 1
                                    WHERE purchase_return_id = @purchase_return_id;";

                    await connection.ExecuteAsync(query, new
                    {
                        purchase_return_id = request.purchase_return_id

                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

       

    }

}

