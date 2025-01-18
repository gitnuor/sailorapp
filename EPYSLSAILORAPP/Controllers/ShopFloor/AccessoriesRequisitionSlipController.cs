using AutoMapper;
using EPYSLSAILORAPP.Application.Interface.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using EPYSLSAILORAPP.SystemServices;
using EPYSLSAILORAPP.Domain.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using BDO.Core.Base;
using System.Security.Claims;
using Newtonsoft.Json;
using IFabricRequisitionService = EPYSLSAILORAPP.Application.Interface.Merchandiser.IFabricRequisitionService;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Services;
using Serilog.Context;
using EPYSLSAILORAPP.Domain.Enum;
using Web.Core.Frame.Helpers;
using Newtonsoft.Json.Linq;

namespace EPYSLSAILORAPP.Controllers.ShopFloor
{
    public class AccessoriesRequisitionSlipController : ExtendedBaseController
    {
        private readonly ILogger<AccessoriesRequisitionSlipController> _logger;

        private readonly IConfiguration _configuration;

        private readonly ITranMcdRequisitionSlipService _TranMcdRequisitionSlipService;
       
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenUnitService _GenUnitService;
        private readonly IGenItemStructureGroupService _GenItemStructureGroupService;
        private readonly IGenItemStructureGroupSubService _IGenItemStructureGroupSubService;

        private HelperService objHelperService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside AccessoriesRequisitionSlipController !");
            return View();
        }


