//using AutoMapper;
//using EPYSLSAILORAPP.Application.DTO;
//using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
//using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
//using EPYSLSAILORAPP.Application.Interface.Tran_MerchandisingMgt;
//using EPYSLSAILORAPP.Domain.DTO;
//using EPYSLSAILORAPP.Domain.Entity;
//using EPYSLSAILORAPP.Domain.Entity.SupplyChain;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Serilog.Context;
//using Supabase;
//using static Postgrest.Constants;
//using EPYSLSAILORAPP.Domain.RPC;
//using EPYSLSAILORAPP.Application.Interface;

//namespace EPYSLSAILORAPP.Infrastructure.Services.Tran_MerchandisingMgt
//{

//    public class PRApprovalService : IPRApprovalService
//    {
//        private readonly IGenItemMasterService _GenItemMasterService;
//        private readonly IConfiguration _configuration;
        
//        private readonly IMapper _mapper;
//		private readonly ILogger<PRApprovalService> _logger;
//		private readonly IHostingEnvironment _hostingEnvironment;
//        private readonly IGenMeasurementUnitDetailService _GenMeasurementUnitDetailService;
//        private readonly ITranPurchaseRequisitionService _TranPurchaseRequisitionService;
//        public PRApprovalService(IGenItemMasterService GenItemMasterService,
//            ITranPurchaseRequisitionService TranPurchaseRequisitionService,IGenMeasurementUnitDetailService GenMeasurementUnitDetailService, IConfiguration configuration, IMapper mapper, IHostingEnvironment hostingEnvironment, ILogger<PRApprovalService> logger)
//        {
//            _hostingEnvironment = hostingEnvironment;
//            _GenItemMasterService = GenItemMasterService;
//            _GenMeasurementUnitDetailService = GenMeasurementUnitDetailService;
//            _TranPurchaseRequisitionService = TranPurchaseRequisitionService;
//            _mapper = mapper;
//			_logger = logger;
//			_configuration = configuration;
//            HttpClient httpClient = new HttpClient();
//            var supabaseKey = _configuration.GetValue<string>("supabaseKey");
//            var supabaseUrl = _configuration.GetValue<string>("supabaseUrl");
//            httpClient.DefaultRequestHeaders.Add("apikey", supabaseKey);
//            //_connectionSupabse = new Supabase.Client(supabaseUrl, supabaseKey, new SupabaseOptions { AutoConnectRealtime = false });
//        }


//		//public async Task<bool> DraftSaveAsync(long dpr_id)
//		//{
//		//	try
//		//	{
//		//		await _connectionSupabse.InitializeAsync();
//		//		var response = await _connectionSupabse.From<tran_draft_purchase_requisition_entity>()
//		//		.Where(x => x.dpr_id == dpr_id)
//		//		.Select("*").Get();
//		//		tran_draft_purchase_requisition_entity entity = JsonConvert.DeserializeObject<List<tran_draft_purchase_requisition_entity>>(response.Content).SingleOrDefault();
//		//		entity.is_approved = true;
//		//		entity.is_acknowledged = false;
//		//		await _connectionSupabse.From<tran_draft_purchase_requisition_entity>().Update(entity);
//		//		return true;
//		//	}
//		//	catch (Exception ex)
//		//	{
//		//		return false;
//		//	}

//		//}

//		//public async Task<bool> SaveAsync(long pr_id)
//  //      {
//  //          try
//  //          {
//  //              await _connectionSupabse.InitializeAsync();
//  //              var response = await _connectionSupabse.From<tran_purchase_requisition_entity>()
//  //              .Where(x => x.pr_id == pr_id)
//  //              .Select("*").Get();
//		//		tran_purchase_requisition_entity entity=JsonConvert.DeserializeObject<List<tran_purchase_requisition_entity>>(response.Content).SingleOrDefault();
//  //              entity.is_approved = 1;
//  //              entity.is_acknowledged = false;
//  //              await _connectionSupabse.From<tran_purchase_requisition_entity>().Update(entity);
//  //              return true;
//		//	}
//  //          catch (Exception ex)
//  //          {
//  //              return false;
//  //          }

//  //      }
//  //      public async Task<List<tran_purchase_requisition_DTO>> GetAllAsync()
//  //      {
//  //          try
//  //          {
//  //              await _connectionSupabse.InitializeAsync();

//  //              var response =await _connectionSupabse.From<tran_purchase_requisition_entity>().Select("*").Get(); 

