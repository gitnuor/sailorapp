using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using EPYSLSAILORAPP.Domain.DTO;

namespace EPYSLSAILORAPP.ViewComponents
{
	public class DataFilterMCDViewComponent: ViewComponent
	{
		private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly IRPCDbService _rpc_db_service;
		private readonly IGenUnitService _GenUnitService;
		private readonly IGenItemStructureGroupService _GenItemStrucgrpsvc;
		private readonly IGenItemStructureGroupSubService _GenItemStrucgrpsubsvc;
		private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;


        public DataFilterMCDViewComponent(IHttpContextAccessor contextAccessor, IGenUnitService GenUnitService, IGenItemStructureGroupService genItemStrucgrpsvc, IGenItemStructureGroupSubService genItemStrucgrpsubsvc,
			IRPCDbService rpc_db_service, IGenFiscalYearService gen_fiscal_year_Service,

           Microsoft.Extensions.Configuration.IConfiguration config)
		{
			_config = config;
			_contextAccessor = contextAccessor;
			_redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_config);
			_GenUnitService = GenUnitService;
			_GenItemStrucgrpsvc=genItemStrucgrpsvc;
			_GenItemStrucgrpsubsvc = genItemStrucgrpsubsvc;
			_rpc_db_service = rpc_db_service;
			_gen_fiscal_year_Service = gen_fiscal_year_Service;

        }


		public IViewComponentResult Invoke()
		{
			GenSeasonEventConfigurationDTO objFilter = new GenSeasonEventConfigurationDTO();

            Int64? fiscal_year_id;

            if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
            {
                objFilter = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();
                fiscal_year_id = objFilter?.fiscal_year_id;
            }
            else
            {
                var objfiscal_year = _gen_fiscal_year_Service.get_fiscal_year_GetAll().Result;

                fiscal_year_id = objfiscal_year.Where(p => p.is_used == true).ToList().Count > 0 ? objfiscal_year.Where(p => p.is_used == true).FirstOrDefault().fiscal_year_id : 0;
            }


            var objFlter = _rpc_db_service.GetAllproc_get_filter_itemsAsync(fiscal_year_id).Result;

			var fiscal_year_list = JsonConvert.DeserializeObject<List<gen_fiscal_year_dto>>(objFlter.genfiscalyear_list);

			ViewBag.fiscal_year_id = fiscal_year_id.ToString();
			ViewBag.fiscal_year_list = fiscal_year_list.ToList()
										 .Select(a =>
												   new SelectListItem
												   {
													   Text = a.year_name.ToString(),
													   Value = a.fiscal_year_id.ToString(),
													   Selected = a.fiscal_year_id == fiscal_year_id ? true : false
												   }
												   ).ToList();

			var gen_event_list = JsonConvert.DeserializeObject<List<gen_season_event_config>>(objFlter.genseasoneventconfig_list);


			ViewBag.event_id = objFilter.event_id.ToString();
			ViewBag.event_list = gen_event_list
									.Select(a =>
												   new SelectListItem
												   {
													   Text = a.event_title.ToString(),
													   Value = a.event_id.ToString(),
													   Selected = a.event_id == objFilter.event_id ? true : false

												   }
												   ).ToList();


			var gen_item_structure_group_list = _GenItemStrucgrpsvc.GetAllAsync();

			ViewBag.gen_item_structure_group_list = gen_item_structure_group_list.Result
				.Select(a =>
												   new SelectListItem
												   {
													   Text = a.structure_group_name.ToString(),
													   Value = a.item_structure_group_id.ToString()
												   }
												   ).ToList();

			var gen_item_structure_group_sub_list= _GenItemStrucgrpsubsvc.GetAllAsync();

			ViewBag.gen_item_structure_group_sub_list= gen_item_structure_group_sub_list.Result
				.Select(a =>
												   new SelectListItem
												   {
													   Text = a.sub_group_name.ToString(),
													   Value = a.gen_item_structure_group_sub_id.ToString()
												   }
												   ).ToList();



			return View("~/Views/Shared/Components/DataFilterMCD.cshtml");
		}


	}
}
