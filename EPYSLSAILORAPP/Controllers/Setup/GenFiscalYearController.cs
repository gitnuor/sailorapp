using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Application.DTO;
using System.Security.Claims;
using EPYSLSAILORAPP.Application.Interface;
using BDO.Core.Base;
using Web.Core.Frame.Helpers;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.SystemServices;
using EPYSLSAILORAPP.Domain.DTO;
using ServiceStack;
using Serilog.Context;

using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Domain.Entity.GenTables;

namespace EPYSLSAILORAPP.Controllers
{
    public class GenFiscalYearController : ExtendedBaseController
    {
        private readonly ILogger<GenFiscalYearController> _logger;

        private readonly IGenFiscalYearService _GenFiscalYearService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        //private readonly IConfiguration _configuration;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenColorController !");
            return View();
        }

        public GenFiscalYearController(
           IMapper mapper, ILogger<GenFiscalYearController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            IGenFiscalYearService GenFiscalYearService,
            IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenFiscalYearService = GenFiscalYearService;
            //_configuration = configuration;
           // objHelperService = new HelperService(_contextAccessor);

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> GenFiscalYearLanding()
        {

            return View("~/Views/Setup/GenFiscalYear/GenFiscalYearLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenFiscalYearControllerNew()
        {
            gen_fiscal_year_dto model = new gen_fiscal_year_dto();
            model.start_date= DateTime.Now;
            model.end_date= DateTime.Now;

            return PartialView("~/Views/Setup/GenFiscalYear/_GenFiscalYearNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenFiscalYearEdit(string fiscal_year_id)
        {

            string decoded_fiscal_year_id = clsUtil.DecodeString(fiscal_year_id);

            gen_fiscal_year_dto model = new gen_fiscal_year_dto();

            var objmodel = await _GenFiscalYearService.GetSingleAsync(Convert.ToInt64(decoded_fiscal_year_id));

            model = JsonConvert.DeserializeObject<gen_fiscal_year_dto>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenFiscalYear/_GenFiscalYearEdit.cshtml", model);

        }

      
        public async Task<IActionResult> GenFiscalYearView(string fiscal_year_id)
        {

            string decoded_fiscal_year_id = clsUtil.DecodeString(fiscal_year_id);

            gen_fiscal_year_dto model = new gen_fiscal_year_dto();

            var objmodel = await _GenFiscalYearService.GetSingleAsync(Convert.ToInt64(decoded_fiscal_year_id));

            model = JsonConvert.DeserializeObject<gen_fiscal_year_dto>(JsonConvert.SerializeObject(objmodel));

            return PartialView("~/Views/Setup/GenFiscalYear/_GenFiscalYearView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetGenFiscalYearData(DtParameters request)
        {
           

            
            var records = await _GenFiscalYearService.GetAllPagedDataAsync(request);




            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.fiscal_year_id,
                            t.year_no,
                            t.year_name,
                            t.start_date,
                            t.end_date,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenFiscalYear(this)' style='width: 48px;' class='btn btn-secondary btnEdit'  fiscal_year_id='" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "'><i class='fa fa-edit' aria-hidden='true'></i></button>" +

                            "<button type='button' onclick='ViewGenFiscalYear(this)' style='width: 48px;' class='btn btn-secondary btnView'  fiscal_year_id='" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "'><i class='fa fa-eye'></i></button>" +

                            "<button type='button' onclick='DeleteGenFiscalYear(\"" + clsUtil.EncodeString(t.fiscal_year_id.ToString()) + "\")' style='width: 48px;' class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.FirstOrDefault().total_rows.Value, data);
            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> SaveGenFiscalYear([FromBody] gen_fiscal_year_dto request)
        {
            var ret = true;


            request.added_by = sec_object.userid.Value;

            request.date_added = DateTime.Now;

            try
            {
                var model = JsonConvert.DeserializeObject<gen_fiscal_year_dto>(JsonConvert.SerializeObject(request));

                ret = await _GenFiscalYearService.SaveAsync(model);

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
        public async Task<IActionResult> UpdateGenFiscalYear([FromBody] gen_fiscal_year_dto request)
        {
            var ret = true;



            

            request.update_by = sec_object.userid.Value;

            request.date_updated = DateTime.Now;


            try
            {
                var model = JsonConvert.DeserializeObject<gen_fiscal_year>(JsonConvert.SerializeObject(request));
                var olddata = await _GenFiscalYearService.GetSingleAsync(request.fiscal_year_id);
                model.date_added=olddata.date_added;
                model.added_by = olddata.added_by;

                ret = await _GenFiscalYearService.UpdateAsync(model);

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
        public async Task<IActionResult> DeleteFiscalYear([FromBody] gen_fiscal_year_dto request)
        {


            var ret = true;


           // request.added_by = sec_object.userid.Value;

            //request.date_added = DateTime.Now;



            try
            {

                string decoded_fiscal_year_id = clsUtil.DecodeString(request.strMasterID);

                request.fiscal_year_id = Convert.ToInt64(decoded_fiscal_year_id);

                ret = await _GenFiscalYearService.DeleteAsync(request.fiscal_year_id);

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


    }
}