//  //              return  JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response.Content);

//  //          }
//  //          catch (Exception ex)
//  //          {
//  //              throw (ex);
//  //          }

//  //      }

//  //       public async Task<List<tran_purchase_requisition_DTO>> GetAllPagedDataAsync(DtParameters param, long group_id)
//  //      {
//  //          try
//  //          {
//  //              await _connectionSupabse.InitializeAsync();

//  //              var sort_column = ""; var sort_order = "";

//  //              if (param.SortOrder.Contains(' '))
//  //              {
//  //                  sort_column = param.SortOrder.Split(' ')[0];
//  //                  sort_order = param.SortOrder.Split(' ')[1];
//  //              }
//  //              else
//  //              {
//  //                  sort_column = param.SortOrder;
//  //                  sort_order = param.Order.Count() > 0 ? param.Order[0].ToString() : "asc";
//  //              }

//  //              if (!string.IsNullOrEmpty(param.Search.Value))
//  //              {
//  //                  //replace primary column from filter by your required column
//  //                  var response = await _connectionSupabse.From<tran_purchase_requisition_entity>()
//  //                 .Select("*")
//  //                 .Filter(p => p.pr_no, Operator.ILike, "%" + param.Search.Value + "%")
//  //                 .Order("pr_id", Ordering.Descending)
//  //                 .Range(param.Start, param.Start + param.Length - 1)
//		//		   .Where(x=>x.is_submitted== 2)
//		//		   .Where(x=>x.is_approved== null)
				 
//  //                 .Get();

//  //                  return JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response.Content);
//  //              }
//  //              else
//  //              {
//  //                  var response = await _connectionSupabse.From<tran_purchase_requisition_entity>()
//  //                 .Select("*")
//  //                 .Order("pr_id", Ordering.Descending)
//  //                 .Range(param.Start, param.Start + param.Length - 1)
//		//		   .Where(x => x.is_submitted == 2)
//		//		   .Where(x => x.is_approved == 1)
				
//  //                 .Get();

//  //                  return JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response.Content);
//  //              }

//  //          }
//  //          catch (Exception ex)
//  //          {
//  //              throw (ex);
//  //          }

//  //      }
//		//public async Task<List<tran_draft_purchase_requisition_DTO>> GetDraftPRApprovalData(DtParameters param, long group_id)
//		//{
//		//	try
//		//	{
//		//		await _connectionSupabse.InitializeAsync();

//		//		var sort_column = ""; var sort_order = "";

//		//		if (param.SortOrder.Contains(' '))
//		//		{
//		//			sort_column = param.SortOrder.Split(' ')[0];
//		//			sort_order = param.SortOrder.Split(' ')[1];
//		//		}
//		//		else
//		//		{
//		//			sort_column = param.SortOrder;
//		//			sort_order = param.Order.Count() > 0 ? param.Order[0].ToString() : "asc";
//		//		}

//		//		if (!string.IsNullOrEmpty(param.Search.Value))
//		//		{
//		//			//replace primary column from filter by your required column
//		//			var response = await _connectionSupabse.From<tran_draft_purchase_requisition_entity>()
//		//		   .Select("*")
//		//		   .Filter(p => p.dpr_no, Operator.ILike, "%" + param.Search.Value + "%")
//		//		   .Order("dpr_id", Ordering.Descending)
//		//		   .Range(param.Start, param.Start + param.Length - 1)
//		//		   .Where(x => x.is_submitted == true)
//		//		   .Where(x => x.is_approved == false)
//		//		   .Where(x => x.techpack_id != null)
//		//		   .Get();

//		//			return JsonConvert.DeserializeObject<List<tran_draft_purchase_requisition_DTO>>(response.Content);
//		//		}
//		//		else
//		//		{
//		//			var response = await _connectionSupabse.From<tran_draft_purchase_requisition_entity>()
//		//		   .Select("*")
//		//		   .Order("dpr_id", Ordering.Descending)
//		//		   .Range(param.Start, param.Start + param.Length - 1)
//		//		   .Where(x => x.is_submitted == true)
//		//		   .Where(x => x.is_approved == false)
//		//		   .Where(x => x.techpack_id != null)
//		//		   .Get();

//		//			return JsonConvert.DeserializeObject<List<tran_draft_purchase_requisition_DTO>>(response.Content);
//		//		}

//		//	}
//		//	catch (Exception ex)
//		//	{
//		//		throw (ex);
//		//	}

