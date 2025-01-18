
using AutoMapper;
using Azure;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.RPC;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using Postgrest;

using System.Data;
using Web.Core.Frame.Helpers;
using static Postgrest.Constants;
using static Postgrest.QueryOptions;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranMcdReceiveService : ITranMcdReceiveService
    {


        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRPCDbService _rpc_db_service;
        private readonly ILogger<TranMcdReceiveService> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICommonService _CommonService;




        public TranMcdReceiveService(IHostingEnvironment hostingEnvironment, ILogger<TranMcdReceiveService> logger, IRPCDbService rpc_db_service, IConfiguration configuration, IMapper mapper, ICommonService CommonService)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _rpc_db_service = rpc_db_service;
            _CommonService = CommonService;
        }


        public async Task<bool> SaveAsync(tran_mcd_receive_entity entity, List<tran_mcd_receive_detail_entity> details, List<file_upload> files)
        {
            try
            {

                long receive_id = 0;
                long gen_item_master_id = 0;
                string floorname = string.Empty;
                long floor_id = 0;

                #region
                //List<file_upload> doc_files = new List<file_upload>();
                //foreach (var singleimage in files)
                //{
                //    file_upload file = new file_upload();
                //    file.filetype = singleimage.extension;
                //    file.server_filename = System.Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);
                //    file.filename = singleimage.filename;
                //    file.extension = Path.GetExtension(singleimage.filename);
                //    byte[] byteArray = Convert.FromBase64String(singleimage.base64string);


                //    try
                //    {

                //        string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                //        file.filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/purchase_order/" + file.server_filename;

                //        if (!Directory.Exists(file.filePath))
                //        {
                //            DirectoryInfo di = Directory.CreateDirectory(file.filePath);
                //        }

                //        File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath), byteArray);
                //        doc_files.Add(file);
                //    }
                //    catch (Exception ex)
                //    {

                //    }


                //}
                #endregion

                List<file_upload> uploadedfiles = await _CommonService.UploadFiles(files, "mcd");

                entity.documents = JArray.Parse(JsonConvert.SerializeObject(uploadedfiles.ToList())).ToString();



                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var Command = new NpgsqlCommand("tran_mcd_receive_insert", connection);

                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.AddWithValue("in_receive_no", NpgsqlDbType.Text, entity.receive_no == null ? DBNull.Value : entity.receive_no);
                            Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, entity.po_id == null ? DBNull.Value : entity.po_id);
                            Command.Parameters.AddWithValue("in_po_date", NpgsqlDbType.Date, entity.po_date == null ? DBNull.Value : entity.po_date);
                            Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, entity.supplier_id == null ? DBNull.Value : entity.supplier_id);
                            Command.Parameters.AddWithValue("in_delivery_unit", NpgsqlDbType.Bigint, entity.delivery_unit == null ? DBNull.Value : entity.delivery_unit);
                            Command.Parameters.AddWithValue("in_delivery_address", NpgsqlDbType.Bigint, entity.delivery_address == null ? DBNull.Value : entity.delivery_address);
                            Command.Parameters.AddWithValue("in_arrival_date", NpgsqlDbType.Date, entity.arrival_date == null ? DBNull.Value : entity.arrival_date);
                            Command.Parameters.AddWithValue("in_del_chalan_no", NpgsqlDbType.Text, entity.del_chalan_no == null ? DBNull.Value : entity.del_chalan_no);
                            Command.Parameters.AddWithValue("in_del_chalan_date", NpgsqlDbType.Date, entity.del_chalan_date == null ? DBNull.Value : entity.del_chalan_date);
                            Command.Parameters.AddWithValue("in_tran_mode", NpgsqlDbType.Text, entity.tran_mode == null ? DBNull.Value : entity.tran_mode);
                            Command.Parameters.AddWithValue("in_transport_type", NpgsqlDbType.Text, entity.transport_type == null ? DBNull.Value : entity.transport_type);
                            Command.Parameters.AddWithValue("in_tollerence", NpgsqlDbType.Bigint, entity.tollerence == null ? DBNull.Value : entity.tollerence);
                            Command.Parameters.AddWithValue("in_vehical_no", NpgsqlDbType.Text, entity.vehical_no == null ? DBNull.Value : entity.vehical_no);
                            Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, entity.driver_name == null ? DBNull.Value : entity.driver_name);
                            Command.Parameters.AddWithValue("in_delevary_status", NpgsqlDbType.Text, entity.delevary_status == null ? DBNull.Value : entity.delevary_status);
                            Command.Parameters.AddWithValue("in_lc_no", NpgsqlDbType.Text, entity.lc_no == null ? DBNull.Value : entity.lc_no);
                            Command.Parameters.AddWithValue("in_lc_date", NpgsqlDbType.Date, entity.lc_date == null ? DBNull.Value : entity.lc_date);
                            Command.Parameters.AddWithValue("in_invoice_no", NpgsqlDbType.Text, entity.invoice_no == null ? DBNull.Value : entity.invoice_no);
                            Command.Parameters.AddWithValue("in_invoice_date", NpgsqlDbType.Date, entity.invoice_date == null ? DBNull.Value : entity.invoice_date);
                            Command.Parameters.AddWithValue("in_documents", NpgsqlDbType.Text, entity.documents == null ? DBNull.Value : entity.documents);
                            Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, entity.added_by == null ? DBNull.Value : entity.added_by);
                            Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, entity.date_added == null ? DBNull.Value : entity.date_added);
                            Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, entity.updated_by == null ? DBNull.Value : entity.updated_by);
                            Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, entity.date_updated == null ? DBNull.Value : entity.date_updated);
                            Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, entity.approved_by == null ? DBNull.Value : entity.approved_by);
                            Command.Parameters.AddWithValue("in_date_approved", NpgsqlDbType.Date, entity.date_approved == null ? DBNull.Value : entity.date_approved);
                            Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, entity.gen_item_structure_group_id == null ? DBNull.Value : entity.gen_item_structure_group_id);
                            Command.Parameters.AddWithValue("in_po_status", NpgsqlDbType.Text, entity.po_status == null ? DBNull.Value : entity.po_status);
                            Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, entity.fiscal_year_id == null ? DBNull.Value : entity.fiscal_year_id);
                            Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, entity.event_id == null ? DBNull.Value : entity.event_id);
                            Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, entity.is_submitted == null ? DBNull.Value : entity.is_submitted);
                            Command.Parameters.AddWithValue("in_is_acknowledged", NpgsqlDbType.Bigint, entity.is_acknowledged == null ? DBNull.Value : entity.is_acknowledged);

                            Command.Parameters.AddWithValue("in_tran_mcd_receive_json", NpgsqlDbType.Text, JArray.Parse(JsonConvert.SerializeObject(details)).ToString() == null ? DBNull.Value : JArray.Parse(JsonConvert.SerializeObject(details)).ToString());


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
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> UpdateAsync(tran_mcd_receive_entity entity, List<tran_mcd_receive_detail_entity> details, List<file_upload> files)
        {
            try
            {
                #region
                //List<file_upload> doc_files = new List<file_upload>();
                //foreach (var singleimage in files)
                //{
                //    file_upload file = new file_upload();
                //    file.filetype = singleimage.extension;
                //    file.server_filename = System.Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);
                //    file.filename = singleimage.filename;
                //    file.extension = Path.GetExtension(singleimage.filename);
                //    byte[] byteArray = Convert.FromBase64String(singleimage.base64string);


                //    try
                //    {

                //        string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                //        file.filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/purchase_order/" + file.server_filename;

                //        if (!Directory.Exists(file.filePath))
                //        {
                //            DirectoryInfo di = Directory.CreateDirectory(file.filePath);
                //        }

                //        File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath), byteArray);
                //        doc_files.Add(file);
                //    }
                //    catch (Exception ex)
                //    {

                //    }


                //}
                #endregion

                List<file_upload> uploadedfiles = await _CommonService.UploadFiles(files, "mcd");

                entity.documents = JArray.Parse(JsonConvert.SerializeObject(uploadedfiles.ToList())).ToString();

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var Command = new NpgsqlCommand("tran_mcd_receive_update", connection);

                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.AddWithValue("in_received_id", NpgsqlDbType.Bigint, entity.received_id == null ? DBNull.Value : entity.received_id);
                            Command.Parameters.AddWithValue("in_receive_no", NpgsqlDbType.Text, entity.receive_no == null ? DBNull.Value : entity.receive_no);
                            Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, entity.po_id == null ? DBNull.Value : entity.po_id);
                            Command.Parameters.AddWithValue("in_po_date", NpgsqlDbType.Date, entity.po_date == null ? DBNull.Value : entity.po_date);
                            Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, entity.supplier_id == null ? DBNull.Value : entity.supplier_id);
                            Command.Parameters.AddWithValue("in_delivery_unit", NpgsqlDbType.Bigint, entity.delivery_unit == null ? DBNull.Value : entity.delivery_unit);
                            Command.Parameters.AddWithValue("in_delivery_address", NpgsqlDbType.Bigint, entity.delivery_address == null ? DBNull.Value : entity.delivery_address);
                            Command.Parameters.AddWithValue("in_arrival_date", NpgsqlDbType.Date, entity.arrival_date == null ? DBNull.Value : entity.arrival_date);
                            Command.Parameters.AddWithValue("in_del_chalan_no", NpgsqlDbType.Text, entity.del_chalan_no == null ? DBNull.Value : entity.del_chalan_no);
                            Command.Parameters.AddWithValue("in_del_chalan_date", NpgsqlDbType.Date, entity.del_chalan_date == null ? DBNull.Value : entity.del_chalan_date);
                            Command.Parameters.AddWithValue("in_tran_mode", NpgsqlDbType.Text, entity.tran_mode == null ? DBNull.Value : entity.tran_mode);
                            Command.Parameters.AddWithValue("in_transport_type", NpgsqlDbType.Text, entity.transport_type == null ? DBNull.Value : entity.transport_type);
                            Command.Parameters.AddWithValue("in_tollerence", NpgsqlDbType.Bigint, entity.tollerence == null ? DBNull.Value : entity.tollerence);
                            Command.Parameters.AddWithValue("in_vehical_no", NpgsqlDbType.Text, entity.vehical_no == null ? DBNull.Value : entity.vehical_no);
                            Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, entity.driver_name == null ? DBNull.Value : entity.driver_name);
                            Command.Parameters.AddWithValue("in_delevary_status", NpgsqlDbType.Text, entity.delevary_status == null ? DBNull.Value : entity.delevary_status);
                            Command.Parameters.AddWithValue("in_lc_no", NpgsqlDbType.Text, entity.lc_no == null ? DBNull.Value : entity.lc_no);
                            Command.Parameters.AddWithValue("in_lc_date", NpgsqlDbType.Date, entity.lc_date == null ? DBNull.Value : entity.lc_date);
                            Command.Parameters.AddWithValue("in_invoice_no", NpgsqlDbType.Text, entity.invoice_no == null ? DBNull.Value : entity.invoice_no);
                            Command.Parameters.AddWithValue("in_invoice_date", NpgsqlDbType.Date, entity.invoice_date == null ? DBNull.Value : entity.invoice_date);
                            Command.Parameters.AddWithValue("in_documents", NpgsqlDbType.Text, entity.documents == null ? DBNull.Value : entity.documents);
                            Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, entity.added_by == null ? DBNull.Value : entity.added_by);
                            Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, entity.date_added == null ? DBNull.Value : entity.date_added);
                            Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, entity.updated_by == null ? DBNull.Value : entity.updated_by);
                            Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, entity.date_updated == null ? DBNull.Value : entity.date_updated);
                            Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, entity.approved_by == null ? DBNull.Value : entity.approved_by);
                            Command.Parameters.AddWithValue("in_date_approved", NpgsqlDbType.Date, entity.date_approved == null ? DBNull.Value : entity.date_approved);
                            Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, entity.gen_item_structure_group_id == null ? DBNull.Value : entity.gen_item_structure_group_id);
                            Command.Parameters.AddWithValue("in_po_status", NpgsqlDbType.Text, entity.po_status == null ? DBNull.Value : entity.po_status);
                            Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, entity.fiscal_year_id == null ? DBNull.Value : entity.fiscal_year_id);
                            Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, entity.event_id == null ? DBNull.Value : entity.event_id);
                            Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, entity.is_submitted == null ? DBNull.Value : entity.is_submitted);
                            Command.Parameters.AddWithValue("in_is_acknowledged", NpgsqlDbType.Bigint, entity.is_acknowledged == null ? DBNull.Value : entity.is_acknowledged);
                            Command.Parameters.AddWithValue("in_tran_mcd_receive_json", NpgsqlDbType.Text, JArray.Parse(JsonConvert.SerializeObject(details)).ToString() == null ? DBNull.Value : JArray.Parse(JsonConvert.SerializeObject(details)).ToString());


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
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_mcd_receive_DTO>> GetAllAsync()
        {
            try
            {
                List<tran_mcd_receive_entity> list = new List<tran_mcd_receive_entity>();

                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        var dataList = connection.GetAll<tran_mcd_receive_DTO>().ToList();

                        return dataList;//JsonConvert.DeserializeObject<List<tran_mcd_receive_DTO>>(JsonConvert.SerializeObject(dataList));
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

        //public async Task<List<tran_scm_po_entity_test>> GetAllPagedDataAsync(DtParameters param)
        //{
        //    try
        //    {
        //        using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
        //        {
        //            connection.Open();

        //            string query = @"WITH cte_saved AS (SELECT m.*
        //                                   FROM tran_scm_po_test m
        //                                   where case
        //                                             when @search_text is null then true
        //                                             when @search_text is not null and (
        //                                                    m.po_no ilike '%' || @search_text || '%'
        //                                                 ) then true
        //                                             else false end)


        //                                    SELECT cte_saved.*,
        //                                           (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
        //                                    FROM cte_saved
        //                                    OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

        //            var dataList = connection.Query<tran_scm_po_entity_test>(query,
        //                new
        //                {
        //                    search_text = param.Search.Value,
        //                    row_index = param.Start,
        //                    page_size = param.Length
        //                }).ToList();

        //            return dataList;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //}



        public async Task<tran_mcd_receive_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                List<file_upload> objpdf_list = new List<file_upload>();

                tran_mcd_receive_entity model = new tran_mcd_receive_entity();

                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = @"SELECT m.*,gsi.name,gu.unit_name,gsi.contact_person,gsi.factory_address,tcp.po_no,
            (select jsonb_agg(
                jsonb_build_object(
        'receive_detail_id', sipsc.receive_detail_id,
        'received_id', sipsc.received_id,
        'gen_item_master_id', sipsc.gen_item_master_id,
        'po_quantity', sipsc.po_quantity,
        'measurement_unit_detail_id', sipsc.measurement_unit_detail_id,
        'measurement_unit', sipsc.measurement_unit,
        'receive_quantity', sipsc.receive_quantity,
        'receive_unit', sipsc.receive_unit,
        'chalan_quantity', sipsc.chalan_quantity,
        'exceess_quantity', sipsc.exceess_quantity,
        'shortage_quantity', sipsc.shortage_quantity,
        'random_sample_quantity', sipsc.random_sample_quantity,
        'aql', sipsc.aql,
        'pass_quantity', sipsc.pass_quantity,
        'fail_quantity', sipsc.fail_quantity,
        'defect_percentage', sipsc.defect_percentage,
        'remarks', sipsc.remarks,
        'po_id', sipsc.po_id,
        'mcd_no', sipsc.mcd_no,

        'gen_warehouse_floor_rack_info', sipsc.gen_warehouse_floor_rack_info,
        'gen_warehouse_floor_rack_id', sipsc.gen_warehouse_floor_rack_id,
        'tran_type', sipsc.tran_type,
        'item_name',gim.item_name,
        'item_spec',gim.item_spec



                                                           )
                                                   )
                  from tran_mcd_receive_detail sipsc
                  INNER JOIN gen_item_master gim ON
                  gim.gen_item_master_id = sipsc.gen_item_master_id
                  where sipsc.received_id = m.received_id) as tran_mcd_receive_json

                FROM tran_mcd_receive m
                INNER JOIN gen_supplier_information gsi ON
		        gsi.gen_supplier_information_id = m.supplier_id
			    INNER JOIN gen_unit gu ON
		        gu.gen_unit_id = m.delivery_unit
                INNER JOIN tran_scm_po tcp
                ON tcp.po_id=m.po_id
                where m.received_id =@Id";

                        var objdata = connection.Query<tran_mcd_receive_DTO>(query,
                            new { Id = Id }).ToList().FirstOrDefault();

                        var objRet = JsonConvert.DeserializeObject<tran_mcd_receive_DTO>(JsonConvert.SerializeObject(objdata));


                        if (!string.IsNullOrEmpty(objRet.documents))
                        {
                            var file_list = JsonConvert.DeserializeObject<List<file_upload>>(objRet.documents);

                            objpdf_list = await _CommonService.LoadAllFiles(file_list, "mcd");
                        }

                        objRet.List_Files = objpdf_list;

                        if (objRet != null && !string.IsNullOrEmpty(objdata.tran_mcd_receive_json))
                        {
                            objRet.details = JsonConvert.DeserializeObject<List<tran_mcd_receive_detail_DTO>>(objdata.tran_mcd_receive_json);
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

                    var objToDelete = new tran_mcd_receive_entity { received_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_scm_po_DTO>> GetDropDownData(DtParameters filter, long group)
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
                            search_text = filter.Search.Value,
                            row_index = filter.Start,
                            page_size = filter.Length
                        }).ToList();

                    return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(JsonConvert.SerializeObject(dataList));

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        //public async Task<tran_scm_po_DTO> GetPO(long po_id)
        //{

        //    try
        //    {

        //        var resp=await _rpc_db_service.GetAllsp_get_data_for_mcd_receiveAsync(po_id);

        //        tran_scm_po_DTO ret = JsonConvert.DeserializeObject<tran_scm_po_DTO>(JsonConvert.SerializeObject(resp));

        //        //if (resp.receive_quantity == null)
        //        //{
        //        //    resp.receive_quantity = 0;

        //        //    ret.receive_quantity = resp.receive_quantity;
        //        //}

        //        ret.po_details = JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(resp.list_po_details);


        //        //ret.receive_details = JsonConvert.DeserializeObject<List<tran_mcd_receive_DTO>>(resp.receive_quantity.ToString());

        //        foreach (var obj in ret.po_details)
        //        {
        //            obj.uomText = obj.unit;
        //        }

        //        ret.delivery_unit_name = resp.unit_name;
        //        ret.supplier_name = resp.name;
        //        ret.deliveryAddress = resp.unit_address;
        //        //ret.receive_details = resp.receive_quantity;
        //        //resp.receive_quantity
        //        //var response = await _connectionSupabse.From<tran_scm_po_entity>().Select("*").Where(p => p.po_id==po_id).Get();

        //        //var ret = JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content).FirstOrDefault();
        //        //ret.item_structure_group_id = ret.item_structure_group_id;

        //        //if (ret.delivery_unit is not null)
        //        //{
        //        //    var delivery_unit = await _connectionSupabse.From<gen_unit_entity>().Select("*").Where(p => p.gen_unit_id == ret.delivery_unit).Get();
        //        //    ret.delivery_unit_name = JsonConvert.DeserializeObject<List<gen_unit_DTO>>(delivery_unit.Content).FirstOrDefault().unit_name;
        //        //}
        //        //if (ret.supplier_id is not null)
        //        //{
        //        //    var supp = await _connectionSupabse.From<gen_supplier_information_entity>().Select("*").Where(p => p.gen_supplier_information_id == ret.supplier_id).Get();
        //        //    ret.supplier_name = JsonConvert.DeserializeObject<List<gen_supplier_information_DTO>>(supp.Content).FirstOrDefault().name;
        //        //}
        //        //// need to be converted to sp
        //        //var detailsJson = await _connectionSupabse.From<tran_scm_po_details_entity>().Select("*").Where(p => p.po_id == ret.po_id).Get();
        //        //var details = JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(detailsJson.Content);
        //        //foreach (tran_scm_po_details_DTO dto in details)
        //        //{
        //        //    var gen_master = await _connectionSupabse.From<gen_item_master_entity>().Select("*").Where(p => p.gen_item_master_id == dto.item_id).Get();
        //        //    gen_item_master_DTO item_master = JsonConvert.DeserializeObject<List<gen_item_master_DTO>>(gen_master.Content).FirstOrDefault();
        //        //    dto.item_name = item_master.item_name;
        //        //    dto.item_spec = item_master.item_spec;
        //        //    // var gen_master_unit = await _connectionSupabse.From<gen_measurement_unit_detail_entity>().Select("*").Where(p => p.gen_measurement_unit_detail_id ==dto.pr_id).Get();
        //        //    // var gen_master_unit_Dto = JsonConvert.DeserializeObject<List<gen_measurement_unit_detail_DTO>>(gen_master_unit.Content).FirstOrDefault();
        //        //    dto.uomText = dto.unit;//gen_master_unit_Dto.unit_detail_display;
        //        //}
        //        //ret.po_details = details;

        //        //end

        //        return ret;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //}


        //public async Task<List<tran_scm_po_DTO>> GetAllPagedDataForSelect2(DtParameters param, long group)
        //{
        //    try
        //    {
        //        await _connectionSupabse.InitializeAsync();

        //        var sort_column = ""; var sort_order = "";

        //        if (!string.IsNullOrEmpty(param.Search.Value))
        //        {

        //            var TotalRecord = await _connectionSupabse
        //                   .From<tran_scm_po_entity>()
        //                   .Select(x => new object[] { x.po_id })
        //                   .Filter(p => p.po_no, Operator.ILike, "%" + param.Search.Value + "%")
        //                   .Count(Constants.CountType.Exact);

        //            var response = await _connectionSupabse.From<tran_scm_po_entity>()
        //           .Select("*")
        //           .Filter(p => p.po_no, Operator.ILike, "%" + param.Search.Value + "%")
        //           .Order("po_no", Ordering.Ascending)
        //           .Range(param.Start, param.Start + param.Length - 1)
        //           .Get();

        //            var list = JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content);

        //            list.ForEach(p => p.TotalRecord = TotalRecord);

        //            return list;
        //        }
        //        else
        //        {
        //            var TotalRecord = await _connectionSupabse
        //                  .From<tran_scm_po_entity>()
        //                  .Select(x => new object[] { x.po_id })
        //                  .Count(Constants.CountType.Exact);

        //            var response = await _connectionSupabse.From<tran_scm_po_entity>()
        //           .Select("*")
        //           .Order("po_no", Ordering.Ascending)
        //           .Range(param.Start, param.Start + param.Length - 1)
        //           .Get();



        //            var list = JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content);

        //            list.ForEach(p => p.TotalRecord = TotalRecord);

        //            return list;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //}


        //public async Task<List<gen_warehouse_floor_DTO>> GetWarehouseFloor()
        //{

        //    try
        //    {
        //        await _connectionSupabse.InitializeAsync();

        //        var response = await _connectionSupabse.From<gen_warehouse_floor_entity>()
        //        .Select("gen_warehouse_floor_id,floor_name")
        //        .Get();

        //        return JsonConvert.DeserializeObject<List<gen_warehouse_floor_DTO>>(response.Content);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);

        //    }

        //}

        //public async Task<List<gen_warehouse_floor_rack_DTO>> GetWarehouseRackName(long selectedValue)
        //{
        //    try
        //    {
        //        await _connectionSupabse.InitializeAsync();
        //        var response = await _connectionSupabse.From<gen_warehouse_floor_rack_entity>()
        //        .Select("gen_warehouse_floor_id,rack_name")
        //        .Where(p => p.gen_warehouse_floor_id == selectedValue)
        //        .Get();

        //        return JsonConvert.DeserializeObject<List<gen_warehouse_floor_rack_DTO>>(response.Content);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);

        //    }

        //}


        public async Task<bool> AcKnowledgeAsync(tran_mcd_receive_entity entity)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT public.tranmcdacKnowledge(@p_receive_id, @p_is_acknowledged)";
                    // Create the parameters
                    var parameters = new
                    {
                        p_receive_id = entity.received_id,
                        p_is_acknowledged = 1
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
                throw ex;
            }
        }

        public async Task<List<tran_mcd_receive_transport_DTO>> GetAllTransportType()
        {
            List<tran_mcd_receive_transport_DTO> list = new List<tran_mcd_receive_transport_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    var dataList = connection.GetAll<gen_tran_transport_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<tran_mcd_receive_transport_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public async Task<List<gen_transaction_mode_DTO>> GetAllTransactionMode()
        {
            List<gen_transaction_mode_DTO> list = new List<gen_transaction_mode_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    var dataList = connection.GetAll<gen_transaction_mode_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<gen_transaction_mode_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public async Task<List<gen_delivery_status_DTO>> GetAllDeliveryStatus()
        {
            List<gen_delivery_status_DTO> list = new List<gen_delivery_status_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    var dataList = connection.GetAll<gen_delivery_status_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<gen_delivery_status_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public async Task<List<rpc_tran_mcd_requisition_slip_DTO>> GetAllsp_get_techPack_by_gen_item_structure_group_sub_idAsync(Int64 gen_item_master_id, Int64 gen_item_structure_group_sub_id_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_Item_detail_by_gen_item_structure_group_sub_id( @gen_item_master_id_filter,@gen_item_structure_group_sub_id_filter)";

                    var dataList = connection.Query<rpc_tran_mcd_requisition_slip_DTO>(query,
                          new
                          {
                              gen_item_master_id_filter = gen_item_master_id,
                              gen_item_structure_group_sub_id_filter
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

