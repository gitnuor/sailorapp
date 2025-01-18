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
using Newtonsoft.Json.Linq;

namespace EPYSLSAILORAPP.Controllers
{
    public class GenSupplierInformationController : BaseController
    {
        private readonly ILogger<GenSupplierInformationController> _logger;

        private readonly IGenSupplierInformationService _GenSupplierInformationService;

        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside GenSupplierInformationController !");
            return View();
        }

        public GenSupplierInformationController(
           IMapper mapper, ILogger<GenSupplierInformationController> logger, IHttpContextAccessor contextAccessor,
            IGenSupplierInformationService GenSupplierInformationService,
            IRPCDbService rpc_db_service)
        {
            _mapper = mapper;

            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenSupplierInformationService = GenSupplierInformationService;

            

            _contextAccessor = contextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> GenSupplierInformationLanding()
        {
          
            return View("~/Views/Setup/GenSupplierInformation/GenSupplierInformationLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GenSupplierInformationNew()
        {
            gen_supplier_information_DTO model = new gen_supplier_information_DTO();

            var obj_Data = await _rpc_db_service.GetAllproc_get_supplier_newAsync();

            model = _mapper.Map<gen_supplier_information_DTO>(obj_Data);

            return PartialView("~/Views/Setup/GenSupplierInformation/_GenSupplierInformationNew.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenSupplierInformationEdit(string gen_supplier_information_id)
        {

            string decoded_gen_supplier_information_id = clsUtil.DecodeString(gen_supplier_information_id);

            gen_supplier_information_DTO model = new gen_supplier_information_DTO();

            model = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(decoded_gen_supplier_information_id));

            var obj_Data = await _rpc_db_service.GetAllproc_get_supplier_newAsync();

            model.List_bank = obj_Data.List_bank;
            model.List_bankbranch = obj_Data.List_bankbranch;
            model.List_country = obj_Data.List_country;
            model.List_calculationoftenure = obj_Data.List_calculationoftenure;
            model.List_contact_category = obj_Data.List_contact_category;
            model.List_itemstructuregroupsub = obj_Data.List_itemstructuregroupsub;
            model.List_creditperiod = obj_Data.List_creditperiod;

            //model.List_incoterm = obj_Data.List_incoterm;
            //model.List_mode_of_transaction = obj_Data.List_mode_of_transaction;
            //model.List_paymentmethod = obj_Data.List_paymentmethod;
            //model.List_paymentterm = obj_Data.List_paymentterm;
            
            return PartialView("~/Views/Setup/GenSupplierInformation/_GenSupplierInformationEdit.cshtml", model);

        }

        [HttpGet]
        public async Task<IActionResult> GenSupplierInformationView(string gen_supplier_information_id)
        {

            string decoded_gen_supplier_information_id = clsUtil.DecodeString(gen_supplier_information_id);

            gen_supplier_information_DTO model = new gen_supplier_information_DTO();

            model = await _GenSupplierInformationService.GetSingleAsync(Convert.ToInt64(decoded_gen_supplier_information_id));

            var obj_Data = await _rpc_db_service.GetAllproc_get_supplier_newAsync();

            model.List_bank = obj_Data.List_bank;
            model.List_bankbranch = obj_Data.List_bankbranch;
            model.List_country = obj_Data.List_country;
            model.List_itemstructuregroupsub = obj_Data.List_itemstructuregroupsub;

            //model.List_creditperiod = obj_Data.List_creditperiod;
            //model.List_calculationoftenure = obj_Data.List_calculationoftenure;
            ////model.List_contact_category = obj_Data.List_contact_category;
            //model.List_incoterm = obj_Data.List_incoterm;
            //model.List_mode_of_transaction = obj_Data.List_mode_of_transaction;
            //model.List_paymentmethod = obj_Data.List_paymentmethod;
            // model.List_paymentterm = obj_Data.List_paymentterm;

            return PartialView("~/Views/Setup/GenSupplierInformation/_GenSupplierInformationView.cshtml", model);

        }


        [HttpPost]
        public async Task<IActionResult> GetGenSupplierInformationData(DtParameters request)
        {

            var records = await _GenSupplierInformationService.GetAllPagedDataAsync(request);

            var index = request.Start + 1;
            var data = (from t in records
                        select new
                        {
                            row_index = index++,
                            t.gen_supplier_information_id,
                            t.contact_code,
                            t.name,
                            t.short_name,
                            t.display_code,
                            t.email,
                            t.office_address,
                            t.factory_address,
                            t.fax_no,
                            //t.contact_category,
                            t.contact_person,
                            t.country_id,
                            t.city,
                            t.agent_name,
                           
                           
                            t.vat_bin_number,
                            t.vat_bin_number_start_date,
                            t.vat_bin_number_expire_date,
                            //t.vat_bin_documnet,
                            //t.tin_number,
                            //t.tin_document,
                            t.trade_license,
                            t.trade_license_start_date,
                            t.trade_license_expire_date,
                            //t.trade_license_document,
                            //t.branch_list,
                            //t.concern_person_list,
                            //t.inco_term_list,
                            //t.mode_of_transction_list,
                            //t.payment_method_list,
                            //t.payment_term_list,
                            //t.bank_account_info_list,
                            //t.item_sub_group_detail_list,
                            //t.geographical_location_list,
                            t.active,
                            t.added_by,
                            t.updated_by,
                            t.date_added,
                            t.date_updated,


                            datatablebuttonscode = "<div style='text-align:center;' role='toolbar' >" +
                            "<button type='button' onclick='EditGenSupplierInformation(this)' style='width: 120px;' class='btn btn-secondary btnEdit'  gen_supplier_information_id='" + clsUtil.EncodeString(t.gen_supplier_information_id.ToString()) + "'>Edit</button>" +
                            "<button type='button' onclick='ViewGenSupplierInformation(this)' style='width: 120px;' class='btn btn-secondary btnView'  gen_supplier_information_id='" + clsUtil.EncodeString(t.gen_supplier_information_id.ToString()) + "'>View</button>" +
                            "<button type='button' onclick='DeleteGenSupplierInformation(\"" + clsUtil.EncodeString(t.gen_supplier_information_id.ToString()) + "\")' style='width: 120px;' class='btn btn-danger btnDelete'>Delete</button>" +
                            "</div>"
                        }).ToList();
            var ret_obj = new AjaxResponse(records.Count, data);
            return Json(ret_obj);

        }



        [HttpPost]
        public async Task<IActionResult> SaveGenSupplierInformation([FromBody] gen_supplier_information_DTO request)
        {
            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            var ret = true;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                if (request.gen_supplier_information_id == null)
                {
                    request.added_by = sec_object.userid.Value;

                    request.date_added = DateTime.Now;
                }

            }

            try
            {
               
                ret = await _GenSupplierInformationService.SaveAsync(request);

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
        public async Task<IActionResult> UpdateGenSupplierInformation([FromBody] gen_supplier_information_DTO request)
        {
            var ret = true;

            var claim = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            List<Claim> listClaims = (List<Claim>)claim.Claims;

            if (listClaims.Exists(c => c.Type == "secobject"))
            {
                var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                SecurityCapsule sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                request.added_by = sec_object.userid.Value;

                request.date_added = DateTime.Now;

                request.updated_by = sec_object.userid.Value;

                request.date_updated = DateTime.Now;
            }

            try
            {
                
                ret = await _GenSupplierInformationService.UpdateAsync(request);

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
        public async Task<IActionResult> DeleteGenSupplierInformation([FromBody] gen_supplier_information_DTO request)
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

                string decoded_gen_supplier_information_id = clsUtil.DecodeString(request.strMasterID);

                request.gen_supplier_information_id = Convert.ToInt64(decoded_gen_supplier_information_id);

                ret = await _GenSupplierInformationService.DeleteAsync(request.gen_supplier_information_id.Value);

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

