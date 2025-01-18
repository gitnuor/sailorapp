using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranScmPoService : ITranScmPoService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<AccessoriesPoService> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenItemMasterService _GenItemMasterService;
        private readonly IGenMeasurementUnitDetailService _GenMeasurementUnitDetailService;
        private readonly IGenUnitService _GenUnitService;
        private readonly IRPCDbService _RPCDbService;
        private readonly ITranPurchaseRequisitionService _TranPurchaseRequisitionService;
        private readonly ICommonService _CommonService;

        public TranScmPoService(ITranPurchaseRequisitionService TranPurchaseRequisitionService, IRPCDbService RPCDbService, ICommonService CommonService,
            IGenUnitService GenUnitService, IGenMeasurementUnitDetailService GenMeasurementUnitDetailService, IGenItemMasterService GenItemMasterService, IGenSupplierInformationService GenSupplierInformationService, IConfiguration configuration, ILogger<AccessoriesPoService> logger, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _GenItemMasterService = GenItemMasterService;
            _GenUnitService = GenUnitService;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;

            _GenMeasurementUnitDetailService = GenMeasurementUnitDetailService;
            _GenSupplierInformationService = GenSupplierInformationService;
            _TranPurchaseRequisitionService = TranPurchaseRequisitionService;
            _RPCDbService = RPCDbService;
            _CommonService = CommonService;
        }

        public async Task<bool> SaveAsync(tran_scm_po_entity entity, List<tran_scm_po_details_entity> details, List<file_upload> files)
        {
            try
            {
              
                List<file_upload> doc_files =await _CommonService.UploadFiles(files, "purchase_order");

                entity.documents = JArray.Parse(JsonConvert.SerializeObject(doc_files.ToList())).ToString();

                entity.po_details = JArray.Parse(JsonConvert.SerializeObject(details)).ToString();

                return await tran_scm_po_insert_sp(entity);


            }
            catch (Exception ex)
            {

                return false;
            }
        }

      
        public async Task<bool> UpdateAsync(tran_scm_po_DTO entity)
        {
            try
            {

                var response = await GetSingleAsync(entity.po_id.Value);

                var ret = JsonConvert.DeserializeObject<tran_scm_po_DTO>(JsonConvert.SerializeObject(response));

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


                ret.terms_conditions = JArray.Parse(groupedJson).ToString();


                List<file_upload> file_upload = entity.List_Files;

                List<string> DeleteFiles = entity.DeleteList;

                List<file_upload> files = new List<file_upload>();

                if (DeleteFiles.Count > 0)
                {

                    if (!string.IsNullOrEmpty(ret.documents) != null)
                    {
                        var file_list = JsonConvert.DeserializeObject<List<file_upload>>(ret.documents);

                        await _CommonService.Delete_Files(DeleteFiles, file_upload, "purchase_order");

                    }

                }

                await _CommonService.UploadFiles(file_upload, "purchase_order");

                

                

                ret.documents = JArray.Parse(JsonConvert.SerializeObject(file_upload.ToList())).ToString();
                ret.po_details= JArray.Parse(JsonConvert.SerializeObject(entity.List_po_details.ToList())).ToString();
                ret.updated_by = entity.added_by;
                ret.date_updated = DateTime.Now;
                ret.po_date = entity.po_date;
                ret.delivery_start_date = entity.delivery_start_date;
                ret.delivery_end_date = entity.delivery_end_date;
                ret.supplier_id = entity.supplier_id;
                ret.is_submitted = entity.is_submitted;
                ret.vat_amount = entity.vat_amount;
                ret.discount_amount = entity.discount_amount;
                ret.final_amount = entity.final_amount;
                ret.supplier_concern_person = entity.supplier_concern_person;


                var model = JsonConvert.DeserializeObject<tran_scm_po_entity>(JsonConvert.SerializeObject(ret));

                await tran_scm_po_update_sp(model);

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> SendForApproval(tran_scm_po_DTO entity)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_scm_po
                                          SET  is_submitted = 2
                                          WHERE po_id = @po_id";

                    await connection.ExecuteAsync(query, new
                    {
                        po_id = entity.po_id

                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        public async Task<bool> ApprovedAsync(tran_scm_po_DTO entity)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_scm_po
                                          SET  is_approved = 1
                                          WHERE po_id = @po_id";

                    await connection.ExecuteAsync(query, new
                    {
                        po_id = entity.po_id

                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }


        public async Task<bool> UpdateApproval(long po_id, string status_remarks)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_scm_po
                                          SET is_approved=1,
                                              is_submitted = 2,
                                              status_remarks=@status_remarks
                                          WHERE po_id = @po_id";


                    await connection.ExecuteAsync(query, new
                    {
                        po_id = po_id,
                        status_remarks = status_remarks

                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        public async Task<bool> UpdateRejectApproval(long po_id, string status_remarks)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_scm_po
                                          SET is_approved=2,
                                              is_submitted = null,
                                              status_remarks=@status_remarks
                                          WHERE po_id = @po_id";


                    await connection.ExecuteAsync(query, new
                    {
                        po_id = po_id,
                        status_remarks = status_remarks

                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }



        public async Task<tran_scm_po_DTO> GetPurchaseOrder(long po_id)
        {

            try
            {

                var resp = await _RPCDbService.GetAllsp_get_data_for_mcd_receiveAsync(po_id);

                tran_scm_po_DTO ret = JsonConvert.DeserializeObject<tran_scm_po_DTO>(JsonConvert.SerializeObject(resp));

                ret.List_po_details = JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(resp.list_po_details);

                ret.delivery_unit_name = resp.unit_name;
                ret.supplier_name = resp.name;
                ret.deliveryAddress = resp.unit_address;
               
                return ret;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_scm_po_entity>> GetAllAsync()
        {
            List<tran_scm_po_entity> list = new List<tran_scm_po_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_scm_po_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_scm_po_entity>> GetAllPagedDataAsync(DtParameters param)
        {

            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_scm_po m
                                           where 
                                                     m.fiscal_year_id=@fiscal_year_id and m.event_id=@event_id
                                                     and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.po_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_scm_po_entity>(query,
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

        public async Task<tran_scm_po_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                List<file_upload> objpdf_list = new List<file_upload>();

                tran_scm_po_DTO po = new tran_scm_po_DTO();

                var ret_rpc = await _RPCDbService.GetAllproc_sp_get_data_tran_scm_poAsync(Id);

                po = JsonConvert.DeserializeObject<tran_scm_po_DTO>(JsonConvert.SerializeObject(ret_rpc));

                po.List_po_details = JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(ret_rpc.po_details);


                //po.terms_and_conditions_list = JsonConvert.DeserializeObject<List<TermConditionGrouped>>(ret_rpc.terms_conditions);
                po.terms_and_conditions_list = !string.IsNullOrEmpty(ret_rpc.terms_conditions)? JsonConvert.DeserializeObject<List<TermConditionGrouped>>(ret_rpc.terms_conditions): new List<TermConditionGrouped>();

               // po.detChallan= JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(ret_rpc.po_details);


                if (!string.IsNullOrEmpty( po.documents))
                {
                    var file_list = JsonConvert.DeserializeObject<List<file_upload>>(po.documents);

                    objpdf_list=await _CommonService.LoadAllFiles( file_list,"purchase_order");
                }

                po.List_Files = objpdf_list;


                return po;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_tran_scm_po_detail_DTO> Get_data_tran_scm_po_Async(Int64 po_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_mcd_receive( @po_id_filter)";

                    var dataList = connection.Query<rpc_tran_scm_po_detail_DTO>(query,
                          new
                          {

                              po_id_filter = po_id
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

        public async Task<bool> DeleteAsync(Int64? Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new tran_scm_po_entity { po_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_scm_po_insert_sp(tran_scm_po_entity objtran_scm_po)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_scm_po_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_po_no", NpgsqlDbType.Text, objtran_scm_po.po_no == null ? DBNull.Value : objtran_scm_po.po_no);
                        Command.Parameters.AddWithValue("in_po_date", NpgsqlDbType.Date, objtran_scm_po.po_date == null ? DBNull.Value : objtran_scm_po.po_date);
                        Command.Parameters.AddWithValue("in_delivery_start_date", NpgsqlDbType.Date, objtran_scm_po.delivery_start_date == null ? DBNull.Value : objtran_scm_po.delivery_start_date);
                        Command.Parameters.AddWithValue("in_delivery_end_date", NpgsqlDbType.Date, objtran_scm_po.delivery_end_date == null ? DBNull.Value : objtran_scm_po.delivery_end_date);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_scm_po.supplier_id == null ? DBNull.Value : objtran_scm_po.supplier_id);
                        Command.Parameters.AddWithValue("in_delivery_unit", NpgsqlDbType.Bigint, objtran_scm_po.delivery_unit == null ? DBNull.Value : objtran_scm_po.delivery_unit);
                        Command.Parameters.AddWithValue("in_delivery_address", NpgsqlDbType.Bigint, objtran_scm_po.delivery_address == null ? DBNull.Value : objtran_scm_po.delivery_address);
                        Command.Parameters.AddWithValue("in_currency_id", NpgsqlDbType.Bigint, objtran_scm_po.currency_id == null ? DBNull.Value : objtran_scm_po.currency_id);
                        Command.Parameters.AddWithValue("in_documents", NpgsqlDbType.Text, objtran_scm_po.documents == null ? DBNull.Value : objtran_scm_po.documents);
                        Command.Parameters.AddWithValue("in_terms_conditions", NpgsqlDbType.Text, objtran_scm_po.terms_conditions == null ? DBNull.Value : objtran_scm_po.terms_conditions);
                        Command.Parameters.AddWithValue("in_status", NpgsqlDbType.Bigint, objtran_scm_po.status == null ? DBNull.Value : objtran_scm_po.status);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_scm_po.added_by == null ? DBNull.Value : objtran_scm_po.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_scm_po.updated_by == null ? DBNull.Value : objtran_scm_po.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_scm_po.date_added == null ? DBNull.Value : objtran_scm_po.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_scm_po.date_updated == null ? DBNull.Value : objtran_scm_po.date_updated);
                        Command.Parameters.AddWithValue("in_pr_id", NpgsqlDbType.Bigint, objtran_scm_po.pr_id == null ? DBNull.Value : objtran_scm_po.pr_id);
                        Command.Parameters.AddWithValue("in_item_structure_group_id", NpgsqlDbType.Bigint, objtran_scm_po.item_structure_group_id == null ? DBNull.Value : objtran_scm_po.item_structure_group_id);
                        Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Date, objtran_scm_po.approved_date == null ? DBNull.Value : objtran_scm_po.approved_date);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_scm_po.approved_by == null ? DBNull.Value : objtran_scm_po.approved_by);
                        //Command.Parameters.AddWithValue("in_is_bill_submitted", NpgsqlDbType.Boolean, objtran_scm_po.is_bill_submitted == null ? DBNull.Value : objtran_scm_po.is_bill_submitted);
                        Command.Parameters.AddWithValue("in_po_details", NpgsqlDbType.Text, objtran_scm_po.po_details == null ? DBNull.Value : objtran_scm_po.po_details);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_scm_po.event_id == null ? DBNull.Value : objtran_scm_po.event_id);
                         Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_scm_po.fiscal_year_id == null ? DBNull.Value : objtran_scm_po.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_scm_po.is_submitted == null ? DBNull.Value : objtran_scm_po.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_scm_po.is_approved == null ? DBNull.Value : objtran_scm_po.is_approved);


                        Command.Parameters.AddWithValue("in_vat_amount", NpgsqlDbType.Numeric, objtran_scm_po.vat_amount == null ? DBNull.Value : objtran_scm_po.vat_amount);
                        Command.Parameters.AddWithValue("in_discount_amount", NpgsqlDbType.Numeric, objtran_scm_po.discount_amount == null ? DBNull.Value : objtran_scm_po.discount_amount);
                        Command.Parameters.AddWithValue("in_final_amount", NpgsqlDbType.Bigint, objtran_scm_po.final_amount == null ? DBNull.Value : objtran_scm_po.final_amount);

                        Command.Parameters.AddWithValue("in_supplier_concern_person", NpgsqlDbType.Text, objtran_scm_po.supplier_concern_person == null ? DBNull.Value : objtran_scm_po.supplier_concern_person);

                        Command.Parameters.AddWithValue("in_supplier_address", NpgsqlDbType.Text, objtran_scm_po.supplier_address == null ? DBNull.Value : objtran_scm_po.supplier_address);

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
        public async Task<bool> tran_scm_po_update_sp(tran_scm_po_entity objtran_scm_po)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
				connection.Open();

				using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_scm_po_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, objtran_scm_po.po_id == null ? DBNull.Value : objtran_scm_po.po_id);
                        Command.Parameters.AddWithValue("in_po_no", NpgsqlDbType.Text, objtran_scm_po.po_no == null ? DBNull.Value : objtran_scm_po.po_no);
                        Command.Parameters.AddWithValue("in_po_date", NpgsqlDbType.Date, objtran_scm_po.po_date == null ? DBNull.Value : objtran_scm_po.po_date);
                        Command.Parameters.AddWithValue("in_delivery_start_date", NpgsqlDbType.Date, objtran_scm_po.delivery_start_date == null ? DBNull.Value : objtran_scm_po.delivery_start_date);
                        Command.Parameters.AddWithValue("in_delivery_end_date", NpgsqlDbType.Date, objtran_scm_po.delivery_end_date == null ? DBNull.Value : objtran_scm_po.delivery_end_date);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_scm_po.supplier_id == null ? DBNull.Value : objtran_scm_po.supplier_id);
                        Command.Parameters.AddWithValue("in_delivery_unit", NpgsqlDbType.Bigint, objtran_scm_po.delivery_unit == null ? DBNull.Value : objtran_scm_po.delivery_unit);
                        Command.Parameters.AddWithValue("in_delivery_address", NpgsqlDbType.Bigint, objtran_scm_po.delivery_address == null ? DBNull.Value : objtran_scm_po.delivery_address);
                        Command.Parameters.AddWithValue("in_currency_id", NpgsqlDbType.Bigint, objtran_scm_po.currency_id == null ? DBNull.Value : objtran_scm_po.currency_id);
                        Command.Parameters.AddWithValue("in_documents", NpgsqlDbType.Text, objtran_scm_po.documents == null ? DBNull.Value : objtran_scm_po.documents);
                        Command.Parameters.AddWithValue("in_terms_conditions", NpgsqlDbType.Text, objtran_scm_po.terms_conditions == null ? DBNull.Value : objtran_scm_po.terms_conditions);
                        Command.Parameters.AddWithValue("in_status", NpgsqlDbType.Bigint, objtran_scm_po.status == null ? DBNull.Value : objtran_scm_po.status);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_scm_po.added_by == null ? DBNull.Value : objtran_scm_po.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_scm_po.updated_by == null ? DBNull.Value : objtran_scm_po.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_scm_po.date_added == null ? DBNull.Value : objtran_scm_po.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_scm_po.date_updated == null ? DBNull.Value : objtran_scm_po.date_updated);
                        Command.Parameters.AddWithValue("in_pr_id", NpgsqlDbType.Bigint, objtran_scm_po.pr_id == null ? DBNull.Value : objtran_scm_po.pr_id);
                        Command.Parameters.AddWithValue("in_item_structure_group_id", NpgsqlDbType.Bigint, objtran_scm_po.item_structure_group_id == null ? DBNull.Value : objtran_scm_po.item_structure_group_id);
                        Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Date, objtran_scm_po.approved_date == null ? DBNull.Value : objtran_scm_po.approved_date);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_scm_po.approved_by == null ? DBNull.Value : objtran_scm_po.approved_by);
                        // Command.Parameters.AddWithValue("in_is_bill_submitted", NpgsqlDbType.Boolean, objtran_scm_po.is_bill_submitted == null ? DBNull.Value : objtran_scm_po.is_bill_submitted);
                        Command.Parameters.AddWithValue("in_po_details", NpgsqlDbType.Text, objtran_scm_po.po_details == null ? DBNull.Value : objtran_scm_po.po_details);
                       // Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_scm_po.event_id == null ? DBNull.Value : objtran_scm_po.event_id);
                       // Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_scm_po.fiscal_year_id == null ? DBNull.Value : objtran_scm_po.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_scm_po.is_submitted == null ? DBNull.Value : objtran_scm_po.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_scm_po.is_approved == null ? DBNull.Value : objtran_scm_po.is_approved);



                        Command.Parameters.AddWithValue("in_vat_amount", NpgsqlDbType.Numeric, objtran_scm_po.vat_amount == null ? DBNull.Value : objtran_scm_po.vat_amount);
                        Command.Parameters.AddWithValue("in_discount_amount", NpgsqlDbType.Numeric, objtran_scm_po.discount_amount == null ? DBNull.Value : objtran_scm_po.discount_amount);
                        Command.Parameters.AddWithValue("in_final_amount", NpgsqlDbType.Bigint, objtran_scm_po.final_amount == null ? DBNull.Value : objtran_scm_po.final_amount);

                        Command.Parameters.AddWithValue("in_supplier_concern_person", NpgsqlDbType.Text, objtran_scm_po.supplier_concern_person == null ? DBNull.Value : objtran_scm_po.supplier_concern_person);

                        Command.Parameters.AddWithValue("in_supplier_address", NpgsqlDbType.Text, objtran_scm_po.supplier_address == null ? DBNull.Value : objtran_scm_po.supplier_address);


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
        public async Task<List<rpc_tran_scm_po_DTO>> GetAllJoined_TranScmPoAsync(Int64 row_index, Int64 page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po( @row_index,@page_size)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
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
        public async Task<List<tran_scm_po_DTO>> GetAllPOApprovaData_Fro_Bill_Submission_Async(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                               FROM tran_scm_po m
                               left join tran_scm_bill_submission tb on tb.po_id=m.po_id
                               where
                                        m.is_approved=1  and m.is_submitted=2
                                        and case
                                         when @search_text is null or length(@search_text)=0 then true
                                         when @search_text is not null and length(@search_text)>0 and (
                                                m.po_no ilike '%' || @search_text || '%'
                                             ) then true
                                         else false end)


                                SELECT cte_saved.*,
                                       (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                FROM cte_saved
                                OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_scm_po_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(JsonConvert.SerializeObject(dataList));

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


       





        public async Task<List<rpc_tran_scm_po_DTO>> GetAllAccessoriesPoAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, Int64 listType, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_acc( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@list_type,@search_text)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              list_type = listType,
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


        public async Task<List<rpc_tran_scm_po_DTO>> GetAllFabricsPoAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id,
          Int64 listType, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_fab( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@list_type,@search_text)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
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
                throw (ex);
            }
            

        }

        public async Task<List<rpc_tran_scm_po_DTO>> GetAllOpenPoAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_open( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
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

        public async Task<List<rpc_tran_scm_po_DTO>> GetSubmittedPOData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_submitted_open( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
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
                throw (ex);
            }
           

        }

        public async Task<List<rpc_tran_scm_po_DTO>> GetOpenPOApprovedData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_open_po_approved( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
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
                throw (ex);
            }

        }



        public async Task<List<rpc_tran_scm_po_DTO>> GetOpenPORejectData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_open_po_reject( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
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
                throw (ex);
            }

        }





        public async Task<List<rpc_tran_scm_po_DTO>> GetAllOpenPoPendingApprovalAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_open_po_pending_approval( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
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
                throw (ex);
            }
        }




        public async Task<List<rpc_tran_scm_po_DTO>> GetAllPoRejectAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, DtSearch search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_reject( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id , @search_text)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              search_text = search.Value
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







        public async Task<List<rpc_tran_scm_po_DTO>> GetAllPoApprovalAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id , DtSearch search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_approval( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id , @search_text)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              search_text= search.Value
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

        public async Task<List<rpc_tran_scm_po_DTO>> GetAllPoPendingAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_pending_approval( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
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
                throw (ex);
            }

        }

        public async Task<bool> SaveRevisePoAsync(tran_mcd_receive_DTO obj_revised_po)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_scm_po_revise_detail_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, obj_revised_po.po_id == null ? DBNull.Value : obj_revised_po.po_id);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, obj_revised_po.is_submitted == null ? DBNull.Value : obj_revised_po.is_submitted);
                        Command.Parameters.AddWithValue("in_po_details", NpgsqlDbType.Text, obj_revised_po.tran_revise_po_details.ToString() == null ? DBNull.Value : obj_revised_po.tran_revise_po_details.ToString());


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


        public async Task<List<concern_person>> GetConcernPersonsAsync(long supplierId)
        {


            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM get_concern_person_details( @supplier_id)";

                    var dataList = connection.Query<concern_person>(query,
                          new
                          {
                              supplier_id = supplierId
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

        public async Task<List<rpc_tran_scm_po_DTO>> GetAllFabricsPoReviseAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id,
         Int64 listType)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_revise( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@list_type)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              list_type = listType
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

        public async Task<tran_scm_po_DTO> GetSingleReviseAsync(Int64 Id)
        {
            try
            {
                List<file_upload> objpdf_list = new List<file_upload>();

                tran_scm_po_DTO po = new tran_scm_po_DTO();

                var ret_rpc = await _RPCDbService.GetAllproc_sp_get_data_tran_scm_revise_poAsync(Id);

                po = JsonConvert.DeserializeObject<tran_scm_po_DTO>(JsonConvert.SerializeObject(ret_rpc));

                po.List_po_details = !string.IsNullOrEmpty(ret_rpc.po_details) ? JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(ret_rpc.po_details) : new List<tran_scm_po_details_DTO>();


                //po.terms_and_conditions_list = JsonConvert.DeserializeObject<List<TermConditionGrouped>>(ret_rpc.terms_conditions);
                po.terms_and_conditions_list = !string.IsNullOrEmpty(ret_rpc.terms_conditions) ? JsonConvert.DeserializeObject<List<TermConditionGrouped>>(ret_rpc.terms_conditions) : new List<TermConditionGrouped>();




                if (!string.IsNullOrEmpty(po.documents))
                {
                    var file_list = JsonConvert.DeserializeObject<List<file_upload>>(po.documents);

                    objpdf_list = await _CommonService.LoadAllFiles(file_list, "purchase_order");
                }

                po.List_Files = objpdf_list;


                return po;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_proc_sp_get_data_tran_scm_po_DTO>> GetBillChallanDetailAsync(long poId)
        {


            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT mcd.del_chalan_no,mcd.del_chalan_date,received_id FROM tran_mcd_receive mcd INNER JOIN tran_scm_po tsp ON tsp.po_id=mcd.po_id where tsp.po_id=@poId";

                    var dataList = connection.Query<rpc_proc_sp_get_data_tran_scm_po_DTO>(query,
                          new
                          {
                              poId = poId
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


        public async Task<List<tran_scm_po_details_DTO>> GetBillChallanItemDetailAsync(Int64 p_po_id, Int64 p_received_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_bill_submission_item_detail( @p_po_id,@p_received_id)";

                    var dataList = connection.Query<tran_scm_po_details_DTO>(query,
                          new
                          {
                              p_po_id = p_po_id,
                              p_received_id = p_received_id

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


    }


}

