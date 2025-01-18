using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.CustomIdentityManagers;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class DraftPRController : BaseController
    {
        private readonly ILogger<DraftPRController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITranDraftPurchaseRequisitionService _DraftPRService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly Application.Interface.Tran_DesignMgt.ITrantechpackService _trantechpackService;
        private readonly ApplicationUserManager<owin_user_DTO> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
		private readonly IGenunitService _GenunitService;
		private HelperService objHelperService;
		private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenItemMasterService _GenItemMasterService;
        private readonly IGenitemstructuregroupService _GenitemstructuregroupService;
        private readonly IGenitemstructuregroupsubService _GenitemstructuregroupsubService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside Accessories Requisition Controller !");
            return View();
        }

        public DraftPRController(IGenitemstructuregroupsubService GenitemstructuregroupsubService,
  IGenitemstructuregroupService GenitemstructuregroupService, ApplicationUserManager<owin_user_DTO> userManager, IGenSupplierInformationService GenSupplierInformationService, Application.Interface.Tran_DesignMgt.ITrantechpackService trantechpackService,
           IGenItemMasterService GenItemMasterService, IMapper mapper, ILogger<DraftPRController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration, IGenunitService genunitService,
            ITranDraftPurchaseRequisitionService TranDraftPurchaseRequisition, IGenSeasonEventConfigurationService genseasoneventconfigurationservice,
            IRPCDbService rpc_db_service)
        {
            _GenitemstructuregroupService = GenitemstructuregroupService;
            _GenitemstructuregroupsubService = GenitemstructuregroupsubService;
            _mapper = mapper;
            _userManager = userManager;
            _GenItemMasterService = GenItemMasterService;
            _logger = logger;
            _trantechpackService = trantechpackService;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _DraftPRService = TranDraftPurchaseRequisition;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            objHelperService = new HelperService(_contextAccessor);
			_GenunitService = genunitService;
			_contextAccessor = contextAccessor;
			_GenSupplierInformationService = GenSupplierInformationService;
		}

        [HttpGet]
        public async Task<IActionResult> DraftPRLanding()
        {
            GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();

            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                if (objFilter == null)
                {
                    return RedirectToAction("Index", "Dashboard",new { load_filter = "1" });
                }
            }
            else
            {
                return RedirectToAction("Index", "Dashboard",new { load_filter = "1" });
            }
            return View("~/Views/MerchandiserMgt/DraftPR/DraftPRLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> DraftAccesoriesPRNew()
        {
            GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();

            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                if (objFilter == null)
                {
                    return RedirectToAction("Index", "Dashboard",new { load_filter = "1" });
                }
            }
            else
            {
                return RedirectToAction("Index", "Dashboard",new { load_filter = "1" });
            }
            SecurityCapsule sec_object = new SecurityCapsule();

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
            }
			tran_draft_purchase_requisition_DTO model = new tran_draft_purchase_requisition_DTO();
			model.event_id = objFilter.event_id;

			model.merchandiser_id = sec_object.userid;

			model.merchandiser_name = sec_object.username;


			List<gen_item_structure_group_sub_DTO> SubCategorySewingList = await _GenitemstructuregroupsubService.GetAllItemStructureSubGroups(2);
			List<gen_item_structure_group_DTO> GroupList = await _GenitemstructuregroupService.GetAllAccessoriesGroups();
			List<gen_item_structure_group_sub_DTO> SubCategoryFinishinList = await _GenitemstructuregroupsubService.GetAllItemStructureSubGroups(3);
			var unit_list = await _GenunitService.GetAllAsync();
			ViewBag.SubCategorySewingList = SubCategorySewingList.ToList()
		   .Select(a =>
					new SelectListItem
					{
						Text = a.sub_group_name.ToString(),
						Value = a.gen_item_structure_group_sub_id.ToString(),
					}
					).ToList();
			ViewBag.SubCategoryFinishinList = SubCategoryFinishinList.ToList()
		   .Select(a =>
					new SelectListItem
					{
						Text = a.sub_group_name.ToString(),
						Value = a.gen_item_structure_group_sub_id.ToString(),
					}
					).ToList();
			ViewBag.GroupList = GroupList.ToList()
		   .Select(a =>
					new SelectListItem
					{
						Text = a.structure_group_name.ToString(),
						Value = a.item_structure_group_id.ToString(),
					}
					).ToList();
			ViewBag.DeliveryUnits = unit_list.ToList()
		   .Select(a =>
					new SelectListItem
					{
						Text = a.unit_name.ToString(),
						Value = a.gen_unit_id.ToString(),
					}
					).ToList();
			return PartialView("~/Views/MerchandiserMgt/DraftPR/_DraftAccessoriesRequisitionNew.cshtml", model);

        }
        [HttpGet]
        public async Task<IActionResult> DraftFabricPRNew()
        {
            GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();

            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                if (objFilter == null)
                {
                    return RedirectToAction("Index", "Dashboard",new { load_filter = "1" });
                }
            }
            else
            {
                return RedirectToAction("Index", "Dashboard",new { load_filter = "1" });
            }
            SecurityCapsule sec_object = new SecurityCapsule();

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
            }
            tran_draft_purchase_requisition_DTO model = new tran_draft_purchase_requisition_DTO();
            model.event_id = objFilter.event_id;
            model.item_structure_group_id = 1;

            model.merchandiser_id = sec_object.userid;

            model.merchandiser_name = sec_object.username;

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _GenitemstructuregroupsubService.GetAllItemStructureSubGroups(model.item_structure_group_id);
            var unit_list = await _GenunitService.GetAllAsync();
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();
            ViewBag.DeliveryUnits = unit_list.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.unit_name.ToString(),
                        Value = a.gen_unit_id.ToString(),
                    }
                    ).ToList();
            return PartialView("~/Views/MerchandiserMgt/DraftPR/_DraftFabricRequisitionNew.cshtml", model);

        }
        [HttpGet]
        public async Task<IActionResult> DraftPREdit(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);
			var unit_list = await _GenunitService.GetAllAsync();
			ViewBag.DeliveryUnits = unit_list.ToList()
	           .Select(a =>
				        new SelectListItem
				        {
					        Text = a.unit_name.ToString(),
					        Value = a.gen_unit_id.ToString(),
				        }
				        ).ToList();
			tran_draft_purchase_requisition_DTO model = new tran_draft_purchase_requisition_DTO();

            model = await _DraftPRService.GetSingleAsync(Convert.ToInt64(decoded_pr_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            var techpack = await _trantechpackService.GetAsync(Convert.ToInt64(model.techpack_id));
        
            model.ddlsupplier_info = new dropdown_entity() { 
            id=model.supplier_id.ToString(),
            text= sup.name.ToString()
		};
            model.ddlTechpack_info = new dropdown_TechpackEntity()
            {
                tran_techpack_id = model.techpack_id.ToString(),
                techpack_number = techpack.techpack_number.ToString()
            };
            return PartialView("~/Views/MerchandiserMgt/DraftPR/_DraftPREdit.cshtml", model);

        }

         [HttpGet]
        public async Task<IActionResult> DraftPRView(string pr_id)
        {

            string decoded_pr_id = clsUtil.DecodeString(pr_id);

            tran_draft_purchase_requisition_DTO model = new tran_draft_purchase_requisition_DTO();

            model = await _DraftPRService.GetSingleAsync(Convert.ToInt64(decoded_pr_id));
			var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            var du=  await _GenunitService.GetAsync(Convert.ToInt64(model.delivery_unit_id));
            model.supplier_name=sup.name.ToString();
            model.delivery_unit_name=du.FirstOrDefault().unit_name;

			return PartialView("~/Views/MerchandiserMgt/DraftPR/_DraftPRView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetDraftPRData(DtParameters request)
        {
            GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();

            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                if (objFilter == null)
                {
                    return RedirectToAction("Index", "Dashboard",new { load_filter = "1" });
                }
            }
            else
            {
                return RedirectToAction("Index", "Dashboard",new { load_filter = "1" });
            }

            var records = await _DraftPRService.GetAllDraftTranPurchaseRequisitionAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.dpr_id,
                            t.dpr_no,
                            t.dpr_date,
                            t.delivery_date,
                            t.event_title,
                            t.designer_name,
                            t.unit_name,
                            t.supplier_name,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditDraftPR(this)' class='btn btn-primary btnEdit'  pr_id='" + clsUtil.EncodeString(t.dpr_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='ViewDraftPR(this)' class='btn btn-warning btnView'  pr_id='" + clsUtil.EncodeString(t.dpr_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
							"<button type='button' onclick='DeleteDraftPR(\""+ clsUtil.EncodeString(t.dpr_id.ToString()) + "\")' class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
							"</div>"
                       }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> SaveAccessoriesRequisition([FromBody] tran_draft_purchase_requisition_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.dpr_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }
               
            }

            try
            {
                var model = JsonConvert.DeserializeObject<tran_draft_purchase_requisition_entity>(JsonConvert.SerializeObject(request));
                model.gen_item_structure_group_id = request.item_structure_group_id;
                List<tran_draft_purchase_requisition_dtl_entity> detail = JsonConvert.DeserializeObject<List<tran_draft_purchase_requisition_dtl_entity>>(JsonConvert.SerializeObject(request.details));

                
                ret = await _DraftPRService.SaveAsync(model,request.List_Files, detail);
                
                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Updated Successfully" : "Data Saving Failed")
                });
            }
            catch (Exception ex)
            {
                ret = false;
               
                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(ex.ToString());
                }

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                     Status_Result = (ret == true ? "Operation Failed" : ex.Message)
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> SaveFabricRequisition([FromBody] tran_draft_purchase_requisition_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.dpr_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }

            }

            try
            {
                var model = JsonConvert.DeserializeObject<tran_draft_purchase_requisition_entity>(JsonConvert.SerializeObject(request));
                model.gen_item_structure_group_id = 1;
                List<tran_draft_purchase_requisition_dtl_entity> detail = JsonConvert.DeserializeObject<List<tran_draft_purchase_requisition_dtl_entity>>(JsonConvert.SerializeObject(request.details));


                ret = await _DraftPRService.SaveAsync(model, request.List_Files, detail);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Updated Successfully" : "Data Saving Failed")
                });
            }
            catch (Exception ex)
            {
                ret = false;

                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(ex.ToString());
                }

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Operation Failed" : ex.Message)
                });
            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateDraftPR([FromBody] tran_draft_purchase_requisition_DTO request)
        {
            var ret = true;

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

              

                request.updated_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
            }

            try
            {
                var model = JsonConvert.DeserializeObject<tran_draft_purchase_requisition_entity>(JsonConvert.SerializeObject(request));
                List<tran_draft_purchase_requisition_dtl_entity> detail = JsonConvert.DeserializeObject<List<tran_draft_purchase_requisition_dtl_entity>>(JsonConvert.SerializeObject(request.details));

                ret = await _DraftPRService.UpdateAsync(model, request.List_Files, detail, request.DeleteList);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Updated Successfully" : "Data Saving Failed")
                });
            }
            catch (Exception ex)
            {
                ret = false;
                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(ex.ToString());
                }
                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Operation Failed" : ex.Message)
                });
            }

          
        }
		
		

         [HttpPost]
         public async Task<IActionResult> DeleteDraftPR([FromBody] tran_draft_purchase_requisition_DTO request)
  {
      var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

      List<Claim> listClaims = (List<Claim>)claim.Claims;

      var ret = true;

      if (listClaims.Exists(c => c.Type == "secobject"))
      {
          var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

          SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

          request.added_by = sec_object.userid.Value;

          request.date_added = DateTime.Now;
          
      }

      try
      {

          string decoded_pr_id = clsUtil.DecodeString(request.strMasterID);

          request.dpr_id = Convert.ToInt64(decoded_pr_id);

          ret = await _DraftPRService.DeleteAsync(request.dpr_id.Value);

          return Json(new ResultEntity
          {
              Status_Code = (ret == true ? "200" : "400"),
              Status_Result = (ret == true ? "Data Deleted Successfully" : "Data Deletion Failed")
          });
      }
      catch (Exception ex)
      {
          ret = false;

          using (LogContext.PushProperty("SpecialLogType", true))
          {
              _logger.LogError(ex.ToString());
          }

          return Json(new ResultEntity
          {
              Status_Code = (ret == true ? "200" : "400"),
              Status_Result = (ret == true ? "Operation Failed" : ex.Message)
          });
      }

  }


		[HttpGet]
		public async Task<IActionResult> AddNewAccessories(string item_structure_group_id, string gen_item_structure_group_sub_id, string measurement_unit_id)
		{
			SecurityCapsule sec_object = new SecurityCapsule();

			var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

			List<Claim> listClaims = (List<Claim>)claim.Claims;

			if (listClaims.Exists(c => c.Type == "secobject"))
			{
				var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

				sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
			}


			

			var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(measurement_unit_id));

			ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
			{
				Text = p.unit_detail_display,
				Value = p.gen_measurement_unit_detail_id.ToString(),
				Selected = p.gen_measurement_unit_detail_id.ToString() == "86",
			}).ToList();





            fabric_mapping_segment_details_for_fabricbooking_dto model = new fabric_mapping_segment_details_for_fabricbooking_dto();
            model.gen_item_structure_group_id = Convert.ToInt32(item_structure_group_id);
            model.gen_item_structure_group_sub_id = Convert.ToInt32(gen_item_structure_group_sub_id);
            model.mapping_item = await _rpc_db_service.GetAllsp_get_mapped_segment_by_gen_item_structure_group_sub_idAsync(Convert.ToInt32(gen_item_structure_group_sub_id));


            return PartialView("~/Views/MerchandiserMgt/DraftPR/_AddAccessoriesDetails.cshtml", model);

		}

		[HttpGet]
		public async Task<JsonResult> AddAccessoriesDetailsMaster(string input)
		{
			SecurityCapsule sec_object = new SecurityCapsule();

			var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

			List<Claim> listClaims = (List<Claim>)claim.Claims;

			if (listClaims.Exists(c => c.Type == "secobject"))
			{
				var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

				sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
			}
			var AccessoriesDetetail= JsonConvert.DeserializeObject<List<gen_item_master_DTO>>(input);
            long InsertedID = 0;

			 InsertedID =await  _GenItemMasterService.CheckAndSaveItemMasterAsync(AccessoriesDetetail.FirstOrDefault(), sec_object.userid);


			return Json(InsertedID);


		}

	}
}

