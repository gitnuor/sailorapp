using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using static Postgrest.Constants;
using AutoMapper;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using static Postgrest.QueryOptions;
using Postgrest;
using Microsoft.Extensions.Logging;
using Npgsql;
using Dapper.Contrib.Extensions;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using NUnit.Framework.Internal.Execution;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranDraftPurchaseRequisitionService : ITranDraftPurchaseRequisitionService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranDraftPurchaseRequisitionService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_draft_purchase_requisition_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_draft_purchase_requisition_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_draft_purchase_requisition_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_draft_purchase_requisition_entity>(JsonConvert.SerializeObject(entity));

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



        public async Task<List<tran_draft_purchase_requisition_entity>> GetAllAsync()
        {
            List<tran_draft_purchase_requisition_entity> list = new List<tran_draft_purchase_requisition_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_draft_purchase_requisition_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_draft_purchase_requisition_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_draft_purchase_requisition_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_draft_purchase_requisition m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.dpr_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_draft_purchase_requisition_entity>(query,
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



        public async Task<tran_draft_purchase_requisition_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*, gsi.name supplier_name, gu.unit_name delivery_unit_name,(
                                                    select jsonb_agg(
                                                           jsonb_build_object(
                                                                   'dpr_id', tdprd.dpr_id,
                                                                   'item_id', tdprd.item_id,
                                                                   'item_quantity', tdprd.item_quantity,
                                                                   'unit_price', tdprd.unit_price,
                                                                   'total_price', tdprd.total_price,
                                                                   'uom', tdprd.uom,
                                                                   'current_state', tdprd.current_state
                                                               )
                                                       )
                                                   from tran_draft_purchase_requisition_dtl tdprd 
                                                       where tdprd.dpr_id=m.dpr_id ) as detl_list
                                    FROM tran_draft_purchase_requisition m
                                    INNER JOIN gen_supplier_information gsi ON  gsi.gen_supplier_information_id = m.supplier_id
                                    inner join gen_unit gu on gu.gen_unit_id=m.delivery_unit_id
                                    where m.dpr_id=@Id";

                    var dataList = connection.Query<tran_draft_purchase_requisition_DTO>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_draft_purchase_requisition_DTO>> GetAllDraftTranPurchaseRequisitionAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_draft_purchase_requisitionn( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id)";

                    var dataList = connection.Query<rpc_draft_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id
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

        public async Task<bool> DeleteAsync(Int64? Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new tran_draft_purchase_requisition_entity { dpr_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_draft_purchase_requisition_insert_sp(tran_draft_purchase_requisition_DTO objtran_draft_purchase_requisition)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_draft_purchase_requisition_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_dpr_no", NpgsqlDbType.Text, objtran_draft_purchase_requisition.dpr_no == null ? DBNull.Value : objtran_draft_purchase_requisition.dpr_no);
                        Command.Parameters.AddWithValue("in_dpr_date", NpgsqlDbType.Date, objtran_draft_purchase_requisition.dpr_date == null ? DBNull.Value : objtran_draft_purchase_requisition.dpr_date);
                        Command.Parameters.AddWithValue("in_delivery_date", NpgsqlDbType.Date, objtran_draft_purchase_requisition.delivery_date == null ? DBNull.Value : objtran_draft_purchase_requisition.delivery_date);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.event_id == null ? DBNull.Value : objtran_draft_purchase_requisition.event_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.techpack_id == null ? DBNull.Value : objtran_draft_purchase_requisition.techpack_id);
                        Command.Parameters.AddWithValue("in_designer_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.designer_id == null ? DBNull.Value : objtran_draft_purchase_requisition.designer_id);
                        Command.Parameters.AddWithValue("in_merchandiser_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.merchandiser_id == null ? DBNull.Value : objtran_draft_purchase_requisition.merchandiser_id);
                        Command.Parameters.AddWithValue("in_currency_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.currency_id == null ? DBNull.Value : objtran_draft_purchase_requisition.currency_id);
                        Command.Parameters.AddWithValue("in_delivery_unit_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.delivery_unit_id == null ? DBNull.Value : objtran_draft_purchase_requisition.delivery_unit_id);
                        Command.Parameters.AddWithValue("in_delivery_unit_address", NpgsqlDbType.Text, objtran_draft_purchase_requisition.delivery_unit_address == null ? DBNull.Value : objtran_draft_purchase_requisition.delivery_unit_address);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_draft_purchase_requisition.remarks == null ? DBNull.Value : objtran_draft_purchase_requisition.remarks);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.supplier_id == null ? DBNull.Value : objtran_draft_purchase_requisition.supplier_id);
                        Command.Parameters.AddWithValue("in_supplier_address", NpgsqlDbType.Text, objtran_draft_purchase_requisition.supplier_address == null ? DBNull.Value : objtran_draft_purchase_requisition.supplier_address);
                        Command.Parameters.AddWithValue("in_supplier_concern_person", NpgsqlDbType.Text, objtran_draft_purchase_requisition.supplier_concern_person == null ? DBNull.Value : objtran_draft_purchase_requisition.supplier_concern_person);
                        Command.Parameters.AddWithValue("in_terms_condition_list", NpgsqlDbType.Text, objtran_draft_purchase_requisition.terms_condition_list == null ? DBNull.Value : objtran_draft_purchase_requisition.terms_condition_list);
                        Command.Parameters.AddWithValue("in_test_requirements_list", NpgsqlDbType.Text, objtran_draft_purchase_requisition.test_requirements_list == null ? DBNull.Value : objtran_draft_purchase_requisition.test_requirements_list);
                        Command.Parameters.AddWithValue("in_document_list", NpgsqlDbType.Text, objtran_draft_purchase_requisition.document_list == null ? DBNull.Value : objtran_draft_purchase_requisition.document_list);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, objtran_draft_purchase_requisition.is_submitted == null ? DBNull.Value : objtran_draft_purchase_requisition.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, objtran_draft_purchase_requisition.is_approved == null ? DBNull.Value : objtran_draft_purchase_requisition.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.approved_by == null ? DBNull.Value : objtran_draft_purchase_requisition.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_draft_purchase_requisition.approve_date == null ? DBNull.Value : objtran_draft_purchase_requisition.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_draft_purchase_requisition.approve_remarks == null ? DBNull.Value : objtran_draft_purchase_requisition.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.added_by == null ? DBNull.Value : objtran_draft_purchase_requisition.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_draft_purchase_requisition.date_added == null ? DBNull.Value : objtran_draft_purchase_requisition.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.updated_by == null ? DBNull.Value : objtran_draft_purchase_requisition.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_draft_purchase_requisition.date_updated == null ? DBNull.Value : objtran_draft_purchase_requisition.date_updated);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.gen_item_structure_group_id == null ? DBNull.Value : objtran_draft_purchase_requisition.gen_item_structure_group_id);
                        Command.Parameters.AddWithValue("in_is_acknowledged", NpgsqlDbType.Boolean, objtran_draft_purchase_requisition.is_acknowledged == null ? DBNull.Value : objtran_draft_purchase_requisition.is_acknowledged);
                        Command.Parameters.AddWithValue("in_acknowledged_by", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.acknowledged_by == null ? DBNull.Value : objtran_draft_purchase_requisition.acknowledged_by);
                        Command.Parameters.AddWithValue("in_acknowledged_date", NpgsqlDbType.Date, objtran_draft_purchase_requisition.acknowledged_date == null ? DBNull.Value : objtran_draft_purchase_requisition.acknowledged_date);
                        Command.Parameters.AddWithValue("in_acknowledged_remarks", NpgsqlDbType.Text, objtran_draft_purchase_requisition.acknowledged_remarks == null ? DBNull.Value : objtran_draft_purchase_requisition.acknowledged_remarks);
                        Command.Parameters.AddWithValue("in_detl_list", NpgsqlDbType.Text, objtran_draft_purchase_requisition.detl_list == null ? DBNull.Value : objtran_draft_purchase_requisition.detl_list);


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
        public async Task<bool> tran_draft_purchase_requisition_update_sp(tran_draft_purchase_requisition_DTO objtran_draft_purchase_requisition)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_draft_purchase_requisition_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_dpr_no", NpgsqlDbType.Text, objtran_draft_purchase_requisition.dpr_no == null ? DBNull.Value : objtran_draft_purchase_requisition.dpr_no);
                        Command.Parameters.AddWithValue("in_dpr_date", NpgsqlDbType.Date, objtran_draft_purchase_requisition.dpr_date == null ? DBNull.Value : objtran_draft_purchase_requisition.dpr_date);
                        Command.Parameters.AddWithValue("in_delivery_date", NpgsqlDbType.Date, objtran_draft_purchase_requisition.delivery_date == null ? DBNull.Value : objtran_draft_purchase_requisition.delivery_date);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.event_id == null ? DBNull.Value : objtran_draft_purchase_requisition.event_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.techpack_id == null ? DBNull.Value : objtran_draft_purchase_requisition.techpack_id);
                        Command.Parameters.AddWithValue("in_designer_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.designer_id == null ? DBNull.Value : objtran_draft_purchase_requisition.designer_id);
                        Command.Parameters.AddWithValue("in_merchandiser_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.merchandiser_id == null ? DBNull.Value : objtran_draft_purchase_requisition.merchandiser_id);
                        Command.Parameters.AddWithValue("in_currency_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.currency_id == null ? DBNull.Value : objtran_draft_purchase_requisition.currency_id);
                        Command.Parameters.AddWithValue("in_delivery_unit_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.delivery_unit_id == null ? DBNull.Value : objtran_draft_purchase_requisition.delivery_unit_id);
                        Command.Parameters.AddWithValue("in_delivery_unit_address", NpgsqlDbType.Text, objtran_draft_purchase_requisition.delivery_unit_address == null ? DBNull.Value : objtran_draft_purchase_requisition.delivery_unit_address);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_draft_purchase_requisition.remarks == null ? DBNull.Value : objtran_draft_purchase_requisition.remarks);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.supplier_id == null ? DBNull.Value : objtran_draft_purchase_requisition.supplier_id);
                        Command.Parameters.AddWithValue("in_supplier_address", NpgsqlDbType.Text, objtran_draft_purchase_requisition.supplier_address == null ? DBNull.Value : objtran_draft_purchase_requisition.supplier_address);
                        Command.Parameters.AddWithValue("in_supplier_concern_person", NpgsqlDbType.Text, objtran_draft_purchase_requisition.supplier_concern_person == null ? DBNull.Value : objtran_draft_purchase_requisition.supplier_concern_person);
                        Command.Parameters.AddWithValue("in_terms_condition_list", NpgsqlDbType.Text, objtran_draft_purchase_requisition.terms_condition_list == null ? DBNull.Value : objtran_draft_purchase_requisition.terms_condition_list);
                        Command.Parameters.AddWithValue("in_test_requirements_list", NpgsqlDbType.Text, objtran_draft_purchase_requisition.test_requirements_list == null ? DBNull.Value : objtran_draft_purchase_requisition.test_requirements_list);
                        Command.Parameters.AddWithValue("in_document_list", NpgsqlDbType.Text, objtran_draft_purchase_requisition.document_list == null ? DBNull.Value : objtran_draft_purchase_requisition.document_list);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, objtran_draft_purchase_requisition.is_submitted == null ? DBNull.Value : objtran_draft_purchase_requisition.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, objtran_draft_purchase_requisition.is_approved == null ? DBNull.Value : objtran_draft_purchase_requisition.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.approved_by == null ? DBNull.Value : objtran_draft_purchase_requisition.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_draft_purchase_requisition.approve_date == null ? DBNull.Value : objtran_draft_purchase_requisition.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_draft_purchase_requisition.approve_remarks == null ? DBNull.Value : objtran_draft_purchase_requisition.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.added_by == null ? DBNull.Value : objtran_draft_purchase_requisition.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_draft_purchase_requisition.date_added == null ? DBNull.Value : objtran_draft_purchase_requisition.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.updated_by == null ? DBNull.Value : objtran_draft_purchase_requisition.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_draft_purchase_requisition.date_updated == null ? DBNull.Value : objtran_draft_purchase_requisition.date_updated);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.gen_item_structure_group_id == null ? DBNull.Value : objtran_draft_purchase_requisition.gen_item_structure_group_id);
                        Command.Parameters.AddWithValue("in_is_acknowledged", NpgsqlDbType.Boolean, objtran_draft_purchase_requisition.is_acknowledged == null ? DBNull.Value : objtran_draft_purchase_requisition.is_acknowledged);
                        Command.Parameters.AddWithValue("in_acknowledged_by", NpgsqlDbType.Bigint, objtran_draft_purchase_requisition.acknowledged_by == null ? DBNull.Value : objtran_draft_purchase_requisition.acknowledged_by);
                        Command.Parameters.AddWithValue("in_acknowledged_date", NpgsqlDbType.Date, objtran_draft_purchase_requisition.acknowledged_date == null ? DBNull.Value : objtran_draft_purchase_requisition.acknowledged_date);
                        Command.Parameters.AddWithValue("in_acknowledged_remarks", NpgsqlDbType.Text, objtran_draft_purchase_requisition.acknowledged_remarks == null ? DBNull.Value : objtran_draft_purchase_requisition.acknowledged_remarks);
                        Command.Parameters.AddWithValue("in_detl_list", NpgsqlDbType.Text, objtran_draft_purchase_requisition.detl_list == null ? DBNull.Value : objtran_draft_purchase_requisition.detl_list);


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

