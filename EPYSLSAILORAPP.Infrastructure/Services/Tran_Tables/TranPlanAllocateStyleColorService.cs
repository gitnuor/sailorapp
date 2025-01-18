
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;

using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Domain.DTO;
using Npgsql;
using Dapper.Contrib.Extensions;
using Azure;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranPlanAllocateStyleColorService : ITranPlanAllocateStyleColorService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranPlanAllocateStyleColorService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_plan_allocate_style_color_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_plan_allocate_style_color_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_plan_allocate_style_color_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_plan_allocate_style_color_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_plan_allocate_style_color_entity>> GetAllAsync()
        {
            List<tran_plan_allocate_style_color_entity> list = new List<tran_plan_allocate_style_color_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_plan_allocate_style_color_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_plan_allocate_style_color_DTO>> GetAsync(Int64 Id)
        {

            try
            {
                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = @"SELECT m.*,
                                      (select jsonb_agg(
                                               jsonb_build_object(
                                                 'tran_va_plan_detl_style_color_size_id', tpascs.tran_va_plan_detl_style_color_size_id,
                                                 'tran_va_plan_detl_style_color_id', tpascs.tran_va_plan_detl_style_color_id,
                                                 'style_product_size_group_detid', tpascs.style_product_size_group_detid,
                                                 'style_color_size_quantity', tpascs.style_color_size_quantity
                                                          )
                                        )
                                      from tran_plan_allocate_style_color_size tpascs
                                      where tpascs.tran_va_plan_detl_style_color_id = m.tran_va_plan_detl_style_color_id) as det_list
                                                FROM tran_plan_allocate_style_color m
                                               where m.tran_va_plan_detl_style_color_id =@Id";

                        var objdata = connection.Query<tran_plan_allocate_style_color_DTO>(query,
                            new { Id = Id }).ToList();


                        foreach (var obj in objdata)
                        {

                            obj.List_ColorSizeInfo = JsonConvert.DeserializeObject<List<tran_plan_allocate_style_color_size_DTO>>(JsonConvert.SerializeObject(obj.tran_color_size_detl_json));
                        }



                        return objdata;
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }





        }










    }

}

