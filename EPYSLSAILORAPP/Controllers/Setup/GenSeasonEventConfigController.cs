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

namespace EPYSLSAILORAPP.Controllers
{
	public class GenSeasonEventConfigController : ExtendedBaseController
	{
		private readonly ILogger<GenSeasonEventConfigController> _logger;

		private readonly IGenSeasonEventConfigService _GenSeasonEventConfigService;

		private readonly IRPCDbService _rpc_db_service;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly IConfiguration _configuration;
		private HelperService objHelperService;

		public IActionResult Index()
		{
			_logger.LogInformation("Hello from inside GenSeasonEventConfigController !");
			return View();
		}

		public GenSeasonEventConfigController(
		   IMapper mapper, ILogger<GenSeasonEventConfigController> logger, IHttpContextAccessor contextAccessor, IConfiguration configuration,
			IGenSeasonEventConfigService GenSeasonEventConfigService,
			IRPCDbService rpc_db_service) : base(contextAccessor, configuration)
		{
			_mapper = mapper;

			_logger = logger;
			_rpc_db_service = rpc_db_service;
			_contextAccessor = contextAccessor;
			_GenSeasonEventConfigService = GenSeasonEventConfigService;
			_configuration = configuration;




		}

		[HttpGet]
		public async Task<IActionResult> GenSeasonEventConfigLanding()
		{

			return View("~/Views/Setup/GenSeasonEventConfig/GenSeasonEventConfigLanding.cshtml");
		}

		[HttpGet]
		public async Task<IActionResult> GenSeasonEventConfigNew()
		{
			gen_season_event_config_DTO model = new gen_season_event_config_DTO();

			return PartialView("~/Views/Business Plan/GenSeasonEventConfig/_GenSeasonEventConfigNew.cshtml", model);

		}

		[HttpGet]
		public async Task<IActionResult> GenSeasonEventConfigEdit(string event_id)
		{

			string decoded_event_id = clsUtil.DecodeString(event_id);

			gen_season_event_config_DTO model = new gen_season_event_config_DTO();

			var objmodel = await _GenSeasonEventConfigService.GetSingleAsync(Convert.ToInt64(decoded_event_id));

			model = JsonConvert.DeserializeObject<gen_season_event_config_DTO>(JsonConvert.SerializeObject(objmodel));

			return PartialView("~/Views/Business Plan/GenSeasonEventConfig/_GenSeasonEventConfigEdit.cshtml", model);

		}

		[HttpGet]
		public async Task<IActionResult> GenSeasonEventConfigView(string event_id)
		{

			string decoded_event_id = clsUtil.DecodeString(event_id);

			gen_season_event_config_DTO model = new gen_season_event_config_DTO();

			var objmodel = await _GenSeasonEventConfigService.GetSingleAsync(Convert.ToInt64(decoded_event_id));

			model = JsonConvert.DeserializeObject<gen_season_event_config_DTO>(JsonConvert.SerializeObject(objmodel));

			return PartialView("~/Views/Business Plan/GenSeasonEventConfig/_GenSeasonEventConfigView.cshtml", model);

		}


		[HttpPost]
		public async Task<IActionResult> GetGenSeasonEventConfigData(DtParameters request)
		{

			var records = await _GenSeasonEventConfigService.GetAllPagedDataAsync(request);

			var index = request.Start + 1;
			var data = (from t in records
						select new
						{
							row_index = index++,
							t.event_id,
							t.season_id,
							t.fiscal_year_id,
							t.start_date,
							t.start_month_id,
							t.end_month_id,
							t.added_by,
							t.updated_by,
							t.date_added,
							t.event_title,
							t.is_active,
							t.event_sequence,
							t.end_date,
							t.date_updated,
							t.year_name,
							t.season_name,



							datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
							"<button type='button' onclick='EditGenSeasonEventConfig(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  event_id='" + clsUtil.EncodeString(t.event_id.ToString()) + "'>Edit</button>" +
							"<button type='button' onclick='ViewGenSeasonEventConfig(this)' style='width: 120px;' class='btn btn-secondary btnView'  event_id='" + clsUtil.EncodeString(t.event_id.ToString()) + "'>View</button>" +
							"<button type='button' onclick='DeleteGenSeasonEventConfig(\"" + clsUtil.EncodeString(t.event_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
							"</div>"
						}).ToList();
			var ret_obj = new AjaxResponse(records.Count, data);
			return Json(ret_obj);

		}

		[HttpPost]
		public async Task<IActionResult> GetJoinedGenSeasonEventConfigData(DtParameters request)
		{

			var records = await _GenSeasonEventConfigService.GetAllJoined_GenSeasonEventConfigAsync(request.Start, request.Length);

			var index = request.Start + 1;
			var data = (from t in records
						select new
						{
							row_index = index++,
							t.event_title,


							datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
							"<button type='button' onclick='EditGenSeasonEventConfig(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  event_id='" + clsUtil.EncodeString(t.event_id.ToString()) + "'>Edit</button>" +
							"<button type='button' onclick='ViewGenSeasonEventConfig(this)' style='width: 120px;' class='btn btn-secondary btnView'  event_id='" + clsUtil.EncodeString(t.event_id.ToString()) + "'>View</button>" +
							"<button type='button' onclick='DeleteGenSeasonEventConfig(\"" + clsUtil.EncodeString(t.event_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
							"</div>"

						}).ToList();
			var ret_obj = new AjaxResponse(records.Count, data);
			return Json(ret_obj);

		}




		[HttpPost]
		public async Task<IActionResult> SaveGenSeasonEventConfig([FromBody] gen_season_event_config_DTO request)
		{


			var ret = true;


			request.added_by = sec_object.userid.Value;

			request.date_added = DateTime.Now;




			try
			{
				var model = JsonConvert.DeserializeObject<gen_season_event_config_entity>(JsonConvert.SerializeObject(request));

				ret = await _GenSeasonEventConfigService.SaveAsync(model);

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
		public async Task<IActionResult> UpdateGenSeasonEventConfig([FromBody] gen_season_event_config_DTO request)
		{
			var ret = true;



			request.added_by = sec_object.userid.Value;

			request.date_added = DateTime.Now;

			request.updated_by = sec_object.userid.Value;

			request.date_updated = DateTime.Now;


			try
			{
				var model = JsonConvert.DeserializeObject<gen_season_event_config_entity>(JsonConvert.SerializeObject(request));

				ret = await _GenSeasonEventConfigService.UpdateAsync(model);

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
		public async Task<IActionResult> DeleteGenSeasonEventConfig([FromBody] gen_season_event_config_DTO request)
		{


			var ret = true;


			request.added_by = sec_object.userid.Value;

			request.date_added = DateTime.Now;



			try
			{

				string decoded_event_id = clsUtil.DecodeString(request.strMasterID);

				request.event_id = Convert.ToInt64(decoded_event_id);

				ret = await _GenSeasonEventConfigService.DeleteAsync(request.event_id);

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

