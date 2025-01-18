
using AutoMapper;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Enum;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Domain.Statics;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using Org.BouncyCastle.Ocsp;
using Postgrest;
using ServiceStack;
using Supabase;
using System.Data;
using System.Drawing.Printing;
using static Postgrest.QueryOptions;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranServiceWorkOrderService : ITranServiceWorkOrderService
    {
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public TranServiceWorkOrderService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;

        }

        public async Task<List<tran_tech_pack_DTO>> GetTranServiceOrderPendingListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 receivedID, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, string search)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {


                    await connection.OpenAsync();

                    string query = "SELECT * FROM proc_sp_get_data_tran_serviceorder_pendinglist(@row_index,@page_size,@query_type,@p_techpack_id,@fiscal_year_id,@event_id,@supplier_id,@search_text)";

                    var dataList = connection.Query<tran_tech_pack_DTO>(query,
                         new
                         {
                             row_index = row_index,
                             page_size = page_size,
                             query_type = query_type,

                             p_techpack_id = string.IsNullOrEmpty(Convert.ToString(receivedID)) ? (long?)null : Convert.ToInt64(receivedID),
                             fiscal_year_id = fiscal_year_id,
                             event_id = event_id,
                             supplier_id = supplier_id,
                             search_text = search
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

        public async Task<tran_tech_pack_DTO> GetEmbellishmentByTechpackAsync(Int64 row_index, Int64 page_size, string query_type, Int64 receivedID, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {

                    await connection.OpenAsync();
                    string sqlCommand = "SELECT * FROM proc_sp_get_data_tran_serviceorder_by_techpackid(@p_techpack_id)";

                    var parameters = new DynamicParameters();

                    parameters.Add("p_techpack_id", string.IsNullOrEmpty(Convert.ToString(receivedID)) ? null : Convert.ToInt64(receivedID));

                    var result = await connection.QueryAsync<tran_tech_pack_DTO>(
                      sqlCommand,
                      parameters
                    );

                    return result.OrderBy(x => x.techpack_number).FirstOrDefault();
                }



            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        //public async Task<(bool success, long service_work_order_id)> SaveAsync(tran_service_work_order_entity entity, List<tran_service_work_order_detail_entity> details)
        //{
        //    try
        //    {

        //        var model= _mapper.Map<tran_mcd_purchase_return_DTO>(entity);

        //        entity.work_order_detail_list = JArray.Parse(JsonConvert.SerializeObject(details));

        //        var service_work_order_id=await tran_service_work_order_insert_sp(entity);

        //        return (true, service_work_order_id);

        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, 0);
        //    }

        //}



        public async Task<bool> SaveAsync(tran_service_work_order_entity objtran_service_work_order, List<tran_service_work_order_detail_entity> details)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_service_work_order_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_service_work_order_no", NpgsqlDbType.Text, objtran_service_work_order.service_work_order_no == null ? DBNull.Value : objtran_service_work_order.service_work_order_no);
                        Command.Parameters.AddWithValue("in_service_work_date", NpgsqlDbType.Date, objtran_service_work_order.service_work_date == null ? DBNull.Value : objtran_service_work_order.service_work_date);
                        Command.Parameters.AddWithValue("in_gen_process_master_id", NpgsqlDbType.Bigint, objtran_service_work_order.gen_process_master_id == null ? DBNull.Value : objtran_service_work_order.gen_process_master_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_service_work_order.techpack_id == null ? DBNull.Value : objtran_service_work_order.techpack_id);
                        Command.Parameters.AddWithValue("in_tran_tech_pack_embellishment_info_id", NpgsqlDbType.Bigint, objtran_service_work_order.tran_tech_pack_embellishment_info_id == null ? DBNull.Value : objtran_service_work_order.tran_tech_pack_embellishment_info_id);
                        Command.Parameters.AddWithValue("in_delivery_date", NpgsqlDbType.Date, objtran_service_work_order.delivery_date == null ? DBNull.Value : objtran_service_work_order.delivery_date);
                        Command.Parameters.AddWithValue("in_delivery_unit_id", NpgsqlDbType.Bigint, objtran_service_work_order.delivery_unit_id == null ? DBNull.Value : objtran_service_work_order.delivery_unit_id);
                        Command.Parameters.AddWithValue("in_delivery_unit_address", NpgsqlDbType.Text, objtran_service_work_order.delivery_unit_address == null ? DBNull.Value : objtran_service_work_order.delivery_unit_address);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_service_work_order.remarks == null ? DBNull.Value : objtran_service_work_order.remarks);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_service_work_order.supplier_id == null ? DBNull.Value : objtran_service_work_order.supplier_id);
                        Command.Parameters.AddWithValue("in_supplier_address", NpgsqlDbType.Text, objtran_service_work_order.supplier_address == null ? DBNull.Value : objtran_service_work_order.supplier_address);
                        Command.Parameters.AddWithValue("in_supplier_concern_person", NpgsqlDbType.Text, objtran_service_work_order.supplier_concern_person == null ? DBNull.Value : objtran_service_work_order.supplier_concern_person);
                        Command.Parameters.AddWithValue("in_supplier_referrence", NpgsqlDbType.Text, objtran_service_work_order.supplier_referrence == null ? DBNull.Value : objtran_service_work_order.supplier_referrence);
                        Command.Parameters.AddWithValue("in_terms_condition_list", NpgsqlDbType.Text, objtran_service_work_order.terms_condition_list == null ? DBNull.Value : objtran_service_work_order.terms_condition_list);
                        Command.Parameters.AddWithValue("in_test_requirements_list", NpgsqlDbType.Text, objtran_service_work_order.test_requirements_list == null ? DBNull.Value : objtran_service_work_order.test_requirements_list);

                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_service_work_order.is_submitted == null ? DBNull.Value : objtran_service_work_order.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_service_work_order.submitted_by == null ? DBNull.Value : objtran_service_work_order.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_service_work_order.is_approved == null ? DBNull.Value : objtran_service_work_order.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_service_work_order.approved_by == null ? DBNull.Value : objtran_service_work_order.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_service_work_order.approve_date == null ? DBNull.Value : objtran_service_work_order.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_service_work_order.approve_remarks == null ? DBNull.Value : objtran_service_work_order.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_service_work_order.added_by == null ? DBNull.Value : objtran_service_work_order.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_service_work_order.date_added == null ? DBNull.Value : objtran_service_work_order.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_service_work_order.updated_by == null ? DBNull.Value : objtran_service_work_order.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_service_work_order.date_updated == null ? DBNull.Value : objtran_service_work_order.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_service_work_order.fiscal_year_id == null ? DBNull.Value : objtran_service_work_order.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_service_work_order.event_id == null ? DBNull.Value : objtran_service_work_order.event_id);

                        // Command.Parameters.AddWithValue("in_work_order_detail_list", NpgsqlDbType.Text, objtran_service_work_order.work_order_detail_list == null ? DBNull.Value : objtran_service_work_order.work_order_detail_list);
                        Command.Parameters.AddWithValue("in_work_order_detail_list", NpgsqlDbType.Text, JArray.Parse(JsonConvert.SerializeObject(details)).ToString() == null ? DBNull.Value : JArray.Parse(JsonConvert.SerializeObject(details)).ToString());

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

        public async Task<bool> UpdateAsync(tran_service_work_order_DTO entity)
        {

            try
            {

                foreach (var single in entity.details)
                {
                    single.date_updated = DateTime.Now;
                    single.updated_by = entity.updated_by;
                }

                entity.tran_service_work_order_detail_list = JsonConvert.SerializeObject(entity.details);//.ToString();

                var trem = JsonConvert.DeserializeObject<List<TermConditionDetail>>(JsonConvert.SerializeObject(entity.terms_conditions_list));

                var groupedTerms = trem.GroupBy(t => new { t.term_condition_name, t.gen_term_and_conditions_id })
                                            .Select(g => new TermConditionGrouped
                                            {
                                                term_condition_name = g.Key.term_condition_name,
                                                gen_term_and_conditions_id = g.Key.gen_term_and_conditions_id,
                                                Details = g.Select(d => new TermConditionDetailGrouped
                                                {
                                                    gen_term_and_conditions_details_id = d.gen_term_and_conditions_details_id,
                                                    description = d.description
                                                }).ToList()
                                            })
                                            .ToList();

                var groupedJson = JsonConvert.SerializeObject(groupedTerms);


                entity.terms_condition_list = JArray.Parse(groupedJson).ToString();



                return await tran_service_work_order_update_sp(entity);

            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<bool> tran_service_work_order_update_sp(tran_service_work_order_DTO objtran_service_work_order)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_service_work_order_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("in_service_work_order_id", NpgsqlDbType.Bigint, objtran_service_work_order.service_work_order_id == null ? DBNull.Value : objtran_service_work_order.service_work_order_id);
                        Command.Parameters.AddWithValue("in_service_work_order_no", NpgsqlDbType.Text, objtran_service_work_order.service_work_order_no == null ? DBNull.Value : objtran_service_work_order.service_work_order_no);
                        Command.Parameters.AddWithValue("in_service_work_date", NpgsqlDbType.Date, objtran_service_work_order.service_work_date == null ? DBNull.Value : objtran_service_work_order.service_work_date);

                        Command.Parameters.AddWithValue("in_terms_condition_list", NpgsqlDbType.Text, objtran_service_work_order.terms_condition_list == null ? DBNull.Value : objtran_service_work_order.terms_condition_list);

                        Command.Parameters.AddWithValue("in_work_order_detail_list", NpgsqlDbType.Text, objtran_service_work_order.tran_service_work_order_detail_list == null ? DBNull.Value : objtran_service_work_order.tran_service_work_order_detail_list);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_service_work_order.is_submitted == null ? DBNull.Value : objtran_service_work_order.is_submitted);

                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_service_work_order.updated_by == null ? DBNull.Value : objtran_service_work_order.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_service_work_order.date_updated == null ? DBNull.Value : objtran_service_work_order.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_service_work_order.fiscal_year_id == null ? DBNull.Value : objtran_service_work_order.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_service_work_order.event_id == null ? DBNull.Value : objtran_service_work_order.event_id);


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

        public async Task<bool> ProposedAsync(tran_service_work_order_DTO request)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_service_work_order
                                          SET submitted_by=@submitted_by,
                                              is_submitted = 2
                                          WHERE service_work_order_id = @service_work_order_id";

                    await connection.ExecuteAsync(query, new
                    {
                        submitted_by = request.added_by,
                        service_work_order_id = request.service_work_order_id

                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<tran_service_work_order_DTO>> GetTranServiceWorkOrderDraftListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, string search)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = "SELECT * FROM proc_sp_get_data_tran_mcd_service_workOrder_draft(@row_index,@page_size,@action_type,@fiscal_year_id,@p_event_id,@p_supplier_id,@search_text)";

                    var data = connection.Query<tran_service_work_order_DTO>(query, new
                    {
                        row_index = row_index,
                        page_size = page_size,
                        action_type = actiontype,
                        fiscal_year_id = fiscal_year_id,
                        p_event_id = event_id,
                        p_supplier_id = supplier_id,
                        search_text = search
                    }).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_service_work_order_DTO> Get_master_detail_tran_service_order_Async(Int64 service_work_order_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_service_work_order( @p_service_work_order_id)";

                    var dataList = connection.Query<tran_service_work_order_DTO>(query,
                          new
                          {

                              p_service_work_order_id = service_work_order_id
                          }
                         ).FirstOrDefault();

                    return dataList;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public List<rpc_proc_sp_get_colors_by_work_order_DTO> GetAllproc_sp_get_colors_by_work_order(Int64? p_work_order_id)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_colors_by_workorder( @p_work_order_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_colors_by_work_order_DTO>(query,
                          new { p_work_order_id }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> ApproveAsync(tran_service_work_order_DTO request)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @" UPDATE tran_service_work_order
                                        SET is_approved = 1
                                        WHERE service_work_order_id = @p_purchase_return_id";

                    await connection.ExecuteAsync(query, new
                    {
                        p_purchase_return_id = request.service_work_order_id
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

