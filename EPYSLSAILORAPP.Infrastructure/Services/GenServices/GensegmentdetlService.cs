
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity.Tran_Tables;
using static Postgrest.Constants;
using Npgsql;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenSegmentDetlService : IGenSegmentDetlService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public GenSegmentDetlService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync( gen_segment_detl_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_segment_detl_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_segment_detl_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_segment_detl_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_segment_detl_entity>> GetAllAsync()
        {
            List<gen_segment_detl_entity> list = new List<gen_segment_detl_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_segment_detl_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_segment_detl_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_segment_detl_entity>> GetPagedData(DtParameters filter)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_segment_detl m
                                           where 
                                           case when @segment_id is null then true
                                                when  @segment_id is not null and m.gen_segment_id=@segment_id then true else false end
                                           and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.segment_value ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                var dataList = connection.Query<gen_segment_detl_entity>(query,
                    new
                    {
                        segment_id=filter.MasterID.Value,
                        search_text = filter.Search.Value,
                        row_index = filter.Start,
                        page_size = filter.Length
                    }).ToList();

                return dataList;

            }

        }

        public async Task<List<gen_segment_detl_entity>> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_segment_detl m   where m.gen_segment_detl_id=@Id";

                    var dataList = connection.Query<gen_segment_detl_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_segment_detl_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_segment_detl_entity>> GetAllBySegmentID(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_segment_detl m   where m.gen_segment_id=@Id";

                    var dataList = connection.Query<gen_segment_detl_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_segment_detl_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

