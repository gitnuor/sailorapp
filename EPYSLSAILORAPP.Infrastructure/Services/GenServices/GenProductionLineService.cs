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
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenProductionLineService : IGenProductionLineService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenProductionLineService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_production_line_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_production_line_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_production_line_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_production_line_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_production_line_entity>> GetAllAsync()
        {
            List<gen_production_line_entity> list = new List<gen_production_line_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_production_line_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_production_line_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_production_line_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_production_line m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.line_desc ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_production_line_entity>(query,
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



        public async Task<gen_production_line_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_production_line m   where m.production_line_id=@Id";

                    var dataList = connection.Query<gen_production_line_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_production_line_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public List<gen_production_line_entity> GetSingleLineByAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_dropdown_data_for_production_line( @p_warehouse_id)";

                    var dataList = connection.Query<gen_production_line_entity>(query,
                          new
                          {
                              p_warehouse_id = Id
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<bool> DeleteAsync(Int64? Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new gen_production_line_entity { production_line_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> gen_production_line_insert_sp(gen_production_line_DTO objgen_production_line)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_production_line_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_floor_id", NpgsqlDbType.Bigint, objgen_production_line.floor_id == null ? DBNull.Value : objgen_production_line.floor_id);
                        Command.Parameters.AddWithValue("in_line_desc", NpgsqlDbType.Text, objgen_production_line.line_desc == null ? DBNull.Value : objgen_production_line.line_desc);
                        Command.Parameters.AddWithValue("in_after_wash_status", NpgsqlDbType.Bigint, objgen_production_line.after_wash_status == null ? DBNull.Value : objgen_production_line.after_wash_status);
                        Command.Parameters.AddWithValue("in_auto_sewing_output", NpgsqlDbType.Bigint, objgen_production_line.auto_sewing_output == null ? DBNull.Value : objgen_production_line.auto_sewing_output);
                        Command.Parameters.AddWithValue("in_seq_no", NpgsqlDbType.Bigint, objgen_production_line.seq_no == null ? DBNull.Value : objgen_production_line.seq_no);
                        Command.Parameters.AddWithValue("in_auto_iron_output", NpgsqlDbType.Bigint, objgen_production_line.auto_iron_output == null ? DBNull.Value : objgen_production_line.auto_iron_output);
                        Command.Parameters.AddWithValue("in_auto_hang_tag_output", NpgsqlDbType.Bigint, objgen_production_line.auto_hang_tag_output == null ? DBNull.Value : objgen_production_line.auto_hang_tag_output);
                        Command.Parameters.AddWithValue("in_auto_folding_output", NpgsqlDbType.Bigint, objgen_production_line.auto_folding_output == null ? DBNull.Value : objgen_production_line.auto_folding_output);
                        Command.Parameters.AddWithValue("in_auto_poly_output", NpgsqlDbType.Bigint, objgen_production_line.auto_poly_output == null ? DBNull.Value : objgen_production_line.auto_poly_output);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_production_line.added_by == null ? DBNull.Value : objgen_production_line.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_production_line.date_added == null ? DBNull.Value : objgen_production_line.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_production_line.updated_by == null ? DBNull.Value : objgen_production_line.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_production_line.date_updated == null ? DBNull.Value : objgen_production_line.date_updated);
                        Command.Parameters.AddWithValue("in_line_name", NpgsqlDbType.Text, objgen_production_line.line_name == null ? DBNull.Value : objgen_production_line.line_name);


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
        public async Task<bool> gen_production_line_update_sp(gen_production_line_DTO objgen_production_line)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_production_line_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_floor_id", NpgsqlDbType.Bigint, objgen_production_line.floor_id == null ? DBNull.Value : objgen_production_line.floor_id);
                        Command.Parameters.AddWithValue("in_line_desc", NpgsqlDbType.Text, objgen_production_line.line_desc == null ? DBNull.Value : objgen_production_line.line_desc);
                        Command.Parameters.AddWithValue("in_after_wash_status", NpgsqlDbType.Bigint, objgen_production_line.after_wash_status == null ? DBNull.Value : objgen_production_line.after_wash_status);
                        Command.Parameters.AddWithValue("in_auto_sewing_output", NpgsqlDbType.Bigint, objgen_production_line.auto_sewing_output == null ? DBNull.Value : objgen_production_line.auto_sewing_output);
                        Command.Parameters.AddWithValue("in_seq_no", NpgsqlDbType.Bigint, objgen_production_line.seq_no == null ? DBNull.Value : objgen_production_line.seq_no);
                        Command.Parameters.AddWithValue("in_auto_iron_output", NpgsqlDbType.Bigint, objgen_production_line.auto_iron_output == null ? DBNull.Value : objgen_production_line.auto_iron_output);
                        Command.Parameters.AddWithValue("in_auto_hang_tag_output", NpgsqlDbType.Bigint, objgen_production_line.auto_hang_tag_output == null ? DBNull.Value : objgen_production_line.auto_hang_tag_output);
                        Command.Parameters.AddWithValue("in_auto_folding_output", NpgsqlDbType.Bigint, objgen_production_line.auto_folding_output == null ? DBNull.Value : objgen_production_line.auto_folding_output);
                        Command.Parameters.AddWithValue("in_auto_poly_output", NpgsqlDbType.Bigint, objgen_production_line.auto_poly_output == null ? DBNull.Value : objgen_production_line.auto_poly_output);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_production_line.added_by == null ? DBNull.Value : objgen_production_line.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_production_line.date_added == null ? DBNull.Value : objgen_production_line.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_production_line.updated_by == null ? DBNull.Value : objgen_production_line.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_production_line.date_updated == null ? DBNull.Value : objgen_production_line.date_updated);
                        Command.Parameters.AddWithValue("in_line_name", NpgsqlDbType.Text, objgen_production_line.line_name == null ? DBNull.Value : objgen_production_line.line_name);


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
        //public async Task<List<rpc_gen_production_line_DTO>> GetAllJoined_GenProductionLineAsync(Int64 currnet_page, Int64 page_size)
        //{
        //    try
        //    {
        //        using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
        //        {
        //            connection.Open();

        //            string query = $"SELECT * FROM proc_sp_get_data_gen_production_line( @currnet_page,@page_size)";

        //            var dataList = connection.Query<rpc_gen_production_line_DTO>(query,
        //                  new
        //                  {
        //                      currnet_page = currnet_page,
        //                      page_size = page_size
        //                  }
        //                 ).AsList();

        //            return dataList;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //}

    }

}

