using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
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

    public class TranPlanAllocateService : ITranPlanAllocateService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranPlanAllocateService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }
       
        public async Task<bool> SaveAsync(tran_plan_allocate_DTO entity)
        {
            try
            {
                PrepareJArrayObject(entity);

                var objVAPlan = _mapper.Map<tran_plan_allocate_entity>(entity);

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var vaCommand = new NpgsqlCommand("tran_planning_and_allocation_insert", connection);
                            vaCommand.CommandType = CommandType.StoredProcedure;

                            vaCommand.Parameters.AddWithValue("p_tran_va_plan_detl_id", objVAPlan.tran_va_plan_detl_id==null?DBNull.Value: objVAPlan.tran_va_plan_detl_id);
                            vaCommand.Parameters.AddWithValue("p_range_plan_detail_id", objVAPlan.range_plan_detail_id);
                            vaCommand.Parameters.AddWithValue("p_style_item_product_id", objVAPlan.style_item_product_id);
                            vaCommand.Parameters.AddWithValue("p_no_of_style", objVAPlan.no_of_style);
                            vaCommand.Parameters.AddWithValue("p_fiscal_year_id", objVAPlan.fiscal_year_id);
                            vaCommand.Parameters.AddWithValue("p_event_id", objVAPlan.event_id);
                           
                            vaCommand.Parameters.AddWithValue("p_added_by", objVAPlan.added_by);
                           
                            vaCommand.Parameters.AddWithValue("p_style_code_gen", objVAPlan.style_code_gen);
                            vaCommand.Parameters.AddWithValue("p_style_details", objVAPlan.style_details.ToString());

                            vaCommand.Parameters[7].NpgsqlDbType = NpgsqlDbType.Text;
                            vaCommand.Parameters[8].NpgsqlDbType = NpgsqlDbType.Text;

                            vaCommand.ExecuteNonQuery();

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            // Handle exception, log, and optionally rollback the transaction
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

        }//style_color_quantity

        private static void PrepareJArrayObject(tran_plan_allocate_DTO entity)
        {

            foreach (var single_va_style in entity.List_style)
            {
                foreach (var single_va_color in single_va_style.List_ColorInfo)
                {
                    single_va_color.style_color_size_details = JArray.Parse(JsonConvert.SerializeObject(single_va_color.List_ColorSizeInfo));
                    single_va_color.style_color_quantity = single_va_color.List_ColorSizeInfo.Sum(p => p.style_color_size_quantity);
                }

                single_va_style.style_color_details = JArray.Parse(JsonConvert.SerializeObject(single_va_style.List_ColorInfo));
                single_va_style.style_embellishment_ids = JArray.Parse(JsonConvert.SerializeObject(single_va_style.List_Embellishment));
            }

            entity.style_details = JArray.Parse(JsonConvert.SerializeObject(entity.List_style));
            entity.no_of_style = entity.List_style.Count;

        }

      
        public async Task<bool> UpdateAsync(tran_plan_allocate_DTO entity)
        {
            try
            {
                PrepareJArrayObject(entity);

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Insert VA Plan
                            var vaCommand = new NpgsqlCommand("tran_planning_and_allocation_update", connection);
                            vaCommand.CommandType = CommandType.StoredProcedure;
                            
                            vaCommand.Parameters.AddWithValue("p_tran_va_plan_detl_id", entity.tran_va_plan_detl_id);
                            vaCommand.Parameters.AddWithValue("p_range_plan_detail_id", entity.range_plan_detail_id);
                            vaCommand.Parameters.AddWithValue("p_style_item_product_id", entity.style_item_product_id);
                            vaCommand.Parameters.AddWithValue("p_no_of_style", entity.no_of_style);
                            vaCommand.Parameters.AddWithValue("p_fiscal_year_id", entity.fiscal_year_id);
                            vaCommand.Parameters.AddWithValue("p_event_id", entity.event_id);

                            vaCommand.Parameters.AddWithValue("p_added_by", entity.added_by);

                            vaCommand.Parameters.AddWithValue("p_style_code_gen", entity.style_code_gen);
                            vaCommand.Parameters.AddWithValue("p_style_details", entity.style_details.ToString());


                            vaCommand.ExecuteNonQuery();

                            // Commit the transaction if all steps are successful
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {

                            transaction.Rollback();
                            throw (ex);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }//style_color_quantity


        public async Task<List<tran_plan_allocate_entity>> GetAllAsync()
        {
            List<tran_plan_allocate_entity> list = new List<tran_plan_allocate_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_plan_allocate_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_plan_allocate_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_plan_allocate_entity>> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_plan_allocate m   where m.tran_va_plan_detl_id=@Id";

                    var dataList = connection.Query<tran_plan_allocate_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_plan_allocate_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }

}

