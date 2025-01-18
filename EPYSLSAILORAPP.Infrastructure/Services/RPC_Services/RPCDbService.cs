
using AutoMapper;
using Dapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.DTO.TranTables;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.DashBoard;
using EPYSLSAILORAPP.Domain.Enum;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using Serilog.Context;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class RPCDbService : IRPCDbService
    {

        private readonly IConfiguration _configuration;
      
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public RPCDbService(IConfiguration configuration, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {

            _mapper = mapper;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
          
        }


        public async Task<List<rpc_range_plan_getfor_createupdate_DTO>> GetAll_Range_plan_getfor_CreateUpdate(Filter_RangePlan_DataTable param)
        {
            try
            {
                var searhtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM range_plan_getfor_createupdate( @year_id_filter,@event_id_filter,@filter_option,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_range_plan_getfor_createupdate_DTO>(query,
                          new
                          {
                              year_id_filter = param.fiscal_year_id,
                              event_id_filter = param.event_id,
                              filter_option = param.filter_option,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searhtext
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_calendar_chart_data>> get_calendar_chart_data(long year_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM fn_get_calendar_chart_data( @search_text)";

                    var dataList = connection.Query<rpc_calendar_chart_data>(query,
                          new { search_text = year_id }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<rpc_get_outlet_sales_data>> GetAllfn_get_outlet_salesAsync(Int64? year_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM fn_get_outlet_sales( @year_id)";

                    var dataList = connection.Query<rpc_get_outlet_sales_data>(query,
                          new { year_id = year_id }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<rpc_range_plan_getfor_view_DTO>> GetAll_Range_plan_getfor_View(Filter_RangePlan_DataTable param)
        {
            try
            {
                var searchtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM range_plan_getfor_view( @year_id_filter,@event_id_filter,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_range_plan_getfor_view_DTO>(query,
                        new
                        {
                            year_id_filter = param.fiscal_year_id,
                            event_id_filter = param.event_id,
                            item_type_id_filter = param.style_item_type_id,
                            product_type_id_filter = param.style_item_product_id,
                            gender_id_filter = param.style_gender_id,
                            origin_id_filter = param.style_item_origin_id,
                            master_category_id_filter = param.style_master_category_id,
                            row_index = param.Start,
                            page_size = param.Length,
                            search_text = searchtext
                        }
                        ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_style_group_size_DTO>> GetAll_style_group_sizeAsync()
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_style_group_size( )";

                    var dataList = connection.Query<rpc_sp_get_style_group_size_DTO>(query,
                          new { }
                         ).AsList();

                    return dataList;

                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<get_table_data_count_for_select2_DTO> GetCountForSelect2Async()
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM rpc_get_table_data_count_for_select2( )";

                    var dataList = connection.Query<get_table_data_count_for_select2_DTO>(query,
                          new { }
                         ).AsList();

                    return dataList.FirstOrDefault();

                }



            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<rpd_sp_get_style_group_size_by_fiscalyearid_DTO>> GetAll_style_group_size_by_fiscalyearid(Int64? fiscal_year_id, Int64? evet_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_style_group_size_by_fiscalyearid( @_fiscal_year_id,@_event_id)";

                    var dataList = connection.Query<rpd_sp_get_style_group_size_by_fiscalyearid_DTO>(query,
                          new
                          {
                              _fiscal_year_id = fiscal_year_id,
                              _event_id = evet_id
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<rpc_sp_get_tran_range_plan_events_DTO>> GetAll_Tran_range_plan_eventsAsync(Int64? fiscal_year_id,string search,Int64? primaryid)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_tran_range_plan_events( @_fiscal_year_id,@search_text,@_primary_id)";

                    var dataList = connection.Query<rpc_sp_get_tran_range_plan_events_DTO>(query,
                          new { 
                              _fiscal_year_id = fiscal_year_id,
                              search_text=search,
                              _primary_id =primaryid
                          }
                         ).AsList();

                    return dataList;

                }





            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public async Task<List<rpc_sp_get_tran_range_plan_summary_DTO>> GetAll_Tran_range_plan_summaryAsync()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_tran_range_plan_summary( )";

                    var dataList = connection.Query<rpc_sp_get_tran_range_plan_summary_DTO>(query,
                          new { }
                         ).AsList();

                    return dataList;

                }





            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_tran_va_plan_summary_DTO>> GetAll_VA_plan_summaryAsync()
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_tran_va_plan_summary( )";

                    var dataList = connection.Query<rpc_sp_get_tran_va_plan_summary_DTO>(query,
                          new { }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_va_plan_events_DTO>> GetAllsp_get_va_plan_eventsAsync(Int64? fiscal_year_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_va_plan_events( @_fiscal_year_id)";

                    var dataList = connection.Query<rpc_sp_get_va_plan_events_DTO>(query,
                          new { _fiscal_year_id = fiscal_year_id }
                         ).AsList();

                    return dataList;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<rpc_sp_get_va_plan_events_DTO>> GetSampleOrderTradingEventData(Int64? fiscal_year_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_va_plan_events( @_fiscal_year_id)";

                    var dataList = connection.Query<rpc_sp_get_va_plan_events_DTO>(query,
                          new { _fiscal_year_id = fiscal_year_id }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_plan_allocate_getfor_createupdate_DTO>> GetAllva_plan_getfor_createupdateAsync(Filter_RangePlan_DataTable param)
        {
            try
            {
                var searchtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM tran_plan_allocate_getfor_createupdate( @year_id_filter,@event_id_filter,@filter_option,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_plan_allocate_getfor_createupdate_DTO>(query,
                         new
                         {
                             year_id_filter = param.fiscal_year_id,
                             event_id_filter = param.event_id,
                             filter_option = param.filter_option,
                             item_type_id_filter = param.style_item_type_id,
                             product_type_id_filter = param.style_product_type_id,
                             gender_id_filter = param.style_gender_id,
                             origin_id_filter = param.style_item_origin_id,
                             master_category_id_filter = param.style_master_category_id,
                             row_index = param.Start,
                             page_size = param.Length,
                             search_text = searchtext
                         }
                         ).AsList();

                    return dataList;

                }





            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        
          public async Task<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>> GetAllsp_get_color_detl_size_by_style_id(Int64? tran_va_plan_detl_style_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_color_detl_size_by_style_id( @tran_va_plan_detl_style_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_color_detl_size_by_vaplandetlid_Root_DTO>(query,
                          new { tran_va_plan_detl_style_id_filter= tran_va_plan_detl_style_id_filter }
                         ).AsList().FirstOrDefault();

                    var extList = JsonConvert.DeserializeObject<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>>(dataList.style_product_size_details);

                    return extList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>> GetAllsp_get_color_detl_size_by_vaplandetlidAsync(Int64? va_plan_detl_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_color_detl_size_by_vaplandetlid( @va_plan_detl_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_color_detl_size_by_vaplandetlid_Root_DTO>(query,
                          new { va_plan_detl_id_filter }
                         ).AsList().FirstOrDefault();

                    var extList = JsonConvert.DeserializeObject<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>>(dataList.style_product_size_details);

                    return extList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<List<rpc_va_plan_get_designer_assign_details_DTO>> GetAllva_plan_get_designer_assign_detailsAsync
            (Int64 fiscal_id_filter, Int64 event_id_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM va_plan_get_designer_assign_details( @fiscal_year_id_filter,@event_id_filter)";

                    var dataList = connection.Query<rpc_va_plan_get_designer_assign_details_DTO>(query,
                          new
                          {
                              fiscal_year_id_filter = fiscal_id_filter,
                              event_id_filter = event_id_filter
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_va_plan_get_designer_assign_details_det_DTO>> GetAllva_plan_get_designer_assign_details_det_by_detidAsync(Int64 planning_detl_id_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM va_plan_get_designer_assign_details_det( @plan_detl_id_filter)";

                    var dataList = connection.Query<rpc_va_plan_get_designer_assign_details_det_DTO>(query,
                          new { plan_detl_id_filter = planning_detl_id_filter }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<List<rpc_va_plan_get_designer_assign_details_det_DTO>> GetAllva_plan_get_designer_assign_details_detAsync
            (Int64 fiscal_id_filter, Int64 event_id_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM va_plan_get_designer_assign_details_det(@fiscal_year_id_filter,@event_id_filter)";

                    var dataList = connection.Query<rpc_va_plan_get_designer_assign_details_det_DTO>(query,
                          new
                          {
                              fiscal_year_id_filter = fiscal_id_filter,
                              event_id_filter = event_id_filter
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_style_product_item_DTO>> GetAllsp_get_style_product_itemAsync(
          
            Int64? style_master_category_id_filter,
            Int64? style_gender_id_filter,
            Int64? style_item_origin_id_filter,
            Int64? style_item_type_id_filter,
            Int64? style_product_type_id_filter, string search
            )
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_style_product_item( @style_master_category_id_filter,@style_gender_id_filter,@style_item_origin_id_filter,@style_item_type_id_filter,@style_product_type_id_filter, @search_text)";

                    var dataList = connection.Query<rpc_sp_get_style_product_item_DTO>(query,
                          new
                          {
                             
                              style_master_category_id_filter,
                              style_gender_id_filter,
                              style_item_origin_id_filter,
                              style_item_type_id_filter,
                              style_product_type_id_filter,
                              search_text=search
                          }
                         ).AsList();

                    return dataList;

                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_data_for_sampleorder_DTO>> GetAllsp_get_data_for_sampleorderAsync(
              Int64 fiscal_year_id, Int64 event_id_filter, Int64? userid_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_sampleorder( @event_id_filter,@fiscal_year_id_filter,@userid_filter)";

                    var dataList = connection.Query<rpc_sp_get_data_for_sampleorder_DTO>(query,
                          new
                          {
                              event_id_filter,
                              fiscal_year_id_filter = fiscal_year_id,
                              userid_filter
                          }
                         ).AsList();

                    return dataList;

                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_mapped_item_structure_DTO>> GetAllsp_get_mapped_item_structureAsync(Int64 style_master_category_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_mapped_item_structure( @style_master_category_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_mapped_item_structure_DTO>(query,
                          new { style_master_category_id_filter }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_sample_order_details_DTO>> GetAllsp_get_sample_order_detailsAsync(Int64 designer_member_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_sample_order_details( @designer_member_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_sample_order_details_DTO>(query,
                          new { designer_member_id_filter }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_sampleorder_subgroup_det_DTO>> GetAllsp_get_sampleorder_subgroup_detAsync(Int64 tran_sample_order_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_sampleorder_subgroup_det( @tran_sample_order_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_sampleorder_subgroup_det_DTO>(query,
                          new { tran_sample_order_id_filter }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_sampleorder_embellishment_det_DTO>> GetAllsp_get_sampleorder_embellishment_detAsync(Int64 tran_sample_order_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_sampleorder_embellishment_det( @tran_sample_order_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_sampleorder_embellishment_det_DTO>(query,
                          new { tran_sample_order_id_filter }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_pre_costing_details_DTO>> GetAllsp_get_pre_costing_detailsAsync(Int64 designer_member_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_pre_costing_details( @designer_member_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_pre_costing_details_DTO>(query,
                          new { designer_member_id_filter }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_data_for_pre_costing_DTO>> GetAllsp_get_data_for_pre_costingAsync
            (Filter_RangePlan_DataTable param)
        {
            try
            {
                var searchtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_pre_costing( @event_id_filter,@fiscal_year_id_filter,@userid_filter,@data_filter_type,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_sp_get_data_for_pre_costing_DTO>(query,
                          new
                          {
                              event_id_filter = param.event_id,
                              fiscal_year_id_filter = param.fiscal_year_id,
                              userid_filter = param.userid,
                              data_filter_type = param.filter_option,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searchtext
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_mapped_segment_by_gen_item_structure_group_sub_id_DTO>> GetAllsp_get_mapped_segment_by_gen_item_structure_group_sub_idAsync(Int64 gen_item_structure_group_sub_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_mapped_segment_by_gen_item_structure_group_sub_id( @gen_item_structure_group_sub_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_mapped_segment_by_gen_item_structure_group_sub_id_DTO>(query,
                          new { gen_item_structure_group_sub_id_filter }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<rpc_sp_get_measurement_unit_details_by_masterid_DTO>> GetAllsp_get_measurement_unit_details_by_masteridAsync(Int64 gen_measurement_unit_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_measurement_unit_details_by_masterid( @gen_measurement_unit_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_measurement_unit_details_by_masterid_DTO>(query,
                          new { gen_measurement_unit_id_filter }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public async Task<List<rpc_sp_get_event_details_allphase_DTO>> GetAllsp_get_event_details_allphaseAsync(Int64? _fiscal_year_id_filter, Int64? _event_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_event_details_allphase( @_fiscal_year_id_filter,@_event_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_event_details_allphase_DTO>(query,
                          new
                          {
                              _fiscal_year_id_filter,
                              _event_id_filter
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<rpc_sp_get_data_for_designer_review_DTO>> GetAllsp_get_data_for_designer_reviewAsync
            (Filter_RangePlan_DataTable param)
        {
            try
            {
                var searchtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_designer_review( @event_id_filter,@fiscal_year_id_filter,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_sp_get_data_for_designer_review_DTO>(query,
                          new
                          {
                              event_id_filter = param.event_id,
                              fiscal_year_id_filter = param.fiscal_year_id,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searchtext
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        public async Task<List<rpc_sp_get_data_for_designer_review_DTO>> GetAllsp_get_data_for_designer_Approval_reviewAsync
            (Filter_RangePlan_DataTable param)
        {
            try
            {
                var searchvalue = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_designer_approval_review( @event_id_filter,@fiscal_year_id_filter,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_sp_get_data_for_designer_review_DTO>(query,
                          new
                          {
                              event_id_filter = param.event_id,
                              fiscal_year_id_filter = param.fiscal_year_id,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searchvalue
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<rpc_sp_get_pre_costing_details_by_masterid_DTO>> GetAllsp_get_pre_costing_details_by_masteridAsync(Int64 pre_costing_id_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_pre_costing_details_by_masterid( @pre_costing_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_pre_costing_details_by_masterid_DTO>(query,
                          new { pre_costing_id_filter }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<rpc_sp_get_data_for_techpack_DTO>> GetAllsp_get_data_for_techpackAsync
            (Filter_RangePlan_DataTable param)
        {
            try
            {
                var searhtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_techpack( @event_id_filter,@fiscal_year_id_filter,@is_ack_filter,@data_filter_type,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@designer_user_id,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_sp_get_data_for_techpack_DTO>(query,
                          new
                          {
                              event_id_filter = param.event_id,
                              fiscal_year_id_filter = param.fiscal_year_id,
                              is_ack_filter = param.is_ack,
                              data_filter_type = param.filter_option,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              designer_user_id= param.userid,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searhtext
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<List<rpc_sp_get_data_for_techpack_DTO>> GetAllsp_get_data_for_techpack_ackAsync
         (Filter_RangePlan_DataTable param)
        {
            try
            {
                var searhtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_techpack_ack( @event_id_filter,@fiscal_year_id_filter,@is_ack_filter,@data_filter_type,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@merchant_user_id,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_sp_get_data_for_techpack_DTO>(query,
                          new
                          {
                              event_id_filter = param.event_id,
                              fiscal_year_id_filter = param.fiscal_year_id,
                              is_ack_filter = param.is_ack,
                              data_filter_type = param.filter_option,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              merchant_user_id = param.userid,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searhtext
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_bp_event_month_dto>> GetAllget_bp_year_event_month_dataAsync(Int64 p_tran_bp_year_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM get_bp_year_event_month_data( @p_tran_bp_year_id)";

                    var dataList = connection.Query<tran_bp_event_month_dto>(query,
                          new { p_tran_bp_year_id }
                         ).AsList();

                    return dataList;

                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_sp_get_sample_order_data_DTO> GetAllproc_sp_get_sample_order_dataAsync
            (Int64 fiscal_year_id_filter, Int64 style_item_product_id_filter, Int64 team_category_id_filter, Int64 tran_va_plan_detl_id_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_sample_order_data( @fiscal_year_id_filter,@tran_va_plan_detl_id_filter,@style_item_product_id_filter,@team_category_id_filter)";

                    var objRet = connection.Query<rpc_proc_sp_get_sample_order_data_DTO>(query,
                          new
                          {
                              fiscal_year_id_filter,
                              tran_va_plan_detl_id_filter,
                              style_item_product_id_filter,
                              team_category_id_filter
                          }
                         ).FirstOrDefault();

                    objRet.obj_style_fit_info_list = !string.IsNullOrEmpty(objRet.style_fit_info_list) ? JsonConvert.DeserializeObject<List<style_fit_info_DTO>>(objRet.style_fit_info_list) : new List<style_fit_info_DTO>();

                    objRet.obj_style_pattern_list = !string.IsNullOrEmpty(objRet.style_pattern_list) ? JsonConvert.DeserializeObject<List<style_pattern_DTO>>(objRet.style_pattern_list) : new List<style_pattern_DTO>();

                    return objRet;

                }



            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<rpc_proc_sp_get_sample_order_data_edit_DTO> GetAllproc_sp_get_sample_order_data_editAsync(Int64? tran_sample_order_id_filter
        , Int64? fiscal_year_id_filter, Int64? style_item_product_id_filter, Int64? team_category_id_filter
        )
        {
            try
            {
                List<file_upload> objfile_list = new List<file_upload>();

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_sample_order_data_edit( @tran_sample_order_id_filter,@style_item_product_id_filter,@fiscal_year_id_filter,@team_category_id_filter)";

                    var objRet = connection.Query<rpc_proc_sp_get_sample_order_data_edit_DTO>(query,
                          new
                          {
                              tran_sample_order_id_filter,
                              style_item_product_id_filter,
                              fiscal_year_id_filter,
                              team_category_id_filter
                          }
                         ).FirstOrDefault();



                    if (!string.IsNullOrEmpty(objRet.sample_photos))
                    {
                        var list_sample_photos = JsonConvert.DeserializeObject<List<file_upload>>(objRet.sample_photos);

                        foreach (var file in list_sample_photos)
                        {
                            try
                            {
                                // var file = JsonConvert.DeserializeObject<file_upload>(str.ToString());

                                // var bytes = await _connectionSupabse.Storage.From("sailor_bucket").Download("sample_order/" + file.server_filename, null);
                                string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                                string filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/sample_order/" + file.server_filename;

                                if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                                {
                                    // Read the file into a byte array
                                    var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                                    string base64String = Convert.ToBase64String(bytes);

                                    var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                                    if (file_extension == "pdf")
                                    {
                                        file.base64string = base64String;
                                    }
                                    else
                                    {
                                        file.base64string = $"data:image/{file_extension};base64,{base64String}";

                                    }

                                    file.current_state = 2;

                                    objfile_list.Add(file);
                                }
                            }
                            catch (Exception ex)
                            {

                                using (LogContext.PushProperty("SpecialLogType", true))
                                {
                                    // _logger.LogError(ex.ToString());
                                }
                            }

                        }
                    }

                    objRet.List_SampleOrderFiles = objfile_list;

                    objRet.obj_style_fit_info_list = !string.IsNullOrEmpty(objRet.style_fit_info_list) ? JsonConvert.DeserializeObject<List<style_fit_info_DTO>>(objRet.style_fit_info_list) : new List<style_fit_info_DTO>();

                    objRet.obj_style_pattern_list = !string.IsNullOrEmpty(objRet.style_pattern_list) ? JsonConvert.DeserializeObject<List<style_pattern_DTO>>(objRet.style_pattern_list) : new List<style_pattern_DTO>();

                    objRet.obj_style_fit_info = !string.IsNullOrEmpty(objRet.fit_name) ? JsonConvert.DeserializeObject<style_fit_info_DTO>(objRet.fit_name) : new style_fit_info_DTO();

                    objRet.obj_style_pattern = !string.IsNullOrEmpty(objRet.pattern) ? JsonConvert.DeserializeObject<style_pattern_DTO>(objRet.pattern) : new style_pattern_DTO();


                    return objRet;





                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_sp_get_pre_costing_data_DTO> GetAllproc_sp_get_pre_costing_dataAsync(Int64? tran_sample_order_id_filter
        , Int64? style_item_product_id_filter, Int64? fiscal_year_id_filter, Int64? team_category_id_filter
        )
        {
            try
            {
                List<file_upload> objfile_list = new List<file_upload>();

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_pre_costing_data( @tran_sample_order_id_filter,@style_item_product_id_filter,@fiscal_year_id_filter,@team_category_id_filter)";

                    var objRet = connection.QueryFirstOrDefault<rpc_proc_sp_get_pre_costing_data_DTO>(query,
                          new
                          {
                              tran_sample_order_id_filter,
                              style_item_product_id_filter,
                              fiscal_year_id_filter,
                              team_category_id_filter
                          }
                         );



                    if (objRet != null)
                    {
                        if (!string.IsNullOrEmpty(objRet.sample_photos))
                        {
                            var list_photoes = JsonConvert.DeserializeObject<List<file_upload>>(objRet.sample_photos);

                            foreach (var file in list_photoes)
                            {
                                try
                                {
                                    //var file = JsonConvert.DeserializeObject<file_upload>(str.ToString());

                                    // var bytes = await _connectionSupabse.Storage.From("sailor_bucket").Download("sample_order/" + file.server_filename, null);
                                    string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                                    string filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/sample_order/" + file.server_filename;

                                    if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                                    {
                                        // Read the file into a byte array
                                        var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                                        string base64String = Convert.ToBase64String(bytes);

                                        var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                                        if (file_extension == "pdf")
                                        {
                                            file.base64string = base64String;
                                        }
                                        else
                                        {
                                            file.base64string = $"data:image/{file_extension};base64,{base64String}";

                                        }

                                        file.current_state = 2;

                                        objfile_list.Add(file);
                                    }
                                }
                                catch (Exception ex)
                                {

                                    using (LogContext.PushProperty("SpecialLogType", true))
                                    {
                                        // _logger.LogError(ex.ToString());
                                    }
                                }

                            }
                        }

                        objRet.List_SampleOrderFiles = objfile_list;
                    }

                    return objRet;




                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_sp_get_pre_costing_data_edit_DTO> GetAllproc_sp_get_pre_costing_data_editAsync(Int64? tran_pre_costing_id_filter
        , Int64? tran_sample_order_id_filter, Int64? style_item_product_id_filter, Int64? fiscal_year_id_filter
        , Int64? team_category_id_filter
        )
        {
            try
            {

                List<file_upload> objfile_list = new List<file_upload>();

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_pre_costing_data_edit( @tran_pre_costing_id_filter,@tran_sample_order_id_filter,@style_item_product_id_filter,@fiscal_year_id_filter,@team_category_id_filter)";

                    var objRet = connection.QueryFirstOrDefault<rpc_proc_sp_get_pre_costing_data_edit_DTO>(query,
                          new
                          {
                              tran_pre_costing_id_filter,
                              tran_sample_order_id_filter,
                              style_item_product_id_filter,
                              fiscal_year_id_filter,
                              team_category_id_filter
                          }
                         );

                    if (objRet != null)
                    {
                        if (!string.IsNullOrEmpty(objRet.sample_photos))
                        {
                            var photo_list = JsonConvert.DeserializeObject<List<file_upload>>(objRet.sample_photos);

                            foreach (var file in photo_list)
                            {
                                try
                                {

                                    string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                                    string filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/sample_order/" + file.server_filename;

                                    if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                                    {
                                        var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                                        string base64String = Convert.ToBase64String(bytes);

                                        var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                                        if (file_extension == "pdf")
                                        {
                                            file.base64string =base64String;
                                        }
                                        else
                                        {
                                            file.base64string = $"data:image/{file_extension};base64,{base64String}";

                                        }

                                        file.current_state = 2;

                                        objfile_list.Add(file);
                                    }
                                }
                                catch (Exception ex)
                                {

                                    using (LogContext.PushProperty("SpecialLogType", true))
                                    {
                                        // _logger.LogError(ex.ToString());
                                    }
                                }

                            }
                        }

                        objRet.List_SampleOrderFiles = objfile_list;
                    }

                    return objRet;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<rpc_proc_sp_get_tech_pack_data_DTO> GetAllproc_sp_get_tech_pack_dataAsync(Int64? tran_pre_costing_id_filter
        , Int64? tran_sample_order_id_filter
        , Int64? style_item_product_id_filter
        , Int64? fiscal_year_id_filter
        )
        {
            try
            {
                List<file_upload> objfile_list = new List<file_upload>();
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_tech_pack_data( @tran_pre_costing_id_filter,@tran_sample_order_id_filter,@style_item_product_id_filter,@fiscal_year_id_filter)";

                    var objRet = connection.Query<rpc_proc_sp_get_tech_pack_data_DTO>(query,
                          new
                          {
                              tran_pre_costing_id_filter,
                              tran_sample_order_id_filter,
                              style_item_product_id_filter,
                              fiscal_year_id_filter
                          }
                         ).FirstOrDefault();

                    if (!string.IsNullOrEmpty(objRet.sample_photos))
                    {
                        var photo_list = JsonConvert.DeserializeObject<List<file_upload>>(objRet.sample_photos);

                        foreach (var file in photo_list)
                        {
                            try
                            {

                                string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                                string filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/sample_order/" + file.server_filename;

                                if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                                {
                                    var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                                    string base64String = Convert.ToBase64String(bytes);

                                    var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                                    if (file_extension == "pdf")
                                    {
                                        file.base64string = base64String;
                                    }
                                    else
                                    {
                                        file.base64string = $"data:image/{file_extension};base64,{base64String}";

                                    }

                                    file.current_state = 2;

                                    objfile_list.Add(file);
                                }
                            }
                            catch (Exception ex)
                            {

                                using (LogContext.PushProperty("SpecialLogType", true))
                                {
                                    // _logger.LogError(ex.ToString());
                                }
                            }

                        }
                    }

                    objRet.List_SampleOrderFiles = objfile_list;

                    if (!string.IsNullOrEmpty(objRet.styles_leeve_info_list))
                        objRet.List_style_sleeve_info = JsonConvert.DeserializeObject<List<style_sleeve_info_DTO>>(objRet.styles_leeve_info_list);
                    if (!string.IsNullOrEmpty(objRet.style_product_composition_list))
                        objRet.List_style_product_composition = JsonConvert.DeserializeObject<List<style_product_composition_DTO>>(objRet.style_product_composition_list);

                    return objRet;

                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_sp_get_tech_pack_data_edit_DTO> GetAllproc_sp_get_tech_pack_data_editAsync(Int64? tran_tech_pack_id_filter
        , Int64? tran_pre_costing_id_filter
        , Int64? tran_sample_order_id_filter
        , Int64? style_item_product_id_filter
        , Int64? fiscal_year_id_filter
        , Int64? team_category_id_filter
        )
        {
            List<file_upload> sampleorderfile_list = new List<file_upload>();

            List<file_upload> techpackfile_list = new List<file_upload>();

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_tech_pack_data_edit( @tran_tech_pack_id_filter,@tran_pre_costing_id_filter,@tran_sample_order_id_filter,@style_item_product_id_filter,@fiscal_year_id_filter)";

                    var objRet = connection.Query<rpc_proc_sp_get_tech_pack_data_edit_DTO>(query,
                          new
                          {
                              tran_tech_pack_id_filter,
                              tran_pre_costing_id_filter,
                              tran_sample_order_id_filter,
                              style_item_product_id_filter,
                              fiscal_year_id_filter
                          }
                         ).FirstOrDefault();

                    if (!string.IsNullOrEmpty(objRet.sample_photos))
                    {
                        var list_sample_photos = JsonConvert.DeserializeObject<List<file_upload>>(objRet.sample_photos);

                        foreach (var file in list_sample_photos)
                        {
                            try
                            {
                                // var file = JsonConvert.DeserializeObject<file_upload>(str.ToString());

                                string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                                string filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/sample_order/" + file.server_filename;

                                if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                                {

                                    var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                                    string base64String = Convert.ToBase64String(bytes);

                                    var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                                    if (file_extension == "pdf")
                                    {
                                        file.base64string = base64String;
                                    }
                                    else
                                    {
                                        file.base64string = $"data:image/{file_extension};base64,{base64String}";

                                    }

                                    file.current_state = 2;

                                    sampleorderfile_list.Add(file);
                                }
                            }
                            catch (Exception ex)
                            {

                                using (LogContext.PushProperty("SpecialLogType", true))
                                {
                                    //_logger.LogError(ex.ToString());
                                }
                            }

                        }
                    }

                    objRet.List_SampleOrderFiles = sampleorderfile_list;

                    if (!string.IsNullOrEmpty(objRet.photos))
                    {
                        var list_sample_photos = JsonConvert.DeserializeObject<List<file_upload>>(objRet.photos);

                        foreach (var file in list_sample_photos)
                        {
                            try
                            {
                                // var file = JsonConvert.DeserializeObject<file_upload>(str.ToString());

                                string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                                string filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/techpack/" + file.server_filename;

                                if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                                {

                                    var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                                    string base64String = Convert.ToBase64String(bytes);

                                    var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                                    if (file_extension == "pdf")
                                    {
                                        file.base64string = base64String;
                                    }
                                    else
                                    {
                                        file.base64string = $"data:image/{file_extension};base64,{base64String}";

                                    }

                                    file.current_state = 2;

                                    techpackfile_list.Add(file);
                                }
                            }
                            catch (Exception ex)
                            {

                                using (LogContext.PushProperty("SpecialLogType", true))
                                {
                                    // _logger.LogError(ex.ToString());
                                }
                            }

                        }
                    }

                    objRet.List_TechPackFiles = techpackfile_list;

                    if (!string.IsNullOrEmpty(objRet.sleeve_details))
                        objRet.obj_sleeve_details = JsonConvert.DeserializeObject<style_sleeve_info_DTO>(objRet.sleeve_details);

                    if (!string.IsNullOrEmpty(objRet.product_composition))
                        objRet.obj_product_composition = JsonConvert.DeserializeObject<style_product_composition_DTO>(objRet.product_composition);

                    if (!string.IsNullOrEmpty(objRet.styles_leeve_info_list))
                        objRet.List_style_sleeve_info = JsonConvert.DeserializeObject<List<style_sleeve_info_DTO>>(objRet.styles_leeve_info_list);
                    if (!string.IsNullOrEmpty(objRet.style_product_composition_list))
                        objRet.List_style_product_composition = JsonConvert.DeserializeObject<List<style_product_composition_DTO>>(objRet.style_product_composition_list);

                    if (!string.IsNullOrEmpty(objRet.fit_name))
                        objRet.objt_fit_name = JsonConvert.DeserializeObject<style_fit_info_DTO>(objRet.fit_name);
                    if (!string.IsNullOrEmpty(objRet.pattern))
                        objRet.obj_pattern = JsonConvert.DeserializeObject<style_pattern_DTO>(objRet.pattern);


                    return objRet;

                }










            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<rpc_get_user_info_DTO> GetAllget_user_infoAsync(String user_name_flter)
        {
            try
            {
                List<rpc_get_user_info_DTO> dataList = new List<rpc_get_user_info_DTO>();


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string functionName = "get_user_info";

                    string query = $"SELECT * FROM get_user_info(@user_name_flter)";

                    dataList = connection.Query<rpc_get_user_info_DTO>(query,
                        new { user_name_flter = user_name_flter }
                        ).AsList();

                    return dataList.FirstOrDefault();

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_get_filter_items_DTO> GetAllproc_get_filter_itemsAsync(Int64? fiscal_year_id_filter)
        {
            List<rpc_proc_get_filter_items_DTO> dataList = new List<rpc_proc_get_filter_items_DTO>();

            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_get_filter_items( @fiscal_year_id_filter)";

                    dataList = connection.Query<rpc_proc_get_filter_items_DTO>(query,
                         new { fiscal_year_id_filter }
                        ).AsList();

                    return dataList.FirstOrDefault();

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_get_supplier_new_DTO> GetAllproc_get_supplier_newAsync()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_get_supplier_new( )";

                    var ret = connection.Query<rpc_proc_get_supplier_new_DTO>(query,
                          new { }
                         ).AsList().FirstOrDefault();

                    ret.List_bank = JsonConvert.DeserializeObject<List<gen_bank_DTO>>(ret.genbank_list);

                    ret.List_bankbranch = JsonConvert.DeserializeObject<List<gen_bank_branch_DTO>>(ret.genbankbranch_list);

                    ret.List_country = JsonConvert.DeserializeObject<List<gen_country_DTO>>(ret.gencountry_list);

                    ret.List_contact_category = JsonConvert.DeserializeObject<List<gen_contact_category_DTO>>(ret.gen_contact_category_list);

                    ret.List_geographical_location = JsonConvert.DeserializeObject<List<gen_geographical_location_DTO>>(ret.gen_geographical_location_list);

                    ret.List_incoterm = JsonConvert.DeserializeObject<List<gen_inco_term_DTO>>(ret.genincoterm_list);

                    ret.List_paymentterm = JsonConvert.DeserializeObject<List<gen_payment_term_DTO>>(ret.genpaymentterm_list);

                    ret.List_paymentmethod = JsonConvert.DeserializeObject<List<gen_payment_method_DTO>>(ret.genpaymentmethod_list);

                    ret.List_mode_of_transaction = JsonConvert.DeserializeObject<List<gen_mode_of_transaction_DTO>>(ret.genmodeofransaction_list);

                    ret.List_itemstructuregroupsub = JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_DTO>>(ret.genitemstructuregroupsub_list);

                    ret.List_creditperiod = JsonConvert.DeserializeObject<List<gen_credit_period_DTO>>(ret.gencreditperiod_list);

                    ret.List_calculationoftenure = JsonConvert.DeserializeObject<List<gen_calculation_of_tenure_DTO>>(ret.gencalculationoftenure_list);

                    return ret;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_rpt_bp_data_month_wise_DTO>> GetAllrpt_bp_data_month_wiseAsync(Int64? fiscal_year_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM rpt_bp_data_month_wise( @fiscal_year_filter)";

                    var dataList = connection.Query<rpc_rpt_bp_data_month_wise_DTO>(query,
                          new { fiscal_year_filter }
                         ).AsList();

                    return dataList;

                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<List<rpc_rpt_bp_data_outlet_wise_DTO>> GetAllrpt_bp_data_outlet_wiseAsync(Int64? fiscal_year_filter)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM rpt_bp_data_outlet_wise( @fiscal_year_filter)";

                    var dataList = connection.Query<rpc_rpt_bp_data_outlet_wise_DTO>(query,
                          new { fiscal_year_filter }
                         ).AsList();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<rpc_proc_gen_item_structure_group_sub_new_DTO> GetAllproc_gen_item_structure_group_sub_newAsync()
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_gen_item_structure_group_sub_new( )";

                    var dataList = connection.Query<rpc_proc_gen_item_structure_group_sub_new_DTO>(query,
                          new { }
                         ).FirstOrDefault();

                    return dataList;

                }



            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_gen_item_structure_group_sub_edit_DTO> GetAllproc_gen_item_structure_group_sub_editAsync(Int64? gen_item_structure_group_sub_id_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_gen_item_structure_group_sub_edit( @gen_item_structure_group_sub_id_filter)";

                    var objRet = connection.Query<rpc_proc_gen_item_structure_group_sub_edit_DTO>(query,
                          new { gen_item_structure_group_sub_id_filter }
                         ).FirstOrDefault();

                    objRet.List_gen_item_structure_group = JsonConvert.DeserializeObject<List<gen_item_structure_group_DTO>>(objRet.gen_item_structure_group_list);
                    objRet.List_gen_measurement_unit = JsonConvert.DeserializeObject<List<gen_measurement_unit_DTO>>(objRet.gen_measurement_unit_list);
                    objRet.List_measurement_unit_detail = JsonConvert.DeserializeObject<List<gen_measurement_unit_detail_DTO>>(objRet.gen_measurement_unit_detail_list);

                    if (!string.IsNullOrEmpty(objRet.gen_segment_mapp_list))
                        objRet.List_item_structure_group_sub_segment_mapping = JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_segment_mapping_DTO>>(objRet.gen_segment_mapp_list).OrderBy(p => p.gen_item_structure_group_sub_segment_mapping_id).ToList();
                    return objRet;

                }



            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_item_master_entity>> GetAllget_gen_item_master_by_techpack_idAsync(Int64? tran_techpack_id_filter, Int64? item_structure_sub_group_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM get_gen_item_master_by_techpack_id( @tran_techpack_id_filter,@item_structure_sub_group_id_filter)";

                    var dataList = connection.Query<gen_item_master_entity>(query,
                          new
                          {
                              tran_techpack_id_filter,
                              item_structure_sub_group_id_filter = item_structure_sub_group_id
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_data_for_pre_costing_review_DTO>> GetAllsp_get_data_for_pre_costing_reviewAsync(Int64? fiscal_year_id_filter,
            Int64? event_id_filter, Int64? designer_userid_filter, Int64? merchant_userid_filter, Int64? data_filter_type, string search)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_pre_costing_review( @event_id_filter,@fiscal_year_id_filter,@designer_userid_filter,@merchant_userid_filter,@data_filter_type, @search_text)";

                    var dataList = connection.Query<rpc_sp_get_data_for_pre_costing_review_DTO>(query,
                          new
                          {
                              event_id_filter,
                              fiscal_year_id_filter,
                              designer_userid_filter,
                              merchant_userid_filter,
                              data_filter_type,
                              search_text=search
                          }
                         ).AsList();

                    return dataList;

                }



            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_data_for_pre_costing_for_review_approval_DTO>> GetAllsp_get_data_for_pre_costing_for_review_approvalAsync
            (Int64? fiscal_year_id_filter, Int64? event_id_filter
              , Int64? designer_userid_filter, Int64? merchant_userid_filter, Int64? data_filter_type)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_pre_costing_for_review_approval( @event_id_filter,@fiscal_year_id_filter,@designer_userid_filter,@merchant_userid_filter,@data_filter_type)";

                    var dataList = connection.Query<rpc_sp_get_data_for_pre_costing_for_review_approval_DTO>(query,
                          new
                          {
                              event_id_filter,
                              fiscal_year_id_filter,
                              designer_userid_filter,
                              merchant_userid_filter,
                              data_filter_type
                          }
                         ).AsList();

                    return dataList;

                }



            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_sp_get_pre_costing_modification_request_data__DTO> GetAllproc_sp_get_pre_costing_modification_request_data_Async(Int64? tran_pre_costing_review_id_filter
            , Int64? tran_pre_costing_id_filter
            , Int64? tran_sample_order_id_filter
            , Int64? style_item_product_id_filter
            , Int64? fiscal_year_id_filter
            , Int64? team_category_id_filter
            )
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_pre_costing_modification_request_data_( @tran_pre_costing_review_id_filter,@tran_pre_costing_id_filter,@tran_sample_order_id_filter,@style_item_product_id_filter,@fiscal_year_id_filter,@team_category_id_filter)";

                    var objRet = connection.Query<rpc_proc_sp_get_pre_costing_modification_request_data__DTO>(query,
                          new
                          {
                              tran_pre_costing_review_id_filter,
                              tran_pre_costing_id_filter,
                              tran_sample_order_id_filter,
                              style_item_product_id_filter,
                              fiscal_year_id_filter,
                              team_category_id_filter
                          }
                         ).FirstOrDefault();



                    List<file_upload> objfile_list = new List<file_upload>();


                    if (objRet != null)
                    {
                        if (!string.IsNullOrEmpty( objRet.sample_photos))
                        {
                            var list = JsonConvert.DeserializeObject<List<file_upload>>(objRet.sample_photos);

                            foreach (file_upload file in list)
                            {
                                try
                                {
                                    //var file = JsonConvert.DeserializeObject<file_upload>(str.ToString());

                                    // var bytes = await _connectionSupabse.Storage.From("sailor_bucket").Download("sample_order/" + file.server_filename, null);
                                    string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                                    string filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/sample_order/" + file.server_filename;

                                    if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                                    {
                                        // Read the file into a byte array
                                        var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                                        string base64String = Convert.ToBase64String(bytes);

                                        var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                                        if (file_extension == "pdf")
                                        {
                                            file.base64string = base64String;
                                        }
                                        else
                                        {
                                            file.base64string = $"data:image/{file_extension};base64,{base64String}";

                                        }

                                        file.current_state = 2;

                                        objfile_list.Add(file);
                                    }
                                }
                                catch (Exception ex)
                                {

                                    using (LogContext.PushProperty("SpecialLogType", true))
                                    {
                                        // _logger.LogError(ex.ToString());
                                    }
                                }

                            }
                        }

                        objRet.List_SampleOrderFiles = objfile_list;
                    }

                    return objRet;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_sp_get_data_for_mcd_receive_DTO> GetAllsp_get_data_for_mcd_receiveAsync(Int64? po_id_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_mcd_receive( @po_id_filter)";

                    var dataList = connection.Query<rpc_sp_get_data_for_mcd_receive_DTO>(query,
                          new { po_id_filter }
                         ).AsList();


                    //foreach (var data in dataList)
                    //{

                    //    JObject documentsObject = JObject.Parse(data.documents);
                    //    JObject documentsObject2 = JObject.Parse(data.terms_conditions);

                    //}

                    return dataList.FirstOrDefault();

                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_mcd_receive_DTO>> GetAllJoined_TranMcdReceiveListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 receivedID, Int64 actionType, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, DtSearch search)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_mcd_receive_list( @row_index,@page_size,@query_type,@receivedid,@actionType,@fiscal_year,@p_event_id,@supplier,@p_delivery_unit_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_mcd_receive_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type,
                              receivedid = receivedID,
                              actionType,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              supplier = supplier_id,
                              p_delivery_unit_id = delivery_unit_id,
                              search_text =search.Value
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_tran_mcd_receive_DTO> GetAllJoined_TranMcdReceiveAsync(Int64 row_index, Int64 page_size, string query_type, Int64 receivedID, Int64 actionType,Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id,DtSearch search)
        {
            try
            {

               
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_mcd_receive( @row_index,@page_size,@query_type,@receivedid,@action_type,@fiscal_year_id,@event_id,@supplier_id,@delivery_unit_id ,@search_text)";

                    var dataList = connection.QueryFirstOrDefault<rpc_tran_mcd_receive_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = query_type,
                              receivedid = receivedID,
                              action_type= actionType,
                              fiscal_year_id,
                              event_id,
                              supplier_id,
                              delivery_unit_id,
                              search_text = search?.Value ?? string.Empty

                          }
                         );

                    return dataList;

                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_mcd_receive_detail_DTO>> GetAllJoined_TranMcdReceiveDetailAsync(Int64 receivedID)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_mcd_receive_detail( @received_id_filter)";

                    var dataList = connection.Query<rpc_tran_mcd_receive_detail_DTO>(query,
                          new { received_id_filter = receivedID }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_mcd_receive_detail_DTO>> GetAllJoined_TranMcdReceiveDetailFailAsync(Int64 receivedID)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_mcd_receive_detail_fail( @received_id_filter)";

                    var dataList = connection.Query<rpc_tran_mcd_receive_detail_DTO>(query,
                          new { received_id_filter = receivedID }
                         ).AsList();

                    return dataList;

                }



            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<rpc_tran_mcd_requisition_slip_DTO>> GetAllsp_get_techPack_by_gen_item_structure_group_sub_idAsync(Int64 teckPackId, Int64 gen_item_structure_group_sub_id_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_fabric_detail_by_gen_item_structure_group_sub_id( @tech_pack_id_filter,@gen_item_structure_group_sub_id_filter)";

                    var dataList = connection.Query<rpc_tran_mcd_requisition_slip_DTO>(query,
                          new
                          {
                              tech_pack_id_filter = teckPackId,
                              gen_item_structure_group_sub_id_filter
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<List<rpc_tran_mcd_requisition_slip_DTO>> GetAll_gen_item_structure_group_sub_idAsync_by_requisition_slip(long requisition_slip_id, long sub_group_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_fabric_detail_by_requisition_slip( @p_requisition_slip_id,@p_sub_group_id)";

                    var dataList = connection.Query<rpc_tran_mcd_requisition_slip_DTO>(query,
                          new
                          {
                              p_requisition_slip_id = requisition_slip_id,
                              p_sub_group_id = sub_group_id
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<List<tran_mcd_requisition_slip_DTO>> GetAllJoined_FabricRequisitionSlipListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 requisitionSlipid, Int64 gen_groupid, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_fabric_requisition_slip_draft( @row_index,@page_size,@requisitionslipid,@gen_groupid,@query_type,@p_fiscal_year,@p_event_id,@p_group_id,@p_sub_group_id)";

                    var dataList = connection.Query<tran_mcd_requisition_slip_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              requisitionslipid = requisitionSlipid,
                              gen_groupid,
                              query_type,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_group_id,
                              p_sub_group_id
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_mcd_requisition_slip_DTO>> GetAllJoined_FabricRequisitionSlipProposedListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 requisitionSlipid, Int64 gen_groupid, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_fabric_requisition_slip_proposed( @row_index,@page_size,@requisitionslipid,@gen_groupid,@p_fiscal_year,@p_event_id,@p_group_id,@p_sub_group_id,@query_type)";

                    var dataList = connection.Query<tran_mcd_requisition_slip_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              requisitionslipid = requisitionSlipid,
                              gen_groupid,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_group_id,
                              p_sub_group_id,
                              query_type
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_mcd_requisition_slip_DTO>> GetAllJoined_FabricRequisitionSlipApprovedListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 requisitionSlipid, Int64 gen_groupid, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_fabric_requisition_slip_approved( @row_index,@page_size,@requisitionslipid,@gen_groupid,@p_fiscal_year,@p_event_id,@p_group_id,@p_sub_group_id,@query_type)";

                    var dataList = connection.Query<tran_mcd_requisition_slip_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              requisitionslipid = requisitionSlipid,
                              gen_groupid,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_group_id,
                              p_sub_group_id,
                              query_type
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_tran_mcd_requisition_slip_masterDetail_DTO> Get_master_detail_fabric_requisition_slipAsync(Int64 requisitionSlipid)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT * FROM proc_sp_get_mcd_requisition_slip(@p_requisition_slip_id)";
                    // Create parameters
                    var parameters = new DynamicParameters();
                    parameters.Add("p_requisition_slip_id", string.IsNullOrEmpty(Convert.ToString(requisitionSlipid)) ? null : Convert.ToInt64(requisitionSlipid));

                    // Execute the function call
                    var result = await connection.QueryFirstOrDefaultAsync<rpc_tran_mcd_requisition_slip_masterDetail_DTO>(
                      sqlCommand,
                      parameters
                    );

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_mcd_receive_DTO>> GetTranMcdRejectListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 receivedID, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, string search)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT * FROM proc_sp_get_data_tran_mcd_failorReject(@row_index,@page_size,@query_type,@receivedid,@fiscal_year_id,@event_id,@supplier_id,@search_text)";

                    var parameters = new DynamicParameters();
                    parameters.Add("row_index", row_index);
                    parameters.Add("page_size", page_size);
                    parameters.Add("query_type", query_type);
                    parameters.Add("receivedid", string.IsNullOrEmpty(Convert.ToString(receivedID)) ? null : Convert.ToInt64(receivedID));
                    parameters.Add("fiscal_year_id", fiscal_year_id);
                    parameters.Add("event_id", event_id);
                    parameters.Add("supplier_id", supplier_id);
                    parameters.Add("search_text", search);


                    var result = await connection.QueryAsync<rpc_tran_mcd_receive_DTO>(
                      sqlCommand,
                      parameters
                    );

                    return result.OrderByDescending(x => x.received_id).ToList();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_plan_allocate_getfor_approval_DTO>> GetAllplan_allocate_getfor_approvalAsync(Filter_RangePlan_DataTable param)
        {
            try
            {
                var searchtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM tran_plan_allocate_getfor_approval( @year_id_filter,@event_id_filter,@filter_option,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_plan_allocate_getfor_approval_DTO>(query,
                          new
                          {
                              year_id_filter = param.fiscal_year_id,
                              event_id_filter = param.event_id,
                              filter_option = param.filter_option,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searchtext
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_plan_allocate_getfor_designer_distribution_DTO>> GetAlltran_plan_allocate_getfor_designer_distribution(Filter_RangePlan_DataTable param)
        {
            try
            {
                var searchtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM tran_plan_allocate_getfor_designer_distribution( @year_id_filter,@event_id_filter,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_tran_plan_allocate_getfor_designer_distribution_DTO>(query,
                          new
                          {
                              year_id_filter = param.fiscal_year_id,
                              event_id_filter = param.event_id,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searchtext
                          }
                         ).AsList();

                    return dataList;

                }




            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_sample_order_getfor_create_DTO>> GetAlltran_sample_order_getfor_createAsync(Filter_RangePlan_DataTable param)
        {
            try
            {
                var searchtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM tran_sample_order_getfor_create( @year_id_filter,@event_id_filter,@userid_filter,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_tran_sample_order_getfor_create_DTO>(query,
                          new
                          {
                              year_id_filter = param.fiscal_year_id,
                              event_id_filter = param.event_id,
                              userid_filter = param.userid,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searchtext
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<rpc_tran_sample_order_getfor_create_DTO>> GetAlltran_sample_order_getfor_create_with_trading(Filter_RangePlan_DataTable param)
        {
            try
            {
                var searchtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM tran_sample_order_getfor_create_with_trading( @year_id_filter,@event_id_filter,@userid_filter,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_tran_sample_order_getfor_create_DTO>(query,
                          new
                          {
                              year_id_filter = param.fiscal_year_id,
                              event_id_filter = param.event_id,
                              userid_filter = param.userid,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searchtext
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<tran_mcd_purchase_return_DTO>> GetTranMcdRejectDraftListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, string search)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT * FROM proc_sp_get_data_tran_mcd_purchase_return_draft(@row_index,@page_size,@action_type,@fiscal_year_id,@event_id,@supplier_id,@search_text)";

                    var parameters = new DynamicParameters();
                    parameters.Add("row_index", row_index);
                    parameters.Add("page_size", page_size);
                    parameters.Add("action_type", actiontype);
                    parameters.Add("fiscal_year_id", fiscal_year_id);
                    parameters.Add("event_id", event_id);
                    parameters.Add("supplier_id", supplier_id);
                    parameters.Add("search_text", search);

                    // int timeoutInSeconds = 300;
                    var result = await connection.QueryAsync<tran_mcd_purchase_return_DTO>(
                      sqlCommand,
                      parameters
                    // commandTimeout: timeoutInSeconds
                    );

                    return result.OrderByDescending(x => x.purchase_return_id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_mcd_purchase_return_DTO> Get_master_detail_tran_purchase_return_slipAsync(Int64 purchase_return_id)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT * FROM proc_sp_get_data_tran_mcd_purchase_return(@p_purchase_return_id)";
                    // Create parameters
                    var parameters = new DynamicParameters();
                    parameters.Add("p_purchase_return_id", string.IsNullOrEmpty(Convert.ToString(purchase_return_id)) ? null : Convert.ToInt64(purchase_return_id));

                    // Execute the function call
                    var result = await connection.QueryFirstOrDefaultAsync<tran_mcd_purchase_return_DTO>(
                      sqlCommand,
                      parameters
                    );

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<List<rpc_rpt_bp_data_month_compare_data_DTO>> GetAllrpt_bp_data_month_compare_dataAsync(Int64? fiscal_year_filter
, Int64? fiscal_year_compare_with_filter
)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM rpt_bp_data_month_compare_data(@fiscal_year_filter,@fiscal_year_compare_with_filter)";

                    var dataList = connection.Query<rpc_rpt_bp_data_month_compare_data_DTO>(query,
                          new { fiscal_year_filter, fiscal_year_compare_with_filter }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_rpt_bp_data_districtwise_DTO>> GetAllrpt_bp_data_districtwiseAsync(Int64? fiscal_year_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM rpt_bp_data_districtwise(@fiscal_year_filter)";

                    var dataList = connection.Query<rpc_rpt_bp_data_districtwise_DTO>(query,
                          new { fiscal_year_filter }
                         ).AsList();

                    foreach (var obj in dataList)
                    {
                        if (!string.IsNullOrEmpty(obj.monthly_outlet_gross))
                            obj.OutletList = JsonConvert.DeserializeObject<List<rpc_rpt_bp_data_districtwise_outlet_DTO>>(obj.monthly_outlet_gross);
                        if (!string.IsNullOrEmpty(obj.all_outlets))
                            obj.AllOutletList = JsonConvert.DeserializeObject<List<rpc_rpt_bp_data_districtwise_outlet_DTO>>(obj.all_outlets);

                    }

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_rpt_bp_data_month_wise_outlet_DTO>> GetAllrpt_bp_data_month_wise_outletAsync(Int64? fiscal_year_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM rpt_bp_data_month_wise_outlet(@fiscal_year_filter)";

                    var dataList = connection.Query<rpc_rpt_bp_data_month_wise_outlet_DTO>(query,
                          new { fiscal_year_filter }
                         ).AsList();

                    foreach (var obj in dataList)
                    {
                        if (!string.IsNullOrEmpty(obj.monthly_outlet_gross))
                            obj.OutletList = JsonConvert.DeserializeObject<List<rpc_rpt_bp_data_month_wise_outlet_det_DTO>>(obj.monthly_outlet_gross);
                        if (!string.IsNullOrEmpty(obj.all_outlets))
                            obj.AllOutletList = JsonConvert.DeserializeObject<List<rpc_rpt_bp_data_month_wise_outlet_det_DTO>>(obj.all_outlets);

                    }

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_sp_get_data_tran_scm_po_DTO> GetAllproc_sp_get_data_tran_scm_poAsync(Int64? po_id_filter
)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    //string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po( @po_id_filter)";
                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_trem_conditions( @po_id_filter)";

                    var dataList = connection.Query<rpc_proc_sp_get_data_tran_scm_po_DTO>(query,
                          new { po_id_filter }
                         ).AsList().FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_sp_get_data_tran_purchase_requisition_details_DTO> GetAllproc_sp_get_data_tran_purchase_requisition_detailsAsync(Int64? pr_id_filter)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_purchase_requisition_details( @pr_id_filter)";

                    var dataList = connection.Query<rpc_proc_sp_get_data_tran_purchase_requisition_details_DTO>(query,
                          new { pr_id_filter }
                         ).AsList().FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_proc_sp_get_data_tran_scm_po_DTO> GetAllproc_sp_get_data_tran_scm_revise_poAsync(Int64? po_id_filter)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    //string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po( @po_id_filter)";
                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_po_revise_detail( @po_id_filter)";

                    var dataList = connection.Query<rpc_proc_sp_get_data_tran_scm_po_DTO>(query,
                          new { po_id_filter }
                         ).AsList().FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<lookup_trading_DTO>> GetAllTradingData(Int64 p_lookup_master_id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_all_trading_data(@p_lookup_master_id)";

                    var dataList = connection.Query<lookup_trading_DTO>(query,
                          new
                          {
                              p_lookup_master_id
                          }
                         ).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

    }

}


