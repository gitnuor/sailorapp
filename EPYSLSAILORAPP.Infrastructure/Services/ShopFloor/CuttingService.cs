
using AutoMapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Drawing.Printing;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranCuttingService : ICuttingService
    {

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public TranCuttingService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;

        }


        public async Task<bool> SaveAsync(tran_cutting_insert_DTO objtran_cutting)
        {

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var cmnd = new NpgsqlCommand("tran_cutting_insert", connection);
                        cmnd.CommandType = CommandType.StoredProcedure;

                        cmnd.Parameters.AddWithValue("in_techpack_id", objtran_cutting.techpack_id);
                        cmnd.Parameters.AddWithValue("in_tran_pp_meeting_id", objtran_cutting.tran_pp_meeting_id);
                        cmnd.Parameters.AddWithValue("in_fiscal_year_id", objtran_cutting.fiscal_year_id);
                        cmnd.Parameters.AddWithValue("in_event_id", objtran_cutting.event_id);
                        cmnd.Parameters.AddWithValue("in_added_by", objtran_cutting.added_by);
                        cmnd.Parameters.AddWithValue("in_total_cut", objtran_cutting.cutting_Color.total_cut);
                        cmnd.Parameters.AddWithValue("in_total_quantity", objtran_cutting.cutting_Color.total_quantity);
                        cmnd.Parameters.AddWithValue("in_date_added", objtran_cutting.date_added);

                        cmnd.Parameters.AddWithValue("in_cutting_date_start", objtran_cutting.batch_info.cutting_date_start);
                        cmnd.Parameters.AddWithValue("in_cutting_date_end", objtran_cutting.batch_info.cutting_date_end);
                        cmnd.Parameters.AddWithValue("in_batch_no", objtran_cutting.batch_info.batch_no.ToString());
                        cmnd.Parameters.AddWithValue("in_uom_id", objtran_cutting.batch_info.measurement_unit_detail_id);
                        cmnd.Parameters.AddWithValue("in_uom", objtran_cutting.batch_info.measurement_unit);
                        cmnd.Parameters.AddWithValue("in_total_booking_quantity", objtran_cutting.batch_info.total_booking_quantity);
                        cmnd.Parameters.AddWithValue("in_total_receive_quantity", objtran_cutting.batch_info.total_receive_quantity);
                        cmnd.Parameters.AddWithValue("in_total_input_quantity", objtran_cutting.batch_info.total_input_quantity);
                        cmnd.Parameters.AddWithValue("in_total_no_of_lay", objtran_cutting.batch_info.total_no_of_lay);
                        cmnd.Parameters.AddWithValue("in_is_contrast", objtran_cutting.batch_info.is_contrast);
                        cmnd.Parameters.AddWithValue("in_is_hand_cut", objtran_cutting.batch_info.is_hand_cut);
                        cmnd.Parameters.AddWithValue("in_pattern_type", objtran_cutting.batch_info.pattern_type);
                        cmnd.Parameters.AddWithValue("in_marker_type", objtran_cutting.batch_info.marker_type);

                        cmnd.Parameters.AddWithValue("in_style_code", objtran_cutting.style_code);
                        cmnd.Parameters.AddWithValue("in_all_processes", objtran_cutting.all_processes);
                        cmnd.Parameters.AddWithValue("in_remarks", objtran_cutting.remarks);
                        cmnd.Parameters.AddWithValue("in_color_code", objtran_cutting.cutting_Color.color_code);
                        cmnd.Parameters.AddWithValue("in_allocated_unit", objtran_cutting.cutting_Color.allocated_unit);
                        cmnd.Parameters.AddWithValue("in_allocated_unit_id", objtran_cutting.cutting_Color.allocated_unit_id);

                        cmnd.Parameters.AddWithValue("in_tran_cutting_color_batch_fabric", objtran_cutting.FabricJarry.ToString());
                        cmnd.Parameters.AddWithValue("in_bundle_json", objtran_cutting.bundlesJarry.ToString());
                        cmnd.Parameters.AddWithValue("in_garment_part_json", objtran_cutting.garment_partsJarry.ToString());
                        cmnd.Parameters.AddWithValue("in_garment_part_details_json", objtran_cutting.garment_part_detailsJarry.ToString());
                        cmnd.Parameters.AddWithValue("in_tran_cutting_color_batch_size_summery_json", objtran_cutting.tran_cutting_color_batch_size_summeryJarry.ToString());

                        cmnd.Parameters[27].NpgsqlDbType = NpgsqlDbType.Text;
                        cmnd.Parameters[28].NpgsqlDbType = NpgsqlDbType.Text;
                        cmnd.Parameters[29].NpgsqlDbType = NpgsqlDbType.Text;
                        cmnd.Parameters[30].NpgsqlDbType = NpgsqlDbType.Text;
                        cmnd.Parameters[31].NpgsqlDbType = NpgsqlDbType.Text;

                        cmnd.ExecuteNonQuery();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }



        public async Task<bool> UpdateAsync(tran_cutting_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_cutting_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    connection.Update(objEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_cutting_DTO>> GetAllAsync()
        {
            try
            {

                List<tran_cutting_DTO> list = new List<tran_cutting_DTO>();

                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        var dataList = connection.GetAll<tran_cutting_DTO>().ToList();

                        return dataList;

                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_cutting_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_cutting m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.style_code ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_cutting_DTO>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_cutting_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_cutting_by_id(@tran_cutting_id_filter)";

                    var objData = connection.Query<tran_cutting_DTO>(query,
                          new
                          {
                              tran_cutting_id_filter = Id
                          }
                         ).AsList().FirstOrDefault();

                    objData.cutting_color_List = JsonConvert.DeserializeObject<List<tran_cutting_color_DTO>>(objData.tran_cutting_color_json);

                    foreach (var objCuttingColor in objData.cutting_color_List)
                    {
                        if (!string.IsNullOrEmpty(objCuttingColor.tran_cutting_color_batch_json))
                        {
                            objCuttingColor.batches = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_DTO>>(objCuttingColor.tran_cutting_color_batch_json);

                            foreach (var objColorBatch in objCuttingColor.batches)
                            {
                                if (!string.IsNullOrEmpty(objColorBatch.tran_cutting_color_batch_fabric_details_json))
                                    objColorBatch.fabric = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_fabric_details_DTO>>(objColorBatch.tran_cutting_color_batch_fabric_details_json);
                                
                                if (!string.IsNullOrEmpty(objColorBatch.batch_garment_part_bundle_json))
                                    objColorBatch.bundle_list = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_garment_part_bundle_DTO>>(objColorBatch.batch_garment_part_bundle_json);

                                if (!string.IsNullOrEmpty(objColorBatch.tran_cutting_color_batch_size_summery))
                                {
                                    objColorBatch.color_batch_size_summery_List = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_size_summery>>(objColorBatch.tran_cutting_color_batch_size_summery);
                                }

                                if (!string.IsNullOrEmpty(objColorBatch.tran_cutting_color_batch_garment_part))
                                {
                                    objColorBatch.garment_part = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_garment_part_DTO>>(objColorBatch.tran_cutting_color_batch_garment_part);

                                    foreach (var obj_garment_part in objColorBatch.garment_part)
                                    {
                                        if (!string.IsNullOrEmpty(obj_garment_part.tran_cutting_color_batch_garment_part_details_json))
                                            obj_garment_part.garment_part_details = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_garment_part_details_DTO>>(obj_garment_part.tran_cutting_color_batch_garment_part_details_json);

                                    }
                                }

                            }
                        }
                    }

                    return objData;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<tran_cutting_DTO> GetCuttingDetailsWithColor(Int64 Id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_cutting_by_id(@tran_cutting_id_filter)";

                    var objData = connection.Query<tran_cutting_DTO>(query,
                          new
                          {
                              tran_cutting_id_filter = Id
                          }
                         ).AsList().FirstOrDefault();

                    objData.cutting_color_List = JsonConvert.DeserializeObject<List<tran_cutting_color_DTO>>(objData.tran_cutting_color_json);

                    foreach (var objCuttingColor in objData.cutting_color_List)
                    {
                        if (!string.IsNullOrEmpty(objCuttingColor.tran_cutting_color_batch_json))
                        {
                            objCuttingColor.batches = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_DTO>>(objCuttingColor.tran_cutting_color_batch_json);

                            foreach (var objColorBatch in objCuttingColor.batches)
                            {
                                if (!string.IsNullOrEmpty(objColorBatch.tran_cutting_color_batch_fabric_details_json))
                                    objColorBatch.fabric = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_fabric_details_DTO>>(objColorBatch.tran_cutting_color_batch_fabric_details_json);

                                if (!string.IsNullOrEmpty(objColorBatch.batch_garment_part_bundle_json))
                                    objColorBatch.bundle_list = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_garment_part_bundle_DTO>>(objColorBatch.batch_garment_part_bundle_json);

                                if (!string.IsNullOrEmpty(objColorBatch.tran_cutting_color_batch_size_summery))
                                {
                                    objColorBatch.color_batch_size_summery_List = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_size_summery>>(objColorBatch.tran_cutting_color_batch_size_summery);
                                }

                                if (!string.IsNullOrEmpty(objColorBatch.tran_cutting_color_batch_garment_part))
                                {
                                    objColorBatch.garment_part = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_garment_part_DTO>>(objColorBatch.tran_cutting_color_batch_garment_part);

                                    foreach (var obj_garment_part in objColorBatch.garment_part)
                                    {
                                        if (!string.IsNullOrEmpty(obj_garment_part.tran_cutting_color_batch_garment_part_details_json))
                                            obj_garment_part.garment_part_details = JsonConvert.DeserializeObject<List<tran_cutting_color_batch_garment_part_details_DTO>>(obj_garment_part.tran_cutting_color_batch_garment_part_details_json);

                                    }
                                }

                            }
                        }
                    }

                    return objData;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<bool> DeleteAsync(Int64 Id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new tran_cutting_entity { tran_cutting_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<List<rpc_tran_cutting_DTO>> GetAllJoined_CuttingAsync(long row_index, long page_size)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_cutting(@row_index,@page_size)";

                    var dataList = connection.Query<rpc_tran_cutting_DTO>(query,
                          new
                          {

                              row_index = row_index,
                              page_size = page_size,

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
        public async Task<List<rpc_tran_pp_meeting_DTO>> GetPendingCuttingPlans(long row_index, long page_size, long event_id, long fiscal_year_id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_pending_cutting_plan(@row_index,@page_size, @p_fiscal_year_id,@p_event_id)";

                    var dataList = connection.Query<rpc_tran_pp_meeting_DTO>(query,
                          new
                          {

                              row_index = row_index,
                              page_size = page_size,
                              p_fiscal_year_id = fiscal_year_id,
                              p_event_id = event_id
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
        public async Task<List<rpc_tran_pp_meeting_DTO>> GetRunningCuttingPlans(long row_index, long page_size, long event_id, long fiscal_year_id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_running_cutting_plan(@row_index,@page_size, @p_fiscal_year_id,@p_event_id)";

                    var dataList = connection.Query<rpc_tran_pp_meeting_DTO>(query,
                          new
                          {

                              row_index = row_index,
                              page_size = page_size,
                              p_fiscal_year_id = fiscal_year_id,
                              p_event_id = event_id
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
        public async Task<List<rpc_tran_pp_meeting_DTO>> GetCompletedCuttingPlans(long row_index, long page_size, long event_id, long fiscal_year_id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_completed_cutting_plan(@row_index,@page_size, @p_fiscal_year_id,@p_event_id)";

                    var dataList = connection.Query<rpc_tran_pp_meeting_DTO>(query,
                          new
                          {

                              row_index = row_index,
                              page_size = page_size,
                              p_fiscal_year_id = fiscal_year_id,
                              p_event_id = event_id
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
        public async Task<tran_cutting_DTO> GetTechPackInfoForCutting(long Id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_techpack_info_for_cutting (@p_techpack_id)";

                    var dataList = connection.Query<tran_cutting_DTO>(query,
                          new { p_techpack_id = Id }
                         ).AsList().FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<cutting_details_DTO> GetCuttingDetails(long Id, string color_code)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_garment_part_by_techpack( @p_techpack_id,@p_color_code)";

                    var dataList = connection.Query<cutting_details_DTO>(query,
                          new
                          {
                              p_techpack_id = Id,
                              p_color_code = color_code
                          }
                         ).AsList().FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<fabric_details_DTO>> GetFabricDetaiils(long Id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_fabric_details_for_batch_addition( @p_techpack_id)";

                    var dataList = connection.Query<fabric_details_DTO>(query,
                          new { p_techpack_id = Id }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        public async Task<List<tran_cutting_batch_dropdown>> GetAllproc_sp_get_color_wise_batchAsync(Int64? p_techpack_id, String p_color_code)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_color_wise_batch( @p_techpack_id,@p_color_code)";

                    var dataList = connection.Query<tran_cutting_batch_dropdown>(query,
                          new { p_techpack_id, p_color_code }
                         ).AsList();

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

