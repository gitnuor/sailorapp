using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.SupplyChain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Postgrest;
using Serilog.Context;

using static Postgrest.Constants;
using static Postgrest.QueryOptions;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Application.Interface;
namespace EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt
{

    public class AccessoriesBookingRequisitionService : IAccessoriesBookingRequisitionService
    {

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;
		private readonly ILogger<TranDesignerReviewService> _logger;
		private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IGenMeasurementUnitDetailService _GenMeasurementUnitDetailService;

        private readonly IGenItemMasterService _GenItemMasterService;

//        public AccessoriesBookingRequisitionService(IGenMeasurementUnitDetailService GenMeasurementUnitDetailService, IGenItemMasterService GenItemMasterService, IConfiguration configuration, IMapper mapper, IHostingEnvironment hostingEnvironment, ILogger<TranDesignerReviewService> logger)
//        {
//            _GenItemMasterService = GenItemMasterService;
//            _GenMeasurementUnitDetailService = GenMeasurementUnitDetailService;
//            _hostingEnvironment = hostingEnvironment;
//            _mapper = mapper;
//			_logger = logger;
//			_configuration = configuration;
//            HttpClient httpClient = new HttpClient();
//            var supabaseKey = _configuration.GetValue<string>("supabaseKey");
//            var supabaseUrl = _configuration.GetValue<string>("supabaseUrl");
//            httpClient.DefaultRequestHeaders.Add("apikey", supabaseKey);
//            _connectionSupabse = new Supabase.Client(supabaseUrl, supabaseKey, new SupabaseOptions { AutoConnectRealtime = false });
//}

//        public async Task<bool> SaveAsync( tran_purchase_requisition_entity entity, List<file_upload> file_upload, List<tran_purchase_requisition_dtl_entity> detail)
//        {
//            try
//            {
//                List<file_upload> files = new List<file_upload>();
//                foreach (var singleimage in file_upload)
//                {
//                    file_upload file = new file_upload();
//                    file.filetype = singleimage.extension;
//                    file.server_filename = System.Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);
//                    file.filename = singleimage.filename;
//                    file.extension = Path.GetExtension(singleimage.filename);
//                    byte[] byteArray = Convert.FromBase64String(singleimage.base64string);


//                    try
//                    {

//                        string folderPath = _configuration.GetValue<string>("UploadFolderPath");

//                        file.filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/purchase_requisition/" + file.server_filename;

//                        if (!Directory.Exists(file.filePath))
//                        {
//                            DirectoryInfo di = Directory.CreateDirectory(file.filePath);
//                        }

//                        File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath), byteArray);
//                        files.Add(file);
//                    }
//                    catch (Exception ex)
//                    {

//                    }


//                }
//                entity.document_list = JArray.Parse(JsonConvert.SerializeObject(files.ToList())).ToString();
//                await _connectionSupabse.InitializeAsync();
//                long pr_id = 0;

//                var retObj = await _connectionSupabse.From<tran_purchase_requisition_entity>().Insert(entity,
//                    new QueryOptions { Returning = ReturnType.Representation });

//                if (Convert.ToInt64(retObj.Models[0].PrimaryKey.FirstOrDefault().Value) > 0)
//                {
//                    pr_id = Convert.ToInt64(retObj.Models[0].PrimaryKey.FirstOrDefault().Value);

//                };

//                try {

//                    foreach (tran_purchase_requisition_dtl_entity item in detail)
//                    {
//                        item.pr_id = pr_id;
//                    }
//                    await _connectionSupabse.From<tran_purchase_requisition_dtl_entity>().Insert(detail);
//                    return true;

//                }
//                catch(Exception e) {

//                    await _connectionSupabse.From<tran_purchase_requisition_dtl_entity>().Where(x => x.pr_id == pr_id).Delete();
//                    await _connectionSupabse.From<tran_purchase_requisition_entity>().Where(x => x.pr_id == pr_id).Delete();
//                    return false;
//                }
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }

//        }

//        public async Task<bool> UpdateAsync(tran_purchase_requisition_entity entity, List<file_upload> file_upload,
//            List<tran_purchase_requisition_dtl_entity> detail,List<string> DeleteFiles)
//        {
//            try
//            {
//                await _connectionSupabse.InitializeAsync();
//                var response = await _connectionSupabse.From<tran_purchase_requisition_entity>()
//                .Where(x => x.pr_id == entity.pr_id)
//                .Select("*").Get();
//                List<file_upload> files = new List<file_upload>();
//                tran_purchase_requisition_DTO ret = JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response.Content).FirstOrDefault();
//                if (DeleteFiles is not null && DeleteFiles.Count >0)
//                {

//                    if (ret.document_list.Count > 0)
//                    {

//                        foreach (JObject doc in ret.document_list)
//                        {
//                            bool WillKeep = true;
//                            file_upload file_new = JsonConvert.DeserializeObject<file_upload>(doc.ToString());

//                            foreach (string SName in DeleteFiles)
//                            {
//                                if (SName == file_new.server_filename)
//                                {
//                                    WillKeep = false;
//                                    try
//                                    {

//                                        if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, file_new.filePath)))
//                                        {
//                                            File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, file_new.filePath));
//                                        }

//                                    }
//                                    catch (Exception ex)
//                                    {

//                                    }

//                                }

//                            }

//                            if (WillKeep)
//                            {
//                                files.Add(file_new);
//                            }

//                        }

//                    }

//                }
               
//                foreach (var singleimage in file_upload)
//                {
//                    file_upload file = new file_upload();
//                    file.filetype = singleimage.extension;
//                    file.server_filename = System.Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);
//                    file.filename = singleimage.filename;
//                    file.extension = Path.GetExtension(singleimage.filename);
//                    byte[] byteArray = Convert.FromBase64String(singleimage.base64string);


//                    try
//                    {

//                        string folderPath = _configuration.GetValue<string>("UploadFolderPath");

//                        file.filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/purchase_requisition/" + file.server_filename;

//                        if (!Directory.Exists(file.filePath))
//                        {
//                            DirectoryInfo di = Directory.CreateDirectory(file.filePath);
//                        }

//                        File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath), byteArray);
//                        files.Add(file);
//                    }
//                    catch (Exception ex)
//                    {

//                    }


//                }
//                entity.document_list = JArray.Parse(JsonConvert.SerializeObject(files.ToList()));
//                entity.added_by = ret.added_by;
//                entity.date_added = ret.date_added;


//                await _connectionSupabse.From<tran_purchase_requisition_entity>().Update(entity);
//                try
//                {
//                    await _connectionSupabse.From<tran_purchase_requisition_dtl_entity>().Where(x => x.pr_id == entity.pr_id).Delete();
//                    foreach (tran_purchase_requisition_dtl_entity item in detail)
//                    {
//                        item.pr_id = entity.pr_id;
//                    }
//                    await _connectionSupabse.From<tran_purchase_requisition_dtl_entity>().Insert(detail);
//                    return true;

//                }
//                catch (Exception e)
//                {

                   
                    
//                    return false;
//                }
                

               
//            }
//            catch (Exception ex)
//            {
//                throw (ex);
//            }

//        }
		

//        public async Task<List<tran_purchase_requisition_DTO>> GetAllAsync()
//        {
//            try
//            {
//                await _connectionSupabse.InitializeAsync();

//                var response =await _connectionSupabse.From<tran_purchase_requisition_entity>().Select("*").Get(); 

//                return  JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response.Content);

//            }
//            catch (Exception ex)
//            {
//                throw (ex);
//            }

//        }

//         public async Task<List<tran_purchase_requisition_DTO>> GetAllPagedDataAsync(DtParameters param, long group_id)
//        {
//            try
//            {
//                await _connectionSupabse.InitializeAsync();

//                var sort_column = ""; var sort_order = "";

//                if (param.SortOrder.Contains(' '))
//                {
//                    sort_column = param.SortOrder.Split(' ')[0];
//                    sort_order = param.SortOrder.Split(' ')[1];
//                }
//                else
//                {
//                    sort_column = param.SortOrder;
//                    sort_order = param.Order.Count() > 0 ? param.Order[0].ToString() : "asc";
//                }

//                if (!string.IsNullOrEmpty(param.Search.Value))
//                {
//                    //replace primary column from filter by your required column
//                    var response = await _connectionSupabse.From<tran_purchase_requisition_entity>()
//                   .Select("*")
//                   .Filter(p => p.pr_no, Operator.ILike, "%" + param.Search.Value + "%")
//                   .Order("pr_id", Ordering.Descending)
//                   .Range(param.Start, param.Start + param.Length - 1)
//				   .Where(p => p.gen_item_structure_group_id != group_id)
//                      .Where(x => x.techpack_id == null)
//                   .Get();

//                    return JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response.Content);
//                }
//                else
//                {
//                    var response = await _connectionSupabse.From<tran_purchase_requisition_entity>()
//                   .Select("*")
//                   .Order("pr_id", Ordering.Descending)
//                   .Range(param.Start, param.Start + param.Length - 1)
//				   .Where(p => p.gen_item_structure_group_id != group_id)
//                   .Where(x => x.techpack_id == null)
//                   .Get();

//                    return JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response.Content);
//                }

//            }
//            catch (Exception ex)
//            {
//                throw (ex);
//            }

//        }

//        public async Task<tran_purchase_requisition_DTO> GetSingleAsync(Int64 Id)
//        {
//            try
//            {
//                await _connectionSupabse.InitializeAsync();
//				List<file_upload> objfile_list = new List<file_upload>();
//				List<file_upload> objpdf_list = new List<file_upload>();
//				var response =await _connectionSupabse.From<tran_purchase_requisition_entity>().Select("*").Where(p=>p.pr_id==Id).Get();

//				var ret=  JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response.Content).FirstOrDefault();

//				if (ret.document_list is not null && ret.document_list.Count > 0)
//				{

//					foreach (JObject str in ret.document_list)
//					{
//						try
//						{
//							var file = JsonConvert.DeserializeObject<file_upload>(str.ToString());




//							if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath)))
//							{
//								// Read the file into a byte array
//								var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath));




//								file.base64string = Convert.ToBase64String(bytes);
//								//file.base64string = $"data:application/pdf;base64,{base64String}";



//								objpdf_list.Add(file);
//							}
//						}
//						catch (Exception ex)
//						{

//							using (LogContext.PushProperty("SpecialLogType", true))
//							{
//								_logger.LogError(ex.ToString());
//							}
//						}

//					}
//				}
//				ret.List_Files = objpdf_list;


//                // need to be converted to sp
//				var detailsJson=await _connectionSupabse.From<tran_purchase_requisition_dtl_entity>().Select("*").Where(p => p.pr_id == ret.pr_id).Get();
//				var details = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_DTO>>(detailsJson.Content);
//				foreach (tran_purchase_requisition_dtl_DTO dto in details)
//                {
//                    //var gen_master=await _connectionSupabse.From<gen_item_master_entity>().Select("*").Where(p => p.gen_item_master_id == dto.item_id).Get();
//                    var gen_master = await _GenItemMasterService.GetSingleAsync(dto.item_id.Value);

//                    //gen_item_master_DTO item_master = JsonConvert.DeserializeObject <List<gen_item_master_DTO>>(gen_master.Content).FirstOrDefault();


//                    dto.item_name = gen_master.item_name;
//                    dto.item_spec = gen_master.item_spec;
//                    //var gen_master_unit = await _connectionSupabse.From<gen_measurement_unit_detail_entity>().Select("*").Where(p => p.gen_measurement_unit_detail_id == dto.uom).Get();
//                    //var gen_master_unit_Dto = JsonConvert.DeserializeObject<List<gen_measurement_unit_detail_DTO>>(gen_master_unit.Content).FirstOrDefault();
//                    var gen_master_unit_Dto = await _GenMeasurementUnitDetailService.GetAsync(dto.item_id.Value);
//                    dto.uomText = gen_master_unit_Dto.unit_detail_display;
//				}
//                ret.details = details;


//				//end

//				return ret;
//			}
//            catch (Exception ex)
//            {
//				throw (ex);
//            }
            

//		}

//         public async Task<bool> DeleteAsync(Int64 Id)
//         {
//            try
//            {
//                await _connectionSupabse.InitializeAsync();

//                var response = await _connectionSupabse.From<tran_purchase_requisition_entity>().Select("*").Where(p => p.pr_id == Id).Get();

//                var objDelete= JsonConvert.DeserializeObject<List<tran_purchase_requisition_entity>>(response.Content).FirstOrDefault();

//                await _connectionSupabse.From<tran_purchase_requisition_entity>().Delete(objDelete);

//                return true;

//            }
//            catch (Exception ex)
//            {
//                throw (ex);
//            }

//        }
	
//        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionAsync(Int64 currnet_page, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, Int64 listType)
//        {
//            try
//            {
//                await _connectionSupabse.InitializeAsync();

//                var objFilter = new Dictionary<string, object>();



//                objFilter.Add("currnet_page", currnet_page);
//                objFilter.Add("page_size", page_size);
//                objFilter.Add("fiscal_year", fiscal_year_id);
//                objFilter.Add("p_event_id", event_id);
//                objFilter.Add("supplier", supplier_id);
//                objFilter.Add("p_delivery_unit_id", delivery_unit_id);
//                objFilter.Add("list_type", listType);

//                var response = await _connectionSupabse.Rpc("proc_sp_get_data_tran_purchase_requisition_acc", objFilter);

//                return JsonConvert.DeserializeObject<List<rpc_tran_purchase_requisition_DTO>>(response.Content);

//            }
//            catch (Exception ex)
//            {
//                throw (ex);
//            }

//        }


//        public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionSubmittedAsync(Int64 currnet_page, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, Int64 listType)
//        {
//            try
//            {
//                await _connectionSupabse.InitializeAsync();

//                var objFilter = new Dictionary<string, object>();



//                objFilter.Add("currnet_page", currnet_page);
//                objFilter.Add("page_size", page_size);
//                objFilter.Add("fiscal_year", fiscal_year_id);
//                objFilter.Add("p_event_id", event_id);
//                objFilter.Add("supplier", supplier_id);
//                objFilter.Add("p_delivery_unit_id", delivery_unit_id);
//                objFilter.Add("list_type", listType);

//                var response = await _connectionSupabse.Rpc("proc_sp_get_data_tran_purchase_requisition_acc", objFilter);

//                return JsonConvert.DeserializeObject<List<rpc_tran_purchase_requisition_DTO>>(response.Content);

//            }
//            catch (Exception ex)
//            {
//                throw (ex);
//            }

//        }






    }

}

