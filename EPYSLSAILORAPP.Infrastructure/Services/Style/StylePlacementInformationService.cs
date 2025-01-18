
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Domain.DTO;
using static Postgrest.Constants;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using static Postgrest.QueryOptions;
using Postgrest;
using Microsoft.Extensions.Logging;
using Npgsql;
using Dapper.Contrib.Extensions;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using NUnit.Framework.Internal.Execution;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class StylePlacementInformationService : IStylePlacementInformationService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public StylePlacementInformationService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
            }

        public async Task<bool> SaveAsync( style_placement_information_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<style_placement_information_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(style_placement_information_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<style_placement_information_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<style_placement_information_DTO>> GetAllAsync()
        {
            List<style_placement_information_entity> list = new List<style_placement_information_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<style_placement_information_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<style_placement_information_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

         public async Task<List<style_placement_information_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM style_placement_information m
                                           where case
                                                    when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.placement ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<style_placement_information_entity>(query,
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

        public async Task<style_placement_information_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM style_placement_information m   where m.style_placement_information_id=@Id";

                    var dataList = connection.Query<style_placement_information_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return JsonConvert.DeserializeObject<style_placement_information_DTO>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new style_placement_information_entity { style_placement_information_id = Id };

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

