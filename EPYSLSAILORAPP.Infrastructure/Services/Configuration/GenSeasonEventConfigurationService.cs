using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Infrastructure.Helper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;

namespace EPYSLSAILORAPP.Infrastructure.Services.BusinessPlanning
{
    public class GenSeasonEventConfigurationService : IGenSeasonEventConfigurationService
    {
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;
        public GenSeasonEventConfigurationService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;

        }

        public async Task<bool> SaveAsync(GenSeasonEventConfigurationDTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_season_event_config>(JsonConvert.SerializeObject(entity));

                objEntity.start_month_id = objEntity.start_date.Value.Month;
                objEntity.end_month_id = objEntity.end_date.Value.Month;

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
            finally
            {
            }
        }

        public async Task<bool> SaveAsyncCopy(gen_season_event_config entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_season_event_config>(JsonConvert.SerializeObject(entity));

                objEntity.start_month_id = objEntity.start_date.Value.Month;
                objEntity.end_month_id = objEntity.end_date.Value.Month;

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
            finally
            {
            }
        }


        public async Task<bool> UpdateAsync(GenSeasonEventConfigurationDTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_season_event_config>(JsonConvert.SerializeObject(entity));

                objEntity.start_month_id = objEntity.start_date.Value.Month;
                objEntity.end_month_id = objEntity.end_date.Value.Month;


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    connection.Update(objEntity);

                    connection.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }

        public async Task<bool> DeleteAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new gen_season_event_config { event_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }

        public async Task<List<GenSeasonEventConfigurationDTO>> GetAllData()
        {
            List<GenSeasonEventConfigurationDTO> list = new List<GenSeasonEventConfigurationDTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_season_event_config_ext>().ToList();

                    return JsonConvert.DeserializeObject<List<GenSeasonEventConfigurationDTO>>(JsonConvert.SerializeObject(dataList));

                }

            }
            catch (Exception ex)
            {
                return list;

            }
            finally
            {

            }

        }

        public async Task<List<gen_season_event_config_ext>> GetAllPagedData(DtParameters dtparam)
        {
            List<gen_season_event_config_ext> list = new List<gen_season_event_config_ext>();

            try
            {

                if (dtparam.fiscal_year_id != 0 && dtparam.event_id == 0)
                {

                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {

                        connection.Open();

                        string query = @" WITH cte_saved AS (SELECT gs.season_name,m.*
                                           FROM gen_season_event_config m
                                            inner join gen_season gs on m.season_id = gs.season_id
                                            
                                            where m.fiscal_year_id=@fiscal_year_id_filter and
                                            case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            gs.season_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                        var dataList = connection.Query<gen_season_event_config_ext>(query,
                            new
                            {
                                fiscal_year_id_filter = dtparam.fiscal_year_id,
                                search_text = dtparam.Search.Value,
                                row_index = dtparam.Start,
                                page_size = dtparam.Length
                            }).ToList();

                        //foreach (var item in dataList)
                        //{
                        //    item.start_date = TimeZoneHelper.ConvertFromUtc(item.start_date.Value);
                        //    item.end_date = TimeZoneHelper.ConvertFromUtc(item.end_date.Value);
                        //}

                        return dataList;// JsonConvert.DeserializeObject<List<GenSeasonEventConfigurationDTO>>(JsonConvert.SerializeObject(dataList));

                    }

                }
                else if (dtparam.fiscal_year_id == 0 && dtparam.event_id != 0)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = @"WITH cte_saved AS (SELECT gs.season_name,m.*
                                           FROM gen_season_event_config m
                                            inner join gen_season gs on m.season_id = gs.season_id
                                            
                                            where  m.event_id=@event_id_filter and
                                            case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            gs.season_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                        var dataList = connection.Query<gen_season_event_config_ext>(query,
                            new
                            {
                                event_id_filter = dtparam.event_id,
                                search_text = dtparam.Search.Value,
                                row_index = dtparam.Start,
                                page_size = dtparam.Length
                            }).ToList();
                        return dataList;// JsonConvert.DeserializeObject<List<GenSeasonEventConfigurationDTO>>(JsonConvert.SerializeObject(dataList));

                    }
                }
                else if (dtparam.fiscal_year_id == 0 && dtparam.event_id == 0)
                {

                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = @"WITH cte_saved AS (SELECT gs.season_name,m.*
                                           FROM gen_season_event_config m
                                            inner join gen_season gs on m.season_id = gs.season_id
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            gs.season_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                        var dataList = connection.Query<gen_season_event_config_ext>(query,
                            new { search_text = dtparam.Search.Value, row_index = dtparam.Start, page_size = dtparam.Length }).ToList();

                        return dataList;

                    }
                }

                return list;

            }
            catch (Exception ex)
            {
                return list;

            }
            finally
            {

            }

        }

        public async Task<List<gen_season_event_config_ext>> GetAllPagedDataForCopy(DtParameters dtparam)
        {
            List<gen_season_event_config_ext> list = new List<gen_season_event_config_ext>();

            try
            {

               
                

                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {

                        connection.Open();

                        string query = @" WITH cte_saved AS (SELECT gs.season_name,m.*
                                           FROM gen_season_event_config m
                                            inner join gen_season gs on m.season_id = gs.season_id
                                            
                                            where m.fiscal_year_id=@fiscal_year_id_filter and
                                            case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            gs.season_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            order by cte_saved.start_date
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                        var dataList = connection.Query<gen_season_event_config_ext>(query,
                            new
                            {
                                fiscal_year_id_filter = dtparam.fiscal_year_id,
                                search_text = dtparam.Search.Value,
                                row_index = dtparam.Start,
                                page_size = dtparam.Length
                            }).ToList();

                        //foreach (var item in dataList)
                        //{
                        //    item.start_date = TimeZoneHelper.ConvertFromUtc(item.start_date.Value);
                        //    item.end_date = TimeZoneHelper.ConvertFromUtc(item.end_date.Value);
                        //}

                        return dataList;// JsonConvert.DeserializeObject<List<GenSeasonEventConfigurationDTO>>(JsonConvert.SerializeObject(dataList));

                    }

                
                
               

               

            }
            catch (Exception ex)
            {
                return list;

            }
            finally
            {

            }

        }


        public async Task<List<gen_season_event_config>> GetAllPagedData2(DtParameters dtparam)
        {
           
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_season_event_config m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.season_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_season_event_config>(query,
                        new { 
                            search_text = dtparam.Search.Value, 
                            row_index = dtparam.Start, 
                            page_size = dtparam.Length 
                        }).ToList();

                    return dataList;

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

      
    }
}
