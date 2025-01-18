
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using Postgrest;
using System.Data;
using static Postgrest.Constants;
namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class StyleSleeveInfoService : IStyleSleeveInfoService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public StyleSleeveInfoService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
           }

        public async Task<bool> SaveAsync( style_sleeve_info_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<style_sleeve_info_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(style_sleeve_info_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<style_sleeve_info_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<style_sleeve_info_DTO>> GetAllAsync()
        {
            List<style_sleeve_info_entity> list = new List<style_sleeve_info_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<style_sleeve_info_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<style_sleeve_info_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<List<style_sleeve_info_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM style_sleeve_info m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0  and (
                                                            m.sleeve_info ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<style_sleeve_info_entity>(query,
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

        public async Task<style_sleeve_info_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM style_sleeve_info m   where m.style_sleeve_info_id=@Id";

                    var dataList = connection.Query<style_sleeve_info_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return JsonConvert.DeserializeObject<style_sleeve_info_DTO>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new style_sleeve_info_entity { style_sleeve_info_id = Id };

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

