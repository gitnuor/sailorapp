using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using ServiceStack;

namespace EPYSLSAILORAPP.Infrastructure.Services.GenServices
{
    public class GenFiscalYearService : IGenFiscalYearService
    {
        private readonly IConfiguration _configuration;
        public GenFiscalYearService(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public async Task<bool> SaveAsync(gen_fiscal_year_dto entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_fiscal_year>(JsonConvert.SerializeObject(entity));

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


        public async Task<bool> UpdateAsync(gen_fiscal_year entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_fiscal_year>(JsonConvert.SerializeObject(entity));

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


        public async Task<bool> DeleteAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new gen_fiscal_year { fiscal_year_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<gen_fiscal_year> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_fiscal_year m   where m.fiscal_year_id=@Id";

                    var dataList = connection.Query<gen_fiscal_year>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_fiscal_year_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        public async Task<List<gen_fiscal_year_dto>> get_fiscal_year_GetAll()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {

                    connection.Open();

                    var dataList = connection.GetAll<gen_fiscal_year>().ToList().OrderByDescending(p => p.year_no);

                    return JsonConvert.DeserializeObject<List<gen_fiscal_year_dto>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        public async Task<List<gen_fiscal_year_dto>> GetAllAsync()
        {
            List<gen_fiscal_year> list = new List<gen_fiscal_year>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_fiscal_year>().ToList();

                    return JsonConvert.DeserializeObject<List<gen_fiscal_year_dto>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_fiscal_year>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.fiscal_year_id,m.year_no,m.year_name,m.start_date,m.end_date
                                           FROM gen_fiscal_year m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.year_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            order by cte_saved.fiscal_year_id desc
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_fiscal_year>(query,
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
