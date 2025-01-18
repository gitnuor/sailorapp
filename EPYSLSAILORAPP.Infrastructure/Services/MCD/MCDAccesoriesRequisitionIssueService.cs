
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using ServiceStack;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class MCdAccesoriesRequisitionIssueService : IMCDAccesoriesRequisitionIssueService
    {

        private readonly IConfiguration _configuration;
      
        private readonly IMapper _mapper;

        public MCdAccesoriesRequisitionIssueService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
         
        }

        public async Task<bool> SaveAsync(tran_mcd_requisition_issue_entity objtran_mcd_requisition_issue, List<tran_mcd_requisition_issue_details_entity> detail)
        {
            try
            {
               

                

                try
                {

                    foreach (tran_mcd_requisition_issue_details_entity item in detail)
                    {
                        item.tran_mcd_requisition_slip_id = objtran_mcd_requisition_issue.requisition_slip_id;
                        item.rejected_quantity = item.requisition_quantity - item.issue_quantity;
                    }

                    objtran_mcd_requisition_issue.mcd_requisition_issue_details = JArray.Parse(JsonConvert.SerializeObject(detail)).ToString();

                    using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                var Command = new NpgsqlCommand("tran_mcd_requisition_issue_insert", connection);

                                Command.CommandType = CommandType.StoredProcedure;

                                Command.Parameters.AddWithValue("in_issue_no", NpgsqlDbType.Text, objtran_mcd_requisition_issue.issue_no == null ? DBNull.Value : objtran_mcd_requisition_issue.issue_no);
                                Command.Parameters.AddWithValue("in_requisition_slip_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.requisition_slip_id == null ? DBNull.Value : objtran_mcd_requisition_issue.requisition_slip_id);
                                Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.techpack_id == null ? DBNull.Value : objtran_mcd_requisition_issue.techpack_id);
                                Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_mcd_requisition_issue.remarks == null ? DBNull.Value : objtran_mcd_requisition_issue.remarks);
                                Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, objtran_mcd_requisition_issue.is_submitted == null ? DBNull.Value : objtran_mcd_requisition_issue.is_submitted);
                                Command.Parameters.AddWithValue("in_is_full_issued", NpgsqlDbType.Boolean, objtran_mcd_requisition_issue.is_full_issued == null ? DBNull.Value : objtran_mcd_requisition_issue.is_full_issued);
                                Command.Parameters.AddWithValue("in_is_closed", NpgsqlDbType.Boolean, objtran_mcd_requisition_issue.is_closed == null ? DBNull.Value : objtran_mcd_requisition_issue.is_closed);
                                Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, objtran_mcd_requisition_issue.is_approved == null ? DBNull.Value : objtran_mcd_requisition_issue.is_approved);
                                Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.approved_by == null ? DBNull.Value : objtran_mcd_requisition_issue.approved_by);
                                Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_mcd_requisition_issue.approve_date == null ? DBNull.Value : objtran_mcd_requisition_issue.approve_date);
                                Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_mcd_requisition_issue.approve_remarks == null ? DBNull.Value : objtran_mcd_requisition_issue.approve_remarks);
                                Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.added_by == null ? DBNull.Value : objtran_mcd_requisition_issue.added_by);
                                Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_mcd_requisition_issue.date_added == null ? DBNull.Value : objtran_mcd_requisition_issue.date_added);
                                Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.updated_by == null ? DBNull.Value : objtran_mcd_requisition_issue.updated_by);
                                Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_mcd_requisition_issue.date_updated == null ? DBNull.Value : objtran_mcd_requisition_issue.date_updated);
                                Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.gen_item_structure_group_id == null ? DBNull.Value : objtran_mcd_requisition_issue.gen_item_structure_group_id);
                                Command.Parameters.AddWithValue("in_gen_item_structure_group_sub_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.gen_item_structure_group_sub_id == null ? DBNull.Value : objtran_mcd_requisition_issue.gen_item_structure_group_sub_id);
                                Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.event_id == null ? DBNull.Value : objtran_mcd_requisition_issue.event_id);
                                Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.fiscal_year_id == null ? DBNull.Value : objtran_mcd_requisition_issue.fiscal_year_id);
                                Command.Parameters.AddWithValue("in_mcd_requisition_issue_details", NpgsqlDbType.Text, objtran_mcd_requisition_issue.mcd_requisition_issue_details == null ? DBNull.Value : objtran_mcd_requisition_issue.mcd_requisition_issue_details);


                                Command.ExecuteNonQuery();

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
                catch (Exception e)
                {

                    
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(tran_mcd_requisition_issue_entity objtran_mcd_requisition_issue, List<tran_mcd_requisition_issue_details_entity> detail)
        {
            try
            {
                foreach (tran_mcd_requisition_issue_details_entity item in detail)
                {

                    item.tran_mcd_requisition_slip_id = objtran_mcd_requisition_issue.requisition_slip_id;
                    item.rejected_quantity = item.requisition_quantity - item.issue_quantity;
                }

                objtran_mcd_requisition_issue.mcd_requisition_issue_details = JArray.Parse(JsonConvert.SerializeObject(detail)).ToString();

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var Command = new NpgsqlCommand("tran_mcd_requisition_issue_update", connection);

                            Command.CommandType = CommandType.StoredProcedure;


                            Command.Parameters.AddWithValue("in_tran_mcd_requisition_issue_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.tran_mcd_requisition_issue_id == null ? DBNull.Value : objtran_mcd_requisition_issue.tran_mcd_requisition_issue_id);
                            Command.Parameters.AddWithValue("in_issue_no", NpgsqlDbType.Text, objtran_mcd_requisition_issue.issue_no == null ? DBNull.Value : objtran_mcd_requisition_issue.issue_no);
                            Command.Parameters.AddWithValue("in_requisition_slip_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.requisition_slip_id == null ? DBNull.Value : objtran_mcd_requisition_issue.requisition_slip_id);
                            Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.techpack_id == null ? DBNull.Value : objtran_mcd_requisition_issue.techpack_id);
                            Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_mcd_requisition_issue.remarks == null ? DBNull.Value : objtran_mcd_requisition_issue.remarks);
                            Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, objtran_mcd_requisition_issue.is_submitted == null ? DBNull.Value : objtran_mcd_requisition_issue.is_submitted);
                            Command.Parameters.AddWithValue("in_is_full_issued", NpgsqlDbType.Boolean, objtran_mcd_requisition_issue.is_full_issued == null ? DBNull.Value : objtran_mcd_requisition_issue.is_full_issued);
                            Command.Parameters.AddWithValue("in_is_closed", NpgsqlDbType.Boolean, objtran_mcd_requisition_issue.is_closed == null ? DBNull.Value : objtran_mcd_requisition_issue.is_closed);
                            Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, objtran_mcd_requisition_issue.is_approved == null ? DBNull.Value : objtran_mcd_requisition_issue.is_approved);
                            Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.approved_by == null ? DBNull.Value : objtran_mcd_requisition_issue.approved_by);
                            Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_mcd_requisition_issue.approve_date == null ? DBNull.Value : objtran_mcd_requisition_issue.approve_date);
                            Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_mcd_requisition_issue.approve_remarks == null ? DBNull.Value : objtran_mcd_requisition_issue.approve_remarks);
                            Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.added_by == null ? DBNull.Value : objtran_mcd_requisition_issue.added_by);
                            Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_mcd_requisition_issue.date_added == null ? DBNull.Value : objtran_mcd_requisition_issue.date_added);
                            Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.updated_by == null ? DBNull.Value : objtran_mcd_requisition_issue.updated_by);
                            Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_mcd_requisition_issue.date_updated == null ? DBNull.Value : objtran_mcd_requisition_issue.date_updated);
                            Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.gen_item_structure_group_id == null ? DBNull.Value : objtran_mcd_requisition_issue.gen_item_structure_group_id);
                            Command.Parameters.AddWithValue("in_gen_item_structure_group_sub_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.gen_item_structure_group_sub_id == null ? DBNull.Value : objtran_mcd_requisition_issue.gen_item_structure_group_sub_id);
                            Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.event_id == null ? DBNull.Value : objtran_mcd_requisition_issue.event_id);
                            Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_issue.fiscal_year_id == null ? DBNull.Value : objtran_mcd_requisition_issue.fiscal_year_id);
                            Command.Parameters.AddWithValue("in_mcd_requisition_issue_details", NpgsqlDbType.Text, objtran_mcd_requisition_issue.mcd_requisition_issue_details == null ? DBNull.Value : objtran_mcd_requisition_issue.mcd_requisition_issue_details);


                            Command.ExecuteNonQuery();

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
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_mcd_requisition_issue_DTO>> GetAllAsync()
        {
           

            List<tran_mcd_requisition_issue_entity> list = new List<tran_mcd_requisition_issue_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_mcd_requisition_issue_entity>().ToList();

                    //return dataList;
                    return JsonConvert.DeserializeObject<List<tran_mcd_requisition_issue_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_mcd_requisition_issue_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_mcd_requisition_issue m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.issue_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_mcd_requisition_issue_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    //return dataList;
                    return JsonConvert.DeserializeObject<List<tran_mcd_requisition_issue_DTO>>(JsonConvert.SerializeObject(dataList));

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        //public async Task<List<tran_mcd_requisition_issue_DTO>> GetAllPagedDataForSelect2(DtParameters param)
        //{
        //    try
        //    {
        //        await _connectionSupabse.InitializeAsync();

        //        var sort_column = ""; var sort_order = "";

        //        if (!string.IsNullOrEmpty(param.Search.Value))
        //        {

        //            var TotalRecord = await _connectionSupabse
        //                   .From<tran_mcd_requisition_issue_entity>()
        //                   .Select(x => new object[] { x.tran_mcd_requisition_issue_id })
        //                   .Filter(p => p.tran_mcd_requisition_issue_id, Operator.ILike, "%" + param.Search.Value + "%")
        //                   .Count(Constants.CountType.Exact);

        //            var response = await _connectionSupabse.From<tran_mcd_requisition_issue_entity>()
        //           .Select("*")
        //           .Filter(p => p.tran_mcd_requisition_issue_id, Operator.ILike, "%" + param.Search.Value + "%")
        //           .Order("tran_mcd_requisition_issue_id", Ordering.Ascending)
        //           .Range(param.Start, param.Start + param.Length - 1)
        //           .Get();

        //            var list = JsonConvert.DeserializeObject<List<tran_mcd_requisition_issue_DTO>>(response.Content);

        //            list.ForEach(p => p.TotalRecord = TotalRecord);

        //            return list;
        //        }
        //        else
        //        {
        //            var TotalRecord = await _connectionSupabse
        //                  .From<tran_mcd_requisition_issue_entity>()
        //                  .Select(x => new object[] { x.tran_mcd_requisition_issue_id })
        //                  .Count(Constants.CountType.Exact);

        //            var response = await _connectionSupabse.From<tran_mcd_requisition_issue_entity>()
        //           .Select("*")
        //           .Order("tran_mcd_requisition_issue_id", Ordering.Ascending)
        //           .Range(param.Start, param.Start + param.Length - 1)
        //           .Get();

        //            var list = JsonConvert.DeserializeObject<List<tran_mcd_requisition_issue_DTO>>(response.Content);

        //            list.ForEach(p => p.TotalRecord = TotalRecord);

        //            return list;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //}




        public async Task<bool> DeleteAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new tran_mcd_requisition_issue_entity { tran_mcd_requisition_issue_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        

        [HttpPost]
        public async Task<List<rpc_tran_mcd_requisition_slip_for_issueLanding_DTO>> GetAllJoined_MCDRequisitionIssueAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id)
        {


            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_mcd_requisition_slip_accesories( @row_index,@page_size,@fiscal_year,@p_event_id,@p_group_id,@p_sub_group_id)";

                    var dataList = connection.Query<rpc_tran_mcd_requisition_slip_for_issueLanding_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_group_id = p_group_id,
                              p_sub_group_id = p_sub_group_id
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
        [HttpPost]
        public async Task<List<rpc_tran_mcd_requisition_issue_DTO>> GetDraftRequisitionSlipData(long row_index, long page_size, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id)
        {



            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_mcd_requisition_issue_accesories( @row_index,@page_size,@fiscal_year,@p_event_id,@p_group_id,@p_sub_group_id)";

                    var dataList = connection.Query<rpc_tran_mcd_requisition_issue_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_group_id = p_group_id,
                              p_sub_group_id = p_sub_group_id
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

        [HttpPost]
        public async Task<List<rpc_tran_mcd_requisition_issue_DTO>> GetSubmittedRequisitionSlipData(long row_index, long page_size, long fiscal_year_id, long event_id, long group_id, long sub_group_id)
        {

            try
            {



                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_submitteddata_mcd_requisition_accesories( @row_index,@page_size,@fiscal_year,@p_event_id,@p_group_id,@p_sub_group_id)";

                    var dataList = connection.Query<rpc_tran_mcd_requisition_issue_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_group_id = group_id,
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

        [HttpPost]
        public async Task<List<rpc_tran_mcd_requisition_issue_DTO>> GetAprrovedRequisitionSlipData(long row_index, long page_size, long fiscal_year_id, long event_id, long group_id, long sub_group_id)
        {



            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_approvaldata_mcd_requisition_accesories( @row_index,@page_size,@fiscal_year,@p_event_id,@p_group_id,@p_sub_group_id)";

                    var dataList = connection.Query<rpc_tran_mcd_requisition_issue_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_group_id = group_id,
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
        public async Task<rpc_proc_sp_get_mcd_requisition_issue_DTO> Get_mcd_requisition_issueAsync(long? p_issue_id)
        {



            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_mcd_requisition_issue( @p_issue_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_mcd_requisition_issue_DTO>(query,
                          new
                          {
                              p_issue_id = p_issue_id

                          }
                         ).SingleOrDefault();

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

