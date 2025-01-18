
using AutoMapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenItemStockMasterService : IGenItemStockMasterService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenItemStockMasterService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync( gen_item_stock_master_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_item_stock_master_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_item_stock_master_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_item_stock_master_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_item_stock_master_DTO>> GetAllAsync()
        {
            List<gen_item_stock_master_entity> list = new List<gen_item_stock_master_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_item_stock_master_DTO>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_item_stock_master_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

         public async Task<List<gen_item_stock_master_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                                    , gm.item_name, gmud.unit_detail_title, tso.tran_sample_order_number,tpas.style_code
                                                    FROM gen_item_stock_master m
                                                             inner join gen_item_master gm on gm.gen_item_master_id = m.item_master_id
                                                             inner join gen_measurement_unit_detail gmud
                                                                        on gm.measurement_unit_detail_id = gmud.gen_measurement_unit_detail_id
                                                             left outer join tran_tech_pack ttp on ttp.tran_techpack_id = m.tran_techpack_id
                                                             left outer join tran_designer_review tdr on tdr.tran_designer_review_id = ttp.tran_designer_review_id
                                                             left outer join tran_pre_costing tpc on tpc.tran_pre_costing_id = tdr.tran_pre_costing_id
                                                             left outer join tran_sample_order tso on tso.tran_sample_order_id = tpc.tran_pre_costing_id

                                                             left outer join tran_plan_allocate_style tpas on tpas.tran_va_plan_detl_style_id = tso.tran_va_plan_detl_style_id
                                                    where case
                                                    when @search_text is null or length(@search_text)=0 then true
                                                    when @search_text is not null and length(@search_text)>0 and (
                                                            gm.item_name ilike '%' || @search_text || '%' or
                                                            tso.tran_sample_order_number ilike '%' || @search_text || '%' or
                                                            gmud.unit_detail_title ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_item_stock_master_entity>(query,
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

       
        
        public async Task<gen_item_stock_master_DTO> GetSingleAsync(Int64 Id, Int64? TechPackID)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    if (TechPackID.HasValue && TechPackID != 0)
                    {
                        string query = @"SELECT m.*,
                            (select coalesce( sum(d.booking_quantity),0)  from tran_fabric_allocation_req_det d
                            inner join tran_fabric_allocation_req m on m.tran_fabric_allocation_req_id=d.tran_fabric_allocation_req_id
                            where coalesce( is_approved,0)=0 and d.item_master_id= @id  and d.prev_tech_pack_id is null)  as total_unapproved_allocated_quantity
                            FROM gen_item_stock_master m
                            where m.item_master_id=@id and    m.tran_techpack_id=@tran_techpack_id";

                        var dataList = connection.Query<gen_item_stock_master_DTO>(query,
                            new
                            {
                                Id = Id,
                                tran_techpack_id = TechPackID.Value
                            }).ToList().FirstOrDefault();

                        return dataList;
                    }
                    else
                    {
                        string query = @"SELECT m.*,
                            (select coalesce( sum(d.booking_quantity),0)  from tran_fabric_allocation_req_det d
                            inner join tran_fabric_allocation_req m on m.tran_fabric_allocation_req_id=d.tran_fabric_allocation_req_id
                            where coalesce( is_approved,0)=0 and d.item_master_id= @id  and d.prev_tech_pack_id is null)  as total_unapproved_allocated_quantity
                              FROM gen_item_stock_master m
                              where m.item_master_id=@id and m.tran_techpack_id is null";

                        var dataList = connection.Query<gen_item_stock_master_DTO>(query,
                            new { Id = Id }).ToList().FirstOrDefault();

                        return dataList;
                    }
                    //JsonConvert.DeserializeObject<List<gen_item_stock_master_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_item_stock_master_entity { item_stock_master_id = Id.Value };

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

