using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.Entity.SupplyChain;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using static Dapper.SqlMapper;

using Newtonsoft.Json.Linq;
using Postgrest;
using static Postgrest.QueryOptions;
using EPYSLSAILORAPP.Application.Interface.Common;
using System.Drawing.Printing;
using Azure;
using Elasticsearch.Net;
using EPYSLSAILORAPP.Domain.DTO;
using Microsoft.Extensions.Logging;

namespace EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt
{

    public class TranPurchaseRequisitionService : ITranPurchaseRequisitionService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ICommonService _CommonService;
        private readonly IRPCDbService _RPCDbService;

        public TranPurchaseRequisitionService(IConfiguration configuration, IMapper mapper, ICommonService CommonService, IRPCDbService rPCDbService)
        {
            _CommonService = CommonService;
            _mapper = mapper;
            _configuration = configuration;
            _RPCDbService = rPCDbService;
        }

        public async Task<bool> SaveAsync(tran_purchase_requisition_DTO entity)
        {
            try
            {
                List<file_upload> files = await _CommonService.UploadFiles(entity.List_Files, "purchase_requisition");

                entity.document_list = JArray.Parse(JsonConvert.SerializeObject(files.ToList())).ToString();

                var model = JsonConvert.DeserializeObject<tran_purchase_requisition_entity>(JsonConvert.SerializeObject(entity));

                model.document_list = JArray.Parse(JsonConvert.SerializeObject(files.ToList())).ToString();

                model.del_list = JArray.Parse(JsonConvert.SerializeObject(entity.details.ToList())).ToString();

                return await tran_purchase_requisition_insert_sp(model);

                //return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public async Task<bool> UpdateAsync(tran_purchase_requisition_DTO entity, List<string> DeleteFiles)
        {
            try
            {
                List<file_upload> files = new List<file_upload>();

                tran_purchase_requisition_DTO ret = await GetSingleRequisitionAsync(entity.pr_id.Value);

                if (DeleteFiles.Count > 0)
                {
                    var file_list = JsonConvert.DeserializeObject<List<file_upload>>(entity.document_list);

                    if (file_list.Count > 0)
                    {
                        await _CommonService.Delete_Files(DeleteFiles, entity.List_Files, "purchase_requisition");
                    }
                }

                files = await _CommonService.UploadFiles(entity.List_Files, "purchase_requisition");



                entity.document_list = JArray.Parse(JsonConvert.SerializeObject(files)).ToString();

                entity.del_list = JArray.Parse(JsonConvert.SerializeObject(entity.details.ToList())).ToString();

                return await tran_purchase_requisition_update_sp(entity);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<bool> UpdateOpenPRAsync(tran_purchase_requisition_DTO entity, List<string> DeleteFiles)
        {
            try
            {
                List<file_upload> files = new List<file_upload>();

                tran_purchase_requisition_DTO ret = await GetSingleRequisitionWithoutTechpackAsync(entity.pr_id.Value);

                if (DeleteFiles.Count > 0)
                {
                    var file_list = JsonConvert.DeserializeObject<List<file_upload>>(entity.document_list);

                    if (file_list.Count > 0)
                    {
                        await _CommonService.Delete_Files(DeleteFiles, entity.List_Files, "purchase_requisition");
                    }
                }

                files = await _CommonService.UploadFiles(entity.List_Files, "purchase_requisition");



                entity.document_list = JArray.Parse(JsonConvert.SerializeObject(files)).ToString();

                entity.del_list = JArray.Parse(JsonConvert.SerializeObject(entity.details.ToList())).ToString();

                return await tran_purchase_requisition_update_sp(entity);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<bool> ExecuteAcknoledgeAsync(long pr_id, long added_by)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_purchase_requisition
                                          SET is_acknowledged = @is_acknowledged,
                                              acknowledged_by = @acknowledged_by,
                                              is_approved = @p_is_approved
                                          WHERE pr_id = @pr_id";

                    await connection.ExecuteAsync(query, new
                    {
                        is_acknowledged = true,
                        acknowledged_by = added_by,
                        acknowledged_date = DateTime.Now,
                        p_is_approved = 1,
                        pr_id


                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> ExecutePrApprovalAsync(long pr_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_purchase_requisition
                                          SET is_approved = 1,
                                              is_acknowledged = false
                                          WHERE pr_id = @pr_id";

                    await connection.ExecuteAsync(query, new
                    {
                        pr_id = pr_id,

                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        public async Task<tran_purchase_requisition_DTO> GetPR(long pr_id)
        {
            try
            {
                var ret_rpc = await _RPCDbService.GetAllproc_sp_get_data_tran_purchase_requisition_detailsAsync(pr_id);

                var ret = JsonConvert.DeserializeObject<tran_purchase_requisition_DTO>(JsonConvert.SerializeObject(ret_rpc));

                ret.details = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_DTO>>(ret_rpc.tran_purchase_requisition_dtl_list); ;

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<tran_purchase_requisition_entity>> GetAllAsync()
        {
            List<tran_purchase_requisition_entity> list = new List<tran_purchase_requisition_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_purchase_requisition_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<tran_purchase_requisition_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.pr_id, m.pr_no || '  (' || COALESCE(ttp.techpack_number, 'N/A') || ')'  AS pr_no
                                                FROM tran_purchase_requisition m
                                                left join tran_tech_pack ttp on m.techpack_id = ttp.tran_techpack_id

                                           where
                                                m.event_id=@event_id
                                                and case
                                                when @search_text is null then true
                                                when @search_text is not null and (
                                                    m.pr_no ilike '%' || @search_text || '%'
                                                    ) then true
                                                else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_purchase_requisition_entity>(query,
                    new
                    {
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
                throw ex;
            }

        }

        public async Task<List<tran_purchase_requisition_entity>> GetAllFabricDataAsync(PR_DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.pr_id, 
                                                m.pr_no || '  (' || COALESCE(ttp.techpack_number, 'N/A') || ')'  AS pr_no
                                                FROM tran_purchase_requisition m
                                                left join tran_tech_pack ttp on m.techpack_id = ttp.tran_techpack_id
                                           where m.gen_item_structure_group_id=1 and
                                                     m.event_id=@event_id
                                                     and case
                                                     when @search_text is null then true
                                                     when @search_text is not null and (
                                                            m.pr_no ilike '%' || @search_text || '%' or
                                                            ttp.techpack_number ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_purchase_requisition_entity>(query,
                    new
                    {
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
                throw ex;
            }

        }
        public async Task<List<tran_purchase_requisition_entity>> GetAllAccesoriesDataAsync(PR_DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.pr_id,
                                                m.pr_no || '  (' || COALESCE(ttp.techpack_number, 'N/A') || ')'  AS pr_no
                                                FROM tran_purchase_requisition m
                                                left join tran_tech_pack ttp on m.techpack_id = ttp.tran_techpack_id 
                                           where     m.gen_item_structure_group_id!=1 and
                                                     m.event_id=@event_id
                                                     and case
                                                     when @search_text is null then true
                                                     when @search_text is not null and (
                                                            m.pr_no ilike '%' || @search_text || '%' or
                                                            ttp.techpack_number ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_purchase_requisition_entity>(query,
                    new
                    {
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
                throw ex;
            }

        }

        public async Task<tran_purchase_requisition_DTO> GetSingleRequisitionAsync(long Id)
        {
            tran_purchase_requisition_DTO ret = new tran_purchase_requisition_DTO();


            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition_by_id( @pr_id_filter)";

                    var objData = connection.Query<rpc_proc_sp_get_data_tran_purchase_requisition_by_id_DTO>(query,
                          new
                          {
                              pr_id_filter = Id
                          }
                         ).AsList().FirstOrDefault();

                    ret = JsonConvert.DeserializeObject<tran_purchase_requisition_DTO>(JsonConvert.SerializeObject(objData));

                    ret.details = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_DTO>>(objData.tran_purchase_requisition_dtl_list);

                    if (!string.IsNullOrEmpty(ret.document_list))
                    {
                        var file_list = JsonConvert.DeserializeObject<List<file_upload>>(ret.document_list);

                        ret.List_Files = await _CommonService.LoadAllFiles(file_list, "purchase_requisition");
                    }


                }

                return ret;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<tran_purchase_requisition_DTO> GetSingleRequisitionWithoutTechpackAsync(long Id)
        {
            tran_purchase_requisition_DTO ret = new tran_purchase_requisition_DTO();


            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_pr_without_techpack_by_id( @pr_id_filter)";

                    var objData = connection.Query<rpc_proc_sp_get_data_tran_purchase_requisition_by_id_DTO>(query,
                          new
                          {
                              pr_id_filter = Id
                          }
                         ).AsList().FirstOrDefault();

                    ret = JsonConvert.DeserializeObject<tran_purchase_requisition_DTO>(JsonConvert.SerializeObject(objData));

                    ret.details = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_DTO>>(objData.tran_purchase_requisition_dtl_list);

                    if (!string.IsNullOrEmpty(ret.document_list))
                    {
                        var file_list = JsonConvert.DeserializeObject<List<file_upload>>(ret.document_list);

                        ret.List_Files = await _CommonService.LoadAllFiles(file_list, "purchase_requisition");
                    }


                }

                return ret;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<bool> DeleteAsync(long? Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new tran_purchase_requisition_entity { pr_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> tran_purchase_requisition_insert_sp(tran_purchase_requisition_entity objtran_purchase_requisition)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_purchase_requisition_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_pr_no", NpgsqlDbType.Text, objtran_purchase_requisition.pr_no == null ? DBNull.Value : objtran_purchase_requisition.pr_no);
                        Command.Parameters.AddWithValue("in_pr_date", NpgsqlDbType.Date, objtran_purchase_requisition.pr_date == null ? DBNull.Value : objtran_purchase_requisition.pr_date);
                        Command.Parameters.AddWithValue("in_delivery_date", NpgsqlDbType.Date, objtran_purchase_requisition.delivery_date == null ? DBNull.Value : objtran_purchase_requisition.delivery_date);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.event_id == null ? DBNull.Value : objtran_purchase_requisition.event_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.techpack_id == null ? DBNull.Value : objtran_purchase_requisition.techpack_id);
                        Command.Parameters.AddWithValue("in_designer_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.designer_id == null ? DBNull.Value : objtran_purchase_requisition.designer_id);
                        Command.Parameters.AddWithValue("in_merchandiser_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.merchandiser_id == null ? DBNull.Value : objtran_purchase_requisition.merchandiser_id);
                        Command.Parameters.AddWithValue("in_currency_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.currency_id == null ? DBNull.Value : objtran_purchase_requisition.currency_id);
                        Command.Parameters.AddWithValue("in_delivery_unit_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.delivery_unit_id == null ? DBNull.Value : objtran_purchase_requisition.delivery_unit_id);
                        Command.Parameters.AddWithValue("in_delivery_unit_address", NpgsqlDbType.Text, objtran_purchase_requisition.delivery_unit_address == null ? DBNull.Value : objtran_purchase_requisition.delivery_unit_address);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_purchase_requisition.remarks == null ? DBNull.Value : objtran_purchase_requisition.remarks);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.supplier_id == null ? DBNull.Value : objtran_purchase_requisition.supplier_id);
                        Command.Parameters.AddWithValue("in_supplier_address", NpgsqlDbType.Text, objtran_purchase_requisition.supplier_address == null ? DBNull.Value : objtran_purchase_requisition.supplier_address);
                        Command.Parameters.AddWithValue("in_supplier_concern_person", NpgsqlDbType.Text, objtran_purchase_requisition.supplier_concern_person == null ? DBNull.Value : objtran_purchase_requisition.supplier_concern_person);
                        Command.Parameters.AddWithValue("in_terms_condition_list", NpgsqlDbType.Text, objtran_purchase_requisition.terms_condition_list == null ? DBNull.Value : objtran_purchase_requisition.terms_condition_list);
                        Command.Parameters.AddWithValue("in_test_requirements_list", NpgsqlDbType.Text, objtran_purchase_requisition.test_requirements_list == null ? DBNull.Value : objtran_purchase_requisition.test_requirements_list);
                        Command.Parameters.AddWithValue("in_document_list", NpgsqlDbType.Text, objtran_purchase_requisition.document_list == null ? DBNull.Value : objtran_purchase_requisition.document_list);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_purchase_requisition.approved_by == null ? DBNull.Value : objtran_purchase_requisition.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_purchase_requisition.approve_date == null ? DBNull.Value : objtran_purchase_requisition.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_purchase_requisition.approve_remarks == null ? DBNull.Value : objtran_purchase_requisition.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_purchase_requisition.added_by == null ? DBNull.Value : objtran_purchase_requisition.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_purchase_requisition.date_added == null ? DBNull.Value : objtran_purchase_requisition.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_purchase_requisition.updated_by == null ? DBNull.Value : objtran_purchase_requisition.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_purchase_requisition.date_updated == null ? DBNull.Value : objtran_purchase_requisition.date_updated);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.gen_item_structure_group_id == null ? DBNull.Value : objtran_purchase_requisition.gen_item_structure_group_id);
                        Command.Parameters.AddWithValue("in_is_acknowledged", NpgsqlDbType.Boolean, objtran_purchase_requisition.is_acknowledged == null ? DBNull.Value : objtran_purchase_requisition.is_acknowledged);
                        Command.Parameters.AddWithValue("in_acknowledged_by", NpgsqlDbType.Bigint, objtran_purchase_requisition.acknowledged_by == null ? DBNull.Value : objtran_purchase_requisition.acknowledged_by);
                        Command.Parameters.AddWithValue("in_acknowledged_date", NpgsqlDbType.Date, objtran_purchase_requisition.acknowledged_date == null ? DBNull.Value : objtran_purchase_requisition.acknowledged_date);
                        Command.Parameters.AddWithValue("in_acknowledged_remarks", NpgsqlDbType.Text, objtran_purchase_requisition.acknowledged_remarks == null ? DBNull.Value : objtran_purchase_requisition.acknowledged_remarks);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_purchase_requisition.is_submitted == null ? DBNull.Value : objtran_purchase_requisition.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_purchase_requisition.is_approved == null ? DBNull.Value : objtran_purchase_requisition.is_approved);
                        Command.Parameters.AddWithValue("in_det_list", NpgsqlDbType.Text, objtran_purchase_requisition.del_list == null ? DBNull.Value : objtran_purchase_requisition.del_list);


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
        public async Task<bool> tran_purchase_requisition_update_sp(tran_purchase_requisition_DTO objtran_purchase_requisition)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_purchase_requisition_update", connection);


                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_pr_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.pr_id == null ? DBNull.Value : objtran_purchase_requisition.pr_id);
                        Command.Parameters.AddWithValue("in_pr_no", NpgsqlDbType.Text, objtran_purchase_requisition.pr_no == null ? DBNull.Value : objtran_purchase_requisition.pr_no);
                        Command.Parameters.AddWithValue("in_pr_date", NpgsqlDbType.Date, objtran_purchase_requisition.pr_date == null ? DBNull.Value : objtran_purchase_requisition.pr_date);
                        Command.Parameters.AddWithValue("in_delivery_date", NpgsqlDbType.Date, objtran_purchase_requisition.delivery_date == null ? DBNull.Value : objtran_purchase_requisition.delivery_date);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.techpack_id == null ? DBNull.Value : objtran_purchase_requisition.techpack_id);
                        Command.Parameters.AddWithValue("in_designer_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.designer_id == null ? DBNull.Value : objtran_purchase_requisition.designer_id);
                        Command.Parameters.AddWithValue("in_merchandiser_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.merchandiser_id == null ? DBNull.Value : objtran_purchase_requisition.merchandiser_id);
                        Command.Parameters.AddWithValue("in_currency_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.currency_id == null ? DBNull.Value : objtran_purchase_requisition.currency_id);
                        Command.Parameters.AddWithValue("in_delivery_unit_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.delivery_unit_id == null ? DBNull.Value : objtran_purchase_requisition.delivery_unit_id);
                        Command.Parameters.AddWithValue("in_delivery_unit_address", NpgsqlDbType.Text, objtran_purchase_requisition.delivery_unit_address == null ? DBNull.Value : objtran_purchase_requisition.delivery_unit_address);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_purchase_requisition.remarks == null ? DBNull.Value : objtran_purchase_requisition.remarks);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_purchase_requisition.supplier_id == null ? DBNull.Value : objtran_purchase_requisition.supplier_id);
                        Command.Parameters.AddWithValue("in_supplier_address", NpgsqlDbType.Text, objtran_purchase_requisition.supplier_address == null ? DBNull.Value : objtran_purchase_requisition.supplier_address);
                        Command.Parameters.AddWithValue("in_supplier_concern_person", NpgsqlDbType.Text, objtran_purchase_requisition.supplier_concern_person == null ? DBNull.Value : objtran_purchase_requisition.supplier_concern_person);
                        Command.Parameters.AddWithValue("in_terms_condition_list", NpgsqlDbType.Text, objtran_purchase_requisition.terms_condition_list == null ? DBNull.Value : objtran_purchase_requisition.terms_condition_list);
                        Command.Parameters.AddWithValue("in_test_requirements_list", NpgsqlDbType.Text, objtran_purchase_requisition.test_requirements_list == null ? DBNull.Value : objtran_purchase_requisition.test_requirements_list);
                        Command.Parameters.AddWithValue("in_document_list", NpgsqlDbType.Text, objtran_purchase_requisition.document_list == null ? DBNull.Value : objtran_purchase_requisition.document_list);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_purchase_requisition.approved_by == null ? DBNull.Value : objtran_purchase_requisition.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_purchase_requisition.approve_date == null ? DBNull.Value : objtran_purchase_requisition.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_purchase_requisition.approve_remarks == null ? DBNull.Value : objtran_purchase_requisition.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_purchase_requisition.added_by == null ? DBNull.Value : objtran_purchase_requisition.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_purchase_requisition.date_added == null ? DBNull.Value : objtran_purchase_requisition.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_purchase_requisition.updated_by == null ? DBNull.Value : objtran_purchase_requisition.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_purchase_requisition.date_updated == null ? DBNull.Value : objtran_purchase_requisition.date_updated);
                        Command.Parameters.AddWithValue("in_is_acknowledged", NpgsqlDbType.Boolean, objtran_purchase_requisition.is_acknowledged == null ? DBNull.Value : objtran_purchase_requisition.is_acknowledged);
                        Command.Parameters.AddWithValue("in_acknowledged_by", NpgsqlDbType.Bigint, objtran_purchase_requisition.acknowledged_by == null ? DBNull.Value : objtran_purchase_requisition.acknowledged_by);
                        Command.Parameters.AddWithValue("in_acknowledged_date", NpgsqlDbType.Date, objtran_purchase_requisition.acknowledged_date == null ? DBNull.Value : objtran_purchase_requisition.acknowledged_date);
                        Command.Parameters.AddWithValue("in_acknowledged_remarks", NpgsqlDbType.Text, objtran_purchase_requisition.acknowledged_remarks == null ? DBNull.Value : objtran_purchase_requisition.acknowledged_remarks);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_purchase_requisition.is_submitted == null ? DBNull.Value : objtran_purchase_requisition.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_purchase_requisition.is_approved == null ? DBNull.Value : objtran_purchase_requisition.is_approved);
                        Command.Parameters.AddWithValue("in_det_list", NpgsqlDbType.Text, objtran_purchase_requisition.del_list == null ? DBNull.Value : objtran_purchase_requisition.del_list);



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
        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionAsync(long row_index, long page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition( @row_index,@page_size)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<tran_purchase_requisition_DTO>> GetPurchaseRequisitionDropDownData(DtParameters param, long group)
        {
            string query = "";
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    if (group == 1)
                    {
                        query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_scm_po m
                                           where m.item_structure_group_id = " + Convert.ToInt64(Enum_gen_item_structure_group.Fabric).ToString() + @" and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.po_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";
                    }
                    else
                    {
                        query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_scm_po m
                                           where m.item_structure_group_id != " + Convert.ToInt64(Enum_gen_item_structure_group.Fabric).ToString() + @" and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.po_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";
                    }

                    var dataList = connection.Query<tran_scm_po_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    return JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(JsonConvert.SerializeObject(dataList));

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionPendingAckAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition_pending_ack( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              search_text= search
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionAckAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition_acknowledgement_ack( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              search_text= search
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionMerchandiseFabricAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id, long listType, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition_merchandise_fabric( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@list_type,@search_text)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              list_type = listType,
                              search_text= search
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionMerchantPendingApprovalAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id,string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_purchase_requisition_merchandise_pending_approval( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              search_text= search
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionMerchandise_Approved(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_purchase_requisition_merchandise_approved( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              search_text=search
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionMerchanAccessories(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id, long listType, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition_merchandise_acc( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@list_type, @search_text)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              list_type = listType,
                              search_text = search
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionAccessoriesAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id, long listType, string search_text)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition_acc( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@list_type,@search_text)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              list_type = listType,
                              search_text = search_text
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id, long listType, string search_text)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@list_type, @search_text)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              list_type = listType,
                              search_text= search_text
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition_pending_approvall( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              search_text= search
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionApprovedAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition_approvall( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_purchase_requisition_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              search_text= search
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<List<tran_purchase_requisition_dtl_DTO>> GetItemDetailsByTechpackforAcc(long p_techpack_id, long gen_item_structure_group_sub_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_item_detail_by_techpack_id_for_accpr( @p_techpack_id,@p_gen_item_structure_group_sub_id)";

                    var dataList = connection.Query<tran_purchase_requisition_dtl_DTO>(query,
                          new
                          {

                              p_techpack_id = p_techpack_id,
                              p_gen_item_structure_group_sub_id = gen_item_structure_group_sub_id

                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<tran_purchase_requisition_dtl_DTO>> GetItemDetailsByTechpackforFabric(long p_techpack_id, long gen_item_structure_group_sub_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_item_detail_by_techpack_id_for_fpr( @p_techpack_id,@p_gen_item_structure_group_sub_id)";

                    var dataList = connection.Query<tran_purchase_requisition_dtl_DTO>(query,
                          new
                          {

                              p_techpack_id = p_techpack_id,
                              p_gen_item_structure_group_sub_id = gen_item_structure_group_sub_id
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<tran_purchase_requisition_DTO> GetMerchandiserByTechpack(long p_techpack_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM get_merchandiser_info( @techpack_id)";

                    var dataList = connection.Query<tran_purchase_requisition_DTO>(query,
                          new
                          {

                              techpack_id = p_techpack_id,

                          }
                         ).AsList().FirstOrDefault(); ;

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }

}

