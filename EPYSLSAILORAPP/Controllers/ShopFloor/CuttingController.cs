using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using ServiceStack;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class CuttingController : ExtendedBaseController
    {
        private readonly ILogger<CuttingController> _logger;

        private readonly ICuttingService _CuttingService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside CuttingController !");
            return View();
        }

        public CuttingController(
           IMapper mapper, ILogger<CuttingController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            ICuttingService CuttingService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _CuttingService = CuttingService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            
            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> CuttingLanding()
        {
            //var obj = await _CuttingService.GetSingleAsync(45);
            return View("~/Views/ShopFloor/Cutting/CuttingLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> CuttingNew(string techpack_id)
        {
            long p_techpack_id = Convert.ToInt64(clsUtil.DecodeString(techpack_id));
            tran_cutting_DTO model = await _CuttingService.GetTechPackInfoForCutting(p_techpack_id);
            model.techpack_id = p_techpack_id;
            model.size_wise_color = JsonConvert.DeserializeObject<List<size_wise_color_quantity_DTO>>(model.size_wise_color_quantity);

            model.color_quantity = JsonConvert.DeserializeObject<List<color_quantity_DTO>>(JsonConvert.SerializeObject(model.size_wise_color.GroupBy(x => x.color_code).Select(g => new { color_code = g.Key, quantity = g.Sum(x => x.quantity) })));

            return PartialView("~/Views/ShopFloor/Cutting/_CuttingNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GetCuttingDetails(long techpack_id, string p_color_code)
        {

            cutting_details_DTO model = await _CuttingService.GetCuttingDetails(techpack_id, p_color_code);
            model.techpack_id = techpack_id;
            model.techpack_garment_part_list = JsonConvert.DeserializeObject<List<garment_part>>(model.techpack_garment_parts);
            model.size_list = JsonConvert.DeserializeObject<List<sizes>>(model.size_text);
            ViewBag.garment_part_List = JsonConvert.DeserializeObject<List<garment_part>>(model.all_garment_parts)
               .Select(a =>
                           new SelectListItem
                           {
                               Text = a.garment_part_name,
                               Value = a.gen_garment_part_id.ToString()
                           }
                       ).ToList();

            return PartialView("~/Views/ShopFloor/Cutting/_CuttingDetails.cshtml", model);

        }


        [HttpGet]
        public async Task<IActionResult> GetFabricDetaiils(long techpack_id)
        {

            List<fabric_details_DTO> model = await _CuttingService.GetFabricDetaiils(techpack_id);

            return PartialView("~/Views/ShopFloor/Cutting/_FabricDetails.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> CuttingEdit(string tran_cutting_id)
        {

            string decoded_tran_cutting_id = clsUtil.DecodeString(tran_cutting_id);

            tran_cutting_DTO model = new tran_cutting_DTO();

            model = await _CuttingService.GetSingleAsync(Convert.ToInt64(decoded_tran_cutting_id));

            return PartialView("~/Views/ShopFloor/Cutting/_CuttingEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> CuttingView(string tran_cutting_id)
        {

            string decoded_tran_cutting_id = clsUtil.DecodeString(tran_cutting_id);

            tran_cutting_DTO model = new tran_cutting_DTO();

            model = await _CuttingService.GetSingleAsync(Convert.ToInt64(decoded_tran_cutting_id));

            return PartialView("~/Views/ShopFloor/Cutting/_CuttingView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetCuttingData(DtParameters request)
        {

            var records = await _CuttingService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_cutting_id,
                            t.tran_pp_meeting_id,
                            t.techpack_id,
                            t.fiscal_year_id,
                            t.event_id,
                            t.style_code,
                            t.all_processes,
                            t.remarks,
                            t.added_by,
                            t.date_added,
                            t.updated_by,
                            t.date_updated,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditCutting(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  tran_cutting_id='" + clsUtil.EncodeString(t.tran_cutting_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewCutting(this)' style='width: 120px;' class='btn btn-secondary btnView'  tran_cutting_id='" + clsUtil.EncodeString(t.tran_cutting_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteCutting(\"" + clsUtil.EncodeString(t.tran_cutting_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetJoinedCuttingData(DtParameters request)
        {

            var records = await _CuttingService.GetAllJoined_CuttingAsync(request.Start, request.Length);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.style_code,
                            t.all_processes,
                            t.remarks,
                            t.techpack_number,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditCutting(this)'  class='btn btn-secondary btnEdit'  tran_cutting_id='" + clsUtil.EncodeString(t.tran_cutting_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='ViewCutting(this)'  class='btn btn-secondary btnView'  tran_cutting_id='" + clsUtil.EncodeString(t.tran_cutting_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteCutting(\"" + clsUtil.EncodeString(t.tran_cutting_id.ToString()) + "\")' class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SaveCutting([FromBody] tran_cutting_insert_DTO request)
        {
            var ret = true;
            request.added_by = sec_object.userid.Value;
            request.date_added = DateTime.Now;
            try
            {
                request.event_id = objFilter.event_id;
                request.fiscal_year_id = objFilter.fiscal_year_id;
                request.bundlesJarry = JArray.Parse(JsonConvert.SerializeObject(request.bundles));

                List<tran_cutting_color_batch_fabric_details_DTO> fabrics = new List<tran_cutting_color_batch_fabric_details_DTO>();
                List<tran_cutting_color_batch_garment_part_DTO> g_parts = new List<tran_cutting_color_batch_garment_part_DTO>();
                List<tran_cutting_color_batch_garment_part_details_DTO> g_part_det = new List<tran_cutting_color_batch_garment_part_details_DTO>();

                foreach (fabric_detail fabric in request.batch_info.details)
                {

                    tran_cutting_color_batch_fabric_details_DTO fd = new tran_cutting_color_batch_fabric_details_DTO();

                    fd.gen_item_master_id = Convert.ToInt64(fabric.item_id);
                    fd.fabric_name = fabric.item_name;
                    fd.uom_id = Convert.ToInt64(fabric.measurement_unit_detail_id);
                    fd.uom = fabric.measurement_unit;
                    fd.booking_quantity = Convert.ToDecimal(fabric.booking_quantity);
                    fd.receive_quantity = Convert.ToDecimal(fabric.issue_quantity);
                    fd.input_quantity = Convert.ToDecimal(fabric.input_quantity);
                    fd.no_of_lay = Convert.ToInt64(fabric.no_of_lay);

                    fabrics.Add(fd);
                }

                foreach (tran_insert_garment_part_DTO part in request.garment_parts)
                {

                    tran_cutting_color_batch_garment_part_DTO gp = new tran_cutting_color_batch_garment_part_DTO();

                    gp.gen_part_id = part.gen_garment_part_id;
                    gp.gen_part = part.garment_part_name;
                    gp.batch_summary_size_info = JsonConvert.SerializeObject(request.garment_part_details.Where(detail => detail.gen_garment_part_id == part.gen_garment_part_id).ToList());
                    gp.total_output_quantity = request.garment_part_details.Where(detail => detail.gen_garment_part_id == part.gen_garment_part_id).Sum(detail => detail.output_quantity);

                    g_parts.Add(gp);
                }

                foreach (tran_insert_garment_part_details_DTO part in request.garment_part_details)
                {

                    tran_cutting_color_batch_garment_part_details_DTO gp_det = new tran_cutting_color_batch_garment_part_details_DTO();

                    gp_det.style_size = part.style_size;
                    gp_det.tran_cutting_color_batch_garment_part_id = part.gen_garment_part_id;
                    gp_det.style_product_size_group_detid = part.style_product_size_group_detid;
                    gp_det.output_quantity = part.output_quantity;
                    gp_det.ratio = part.ratio;

                    g_part_det.Add(gp_det);
                }


                request.FabricJarry = JArray.Parse(JsonConvert.SerializeObject(fabrics));

                request.garment_partsJarry = JArray.Parse(JsonConvert.SerializeObject(g_parts));
                request.garment_part_detailsJarry = JArray.Parse(JsonConvert.SerializeObject(g_part_det));
                request.tran_cutting_color_batch_size_summeryJarry = JArray.Parse(JsonConvert.SerializeObject(request.tran_cutting_color_batch_size_summery));
                ret = await _CuttingService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateCutting([FromBody] tran_cutting_DTO request)
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
                var model = JsonConvert.DeserializeObject<tran_cutting_entity>(JsonConvert.SerializeObject(request));

                ret = await _CuttingService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteCutting([FromBody] tran_cutting_DTO request)
        {


            var ret = true;


            {


                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

            }

            try
            {

                string decoded_tran_cutting_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_cutting_id = Convert.ToInt64(decoded_tran_cutting_id);

                ret = await _CuttingService.DeleteAsync(request.tran_cutting_id);

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
        public async Task<IActionResult> GetPendingCuttingPlans(DtParameters request)
        {

            var records = await _CuttingService.GetPendingCuttingPlans(request.Start, request.Length, request.event_id, request.fiscal_year_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.meeting_conducted_date,
                            t.event_title,
                            t.techpack_number,
                            t.conducted_by,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='BatchCreate(this)' class='btn btn-primary btnView'  techpack_id='" + clsUtil.EncodeString(t.techpack_id.ToString()) + "'>Batch Create</button>&nbsp;" +

                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetRunningCuttingPlans(DtParameters request)
        {

            var records = await _CuttingService.GetRunningCuttingPlans(request.Start, request.Length, request.event_id, request.fiscal_year_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.meeting_conducted_date,
                            t.event_title,
                            t.techpack_number,
                            t.conducted_by,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='EditCuttingPlan(this)' class='btn btn-primary btnView'  techpack_id='" + clsUtil.EncodeString(t.techpack_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>&nbsp;" +

                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }
        [HttpPost]
        public async Task<IActionResult> GetCompletedCuttingPlans(DtParameters request)
        {

            var records = await _CuttingService.GetCompletedCuttingPlans(request.Start, request.Length, request.event_id, request.fiscal_year_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.meeting_conducted_date,
                            t.event_title,
                            t.techpack_number,
                            t.conducted_by,
                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewCuttingPlan(this)' class='btn btn-primary btnView'  tran_cutting_id='" + clsUtil.EncodeString(t.tran_cutting_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>&nbsp;" +

                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }
    }
}

