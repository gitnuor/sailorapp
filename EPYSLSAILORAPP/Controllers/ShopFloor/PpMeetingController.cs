using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Helper;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers
{
    public class PpMeetingController : ExtendedBaseController
    {
        private readonly ILogger<PpMeetingController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPpMeetingService _PpMeetingService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICommonService _commonService;



        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside PpMeetingController !");
            return View();
        }

        public PpMeetingController(
           IMapper mapper, ILogger<PpMeetingController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
            IPpMeetingService PpMeetingService,
            IRPCDbService rpc_db_service, ICommonService commonService) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _PpMeetingService = PpMeetingService;
            _configuration = configuration;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

            _commonService = commonService;
            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> PpMeetingLanding()
        {

            return View("~/Views/ShopFloor/PpMeeting/PpMeetingLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> PpMeetingNew()
        {
            tran_pp_meeting_DTO model = new tran_pp_meeting_DTO();

            {


                model.conducted_by = sec_object.username;



            }
            return PartialView("~/Views/ShopFloor/PpMeeting/_PpMeetingNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> PpMeetingEdit(string tran_pp_meeting_id)
        {

            string decoded_tran_pp_meeting_id = clsUtil.DecodeString(tran_pp_meeting_id);

            tran_pp_meeting_DTO model = new tran_pp_meeting_DTO();

            model = await _PpMeetingService.GetSingleAsync(Convert.ToInt64(decoded_tran_pp_meeting_id));

            return PartialView("~/Views/ShopFloor/PpMeeting/_PpMeetingEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> PpMeetingView(string tran_pp_meeting_id)
        {

            string decoded_tran_pp_meeting_id = clsUtil.DecodeString(tran_pp_meeting_id);

            tran_pp_meeting_DTO model = new tran_pp_meeting_DTO();

            model = await _PpMeetingService.GetSingleAsync(Convert.ToInt64(decoded_tran_pp_meeting_id));

            

            return PartialView("~/Views/ShopFloor/PpMeeting/_PpMeetingView.cshtml", model);

        }



        [HttpPost]
        public async Task<IActionResult> GetJoinedPpMeetingData(DtParameters request)
        {

            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;

            var records = await _PpMeetingService.GetAllJoined_TranPpMeetingAsync(request.Start, request.Length, request.event_id, request.fiscal_year_id);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.tran_pp_meeting_id,


                            t.meeting_conducted_date,
                            t.event_title,

                            t.techpack_number,
                            t.conducted_by,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +

                            "<button type='button' onclick='ViewTranPpMeeting(this)' class='btn btn-warning btnView'  tran_pp_meeting_id='" + clsUtil.EncodeString(t.tran_pp_meeting_id.ToString()) + "'><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                            "<button type='button' onclick='DeleteTranPpMeeting(\"" + clsUtil.EncodeString(t.tran_pp_meeting_id.ToString()) + "\")'  class='btn btn-danger btnDelete'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                            "</div>"

                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows : 0, data);
            return Json(ret_obj);

        }




        [HttpPost]
        public async Task<IActionResult> SavePpMeeting([FromBody] tran_pp_meeting_DTO request)
        {



            var ret = true;

            


            request.added_by = sec_object.userid.Value;
            request.meeting_conducted_by = sec_object.userid.Value;
            request.date_added = DateTime.Now;
            request.fiscal_year_id = Fiscal_Year_ID;
            request.event_id = Event_ID;
           





            try
            {
                var model = JsonConvert.DeserializeObject<tran_pp_meeting_DTO>(JsonConvert.SerializeObject(request));

                ret = await _PpMeetingService.SaveAsync(model);

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
        public async Task<IActionResult> UpdatePpMeeting([FromBody] tran_pp_meeting_DTO request)
        {
            var ret = true;




            {


                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

                request.updated_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
                request.fiscal_year_id = Fiscal_Year_ID;
                request.event_id = Event_ID;
            }

            try
            {
                var model = JsonConvert.DeserializeObject<tran_pp_meeting_entity>(JsonConvert.SerializeObject(request));

                ret = await _PpMeetingService.UpdateAsync(model);

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
        public async Task<IActionResult> DeletePpMeeting([FromBody] tran_pp_meeting_DTO request)
        {


            var ret = true;


            {


                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

            }

            try
            {

                string decoded_tran_pp_meeting_id = clsUtil.DecodeString(request.strMasterID);

                request.tran_pp_meeting_id = Convert.ToInt64(decoded_tran_pp_meeting_id);

                ret = await _PpMeetingService.DeleteAsync(request.tran_pp_meeting_id);

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



        //public string UploadImage(tran_pp_meeting_DTO model)
        //{
        //    string uniqueFileName= string.Empty;
        //    if(model.imageFormFile != null)
        //    {
        //        string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath,"UploadFolder/ppMeeting/");
        //        uniqueFileName = Guid.NewGuid().ToString()+"_"+model.imageFormFile.FileName;
        //        string filePath=Path.Combine(uploadFolder, uniqueFileName);
        //        using (var fileStream=new FileStream(filePath, FileMode.Create))
        //        {
        //            model.imageFormFile.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}

    }
}

