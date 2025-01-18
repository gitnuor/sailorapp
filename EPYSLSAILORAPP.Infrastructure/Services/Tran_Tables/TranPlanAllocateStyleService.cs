
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using EPYSLSAILORAPP.Domain.Statics;
using static Dapper.SqlMapper;
using Newtonsoft.Json;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using static Postgrest.QueryOptions;
using Postgrest;
using Microsoft.Extensions.Logging;
using Npgsql;
using Dapper.Contrib.Extensions;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using NUnit.Framework.Internal.Execution;
using System.Collections.Generic;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranPlanAllocateStyleService : ITranPlanAllocateStyleService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public TranPlanAllocateStyleService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;

        }

        public async Task<bool> SaveAsync(tran_plan_allocate_style_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_plan_allocate_style_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_plan_allocate_style_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_plan_allocate_style_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> DesignerAssignListAsync(List<tran_plan_allocate_style_entity> list)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {

                    try
                    {
                        foreach (var obj in list)
                        {
                            if (obj != null)
                            {
                                string query = @"update tran_plan_allocate_style set designer_member_id=@designer_member_id  where tran_va_plan_detl_style_id=@tran_va_plan_detl_style_id";

                                var dataList = connection.Execute(query,
                                            new
                                            {
                                                designer_member_id = obj.designer_member_id,
                                                tran_va_plan_detl_style_id = obj.tran_va_plan_detl_style_id
                                            }
                                    );
                            }
                        }

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

            return true;

        }

        public async Task<List<tran_plan_allocate_style_entity>> GetAllAsync()
        {
            List<tran_plan_allocate_style_entity> list = new List<tran_plan_allocate_style_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_plan_allocate_style_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_plan_allocate_style_DTO>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_plan_allocate_style_entity { tran_va_plan_detl_style_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<List<tran_plan_allocate_style_entity>> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_plan_allocate_style m   where m.tran_va_plan_detl_style_id=@Id";

                    var dataList = connection.Query<tran_plan_allocate_style_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_plan_allocate_style_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<List<tran_plan_allocate_style_entity>> GetStyleListByPlanDetlID(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_plan_allocate_style m   where m.tran_va_plan_detl_id=@Id";

                    var dataList = connection.Query<tran_plan_allocate_style_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_plan_allocate_style_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_plan_allocate_style_entity> GetTotalAddedStyle(Int64 tran_va_plan_detl_id, Int64? tran_va_plan_detl_style_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_plan_allocate_style m   where m.tran_va_plan_detl_id=@tran_va_plan_detl_id and 
                                m.tran_va_plan_detl_style_id!=@tran_va_plan_detl_style_id";

                    var dataList = connection.Query<tran_plan_allocate_style_entity>(query,
                        new
                        {
                            tran_va_plan_detl_id = tran_va_plan_detl_id,
                            tran_va_plan_detl_style_id = tran_va_plan_detl_style_id
                        }).ToList();

                    return new tran_plan_allocate_style_entity
                    {
                        style_quantity = dataList.Sum(p => p.style_quantity).Value,
                        total_rows = dataList.Count
                    };//JsonConvert.DeserializeObject<List<tran_plan_allocate_style_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_plan_allocate_style_approve_reject(tran_plan_allocate_style_DTO objtran_plan_allocate_style)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var yearCommand = new NpgsqlCommand("tran_plan_allocate_style_approve_reject", connection);

                            yearCommand.CommandType = CommandType.StoredProcedure;

                            yearCommand.Parameters.AddWithValue("in_tran_va_plan_detl_style_id", NpgsqlDbType.Bigint, objtran_plan_allocate_style.tran_va_plan_detl_style_id);
                            yearCommand.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_plan_allocate_style.is_submitted);
                            yearCommand.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_plan_allocate_style.is_approved);
                            yearCommand.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_plan_allocate_style.approved_by);
                            yearCommand.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_plan_allocate_style.approve_date);
                            yearCommand.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_plan_allocate_style.approve_remarks);

                            yearCommand.ExecuteNonQuery();

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

    }

}

