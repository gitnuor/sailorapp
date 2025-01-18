using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Security;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Controllers
{
    public class Select2Controller : Controller
    {
        private readonly IGenSeasonEventConfigurationService _genseasoneventconfigurationService;
        private readonly IGenProcessMasterService _GenProcessMasterService;
        private readonly IGenSegmentService _GenSegmentService;
        private readonly IOwinUserService _OwinUserService;
        private readonly IGenChatGroupService _GenChatGroupService;
        private readonly IGenItemMasterService _GenItemMasterService;
        private readonly IGenTeamGroupService _GenTeamGroupService;
        private readonly IGenItemStockMasterService _GenItemStockMasterService;
        private readonly IGenSegmentDetlService _GenSegmentDetlService;
        private readonly IGenGarmentPartService _GenGarmentPartService;
        private readonly ITrantechpackService _trantechpackService;
        private readonly ITranScmPoService _tramScmPoService;
        private readonly IGenColorService _GenColorService;
        private IRedisService<GenSeasonEventConfigurationDTO> _redisgenseasoneventconfigurationservice;
        private IRedisService<style_master_category_entity> _redisMasterCategoryService;
        private IRedisService<gen_process_master_entity> _redisEmbellishmentService;
        private IRedisService<get_table_data_count_for_select2_DTO> _redis_select2_service;
        private readonly IConfiguration _configuration;
        private readonly List<get_table_data_count_for_select2_DTO> counter;
        private readonly IStylemastercategoryService _stylemastercategoryservice;
        private readonly IGenSupplierInformationService _GensupplierinformationService;
        private readonly IRPCDbService _RPCDbService;
        private readonly ITranPurchaseRequisitionService _TranpurchaserequisitionService;
        private readonly ITranPackingListService _TranpackinglistService;
        private readonly ITranMcdReceiveService _TranMcdReceiveService;
        private readonly ICommonService _CommonService;

        private readonly ITranMcdRequisitionIssueService _TranmcdrequisitionissueService;




        public Select2Controller(IGenTeamGroupService GenTeamGroupService, IGenSeasonEventConfigurationService genseasoneventconfigurationService, ITranMcdRequisitionIssueService TranmcdrequisitionissueService
            , IStylemastercategoryService stylemastercategoryservice, IGenChatGroupService GenChatGroupService
          , IGenProcessMasterService GenProcessMasterService, ITrantechpackService trantechpackService, IOwinUserService OwinUserService,
            IGenColorService GenColorService,
                ITranPackingListService TranpackinglistService
            , IConfiguration configuration, IGenGarmentPartService GenGarmentPartService,
            IGenSegmentService GenSegmentService, IGenSegmentDetlService GenSegmentDetlService,
               IGenSupplierInformationService GensupplierinformationService,
               IGenItemMasterService GenItemMasterService, ITranPurchaseRequisitionService TranpurchaserequisitionService,
               IRPCDbService RPCDbService, ITranMcdReceiveService TranMcdReceiveService,
                IGenItemStockMasterService GenItemStockMasterService
            , ITranScmPoService tramScmPoService, ICommonService CommonService
            )
        {
            _genseasoneventconfigurationService = genseasoneventconfigurationService;
            _stylemastercategoryservice = stylemastercategoryservice;
            _GenProcessMasterService = GenProcessMasterService;
            _GenSegmentService = GenSegmentService;
            _GenSegmentDetlService = GenSegmentDetlService;
            _GenGarmentPartService = GenGarmentPartService;
            _GensupplierinformationService = GensupplierinformationService;
            _trantechpackService = trantechpackService;
            _configuration = configuration;
            _GenColorService = GenColorService;
            _GenItemMasterService = GenItemMasterService;
            _RPCDbService = RPCDbService;
            _GenItemStockMasterService = GenItemStockMasterService;
            _TranpurchaserequisitionService = TranpurchaserequisitionService;
            _GenChatGroupService = GenChatGroupService;
            _TranMcdReceiveService = TranMcdReceiveService;
            _OwinUserService = OwinUserService;
            _TranpackinglistService = TranpackinglistService;
            _redisgenseasoneventconfigurationservice = new RedisService<GenSeasonEventConfigurationDTO>(_configuration);
            _redisMasterCategoryService = new RedisService<style_master_category_entity>(_configuration);
            _redis_select2_service = new RedisService<get_table_data_count_for_select2_DTO>(_configuration);
            _TranmcdrequisitionissueService = TranmcdrequisitionissueService;
            counter = new List<get_table_data_count_for_select2_DTO>();

            if (_redisMasterCategoryService.IsKeyExists("Select2_Counter"))
            {
                counter = _redis_select2_service.GetDataFromRedisCache("Select2_Counter");
            }
            _redisEmbellishmentService = new RedisService<gen_process_master_entity>(_configuration);

            _tramScmPoService = tramScmPoService;
            _CommonService = CommonService;
            _GenTeamGroupService = GenTeamGroupService;

        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> GetSeasonEventList(Int64 fiscal_year_id, string searchTerm)
        {
            List<GenSeasonEventConfigurationDTO> ListData = new List<GenSeasonEventConfigurationDTO>();
            try
            {
                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redisgenseasoneventconfigurationservice"))
                {
                    ListData = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redisgenseasoneventconfigurationservice");

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        ListData = ListData.Where(p => p.event_title.ToLower().Contains(searchTerm.ToLower())).ToList();
                    }

                    var list = (from t in ListData
                                select new
                                {
                                    Id = t.event_id,
                                    Text = t.event_title

                                }).ToList();

                    var result = new
                    {
                        recordstotal = ListData.Count(),
                        data = list
                    };

                    return Json(result);
                }
                else
                {
                    DtParameters dtparam = new DtParameters();
                    dtparam.fiscal_year_id = Convert.ToInt64(fiscal_year_id);
                    dtparam.Start = 0;
                    dtparam.Length = 100;
                    dtparam.Search = new DtSearch();

                    var tempList = await _genseasoneventconfigurationService.GetAllPagedData(dtparam);

                    ListData = JsonConvert.DeserializeObject<List<GenSeasonEventConfigurationDTO>>(JsonConvert.SerializeObject(tempList));

                    _redisgenseasoneventconfigurationservice.SaveDataInRedisCache("redisgenseasoneventconfigurationservice", ListData);

                    var list = (from t in ListData
                                select new
                                {
                                    Id = t.event_id,
                                    Text = t.event_title

                                }).ToList();


                    var result = new
                    {
                        recordstotal = ListData.Count(),
                        data = list
                    };

                    return Json(result);
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> GetMasterCategoryList(string searchTerm)
        {
            List<style_master_category_entity> ListData = new List<style_master_category_entity>();
            try
            {
                if (_redisMasterCategoryService.IsKeyExists("redismastercategoryservice"))
                {
                    ListData = _redisMasterCategoryService.GetDataFromRedisCache("redismastercategoryservice");

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        ListData = ListData.Where(p => p.master_category_name.ToLower().Contains(searchTerm.ToLower())).ToList();
                    }

                    var list = (from t in ListData.OrderBy(p => p.master_category_name)
                                select new
                                {
                                    Id = t.style_master_category_id.ToString(),
                                    Text = t.master_category_name

                                }).ToList();

                    var result = new
                    {
                        recordstotal = ListData.Count(),
                        data = list
                    };

                    return Json(result);
                }
                else
                {
                    ListData = await _stylemastercategoryservice.GetAllAsync();

                    _redisgenseasoneventconfigurationservice.SaveDataInRedisCache("redismastercategoryservice", ListData);


                    var list = (from t in ListData.OrderBy(p => p.master_category_name)
                                select new
                                {
                                    Id = t.style_master_category_id.ToString(),
                                    Text = t.master_category_name

                                }).ToList();


                    var result = new
                    {
                        recordstotal = ListData.Count(),
                        data = list
                    };

                    return Json(result);
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> GetProcessMasterList(Int64? hasEmbellishment, string searchTerm)
        {
            List<gen_process_master_entity> ListData = new List<gen_process_master_entity>();
            try
            {


                ListData = await _GenProcessMasterService.GetAllAsync();

                if (hasEmbellishment.HasValue)
                {
                    ListData = ListData.Where(p => p.has_embellishment == (hasEmbellishment == 1 ? true : false)).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    ListData = ListData.Where(p => p.process_name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                var list = (from t in ListData
                            select new
                            {
                                Id = t.gen_process_master_id,
                                Text = t.process_name

                            }).ToList();


                var result = new
                {
                    recordstotal = ListData.Count(),
                    data = list
                };

                return Json(result);





            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GetSegmentDetailList(Int64 SegmentGroupID, int? pageSize, int? pageNum, string searchTerm = "")
        {

            try
            {

                var filter = new DtParameters();
                filter.MasterID = SegmentGroupID;
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                var ListData = await _GenSegmentDetlService.GetPagedData(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.gen_segment_detl_id,
                                Text = t.segment_value

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.FirstOrDefault().total_rows,
                    data = list
                };

                return Json(result);




            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GetGarmentPartList(int? pageSize, int? pageNum, string searchTerm = "")
        {

            try
            {
                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                var ListData = await _GenGarmentPartService.GetPagedData(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.gen_garment_part_id.ToString(),
                                Text = t.garment_part_name

                            }).ToList();

                var result = new
                {
                    recordstotal = counter.FirstOrDefault().gen_garment_part_count,
                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GetGarmentChatGroupList(int? pageSize, int? pageNum, string searchTerm = "")
        {

            try
            {
                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                var ListData = await _GenChatGroupService.GetAllPagedDataAsync(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.chat_group_id.ToString(),
                                Text = t.chat_group_name

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.FirstOrDefault() != null ? ListData.FirstOrDefault().total_rows : 0,
                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GenSupplierInformationDetailList(int? pageSize, int? pageNum, string searchTerm = "", int tech_pack_id = 0)
        {

            try
            {
                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = !string.IsNullOrEmpty(searchTerm) ? searchTerm : string.Empty;

                if (tech_pack_id != 0)
                {
                    filter.MasterID = tech_pack_id;
                }

                var ListData = await _GensupplierinformationService.GetAllPagedDataAsync(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.gen_supplier_information_id,
                                Text = t.name,
                                contact_person = t.contact_person,
                                office_address = t.office_address,



                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.FirstOrDefault() != null ? ListData.FirstOrDefault().total_rows : 0,
                    data = list
                };

                return Json(result);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GenSegmentList(int? pageSize, int? pageNum, string searchTerm = "")
        {

            try
            {
                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                var ListData = await _GenSegmentService.GetPagedData(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.gen_segment_id,
                                Text = t.gen_segment_name

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.FirstOrDefault() != null ? ListData.FirstOrDefault().total_rows : 0,
                    data = list
                };

                return Json(result);
                ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> TechPackDetailList(int? pageSize, int? pageNum, string searchTerm = "")
        {

            try
            {
                GenSeasonEventConfigurationDTO obj = new GenSeasonEventConfigurationDTO();

                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {

                    obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    filter.fiscal_year_id = obj.fiscal_year_id;
                    filter.event_id = obj.event_id;
                }

                var ListData = await _trantechpackService.GetPagedDataForSelect2(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.tran_techpack_id,
                                Text = t.techpack_number,
                                photos = t.photos

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.Count > 0 ? ListData.FirstOrDefault().total_rows : 0,
                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> TechPackDetailListForFinishProductionProcess(int? pageSize, int? pageNum, string searchTerm = "")
        {

            try
            {
                GenSeasonEventConfigurationDTO obj = new GenSeasonEventConfigurationDTO();

                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {

                    obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    filter.fiscal_year_id = obj.fiscal_year_id;
                    filter.event_id = obj.event_id;
                }

                var ListData = await _trantechpackService.GetPagedDataForSelectFinishProductionProcess(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.tran_techpack_id,
                                Text = t.techpack_number,
                                photos = t.photos

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.Count > 0 ? ListData.FirstOrDefault().total_rows : 0,
                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> TechPackDetailListForPPMeeting(int? pageSize, int? pageNum, string searchTerm = "")
        {

            try
            {
                GenSeasonEventConfigurationDTO obj = new GenSeasonEventConfigurationDTO();

                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {

                    obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    filter.fiscal_year_id = obj.fiscal_year_id;
                    filter.event_id = obj.event_id;
                }

                var ListData = await _trantechpackService.TechPackDetailListForPPMeeting(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.tran_techpack_id,
                                Text = t.techpack_number,
                                photos = t.photos

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.Count > 0 ? ListData.FirstOrDefault().total_rows : 0,
                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvalTechPackListWithFabric(long item_master_id, int? pageSize, int? pageNum, string searchTerm = "")
        {

            try
            {
                GenSeasonEventConfigurationDTO obj = new GenSeasonEventConfigurationDTO();

                var filter = new DtParameters();
                filter.MasterID = item_master_id;
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {

                    obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    filter.fiscal_year_id = obj.fiscal_year_id;
                    filter.event_id = obj.event_id;
                }

                var ListData = await _trantechpackService.GetTechPackListWithAvlFabric(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.tran_techpack_id,
                                Text = t.techpack_number,
                                photos = t.photos

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.Count > 0 ? ListData.FirstOrDefault().total_rows : 0,
                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GenItemMasterDetailList(int TechPackID, int Item_Structure_Sub_Group_ID, int? pageSize, int? pageNum, string searchTerm = "")
        {
            try
            {
                if (TechPackID == 0)
                {

                    var filter = new DtParameters();
                    filter.Start = (pageNum.Value - 1) * pageSize.Value;
                    filter.Length = pageSize.Value;
                    filter.Search = new DtSearch();
                    filter.Search.Value = searchTerm;
                    filter.MasterID = Item_Structure_Sub_Group_ID;

                    var ListData = await _GenItemMasterService.GetAllPagedDataAsync(filter);

                    var list = (from t in ListData
                                select new
                                {
                                    Id = t.gen_item_master_id,
                                    Text = t.item_name,
                                    OtherData = JsonConvert.SerializeObject(t)

                                }).ToList();

                    var result = new
                    {
                        recordstotal = ListData.Count > 0 ? ListData[0].total_rows : 0,
                        data = list
                    };
                    return Json(result);
                }
                else
                {
                    var ListData = await _RPCDbService.GetAllget_gen_item_master_by_techpack_idAsync(TechPackID, Item_Structure_Sub_Group_ID);

                    var list = (from t in ListData
                                select new
                                {
                                    Id = t.gen_item_master_id,
                                    Text = t.item_name
                                    ,
                                    OtherData = JsonConvert.SerializeObject(t)

                                }).ToList();
                    var result = new
                    {
                        recordstotal = list.Count,
                        data = list
                    };
                    return Json(result);
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GenItemMasterStockDetByItemMasterID(Int64 ItemMasterID, Int64? TechPackID = 0)
        {
            try
            {
                var objData = await _GenItemStockMasterService.GetSingleAsync(ItemMasterID, TechPackID);

                if (objData != null)
                {
                    objData.total_issued_quantity = objData.total_issued_quantity == null ? 0 : objData.total_issued_quantity;
                    objData.total_allocated_quantity = objData.total_allocated_quantity == null ? 0 : objData.total_allocated_quantity;
                    objData.total_failed_quantity = objData.total_failed_quantity == null ? 0 : objData.total_failed_quantity;
                    objData.total_floor_return_quantity = objData.total_floor_return_quantity == null ? 0 : objData.total_floor_return_quantity;
                    objData.total_issued_quantity = objData.total_issued_quantity == null ? 0 : objData.total_issued_quantity;
                    objData.total_quarantine_quantity = objData.total_quarantine_quantity == null ? 0 : objData.total_quarantine_quantity;
                    objData.total_received_quantity = objData.total_received_quantity == null ? 0 : objData.total_received_quantity;
                    objData.total_stock_quantity = objData.total_stock_quantity == null ? 0 : objData.total_stock_quantity;
                    objData.available_stock_quantity = objData.available_stock_quantity == null ? 0 : objData.available_stock_quantity;

                    objData.available_stock_quantity = (long?)(objData.available_stock_quantity - objData.total_unapproved_allocated_quantity);
                }
                else
                {
                    objData = new gen_item_stock_master_DTO();
                    objData.total_issued_quantity = 0;
                    objData.total_allocated_quantity = 0;
                    objData.total_failed_quantity = 0;
                    objData.total_floor_return_quantity = 0;
                    objData.total_issued_quantity = 0;
                    objData.total_quarantine_quantity = 0;
                    objData.total_received_quantity = 0;
                    objData.total_stock_quantity = 0;
                    objData.available_stock_quantity = 0;

                }

                var result = new
                {
                    data = objData
                };
                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> TranPurchaseRequisitionDetailList(int? pageSize, int? pageNum, string searchTerm = "", long group = 1)
        {
            GenSeasonEventConfigurationDTO obj = new GenSeasonEventConfigurationDTO();

            try
            {

                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * 10;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {

                    obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    filter.event_id = obj.event_id;

                }

                var ListData = await _TranpurchaserequisitionService.GetAllPagedDataAsync(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.pr_id,
                                Text = t.pr_no

                            }).ToList();

                var result = new
                {
                    cordstotal = ListData.FirstOrDefault() != null ? ListData.FirstOrDefault().total_rows : 0,

                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> TranPRFabricDetailList(int? pageSize, int? pageNum, string searchTerm = "", long group = 1)
        {
            GenSeasonEventConfigurationDTO obj = new GenSeasonEventConfigurationDTO();

            try
            {

                var filter = new PR_DtParameters();
                filter.Start = (pageNum.Value - 1) * 10;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;
                filter.group_id = group;

                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {

                    obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    filter.event_id = obj.event_id;

                }

                var ListData = await _TranpurchaserequisitionService.GetAllFabricDataAsync(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.pr_id,
                                Text = t.pr_no

                            }).ToList();

                var result = new
                {
                    cordstotal = ListData.FirstOrDefault() != null ? ListData.FirstOrDefault().total_rows : 0,

                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> TranPRAcccesoriesDetailList(int? pageSize, int? pageNum, string searchTerm = "", long group = 1)
        {
            GenSeasonEventConfigurationDTO obj = new GenSeasonEventConfigurationDTO();

            try
            {

                var filter = new PR_DtParameters();
                filter.Start = (pageNum.Value - 1) * 10;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;
                filter.group_id = group;
                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {

                    obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    filter.event_id = obj.event_id;

                }

                var ListData = await _TranpurchaserequisitionService.GetAllAccesoriesDataAsync(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.pr_id,
                                Text = t.pr_no

                            }).ToList();

                var result = new
                {
                    cordstotal = ListData.FirstOrDefault() != null ? ListData.FirstOrDefault().total_rows : 0,

                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> TranScmPoDetailList(int? pageSize, int? pageNum, string searchTerm = "", long group = 0)
        {
            GenSeasonEventConfigurationDTO obj = new GenSeasonEventConfigurationDTO();

            try
            {

                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * 10;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {

                    obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    filter.fiscal_year_id = obj.fiscal_year_id;
                    filter.event_id = obj.event_id;

                }


                var ListData = await _tramScmPoService.GetAllPagedDataAsync(filter);


                var list = (from t in ListData
                            select new
                            {
                                Id = t.po_id.ToString(),
                                Text = t.po_no

                            }).ToList();

                var result = new
                {
                    cordstotal = ListData.FirstOrDefault() != null ? ListData.FirstOrDefault().total_rows : 0,

                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GetMcdRequisitionIssueDetailList(int? pageSize, int? pageNum, string searchTerm = "")
        {
            GenSeasonEventConfigurationDTO obj = new GenSeasonEventConfigurationDTO();
            try
            {

                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;

                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {

                    obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    filter.fiscal_year_id = obj.fiscal_year_id;
                    filter.event_id = obj.event_id;

                }

                var ListData = await _TranmcdrequisitionissueService.GetAllPagedDataAsync(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.tran_mcd_requisition_issue_id,
                                Text = t.issue_no

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.Count > 0 ? ListData[0].total_rows : 0,
                    data = list
                };

                return Json(result);





            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GetTeamMemberListByTeamGroupID(int teamGroupID, int? pageSize, int? pageNum, string searchTerm = "")
        {

            try
            {

                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;
                filter.MasterID = teamGroupID;

                var ListData = await _OwinUserService.GetAllPagedDataForSelect2(filter);

                var ListDTO = JsonConvert.DeserializeObject<List<owin_user_DTO>>(JsonConvert.SerializeObject(ListData));

                ListDTO = await _CommonService.LoadAllEmployeePic(ListDTO);

                var list = (from t in ListDTO
                            select new
                            {
                                Id = t.userid,
                                Text = t.name,
                                new_emp_pic = t.new_emp_pic

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.Count > 0 ? ListData.FirstOrDefault().total_rows : 0,
                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> TranPackingListDetailList(int? pageSize, int? pageNum, string searchTerm = "")
        {

            try
            {
                GenSeasonEventConfigurationDTO obj = new GenSeasonEventConfigurationDTO();
                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * 10;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = searchTerm;
                if (_redisgenseasoneventconfigurationservice.IsKeyExists("redis_set_user_filter"))
                {

                    obj = _redisgenseasoneventconfigurationservice.GetDataFromRedisCache("redis_set_user_filter").FirstOrDefault();

                    filter.fiscal_year_id = obj.fiscal_year_id;
                    filter.event_id = obj.event_id;
                }
                var ListData = await _TranpackinglistService.GetAllApprovedDataAsync(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.tran_packing_list_id,
                                Text = t.packing_list_no

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.Count > 0 ? ListData[0].total_rows : 0,
                    data = list
                };

                return Json(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GenTeamGroupDetailList(int? pageSize, int? pageNum, string searchTerm = "", int tech_pack_id = 0)
        {

            try
            {
                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = !string.IsNullOrEmpty(searchTerm) ? searchTerm : string.Empty;

                if (tech_pack_id != 0)
                {
                    filter.MasterID = tech_pack_id;
                }

                var ListData = await _GenTeamGroupService.GetAllPagedDataAsync(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.gen_team_group_id,
                                Text = t.team_group_name

                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.FirstOrDefault() != null ? ListData.FirstOrDefault().total_rows : 0,
                    data = list
                };

                return Json(result);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> GenColorDetailList(int? pageSize, int? pageNum, string searchTerm = "", int tech_pack_id = 0)
        {

            try
            {
                var filter = new DtParameters();
                filter.Start = (pageNum.Value - 1) * pageSize.Value;
                filter.Length = pageSize.Value;
                filter.Search = new DtSearch();
                filter.Search.Value = !string.IsNullOrEmpty(searchTerm) ? searchTerm : string.Empty;

                if (tech_pack_id != 0)
                {
                    filter.MasterID = tech_pack_id;
                }

                var ListData = await _GenColorService.GetAllPagedDataAsync(filter);

                var list = (from t in ListData
                            select new
                            {
                                Id = t.color_name + "(" + t.color_code + ")",
                                Text = t.color_name + "(" + t.color_code + ")",
                                textandcolor = @"<span style='width:150px'>
                                <input type='text' value='" + t.color_name + "(" + t.color_code + ")" + @"' class='border-gray-200 rounded-sm txtColorCode' style='width:100px;background-color:" + t.color_code + @"' /></span>"


                            }).ToList();

                var result = new
                {
                    recordstotal = ListData.FirstOrDefault() != null ? ListData.FirstOrDefault().total_rows : 0,
                    data = list
                };

                return Json(result);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
