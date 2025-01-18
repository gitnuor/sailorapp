using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.CustomIdentityManagers;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Enum;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Reporting.NETCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using System.Text;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class FabricPoController : ExtendedBaseController
    {
        private readonly ILogger<FabricPoController> _logger;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly HelperService _HelperService;
        private readonly ITranScmPoService _TranScmPoService;
        private readonly ITranPurchaseRequisitionService _TranPurchaseRequisitionService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserManager<owin_user_DTO> _userManager;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private readonly IGenUnitService _GenUnitService;
        private readonly IGenTermAndConditionsService _genTermAndConditionsService;
        private readonly IWebHostEnvironment _env;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside Fabric PO Controller !");
            return View();
        }

        public FabricPoController(ITranScmPoService TranScmPoService, IWebHostEnvironment env,
           IMapper mapper, ILogger<FabricPoController> logger, ApplicationUserManager<owin_user_DTO> userManager, IHttpContextAccessor contextAccessor, IGenSupplierInformationService GenSupplierInformationService, IConfiguration configuration,
           IGenUnitService GenUnitService,
            IFabricPoService FabricPoService, HelperService HelperService,
            IRPCDbService rpc_db_service, ITranPurchaseRequisitionService TranPurchaseRequisitionService, IGenTermAndConditionsService genTermAndConditionsService) : base(contextAccessor, configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _TranPurchaseRequisitionService = TranPurchaseRequisitionService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            _TranScmPoService = TranScmPoService;
            _genTermAndConditionsService = genTermAndConditionsService;
            _GenSupplierInformationService = GenSupplierInformationService;
            _GenUnitService = GenUnitService;
            _contextAccessor = contextAccessor;
            _HelperService = HelperService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> FabricPoLanding()
        {


            return View("~/Views/SCM/FabricPo/FabricPoLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> FabricPoNew()
        {


            tran_scm_po_DTO model = new tran_scm_po_DTO();
            model.event_id = objFilter.event_id;
            model.item_structure_group_id = 1;
            model.po_date = DateTime.Now;
            List<gen_term_and_conditions_DTO> termConditionList = await _genTermAndConditionsService.GetAllAsync();

            ViewBag.termConditionList =
                termConditionList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.term_condition_name.ToString(),
                        Value = a.gen_term_and_conditions_id.ToString()
                    }).ToList();


            return PartialView("~/Views/SCM/FabricPo/_FabricPoNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> FabricPoEdit(string po_id)
        {

            string decoded_po_id = clsUtil.DecodeString(po_id);

            tran_scm_po_DTO model = new tran_scm_po_DTO();

            model = await _TranScmPoService.GetSingleAsync(Convert.ToInt64(decoded_po_id));

            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));

            model.concern_Person = sup.List_concern_person;

            ViewBag.Supplier_Concern_Person = model.concern_Person.ToList().Select(a => new SelectListItem
            {
                Text = a.name.ToString(),
                Value=a.name.ToString()

            }).ToList();

            model.ddlsupplier_info = new dropdown_entity()
            {
                id = model.supplier_id.ToString(),
                text = sup.name.ToString()
            };

            List<gen_term_and_conditions_DTO> termConditionList = await _genTermAndConditionsService.GetAllAsync();

            ViewBag.termConditionList =
                termConditionList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.term_condition_name.ToString(),
                        Value = a.gen_term_and_conditions_id.ToString()
                    }).ToList();


            return PartialView("~/Views/SCM/FabricPo/_FabricPoEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> FabricPoView(string po_id)
        {

            string decoded_po_id = clsUtil.DecodeString(po_id);

            tran_scm_po_DTO model = new tran_scm_po_DTO();

            model = await _TranScmPoService.GetSingleAsync(Convert.ToInt64(decoded_po_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));


            model.selected_supplier_name = sup.name;
            //model.delivery_unit_name = du.FirstOrDefault().unit_name;
            return PartialView("~/Views/SCM/FabricPo/_FabricPoView.cshtml", model);

        }

        public async Task<IActionResult> FabricPoSubmittedView(string po_id)
        {

            string decoded_po_id = clsUtil.DecodeString(po_id);
            SecurityCapsule sec_object = new SecurityCapsule();

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);
            }


            tran_scm_po_DTO model = new tran_scm_po_DTO();

            model = await _TranScmPoService.GetSingleAsync(Convert.ToInt64(decoded_po_id));
            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));
            // var du = await _GenUnitService.GetAsync(Convert.ToInt64(model.delivery_unit));
            model.selected_supplier_name = sup.name;
            //model.delivery_unit_name = du.FirstOrDefault().unit_name;

            return PartialView("~/Views/SCM/FabricPo/_FabricPoSubmittedView.cshtml", model);

        }
        [HttpPost]
        public async Task<IActionResult> GetFabricPoData(DtParameters request)
        {


            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranScmPoService.GetAllFabricsPoAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, 0,request.Search.Value);


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
                            t.pr_no,
                            t.event_title,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='EditFabricPo(this)' class='btn btn-primary btnEdit'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='ViewFabricPo(this)' class='btn btn-warning btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='ViewPDFFabricPo(this)' class='btn btn-success btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-sticky-note' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteFabricPo(\"" + clsUtil.EncodeString(t.po_id.ToString()) + "\")' class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }


        public async Task<IActionResult> GetFabricPoSubmittedData(DtParameters request)
        {
            try
            {

                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;


                var records = await _TranScmPoService.GetAllFabricsPoAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, 1,request.Search.Value);

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
                                t.pr_no,
                                t.event_title,
                                datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                                "<button type='button' onclick='ViewFabricPoSubmitted(this)'  class='btn btn-warning btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                                "<button type='button' onclick='ViewPDFFabricPo(this)' class='btn btn-success btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-sticky-note' aria-hidden='true'></i></button>" +

                                "</div>"
                            }).ToList();
                var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
                return Json(ret_obj);

            }
            catch (Exception)
            {

                throw;
            }

        }



        [HttpPost]
        public async Task<IActionResult> SaveFabricPo([FromBody] tran_scm_po_DTO request)
        {
            var ret = true;

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
            request.added_by = sec_object.userid.Value;
            request.date_added = DateTime.Now;



            try
            {
                var model = JsonConvert.DeserializeObject<tran_scm_po_entity>(JsonConvert.SerializeObject(request));

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
        public async Task<IActionResult> UpdateFabricPo([FromBody] tran_scm_po_DTO request)
        {
            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

          // request.event_id = objFilter.event_id;

          // request.fiscal_year_id = objFilter.fiscal_year_id;


            try
            {
                List<tran_scm_po_details_entity> detail = JsonConvert.DeserializeObject<List<tran_scm_po_details_entity>>(JsonConvert.SerializeObject(request.List_po_details));

                ret = await _TranScmPoService.UpdateAsync(request);

                return Json(new ResultEntity
                {
                    Status_Code = (ret == true ? "200" : "400"),
                    Status_Result = (ret == true ? "Successfully Updated" : "Data Saving Failed")
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
        public async Task<JsonResult> GetPRDetails(long pr_id)
        {
            var pr = await _TranPurchaseRequisitionService.GetPR(pr_id);
            pr.supplier_name = pr.suggested_supplier_name;

            return Json(new { pr = pr, data = pr.details });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFabricPo([FromBody] tran_scm_po_DTO request)
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
        public async Task<IActionResult> SendForApprovalFabricPo([FromBody] tran_scm_po_DTO request)
        {
            var ret = true;

            request.updated_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;

            try
            {

                ret = await _TranScmPoService.SendForApproval(request);

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


        public async Task<IActionResult> GeneratePdf(string po_id)
        {

            string decoded_po_id = clsUtil.DecodeString(po_id);

            tran_scm_po_DTO model = new tran_scm_po_DTO();

            model = await _TranScmPoService.GetSingleAsync(Convert.ToInt64(decoded_po_id));

            var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));

            model.ddlsupplier_info = new dropdown_entity()
            {
                id = model.supplier_id.ToString(),
                text = sup.name.ToString()
            };

            var bytearrReport = GenerateReport(model);

            String base64 = Convert.ToBase64String(bytearrReport);

            return Json(new ResultEntity
            {
                Status_Code = "200",
                Status_Result = base64
            });
        }

        private byte[] GenerateReport(tran_scm_po_DTO masterdata)
        {
            //string fileDirPath = _webhost.ContentRootPath; //Assembly.GetExecutingAssembly().Location.Replace("WebAdmin.dll", string.Empty);
            string fileDirPath = _env.WebRootPath; //Assembly.GetExecutingAssembly().Location.Replace("WebAdmin.dll", string.Empty);
            string rdlcFilePath = string.Format("{0}\\Reports\\SCM\\FabricPo\\RDLC\\FabricPo.rdlc", fileDirPath);

            var list = new List<tran_scm_po_DTO>();
            list.Add(masterdata);

            string FileName = "FabricPo_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
            string extension;
            string encoding;
            string mimeType;
            string[] streams;
            Warning[] warnings;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1252");

            LocalReport report = new LocalReport();
            report.ReportPath = rdlcFilePath;
            report.EnableExternalImages = true;

            #region Enable External Images in report

            #endregion
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = list;

            ReportDataSource rds2 = new ReportDataSource();
            rds2.Name = "DataSet2";
            rds2.Value = masterdata.List_po_details;


            ReportDataSource rds3 = new ReportDataSource();
            rds3.Name = "DataSet3";
            rds3.Value = masterdata.terms_and_conditions_list;

            report.DataSources.Add(rds);
            report.DataSources.Add(rds2);
            report.DataSources.Add(rds3);

            ReportParameter[] parameters = new ReportParameter[1];

            // Create the parameter and set its value
            parameters[0] = new ReportParameter("AmountInWord", EPYSLSAILORAPP.HelperUtility.HelperUtility.NumberToWordsConverter.ConvertNumberToWords(Convert.ToInt64(masterdata.final_amount)));

            // Set the parameters to the report
            report.SetParameters(parameters);

            var result = report.Render("pdf", null,
                            out extension, out encoding,
                            out mimeType, out streams, out warnings);
            return result;
        }


        public async Task<IActionResult> GetTermsAndConditionsDetails(Int64 term_and_conditions_id)
        {



            var data = await _genTermAndConditionsService.GetTermsandConditionDetail(term_and_conditions_id);
            //return Json(data);
            return PartialView("~/Views/SCM/FabricPo/_termandConditionsDetail.cshtml", data);


        }

        [HttpPost]
        public async Task<IActionResult> GetFabricPoReviseData(DtParameters request)
        {


            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _TranScmPoService.GetAllFabricsPoReviseAsync(request.Start, request.Length, request.fiscal_year_id, request.event_id, request.supplier_id, request.delivery_unit_id, Convert.ToUInt32(StatusFalg.is_revised));


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
                            t.pr_no,
                            t.event_title,



                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='EditFabricPoRevise(this)' class='btn btn-primary btnEdit'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='ViewFabricRevisePo(this)' class='btn btn-warning btnView'  po_id='" + clsUtil.EncodeString(t.po_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }


        [HttpGet]
        public async Task<IActionResult> FabricPoReviseEdit(string po_id)
        {

            string decoded_po_id = clsUtil.DecodeString(po_id);

            tran_scm_po_DTO model = new tran_scm_po_DTO();

            model = await _TranScmPoService.GetSingleReviseAsync(Convert.ToInt64(decoded_po_id));

            //var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));

            model.ddlsupplier_info = new dropdown_entity()
            {
                id = model.supplier_id.ToString(),
                text =model.suggested_supplier_name.ToString() //sup.name.ToString()
            };

            List<gen_term_and_conditions_DTO> termConditionList = await _genTermAndConditionsService.GetAllAsync();

            ViewBag.termConditionList =
                termConditionList.ToList().Select(a =>
                    new SelectListItem
                    {
                        Text = a.term_condition_name.ToString(),
                        Value = a.gen_term_and_conditions_id.ToString()
                    }).ToList();


            return PartialView("~/Views/SCM/FabricPo/_FabricPoReviseEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> FabricPoReviseView(string po_id)
        {

            string decoded_po_id = clsUtil.DecodeString(po_id);

            tran_scm_po_DTO model = new tran_scm_po_DTO();
 
            model = await _TranScmPoService.GetSingleReviseAsync(Convert.ToInt64(decoded_po_id));
           // var sup = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(model.supplier_id));


           // model.selected_supplier_name = sup.name;
            //model.delivery_unit_name = du.FirstOrDefault().unit_name;
            return PartialView("~/Views/SCM/FabricPo/_FabricPoReviseView.cshtml", model);

        }



        [HttpGet]
        public async Task<IActionResult> GetConcernPersons(long supplier_id)
        {
            var concernPersons = await _TranScmPoService.GetConcernPersonsAsync(supplier_id);
            return Json(concernPersons);
        }



    }
}