        public AccessoriesRequisitionSlipController(
              IGenItemStructureGroupSubService IGenItemStructureGroupSubService,
              IGenItemStructureGroupService GenItemStructureGroupService,
          IMapper mapper, ILogger<AccessoriesRequisitionSlipController> logger, IHttpContextAccessor contextAccessor,
           ITranMcdRequisitionSlipService TranMcdRequisitionSlipService, IGenSupplierInformationService GenSupplierInformationService, IGenUnitService GenUnitService,
           IRPCDbService rpc_db_service, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranMcdRequisitionSlipService = TranMcdRequisitionSlipService;
           
            _GenSupplierInformationService = GenSupplierInformationService;
            _GenUnitService = GenUnitService;

            _GenItemStructureGroupService = GenItemStructureGroupService;
            _IGenItemStructureGroupSubService = IGenItemStructureGroupSubService;

            

            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> AccessoriesRequisitionSlipLandingSewing()
        {

            return View("~/Views/ShopFloor/AccessoriesRequisitionSlipSewing/AccessoriesRequisitionSlipLanding.cshtml");
        }

       
        [HttpGet]
        public async Task<IActionResult> AddAccessoriesRequisitionSlipNew()
        {
            tran_mcd_requisition_slip_DTO model = new tran_mcd_requisition_slip_DTO();

            model.gen_item_structure_group_id = 1;

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(Convert.ToInt64(model.gen_item_structure_group_id));
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();

            List<gen_item_structure_group_sub_DTO> SubCategorySewingList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(2);
            List<gen_item_structure_group_DTO> GroupList = await _GenItemStructureGroupService.GetAllAccessoriesGroups();
            List<gen_item_structure_group_sub_DTO> SubCategoryFinishinList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(3);

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


            
            var ret = true;

            
            {
                

                
                model.requisition_by = sec_object.userid.Value;
            }
            //ViewBag.requisitionBy = list.FirstOrDefault().requisition_by;
            return PartialView("~/Views/ShopFloor/AccessoriesRequisitionSlipSewing/_AccessoriesRequisitionSlipNew.cshtml", model);

        }



        [HttpGet]
        public async Task<IActionResult> AddNewAccessories(string techPackId, string gen_item_structure_group_sub_id)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {
          

            
           

            var list = await _rpc_db_service.GetAllsp_get_techPack_by_gen_item_structure_group_sub_idAsync(Convert.ToInt32(techPackId), Convert.ToInt32(gen_item_structure_group_sub_id));

            // var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(measurement_unit_id));

            //ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            //{
            //    Text = p.unit_detail_display,
            //    Value = p.gen_measurement_unit_detail_id.ToString(),
            //    Selected = p.gen_measurement_unit_detail_id.ToString() == default_measurement_unit_detail_id ? true : false,
            //}).ToList();

            // var list_placement = JsonConvert.DeserializeObject<List<style_placement_information_DTO>>(list[0].styleplacementinformation_list);

            //ViewBag.list_placement = list_placement.Select(p => new SelectListItem
            //{
            //    Text = p.placement,
            //    Value = p.style_placement_information_id.ToString(),
            //}).ToList();

            ViewBag.gen_item_structure_group_sub_id = Convert.ToInt32(gen_item_structure_group_sub_id);
            ViewBag.requisition_quantity = list.ToList().Select(x => x.total_requisition_quantity);

            // ViewBag.item_structure_group_id = Convert.ToInt32(item_structure_group_id);

            return PartialView("~/Views/ShopFloor/AccessoriesRequisitionSlipSewing/_AccessoriesDetailsList.cshtml", list);

        }

        [HttpPost]
        public async Task<IActionResult> SaveSewingRequisitionSlip([FromBody] tran_mcd_requisition_slip_DTO request)
        {
           
            var ret = true;
           
            try
            {
               
                {
                   
                    if (request.requisition_slip_id == null)
                    {
                        request.added_by = sec_object.userid.Value;
                        request.date_added = DateTime.Now;
                        request.fiscal_year_id = Fiscal_Year_ID;
                        request.event_id = Event_ID;
                    }
                }
               
                
                {
                    
                    request.details.ForEach(n =>
                    {
                        n.tran_type = 1;
                        n.added_by = sec_object.userid.Value;
                        n.date_added = DateTime.Now;
                    });
                }

                ret = await _TranMcdRequisitionSlipService.SaveAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret==true ? "200" : "400"),
                    data ="0",
                    Status_Result = (ret == true ? "Data Saved Successfully." : "Data Saving Failed")
                });
            }
            catch (Exception ex)
            {
                
                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(ex.ToString());
                }

                return Json(new ResultEntity
                {
                    Status_Code = "400", // Since the operation failed due to an exception, set the status code to 400
                    Status_Result = "Operation Failed: " + ex.Message //
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> ProposeForApproval(Int64 Id)
        {
            try
            {
                var ret = true;
                tran_mcd_requisition_slip_DTO model = new tran_mcd_requisition_slip_DTO();
                model.requisition_slip_id = Id;

                ret = await _TranMcdRequisitionSlipService.ProposedAsync(model);
                return Json(new ResultEntity
                {
                    Status_Code = "200",
                });
            }
            catch (Exception ex)
            {
                return Json(new { Status_Code = "500", Status_Result = "Failed to propose for approval" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetSewingRequisitionSlipData(MCD_DtParameters request)
        {
            Int64 requisitionSlipid = 0;
            int genGroupId = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllJoined_FabricRequisitionSlipListAsync(request.Start , request.Length, ActionType.getAll.ToString(), requisitionSlipid, genGroupId, request.fiscal_year_id, request.event_id, request.group_id, request.sub_group_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.requisition_slip_id,
                            t.requisition_slip_no,
                            t.requisition_date,
                            t.requisition_by,
                            t.techpack_id,
                            t.gen_item_structure_group_sub_id,
                            t.techpack_number,
                            t.name,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditSewingRequisitionSlip(this)' style='font-weight: 600;background: green;color: #fff;' class='btn btn-secondary btnView'  requisition_slip_id='" + clsUtil.EncodeString(t.requisition_slip_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetSewingRequisitionSlipProposedData(MCD_DtParameters request)
        {
            Int64 requisitionSlipid = 0;
            int genGroupId = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllJoined_FabricRequisitionSlipProposedListAsync(request.Start , request.Length, ActionType.getAll.ToString(), requisitionSlipid, genGroupId, request.fiscal_year_id, request.event_id, request.group_id, request.sub_group_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.requisition_slip_id,
                            t.requisition_slip_no,
                            t.requisition_date,
                            t.requisition_by,
                            t.techpack_id,
                            t.gen_item_structure_group_sub_id,
                            t.techpack_number,
                            t.name,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewFabricRequisitionSlip(this)' style='font-weight: 600;background: green;color: #fff;' class='btn btn-secondary btnView'  requisition_slip_id='" + clsUtil.EncodeString(t.requisition_slip_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> SewingRequisitionSlipEdit(string requisition_slip_id)
        {

            string decoded_requisition_slip_id = clsUtil.DecodeString(requisition_slip_id);

            var data = await _rpc_db_service.Get_master_detail_fabric_requisition_slipAsync(Convert.ToInt64(decoded_requisition_slip_id));
            tran_mcd_requisition_slip_DTO model = JsonConvert.DeserializeObject<tran_mcd_requisition_slip_DTO>(JsonConvert.SerializeObject(data));
            model.details = JsonConvert.DeserializeObject<List<tran_mcd_requisition_slip_detail_DTO>>(data.tran_mcd_requisition_slip_detail_list);

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(Convert.ToInt64(model.gen_item_structure_group_sub_id));
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();

            return PartialView("~/Views/ShopFloor/AccessoriesRequisitionSlipSewing/_TranAccessoriesRequisitionSlipEdit.cshtml", model);

        }

        [HttpPost]
        public async Task<IActionResult> GetSewingRequisitionSlipApprovedData(MCD_DtParameters request)
        {
            Int64 requisitionSlipid = 0;
            int genGroupId = 2;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllJoined_FabricRequisitionSlipApprovedListAsync(request.Start , request.Length, ActionType.getAll.ToString(), requisitionSlipid, genGroupId, request.fiscal_year_id, request.event_id, request.group_id, request.sub_group_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.requisition_slip_id,
                            t.requisition_slip_no,
                            t.requisition_date,
                            t.requisition_by,
                            t.techpack_id,
                            t.gen_item_structure_group_sub_id,
                            t.techpack_number,
                            t.name,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewFabricRequisitionSlip(this)' style='font-weight: 600;background: green;color: #fff;' class='btn btn-secondary btnView'  requisition_slip_id='" + clsUtil.EncodeString(t.requisition_slip_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        #region AccessoriesRequisitionSlipFinishing
        [HttpGet]
        public async Task<IActionResult> AccessoriesRequisitionSlipLandingFinishing()
        {

            return View("~/Views/ShopFloor/AccessoriesRequisitionSlipFinishing/AccessoriesRequisitionSlipLandingFinishing.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> AddAccessoriesRequisitionSlipFinishingNew()
        {
            tran_mcd_requisition_slip_DTO model = new tran_mcd_requisition_slip_DTO();

            model.gen_item_structure_group_id = 1;

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(Convert.ToInt64(model.gen_item_structure_group_id));
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();

            List<gen_item_structure_group_sub_DTO> SubCategorySewingList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(2);
            List<gen_item_structure_group_DTO> GroupList = await _GenItemStructureGroupService.GetAllAccessoriesGroups();
            List<gen_item_structure_group_sub_DTO> SubCategoryFinishinList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(3);

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


            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            List<Claim> listClaims = (List<Claim>)claim.Claims;
            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
                model.requisition_by = sec_object.userid.Value;
            }
            //ViewBag.requisitionBy = list.FirstOrDefault().requisition_by;
            return PartialView("~/Views/ShopFloor/AccessoriesRequisitionSlipFinishing/_AccessoriesRequisitionSlipNewFinishing.cshtml", model);

        }



        [HttpGet]
        public async Task<IActionResult> AddNewAccessoriesFinishing(string techPackId, string gen_item_structure_group_sub_id)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
        {
            

            

            var list = await _rpc_db_service.GetAllsp_get_techPack_by_gen_item_structure_group_sub_idAsync(Convert.ToInt32(techPackId), Convert.ToInt32(gen_item_structure_group_sub_id));

            // var list_measurement = await _rpc_db_service.GetAllsp_get_measurement_unit_details_by_masteridAsync(Convert.ToInt64(measurement_unit_id));

            //ViewBag.list_measurement = list_measurement.Select(p => new SelectListItem
            //{
            //    Text = p.unit_detail_display,
            //    Value = p.gen_measurement_unit_detail_id.ToString(),
            //    Selected = p.gen_measurement_unit_detail_id.ToString() == default_measurement_unit_detail_id ? true : false,
            //}).ToList();

            // var list_placement = JsonConvert.DeserializeObject<List<style_placement_information_DTO>>(list[0].styleplacementinformation_list);

            //ViewBag.list_placement = list_placement.Select(p => new SelectListItem
            //{
            //    Text = p.placement,
            //    Value = p.style_placement_information_id.ToString(),
            //}).ToList();

            ViewBag.gen_item_structure_group_sub_id = Convert.ToInt32(gen_item_structure_group_sub_id);
            ViewBag.requisition_quantity = list.ToList().Select(x=> x.requisition_quantity);


            // ViewBag.item_structure_group_id = Convert.ToInt32(item_structure_group_id);

            return PartialView("~/Views/ShopFloor/AccessoriesRequisitionSlipFinishing/_AccessoriesDetailsListFinishing.cshtml", list);

        }

        [HttpPost]
        public async Task<IActionResult> SaveFinishingRequisitionSlip([FromBody] tran_mcd_requisition_slip_DTO request)
        {
            

            var ret = true;
           
            try
            {
                
                {
                    

                    if (request.requisition_slip_id == null)
                    {
                        request.added_by = sec_object.userid.Value;
                        request.date_added = DateTime.Now;
                        request.fiscal_year_id = Fiscal_Year_ID;
                        request.event_id = Event_ID;
                    }
                }
                //var model = JsonConvert.DeserializeObject<tran_mcd_requisition_slip_entity>(JsonConvert.SerializeObject(request));
                //var details = JsonConvert.DeserializeObject<List<tran_mcd_requisition_slip_detail_entity>>(JsonConvert.SerializeObject(request.details));
                
               
                {
                   

                    request.details.ForEach(n =>
                    {
                        n.tran_type = 1;
                        n.added_by = sec_object.userid.Value;
                        n.date_added = DateTime.Now;
                    });
                }
                request.requisition_slip_detail = JArray.Parse(JsonConvert.SerializeObject(request.details)).ToString();

                ret = await _TranMcdRequisitionSlipService.SaveAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret==true ? "200" : "400"),
                    data = "0",
                    Status_Result = (ret == true ? "Data Saved Successfully" : "Data Saving Failed")
                });
            }
            catch (Exception ex)
            {
                
                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    _logger.LogError(ex.ToString());
                }

                return Json(new ResultEntity
                {
                    Status_Code = "400", // Since the operation failed due to an exception, set the status code to 400
                    Status_Result = "Operation Failed: " + ex.Message //
                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> ProposeForApprovalFinishing(Int64 Id)
        {
            try
            {
                var ret = true;
                tran_mcd_requisition_slip_DTO model = new tran_mcd_requisition_slip_DTO();
                model.requisition_slip_id = Id;

                ret = await _TranMcdRequisitionSlipService.ProposedAsync(model);
                return Json(new ResultEntity
                {
                    Status_Code = "200",
                });
            }
            catch (Exception ex)
            {
                return Json(new { Status_Code = "500", Status_Result = "Failed to propose for approval" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetFinishingRequisitionSlipData(MCD_DtParameters request)
        {
            Int64 requisitionSlipid = 0;
            int genGroupId = 3;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllJoined_FabricRequisitionSlipListAsync(request.Start , request.Length, ActionType.getAll.ToString(), requisitionSlipid, genGroupId, request.fiscal_year_id, request.event_id, request.group_id, request.sub_group_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.requisition_slip_id,
                            t.requisition_slip_no,
                            t.requisition_date,
                            t.requisition_by,
                            t.techpack_id,
                            t.gen_item_structure_group_sub_id,
                            t.techpack_number,
                            t.name,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewFinishingRequisitionSlip(this)' style='font-weight: 600;background: green;color: #fff;' class='btn btn-secondary btnView'  requisition_slip_id='" + clsUtil.EncodeString(t.requisition_slip_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetFinishingRequisitionSlipProposedData(MCD_DtParameters request)
        {
            Int64 requisitionSlipid = 0;
            int genGroupId = 3;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllJoined_FabricRequisitionSlipProposedListAsync(request.Start , request.Length, ActionType.getAll.ToString(), requisitionSlipid, genGroupId, request.fiscal_year_id, request.event_id, request.group_id, request.sub_group_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.requisition_slip_id,
                            t.requisition_slip_no,
                            t.requisition_date,
                            t.requisition_by,
                            t.techpack_id,
                            t.gen_item_structure_group_sub_id,
                            t.techpack_number,
                            t.name,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewFinishingRequisitionSlip(this)' style='font-weight: 600;background: green;color: #fff;' class='btn btn-secondary btnView'  requisition_slip_id='" + clsUtil.EncodeString(t.requisition_slip_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetFinishingRequisitionSlipApprovedData(MCD_DtParameters request)
        {
            Int64 requisitionSlipid = 0;
            int genGroupId = 3;


            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _rpc_db_service.GetAllJoined_FabricRequisitionSlipApprovedListAsync(request.Start , request.Length, ActionType.getAll.ToString(), requisitionSlipid, genGroupId, request.fiscal_year_id, request.event_id, request.group_id, request.sub_group_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.requisition_slip_id,
                            t.requisition_slip_no,
                            t.requisition_date,
                            t.requisition_by,
                            t.techpack_id,
                            t.gen_item_structure_group_sub_id,
                            t.techpack_number,
                            t.name,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewFinishingRequisitionSlip(this)' style='font-weight: 600;background: green;color: #fff;' class='btn btn-secondary btnView'  requisition_slip_id='" + clsUtil.EncodeString(t.requisition_slip_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> FinishingRequisitionSlipEdit(string requisition_slip_id)
        {

            string decoded_requisition_slip_id = clsUtil.DecodeString(requisition_slip_id);

            var data = await _rpc_db_service.Get_master_detail_fabric_requisition_slipAsync(Convert.ToInt64(decoded_requisition_slip_id));
            tran_mcd_requisition_slip_DTO model = JsonConvert.DeserializeObject<tran_mcd_requisition_slip_DTO>(JsonConvert.SerializeObject(data));
            model.details = JsonConvert.DeserializeObject<List<tran_mcd_requisition_slip_detail_DTO>>(data.tran_mcd_requisition_slip_detail_list);

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(Convert.ToInt64(model.gen_item_structure_group_sub_id));
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();

            return PartialView("~/Views/ShopFloor/AccessoriesRequisitionSlipFinishing/_TranAccessoriesRequisitionSlipView.cshtml", model);

        }

        #endregion AccessoriesRequisitionSlipFinishing

    }
}
