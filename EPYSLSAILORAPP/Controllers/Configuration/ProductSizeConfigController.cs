using AutoMapper;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using EPYSLSAILORAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using DnsClient.Protocol;
using EPYSLSAILORAPP.Application.DTO;
using ServiceStack.Messaging;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using EPYSLSAILORAPP.Application.Interface;
using System.Globalization;
using BDO.Core.Base;
using EPYSLSAILORAPP.Infrastructure.Services.GenServices;
using System.Text;
using Web.Core.Frame.Helpers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.SystemServices;
using EPYSLSAILORAPP.Controllers.BusinessPlanning;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Services.BusinessPlanning;
using Org.BouncyCastle.Ocsp;

namespace EPYSLSAILORAPP.Controllers.Configuration
{
    public class ProductSizeConfigController : BaseController
    {
        private readonly ILogger<ProductSizeConfigController> _logger;
        private readonly IConfiguration _configuration;
      

        private readonly IStylecategoryService _StylecategoryService;
        private readonly IStylegenderService _StylegenderService;
        private readonly IStyleitemoriginService _StyleitemoriginService;
        private readonly IStyleitemproductService _StyleitemproductService;
        private readonly IStyleitemtypeService _StyleitemtypeService;
        private readonly IStylelabelService _StylelabelService;
        private readonly IStylemastercategoryService _StylemastercategoryService;
        private readonly IStyleproducttypeService _StyleproducttypeService;
        private readonly IStyleproductsizegroupService _StyleproductsizeeService;
        private readonly IStyleproductsizegroupdetailsService _StyleproductsizeDetlService;

        private readonly IGenFiscalYearService _gen_fiscal_year_Service;
        private readonly IRPCDbService _rpc_db_service;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        private IRedisService<rpc_range_plan_getfor_createupdate_DTO> _redisRangePlanService;
        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside ProductSizeConfigController !");
            return View();
        }

        public ProductSizeConfigController(
            IGenFiscalYearService gen_fiscal_year_Service
             ,IMapper mapper, ILogger<ProductSizeConfigController> logger, IHttpContextAccessor contextAccessor,
            IStylecategoryService StylecategoryService,
            IStylegenderService StylegenderService,
            IStyleitemoriginService StyleitemoriginService,
            IStyleitemproductService StyleitemproductService,
            IStyleitemtypeService StyleitemtypeService,
            IStylelabelService StylelabelService, 
            IStylemastercategoryService StylemastercategoryService,
            IStyleproducttypeService StyleproducttypeService,
            IStyleproductsizegroupService StyleproductsizeeService,
            IStyleproductsizegroupdetailsService StyleproductsizeDetlService,
            IRPCDbService rpc_db_service,  IConfiguration configuration)
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
            _StyleitemtypeService = StyleitemtypeService;
            _StylelabelService = StylelabelService;
            _StylemastercategoryService = StylemastercategoryService;
            _StyleproducttypeService = StyleproducttypeService;
            _StyleproductsizeeService = StyleproductsizeeService;
            _StyleproductsizeDetlService = StyleproductsizeDetlService;

            _logger.LogInformation("Hello from inside RangePlan !");
         
            _configuration = configuration;

            _redisRangePlanService = new RedisService<rpc_range_plan_getfor_createupdate_DTO>(_configuration);

            

