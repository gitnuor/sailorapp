
using AutoMapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using ServiceStack;
using System.Data;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenSegmentService : IGenSegmentService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenSegmentService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_segment_DTO objgen_segment)
        {
            try
            {
                objgen_segment.segment_detl_json = JArray.Parse(JsonConvert.SerializeObject(objgen_segment.DetList)).ToString();

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var Command = new NpgsqlCommand("gen_segment_insert", connection);

                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.AddWithValue("in_gen_segment_name", NpgsqlDbType.Text, objgen_segment.gen_segment_name == null ? DBNull.Value : objgen_segment.gen_segment_name);
                            Command.Parameters.AddWithValue("in_display_name", NpgsqlDbType.Text, objgen_segment.display_name == null ? DBNull.Value : objgen_segment.display_name);
                            Command.Parameters.AddWithValue("in_is_item_segment", NpgsqlDbType.Boolean, objgen_segment.is_item_segment == null ? DBNull.Value : objgen_segment.is_item_segment);
                            Command.Parameters.AddWithValue("in_is_active", NpgsqlDbType.Boolean, objgen_segment.is_active == null ? DBNull.Value : objgen_segment.is_active);
                            Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_segment.added_by == null ? DBNull.Value : objgen_segment.added_by);
                            Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_segment.date_added == null ? DBNull.Value : objgen_segment.date_added);
                            Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_segment.updated_by == null ? DBNull.Value : objgen_segment.updated_by);
                            Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_segment.date_updated == null ? DBNull.Value : objgen_segment.date_updated);
                            Command.Parameters.AddWithValue("in_segment_detl_json", NpgsqlDbType.Text, objgen_segment.segment_detl_json == null ? DBNull.Value : objgen_segment.segment_detl_json);


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

        public async Task<bool> UpdateAsync(gen_segment_DTO objgen_segment)
        {

            objgen_segment.segment_detl_json = JArray.Parse(JsonConvert.SerializeObject(objgen_segment.DetList)).ToString();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {

                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {

                   

                    try
                    {
                        var Command = new NpgsqlCommand("gen_segment_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_gen_segment_id", NpgsqlDbType.Bigint, objgen_segment.gen_segment_id == null ? DBNull.Value : objgen_segment.gen_segment_id);

                        Command.Parameters.AddWithValue("in_gen_segment_name", NpgsqlDbType.Text, objgen_segment.gen_segment_name == null ? DBNull.Value : objgen_segment.gen_segment_name);
                        Command.Parameters.AddWithValue("in_display_name", NpgsqlDbType.Text, objgen_segment.display_name == null ? DBNull.Value : objgen_segment.display_name);
                        Command.Parameters.AddWithValue("in_is_item_segment", NpgsqlDbType.Boolean, objgen_segment.is_item_segment == null ? DBNull.Value : objgen_segment.is_item_segment);
                        Command.Parameters.AddWithValue("in_is_active", NpgsqlDbType.Boolean, objgen_segment.is_active == null ? DBNull.Value : objgen_segment.is_active);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_segment.added_by == null ? DBNull.Value : objgen_segment.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_segment.date_added == null ? DBNull.Value : objgen_segment.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_segment.updated_by == null ? DBNull.Value : objgen_segment.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_segment.date_updated == null ? DBNull.Value : objgen_segment.date_updated);
                        Command.Parameters.AddWithValue("in_segment_detl_json", NpgsqlDbType.Text, objgen_segment.segment_detl_json == null ? DBNull.Value : objgen_segment.segment_detl_json);


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

        public async Task<List<gen_segment_entity>> GetAllAsync()
        {
            List<gen_segment_entity> list = new List<gen_segment_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_segment_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_segment_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_segment_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_segment m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.gen_segment_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_segment_entity>(query,
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

        public async Task<gen_segment_DTO> GetAsync(Int64 Id)
        {
            try
            {
                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = @"SELECT m.*,
                                       (select jsonb_agg(
                                                       jsonb_build_object(
                                                               'gen_segment_detl_id', sipsc.gen_segment_detl_id,
                                                               'gen_segment_id', sipsc.gen_segment_id,
                                                               'segment_value', sipsc.segment_value,
                                                               'is_active', sipsc.is_active
                                                           )
                                                   )
                                        from gen_segment_detl sipsc
                                        where sipsc.gen_segment_id = m.gen_segment_id) as segment_detl_json
                                FROM gen_segment m
                                where m.gen_segment_id =@Id";

                        var objdata = connection.Query<gen_segment_DTO>(query,
                            new { @Id = Id }).ToList().FirstOrDefault();

                        var objRet = JsonConvert.DeserializeObject<gen_segment_DTO>(JsonConvert.SerializeObject(objdata));

                        if (objRet != null && !string.IsNullOrEmpty(objdata.segment_detl_json))
                        {
                            //objRet.DetList = JsonConvert.DeserializeObject<List<gen_segment_detl_DTO>>(JsonConvert.SerializeObject(objdata.segment_detl_json));
                            objRet.DetList = JsonConvert.DeserializeObject<List<gen_segment_detl_DTO>>(objdata.segment_detl_json);
                        }

                        return objRet;//
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

        public async Task<List<gen_segment_entity>> GetPagedData(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_segment m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.gen_segment_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_segment_entity>(query,
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
    }

}

