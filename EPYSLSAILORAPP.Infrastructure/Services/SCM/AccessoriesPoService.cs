using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using Serilog.Context;
namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class AccessoriesPoService : IAccessoriesPoService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<AccessoriesPoService> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenItemMasterService _GenItemMasterService;
        private readonly IGenMeasurementUnitDetailService _GenMeasurementUnitDetailService;
        private readonly IGenUnitService _GenUnitService;
        private readonly ITranScmPoService _TranScmPoService;
        private readonly IRPCDbService _RPCDbService;
        private readonly ITranPurchaseRequisitionService _TranPurchaseRequisitionService;
        public AccessoriesPoService(ITranPurchaseRequisitionService TranPurchaseRequisitionService,IRPCDbService RPCDbService,
            ITranScmPoService TranScmPoService,IGenUnitService GenUnitService,IGenMeasurementUnitDetailService GenMeasurementUnitDetailService,IGenItemMasterService GenItemMasterService,IGenSupplierInformationService GenSupplierInformationService,IConfiguration configuration, ILogger<AccessoriesPoService> logger, IMapper mapper, IHostingEnvironment hostingEnvironment)
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
            _TranScmPoService = TranScmPoService;
            _RPCDbService = RPCDbService;
        }

        public async Task<bool> SaveAsync(tran_scm_po_entity entity, List<tran_scm_po_details_entity> details, List<file_upload> files)
        {
            try
            {
               
                long po_id = 0;
                List<file_upload> doc_files = new List<file_upload>();

                foreach (var singleimage in files)
                {
                    file_upload file = new file_upload();
                    file.filetype = singleimage.extension;
                    file.server_filename = System.Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);
                    file.filename = singleimage.filename;
                    file.extension = Path.GetExtension(singleimage.filename);
                    byte[] byteArray = Convert.FromBase64String(singleimage.base64string);

                    try
                    {
                        string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                        file.filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/purchase_order/" + file.server_filename;

                        if (!Directory.Exists(file.filePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(file.filePath);
                        }

                        File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath), byteArray);
                        doc_files.Add(file);
                    }
                    catch (Exception ex)
                    {

                    }


                }
               
                entity.documents = JArray.Parse(JsonConvert.SerializeObject(doc_files.ToList())).ToString();
               
                entity.po_details = JArray.Parse(JsonConvert.SerializeObject(details)).ToString();

                //var retObj = await _connectionSupabse.From<tran_scm_po_entity>().Insert(entity,
                //                 new QueryOptions { Returning = ReturnType.Representation });

                //if (Convert.ToInt64(retObj.Models[0].PrimaryKey.FirstOrDefault().Value) > 0)
                //{
                //    po_id = Convert.ToInt64(retObj.Models[0].PrimaryKey.FirstOrDefault().Value);

                //};

                 await _TranScmPoService.tran_scm_po_update_sp(entity);

                //try
                //{

                //    foreach (tran_scm_po_details_entity item in details)
                //    {
                //        item.po_id = po_id;

                //    }
                //    await _connectionSupabse.From<tran_scm_po_details_entity>().Insert(details);


                //    return true;
                //}
                //catch (Exception ex)
                //{
                //    //await _connectionSupabse.From<tran_scm_po_entity>().Where(x => x.po_id == entity.po_id).Delete();
                //    //await _connectionSupabse.From<tran_scm_po_details_entity>().Where(x => x.po_id == entity.po_id).Delete();
                //    return false;
                //}

                return true;

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
               // await _connectionSupabse.InitializeAsync();

               // var response = await _connectionSupabse.From<tran_scm_po_entity>()
               //.Where(x => x.po_id == entity.po_id)
               //.Select("*").Get();

                List<file_upload> file_upload = entity.List_Files;
                List<string> DeleteFiles = entity.DeleteList;
                List<file_upload> files = new List<file_upload>();
                // tran_scm_po_DTO ret = JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content).FirstOrDefault();
                var response = await _TranScmPoService.GetSingleAsync(entity.po_id.Value);
                tran_scm_po_DTO ret = JsonConvert.DeserializeObject<tran_scm_po_DTO>(JsonConvert.SerializeObject( response));

                if (DeleteFiles.Count>0)
                {

                    if (ret.documents!= null)
                    {
                        var file_list=JsonConvert.DeserializeObject<List<file_upload>>(ret.documents);

                        foreach (var file_new in file_list)
                        {
                            bool WillKeep = true;
                           // file_upload file_new = JsonConvert.DeserializeObject<file_upload>(doc.ToString());

                            foreach (string SName in DeleteFiles)
                            {
                                if (SName == file_new.server_filename)
                                {
                                    WillKeep = false;

                                    try
                                    {

                                        if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, file_new.filePath)))
                                        {
                                            File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, file_new.filePath));
                                        }

                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                }

                            }

                            if (WillKeep)
                            {
                                files.Add(file_new);
                            }

                        }

                    }

                }

                foreach (var singleimage in file_upload)
                {
                    file_upload file = new file_upload();
                    file.filetype = singleimage.extension;
                    file.server_filename = System.Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);
                    file.filename = singleimage.filename;
                    file.extension = Path.GetExtension(singleimage.filename);
                    byte[] byteArray = Convert.FromBase64String(singleimage.base64string);


                    try
                    {

                        string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                        file.filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/purchase_order/" + file.server_filename;

                        if (!Directory.Exists(file.filePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(file.filePath);
                        }

                        File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath), byteArray);
                        files.Add(file);
                    }
                    catch (Exception ex)
                    {

                    }


                }
                ret.documents = (string?)JArray.Parse(JsonConvert.SerializeObject(files.ToList()));
                ret.updated_by = entity.added_by;
                ret.date_updated = DateTime.Now;
                ret.po_date = entity.po_date;
                ret.delivery_start_date = entity.delivery_start_date;
                ret.delivery_end_date = entity.delivery_end_date;
                ret.supplier_id= entity.supplier_id;
				ret.is_submitted = entity.is_submitted;

				var model = JsonConvert.DeserializeObject<tran_scm_po_entity>(JsonConvert.SerializeObject(ret));

               // model.po_details = JArray.Parse(JsonConvert.SerializeObject(details)).ToString();

                // await _connectionSupabse.From<tran_scm_po_entity>().Update(model);

                await _TranScmPoService.tran_scm_po_update_sp(model);

                return true;

                //try
                //{
                //    await _connectionSupabse.From<tran_scm_po_details_entity>().Where(x => x.po_id == entity.po_id).Delete();

                //    List<tran_scm_po_details_entity> details = JsonConvert.DeserializeObject<List<tran_scm_po_details_entity>>(JsonConvert.SerializeObject(entity.po_details));
                //    foreach (tran_scm_po_details_entity item in details)
                //    {
                //        item.po_id = entity.po_id;
                //        item.pr_id = entity.pr_id;

                //    }
                //    await _connectionSupabse.From<tran_scm_po_details_entity>().Insert(details);
                //    return true;

                //}
                //catch (Exception e)
                //{



                //    return false;
                //}


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_scm_po_DTO>> GetAllAsync()
        {
            List<tran_scm_po_entity> list = new List<tran_scm_po_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_scm_po_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(JsonConvert.SerializeObject(dataList));
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
                                           where m.item_structure_group_id != "+ Convert.ToInt64(Enum_gen_item_structure_group.Fabric).ToString() +@" and case
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

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
    //        try
    //        {
    //            long group = 1;

				//await _connectionSupabse.InitializeAsync();

    //            var sort_column = ""; var sort_order = "";

    //            if (param.SortOrder.Contains(' '))
    //            {
    //                sort_column = param.SortOrder.Split(' ')[0];
    //                sort_order = param.SortOrder.Split(' ')[1];
    //            }
    //            else
    //            {
    //                sort_column = param.SortOrder;
    //                sort_order = param.Order.Count() > 0 ? param.Order[0].ToString() : "asc";
    //            }

    //            if (!string.IsNullOrEmpty(param.Search.Value))
    //            {
    //                //replace primary column from filter by your required column
    //                var response = await _connectionSupabse.From<tran_scm_po_entity>()
    //               .Select("*")
    //               .Filter(p => p.po_id, Operator.ILike, "%" + param.Search.Value + "%")
    //               .Order("po_id", Ordering.Descending)
    //               .Range(param.Start, param.Start + param.Length - 1)
				//	.Where(x => x.item_structure_group_id != group)
				//   .Get();

    //                return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content);
    //            }
    //            else
    //            {
    //                var response = await _connectionSupabse.From<tran_scm_po_entity>()
    //               .Select("*")
    //               .Order("po_id", Ordering.Descending)
    //               .Range(param.Start, param.Start + param.Length - 1)
				//	.Where(x => x.item_structure_group_id != group)
				//   .Get();

    //                return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content);
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            throw (ex);
    //        }

        }

        public async Task<tran_scm_po_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
				List<file_upload> objpdf_list = new List<file_upload>();

                tran_scm_po_DTO po = new tran_scm_po_DTO();

                var ret_rpc = await _RPCDbService.GetAllproc_sp_get_data_tran_scm_poAsync(Id);

                po = JsonConvert.DeserializeObject<tran_scm_po_DTO>(JsonConvert.SerializeObject(ret_rpc));

                po.List_po_details= JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(ret_rpc.tran_scm_po_details_list);

                //            var response = await _connectionSupabse.From<tran_scm_po_entity>().Select("*").Where(p => p.po_id == Id).Get();
                //            var details = await _connectionSupabse.From<tran_scm_po_details_entity>().Select("*").Where(p => p.po_id == Id).Get();

                //            tran_scm_po_DTO po= JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content).FirstOrDefault();
                //            po.po_details = JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(details.Content);

                //foreach (tran_scm_po_details_DTO dto in po.po_details)
                //{
                //                var gen_master =await _GenItemMasterService.GetSingleAsync(dto.item_id.Value);

                //               dto.item_name = gen_master.item_name;
                //	dto.item_spec = gen_master.item_spec;

                //}

                //var response2 = await _connectionSupabse.From<tran_purchase_requisition_entity>().Select("*").Where(p => p.pr_id == po.pr_id).Get();
                //var pr_response = JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response2.Content).FirstOrDefault();


                //if (pr_response.techpack_id is not null)
                //{
                //    var techpack = await _connectionSupabse.From<tran_tech_pack_entity>().Select("*").Where(p => p.tran_techpack_id == pr_response.techpack_id).Get();
                //    po.techpack_number = JsonConvert.DeserializeObject<List<tran_tech_pack_DTO>>(techpack.Content).FirstOrDefault().techpack_number;
                //}


                //if (pr_response.delivery_unit_id is not null)
                //{
                //    var delivery_unit =await _GenUnitService.GetAsync(pr_response.delivery_unit_id.Value);
                   
                //    po.delivery_unit_name = delivery_unit.FirstOrDefault().unit_name;
                //}
                //if (pr_response.supplier_id is not null)
                //{
                //    var supp = await _GenSupplierInformationService.GetSingleAsync(pr_response.supplier_id.Value);

                //    po.suggested_supplier_name = supp.name;
                //}
                //po.pr_no = pr_response.pr_no;


				if (po.documents is not null)
				{

					foreach (JObject str in po.documents)
					{
						try
						{
							var file = JsonConvert.DeserializeObject<file_upload>(str.ToString());


							if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath)))
							{
								var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath));

								file.base64string = Convert.ToBase64String(bytes);

								objpdf_list.Add(file);
							}
						}
						catch (Exception ex)
						{

							using (LogContext.PushProperty("SpecialLogType", true))
							{
								_logger.LogError(ex.ToString());
							}
						}

					}
				}

				po.List_Files = objpdf_list;


				return po;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<rpc_tran_scm_po_DTO>> GetAllJoined_TranScmPoAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, Int64 listType)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_acc( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@list_type)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year= fiscal_year_id,
                              p_event_id=event_id,
                              supplier= supplier_id,
                              p_delivery_unit_id= delivery_unit_id,
                              list_type=listType
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
        public async Task<bool> DeleteAsync(Int64 Id)
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

       
        public async Task<List<tran_purchase_requisition_dtl_DTO>> GetPRDetails(long pr_id)
        {

            var ret_rpc = await _RPCDbService.GetAllproc_sp_get_data_tran_purchase_requisition_detailsAsync(pr_id);

            //var detailsJson = await _connectionSupabse.From<tran_purchase_requisition_dtl_entity>().Select("*").Where(p => p.pr_id == pr_id).Get();
            //var details = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_DTO>>(detailsJson.Content);
            //foreach (tran_purchase_requisition_dtl_DTO dto in details)
            //{
            //    var gen_master = await _GenItemMasterService.GetSingleAsync(dto.item_id.Value);

            //    dto.item_name = gen_master.item_name;
            //    dto.item_spec = gen_master.item_spec;
            //    //var gen_master_unit = await _connectionSupabse.From<gen_measurement_unit_detail_entity>().Select("*").Where(p => p.gen_measurement_unit_detail_id == dto.uom).Get();
            //    //var gen_master_unit_Dto = JsonConvert.DeserializeObject<List<gen_measurement_unit_detail_DTO>>(gen_master_unit.Content).FirstOrDefault();
            //    var gen_master_unit_Dto =await _GenMeasurementUnitDetailService.GetAsync(dto.item_id.Value);

            //    dto.uomText = gen_master_unit_Dto.unit_detail_display;
            //}
            //return details.ToList();

            var details = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_DTO>>(ret_rpc.tran_purchase_requisition_dtl_list);
            
            return details.ToList();

        }

        public async Task<tran_purchase_requisition_DTO> GetPR(long pr_id)
        {

            try
            {

                //var response = await _connectionSupabse.From<tran_purchase_requisition_entity>().Select("*").Where(p => p.pr_id == pr_id).Get();

                //var ret = JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response.Content).FirstOrDefault();

                var ret_rpc = await _RPCDbService.GetAllproc_sp_get_data_tran_purchase_requisition_detailsAsync(pr_id);

                var ret = JsonConvert.DeserializeObject<tran_purchase_requisition_DTO>(JsonConvert.SerializeObject(ret_rpc));


                //document if needded 
                //if (ret.techpack_id is not null)
                //{
                //   var techpack= await _connectionSupabse.From<tran_tech_pack_entity>().Select("*").Where(p => p.tran_techpack_id == ret.techpack_id).Get();
                //   ret.techpack_name = JsonConvert.DeserializeObject<List<tran_tech_pack_DTO>>(techpack.Content).FirstOrDefault().techpack_number;
                //}
                //if (ret.delivery_unit_id is not null)
                //{
                //    var delivery_unit = await _GenUnitService.GetAsync(ret.delivery_unit_id.Value);

                //    ret.delivery_unit_name = delivery_unit.FirstOrDefault().unit_name;
                //}
                //if (ret.supplier_id is not null)
                //{
                //    var supp = await _GenSupplierInformationService.GetSingleAsync(ret.supplier_id.Value);

                //    ret.supplier_name = supp.name;// JsonConvert.DeserializeObject<List<gen_supplier_information_DTO>>(supp.Content).FirstOrDefault().name;
                //}
                // need to be converted to sp
                //var detailsJson = await _connectionSupabse.From<tran_purchase_requisition_dtl_entity>().Select("*").Where(p => p.pr_id == ret.pr_id).Get();
                //var details = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_DTO>>(detailsJson.Content);
               
                //foreach (tran_purchase_requisition_dtl_DTO dto in details)
                //{
                    
                //    var gen_master = await _GenItemMasterService.GetSingleAsync(dto.item_id.Value);

                //    dto.item_name = gen_master.item_name;
                //    dto.item_spec = gen_master.item_spec;
                    
                //    var gen_master_unit_Dto = await _GenMeasurementUnitDetailService.GetAsync(dto.item_id.Value);

                //    dto.uomText = gen_master_unit_Dto.unit_detail_display;
                //}


                ret.details =  JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_DTO>>(ret_rpc.det_list); ;

                return ret;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

    }
}

