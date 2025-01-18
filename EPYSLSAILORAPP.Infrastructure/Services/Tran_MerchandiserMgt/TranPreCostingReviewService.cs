
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using Postgrest;
using System.Data;
using static Postgrest.Constants;


namespace EPYSLSAILORAPP.Infrastructure.Services
{
    public class TranPreCostingReviewService : ITranPreCostingReviewService
    {

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;


        public TranPreCostingReviewService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
         }

        public async Task<bool> SaveAsync(tran_pre_costing_review_DTO objtran_pre_costing_review)
        {

            try
            {
               // var objEntity = JsonConvert.DeserializeObject<tran_pre_costing_review_entity>(JsonConvert.SerializeObject(entity));

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var Command = new NpgsqlCommand("tran_pre_costing_review_insert", connection);

                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.AddWithValue("in_tran_pre_costing_id", NpgsqlDbType.Bigint, objtran_pre_costing_review.tran_pre_costing_id == null ? DBNull.Value : objtran_pre_costing_review.tran_pre_costing_id);
                            Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_pre_costing_review.is_submitted == null ? DBNull.Value : objtran_pre_costing_review.is_submitted);
                            Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_pre_costing_review.submitted_by == null ? DBNull.Value : objtran_pre_costing_review.submitted_by);
                            Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_pre_costing_review.added_by == null ? DBNull.Value : objtran_pre_costing_review.added_by);
                            Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_pre_costing_review.date_added == null ? DBNull.Value : objtran_pre_costing_review.date_added);

                            Command.Parameters.AddWithValue("in_version_no", NpgsqlDbType.Text, objtran_pre_costing_review.version_no == null ? DBNull.Value : objtran_pre_costing_review.version_no);
                            Command.Parameters.AddWithValue("in_old_data", NpgsqlDbType.Text, objtran_pre_costing_review.old_data == null ? DBNull.Value : objtran_pre_costing_review.old_data);
                            Command.Parameters.AddWithValue("in_new_data", NpgsqlDbType.Text, objtran_pre_costing_review.new_data == null ? DBNull.Value : objtran_pre_costing_review.new_data);
                          
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

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<bool> UpdateAsync(tran_pre_costing_review_DTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_designer_review_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateReviewAck(tran_pre_costing_review_DTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_designer_review_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var query = @"update tran_pre_costing_review
                                    set is_ack_by_merchant=1,
                                    merchant_ack_by=@merchant_ack_by,
                                    merchant_ack_date=@merchant_ack_date,
                                    merchant_ack_remarks=@merchant_ack_remarks
                                where tran_pre_costing_review_id=@tran_pre_costing_review_id";

                    connection.Execute(query,
                        new
                        {
                            merchant_ack_by = entity.merchant_ack_by,
                            merchant_ack_date = entity.merchant_ack_date,
                            merchant_ack_remarks = entity.merchant_ack_remarks,
                            tran_pre_costing_review_id = entity.tran_pre_costing_review_id
                        });
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

       

        public async Task<List<tran_pre_costing_review_DTO>> GetAllAsync()
        {

            List<tran_pre_costing_review_entity> list = new List<tran_pre_costing_review_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_pre_costing_review_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<tran_pre_costing_review_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<List<tran_pre_costing_review_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_pre_costing_review m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.version_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_pre_costing_review_entity>(query,
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

        public async Task<tran_pre_costing_review_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_pre_costing_review m   where m.tran_pre_costing_review_id=@Id";

                    var dataList = connection.Query<tran_pre_costing_review_DTO>(query,
                        new { @Id = Id }).FirstOrDefault();

                    return dataList;
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

                    var objToDelete = new tran_pre_costing_review_entity { tran_pre_costing_review_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }

}

