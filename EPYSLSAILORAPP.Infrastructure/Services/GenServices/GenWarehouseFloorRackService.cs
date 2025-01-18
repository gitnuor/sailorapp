
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

    public class GenWarehouseFloorRackService : IGenWarehouseFloorRackService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenWarehouseFloorRackService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
           }

        public async Task<bool> SaveAsync( gen_warehouse_floor_rack_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_warehouse_floor_rack_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_warehouse_floor_rack_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_warehouse_floor_rack_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_warehouse_floor_rack_DTO>> GetAllAsync()
        {
            List<gen_warehouse_floor_rack_entity> list = new List<gen_warehouse_floor_rack_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_warehouse_floor_rack_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<gen_warehouse_floor_rack_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

         public async Task<List<gen_warehouse_floor_rack_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_warehouse_floor_rack m
                                           where case
                                                    when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.rack_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_warehouse_floor_rack_entity>(query,
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

        public async Task<gen_warehouse_floor_rack_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_warehouse_floor_rack m   where m.gen_warehouse_floor_rack_id=@Id";

                    var dataList = connection.Query<gen_warehouse_floor_rack_entity>(query,
                        new { Id = Id }).ToList().FirstOrDefault();

                    return JsonConvert.DeserializeObject<gen_warehouse_floor_rack_DTO>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_warehouse_floor_rack_DTO>> GetByFloorID(Int64 FloorID)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_warehouse_floor_rack m   where m.gen_warehouse_floor_id=@Id";

                    var dataList = connection.Query<gen_warehouse_floor_rack_entity>(query,
                        new { Id = FloorID }).ToList();

                    return JsonConvert.DeserializeObject<List<gen_warehouse_floor_rack_DTO>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_warehouse_floor_rack_entity { gen_warehouse_floor_rack_id = Id };

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

