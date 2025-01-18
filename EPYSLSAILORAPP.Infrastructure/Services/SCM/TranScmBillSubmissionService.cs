using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Enum;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranScmBillSubmissionService : ITranScmBillSubmissionService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranScmBillSubmissionService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_scm_bill_submission_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_scm_bill_submission_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    connection.Insert(objEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(tran_scm_bill_submission_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_scm_bill_submission_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_scm_bill_submission_DTO>> GetAllAsync()
        {
            List<tran_scm_bill_submission_DTO> list = new List<tran_scm_bill_submission_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_scm_bill_submission_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<tran_scm_bill_submission_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_scm_bill_submission_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_scm_bill_submission m
                                           where case
                                                     when @search_text is null then true
                                                     when @search_text is not null and (
                                                            m.bill_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_scm_bill_submission_entity>(query,
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



        public async Task<List<tran_scm_bill_submission_DTO>> GetAll_Pending_Bill_Submission_Async(Int64 fiscal_year_id, Int64 event_id, Int64 row_index, Int64 page_size, string search)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_pending_bill_submission(@p_fiscal_year_id,@p_event_id, @row_index,@page_size,@search_text)";

                    var dataList = connection.Query<tran_scm_bill_submission_DTO>(query,
                          new
                          {
                              p_fiscal_year_id = fiscal_year_id,
                              p_event_id = event_id,
                              row_index = row_index,
                              page_size = page_size,
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


        public async Task<List<tran_scm_bill_submission_DTO>> GetAll_Bill_Submission_Async(Int64 fiscal_year_id, Int64 event_id, Int64 action_type, Int64 row_index, Int64 page_size, string search)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_bill_submission(@p_fiscal_year_id,@p_event_id,@action_type, @row_index,@page_size,@search_text)";

                    var dataList = connection.Query<tran_scm_bill_submission_DTO>(query,
                          new
                          {
                              p_fiscal_year_id = fiscal_year_id,
                              p_event_id = event_id,
                              action_type=action_type,
                              row_index = row_index,
                              page_size = page_size,
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


        public async Task<List<tran_scm_bill_submission_entity>> GetAll_Bill_Submission_SubmittedPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_scm_bill_submission m
                                           where
                                               m.is_approved is null

                                               AND
                                               case
                                                     when @search_text is null then true
                                                     when @search_text is not null and (
                                                            m.bill_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_scm_bill_submission_entity>(query,
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




        public async Task<List<tran_scm_bill_submission_entity>> Get_Bill_Approval_Pending_AllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_scm_bill_submission m
                                           where

                                               m.is_send_for_approval=false and
                                              
                                               m.is_submitted=true

                                               AND
                                               case
                                                     when @search_text is null then true
                                                     when @search_text is not null and (
                                                            m.bill_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_scm_bill_submission_entity>(query,
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




        public async Task<tran_scm_bill_submission_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_scm_bill_submission m   where m.bill_submission_id=@Id";

                    var dataList = connection.Query<tran_scm_bill_submission_entity>(query,
                        new { Id = Id }).ToList().FirstOrDefault();

                    return JsonConvert.DeserializeObject<tran_scm_bill_submission_DTO>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> DeleteAsync(Int64? Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new tran_scm_bill_submission_entity { bill_submission_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_scm_bill_submission_insert_sp(tran_scm_bill_submission_DTO objtran_scm_bill_submission)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_scm_bill_submission_insert_with_po_update", connection);
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.po_id ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.supplier_id ?? (object)DBNull.Value);

                        Command.Parameters.AddWithValue("in_challan_date", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.challan_date ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_bill_date", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.bill_date ?? (object)DBNull.Value);

                        Command.Parameters.AddWithValue("in_total_po_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.total_po_amount ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_loading_cost_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.loading_cost_in_percentage ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_loading_cost", NpgsqlDbType.Numeric, objtran_scm_bill_submission.loading_cost ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_transport_cost_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.transport_cost_in_percentage ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_transport_cost", NpgsqlDbType.Numeric, objtran_scm_bill_submission.transport_cost ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_discount_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.discount_in_percentage ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_discount_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.discount_amount ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_vat_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.vat_in_percentage ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_vat_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.vat_amount ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_total_value", NpgsqlDbType.Numeric, objtran_scm_bill_submission.total_value ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_submitted ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.submitted_date ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.submitted_by ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_scm_bill_submission.is_approved ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.approved_date ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.approved_by ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_status", NpgsqlDbType.Integer, objtran_scm_bill_submission.status ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.added_by ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.updated_by ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.date_added ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.date_updated ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_is_send_for_approval", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_send_for_approval ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_bill_no", NpgsqlDbType.Text, objtran_scm_bill_submission.bill_no ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_challan_no", NpgsqlDbType.Text, objtran_scm_bill_submission.challan_no ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_documents", NpgsqlDbType.Text, objtran_scm_bill_submission.documents ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_terms_conditions", NpgsqlDbType.Text, objtran_scm_bill_submission.terms_conditions ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_scm_bill_submission.remarks ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_fiscal_yera_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.fiscal_year_id ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.event_id ?? (object)DBNull.Value);

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


        //public async Task<bool> tran_scm_bill_submission_insert_sp(tran_scm_bill_submission_DTO objtran_scm_bill_submission)
        //{
        //    using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
        //    {
        //        connection.Open();

        //        using (var transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                var Command = new NpgsqlCommand("tran_scm_bill_submission_insert", connection);

        //                Command.CommandType = CommandType.StoredProcedure;

        //                Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.po_id == null ? DBNull.Value : objtran_scm_bill_submission.po_id);
        //                Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.supplier_id == null ? DBNull.Value : objtran_scm_bill_submission.supplier_id);

        //                Command.Parameters.AddWithValue("in_challan_date", NpgsqlDbType.Date, objtran_scm_bill_submission.challan_date == null ? DBNull.Value : objtran_scm_bill_submission.challan_date);
        //                Command.Parameters.AddWithValue("in_bill_date", NpgsqlDbType.Date, objtran_scm_bill_submission.bill_date == null ? DBNull.Value : objtran_scm_bill_submission.bill_date);

        //                Command.Parameters.AddWithValue("in_total_po_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.total_po_amount == null ? DBNull.Value : objtran_scm_bill_submission.total_po_amount);

        //                Command.Parameters.AddWithValue("in_loading_cost_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.loading_cost_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.loading_cost_in_percentage);

        //                Command.Parameters.AddWithValue("in_loading_cost", NpgsqlDbType.Numeric, objtran_scm_bill_submission.loading_cost == null ? DBNull.Value : objtran_scm_bill_submission.loading_cost);

        //                Command.Parameters.AddWithValue("in_transport_cost_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.transport_cost_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.transport_cost_in_percentage);

        //                Command.Parameters.AddWithValue("in_transport_cost", NpgsqlDbType.Numeric, objtran_scm_bill_submission.transport_cost == null ? DBNull.Value : objtran_scm_bill_submission.transport_cost);

        //                Command.Parameters.AddWithValue("in_discount_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.discount_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.discount_in_percentage);

        //                Command.Parameters.AddWithValue("in_discount_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.discount_amount == null ? DBNull.Value : objtran_scm_bill_submission.discount_amount);

        //                Command.Parameters.AddWithValue("in_vat_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.vat_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.vat_in_percentage);

        //                Command.Parameters.AddWithValue("in_vat_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.vat_amount == null ? DBNull.Value : objtran_scm_bill_submission.vat_amount);

        //                Command.Parameters.AddWithValue("in_total_value", NpgsqlDbType.Numeric, objtran_scm_bill_submission.total_value == null ? DBNull.Value : objtran_scm_bill_submission.total_value);

        //                Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_submitted == null ? DBNull.Value : objtran_scm_bill_submission.is_submitted);

        //                Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Date, objtran_scm_bill_submission.submitted_date == null ? DBNull.Value : objtran_scm_bill_submission.submitted_date);

        //                Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.submitted_by == null ? DBNull.Value : objtran_scm_bill_submission.submitted_by);

        //                Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_approved == null ? DBNull.Value : objtran_scm_bill_submission.is_approved);

        //                Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Date, objtran_scm_bill_submission.approved_date == null ? DBNull.Value : objtran_scm_bill_submission.approved_date);

        //                Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.approved_by == null ? DBNull.Value : objtran_scm_bill_submission.approved_by);

        //                Command.Parameters.AddWithValue("in_status", NpgsqlDbType.Bigint, objtran_scm_bill_submission.status == null ? DBNull.Value : objtran_scm_bill_submission.status);

        //                Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.added_by == null ? DBNull.Value : objtran_scm_bill_submission.added_by);

        //                Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.updated_by == null ? DBNull.Value : objtran_scm_bill_submission.updated_by);

        //                Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_scm_bill_submission.date_added == null ? DBNull.Value : objtran_scm_bill_submission.date_added);

        //                Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_scm_bill_submission.date_updated == null ? DBNull.Value : objtran_scm_bill_submission.date_updated);

        //                Command.Parameters.AddWithValue("in_is_send_for_approval", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_send_for_approval == null ? DBNull.Value : objtran_scm_bill_submission.is_send_for_approval);

        //                Command.Parameters.AddWithValue("in_bill_no", NpgsqlDbType.Text, objtran_scm_bill_submission.bill_no == null ? DBNull.Value : objtran_scm_bill_submission.bill_no);
        //                Command.Parameters.AddWithValue("in_challan_no", NpgsqlDbType.Text, objtran_scm_bill_submission.challan_no == null ? DBNull.Value : objtran_scm_bill_submission.challan_no);


        //                Command.Parameters.AddWithValue("in_documents", NpgsqlDbType.Text, objtran_scm_bill_submission.documents == null ? DBNull.Value : objtran_scm_bill_submission.documents);
        //                Command.Parameters.AddWithValue("in_terms_conditions", NpgsqlDbType.Text, objtran_scm_bill_submission.terms_conditions == null ? DBNull.Value : objtran_scm_bill_submission.terms_conditions);
        //                Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_scm_bill_submission.remarks == null ? DBNull.Value : objtran_scm_bill_submission.remarks);







        //                Command.ExecuteNonQuery();

        //                transaction.Commit();

        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Error: {ex.Message}");
        //                transaction.Rollback();
        //                return false;
        //            }
        //        }
        //    }
        //}



        public async Task<bool> tran_scm_bill_submission_insert_sp_and_po_id_update(tran_scm_bill_submission_DTO objtran_scm_bill_submission)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_scm_bill_submission_insert_and_po_id_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.po_id == null ? DBNull.Value : objtran_scm_bill_submission.po_id);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.supplier_id == null ? DBNull.Value : objtran_scm_bill_submission.supplier_id);

                        Command.Parameters.AddWithValue("in_challan_date", NpgsqlDbType.Date, objtran_scm_bill_submission.challan_date == null ? DBNull.Value : objtran_scm_bill_submission.challan_date);

                        Command.Parameters.AddWithValue("in_bill_no", NpgsqlDbType.Text, objtran_scm_bill_submission.bill_no == null ? DBNull.Value : objtran_scm_bill_submission.bill_no);
                        Command.Parameters.AddWithValue("in_challan_no", NpgsqlDbType.Text, objtran_scm_bill_submission.challan_no == null ? DBNull.Value : objtran_scm_bill_submission.challan_no);
                        
                        Command.Parameters.AddWithValue("in_bill_date", NpgsqlDbType.Date, objtran_scm_bill_submission.bill_date == null ? DBNull.Value : objtran_scm_bill_submission.bill_date);
                        Command.Parameters.AddWithValue("in_total_po_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.total_po_amount == null ? DBNull.Value : objtran_scm_bill_submission.total_po_amount);
                        Command.Parameters.AddWithValue("in_loading_cost_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.loading_cost_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.loading_cost_in_percentage);
                        Command.Parameters.AddWithValue("in_loading_cost", NpgsqlDbType.Numeric, objtran_scm_bill_submission.loading_cost == null ? DBNull.Value : objtran_scm_bill_submission.loading_cost);
                        Command.Parameters.AddWithValue("in_transport_cost_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.transport_cost_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.transport_cost_in_percentage);
                        Command.Parameters.AddWithValue("in_transport_cost", NpgsqlDbType.Numeric, objtran_scm_bill_submission.transport_cost == null ? DBNull.Value : objtran_scm_bill_submission.transport_cost);
                        Command.Parameters.AddWithValue("in_discount_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.discount_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.discount_in_percentage);
                        Command.Parameters.AddWithValue("in_discount_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.discount_amount == null ? DBNull.Value : objtran_scm_bill_submission.discount_amount);
                        Command.Parameters.AddWithValue("in_vat_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.vat_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.vat_in_percentage);
                        Command.Parameters.AddWithValue("in_vat_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.vat_amount == null ? DBNull.Value : objtran_scm_bill_submission.vat_amount);
                        Command.Parameters.AddWithValue("in_total_value", NpgsqlDbType.Numeric, objtran_scm_bill_submission.total_value == null ? DBNull.Value : objtran_scm_bill_submission.total_value);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_submitted == null ? DBNull.Value : objtran_scm_bill_submission.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Date, objtran_scm_bill_submission.submitted_date == null ? DBNull.Value : objtran_scm_bill_submission.submitted_date);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.submitted_by == null ? DBNull.Value : objtran_scm_bill_submission.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_approved == null ? DBNull.Value : objtran_scm_bill_submission.is_approved);
                        Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Date, objtran_scm_bill_submission.approved_date == null ? DBNull.Value : objtran_scm_bill_submission.approved_date);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.approved_by == null ? DBNull.Value : objtran_scm_bill_submission.approved_by);
                        Command.Parameters.AddWithValue("in_status", NpgsqlDbType.Bigint, objtran_scm_bill_submission.status == null ? DBNull.Value : objtran_scm_bill_submission.status);
                        Command.Parameters.AddWithValue("in_documents", NpgsqlDbType.Text, objtran_scm_bill_submission.List_Files == null ? DBNull.Value : JsonConvert.SerializeObject(objtran_scm_bill_submission.List_Files));
                        Command.Parameters.AddWithValue("in_terms_conditions", NpgsqlDbType.Text, objtran_scm_bill_submission.terms_conditions == null ? DBNull.Value : objtran_scm_bill_submission.terms_conditions);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_scm_bill_submission.remarks == null ? DBNull.Value : objtran_scm_bill_submission.remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.added_by == null ? DBNull.Value : objtran_scm_bill_submission.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.updated_by == null ? DBNull.Value : objtran_scm_bill_submission.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_scm_bill_submission.date_added == null ? DBNull.Value : objtran_scm_bill_submission.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_scm_bill_submission.date_updated == null ? DBNull.Value : objtran_scm_bill_submission.date_updated);
                        Command.Parameters.AddWithValue("in_is_send_for_approval", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_send_for_approval == null ? DBNull.Value : objtran_scm_bill_submission.is_send_for_approval);


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




        //public async Task<bool> tran_scm_bill_submission_update_sp(tran_scm_bill_submission_DTO objtran_scm_bill_submission)
        //{
        //    using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
        //    {
        //        connection.Open();
        //        using (var transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                var Command = new NpgsqlCommand("tran_scm_bill_submission_update", connection);

        //                Command.CommandType = CommandType.StoredProcedure;

        //                Command.Parameters.AddWithValue("in_bill_submission_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.bill_submission_id == null ? DBNull.Value : objtran_scm_bill_submission.bill_submission_id);

        //                Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.po_id == null ? DBNull.Value : objtran_scm_bill_submission.po_id);
        //                Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.supplier_id == null ? DBNull.Value : objtran_scm_bill_submission.supplier_id);
        //                Command.Parameters.AddWithValue("in_bill_no", NpgsqlDbType.Text, objtran_scm_bill_submission.bill_no == null ? DBNull.Value : objtran_scm_bill_submission.bill_no);
        //                Command.Parameters.AddWithValue("in_challan_no", NpgsqlDbType.Text, objtran_scm_bill_submission.challan_no == null ? DBNull.Value : objtran_scm_bill_submission.challan_no);
        //                Command.Parameters.AddWithValue("in_challan_date", NpgsqlDbType.Date, objtran_scm_bill_submission.challan_date == null ? DBNull.Value : objtran_scm_bill_submission.challan_date);
        //                Command.Parameters.AddWithValue("in_bill_date", NpgsqlDbType.Date, objtran_scm_bill_submission.bill_date == null ? DBNull.Value : objtran_scm_bill_submission.bill_date);
        //                Command.Parameters.AddWithValue("in_total_po_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.total_po_amount == null ? DBNull.Value : objtran_scm_bill_submission.total_po_amount);
        //                Command.Parameters.AddWithValue("in_loading_cost_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.loading_cost_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.loading_cost_in_percentage);
        //                Command.Parameters.AddWithValue("in_loading_cost", NpgsqlDbType.Numeric, objtran_scm_bill_submission.loading_cost == null ? DBNull.Value : objtran_scm_bill_submission.loading_cost);
        //                Command.Parameters.AddWithValue("in_transport_cost_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.transport_cost_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.transport_cost_in_percentage);
        //                Command.Parameters.AddWithValue("in_transport_cost", NpgsqlDbType.Numeric, objtran_scm_bill_submission.transport_cost == null ? DBNull.Value : objtran_scm_bill_submission.transport_cost);
        //                Command.Parameters.AddWithValue("in_discount_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.discount_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.discount_in_percentage);
        //                Command.Parameters.AddWithValue("in_discount_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.discount_amount == null ? DBNull.Value : objtran_scm_bill_submission.discount_amount);
        //                Command.Parameters.AddWithValue("in_vat_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.vat_in_percentage == null ? DBNull.Value : objtran_scm_bill_submission.vat_in_percentage);
        //                Command.Parameters.AddWithValue("in_vat_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.vat_amount == null ? DBNull.Value : objtran_scm_bill_submission.vat_amount);
        //                Command.Parameters.AddWithValue("in_total_value", NpgsqlDbType.Numeric, objtran_scm_bill_submission.total_value == null ? DBNull.Value : objtran_scm_bill_submission.total_value);
        //                Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_submitted == null ? DBNull.Value : objtran_scm_bill_submission.is_submitted);
        //                Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Date, objtran_scm_bill_submission.submitted_date == null ? DBNull.Value : objtran_scm_bill_submission.submitted_date);
        //                Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.submitted_by == null ? DBNull.Value : objtran_scm_bill_submission.submitted_by);
        //                Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_approved == null ? DBNull.Value : objtran_scm_bill_submission.is_approved);
        //                Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Date, objtran_scm_bill_submission.approved_date == null ? DBNull.Value : objtran_scm_bill_submission.approved_date);
        //                Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.approved_by == null ? DBNull.Value : objtran_scm_bill_submission.approved_by);
        //                Command.Parameters.AddWithValue("in_status", NpgsqlDbType.Bigint, objtran_scm_bill_submission.status == null ? DBNull.Value : objtran_scm_bill_submission.status);
        //                Command.Parameters.AddWithValue("in_documents", NpgsqlDbType.Text, objtran_scm_bill_submission.List_Files == null ? DBNull.Value : JsonConvert.SerializeObject(objtran_scm_bill_submission.List_Files));
        //                Command.Parameters.AddWithValue("in_terms_conditions", NpgsqlDbType.Text, objtran_scm_bill_submission.terms_conditions == null ? DBNull.Value : objtran_scm_bill_submission.terms_conditions);
        //                Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_scm_bill_submission.remarks == null ? DBNull.Value : objtran_scm_bill_submission.remarks);
        //                Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.added_by == null ? DBNull.Value : objtran_scm_bill_submission.added_by);
        //                Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.updated_by == null ? DBNull.Value : objtran_scm_bill_submission.updated_by);
        //                Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_scm_bill_submission.date_added == null ? DBNull.Value : objtran_scm_bill_submission.date_added);
        //                Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_scm_bill_submission.date_updated == null ? DBNull.Value : objtran_scm_bill_submission.date_updated);
        //                Command.Parameters.AddWithValue("in_is_send_for_approval", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_send_for_approval == null ? DBNull.Value : objtran_scm_bill_submission.is_send_for_approval);


        //                Command.ExecuteNonQuery();

        //                transaction.Commit();

        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Error: {ex.Message}");
        //                transaction.Rollback();
        //                return false;
        //            }
        //        }
        //    }
        //}

        public async Task<bool> tran_scm_bill_submission_update_sp(tran_scm_bill_submission_DTO objtran_scm_bill_submission)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_scm_bill_submission_update", connection);
                        Command.CommandType = CommandType.StoredProcedure;

                        // Matching the exact parameter types as in the stored procedure
                        Command.Parameters.AddWithValue("in_bill_submission_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.bill_submission_id ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_po_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.po_id ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_scm_bill_submission.supplier_id ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_challan_date", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.challan_date ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_bill_date", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.bill_date ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_total_po_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.total_po_amount ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_loading_cost_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.loading_cost_in_percentage ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_loading_cost", NpgsqlDbType.Numeric, objtran_scm_bill_submission.loading_cost ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_transport_cost_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.transport_cost_in_percentage ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_transport_cost", NpgsqlDbType.Numeric, objtran_scm_bill_submission.transport_cost ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_discount_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.discount_in_percentage ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_discount_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.discount_amount ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_vat_in_percentage", NpgsqlDbType.Numeric, objtran_scm_bill_submission.vat_in_percentage ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_vat_amount", NpgsqlDbType.Numeric, objtran_scm_bill_submission.vat_amount ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_total_value", NpgsqlDbType.Numeric, objtran_scm_bill_submission.total_value ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_submitted ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.submitted_date ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.submitted_by ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_scm_bill_submission.is_approved ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.approved_date ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.approved_by ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_status", NpgsqlDbType.Integer, objtran_scm_bill_submission.status ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_documents", NpgsqlDbType.Text, objtran_scm_bill_submission.List_Files == null ? DBNull.Value : JsonConvert.SerializeObject(objtran_scm_bill_submission.List_Files));
                        Command.Parameters.AddWithValue("in_terms_conditions", NpgsqlDbType.Text, objtran_scm_bill_submission.terms_conditions ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_scm_bill_submission.remarks ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.added_by ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_scm_bill_submission.updated_by ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.date_added ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Timestamp, objtran_scm_bill_submission.date_updated ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_is_send_for_approval", NpgsqlDbType.Boolean, objtran_scm_bill_submission.is_send_for_approval ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_bill_no", NpgsqlDbType.Text, objtran_scm_bill_submission.bill_no ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_challan_no", NpgsqlDbType.Text, objtran_scm_bill_submission.challan_no ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_documents", NpgsqlDbType.Text, objtran_scm_bill_submission.documents ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_terms_conditions", NpgsqlDbType.Text, objtran_scm_bill_submission.terms_conditions ?? (object)DBNull.Value);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_scm_bill_submission.remarks ?? (object)DBNull.Value);

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



        public async Task<List<tran_scm_bill_submission_DTO>> GetAllJoined_TranScmBillSubmissionAsync(Int64 row_index, Int64 page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_scm_bill_submission( @row_index,@page_size)";

                    var dataList = connection.Query<tran_scm_bill_submission_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size
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


        public async Task<bool> Update_Bill_Approval_Async(tran_scm_bill_submission_entity entity)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_scm_bill_submission
                                                 SET approved_date = @p_approved_date,
                                                     approved_by = @p_approved_by,
                                                     is_approved = @p_is_approved
                                                 WHERE bill_submission_id = @p_bill_submission_id";



                     await connection.ExecuteAsync(query, new
                    {
                        p_bill_submission_id = entity.bill_submission_id,
                        p_approved_date = DateTime.Now,
                        p_approved_by = entity.updated_by,
                        p_is_approved = entity.is_approved,


                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }


    }

}

