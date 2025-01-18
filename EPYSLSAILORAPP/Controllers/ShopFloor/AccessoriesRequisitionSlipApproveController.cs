using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Merchandiser;
using EPYSLSAILORAPP.Application.Interface.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Enum;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services;

using EPYSLSAILORAPP.Models.MCD;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Collections.Generic;
using System.Security.Claims;
//using System.Web.WebPages.Html;
using Web.Core.Frame.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using IFabricRequisitionService = EPYSLSAILORAPP.Application.Interface.Merchandiser.IFabricRequisitionService;



namespace EPYSLSAILORAPP.Controllers.ShopFloor
{
    public class AccessoriesRequisitionSlipApproveController : ExtendedBaseController
    {
        private readonly ILogger<AccessoriesRequisitionSlipApproveController> _logger;

        private readonly ITranMcdRequisitionSlipService _TranMcdRequisitionSlipService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenUnitService _GenUnitService;
        private readonly IGenItemStructureGroupService _GenItemStructureGroupService;
        private readonly IGenItemStructureGroupSubService _IGenItemStructureGroupSubService;

        private readonly IConfiguration _configuration;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside FabricRequisitionSlipController !");
            return View();
        }

        public AccessoriesRequisitionSlipApproveController(IGenItemStructureGroupSubService IGenItemStructureGroupSubService,
            IGenItemStructureGroupService GenItemStructureGroupService,
           IMapper mapper, ILogger<AccessoriesRequisitionSlipApproveController> logger, IHttpContextAccessor contextAccessor,
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
        public async Task<IActionResult> AccessoriesRequisitionSlipApproveLanding()
        {

            return View("~/Views/ShopFloor/AccessoriesRequisitionSlipApprove/AccessoriesRequisitionSlipApproveLanding.cshtml");
        }


        [HttpGet]
        public async Task<IActionResult> AddFabricRequsitionSlipNew()
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

            // List<gen_warehouse_floor_DTO> WarehousefloorName = await _TranMcdReceiveService.GetWarehouseFloor();
            // ViewBag.WarehousefloorName = WarehousefloorName.ToList()
            //.Select(a =>
            //         new SelectListItem
            //         {
            //             Text = a.floor_name.ToString(),
            //             Value = a.gen_warehouse_floor_id.ToString(),
            //         }
            //         ).ToList();



            
            var ret = true;

           
            {
               
                model.requisition_by = sec_object.userid.Value;
            }
            //ViewBag.requisitionBy = list.FirstOrDefault().requisition_by;
            return PartialView("~/Views/ShopFloor/FabricRequisitionSlip/_FabricRequsitionSlipNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> AddNewFabric(string techPackId, string gen_item_structure_group_sub_id)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
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

            // ViewBag.item_structure_group_id = Convert.ToInt32(item_structure_group_id);

            return PartialView("~/Views/ShopFloor/FabricRequisitionSlip/_FabricDetailsList.cshtml", list);

        }

        [HttpGet]
        public async Task<IActionResult> AddNewFabricEdit(string techPackId, string gen_item_structure_group_sub_id)//(string product_id,string no_of_style,string style_code,string style_product,string range_plan_qnty)
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

            // ViewBag.item_structure_group_id = Convert.ToInt32(item_structure_group_id);

            return PartialView("~/Views/ShopFloor/FabricRequisitionSlip/_FabricDetailsListEdit.cshtml", list);

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
                            "<button type='button' onclick='EditFabricRequisitionSlip(this)' style='font-weight: 600;background: green;color: #fff;' class='btn btn-secondary btnView'  requisition_slip_id='" + clsUtil.EncodeString(t.requisition_slip_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

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

        [HttpGet]
        public async Task<IActionResult> FabricRequisitionSlipEdit(string requisition_slip_id)
        {

            string decoded_requisition_slip_id = clsUtil.DecodeString(requisition_slip_id);

            var data = await _rpc_db_service.Get_master_detail_fabric_requisition_slipAsync(Convert.ToInt64(decoded_requisition_slip_id));
            tran_mcd_requisition_slip_DTO model = JsonConvert.DeserializeObject<tran_mcd_requisition_slip_DTO>(JsonConvert.SerializeObject(data));
            model.details = JsonConvert.DeserializeObject<List<tran_mcd_requisition_slip_detail_DTO>>(data.tran_mcd_requisition_slip_detail_list);

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(Convert.ToInt64(model.gen_item_structure_group_id));
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();

            return PartialView("~/Views/ShopFloor/FabricRequisitionSlipApprove/_TranFabricRequisitionSlipApproveView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> UpdateForApproval([FromBody] tran_mcd_requisition_slip_DTO request)
        {
            var ret = true;

            

            
            {
                

                request.added_by = sec_object.userid.Value;



                request.updated_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
            }
            try
            {

                ret = await _TranMcdRequisitionSlipService.ApproveAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Data Approved Successful" : "Data Saving Failed")
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

        #region FinishingRequisitionSlipApprove

        [HttpGet]
        public async Task<IActionResult> FinishingRequisitionSlipApproveLanding()
        {

            return View("~/Views/ShopFloor/AccessoriesRequisitionSlipApprove/FinishingRequisitionSlipApproveLanding.cshtml");
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
                            "<button type='button' onclick='EditFinishingRequisitionSlip(this)' style='font-weight: 600;background: green;color: #fff;' class='btn btn-secondary btnView'  requisition_slip_id='" + clsUtil.EncodeString(t.requisition_slip_id.ToString()) + "'>View</button>" +
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
                            "<button type='button' onclick='ViewFabricRequisitionSlip(this)' style='font-weight: 600;background: green;color: #fff;' class='btn btn-secondary btnView'  requisition_slip_id='" + clsUtil.EncodeString(t.requisition_slip_id.ToString()) + "'>View</button>" +
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

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(Convert.ToInt64(model.gen_item_structure_group_id));
            ViewBag.SubCategoryList = SubCategoryList.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.sub_group_name.ToString(),
                        Value = a.gen_item_structure_group_sub_id.ToString(),
                    }
                    ).ToList();

            return PartialView("~/Views/ShopFloor/AccessoriesRequisitionSlipApprove/_TranFabricRequisitionSlipApproveView.cshtml", model);

        }

        #endregion FinishingRequisitionSlipApprove

    }
}
