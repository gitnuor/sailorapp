using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Serilog.Context;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers.Configuration
{
    public class ProductConfigController : BaseController
    {
        private readonly ILogger<ProductConfigController> _logger;
        private readonly IConfiguration _configuration;


        private readonly IStylecategoryService _StylecategoryService;
        private readonly IStylegenderService _StylegenderService;
        private readonly IStyleitemoriginService _StyleitemoriginService;

        private readonly IStyleitemproductService _StyleitemproductService;
        private readonly IStyleitemproductsubcategoryService _StyleitemproductsubCategoryService;

        private readonly IStyleitemtypeService _StyleitemtypeService;
        private readonly IStylelabelService _StylelabelService;
        private readonly IStylemastercategoryService _StylemastercategoryService;
        private readonly IStyleproducttypeService _StyleproducttypeService;
        private readonly IStyleproductsizegroupService _StyleproductsizeeService;

        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly IRPCDbService _rpc_db_service;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside ProductConfigController !");
            return View();
        }

        public ProductConfigController(
            IGenFiscalYearService gen_fiscal_year_Service
             , IMapper mapper, ILogger<ProductConfigController> logger, IHttpContextAccessor contextAccessor,
            IStylecategoryService StylecategoryService,
            IStylegenderService StylegenderService,
            IStyleitemoriginService StyleitemoriginService,
            IStyleitemproductService StyleitemproductService,
            IStyleitemproductsubcategoryService StyleitemproductsubcategoryService,
            IStyleitemtypeService StyleitemtypeService,
            IStylelabelService StylelabelService,
            IStylemastercategoryService StylemastercategoryService,
            IStyleproducttypeService StyleproducttypeService,
            IStyleproductsizegroupService StyleproductsizeeService,
            IRPCDbService rpc_db_service, IConfiguration configuration)
        {

            _mapper = mapper;
            _logger = logger;

            _gen_fiscal_year_Service = gen_fiscal_year_Service;

            _rpc_db_service = rpc_db_service;

            _contextAccessor = contextAccessor;

            _StylecategoryService = StylecategoryService;
            _StylegenderService = StylegenderService;
            _StyleitemoriginService = StyleitemoriginService;

            _StyleitemproductService = StyleitemproductService;
            _StyleitemproductsubCategoryService = StyleitemproductsubcategoryService;


            _StyleitemtypeService = StyleitemtypeService;
            _StylelabelService = StylelabelService;
            _StylemastercategoryService = StylemastercategoryService;
            _StyleproducttypeService = StyleproducttypeService;
            _StyleproductsizeeService = StyleproductsizeeService;

            _logger.LogInformation("Hello from inside RangePlan !");

            _configuration = configuration;

            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);

            

            _contextAccessor = contextAccessor;
            _logger.LogInformation("Hello from inside ProductConfigController !");
        }

        [HttpGet]
        public async Task<IActionResult> ProductConfigLanding()
        {

            return View("~/Views/Configuration/ProductConfig/ProductConfigLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> ProductConfigNew(string fiscal_year_id)
        {
            

            style_item_product_DTO model = new style_item_product_DTO();           

      

            var style_item_type_list = await _StyleitemtypeService.GetAllAsync();

            model.style_item_type_List = (List<SelectListItem>)style_item_type_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_item_type_name.ToString(),
                                                       Value = a.style_item_type_id.ToString()
                                                   }
                                                   ).ToList();


            var product_type_list = await _StyleproducttypeService.GetAllAsync();

            model.style_product_type_List = (List<SelectListItem>)product_type_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_product_type_name.ToString(),
                                                       Value = a.style_product_type_id.ToString()
                                                   }
                                                   ).ToList();


            var gender_list = await _StylegenderService.GetAllAsync();

            model.style_gender_List = (List<SelectListItem>)gender_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_gender_name.ToString(),
                                                       Value = a.style_gender_id.ToString()
                                                   }
                                                   ).ToList();

            var origin_list = await _StyleitemoriginService.GetAllAsync();

            model.style_item_origin_List = (List<SelectListItem>)origin_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_item_origin_name.ToString(),
                                                       Value = a.style_item_origin_id.ToString()

                                                   }
                                                   ).ToList();

            var sizegroup_list = await _StyleproductsizeeService.GetAllAsync();

            model.style_product_size_group_List = (List<SelectListItem>)sizegroup_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_product_size_group_name.ToString(),
                                                       Value = a.style_product_size_group_id.ToString()

                                                   }
                                                   ).ToList();
            model.DetList = new List<style_item_product_sub_category_DTO>();
            return PartialView("~/Views/Configuration/ProductConfig/_ProductConfigNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> ProductConfigEdit(string product_id)
        {
            style_item_product_DTO model = new style_item_product_DTO();

            string decoded_product_id = clsUtil.DecodeString(product_id);

            model = await _StyleitemproductService.GetAsync(Convert.ToInt64(decoded_product_id));

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();

            model.gen_fiscal_year_List = (List<SelectListItem>)fiscal_year_list.ToList()
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.year_name.ToString(),
                                                       Value = a.fiscal_year_id.ToString(),
                                                       //Selected = (a.fiscal_year_id == Convert.ToInt64(fiscal_year_id) ? true : false)
                                                   }
            ).ToList();

             var style_item_type_list = await _StyleitemtypeService.GetAllAsync();

            model.style_item_type_List = (List<SelectListItem>)style_item_type_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_item_type_name.ToString(),
                                                       Value = a.style_item_type_id.ToString()
                                                   }
                                                   ).ToList();


            var product_type_list = await _StyleproducttypeService.GetAllAsync();

            model.style_product_type_List = (List<SelectListItem>)product_type_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_product_type_name.ToString(),
                                                       Value = a.style_product_type_id.ToString()
                                                   }
                                                   ).ToList();


            var gender_list = await _StylegenderService.GetAllAsync();

            model.style_gender_List = (List<SelectListItem>)gender_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_gender_name.ToString(),
                                                       Value = a.style_gender_id.ToString()
                                                   }
                                                   ).ToList();

            var origin_list = await _StyleitemoriginService.GetAllAsync();

            model.style_item_origin_List = (List<SelectListItem>)origin_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_item_origin_name.ToString(),
                                                       Value = a.style_item_origin_id.ToString()

                                                   }
                                                   ).ToList();

            var sizegroup_list = await _StyleproductsizeeService.GetAllAsync();

            model.style_product_size_group_List = (List<SelectListItem>)sizegroup_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.style_product_size_group_name.ToString(),
                                                       Value = a.style_product_size_group_id.ToString()

                                                   }
                                                   ).ToList();

            var mastercategory_list = await _StylemastercategoryService.GetAsync(model.style_master_category_id.Value);

            model.style_master_category_List = (List<SelectListItem>)mastercategory_list
                .Select(a =>
                                                   new SelectListItem
                                                   {
                                                       Text = a.master_category_name.ToString(),
                                                       Value = a.style_master_category_id.ToString()
                                                   }
                                                   ).ToList();


            var rowindex = 1;
            foreach (var item in model.DetList)
            {
                item.RowNumber = rowindex++;
                
            }


            return PartialView("~/Views/Configuration/ProductConfig/_ProductConfigEdit.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetProductConfigData(Filter_RangePlan_DataTable request)
        {
            var records = await _rpc_db_service.GetAllsp_get_style_product_itemAsync(
            
                request.style_master_category_id,
                request.style_gender_id,
                request.style_item_origin_id,
                request.style_item_type_id,
                request.style_product_type_id, request.Search.Value);


            var index = request.Start + 1;

            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.style_item_product_id,
                            t.style_item_product_name,
                            t.style_product_type_name,
                            t.master_category_name,
                            t.style_item_origin_name,
                            t.style_gender_name,
                            t.style_item_type_name,
                            t.style_product_size_group_name,
                            t.style_gender_id,
                            t.style_item_origin_id,
                            t.style_item_type_id,
                            t.style_product_type_id,
                            t.style_master_category_id,
                            t.style_product_size_group_id,

                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' style='width: 120px;' class='btn btn-info btnEdit'  style_item_product_id='" + clsUtil.EncodeString(t.style_item_product_id.ToString()) + "'><i class=\"fa-solid fa-pen-to-square\"></i>&nbsp;Edit</button></div>"
                        }).Skip(request.Start).Take(request.Length).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> Get_Style_itemsubcategory_Data(DtParameters request)
        {

            var records = await _StyleitemproductsubCategoryService.GetAsync(Convert.ToInt64(request.strMasterID));

            var index = request.Start + 1;

            var data = (from t in records
                        select new
                        {
                            row_number = index++,
                            t.style_item_product_id,
                            t.style_item_product_sub_category_id,
                            t.sub_category_name,
                            datatablebuttonscode = "<td>' +\r\n                '<button style_item_product_id=\"0\" style_item_product_sub_category_id = \"0\" onclick = \"RemoveRow(this)\" type = \"button\" class= \"btn btn-danger \" style = \"width:70px;    height: 25px;\" >' +\r\n                'Remove' +\r\n                '</button></td>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> SaveProductConfig([FromBody] style_item_product_DTO request)
        {
            
          

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

            }

            try
            {
                var model = JsonConvert.DeserializeObject<style_item_product_entity>(JsonConvert.SerializeObject(request));

              
               

                request.DetList = JsonConvert.DeserializeObject<List<style_item_product_sub_category_DTO>>(JsonConvert.SerializeObject(request.DetList));

                ret = await _StyleitemproductService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateProductConfig([FromBody] style_item_product_DTO request)
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

            var model = _mapper.Map<style_item_product_DTO>(request);

            var ret = await _StyleitemproductService.UpdateAsync(model);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });
        }


        //[HttpPost]
        //public async Task<IActionResult> UpdateProductConfig([FromBody] style_item_product_DTO request)
        //{
        //    var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

        //    List<Claim> listClaims = (List<Claim>)claim.Claims;

        //    if (listClaims.Exists(c => c.Type == "secobject"))
        //    {
        //        var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

        //        SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

        //        request.added_by = sec_object.userid.Value;

        //        request.date_added = DateTime.Now;
        //    }

        //    var model = _mapper.Map<style_item_product_DTO>(request);

        //    var ret = await _StyleitemproductService.UpdateAsync(model);

        //    return Json(new ResultEntity
        //    {
        //        Status_Code = ret == true ? "200" : "400",
        //        Status_Result = ret == true ? "Successful" : "Data Saving Failed"
        //    });
        //}


    }
}
