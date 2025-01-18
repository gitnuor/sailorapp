
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using Postgrest;
using System.Data;
using static Dapper.SqlMapper;
using static Postgrest.Constants;
using static Postgrest.QueryOptions;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranMcdRequisitionSlipService : ITranMcdRequisitionSlipService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranMcdRequisitionSlipService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;

        }


        public async Task<List<tran_mcd_requisition_slip_entity>> GetAllAsync()
        {
            List<tran_mcd_requisition_slip_entity> list = new List<tran_mcd_requisition_slip_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_mcd_requisition_slip_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_mcd_requisition_slip_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<tran_mcd_requisition_slip_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_mcd_requisition_slip m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.requisition_slip_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_mcd_requisition_slip_entity>(query,
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

        public async Task<tran_mcd_requisition_slip_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_mcd_requisition_slip m   where m.requisition_slip_id=@Id";

                    var dataList = connection.Query<tran_mcd_requisition_slip_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_mcd_requisition_slip_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_mcd_requisition_slip_entity { requisition_slip_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<bool> tran_fabric_requisition_slip_insert_sp(tran_mcd_requisition_slip_DTO objtran_mcd_requisition_slip)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_mcd_requisition_slip_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_requisition_slip_no", NpgsqlDbType.Text, objtran_mcd_requisition_slip.requisition_slip_no == null ? DBNull.Value : objtran_mcd_requisition_slip.requisition_slip_no);
                        Command.Parameters.AddWithValue("in_requisition_date", NpgsqlDbType.Date, objtran_mcd_requisition_slip.requisition_date == null ? DBNull.Value : objtran_mcd_requisition_slip.requisition_date);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.techpack_id == null ? DBNull.Value : objtran_mcd_requisition_slip.techpack_id);
                        Command.Parameters.AddWithValue("in_requisition_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.requisition_by == null ? DBNull.Value : objtran_mcd_requisition_slip.requisition_by);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_mcd_requisition_slip.remarks == null ? DBNull.Value : objtran_mcd_requisition_slip.remarks);
                        Command.Parameters.AddWithValue("in_document_list", NpgsqlDbType.Text, objtran_mcd_requisition_slip.document_list == null ? DBNull.Value : objtran_mcd_requisition_slip.document_list);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.approved_by == null ? DBNull.Value : objtran_mcd_requisition_slip.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_mcd_requisition_slip.approve_date == null ? DBNull.Value : objtran_mcd_requisition_slip.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_mcd_requisition_slip.approve_remarks == null ? DBNull.Value : objtran_mcd_requisition_slip.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.added_by == null ? DBNull.Value : objtran_mcd_requisition_slip.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_mcd_requisition_slip.date_added == null ? DBNull.Value : objtran_mcd_requisition_slip.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.updated_by == null ? DBNull.Value : objtran_mcd_requisition_slip.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_mcd_requisition_slip.date_updated == null ? DBNull.Value : objtran_mcd_requisition_slip.date_updated);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.gen_item_structure_group_id == null ? DBNull.Value : objtran_mcd_requisition_slip.gen_item_structure_group_id);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_sub_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.gen_item_structure_group_sub_id == null ? DBNull.Value : objtran_mcd_requisition_slip.gen_item_structure_group_sub_id);
                        Command.Parameters.AddWithValue("in_is_acknowledged", NpgsqlDbType.Boolean, objtran_mcd_requisition_slip.is_acknowledged == null ? DBNull.Value : objtran_mcd_requisition_slip.is_acknowledged);
                        Command.Parameters.AddWithValue("in_acknowledged_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.acknowledged_by == null ? DBNull.Value : objtran_mcd_requisition_slip.acknowledged_by);
                        Command.Parameters.AddWithValue("in_acknowledged_date", NpgsqlDbType.Date, objtran_mcd_requisition_slip.acknowledged_date == null ? DBNull.Value : objtran_mcd_requisition_slip.acknowledged_date);
                        Command.Parameters.AddWithValue("in_acknowledged_remarks", NpgsqlDbType.Text, objtran_mcd_requisition_slip.acknowledged_remarks == null ? DBNull.Value : objtran_mcd_requisition_slip.acknowledged_remarks);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.event_id == null ? DBNull.Value : objtran_mcd_requisition_slip.event_id);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.fiscal_year_id == null ? DBNull.Value : objtran_mcd_requisition_slip.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_requisition_slip_detail", NpgsqlDbType.Text, objtran_mcd_requisition_slip.requisition_slip_detail == null ? DBNull.Value : objtran_mcd_requisition_slip.requisition_slip_detail);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.is_submitted == null ? DBNull.Value : objtran_mcd_requisition_slip.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.is_approved == null ? DBNull.Value : objtran_mcd_requisition_slip.is_approved);


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
       
  
        public async Task<bool> SaveAsync(tran_mcd_requisition_slip_DTO entity)
        {
            try
            {
                foreach (var single in entity.details)
                {
                    single.date_added = DateTime.Now;
                    single.added_by = entity.added_by;
                }

                entity.requisition_slip_detail = JArray.Parse(JsonConvert.SerializeObject(entity.details)).ToString();

                return await tran_fabric_requisition_slip_insert_sp(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> UpdateAsync(tran_mcd_requisition_slip_DTO entity)
        {

            try
            {
                
                foreach (var single in entity.details)
                {
                    single.date_added = DateTime.Now;
                    single.added_by = entity.added_by;
                }

                entity.requisition_slip_detail = JsonConvert.SerializeObject(entity.details);//.ToString();

                return await tran_fabric_requisition_slip_update_sp(entity);

            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<bool> tran_fabric_requisition_slip_update_sp(tran_mcd_requisition_slip_DTO objtran_mcd_requisition_slip)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_mcd_requisition_slip_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_requisition_slip_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.requisition_slip_id == null ? DBNull.Value : objtran_mcd_requisition_slip.requisition_slip_id);
                        Command.Parameters.AddWithValue("in_requisition_slip_no", NpgsqlDbType.Text, objtran_mcd_requisition_slip.requisition_slip_no == null ? DBNull.Value : objtran_mcd_requisition_slip.requisition_slip_no);
                        Command.Parameters.AddWithValue("in_requisition_date", NpgsqlDbType.Date, objtran_mcd_requisition_slip.requisition_date == null ? DBNull.Value : objtran_mcd_requisition_slip.requisition_date);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.techpack_id == null ? DBNull.Value : objtran_mcd_requisition_slip.techpack_id);
                        Command.Parameters.AddWithValue("in_requisition_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.requisition_by == null ? DBNull.Value : objtran_mcd_requisition_slip.requisition_by);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_mcd_requisition_slip.remarks == null ? DBNull.Value : objtran_mcd_requisition_slip.remarks);
                        Command.Parameters.AddWithValue("in_document_list", NpgsqlDbType.Text, objtran_mcd_requisition_slip.document_list == null ? DBNull.Value : objtran_mcd_requisition_slip.document_list);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.approved_by == null ? DBNull.Value : objtran_mcd_requisition_slip.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_mcd_requisition_slip.approve_date == null ? DBNull.Value : objtran_mcd_requisition_slip.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_mcd_requisition_slip.approve_remarks == null ? DBNull.Value : objtran_mcd_requisition_slip.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.added_by == null ? DBNull.Value : objtran_mcd_requisition_slip.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_mcd_requisition_slip.date_added == null ? DBNull.Value : objtran_mcd_requisition_slip.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.updated_by == null ? DBNull.Value : objtran_mcd_requisition_slip.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_mcd_requisition_slip.date_updated == null ? DBNull.Value : objtran_mcd_requisition_slip.date_updated);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.gen_item_structure_group_id == null ? DBNull.Value : objtran_mcd_requisition_slip.gen_item_structure_group_id);
                        Command.Parameters.AddWithValue("in_gen_item_structure_group_sub_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.gen_item_structure_group_sub_id == null ? DBNull.Value : objtran_mcd_requisition_slip.gen_item_structure_group_sub_id);
                        Command.Parameters.AddWithValue("in_is_acknowledged", NpgsqlDbType.Boolean, objtran_mcd_requisition_slip.is_acknowledged == null ? DBNull.Value : objtran_mcd_requisition_slip.is_acknowledged);
                        Command.Parameters.AddWithValue("in_acknowledged_by", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.acknowledged_by == null ? DBNull.Value : objtran_mcd_requisition_slip.acknowledged_by);
                        Command.Parameters.AddWithValue("in_acknowledged_date", NpgsqlDbType.Date, objtran_mcd_requisition_slip.acknowledged_date == null ? DBNull.Value : objtran_mcd_requisition_slip.acknowledged_date);
                        Command.Parameters.AddWithValue("in_acknowledged_remarks", NpgsqlDbType.Text, objtran_mcd_requisition_slip.acknowledged_remarks == null ? DBNull.Value : objtran_mcd_requisition_slip.acknowledged_remarks);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.event_id == null ? DBNull.Value : objtran_mcd_requisition_slip.event_id);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.fiscal_year_id == null ? DBNull.Value : objtran_mcd_requisition_slip.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_requisition_slip_detail", NpgsqlDbType.Text, objtran_mcd_requisition_slip.requisition_slip_detail == null ? DBNull.Value : objtran_mcd_requisition_slip.requisition_slip_detail);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.is_submitted == null ? DBNull.Value : objtran_mcd_requisition_slip.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_mcd_requisition_slip.is_approved == null ? DBNull.Value : objtran_mcd_requisition_slip.is_approved);


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
        public async Task<bool> ProposedAsync(tran_mcd_requisition_slip_DTO request)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @"UPDATE tran_mcd_requisition_slip
                                          SET 
                                              is_submitted = 2

                                          WHERE requisition_slip_id = @requisition_slip_id";


                    await connection.ExecuteAsync(query, new
                    {
                        requisition_slip_id = request.requisition_slip_id

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


        public async Task<bool> ApproveAsync(tran_mcd_requisition_slip_DTO request)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @" UPDATE tran_mcd_requisition_slip
                                        SET is_approved = 1
                                        WHERE requisition_slip_id = @requisition_slip_id;";

                    await connection.ExecuteAsync(query, new
                    {
                        requisition_slip_id = request.requisition_slip_id

                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }

}

