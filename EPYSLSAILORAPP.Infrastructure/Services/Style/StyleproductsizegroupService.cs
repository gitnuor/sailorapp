using AutoMapper;
using Dapper;
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
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class StyleproductsizegroupService : IStyleproductsizegroupService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public StyleproductsizegroupService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        } 

        public async Task<bool> SaveAsync(style_product_size_group_DTO entity)
        {
            try
            {
              
                if (entity.style_product_size_group_id == null)
                {

                    entity.size_group_details_json =JArray.Parse(JsonConvert.SerializeObject(entity.DetList)).ToString();

                    return await style_product_size_group_insert_sp(entity);

                }
                else
                {
                    entity.size_group_details_json = JArray.Parse(JsonConvert.SerializeObject(entity.DetList)).ToString();

                    return await style_product_size_group_update_sp(entity);
                }


                
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<style_product_size_group_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM style_product_size_group m
                                           where case
                                                    when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.style_product_size_group_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)

                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<style_product_size_group_entity>(query,
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

        public async Task<bool> style_product_size_group_insert_sp(style_product_size_group_DTO objstyle_product_size_group)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("style_product_size_group_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_style_product_size_group_name", NpgsqlDbType.Text, objstyle_product_size_group.style_product_size_group_name);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objstyle_product_size_group.added_by == null ? DBNull.Value : objstyle_product_size_group.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objstyle_product_size_group.date_added == null ? DBNull.Value : objstyle_product_size_group.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objstyle_product_size_group.updated_by==null?DBNull.Value: objstyle_product_size_group.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objstyle_product_size_group.date_updated==null?DBNull.Value: objstyle_product_size_group.date_updated);
                        Command.Parameters.AddWithValue("in_is_separate_price", NpgsqlDbType.Boolean, objstyle_product_size_group.is_separate_price == null ? DBNull.Value : objstyle_product_size_group.is_separate_price);
                        Command.Parameters.AddWithValue("in_size_group_details_json", NpgsqlDbType.Text, objstyle_product_size_group.size_group_details_json);

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
        public async Task<bool> style_product_size_group_update_sp(style_product_size_group_DTO objstyle_product_size_group)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("style_product_size_group_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_style_product_size_group_id", NpgsqlDbType.Bigint, objstyle_product_size_group.style_product_size_group_id);
                        Command.Parameters.AddWithValue("in_style_product_size_group_name", NpgsqlDbType.Text, objstyle_product_size_group.style_product_size_group_name);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objstyle_product_size_group.added_by == null ? DBNull.Value : objstyle_product_size_group.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objstyle_product_size_group.date_added == null ? DBNull.Value : objstyle_product_size_group.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objstyle_product_size_group.updated_by == null ? DBNull.Value : objstyle_product_size_group.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objstyle_product_size_group.date_updated == null ? DBNull.Value : objstyle_product_size_group.date_updated);
                        Command.Parameters.AddWithValue("in_is_separate_price", NpgsqlDbType.Boolean, objstyle_product_size_group.is_separate_price == null ? DBNull.Value : objstyle_product_size_group.is_separate_price);
                        Command.Parameters.AddWithValue("in_size_group_details_json", NpgsqlDbType.Text, objstyle_product_size_group.size_group_details_json);

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

        public async Task<bool> UpdateAsync(style_product_size_group_DTO entity)
        {
            try
            {
                entity.size_group_details_json = JArray.Parse(JsonConvert.SerializeObject(entity.DetList)).ToString();

                await style_product_size_group_update_sp(entity);

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

                    var objToDelete = new style_product_size_group_entity { style_product_size_group_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<style_product_size_group_entity>> GetAllAsync()
        {
            List<style_product_size_group_entity> list = new List<style_product_size_group_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<style_product_size_group_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<style_product_size_group_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<style_product_size_group_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT spsg.style_product_size_group_id,
                                       spsg.style_product_size_group_name,
                                       spsg.is_separate_price,
                                       spsg.size_group_details_json,
                                       (select jsonb_agg(
                                                       jsonb_build_object(
                                                               'style_product_size_group_id', spsgd.style_product_size_group_id,
                                                               'style_product_size', spsgd.style_product_size
                                                           )
                                                   )
                                        from style_product_size_group_details spsgd
                                        where spsgd.style_product_size_group_id=spsg.style_product_size_group_id)::text size_group_details_json
                                FROM style_product_size_group spsg
                                where spsg.style_product_size_group_id=@style_product_size_group_id
                                ";

                    var dataList = connection.Query<style_product_size_group_entity>(query,
                      new
                      {
                          style_product_size_group_id = Id
                      }).ToList();

                    if(dataList.Count>0)
                    {
                        var retobj = JsonConvert.DeserializeObject<style_product_size_group_DTO>(JsonConvert.SerializeObject( dataList.FirstOrDefault()));

                        retobj.DetList = JsonConvert.DeserializeObject<List<style_product_size_group_details_DTO>>(dataList.FirstOrDefault().size_group_details_json);

                        return retobj;
                    }

                    return new style_product_size_group_DTO();
                }

                //await _connectionSupabse.InitializeAsync();

                //var response =await _connectionSupabse.From<style_product_size_group_entity>()
                //    .Select("*")
                //    .Where(p=>p.style_product_size_group_id==Id)
                //    .Get(); 

                //return  JsonConvert.DeserializeObject<List<style_product_size_group_entity>>(response.Content);
               
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