//		//}
//		//public async Task<tran_purchase_requisition_DTO> GetSingleAsync(Int64 Id)
//  //      {
//  //          try
//  //          {
//  //              await _connectionSupabse.InitializeAsync();
//		//		List<file_upload> objfile_list = new List<file_upload>();
//		//		List<file_upload> objpdf_list = new List<file_upload>();
//		//		var response =await _connectionSupabse.From<tran_purchase_requisition_entity>().Select("*").Where(p=>p.pr_id==Id).Get();

//		//		var ret=  JsonConvert.DeserializeObject<List<tran_purchase_requisition_DTO>>(response.Content).FirstOrDefault();

//		//		if (ret.document_list is not null && ret.document_list.Count > 0)
//		//		{

//		//			foreach (JObject str in ret.document_list)
//		//			{
//		//				try
//		//				{
//		//					var file = JsonConvert.DeserializeObject<file_upload>(str.ToString());




//		//					if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath)))
//		//					{
//		//						// Read the file into a byte array
//		//						var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath));




//		//						file.base64string = Convert.ToBase64String(bytes);
//		//						//file.base64string = $"data:application/pdf;base64,{base64String}";



//		//						objpdf_list.Add(file);
//		//					}
//		//				}
//		//				catch (Exception ex)
//		//				{

//		//					using (LogContext.PushProperty("SpecialLogType", true))
//		//					{
//		//						_logger.LogError(ex.ToString());
//		//					}
//		//				}

//		//			}
//		//		}
//		//		ret.List_Files = objpdf_list;


//  //              // need to be converted to sp
//		//		var detailsJson=await _connectionSupabse.From<tran_purchase_requisition_dtl_entity>().Select("*").Where(p => p.pr_id == ret.pr_id).Get();
//		//		var details = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_DTO>>(detailsJson.Content);
//		//		foreach (tran_purchase_requisition_dtl_DTO dto in details)
//  //              {
//  //                  //var gen_master=await _connectionSupabse.From<gen_item_master_entity>().Select("*").Where(p => p.gen_item_master_id == dto.item_id).Get();
//  //                  //               gen_item_master_DTO item_master = JsonConvert.DeserializeObject <List<gen_item_master_DTO>>(gen_master.Content).FirstOrDefault();
//  //                  var item_master = await _GenItemMasterService.GetSingleAsync(dto.item_id.Value);

//  //                  dto.item_name = item_master.item_name;
//  //                  dto.item_spec = item_master.item_spec;
//		//			//var gen_master_unit = await _connectionSupabse.From<gen_measurement_unit_detail_entity>().Select("*").Where(p => p.gen_measurement_unit_detail_id == dto.uom).Get();
//		//			//var gen_master_unit_Dto = JsonConvert.DeserializeObject<List<gen_measurement_unit_detail_DTO>>(gen_master_unit.Content).FirstOrDefault();
//  //                  var gen_master_unit_Dto = await _GenMeasurementUnitDetailService.GetAsync(dto.item_id.Value);
//  //                  dto.uomText = gen_master_unit_Dto.unit_detail_display;
//		//		}
//  //              ret.details = details;


//		//		//end

//		//		return ret;
//		//	}
//  //          catch (Exception ex)
//  //          {
//		//		throw (ex);
//  //          }
            

//		//}
//  //      public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionAsync(Int64 currnet_page, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id)
//  //      {
//  //          try
//  //          {
//  //              await _connectionSupabse.InitializeAsync();

//  //              var objFilter = new Dictionary<string, object>();



//  //              objFilter.Add("currnet_page", currnet_page);
//  //              objFilter.Add("page_size", page_size);
//  //              objFilter.Add("fiscal_year", fiscal_year_id);
//  //              objFilter.Add("p_event_id", event_id);
//  //              objFilter.Add("supplier", supplier_id);
//  //              objFilter.Add("p_delivery_unit_id", delivery_unit_id);

//  //              var response = await _connectionSupabse.Rpc("proc_sp_get_purchase_requisition_merchandise_pending_approval", objFilter);

//  //              return JsonConvert.DeserializeObject<List<rpc_tran_purchase_requisition_DTO>>(response.Content);

//  //          }
//  //          catch (Exception ex)
//  //          {
//  //              throw (ex);
//  //          }

//  //      }


