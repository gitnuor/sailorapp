using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Merchandiser;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Web.Core.Frame.Helpers;
using EPYSLSAILORAPP.Infrastructure.Services;
using Newtonsoft.Json.Linq;

namespace EPYSLSAILORAPP.Controllers
{
    public class OpenPOController : ExtendedBaseController
    {
        private readonly ILogger<OpenPOController> _logger;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly ITranScmPoService _TranScmPoService;
        private readonly IConfiguration _configuration;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IGenUnitService _GenUnitService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private HelperService objHelperService;
        private readonly IGenItemStructureGroupService _GenItemStructureGroupService;

        private readonly IGenTermAndConditionsService _genTermAndConditionsService;

        private readonly IGenItemStructureGroupSubService _IGenItemStructureGroupSubService;


        private readonly Application.Interface.Tran_MerchandisingMgt.IAccessoriesRequisitionService _AccessoriesRequisitionService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside Fabric PO Controller !");
            return View();
        }

        public OpenPOController(IGenItemStructureGroupSubService IGenItemStructureGroupSubService,
  IGenItemStructureGroupService GenItemStructureGroupService,
           IMapper mapper, ILogger<OpenPOController> logger, IHttpContextAccessor contextAccessor, IGenSupplierInformationService GenSupplierInformationService, IConfiguration configuration,
            ITranScmPoService TranScmPoService, IGenUnitService GenUnitService,
            IRPCDbService rpc_db_service, IGenTermAndConditionsService genTermAndConditionsService) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranScmPoService = TranScmPoService;
            _GenUnitService = GenUnitService;
            _GenItemStructureGroupService = GenItemStructureGroupService;
            _IGenItemStructureGroupSubService = IGenItemStructureGroupSubService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            _genTermAndConditionsService = genTermAndConditionsService;
            _GenSupplierInformationService = GenSupplierInformationService;
            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> OpenPOLanding()
        {

            return View("~/Views/SCM/OpenPO/OpenPOLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> FabricPoNew()
        {
            tran_scm_po_DTO model = new tran_scm_po_DTO();

            model.item_structure_group_id = 1;
            model.po_date = DateTime.Now;

            List<gen_item_structure_group_sub_DTO> SubCategoryList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(Convert.ToInt64(model.item_structure_group_id));
            var unit_list = await _GenUnitService.GetAllAsync();
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

            List<gen_term_and_conditions_DTO> termConditionList = await _genTermAndConditionsService.GetAllAsync();

            ViewBag.termConditionList =
                termConditionList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.term_condition_name.ToString(),
                        Value = a.gen_term_and_conditions_id.ToString()
                    }).ToList();


            return PartialView("~/Views/SCM/OpenPO/_OpenPOFabricNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> AccessoriesPoNew()
        {
            tran_scm_po_DTO model = new tran_scm_po_DTO();


            List<gen_item_structure_group_sub_DTO> SubCategorySewingList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(2);
            List<gen_item_structure_group_DTO> GroupList = await _GenItemStructureGroupService.GetAllAccessoriesGroups();
            List<gen_item_structure_group_sub_DTO> SubCategoryFinishinList = await _IGenItemStructureGroupSubService.GetAllItemStructureSubGroups(3);
            var unit_list = await _GenUnitService.GetAllAsync();

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

            List<gen_term_and_conditions_DTO> termConditionList = await _genTermAndConditionsService.GetAllAsync();

            ViewBag.termConditionList =
                termConditionList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.term_condition_name.ToString(),
                        Value = a.gen_term_and_conditions_id.ToString()
                    }).ToList();


            return PartialView("~/Views/SCM/OpenPO/_OpenAccessoriesPONew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> OpenPOEdit(string po_id)
        {

            string decoded_po_id = clsUtil.DecodeString(po_id);

            tran_scm_po_DTO model = new tran_scm_po_DTO();

            model = await _TranScmPoService.GetSingleAsync(Convert.ToInt64(decoded_po_id));

            if (model.supplier_id != null)
            {
                var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
                model.ddlsupplier_info = new dropdown_entity()
                {
                    id = model.supplier_id.ToString(),
                    text = sup.name.ToString()
                };
                model.concern_Person = sup.List_concern_person;
            }

			ViewBag.Supplier_Concern_Person = model.concern_Person.ToList().Select(a => new SelectListItem
			{
				Text = a.name.ToString(),
				Value = a.name.ToString()

			}).ToList();

			List<gen_term_and_conditions_DTO> termConditionList = await _genTermAndConditionsService.GetAllAsync();

            ViewBag.termConditionList =
                termConditionList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.term_condition_name.ToString(),
                        Value = a.gen_term_and_conditions_id.ToString()
                    }).ToList();


            return PartialView("~/Views/SCM/OpenPO/_OpenPOEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> OpenPOView(string po_id)
        {

            string decoded_po_id = clsUtil.DecodeString(po_id);

            tran_scm_po_DTO model = new tran_scm_po_DTO();

            model = await _TranScmPoService.GetSingleAsync(Convert.ToInt64(decoded_po_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            model.selected_supplier_name = sup.name;
            return PartialView("~/Views/SCM/OpenPO/_OpenPOView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetOpenPOData(DtParameters request)
        {

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranScmPoService.GetAllOpenPoAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id,request.Search.Value);


            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.po_id,
                            t.po_no,
                            t.po_date,
                            t.supplier_name,
                            t.unit_name,

                            t.event_title,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditOpenPO(this)' class='btn btn-primary btnEdit'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='ViewOpenPO(this)' class='btn btn-warning btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteOpenPo(\"" + clsUtil.EncodeString(t.po_id.ToString()) + "\")' class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetSubmittedPOData(DtParameters request)
        {

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranScmPoService.GetSubmittedPOData(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, request.Search.Value);


            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.po_id,
                            t.po_no,
                            t.po_date,
                            t.supplier_name,
                            t.unit_name,

                            t.event_title,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            

                            "<button type='button' onclick='ViewOpenPO(this)' class='btn btn-warning btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                            
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> SaveFabricOpenPO([FromBody] tran_scm_po_DTO request)
        {


            var ret = true;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            if (request.po_id == null)
            {
                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
            }


            try
            {

                var model = JsonConvert.DeserializeObject<tran_scm_po_entity>(JsonConvert.SerializeObject(request));
                model.event_id = objFilter.event_id;
                model.fiscal_year_id = objFilter.fiscal_year_id;
                model.item_structure_group_id = 1;
                model.is_approved = null;


                var trem = JsonConvert.DeserializeObject<List<TermConditionDetail>>(JsonConvert.SerializeObject(request.terms_conditions_list));


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


                model.terms_conditions = JArray.Parse(groupedJson).ToString();


                var details = JsonConvert.DeserializeObject<List<tran_scm_po_details_entity>>(JsonConvert.SerializeObject(request.List_po_details));
                ret = await _TranScmPoService.SaveAsync(model, details, request.List_Files);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
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
        public async Task<IActionResult> SaveAccessoriesOpenPo([FromBody] tran_scm_po_DTO request)
        {

            var ret = true;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            if (request.po_id == null)
            {
                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
            }



            try
            {

                var model = JsonConvert.DeserializeObject<tran_scm_po_entity>(JsonConvert.SerializeObject(request));
                model.item_structure_group_id = request.item_structure_group_id;
                model.event_id = objFilter.event_id;
                model.fiscal_year_id = objFilter.fiscal_year_id;
                model.is_approved = null;
               

                var details = JsonConvert.DeserializeObject<List<tran_scm_po_details_entity>>(JsonConvert.SerializeObject(request.po_details));
                ret = await _TranScmPoService.SaveAsync(model, details, request.List_Files);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
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
        public async Task<IActionResult> UpdateOpenPO([FromBody] tran_scm_po_DTO request)
        {
            var ret = true;


            {


                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

                request.updated_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
            }

            try
            {


                ret = await _TranScmPoService.UpdateAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
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
        public async Task<IActionResult> DeleteOpenPO([FromBody] tran_scm_po_DTO request)
        {


            var ret = true;


            {


                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

            }

            try
            {

                string decoded_po_id = clsUtil.DecodeString(request.strMasterID);

                request.po_id = Convert.ToInt64(decoded_po_id);

                ret = await _TranScmPoService.DeleteAsync(request.po_id.Value);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Deletion Failed")
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
        public async Task<IActionResult> SendForApprovalOpenPo([FromBody] tran_scm_po_DTO request)
        {
            var ret = true;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            //request.fiscal_year_id = Fiscal_Year_ID;
            //request.event_id = Event_ID;

            try
            {

                var model = JsonConvert.DeserializeObject<tran_scm_po_DTO>(JsonConvert.SerializeObject(request));

                ret = await _TranScmPoService.SendForApproval(model);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
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

        public async Task<IActionResult> GetTermsAndConditionsDetails(Int64 term_and_conditions_id)
        {



            var data = await _genTermAndConditionsService.GetTermsandConditionDetail(term_and_conditions_id);
            
            return PartialView("~/Views/SCM/FabricPo/_termandConditionsDetail.cshtml", data);


        }

        [HttpGet]
        public async Task<IActionResult> GetConcernPersons(long supplier_id)
        {
            var concernPersons = await _TranScmPoService.GetConcernPersonsAsync(supplier_id);
            return Json(concernPersons);
        }



    }
}

