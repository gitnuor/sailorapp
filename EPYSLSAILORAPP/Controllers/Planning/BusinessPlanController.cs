using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.TranTables;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Globalization;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers.BusinessPlanning
{
    public class BusinessPlanController : BaseController
    {
        private readonly ILogger<BusinessPlanController> _logger;
        private readonly IBusinessPlanService _BP_service;
        private readonly IRPCDbService _RPCDbService;
        private readonly IGenOutletService _gen_outlet_entity_service;
        //private readonly ITran_BP_YearService _tran_bp_yearservice;
        private readonly ITran_BP_EventMonthService _tran_bp_eventmonthservice;
        private readonly ITran_BP_EventMonthOutletService _tran_bp_eventmonthoutletservice;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside BusinessPlan !");
            return View();
        }

        public BusinessPlanController(IBusinessPlanService BusinessPlanService,
            IGenFiscalYearService gen_fiscal_year_Service, IGenOutletService gen_outlet_entity_service,
            ITran_BP_EventMonthService tran_bp_eventmonthservice, ITran_BP_EventMonthOutletService tran_bp_eventmonthoutletservice,
            IMapper mapper, ILogger<BusinessPlanController> logger, IHttpContextAccessor contextAccessor,
            IRPCDbService RPCDbService)
        {
            _BP_service = BusinessPlanService;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _gen_outlet_entity_service = gen_outlet_entity_service;
            //_tran_bp_yearservice = tran_bp_yearservice;
            _mapper = mapper;
            _logger = logger;

            _tran_bp_eventmonthservice = tran_bp_eventmonthservice;
            _tran_bp_eventmonthoutletservice = tran_bp_eventmonthoutletservice;
            _RPCDbService = RPCDbService;

            _contextAccessor = contextAccessor;
            _logger.LogInformation("Hello from inside BusinessPlan !");
        }

        [HttpGet]
        public async Task<IActionResult> BusinessPlanLanding()
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (!listClaims.Exists(c => c.Type == "secobject"))
            {
                Response.Redirect("/account/LogOff");
            }
            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _BP_service.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.
                Where(p => tran_bp_year.All(q => q.fiscal_year_id != p.fiscal_year_id)).ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                   }
                                                   ).ToList();

            return View("~/Views/BusinessPlanning/BusinessPlanLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> BusinessPlanApprovalLanding()
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (!listClaims.Exists(c => c.Type == "secobject"))
            {
                Response.Redirect("/account/LogOff");
            }
            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            var tran_bp_year = await _BP_service.get_tran_BP_YearService_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.
                Where(p => tran_bp_year.All(q => q.fiscal_year_id != p.fiscal_year_id)).ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                   }
                                                   ).ToList();

            return View("~/Views/BusinessPlanning/BusinessPlanApprovalLanding.cshtml");
        }

        public async Task<IActionResult> BusinessPlanNew(string param)
        {

            string decoded_fiscalyearid = clsUtil.DecodeString(param);

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.ToList().Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                   }
                                                   ).ToList();
            ViewBag.fiscal_year_id = decoded_fiscalyearid;

            return View("~/Views/BusinessPlanning/BusinessPlanNew.cshtml");

        }

        public async Task<IActionResult> BusinessPlanEdit(string param)
        {
            ViewBag.IsEditMode = true;
            string decoded_tran_bp_year_id = clsUtil.DecodeString(param);

            var obj_bp_year_list = await _BP_service.get_tran_BP_YearService_GetAll();

            var obj_bp_year = obj_bp_year_list.Where(p => p.tran_bp_year_id == Convert.ToInt64(decoded_tran_bp_year_id)).FirstOrDefault();

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.ToList().Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                   }
                                                   ).ToList();
            // ViewBag.fiscal_year_id = obj_bp_year.fiscal_year_id;

            return View("~/Views/BusinessPlanning/BusinessPlanEdit.cshtml", obj_bp_year);

        }
        //
        public async Task<IActionResult> ViewMonthWiseData(string fiscal_year_filter)
        {
            var model = await _RPCDbService.GetAllrpt_bp_data_month_wiseAsync(Convert.ToInt64(fiscal_year_filter));

            return PartialView("~/Views/BusinessPlanning/_MonthWiseDataView.cshtml", model);
        }
        //
        public async Task<IActionResult> ViewAnalysisData(string fiscal_year_filter)
        {
            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.ToList().Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       Selected = a.is_used
                                                   }
                                                   ).ToList();
            return PartialView("~/Views/BusinessPlanning/_DataAnalysis.cshtml");
        }
        public async Task<IActionResult> ViewOutletWiseData(string fiscal_year_filter)
        {
            var model = await _RPCDbService.GetAllrpt_bp_data_outlet_wiseAsync(Convert.ToInt64(fiscal_year_filter));

            return PartialView("~/Views/BusinessPlanning/_OutletWiseDataView.cshtml", model);
        }
        public async Task<IActionResult> BusinessPlanView(string param)
        {
            ViewBag.IsEditMode = false;
            string decoded_tran_bp_year_id = clsUtil.DecodeString(param);

            var obj_bp_year_list = await _BP_service.get_tran_BP_YearService_GetAll();

            var obj_bp_year = obj_bp_year_list.Where(p => p.tran_bp_year_id == Convert.ToInt64(decoded_tran_bp_year_id)).FirstOrDefault();

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.ToList().Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                   }
                                                   ).ToList();

            return View("~/Views/BusinessPlanning/BusinessPlanEdit.cshtml", obj_bp_year);

        }

        public async Task<IActionResult> BusinessPlanApprovalView(string param)
        {
            string decoded_tran_bp_year_id = clsUtil.DecodeString(param);

            var obj_bp_year_list = await _BP_service.get_tran_BP_YearService_GetAll();

            var obj_bp_year = obj_bp_year_list.Where(p => p.tran_bp_year_id == Convert.ToInt64(decoded_tran_bp_year_id)).FirstOrDefault();

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.ToList().Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                   }
                                                   ).ToList();

            return View("~/Views/BusinessPlanning/BusinessPlanApprovalView.cshtml", obj_bp_year);

        }

        public async Task<IActionResult> BusinessPlanApprove(string param)
        {
            string decoded_tran_bp_year_id = clsUtil.DecodeString(param);

            var obj_bp_year_list = await _BP_service.get_tran_BP_YearService_GetAll();

            var obj_bp_year = obj_bp_year_list.Where(p => p.tran_bp_year_id == Convert.ToInt64(decoded_tran_bp_year_id)).FirstOrDefault();

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            ViewBag.fiscal_year_list = (List<SelectListItem>)fiscal_year_list.ToList().Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString()
                                                   }
                                                   ).ToList();

            return View("~/Views/BusinessPlanning/BusinessPlanApprove.cshtml", obj_bp_year);

        }

        private string GetPlanStatus(rpc_tran_bp_year_DTO obj)
        {
            string status = "";

            if (obj.is_submitted != true)
            {
                status = "Draft";
            }
            else
            {
                if (obj.is_approved == true)
                {
                    status = "Approved";
                }
                else
                {
                    status = "Submitted For Approval";
                }
            }
            return status;
        }


        [HttpPost]
        public async Task<IActionResult> GetTranBusinessPlanYearData(DtParameters request)
        {
            List<rpc_tran_bp_year_DTO> records = await _BP_service.GetAllJoined_TranBpYearAsync(request);

            var data = (from t in records.OrderByDescending(p => p.year_no)
                        select new
                        {
                            t.tran_bp_year_id,
                            year_name = t.year_name,
                            t.gross_sales,
                            t.due_amount,

                            str_gross_sales = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + (t.gross_sales.HasValue ? Decimal.Round(t.gross_sales.Value).ToString() : "0") + "\"  class=\"border-gray-200 form-control\">",

                            str_yearly_gross_discount = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + (t.yearly_gross_discount.HasValue ? Decimal.Round(t.yearly_gross_discount.Value).ToString() : "0") + "\"  class=\"border-gray-200 form-control\">",

                            str_yearly_gross_net_amount = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + (t.yearly_gross_net_amount.HasValue ? Decimal.Round(t.yearly_gross_net_amount.Value).ToString() : "0") + "\" id=\"yearly_gross_sales\" name=\"yearly_gross_sales\" class=\"border-gray-200 form-control\">",

                            str_due_amount = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + (t.due_amount.HasValue ? Decimal.Round(t.due_amount.Value).ToString() : "0") + "\"  class=\"border-gray-200 form-control\">",

                            plan_status = GetPlanStatus(t),

                            total_quantity = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + (t.total_quantity.HasValue ? Decimal.Round(t.total_quantity.Value).ToString() : "0") + "\"  class=\"border-gray-200 form-control\">",

                            total_mrp = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + (t.total_mrp.HasValue ? Decimal.Round(t.total_mrp.Value).ToString() : "0") + "\"  class=\"border-gray-200 form-control\">",

                            total_cpu = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + (t.total_cpu.HasValue ? Decimal.Round(t.total_cpu.Value).ToString() : "0") + "\"  class=\"border-gray-200 form-control\">",



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<i class=\"fa-solid fa-eye grid_icon\"  onclick=\"location.href='/BusinessPlan/BusinessPlanView?param=" + clsUtil.EncodeString(t.tran_bp_year_id.ToString()) + "'\" ></i>" +
                           (t.is_submitted != true ? "<i class=\"grid_icon fa-sharp fa-regular fa-pen-to-square\" onclick=\"location.href='/BusinessPlan/BusinessPlanEdit?param=" + clsUtil.EncodeString(t.tran_bp_year_id.ToString()) + "'\"></i>" : "") +

                           "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count() > 0 ? records.FirstOrDefault().total_rows.Value : 0, data);
            return Json(ret_obj);

        }

        public async Task<IActionResult> GenerateExcel()
        {
            var outlet_list = await _gen_outlet_entity_service.GetAllAsync();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Populate some sample data
                worksheet.Cells["A1"].Value = "Outlets";
                worksheet.Cells["B1"].Value = "Outlets";
                worksheet.Cells["C1"].Value = "Gross Sales";
                worksheet.Cells["D1"].Value = "Discount";

                var i = 2;
                foreach (var row in outlet_list.OrderBy(p => p.outlet_name))
                {
                    worksheet.Cells["A" + i.ToString()].Value = row.outlet_name;
                    //worksheet.Cells["B" + i.ToString()].Value = row.outlet_name;
                    worksheet.Cells["C" + i.ToString()].Value = 0;
                    worksheet.Cells["D" + i.ToString()].Value = 0;
                    i++;
                }

                // Set some styling (optional)
                worksheet.Cells["A1:D1"].Style.Font.Bold = true;
                worksheet.Cells["A1:D1"].AutoFitColumns();

                // Convert the Excel package to a byte array
                var excelBytes = package.GetAsByteArray();

                // Return the Excel file as a downloadable content
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BusinessPlanOutlet" + DateTime.Now.ToString("yyyyddMMHHmmss") + ".xlsx");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            var file = Request.Form.Files[0]; // Get the file from the request

            List<rpc_tran_bp_event_month_outlet> dataList = new List<rpc_tran_bp_event_month_outlet>();


            if (file != null && file.Length > 0)
            {
                try
                {
                    var outlet_list = await _gen_outlet_entity_service.GetAllAsync();

                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);

                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                        using (var package = new ExcelPackage(stream))
                        {

                            var worksheet = package.Workbook.Worksheets[0];
                            var rowCount = worksheet.Dimension.Rows;
                            var colCount = worksheet.Dimension.Columns;

                            for (int row = 2; row <= rowCount; row++)
                            {
                                try
                                {
                                    if (worksheet.Cells[row, 1].Value != null && worksheet.Cells[row, 2].Value != null)
                                    {
                                        var outlet_name = worksheet.Cells[row, 1].Value.ToString();


                                        rpc_tran_bp_event_month_outlet rowData = new rpc_tran_bp_event_month_outlet();
                                        rowData.outlet_id = outlet_list.Where(p => p.outlet_name.Trim().ToLower() == outlet_name.Trim().ToLower()).FirstOrDefault().outlet_id;

                                        rowData.outlet_gross_sales = worksheet.Cells[row, 3] != null ?
                                            Convert.ToDecimal(worksheet.Cells[row, 3].Value) : 0;
                                        rowData.outlet_discount_amount = worksheet.Cells[row, 4] != null ?
                                          Convert.ToDecimal(worksheet.Cells[row, 4].Value) : 0;

                                        rowData.outlet_net_amount = rowData.outlet_gross_sales - rowData.outlet_discount_amount;

                                        dataList.Add(rowData);

                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }

                        }
                    }

                    return Json(new { statusCode = "1", Result = dataList });

                }
                catch (Exception ex)
                {
                    return Json(new { statusCode = "0", Result = dataList });
                }
            }
            // Process the uploaded file here and get string output
            string result = "Sample string output"; // Replace this with actual processing

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetTranBusinessPlanYearApprovalData(DtParameters request)
        {
            var records = await _BP_service.GetAllJoined_TranBpYearAsync(request);

            var data = (from t in records.Where(p => p.is_submitted == true)
                        select new
                        {
                            t.tran_bp_year_id,
                            year_name = t.year_name,
                            t.gross_sales,
                            plan_status = GetPlanStatus(t),

                            str_gross_sales = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + t.gross_sales.ToString() + "\" id=\"yearly_gross_sales\" name=\"yearly_gross_sales\" class=\"border-gray-200 rounded-sm text-sm\">\r\n",
                            str_yearly_gross_discount = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + t.yearly_gross_discount.ToString() + "\"  class=\"border-gray-200 form-control\">",
                            str_yearly_gross_net_amount = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + t.yearly_gross_net_amount.ToString() + "\" id=\"yearly_gross_sales\" name=\"yearly_gross_sales\" class=\"border-gray-200 form-control\">",

                            total_quantity = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + (t.total_quantity.HasValue ? Decimal.Round(t.total_quantity.Value).ToString() : "0") + "\"  class=\"border-gray-200 form-control\">",

                            total_mrp = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + (t.total_mrp.HasValue ? Decimal.Round(t.total_mrp.Value).ToString() : "0") + "\"  class=\"border-gray-200 form-control\">",

                            total_cpu = "<input disabled type=\"currency\"  style=\"text-align:center;width:100%;font-size: 16px!important;font-weight: bold;color: blue;\" value=\"" + (t.total_cpu.HasValue ? Decimal.Round(t.total_cpu.Value).ToString() : "0") + "\"  class=\"border-gray-200 form-control\">",

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                             "<i  class='fa-solid fa-eye grid_icon'  onclick=\"location.href='/BusinessPlan/BusinessPlanApprovalView?param=" + clsUtil.EncodeString(t.tran_bp_year_id.ToString()) + "'\"></i>" +

                              "<i  class='fa-solid " + (t.is_approved == false ? "fa-square-check" : "fa-pen-to-square") + " grid_icon'  onclick=\"location.href='/BusinessPlan/BusinessPlanApprove?param=" + clsUtil.EncodeString(t.tran_bp_year_id.ToString()) + "'\"></i>" +

                             "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count() > 0 ? records.FirstOrDefault().total_rows.Value : 0, data);
            return Json(ret_obj);

        }

        [HttpGet]
        public async Task<IActionResult> GetOutletList()
        {
            var records = await _gen_outlet_entity_service.GetAllAsync();

            return PartialView("~/Views/Shared/GenViews/GenOutlet.cshtml", records);

        }

        private async Task<string> GetMonthHtml(rpc_event_tran_data_getall entity, bool? isView = false)
        {
            List<tran_bp_event_month_dto> tran_bp_event_monthList = new List<tran_bp_event_month_dto>();

            if (!string.IsNullOrEmpty(entity.monthlist))
            {
                tran_bp_event_monthList = JsonConvert.DeserializeObject<List<tran_bp_event_month_dto>>(entity.monthlist);
            }

            string strhtml = "<table class='table dataTable table-striped'>";

            if (entity.start_month_id <= entity.end_month_id)
            {
                for (Int64 monthid = entity.start_month_id.Value; monthid <= entity.end_month_id.Value; monthid++)
                {
                    var actual_monthid = monthid > 12 ? (monthid - 12) : monthid;

                    var monthly_grosssalesList = tran_bp_event_monthList.Where(p => p.month_id == monthid).ToList();

                    decimal? monthly_grosssales = 0;
                    decimal? monthly_discount = 0;
                    decimal? monthly_net = 0;
                    Int64? tran_bp_event_month_id = 0;

                    if (monthly_grosssalesList.Count > 0)
                    {
                        monthly_grosssales = monthly_grosssalesList.FirstOrDefault().monthly_gross_sales;
                        monthly_discount = monthly_grosssalesList.FirstOrDefault().monthly_discount_amount;
                        monthly_net = monthly_grosssales - monthly_discount;
                        tran_bp_event_month_id = monthly_grosssalesList.FirstOrDefault().tran_bp_event_month_id;
                    }

                    strhtml += "<tr>" +
                        "<td style='width:108px;'><label class='labelNormal'> " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(monthid)) + "</label></td>" +
                        "<td style='width:130px;'><input disabled  month_id='" + actual_monthid.ToString() + "'  event_id='" + entity.event_id.ToString() + "' style='width:105px;background-color: aliceblue;' type='currency' id='txtmonthlysales" + entity.event_id.ToString() + actual_monthid.ToString() + "'  value='" + Decimal.Round(monthly_grosssales.Value, 2).ToString() + "' tran_bp_event_month_id='" + tran_bp_event_month_id.ToString() + "' class='txtmonthlysales form-control '/></td>" +
                        "<td style='width:130px;'><input disabled  month_id='" + actual_monthid.ToString() + "'  event_id='" + entity.event_id.ToString() + "' style='width:105px;background-color: aliceblue;margin-left: 0px;' type='currency' id='txtmonthlydiscount" + entity.event_id.ToString() + actual_monthid.ToString() + "'  value='" + Decimal.Round(monthly_discount.Value, 2).ToString() + "' tran_bp_event_month_id='" + tran_bp_event_month_id.ToString() + "' class='txtmonthlydiscount form-control '/></td>" +
                        "<td style='width:130px;'><input disabled  month_id='" + actual_monthid.ToString() + "'  event_id='" + entity.event_id.ToString() + "' style='width:105px;background-color: aliceblue;' type='currency' id='txtmonthlynet" + entity.event_id.ToString() + actual_monthid.ToString() + "'  value='" + Decimal.Round(monthly_net.Value, 2).ToString() + "' tran_bp_event_month_id='" + tran_bp_event_month_id.ToString() + "' class='txtmonthlynet form-control '/></td>" +
                        "<td style='width:100px'> <button month_id='" + actual_monthid.ToString() + "' onclick='BtnLoadOutlet(this);return false;'  isview='" + isView.ToString().ToLower() + "' event_id='" + entity.event_id.ToString() + "' class='btn btn-success btnoutlet' style='font-size: 13px;width: 95px;'>Outlet Sales</button> </td>" +
                        "</tr>";
                }
            }
            else
            {
                for (Int64 monthid = entity.start_month_id.Value; monthid <= (entity.end_month_id.Value + 12); monthid++)
                {
                    var actual_monthid = monthid > 12 ? (monthid - 12) : monthid;
                    var monthly_grosssalesList = tran_bp_event_monthList.Where(p => p.month_id == actual_monthid).ToList();

                    decimal? monthly_grosssales = 0;
                    decimal? monthly_discount = 0;
                    decimal? monthly_net = 0;
                    Int64? tran_bp_event_month_id = 0;

                    if (monthly_grosssalesList.Count > 0)
                    {
                        monthly_grosssales = monthly_grosssalesList.FirstOrDefault().monthly_gross_sales;
                        monthly_discount = monthly_grosssalesList.FirstOrDefault().monthly_discount_amount;
                        monthly_net = monthly_grosssalesList.FirstOrDefault().monthly_net_amount;
                        tran_bp_event_month_id = monthly_grosssalesList.FirstOrDefault().tran_bp_event_month_id;
                    }

                    strhtml += "<tr>" +
                        "<td style='width:108px'><label class='labelNormal'> " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(actual_monthid)) + "</label></td>" +
                        "<td style='width:130px'><input disabled  month_id='" + actual_monthid.ToString() + "'  event_id='" + entity.event_id.ToString() + "' style='width:105px;background-color: aliceblue;' type='currency' id='txtmonthlysales" + entity.event_id.ToString() + actual_monthid.ToString() + "'  value='" + Decimal.Round(monthly_grosssales.Value, 2).ToString() + "' tran_bp_event_month_id='" + tran_bp_event_month_id.ToString() + "' class='txtmonthlysales form-control '/></td>" +
                        "<td style='width:130px;'><input disabled  month_id='" + actual_monthid.ToString() + "'  event_id='" + entity.event_id.ToString() + "' style='width:105px;background-color: aliceblue;margin-left: 0px;' type='currency' id='txtmonthlydiscount" + entity.event_id.ToString() + actual_monthid.ToString() + "'  value='" + Decimal.Round(monthly_discount.Value, 2).ToString() + "' tran_bp_event_month_id='" + tran_bp_event_month_id.ToString() + "' class='txtmonthlydiscount form-control '/></td>" +
                        "<td style='width:130px;'><input disabled  month_id='" + actual_monthid.ToString() + "'  event_id='" + entity.event_id.ToString() + "' style='width:105px;background-color: aliceblue;' type='currency' id='txtmonthlynet" + entity.event_id.ToString() + actual_monthid.ToString() + "'  value='" + Decimal.Round(monthly_net.Value, 2).ToString() + "' tran_bp_event_month_id='" + tran_bp_event_month_id.ToString() + "' class='txtmonthlynet form-control '/></td>" +
                        "<td style='width:100px'> <button onclick='BtnLoadOutlet(this);return false;' month_id='" + actual_monthid.ToString() + "'  event_id='" + entity.event_id.ToString() + "' class='btn btn-success btnoutlet' style='font-size: 13px;width: 95px;'>Outlet Sales</button> </td>" +
                        "</tr>";
                }
            }

            strhtml += "</table>";

            return strhtml;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllEventListData(DtParameters request)
        {

            var records = await _BP_service.rpc_event_tran_data_GetAll(request);
            //var monthlist = await _RPCDbService.GetAllget_bp_year_event_month_dataAsync(request.MasterID.Value);
            var data = (from t in records
                        select new
                        {
                            t.tran_bp_event_id,
                            season_event = t.event_title + "<br/>(" + t.start_date.Value.ToString("dd-MMM-yyyy") + " - " + t.end_date.Value.ToString("dd-MMM-yyyy") + ")",
                            t.gross_sales,
                            t.readygoods_qnty,
                            t.readygoods_value,
                            str_start_date = t.start_date.Value.ToString("dd-MMM-yyyy"),
                            str_end_date = t.end_date.Value.ToString("dd-MMM-yyyy"),
                            //stroutlets = JsonConvert.SerializeObject(outlet_list),
                            txteventgross_sales = "<table><tr  style='background-color:transparent;'><td><label style='font-weight:normal;'>Gross Sales</label></td><td><input disabled type='currency' style='width: 135px;background-color: aliceblue;'  value=\"" + (t.event_gross_sales != null ? Decimal.Round(t.event_gross_sales.Value, 2).ToString() : "0") + "\"  class='txtgross_sales form-control '/></td></tr><tr  style='background-color:transparent;'><td><label style='font-weight:normal;'>Discount</label></td><td><input disabled min='0' event_id='" + t.event_id.ToString() + "' type='currency' style='width:135px;'  value=\"" + (t.event_discount_amount != null ? t.event_discount_amount.ToString() : "0") + "\" class='txtgross_discount form-control '/></td></tr><tr  style='background-color:transparent;'><td><label style='font-weight:normal;'>Net Amount</label></td><td><input disabled min='0' event_id='" + t.event_id.ToString() + "' type='currency' style='width:135px;' value=\"" + (t.event_net_amount != null ? t.event_net_amount.ToString() : "0") + "\"  class='txtgross_net form-control'/></td></tr> <table/>  ",

                            txtreadygoods_qnty = "<label style='font-weight:normal;'>Quantity</label> <br/> <input  min='0' event_id='" + t.event_id.ToString() + "' type='number' style='width: 135px'  value=\"" + (t.readygoods_qnty != null ? t.readygoods_qnty.ToString() : "0") + "\" class='txtreadygoods_qnty form-control '/> <label style='font-weight:normal;'>Value</label>  <input min='0' event_id='" + t.event_id.ToString() + "' type='number' style='width: 135px;' value=\"" + (t.readygoods_value != null ? t.readygoods_value.ToString() : "0") + "\"  class='txtreadygoods_value  form-control'/> ",
                            txtreadygoods_value = "  <input min='0' event_id='" + t.event_id.ToString() + "' type='number' style='width: 120px;' value=\"" + (t.readygoods_value != null ? t.readygoods_value.ToString() : "0") + "\"  class='txtreadygoods_value  form-control'/> ",
                            txtmonthlyhtml = GetMonthHtml(t, false).Result,
                            datatablebuttonscode = "<div class='btn-toolbar pull-right' role='toolbar' style='width:200px'>" +
                            "<button type='button' class='btn'  onclick=\"location.href=''\">Edit</button></div>" //objDTBtnPanel.genDTBtnPanel(message.Objconf_receivergroup.ControllerName, t.receivergroupid, "receivergroupid", _contextAccessor.HttpContext.User.Identity as ClaimsIdentity, btnActionList)
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count(), data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetAllSavedEventListData(DtParameters request)
        {

            var records = await _BP_service.rpc_event_tran_data_GetAll(request);
            //var monthlist = await _RPCDbService.GetAllget_bp_year_event_month_dataAsync(request.MasterID.Value);
            var data = (from t in records
                        select new
                        {
                            t.tran_bp_event_id,
                            season_event = t.event_title + "<br/>(" + t.start_date.Value.ToString("dd-MMM-yyyy") + " - " + t.end_date.Value.ToString("dd-MMM-yyyy") + ")",

                            t.gross_sales,
                            t.readygoods_qnty,
                            t.readygoods_value,
                            str_start_date = t.start_date.Value.ToString("dd-MMM-yyyy"),
                            str_end_date = t.end_date.Value.ToString("dd-MMM-yyyy"),

                            //stroutlets = JsonConvert.SerializeObject(outlet_list),
                            txteventgross_sales = "<table><tr  style='background-color:transparent;'><td><label style='font-weight:normal;'>Gross Sales</label></td><td><input disabled type='currency' style='width: 135px;background-color: aliceblue;'  value=\"" + (t.event_gross_sales != null ? Decimal.Round(t.event_gross_sales.Value, 2).ToString() : "0") + "\"  class='txtgross_sales form-control '/></td></tr><tr  style='background-color:transparent;'><td><label style='font-weight:normal;'>Discount</label></td><td><input disabled min='0' event_id='" + t.event_id.ToString() + "' type='currency' style='width:135px;'  value=\"" + (t.event_discount_amount != null ? Decimal.Round(t.event_discount_amount.Value,2).ToString() : "0") + "\" class='txtgross_discount form-control '/></td></tr><tr  style='background-color:transparent;'><td><label style='font-weight:normal;'>Net Amount</label></td><td><input disabled min='0' event_id='" + t.event_id.ToString() + "' type='currency' style='width:135px;' value=\"" + (t.event_net_amount != null ? Decimal.Round(t.event_net_amount.Value).ToString() : "0") + "\"  class='txtgross_net form-control'/></td></tr> <table/>  ",



                            // txtreadygoods_qnty = "<label style='font-weight:normal;'>Quantity</label> <br/> <input  min='0' event_id='" + t.event_id.ToString() + "' type='number' style='width: 100%;'  value=\"" + (t.readygoods_qnty != null ? t.readygoods_qnty.ToString() : "0") + "\" class='txtreadygoods_qnty form-control '/> <br/><label style='font-weight:normal;'>Value</label> <br/> <input min='0' event_id='" + t.event_id.ToString() + "' type='number' style='width: 100%;' value=\"" + (t.readygoods_value != null ? t.readygoods_value.ToString() : "0") + "\"  class='txtreadygoods_value  form-control'/> ",

                            txtevent_gross_discount = " <input disabled type='currency' style='width:135px;background-color: aliceblue;'  value=\"" + (t.event_discount_amount != null ? Decimal.Round(t.event_discount_amount.Value, 2).ToString() : "0") + "\" class='txtgross_discount border-gray-200 form-control '/> ",
                            txtevent_gross_net = " <input disabled type='currency' style='width:135px;background-color: aliceblue;'  value=\"" + (t.event_net_amount != null ? Decimal.Round(t.event_net_amount.Value, 2).ToString() : "0") + "\" class='txtgross_net border-gray-200 form-control '/> ",

                            // txtreadygoods_qnty = "  <input min='0' event_id='" + t.event_id.ToString() + "' type='number' style='width:100%'  value=\"" + (t.readygoods_qnty != null ? t.readygoods_qnty.ToString() : "0") + "\" class='txtreadygoods_qnty form-control '/> ",




                            //txtreadygoods_qnty = "<label style='font-weight:normal;'>ABP(MRP)</label>  <input min='0' event_id='" + t.event_id.ToString() + "' type='currency' style='width: 135px;' value=\"" + (t.readygoods_value != null ? t.readygoods_value.ToString() : "0") + "\"  class='txtreadygoods_value  form-control'/> <label style='font-weight:normal;'>ABP(CPU)</label>  <input min='0' event_id='" + t.event_id.ToString() + "' type='currency' style='width: 135px;' value=\"" + (t.readygoods_cpu != null ? t.readygoods_cpu.ToString() : "0") + "\"  class='txtreadygoods_cpu  form-control'/> <label style='font-weight:normal;'>ABP QTY(Pc)</label> <br/> <input  min='0' event_id='" + t.event_id.ToString() + "' type='currency_ext' style='width: 135px;'  value=\"" + (t.readygoods_qnty != null ? t.readygoods_qnty.ToString() : "0") + "\" class='txtreadygoods_qnty form-control '/> ",



                            txtreadygoods_qnty = "<table>" +
                                    "<tr style='background-color:transparent;'>" +
                                        "<td><label style='font-weight:normal;'>ABP(MRP)</label></td>" +
                                        "<td><input min='0' event_id='" + t.event_id.ToString() + "' type='currency' style='width: 135px;' value=\"" + (t.readygoods_value != null ? t.readygoods_value.ToString() : "0") + "\" class='txtreadygoods_value form-control'/></td>" +
                                    "</tr>" +
                                    "<tr style='background-color:transparent;'>" +
                                        "<td><label style='font-weight:normal;'>ABP(CPU)</label></td>" +
                                        "<td><input min='0' event_id='" + t.event_id.ToString() + "' type='currency' style='width: 135px;' value=\"" + (t.readygoods_cpu != null ? t.readygoods_cpu.ToString() : "0") + "\" class='txtreadygoods_cpu form-control'/></td>" +
                                    "</tr>" +
                                    "<tr style='background-color:transparent;'>" +
                                        "<td><label style='font-weight:normal;'>ABP QTY(Pc)</label></td>" +
                                        "<td><input min='0' event_id='" + t.event_id.ToString() + "' type='currency_ext' style='width: 135px;' value=\"" + (t.readygoods_qnty != null ? t.readygoods_qnty.ToString() : "0") + "\" class='txtreadygoods_qnty form-control'/></td>" +
                                    "</tr>" +
                                "</table>",




                            txtmonthlyhtml = GetMonthHtml(t, true).Result,

                            datatablebuttonscode = "<div class='btn-toolbar pull-right' role='toolbar' style='width:200px'>" +
                            "<button type='button' class='btn'  onclick=\"location.href=''\">Edit</button></div>" //objDTBtnPanel.genDTBtnPanel(message.Objconf_receivergroup.ControllerName, t.receivergroupid, "receivergroupid", _contextAccessor.HttpContext.User.Identity as ClaimsIdentity, btnActionList)
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count(), data);
            return Json(ret_obj);

        }


        public async Task<string> GetAllOutletData()
        {
            var outlet_list = await _gen_outlet_entity_service.GetAllAsync();

            return JsonConvert.SerializeObject(outlet_list);
        }

        [HttpPost]
        public async Task<string> GetAll_TranEventMonthlyOutletData([FromBody] FilterEntity request)
        {
            var outlet_list = await _tran_bp_eventmonthoutletservice.get_tran_bp_event_month_outletService_GetByTran_event_MonthIDs(request.MasterID);

            return JsonConvert.SerializeObject(outlet_list);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBusinessPlan([FromBody] BusinessPlanDTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;
            }

            request.is_submitted = false;
            request.is_approved = false;

            var ret = await _BP_service.SaveBusinessPlan(request);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Business Plan Successful" : "Business Plan Data Saving Failed")
            });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateBusinessPlan([FromBody] BusinessPlanDTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.updated_by = sec_object.userid.Value;
                request.added_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
                request.date_added = DateTime.Now;
            }

            request.is_submitted = false;
            request.is_approved = false;


            var ret = await _BP_service.UpdateBusinessPlan(request);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Business Plan Successful" : "Business Plan Data Saving Failed")
            });


        }

        [HttpPost]
        public async Task<IActionResult> SubmitBusinessPlanForApproval([FromBody] BusinessPlanDTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.updated_by = sec_object.userid.Value;
                request.added_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
                request.date_added = DateTime.Now;

            }

            request.is_submitted = true;
            request.is_approved = null;

            var ret = await _BP_service.UpdateBusinessPlan(request);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? "Business Plan Data Submitted for Approval Successful" : "Business Plan Data Submission for Approval Failed"),
                link = "/BusinessPlan/BusinessPlanApprovalView?param=" + clsUtil.EncodeString(request.tran_bp_year_id.ToString())
            });


        }

        [HttpPost]
        public async Task<IActionResult> BusinessPlanApproveReject([FromBody] BusinessPlanDTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.updated_by = sec_object.userid.Value;
                request.date_updated = DateTime.Now;

            }

            var obj_tran_bp_year = _mapper.Map<tran_bp_year_dto>(request);

            obj_tran_bp_year.added_by = request.event_monthly_outlet_list.FirstOrDefault().added_by;
            obj_tran_bp_year.date_added = request.event_monthly_outlet_list.FirstOrDefault().date_added;

            obj_tran_bp_year.is_submitted = request.is_approved == false ? false : true;
            obj_tran_bp_year.is_approved = request.is_approved;
            obj_tran_bp_year.approve_date = DateTime.Now;

            var ret = await _BP_service.UpdateApproveReject(obj_tran_bp_year);

            return Json(new ResultEntity
            {
                Status_Code = (ret == true ? "200" : "400"),
                Status_Result = (ret == true ? (request.is_approved == false ? "Business Plan has been Rejected" : "Business Plan has been Approval") : (request.is_approved == false ? "Business Plan Rejection Failed" : "Business Plan Approval Failed"))
            });


        }

    }
}