//  //      public async Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionAsyncDraftSubmitted(Int64 currnet_page, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id)
//  //      {
//  //          try
//  //          {
//  //              await _connectionSupabse.InitializeAsync();

//  //              var objFilter = new Dictionary<string, object>();



//  //              objFilter.Add("currnet_page", currnet_page);
//  //              objFilter.Add("page_size", page_size);
//  //              objFilter.Add("fiscal_year", fiscal_year_id);
//  //              objFilter.Add("p_event_id", event_id);
//  //              objFilter.Add("supplier", supplier_id);
//  //              objFilter.Add("p_delivery_unit_id", delivery_unit_id);

//  //              var response = await _connectionSupabse.Rpc("proc_sp_get_purchase_requisition_merchandise_approved", objFilter);

//  //              return JsonConvert.DeserializeObject<List<rpc_tran_purchase_requisition_DTO>>(response.Content);

//  //          }
//  //          catch (Exception ex)
//  //          {
//  //              throw (ex);
//  //          }

//  //      }


//  //      public async Task<tran_draft_purchase_requisition_DTO> GetSingleDraftAsync(Int64 Id)
//  //      {
//  //          try
//  //          {
//  //              await _connectionSupabse.InitializeAsync();
//  //              List<file_upload> objfile_list = new List<file_upload>();
//  //              List<file_upload> objpdf_list = new List<file_upload>();
//  //              var response = await _connectionSupabse.From<tran_draft_purchase_requisition_entity>().Select("*").Where(p => p.dpr_id == Id).Get();

//  //              var ret = JsonConvert.DeserializeObject<List<tran_draft_purchase_requisition_DTO>>(response.Content).FirstOrDefault();

//  //              if (ret.document_list is not null && ret.document_list.Count > 0)
//  //              {

//  //                  foreach (JObject str in ret.document_list)
//  //                  {
//  //                      try
//  //                      {
//  //                          var file = JsonConvert.DeserializeObject<file_upload>(str.ToString());




//  //                          if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath)))
//  //                          {
//  //                              // Read the file into a byte array
//  //                              var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath));




//  //                              file.base64string = Convert.ToBase64String(bytes);
//  //                              //file.base64string = $"data:application/pdf;base64,{base64String}";



//  //                              objpdf_list.Add(file);
//  //                          }
//  //                      }
//  //                      catch (Exception ex)
//  //                      {

//  //                          using (LogContext.PushProperty("SpecialLogType", true))
//  //                          {
//  //                              _logger.LogError(ex.ToString());
//  //                          }
//  //                      }

//  //                  }
//  //              }
//  //              ret.List_Files = objpdf_list;


//  //              // need to be converted to sp
//  //              var detailsJson = await _connectionSupabse.From<tran_draft_purchase_requisition_dtl_entity>().Select("*").Where(p => p.dpr_id == ret.dpr_id).Get();
//  //              var details = JsonConvert.DeserializeObject<List<tran_purchase_requisition_dtl_DTO>>(detailsJson.Content);
//  //              foreach (tran_purchase_requisition_dtl_DTO dto in details)
//  //              {
//  //                  //var gen_master = await _connectionSupabse.From<gen_item_master_entity>().Select("*").Where(p => p.gen_item_master_id == dto.item_id).Get();
//  //                  //gen_item_master_DTO item_master = JsonConvert.DeserializeObject<List<gen_item_master_DTO>>(gen_master.Content).FirstOrDefault();
//  //                  var item_master = await _GenItemMasterService.GetSingleAsync(dto.item_id.Value);

//  //                  dto.item_name = item_master.item_name;
//  //                  dto.item_spec = item_master.item_spec;
//  //                  //var gen_master_unit = await _connectionSupabse.From<gen_measurement_unit_detail_entity>().Select("*").Where(p => p.gen_measurement_unit_detail_id == dto.uom).Get();
//  //                  //var gen_master_unit_Dto = JsonConvert.DeserializeObject<List<gen_measurement_unit_detail_DTO>>(gen_master_unit.Content).FirstOrDefault();
//  //                  var gen_master_unit_Dto = await _GenMeasurementUnitDetailService.GetAsync(dto.item_id.Value);
//  //                  dto.uomText = gen_master_unit_Dto.unit_detail_display;
//  //              }
//  //              ret.details = details;


//  //              //end

//  //              return ret;
//  //          }
//  //          catch (Exception ex)
//  //          {
//  //              throw (ex);
//  //          }


//  //      }
//    }

//}

