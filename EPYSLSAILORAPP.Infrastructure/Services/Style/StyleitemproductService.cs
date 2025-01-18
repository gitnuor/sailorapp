using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{
    public class StyleitemproductService : IStyleitemproductService
    {

        private readonly IConfiguration _configuration;

        public StyleitemproductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(style_item_product_DTO objstyle_item_product)
        {

            try
            {
                foreach (var item in objstyle_item_product.DetList)
                {

                    item.added_by = objstyle_item_product.added_by;
                    item.date_added = objstyle_item_product.date_added;
                    //item.sub_category_name = objstyle_item_product.sub_category_name;
                }

                objstyle_item_product.product_sub_category_json = JArray.Parse(JsonConvert.SerializeObject(objstyle_item_product.DetList)).ToString();

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var Command = new NpgsqlCommand("style_item_product_insert", connection);

                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.AddWithValue("in_style_item_product_name", NpgsqlDbType.Text, objstyle_item_product.style_item_product_name == null ? DBNull.Value : objstyle_item_product.style_item_product_name);

                            Command.Parameters.AddWithValue("in_style_item_type_id", NpgsqlDbType.Bigint, objstyle_item_product.style_item_type_id == null ? DBNull.Value : objstyle_item_product.style_item_type_id);

                            Command.Parameters.AddWithValue("in_style_product_type_id", NpgsqlDbType.Bigint, objstyle_item_product.style_product_type_id == null ? DBNull.Value : objstyle_item_product.style_product_type_id);

                            Command.Parameters.AddWithValue("in_style_item_origin_id", NpgsqlDbType.Bigint, objstyle_item_product.style_item_origin_id == null ? DBNull.Value : objstyle_item_product.style_item_origin_id);

                            Command.Parameters.AddWithValue("in_style_gender_id", NpgsqlDbType.Bigint, objstyle_item_product.style_gender_id == null ? DBNull.Value : objstyle_item_product.style_gender_id);

                            Command.Parameters.AddWithValue("in_style_master_category_id", NpgsqlDbType.Bigint, objstyle_item_product.style_master_category_id == null ? DBNull.Value : objstyle_item_product.style_master_category_id);

                            Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objstyle_item_product.added_by == null ? DBNull.Value : objstyle_item_product.added_by);

                            Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objstyle_item_product.date_added == null ? DBNull.Value : objstyle_item_product.date_added);

                            Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objstyle_item_product.updated_by == null ? DBNull.Value : objstyle_item_product.updated_by);

                            Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objstyle_item_product.date_updated == null ? DBNull.Value : objstyle_item_product.date_updated);

                            Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objstyle_item_product.fiscal_year_id == null ? DBNull.Value : objstyle_item_product.fiscal_year_id);

                            Command.Parameters.AddWithValue("in_style_product_size_group_id", NpgsqlDbType.Bigint, objstyle_item_product.style_product_size_group_id == null ? DBNull.Value : objstyle_item_product.style_product_size_group_id);

                            Command.Parameters.AddWithValue("in_product_sub_category_json", NpgsqlDbType.Text, objstyle_item_product.product_sub_category_json == null ? DBNull.Value : objstyle_item_product.product_sub_category_json);


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

        public async Task<bool> UpdateAsync(style_item_product_DTO objstyle_item_product)
        {
            foreach (var item in objstyle_item_product.DetList)
            {

                item.added_by = objstyle_item_product.added_by;
                item.date_added = objstyle_item_product.date_added;

                //item.sub_category_name = objstyle_item_product.sub_category_name;
            }

            objstyle_item_product.product_sub_category_json = JArray.Parse(JsonConvert.SerializeObject(objstyle_item_product.DetList)).ToString();

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("style_item_product_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint, objstyle_item_product.style_item_product_id == null ? DBNull.Value : objstyle_item_product.style_item_product_id);
                        Command.Parameters.AddWithValue("in_style_item_product_name", NpgsqlDbType.Text, objstyle_item_product.style_item_product_name == null ? DBNull.Value : objstyle_item_product.style_item_product_name);
                        Command.Parameters.AddWithValue("in_style_item_type_id", NpgsqlDbType.Bigint, objstyle_item_product.style_item_type_id == null ? DBNull.Value : objstyle_item_product.style_item_type_id);
                        Command.Parameters.AddWithValue("in_style_product_type_id", NpgsqlDbType.Bigint, objstyle_item_product.style_product_type_id == null ? DBNull.Value : objstyle_item_product.style_product_type_id);
                        Command.Parameters.AddWithValue("in_style_item_origin_id", NpgsqlDbType.Bigint, objstyle_item_product.style_item_origin_id == null ? DBNull.Value : objstyle_item_product.style_item_origin_id);
                        Command.Parameters.AddWithValue("in_style_gender_id", NpgsqlDbType.Bigint, objstyle_item_product.style_gender_id == null ? DBNull.Value : objstyle_item_product.style_gender_id);
                        Command.Parameters.AddWithValue("in_style_master_category_id", NpgsqlDbType.Bigint, objstyle_item_product.style_master_category_id == null ? DBNull.Value : objstyle_item_product.style_master_category_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objstyle_item_product.added_by == null ? DBNull.Value : objstyle_item_product.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objstyle_item_product.date_added == null ? DBNull.Value : objstyle_item_product.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objstyle_item_product.updated_by == null ? DBNull.Value : objstyle_item_product.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objstyle_item_product.date_updated == null ? DBNull.Value : objstyle_item_product.date_updated);
                        Command.Parameters.AddWithValue("in_style_product_size_group_id", NpgsqlDbType.Bigint, objstyle_item_product.style_product_size_group_id == null ? DBNull.Value : objstyle_item_product.style_product_size_group_id);
                        Command.Parameters.AddWithValue("in_product_sub_category_json", NpgsqlDbType.Text, objstyle_item_product.product_sub_category_json == null ? DBNull.Value : objstyle_item_product.product_sub_category_json);


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

        public async Task<List<style_item_product_entity>> GetAllAsync(Int64? product_id)
        {
            List<style_item_product_entity> list = new List<style_item_product_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<style_item_product_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<style_item_product_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<style_item_product_entity>> GetAllByPagingAsync(Filter_RangePlan_DataTable param)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM style_item_product m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m. ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<style_item_product_entity>(query,
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

        public async Task<style_item_product_DTO> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*,
                                   (select jsonb_agg(
                                                   jsonb_build_object(
                                                           'style_item_product_id', sipsc.style_item_product_id,
                                                           'sub_category_name', sipsc.sub_category_name,
                                                           
                                                          'style_item_product_sub_category_id',sipsc.style_item_product_sub_category_id
                                                       )
                                               )
                                    from style_item_product_sub_category sipsc
                                    where sipsc.style_item_product_id=m.style_item_product_id)::text as product_sub_category_json
                                    FROM style_item_product m
                                    where m.style_item_product_id =@Id";

                    var objRet = connection.Query<style_item_product_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    var retObj = JsonConvert.DeserializeObject<style_item_product_DTO>(JsonConvert.SerializeObject(objRet));

                    retObj.DetList = JsonConvert.DeserializeObject<List<style_item_product_sub_category_DTO>>(objRet.product_sub_category_json);

                    return retObj;//
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
    }

}

