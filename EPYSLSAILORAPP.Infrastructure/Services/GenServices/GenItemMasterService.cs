
using AutoMapper;
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
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenItemMasterService : IGenItemMasterService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenItemMasterService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Int64> SaveAsync(gen_item_master_entity entity)
        {
            try
            {
                Int64 ret;

                var objEntity = JsonConvert.DeserializeObject<gen_item_master_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    ret = connection.Insert(objEntity);
                }

                return ret;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(gen_item_master_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_item_master_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<long> CheckAndSaveItemMasterAsync(gen_item_master_DTO dto, long? usid)
        {
            long InsertedID = 0;
            dto.added_by = usid;
            dto.date_added = DateTime.Now;

            if (dto.segment_det1_id == null) dto.segment_det1_id = 0;
            if (dto.segment_det2_id == null) dto.segment_det2_id = 0;
            if (dto.segment_det3_id == null) dto.segment_det3_id = 0;
            if (dto.segment_det4_id == null) dto.segment_det4_id = 0;
            if (dto.segment_det5_id == null) dto.segment_det5_id = 0;
            if (dto.segment_det6_id == null) dto.segment_det6_id = 0;
            if (dto.segment_det7_id == null) dto.segment_det7_id = 0;
            if (dto.segment_det8_id == null) dto.segment_det8_id = 0;
            if (dto.segment_det9_id == null) dto.segment_det9_id = 0;
            if (dto.segment_det10_id == null) dto.segment_det10_id = 0;
            if (dto.segment_det11_id == null) dto.segment_det11_id = 0;
            if (dto.segment_det12_id == null) dto.segment_det12_id = 0;
            if (dto.segment_det13_id == null) dto.segment_det13_id = 0;
            if (dto.segment_det14_id == null) dto.segment_det14_id = 0;
            if (dto.segment_det15_id == null) dto.segment_det15_id = 0;

            gen_item_master_entity entity = _mapper.Map<gen_item_master_entity>(dto);

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"
                                    SELECT m.* 
                                    FROM gen_item_master m 
                                    WHERE m.segment_det1_id = @SegmentDet1Id
                                      AND m.segment_det2_id = @SegmentDet2Id
                                      AND m.segment_det3_id = @SegmentDet3Id
                                      AND m.segment_det4_id = @SegmentDet4Id
                                      AND m.segment_det5_id = @SegmentDet5Id
                                      AND m.segment_det6_id = @SegmentDet6Id
                                      AND m.segment_det7_id = @SegmentDet7Id
                                      AND m.segment_det8_id = @SegmentDet8Id
                                      AND m.segment_det9_id = @SegmentDet9Id
                                      AND m.segment_det10_id = @SegmentDet10Id
                                      AND m.segment_det11_id = @SegmentDet11Id
                                      AND m.segment_det12_id = @SegmentDet12Id
                                      AND m.segment_det13_id = @SegmentDet13Id
                                      AND m.segment_det14_id = @SegmentDet14Id
                                      AND m.segment_det15_id = @SegmentDet15Id
                                      AND m.item_structure_sub_group_id = @ItemStructureSubGroupId
                                      AND m.item_structure_group_id = @ItemStructureGroupId
                                    LIMIT 1;";

                    var objdata = connection.Query<gen_item_master_DTO>(query, new
                    {
                        SegmentDet1Id = entity.segment_det1_id,
                        SegmentDet2Id = entity.segment_det2_id,
                        SegmentDet3Id = entity.segment_det3_id,
                        SegmentDet4Id = entity.segment_det4_id,
                        SegmentDet5Id = entity.segment_det5_id,
                        SegmentDet6Id = entity.segment_det6_id,
                        SegmentDet7Id = entity.segment_det7_id,
                        SegmentDet8Id = entity.segment_det8_id,
                        SegmentDet9Id = entity.segment_det9_id,
                        SegmentDet10Id = entity.segment_det10_id,
                        SegmentDet11Id = entity.segment_det11_id,
                        SegmentDet12Id = entity.segment_det12_id,
                        SegmentDet13Id = entity.segment_det13_id,
                        SegmentDet14Id = entity.segment_det14_id,
                        SegmentDet15Id = entity.segment_det15_id,
                        ItemStructureSubGroupId = entity.item_structure_sub_group_id,
                        ItemStructureGroupId = entity.item_structure_group_id
                    }).ToList().FirstOrDefault();

                    if (objdata!=null)
                    {
                        InsertedID = objdata.gen_item_master_id ?? 0;
                    }

                    else
                    {

                        InsertedID =await gen_item_master_insert_sp(entity);//connection.Insert(entity);

                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return InsertedID;

        }

        public async Task<Int64> gen_item_master_insert_sp(gen_item_master_entity objgen_item_master)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_item_master_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        var insertedIdParam = Command.Parameters.Add("new_gen_item_master_id", NpgsqlDbType.Bigint);
                        insertedIdParam.Direction = ParameterDirection.Output;


                        Command.Parameters.AddWithValue("in_item_name", NpgsqlDbType.Text, objgen_item_master.item_name == null ? DBNull.Value : objgen_item_master.item_name);
                        Command.Parameters.AddWithValue("in_item_structure_group_id", NpgsqlDbType.Bigint, objgen_item_master.item_structure_group_id == null ? DBNull.Value : objgen_item_master.item_structure_group_id);
                        Command.Parameters.AddWithValue("in_item_structure_sub_group_id", NpgsqlDbType.Bigint, objgen_item_master.item_structure_sub_group_id == null ? DBNull.Value : objgen_item_master.item_structure_sub_group_id);
                        Command.Parameters.AddWithValue("in_measurement_unit_detail_id", NpgsqlDbType.Bigint, objgen_item_master.measurement_unit_detail_id == null ? DBNull.Value : objgen_item_master.measurement_unit_detail_id);
                        Command.Parameters.AddWithValue("in_segment_det1_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det1_id == null ? DBNull.Value : objgen_item_master.segment_det1_id);
                        Command.Parameters.AddWithValue("in_segment_det2_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det2_id == null ? DBNull.Value : objgen_item_master.segment_det2_id);
                        Command.Parameters.AddWithValue("in_segment_det3_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det3_id == null ? DBNull.Value : objgen_item_master.segment_det3_id);
                        Command.Parameters.AddWithValue("in_segment_det4_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det4_id == null ? DBNull.Value : objgen_item_master.segment_det4_id);
                        Command.Parameters.AddWithValue("in_segment_det5_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det5_id == null ? DBNull.Value : objgen_item_master.segment_det5_id);
                        Command.Parameters.AddWithValue("in_segment_det6_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det6_id == null ? DBNull.Value : objgen_item_master.segment_det6_id);
                        Command.Parameters.AddWithValue("in_segment_det7_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det7_id == null ? DBNull.Value : objgen_item_master.segment_det7_id);
                        Command.Parameters.AddWithValue("in_segment_det8_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det8_id == null ? DBNull.Value : objgen_item_master.segment_det8_id);
                        Command.Parameters.AddWithValue("in_segment_det9_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det9_id == null ? DBNull.Value : objgen_item_master.segment_det9_id);
                        Command.Parameters.AddWithValue("in_segment_det10_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det10_id == null ? DBNull.Value : objgen_item_master.segment_det10_id);
                        Command.Parameters.AddWithValue("in_segment_det11_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det11_id == null ? DBNull.Value : objgen_item_master.segment_det11_id);
                        Command.Parameters.AddWithValue("in_segment_det12_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det12_id == null ? DBNull.Value : objgen_item_master.segment_det12_id);
                        Command.Parameters.AddWithValue("in_segment_det13_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det13_id == null ? DBNull.Value : objgen_item_master.segment_det13_id);
                        Command.Parameters.AddWithValue("in_segment_det14_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det14_id == null ? DBNull.Value : objgen_item_master.segment_det14_id);
                        Command.Parameters.AddWithValue("in_segment_det15_id", NpgsqlDbType.Bigint, objgen_item_master.segment_det15_id == null ? DBNull.Value : objgen_item_master.segment_det15_id);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objgen_item_master.remarks == null ? DBNull.Value : objgen_item_master.remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_item_master.added_by == null ? DBNull.Value : objgen_item_master.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_item_master.date_added == null ? DBNull.Value : objgen_item_master.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_item_master.updated_by == null ? DBNull.Value : objgen_item_master.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_item_master.date_updated == null ? DBNull.Value : objgen_item_master.date_updated);
                        Command.Parameters.AddWithValue("in_item_spec", NpgsqlDbType.Text, objgen_item_master.item_spec == null ? DBNull.Value : objgen_item_master.item_spec);
                        Command.Parameters.AddWithValue("in_segment_det1_text", NpgsqlDbType.Text, objgen_item_master.segment_det1_text == null ? DBNull.Value : objgen_item_master.segment_det1_text);
                        Command.Parameters.AddWithValue("in_segment_det2_text", NpgsqlDbType.Text, objgen_item_master.segment_det2_text == null ? DBNull.Value : objgen_item_master.segment_det2_text);
                        Command.Parameters.AddWithValue("in_segment_det3_text", NpgsqlDbType.Text, objgen_item_master.segment_det3_text == null ? DBNull.Value : objgen_item_master.segment_det3_text);
                        Command.Parameters.AddWithValue("in_segment_det4_text", NpgsqlDbType.Text, objgen_item_master.segment_det4_text == null ? DBNull.Value : objgen_item_master.segment_det4_text);
                        Command.Parameters.AddWithValue("in_segment_det5_text", NpgsqlDbType.Text, objgen_item_master.segment_det5_text == null ? DBNull.Value : objgen_item_master.segment_det5_text);
                        Command.Parameters.AddWithValue("in_segment_det6_text", NpgsqlDbType.Text, objgen_item_master.segment_det6_text == null ? DBNull.Value : objgen_item_master.segment_det6_text);
                        Command.Parameters.AddWithValue("in_segment_det7_text", NpgsqlDbType.Text, objgen_item_master.segment_det7_text == null ? DBNull.Value : objgen_item_master.segment_det7_text);
                        Command.Parameters.AddWithValue("in_segment_det8_text", NpgsqlDbType.Text, objgen_item_master.segment_det8_text == null ? DBNull.Value : objgen_item_master.segment_det8_text);
                        Command.Parameters.AddWithValue("in_segment_det9_text", NpgsqlDbType.Text, objgen_item_master.segment_det9_text == null ? DBNull.Value : objgen_item_master.segment_det9_text);
                        Command.Parameters.AddWithValue("in_segment_det10_text", NpgsqlDbType.Text, objgen_item_master.segment_det10_text == null ? DBNull.Value : objgen_item_master.segment_det10_text);
                        Command.Parameters.AddWithValue("in_segment_det11_text", NpgsqlDbType.Text, objgen_item_master.segment_det11_text == null ? DBNull.Value : objgen_item_master.segment_det11_text);
                        Command.Parameters.AddWithValue("in_segment_det12_text", NpgsqlDbType.Text, objgen_item_master.segment_det12_text == null ? DBNull.Value : objgen_item_master.segment_det12_text);
                        Command.Parameters.AddWithValue("in_segment_det13_text", NpgsqlDbType.Text, objgen_item_master.segment_det13_text == null ? DBNull.Value : objgen_item_master.segment_det13_text);
                        Command.Parameters.AddWithValue("in_segment_det14_text", NpgsqlDbType.Text, objgen_item_master.segment_det14_text == null ? DBNull.Value : objgen_item_master.segment_det14_text);
                        Command.Parameters.AddWithValue("in_segment_det15_text", NpgsqlDbType.Text, objgen_item_master.segment_det15_text == null ? DBNull.Value : objgen_item_master.segment_det15_text);

                       
                        Command.ExecuteNonQuery();

                        transaction.Commit();

                        return (Int64)insertedIdParam.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }


        public async Task<List<gen_item_master_DTO>> GetAllAsync()
        {
            List<gen_item_master_DTO> list = new List<gen_item_master_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_item_master_DTO>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_item_master_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_item_master_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_item_master m
                                           where case
                                                    when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.item_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_item_master_entity>(query,
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

        public async Task<gen_item_master_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_item_master m   where m.gen_item_master_id=@Id";

                    var dataList = connection.Query<gen_item_master_DTO>(query,
                        new { Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_item_master_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_item_master_entity { gen_item_master_id = Id };

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

