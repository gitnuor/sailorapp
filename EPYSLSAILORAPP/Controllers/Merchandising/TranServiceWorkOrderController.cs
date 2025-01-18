using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Enum;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using ServiceStack;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using Web.Core.Frame.Helpers;


namespace EPYSLSAILORAPP.Controllers
{
    public class TranServiceWorkOrderController : ExtendedBaseController
    {
        private readonly ILogger<TranServiceWorkOrderController> _logger;

        private readonly ITranServiceWorkOrderService _TranServiceWorkOrderService;
        private readonly IGenUnitService _GenUnitService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;

        private readonly IConfiguration _configuration;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITrantechpackService _TrantechpackService;
        private readonly IGenTermAndConditionsService _GenTermAndConditionsService;


        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside TranServiceWorkOrderController !");
            return View();
        }

        public TranServiceWorkOrderController(
           IMapper mapper, ILogger<TranServiceWorkOrderController> logger, IHttpContextAccessor contextAccessor,
            ITranServiceWorkOrderService TranServiceWorkOrderService, IGenUnitService GenUnitService,
            IRPCDbService rpc_db_service, ITrantechpackService TrantechpackService, IConfiguration configuration, IGenTermAndConditionsService genTermAndConditionsService) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranServiceWorkOrderService = TranServiceWorkOrderService;
            _GenUnitService = GenUnitService;
            _TrantechpackService = TrantechpackService;
            _GenTermAndConditionsService = genTermAndConditionsService;



            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> TranServiceWorkOrderLanding()
        {

            return View("~/Views/MerchandiserMgt/TranServiceWorkOrder/TranServiceWorkOrderLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TranWorkOrderAdd(string tran_techpack_id, DtParameters request)
        {

            string techpack_id = clsUtil.DecodeString(tran_techpack_id);
            var start = request.Start;
            var size = 10;

            //will check --arif
            // var data = await _TranServiceWorkOrderService.GetEmbellishmentByTechpackAsync(request.Start, size, ActionType.getById.ToString(), Convert.ToInt64(techpack_id), request.fiscal_year_id, request.event_id, request.supplier_id);

            tran_tech_pack_DTO model = await _TrantechpackService.GetTechpackByID(Convert.ToInt64(techpack_id));


            var gengarmentpartids = model.TechPack_EmbellishmentList
                .SelectMany(embellishment => embellishment.EmbelshmentDet_List)
                .Select(m => m.garment_part_name)
                .ToList();

            var gengarmentpartid = model.TechPack_EmbellishmentList
               .SelectMany(embellishment => embellishment.EmbelshmentDet_List)
               .Select(m => m.gen_garment_part_id)
               .ToList();

            foreach (var embellishment in model.TechPack_EmbellishmentList)
            {
                Dictionary<string, long> colorSizeSum = new Dictionary<string, long>();

                foreach (var color in model.TechPack_ColorList)
                {
                    var colorCode = color.color_code;
                    var sizes = color.List_ColorSize.Select(s => Convert.ToInt64(s.style_color_size_quantity));

                    var sum = sizes.Sum();

                    if (colorSizeSum.ContainsKey(colorCode))
                    {
                        colorSizeSum[colorCode] += sum;
                    }
                    else
                    {
                        colorSizeSum[colorCode] = sum;
                    }
                }

                embellishment.color_code = string.Join(",", colorSizeSum.Keys);
                embellishment.style_color_size_quantity = string.Join(",", colorSizeSum.Values);

                embellishment.gen_garment_part_id = string.Join(",", gengarmentpartid);
                embellishment.garment_part_name = string.Join(",", gengarmentpartids);

            }

            var unit_list = await _GenUnitService.GetAllAsync();
            ViewBag.DeliveryUnits = unit_list.ToList()
           .Select(a =>
                    new SelectListItem
                    {
                        Text = a.unit_name.ToString(),
                        Value = a.gen_unit_id.ToString(),
                    }
                    ).ToList();



            ViewBag.ProcessName = model.TechPack_EmbellishmentList.ToList()
            .Select(a => new SelectListItem
            {
                Text = a.process_name,
                Value = a.gen_process_master_id.ToString(),
            }).ToList();



            ViewBag.SupplierInfoList1 = model.TechPack_EmbellishmentList.Select(e => e.ddlsupplier_info).ToList();
            var groupedSupplierInfo = ((IEnumerable<dynamic>)ViewBag.SupplierInfoList1)
                 .GroupBy(supplierInfo => new { Text = supplierInfo.text, Value = supplierInfo.id })
                 .Select(group => new { Text = group.Key.Text, Value = group.Key.Value, Items = group.ToList() })
                 .ToList();

            ViewBag.SupplierInfoList = groupedSupplierInfo;
            ViewBag.ContactPerson1 = model.TechPack_EmbellishmentList.Select(e => e.contact_person).ToList();
            ViewBag.ContactPerson = model.TechPack_EmbellishmentList.Select(e => e.contact_person).Distinct().ToList();
            ViewBag.embInfoId = model.TechPack_EmbellishmentList.Select(e => e.tran_tech_pack_embellishment_info_id).ToList();
            ViewBag.techpack = model.tran_techpack_id;

            ViewBag.ModelData3 = model.TechPack_EmbellishmentList.ToList();

            List<gen_term_and_conditions_DTO> termConditionList = await _GenTermAndConditionsService.GetAllAsync();

            ViewBag.termConditionList =
                termConditionList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.term_condition_name.ToString(),
                        Value = a.gen_term_and_conditions_id.ToString()
                    }).ToList();

            return PartialView("~/Views/MerchandiserMgt/TranServiceWorkOrder/_TranWorkOrderAdd.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> TranWorkOrderEdit(string service_work_order_id, DtParameters request)
        {
            try
            {
                string decoded_service_work_order_id = clsUtil.DecodeString(service_work_order_id);

                var data = await _TranServiceWorkOrderService.Get_master_detail_tran_service_order_Async(Convert.ToInt64(decoded_service_work_order_id));
                
                tran_service_work_order_DTO model = JsonConvert.DeserializeObject<tran_service_work_order_DTO>(JsonConvert.SerializeObject(data));
                model.details = JsonConvert.DeserializeObject<List<tran_service_work_order_detail_DTO>>(data.tran_service_work_order_detail_list);

                model.terms_and_conditions_list = JsonConvert.DeserializeObject<List<TermConditionGrouped>>(data.terms_condition_list);
                model.service_work_order_id1 = Convert.ToInt32(decoded_service_work_order_id);
                List<gen_term_and_conditions_DTO> termConditionList = await _GenTermAndConditionsService.GetAllAsync();

                ViewBag.termConditionList =
                    termConditionList.ToList().Select(a =>
                        new SelectListItem
                        {
                            Text = a.term_condition_name.ToString(),
                            Value = a.gen_term_and_conditions_id.ToString()
                        }).ToList();


                return PartialView("~/Views/MerchandiserMgt/TranServiceWorkOrder/_TranServiceOrderEdit.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }





        [HttpPost]
        public async Task<IActionResult> GetServiceOrderPendingData(DtParameters request)
        {
            Int64 receivedID = 0;

            var records = await _TranServiceWorkOrderService.GetTranServiceOrderPendingListAsync(request.Start, request.Length, ActionType.getAll.ToString(), receivedID, request.fiscal_year_id, request.event_id, request.supplier_id,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_techpack_id,
                            t.techpack_number,
                            t.techpack_date,
                            t.name,
                            t.style_item_product_name,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='TranWorkOrderAdd(this)' style='width:140px' class='btn btn-success btnEdit'  tran_techpack_id='" + clsUtil.EncodeString(t.tran_techpack_id.ToString()) + "'>Create Work Order</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetWorkOrderDraftData(DtParameters request)
        {
            Int64 actionType = 1;

            var records = await _TranServiceWorkOrderService.GetTranServiceWorkOrderDraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.service_work_order_id,
                            t.service_work_order_no,
                            t.service_work_date,
                            t.supplier_name,
                            t.techpack_number,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='EditTranMcdReturn(this)' class='btn btn-primary btnEdit'  service_work_order_id='" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +


                            "<button type='button' onclick='ViewWorkOrderApprove(this)' style='width:120px' class='btn btn-success btnEdit'  service_work_order_id='" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetWorkOrderProposedData(DtParameters request)
        {
            Int64 actionType = 2;

            var records = await _TranServiceWorkOrderService.GetTranServiceWorkOrderDraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id,request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.service_work_order_id,
                            t.service_work_order_no,
                            t.service_work_date,
                            t.supplier_name,
                            t.techpack_number,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewWorkOrderApprove(this)' style='width:120px' class='btn btn-success btnEdit'  service_work_order_id='" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetWorkOrderApprovedData(DtParameters request)
        {

            Int64 actionType = 3;

            var records = await _TranServiceWorkOrderService.GetTranServiceWorkOrderDraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.service_work_order_id,
                            t.service_work_order_no,
                            t.service_work_date,
                            t.supplier_name,
                            t.techpack_number,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewWorkOrderApprove(this)' style='width:120px' class='btn btn-success btnEdit'  service_work_order_id='" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpGet]
        public async Task<IActionResult> TranWorkOrderlandingApprove(string service_work_order_id, DtParameters request)
        {
            try
            {
                string decoded_service_work_order_id = clsUtil.DecodeString(service_work_order_id);

                var data = await _TranServiceWorkOrderService.Get_master_detail_tran_service_order_Async(Convert.ToInt64(decoded_service_work_order_id));
                tran_service_work_order_DTO model = JsonConvert.DeserializeObject<tran_service_work_order_DTO>(JsonConvert.SerializeObject(data));
                model.details = JsonConvert.DeserializeObject<List<tran_service_work_order_detail_DTO>>(data.tran_service_work_order_detail_list);

                model.terms_and_conditions_list = JsonConvert.DeserializeObject<List<TermConditionGrouped>>(data.terms_condition_list);


                return PartialView("~/Views/MerchandiserMgt/TranServiceWorkOrder/_TranServiceOrderlandingView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        [HttpGet]
        public async Task<IActionResult> TranServiceWorkOrderNew()
        {
            tran_service_work_order_DTO model = new tran_service_work_order_DTO();

            return PartialView("~/Views/SCM/TranServiceWorkOrder/_TranServiceWorkOrderNew.cshtml", model);

        }

        // [HttpGet]
        //public async Task<IActionResult> TranServiceWorkOrderEdit(string service_work_order_id)
        //{

        //    string decoded_service_work_order_id = clsUtil.DecodeString(service_work_order_id);

        //    tran_service_work_order_DTO model = new tran_service_work_order_DTO();

        //    model = await _TranServiceWorkOrderService.GetSingleAsync(Convert.ToInt64(decoded_service_work_order_id));

        //    return PartialView("~/Views/SCM/TranServiceWorkOrder/_TranServiceWorkOrderEdit.cshtml", model);

        //}

        //[HttpGet]
        //public async Task<IActionResult> TranServiceWorkOrderView(string service_work_order_id)
        //{

        //    string decoded_service_work_order_id = clsUtil.DecodeString(service_work_order_id);

        //    tran_service_work_order_DTO model = new tran_service_work_order_DTO();

        //    model = await _TranServiceWorkOrderService.GetSingleAsync(Convert.ToInt64(decoded_service_work_order_id));

        //    return PartialView("~/Views/SCM/TranServiceWorkOrder/_TranServiceWorkOrderView.cshtml", model);

        //}


        //[HttpPost]
        //public async Task<IActionResult> GetTranServiceWorkOrderData(DtParameters request)
        //{

        //    var records = await _TranServiceWorkOrderService.GetAllPagedDataAsync(request);

        //    var index = request.Start + 1;
        //    var data = (from t in records
        //                select new
        //                {
        //                    row_index = index++,
        //                    t.service_work_order_id,
        //                   // t.swo_no,
        //                    //t.swo_date,
        //                    t.delivery_date,
        //                    t.event_id,
        //                    t.delivery_unit_id,
        //                    t.delivery_unit_address,
        //                    t.remarks,
        //                    t.supplier_id,
        //                    t.supplier_address,
        //                    t.supplier_concern_person,
        //                    t.terms_condition_list,
        //                    t.test_requirements_list,
        //                    //t.document_list,
        //                    t.is_submitted,
        //                    t.is_approved,
        //                    t.approved_by,
        //                    t.approve_date,
        //                    t.approve_remarks,
        //                    t.added_by,
        //                    t.date_added,
        //                    t.updated_by,
        //                    t.date_updated,


        //                    datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
        //                    "<button type='button' onclick='EditTranServiceWorkOrder(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  service_work_order_id='" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "'>Edit</button>" +
        //                    "<button type='button' onclick='ViewTranServiceWorkOrder(this)' style='width: 120px;' class='btn btn-secondary btnView'  service_work_order_id='" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "'>View</button>" +
        //                    "<button type='button' onclick='DeleteTranServiceWorkOrder(\"" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
        //                    "</div>"
        //                }).ToList();
        //    var ret_obj = new AjaxResponse(records.Count, data);
        //    return Json(ret_obj);

        //}


        [HttpPost]
        public async Task<IActionResult> SaveTranServiceWorkOrder([FromBody] tran_service_work_order_DTO request)
        {

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            List<Claim> listClaims = (List<Claim>)claim.Claims;
            //var ret = true;
            // (bool success, long service_work_order_id) ret = (true, 0);

            var ret = true;
            try
            {
                if (listClaims.Exists(c => c.Type == "secobject"))
                {
                    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                    SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                    if (request.service_work_order_id == null || request.service_work_order_id == 0)
                    {
                        request.added_by = sec_object.userid.Value;
                        request.date_added = DateTime.Now;
                    }
                }

                var model = JsonConvert.DeserializeObject<tran_service_work_order_entity>(JsonConvert.SerializeObject(request));
                model.event_id = objFilter.event_id;
                model.fiscal_year_id = objFilter.fiscal_year_id;


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

                model.terms_condition_list = JArray.Parse(groupedJson).ToString();


                var details = JsonConvert.DeserializeObject<List<tran_service_work_order_detail_entity>>(JsonConvert.SerializeObject(request.details));
                if (listClaims.Exists(c => c.Type == "secobject"))
                {
                    var strsecobject1 = listClaims.Find(c => c.Type == "secobject").Value;
                    SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject1);

                    details.ForEach(n =>
                    {
                        // n.tran_type = 1;
                        n.added_by = sec_object.userid.Value;
                        n.date_added = DateTime.Now;
                    });
                }


                ret = await _TranServiceWorkOrderService.SaveAsync(model, details);//.SaveAsync1(model, details);
                                                                                   // ret = await _TranMcdPurchaseReturnService.SaveAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
                });
            }
            catch (Exception ex)
            {
                //ret = false;

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
        public async Task<IActionResult> UpdateTranServiceWorkOrder([FromBody] tran_service_work_order_DTO request)
        {
            var ret = true;
           // request.added_by = sec_object.userid.Value;

           // request.date_added = DateTime.Now;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;


            try
            {
               // var model = JsonConvert.DeserializeObject<tran_service_work_order_entity>(JsonConvert.SerializeObject(request));

                ret = await _TranServiceWorkOrderService.UpdateAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successfully Updated." : "Data Saving Failed")
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
        public async Task<IActionResult> ProposeForApproval(Int64 Id)
        {
            try
            {
                var ret = true;
                tran_service_work_order_DTO model = new tran_service_work_order_DTO();
                model.service_work_order_id = Id;

                ret = await _TranServiceWorkOrderService.ProposedAsync(model);
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

        public async Task<IActionResult> GetTermsAndConditionsDetails(Int64 term_and_conditions_id)
        {



            var data = await _GenTermAndConditionsService.GetTermsandConditionDetail(term_and_conditions_id);
            //return Json(data);
            return PartialView("~/Views/MerchandiserMgt/TranServiceWorkOrder/_termandConditionsDetail.cshtml", data);


        }


        //[HttpPost]
        //public async Task<IActionResult> UpdateTranServiceWorkOrder([FromBody] tran_service_work_order_DTO request)
        //{
        //    var ret = true;

        //    var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

        //    List<Claim> listClaims = (List<Claim>)claim.Claims;

        //    if (listClaims.Exists(c => c.Type == "secobject"))
        //    {
        //        var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

        //        SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

        //        request.added_by = sec_object.userid.Value;

        //        request.date_added = DateTime.Now;

        //        request.updated_by = sec_object.userid.Value;

        //        request.date_updated = DateTime.Now;
        //    }

        //    try
        //    {
        //        var model = JsonConvert.DeserializeObject<tran_service_work_order_entity>(JsonConvert.SerializeObject(request));

        //        ret = await _TranServiceWorkOrderService.UpdateAsync(model);

        //        return Json(new ResultEntity
        //        {
        //            Status_Code = (ret == true ? "200" : "400"),
        //            Status_Result = (ret == true ? "Successful" : "Data Saving Failed")
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        ret = false;
        //        using (LogContext.PushProperty("SpecialLogType", true))
        //        {
        //            _logger.LogError(ex.ToString());
        //        }
        //        return Json(new ResultEntity
        //        {
        //            Status_Code = (ret == true ? "200" : "400"),
        //            Status_Result = (ret == true ? "Operation Failed" : ex.Message)
        //        });
        //    }


        //}



        //[HttpPost]
        //public async Task<IActionResult> DeleteTranServiceWorkOrder([FromBody] tran_service_work_order_DTO request)
        //{
        //    var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

        //    List<Claim> listClaims = (List<Claim>)claim.Claims;

        //    var ret = true;

        //    if (listClaims.Exists(c => c.Type == "secobject"))
        //    {
        //        var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

        //        SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

        //        request.added_by = sec_object.userid.Value;

        //        request.date_added = DateTime.Now;

        //    }

        //    try
        //    {

        //        string decoded_service_work_order_id = clsUtil.DecodeString(request.strMasterID);

        //        request.service_work_order_id = Convert.ToInt64(decoded_service_work_order_id);

        //        ret = await _TranServiceWorkOrderService.DeleteAsync(request.service_work_order_id);

        //        return Json(new ResultEntity
        //        {
        //            Status_Code = (ret == true ? "200" : "400"),
        //            Status_Result = (ret == true ? "Successful" : "Data Deletion Failed")
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        ret = false;

        //        using (LogContext.PushProperty("SpecialLogType", true))
        //        {
        //            _logger.LogError(ex.ToString());
        //        }

        //        return Json(new ResultEntity
        //        {
        //            Status_Code = (ret == true ? "200" : "400"),
        //            Status_Result = (ret == true ? "Operation Failed" : ex.Message)
        //        });
        //    }

        //}

        #region Work Order Approve

        [HttpGet]
        public async Task<IActionResult> TranServiceWorkOrderLandingApprove()
        {

            return View("~/Views/MerchandiserMgt/TranServiceWorkOrder/TranServiceWorkOrderLandingApprove.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> GetWorkOrderProposedDataApprove(DtParameters request)
        {
            Int64 actionType = 2;

            var records = await _TranServiceWorkOrderService.GetTranServiceWorkOrderDraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.service_work_order_id,
                            t.service_work_order_no,
                            t.service_work_date,
                            t.supplier_name,
                            t.techpack_number,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewWorkOrderApprove(this)' style='width:120px' class='btn btn-success btnEdit'  service_work_order_id='" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetWorkOrderApprovedData1(DtParameters request)
        {

            Int64 actionType = 3;

            var records = await _TranServiceWorkOrderService.GetTranServiceWorkOrderDraftListAsync(request.Start, request.Length, actionType, request.fiscal_year_id, request.event_id, request.supplier_id, request.Search.Value);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,

                            t.service_work_order_id,
                            t.service_work_order_no,
                            t.service_work_date,
                            t.supplier_name,
                            t.techpack_number,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='ViewWorkOrderApprove(this)' style='width:120px' class='btn btn-success btnEdit'  service_work_order_id='" + clsUtil.EncodeString(t.service_work_order_id.ToString()) + "'>View</button>" +
                            "</div>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);
        }

        [HttpGet]
        public async Task<IActionResult> TranWorkOrderViewApprove(string service_work_order_id, DtParameters request)
        {
            try
            {
                string decoded_service_work_order_id = clsUtil.DecodeString(service_work_order_id);

                var data = await _TranServiceWorkOrderService.Get_master_detail_tran_service_order_Async(Convert.ToInt64(decoded_service_work_order_id));
                tran_service_work_order_DTO model = JsonConvert.DeserializeObject<tran_service_work_order_DTO>(JsonConvert.SerializeObject(data));
                model.details = JsonConvert.DeserializeObject<List<tran_service_work_order_detail_DTO>>(data.tran_service_work_order_detail_list);
                model.terms_and_conditions_list = JsonConvert.DeserializeObject<List<TermConditionGrouped>>(data.terms_condition_list);

                return PartialView("~/Views/MerchandiserMgt/TranServiceWorkOrder/_TranServiceOrderApproveView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        [HttpPost]
        public async Task<IActionResult> UpdateForApproval([FromBody] tran_service_work_order_DTO request)
        {
            var ret = true;




            {


                request.added_by = sec_object.userid.Value;



                request.updated_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
            }
            try
            {

                ret = await _TranServiceWorkOrderService.ApproveAsync(request);

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

        #endregion

    }
}

