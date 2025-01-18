
using AutoMapper;
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
using ServiceStack;
using System.Data;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenItemStructureGroupSubService : IGenItemStructureGroupSubService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenItemStructureGroupSubService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveUpdateAsync(gen_item_structure_group_sub_DTO objgen_warehouse_floor_rack)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_item_structure_insert_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("gen_item_structure_group_sub_id_filter",
                            NpgsqlDbType.Bigint, objgen_warehouse_floor_rack.gen_item_structure_group_sub_id == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.gen_item_structure_group_sub_id);
                        Command.Parameters.AddWithValue("item_structure_group_id_in",
                            NpgsqlDbType.Bigint, objgen_warehouse_floor_rack.item_structure_group_id == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.item_structure_group_id);
                        Command.Parameters.AddWithValue("sub_group_name_in",
                            NpgsqlDbType.Text, objgen_warehouse_floor_rack.sub_group_name == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.sub_group_name);
                        Command.Parameters.AddWithValue("added_by_in",
                            NpgsqlDbType.Bigint, objgen_warehouse_floor_rack.added_by == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.added_by);
                        Command.Parameters.AddWithValue("date_added_in",
                            NpgsqlDbType.Date, objgen_warehouse_floor_rack.date_added == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.date_added);
                        Command.Parameters.AddWithValue("measurement_unit_id_in",
                            NpgsqlDbType.Bigint, objgen_warehouse_floor_rack.measurement_unit_id == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.measurement_unit_id);
                        Command.Parameters.AddWithValue("default_measurement_unit_detail_id_in",
                            NpgsqlDbType.Bigint, objgen_warehouse_floor_rack.default_measurement_unit_detail_id == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.default_measurement_unit_detail_id);
                        Command.Parameters.AddWithValue("measurement_unit_in",
                            NpgsqlDbType.Text, objgen_warehouse_floor_rack.measurement_unit == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.measurement_unit);
                        Command.Parameters.AddWithValue("default_measurement_unit_detail_in",
                            NpgsqlDbType.Text, objgen_warehouse_floor_rack.default_measurement_unit_detail == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.default_measurement_unit_detail);
                        Command.Parameters.AddWithValue("secondary_measurement_unit_id_in",
                            NpgsqlDbType.Bigint, objgen_warehouse_floor_rack.secondary_measurement_unit_id == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.secondary_measurement_unit_id);
                        Command.Parameters.AddWithValue("secondary_measurement_unit_detail_id_in",
                            NpgsqlDbType.Bigint, objgen_warehouse_floor_rack.secondary_measurement_unit_detail_id == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.secondary_measurement_unit_detail_id);
                        Command.Parameters.AddWithValue("secondary_measurement_unit_in",
                            NpgsqlDbType.Text, objgen_warehouse_floor_rack.secondary_measurement_unit == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.secondary_measurement_unit);
                        Command.Parameters.AddWithValue("secondary_measurement_unit_detail_in",
                            NpgsqlDbType.Text, objgen_warehouse_floor_rack.secondary_measurement_unit_detail == null ? DBNull.Value : (object)objgen_warehouse_floor_rack.secondary_measurement_unit_detail);
                        Command.Parameters.AddWithValue("segment_mapping_list",
                            NpgsqlDbType.Text, JArray.Parse(JsonConvert.SerializeObject(objgen_warehouse_floor_rack.List_Mapping)).ToString());

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

        

        public async Task<List<gen_item_structure_group_sub_DTO>> GetAllAsync()
        {
            List<gen_item_structure_group_sub_entity> list = new List<gen_item_structure_group_sub_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_item_structure_group_sub_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<gen_item_structure_group_sub_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_item_structure_group_sub m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.sub_group_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_item_structure_group_sub_entity>(query,
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

        public async Task<gen_item_structure_group_sub_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_item_structure_group_sub m   where m.gen_item_structure_group_sub_id=@Id";

                    var dataList = connection.Query<gen_item_structure_group_sub_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return JsonConvert.DeserializeObject<gen_item_structure_group_sub_DTO>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_item_structure_group_sub_DTO>> GetAllItemStructureSubGroups(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_item_structure_group_sub m   where m.item_structure_group_id=@Id";

                    var dataList = connection.Query<gen_item_structure_group_sub_entity>(query,
                        new { @Id = Id }).ToList();

                    return JsonConvert.DeserializeObject<List<gen_item_structure_group_sub_DTO>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_item_structure_group_sub_entity { gen_item_structure_group_sub_id = Id };

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

