using AutoMapper;
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.SystemServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using ServiceStack;
using System.Security.Claims;
using Web.Core.Frame.Helpers;

namespace EPYSLSAILORAPP.Controllers.SCM
{
    public class BillSubmissionChallanController : ExtendedBaseController
    {
        private readonly ILogger<BillSubmissionChallanController> _logger;


        private readonly ITranScmBillSubmissionService _tranScmBillSubmissionService;

        private readonly ITranScmPoService _tramscmpo;
        private readonly IConfiguration _configuration;

        private readonly ITranScmPoService _TranScmPoService;
        private readonly IRPCDbService _rpc_db_service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenSupplierInformationService _GenSupplierInformationService;
        private HelperService objHelperService;

        public IActionResult Index()
        {
            _logger.LogInformation("Hello from inside BillSubmissionChallanController !");
            return View();
        }

        public BillSubmissionChallanController(
           IMapper mapper, ILogger<BillSubmissionChallanController> logger, IHttpContextAccessor contextAccessor, ITranScmPoService TranScmPoService, IGenSupplierInformationService GenSupplierInformationService,
            IRPCDbService rpc_db_service, ITranScmBillSubmissionService tranScmBillSubmissionService, ITranScmPoService tramscmpo, IConfiguration configuration) : base(contextAccessor, configuration)
        {
            _mapper = mapper;

            _tranScmBillSubmissionService = tranScmBillSubmissionService;
            _tramscmpo = tramscmpo;

            _TranScmPoService = TranScmPoService;
            _logger = logger;
            _rpc_db_service = rpc_db_service;
            _contextAccessor = contextAccessor;
            _GenSupplierInformationService = GenSupplierInformationService;


            _contextAccessor = contextAccessor;
            _tramscmpo = tramscmpo;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> BillSubmissionChallanLanding()
        {

            return View("~/Views/SCM/BillSubmissionChallan/BillSubmissionChallanLanding.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> BillSubmissionChallanNew()
        {


            tran_scm_po_DTO model = new tran_scm_po_DTO();
            model.event_id = objFilter.event_id;
            model.item_structure_group_id = 1;
            model.po_date = DateTime.Now;
            //List<gen_term_and_conditions_DTO> termConditionList = await _genTermAndConditionsService.GetAllAsync();

            //ViewBag.termConditionList =
            //    termConditionList.ToList().Select(a =>
            //        new SelectListItem
            //        {
            //            Text = a.term_condition_name.ToString(),
            //            Value = a.gen_term_and_conditions_id.ToString()
            //        }).ToList();


            return PartialView("~/Views/SCM/BillSubmissionChallan/_BillSubmissionChallanNew.cshtml", model);

        }


    }
}
