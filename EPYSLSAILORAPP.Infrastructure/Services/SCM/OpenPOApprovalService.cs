using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using Serilog.Context;


namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class OpenPOApprovalService : IOpenPOApprovalService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
		private readonly ILogger<OpenPOApprovalService> _logger;
		private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IGenUnitService _GenUnitService;
        private readonly IGenItemMasterService _GenItemMasterService;
        private readonly ITranScmPoService _TranScmPoService;
        private readonly IRPCDbService _RPCDbService;
        private readonly ITranPurchaseRequisitionService _TranPurchaseRequisitionService;
        public OpenPOApprovalService(ITranPurchaseRequisitionService TranPurchaseRequisitionService, IRPCDbService RPCDbService,
            ITranScmPoService TranScmPoService, IGenUnitService GenUnitService, IGenItemMasterService GenItemMasterService, IConfiguration configuration, ILogger<OpenPOApprovalService> logger, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _GenUnitService = GenUnitService;
            _hostingEnvironment = hostingEnvironment;
            _GenItemMasterService = GenItemMasterService;
            _mapper = mapper;
            _logger = logger;
			_configuration = configuration;
         
        }

      
        public async Task<bool> UpdateAsync(tran_scm_po_DTO entity)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_scm_po
                                          SET approved_by=@approved_by,
                                              approved_date = @approved_date,
                                              is_approved=@is_approved
                                          WHERE po_id = @po_id";


                    await connection.ExecuteAsync(query, new
                    {
                        approved_by= entity.added_by,
                        approved_date=DateTime.Now,
                        is_approved=entity.is_approved,
                        po_id = entity.po_id

                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
            //        try
            //        {
            //            await _connectionSupabse.InitializeAsync();
            //            var response = await _connectionSupabse.From<tran_scm_po_entity>()
            //           .Where(x => x.po_id == entity.po_id)
            //           .Select("*").Get();

            //            tran_scm_po_DTO ret = JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content).FirstOrDefault();

            //            ret.approved_by = entity.added_by;
            //            ret.approved_date = DateTime.Now;  

            //            ret.is_approved= entity.is_approved;
            //var model = JsonConvert.DeserializeObject<tran_scm_po_entity>(JsonConvert.SerializeObject(ret));
            //            model.is_bill_submitted = false;
            //            await _connectionSupabse.From<tran_scm_po_entity>().Update(model);



            //            return true;


            //        }
            //        catch (Exception ex)
            //        {
            //            return false;
            //        }

        }

        public async Task<List<tran_scm_po_DTO>> GetAllAsync()
        {
            List<tran_scm_po_entity> list = new List<tran_scm_po_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_scm_po_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_scm_po_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_scm_po m
                                           where 
                                                     m.is_submitted=2 and m.is_approved is null
                                                     and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.po_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_scm_po_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(JsonConvert.SerializeObject(dataList)); ;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            //        try
            //        {
            //            await _connectionSupabse.InitializeAsync();
            //long group = 1;
            //var sort_column = ""; var sort_order = "";

            //            if (param.SortOrder.Contains(' '))
            //            {
            //                sort_column = param.SortOrder.Split(' ')[0];
            //                sort_order = param.SortOrder.Split(' ')[1];
            //            }
            //            else
            //            {
            //                sort_column = param.SortOrder;
            //                sort_order = param.Order.Count() > 0 ? param.Order[0].ToString() : "asc";
            //            }

            //            if (!string.IsNullOrEmpty(param.Search.Value))
            //            {
            //                //replace primary column from filter by your required column
            //                var response = await _connectionSupabse.From<tran_scm_po_entity>()
            //               .Select("*")
            //               .Filter(p => p.po_id, Operator.ILike, "%" + param.Search.Value + "%")
            //               .Order("po_id", Ordering.Descending)
            //               .Range(param.Start, param.Start + param.Length - 1)
            //   .Where(x => x.is_submitted == 2)
            //   .Where(x => x.is_approved == null)
            //   .Get();

            //                return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content);
            //            }
            //            else
            //            {
            //                var response = await _connectionSupabse.From<tran_scm_po_entity>()
            //               .Select("*")
            //               .Order("po_id", Ordering.Descending)
            //               .Range(param.Start, param.Start + param.Length - 1)
            //   .Where(x => x.is_submitted == 2)
            //   .Where(x => x.is_approved == null)
            //   .Get();

            //                return JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content);
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            throw (ex);
            //        }

        }

        
        public async Task<tran_scm_po_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                //List<file_upload> objpdf_list = new List<file_upload>();
                //await _connectionSupabse.InitializeAsync();

                //var response = await _connectionSupabse.From<tran_scm_po_entity>().Select("*").Where(p => p.po_id == Id).Get();
                //var details = await _connectionSupabse.From<tran_scm_po_details_entity>().Select("*").Where(p => p.po_id == Id).Get();

                //tran_scm_po_DTO po = JsonConvert.DeserializeObject<List<tran_scm_po_DTO>>(response.Content).FirstOrDefault();
                //po.po_details = JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(details.Content);

                //foreach (tran_scm_po_details_DTO dto in po.po_details)
                //{
                //    //var gen_master = await _connectionSupabse.From<gen_item_master_entity>().Select("*").Where(p => p.gen_item_master_id == dto.item_id).Get();

                //    var gen_master = await _GenItemMasterService.GetSingleAsync(dto.item_id.Value);


                //    //gen_item_master_DTO item_master = JsonConvert.DeserializeObject<List<gen_item_master_DTO>>(gen_master.Content).FirstOrDefault();

                //    dto.item_name = gen_master.item_name;
                //    dto.item_spec = gen_master.item_spec;

                //}

                //if (po.delivery_unit is not null)
                //{
                //    //var delivery_unit = await _connectionSupabse.From<gen_unit_entity>().Select("*").Where(p => p.gen_unit_id == po.delivery_unit).Get();
                //    var delivery_unit = await _GenUnitService.GetAsync(po.delivery_unit.Value);
                //    po.delivery_unit_name = delivery_unit.FirstOrDefault().unit_name;
                // }

                List<file_upload> objpdf_list = new List<file_upload>();

                tran_scm_po_DTO po = new tran_scm_po_DTO();

                var ret_rpc = await _RPCDbService.GetAllproc_sp_get_data_tran_scm_poAsync(Id);

                po = JsonConvert.DeserializeObject<tran_scm_po_DTO>(JsonConvert.SerializeObject(ret_rpc));

                po.List_po_details = JsonConvert.DeserializeObject<List<tran_scm_po_details_DTO>>(ret_rpc.tran_scm_po_details_list);



                if (po.documents is not null && po.documents != null)
                {

                    foreach (JObject str in po.documents)
                    {
                        try
                        {
                            var file = JsonConvert.DeserializeObject<file_upload>(str.ToString());

                            if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath)))
                            {
                                var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath));

                                file.base64string = Convert.ToBase64String(bytes);
                                
                                objpdf_list.Add(file);
                            }
                        }
                        catch (Exception ex)
                        {

                            using (LogContext.PushProperty("SpecialLogType", true))
                            {
                                _logger.LogError(ex.ToString());
                            }
                        }

                    }
                }
                po.List_Files = objpdf_list;


                return po;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_scm_po_DTO>> GetAllJoined_TranScmPoAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_open_po_pending_approval( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@list_type)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            //try
            //{
            //    await _connectionSupabse.InitializeAsync();

            //    var objFilter = new Dictionary<string, object>();



            //    objFilter.Add("currnet_page", currnet_page);
            //    objFilter.Add("page_size", page_size);
            //    objFilter.Add("fiscal_year", fiscal_year_id);
            //    objFilter.Add("p_event_id", event_id);
            //    objFilter.Add("supplier", supplier_id);
            //    objFilter.Add("p_delivery_unit_id", delivery_unit_id);
            //    var response = await _connectionSupabse.Rpc("proc_sp_get_data_tran_scm_open_po_pending_approval", objFilter);

            //    return JsonConvert.DeserializeObject<List<rpc_tran_scm_po_DTO>>(response.Content);

            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}

        }


        public async Task<List<rpc_tran_scm_po_DTO>> GetOpenPOApprovedData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_open_po_approved( @row_index,@page_size,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@list_type)";

                    var dataList = connection.Query<rpc_tran_scm_po_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            //try
            //{
            //    await _connectionSupabse.InitializeAsync();

            //    var objFilter = new Dictionary<string, object>();



            //    objFilter.Add("currnet_page", currnet_page);
            //    objFilter.Add("page_size", page_size);
            //    objFilter.Add("fiscal_year", fiscal_year_id);
            //    objFilter.Add("p_event_id", event_id);
            //    objFilter.Add("supplier", supplier_id);
            //    objFilter.Add("p_delivery_unit_id", delivery_unit_id);
            //    var response = await _connectionSupabse.Rpc("proc_sp_get_data_tran_scm_open_po_approved", objFilter);

            //    return JsonConvert.DeserializeObject<List<rpc_tran_scm_po_DTO>>(response.Content);

            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}

        }

    }
}

