using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.ViewComponents
{
    /// <summary>
    /// SideBarMenu
    /// </summary>
    public class DataFilterViewComponent : ViewComponent
    {
        //private IPostRepository repository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;

        /// <summary>
        /// AboutUsViewComponent
        /// </summary>
        public DataFilterViewComponent(IHttpContextAccessor contextAccessor,
            IRPCDbService rpc_db_service, IGenFiscalYearService gen_fiscal_year_Service,
           Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _config = config;
            _contextAccessor = contextAccessor;
            _gen_fiscal_year_Service = gen_fiscal_year_Service;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_config);

            _rpc_db_service = rpc_db_service;
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
                var fiscalyearlist = _gen_fiscal_year_Service.get_fiscal_year_GetAll().Result;

                fiscal_year_id = fiscalyearlist.Where(p => p.is_used == true).ToList().Count > 0 ?
                fiscalyearlist.Where(p => p.is_used == true).FirstOrDefault().fiscal_year_id : 0;
            }
            if (objFilter.fiscal_year_id > 0)
            {


                var objFlter = _rpc_db_service.GetAllproc_get_filter_itemsAsync(fiscal_year_id).Result;

                if (!String.IsNullOrEmpty(objFlter.genfiscalyear_list))
                {
                    var fiscal_year_list = JsonConvert.DeserializeObject<List<gen_fiscal_year_dto>>(objFlter.genfiscalyear_list);//await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

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
                }



                if (!String.IsNullOrEmpty(objFlter.genseasoneventconfig_list))
                {
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
                }


                if (!String.IsNullOrEmpty(objFlter.styleitemtype_list))
                {
                    var style_item_type_list = JsonConvert.DeserializeObject<List<style_item_type_DTO>>(objFlter.styleitemtype_list);

                    ViewBag.item_type_list = style_item_type_list
                        .Select(a =>
                                                           new SelectListItem
                                                           {
                                                               Text = a.style_item_type_name.ToString(),
                                                               Value = a.style_item_type_id.ToString()
                                                           }
                                                           ).ToList();
                }


                if (!String.IsNullOrEmpty(objFlter.styleproducttype_list))
                {
                    var product_type_list = JsonConvert.DeserializeObject<List<style_product_type_DTO>>(objFlter.styleproducttype_list); ;

                    ViewBag.product_type_list = product_type_list
                                            .Select(a =>
                                                           new SelectListItem
                                                           {
                                                               Text = a.style_product_type_name.ToString(),
                                                               Value = a.style_product_type_id.ToString()
                                                           }
                                                           ).ToList();
                }


                if (!String.IsNullOrEmpty(objFlter.stylegender_list))
                {

                    var gender_list = JsonConvert.DeserializeObject<List<style_gender_DTO>>(objFlter.stylegender_list);

                    ViewBag.gender_list = gender_list
                        .Select(a =>
                                                           new SelectListItem
                                                           {
                                                               Text = a.style_gender_name.ToString(),
                                                               Value = a.style_gender_id.ToString()
                                                           }
                                                           ).ToList();
                }

                if (!String.IsNullOrEmpty(objFlter.styleitemorigin_list))
                {
                    var origin_list = JsonConvert.DeserializeObject<List<style_item_origin_DTO>>(objFlter.styleitemorigin_list);

                    ViewBag.origin_list = origin_list
                                        .Select(a =>
                                                           new SelectListItem
                                                           {
                                                               Text = a.style_item_origin_name.ToString(),
                                                               Value = a.style_item_origin_id.ToString()

                                                           }
                                                           ).ToList();
                }

            }
            return View("~/Views/Shared/Components/DataFilter.cshtml");
        }

    }
}
