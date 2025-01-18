
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
using System.Data;
using static Postgrest.Constants;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenBankService : IGenBankService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenBankService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync( gen_bank_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_bank_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_bank_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_bank_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_bank_DTO>> GetAllAsync()
        {
            List<gen_bank_DTO> list = new List<gen_bank_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_bank_DTO>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_bank_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

         public async Task<List<gen_bank_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_bank m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.bank_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_bank_entity>(query,
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

        public async Task<gen_bank_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_bank m   where m.gen_bank_id=@Id";

                    var dataList = connection.Query<gen_bank_DTO>(query,
                        new { Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_bank_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_bank_entity { gen_bank_id = Id };

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