            _contextAccessor = contextAccessor;
            _logger.LogInformation("Hello from inside ProductSizeConfigController !");
        }

        [HttpGet]
        public async Task<IActionResult> ProductSizeConfigLanding()
        {

            var fiscal_year_list = await _gen_fiscal_year_Service.get_fiscal_year_GetAll();


            ViewBag.fiscal_year_list = fiscal_year_list
                //Where(p => tran_bp_year.All(q => q.fiscal_year_id != p.fiscal_year_id)).ToList()
                //.Where(p=>p.is_used==true)
                .Select(a =>
                            new SelectListItem
                             {
                                   Text = a.year_name.ToString(),
                                   Value = a.fiscal_year_id.ToString(),
                                   Selected = a.is_used==true? true : false
                              }
                        ).ToList();

            return View("~/Views/Configuration/ProductSizeConfig/ProductSizeConfigLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> ProductSizeConfigNew(string fiscal_year_id)
        {
            style_product_size_group_DTO model = new style_product_size_group_DTO();

            model.DetList = new List<style_product_size_group_details_DTO>();
        
            return PartialView("~/Views/Configuration/ProductSizeConfig/_ProductSizeConfigNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> ProductSizeConfigEdit(string style_product_size_group_id)
        {
           
            string decoded_style_product_size_group_id = clsUtil.DecodeString(style_product_size_group_id);

            var model = await _StyleproductsizeeService.GetSingleAsync(Convert.ToInt64(decoded_style_product_size_group_id));

            //model =  _mapper.Map<style_product_size_group_DTO>(ret_list); 

            //var ret_sizeList = await _StyleproductsizeDetlService.GetAsync(Convert.ToInt64(decoded_style_product_size_group_id));

            //model.DetList = _mapper.Map<List<style_product_size_group_details_DTO>>(ret_sizeList);

            return PartialView("~/Views/Configuration/ProductSizeConfig/_ProductSizeConfigEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> ProductSizeConfigDelete(string style_product_size_group_id)
        {
            style_product_size_group_DTO model = new style_product_size_group_DTO();

            string decoded_style_product_size_group_id = clsUtil.DecodeString(style_product_size_group_id);

            var ret = await _StyleproductsizeeService.DeleteAsync(Convert.ToInt64(decoded_style_product_size_group_id));

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });
        }


        [HttpPost]
        public async Task<IActionResult> GetProductSizeConfigData(DtParameters request)
        {
            var ret_sizeList = await _StyleproductsizeDetlService.GetAllAsync();

            var DetSizeList = _mapper.Map<List<style_product_size_group_details_DTO>>(ret_sizeList);

            var records = await _StyleproductsizeeService.GetAllPagedDataAsync(request);

            var index = request.Start+1;
            var data = (from t in records
                        select new
                        {
                            row_index= index++,
                            t.style_product_size_group_id,
                            t.style_product_size_group_name,
                            t.is_separate_price,
                            strSizes= string.Join(",", DetSizeList.Where(p=>p.style_product_size_group_id==t.style_product_size_group_id)
                            .OrderBy(p=>p.style_product_size_group_detid)
                            .Select(p=> p.style_product_size)),

                            str_is_separate_price =t.is_separate_price==true?"Separate Price":"Same Price",
                           datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                           "<button type='button' onclick='EditClick(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  style_product_size_group_id='" + clsUtil.EncodeString(t.style_product_size_group_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='DeleteClick(this)' style='width: 120px;' class='btn btn-secondary btnDelete'  detid='" + clsUtil.EncodeString(t.style_product_size_group_id.ToString()) + "'>Delete</button>" +
                           "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(data.Count() > 0 ? records[0].total_rows.Value : 0, data);
            return Json(ret_obj);

        }

        [HttpPost]
        public async Task<IActionResult> GetProductSizeConfig_Detl_Data(DtParameters request)
        {
            //string decoded_style_product_size_group_id = clsUtil.DecodeString(request.strMasterID);

            var records = await _StyleproductsizeDetlService.GetAsync(Convert.ToInt64(request.strMasterID));

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.style_product_size_group_id,
                            t.style_product_size_group_detid,
                            t.style_product_size,
                            str_style_product_size= "<input style_product_size_group_id='"+t.style_product_size_group_id.ToString() + "'  style_product_size_group_detid='" + t.style_product_size_group_detid.ToString() + "' type='text' class=' txt_product_size_group_detid border-gray-200 form-control' style='width:100%' value='" + t.style_product_size+"'/>",
                            str_delete_button= "<button type='button' onclick='DeleteDetailSize(this);return false;' style='width: 120px;' class='btn btn-secondary btnDelete'  detid='" + t.style_product_size_group_detid.ToString() + "'>Delete</button>"
                        }).ToList();

            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }


        [HttpPost]
        public async Task<IActionResult> SaveProductSizeConfig([FromBody] style_product_size_group_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.style_product_size_group_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;

                    foreach(var obj in request.DetList)
                    {
                        obj.added_by = sec_object.userid.Value;
                        obj.date_added = DateTime.Now;
                    }
                }
                else 
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;

                    request.updated_by = sec_object.userid.Value;

                    request.date_updated = DateTime.Now;

                    foreach (var obj in request.DetList)
                    {
                        obj.added_by = sec_object.userid.Value;
                        obj.date_added = DateTime.Now;
                        obj.updated_by = sec_object.userid.Value;
                        obj.date_updated = DateTime.Now;
                    }
                }
            }

         
            var ret = await _StyleproductsizeeService.SaveAsync(request);

            return Json(new ResultEntity
            {
                Status_Code = ret == true ? "200" : "400",
                Status_Result = ret == true ? "Successful" : "Data Saving Failed"
            });

        }

      

    }
}
