using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranBarcodeService : ITranBarcodeService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranBarcodeService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_barcode_DTO objtran_barcode)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_barcode_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_barcode.techpack_id == null ? DBNull.Value : objtran_barcode.techpack_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_barcode.added_by == null ? DBNull.Value : objtran_barcode.added_by);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_barcode.fiscal_year_id == null ? DBNull.Value : objtran_barcode.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_barcode.event_id == null ? DBNull.Value : objtran_barcode.event_id);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_barcode.date_added == null ? DBNull.Value : objtran_barcode.date_added);
                        Command.Parameters.AddWithValue("in_tran_barcode", NpgsqlDbType.Text, objtran_barcode.details == null ?
                            DBNull.Value : JArray.Parse(JsonConvert.SerializeObject(objtran_barcode.details)).ToString());

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

        public async Task<bool> UpdateAsync(tran_barcode_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_barcode_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_barcode_entity>> GetAllAsync()
        {
            List<tran_barcode_entity> list = new List<tran_barcode_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_barcode_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_barcode_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_barcode_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_barcode m
                                           where m.fiscal_year_id=@fiscal_year_id and m.event_id=@event_id

                                                     and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.color_code ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_barcode_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length,
                            fiscal_year_id=param.fiscal_year_id,
                            event_id=param.event_id
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        public async Task<tran_barcode_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_barcode m   where m.tran_barcode_id=@Id";

                    var dataList = connection.Query<tran_barcode_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_barcode_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_barcode_entity { tran_barcode_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

   
        public async Task<bool> tran_barcode_update_sp(tran_barcode_DTO objtran_barcode)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_barcode_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_barcode.techpack_id == null ? DBNull.Value : objtran_barcode.techpack_id);
                        Command.Parameters.AddWithValue("in_color_code", NpgsqlDbType.Text, objtran_barcode.color_code == null ? DBNull.Value : objtran_barcode.color_code);
                        Command.Parameters.AddWithValue("in_style_product_size_group_detid", NpgsqlDbType.Bigint, objtran_barcode.style_product_size_group_detid == null ? DBNull.Value : objtran_barcode.style_product_size_group_detid);
                        Command.Parameters.AddWithValue("in_barcode", NpgsqlDbType.Text, objtran_barcode.barcode == null ? DBNull.Value : objtran_barcode.barcode);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_barcode.added_by == null ? DBNull.Value : objtran_barcode.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_barcode.date_added == null ? DBNull.Value : objtran_barcode.date_added);


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
        public async Task<List<rpc_tran_barcode_DTO>> GetAllJoined_TranBarcodeAsync(Int64 fiscal_year_id, Int64 event_id, Int64 row_index, Int64 page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_barcode( @row_index,@page_size,@p_fiscal_year_id,@p_event_id)";

                    var dataList = connection.Query<rpc_tran_barcode_DTO>(query,
                          new
                          {
                              p_fiscal_year_id = fiscal_year_id,
                              p_event_id = event_id,
                              row_index = row_index,
                              page_size = page_size
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

    }

}

