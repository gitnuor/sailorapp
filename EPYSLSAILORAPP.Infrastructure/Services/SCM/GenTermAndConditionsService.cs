using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using static Postgrest.Constants;
using AutoMapper;
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
using Npgsql;
using NpgsqlTypes;
using System.Data;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenTermAndConditionsService : IGenTermAndConditionsService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenTermAndConditionsService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync( gen_term_and_conditions_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_term_and_conditions_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_term_and_conditions_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_term_and_conditions_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_term_and_conditions_DTO>> GetAllAsync()
        {
            List<gen_term_and_conditions_entity> list = new List<gen_term_and_conditions_entity>();
            
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_term_and_conditions_entity>().ToList();
                    return JsonConvert.DeserializeObject<List<gen_term_and_conditions_DTO>>(JsonConvert.SerializeObject(dataList));

                   
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

         public async Task<List<gen_term_and_conditions_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {
               
                 using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_term_and_conditions m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.term_condition_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_term_and_conditions_entity>(query,
                        new { 
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

   

        public async Task<gen_term_and_conditions_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                 using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                      string query = @"SELECT m.*  FROM gen_term_and_conditions m   where m.gen_term_and_conditions_id=@Id";

                    var dataList = connection.Query<gen_term_and_conditions_entity>(query,
                        new {    @Id=Id  }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_term_and_conditions_entity>>(JsonConvert.SerializeObject(dataList));
                }
           }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        public async Task<List<gen_term_and_conditions_details_DTO>> GetTermsandConditionDetail(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM get_term_and_conditions_details(@Id);";

                    var dataList = connection.Query<gen_term_and_conditions_details_DTO>(query,
                        new { @Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_term_and_conditions_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_term_and_conditions_entity { gen_term_and_conditions_id = (long)Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

              public async Task<bool> gen_term_and_conditions_insert_sp(gen_term_and_conditions_DTO objgen_term_and_conditions)
                                            {
                                             using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                                                {
                                                connection.Open();
                                                
                                                using (var transaction = connection.BeginTransaction())
                                               {
                                                   try
                                                   {
                                                       var Command = new NpgsqlCommand("gen_term_and_conditions_insert", connection);
			   
                                                       Command.CommandType = CommandType.StoredProcedure;

				                                        		Command.Parameters.AddWithValue("in_term_condition_name",NpgsqlDbType.Text, objgen_term_and_conditions.term_condition_name==null? DBNull.Value:objgen_term_and_conditions.term_condition_name);
		Command.Parameters.AddWithValue("in_description",NpgsqlDbType.Text, objgen_term_and_conditions.description==null? DBNull.Value:objgen_term_and_conditions.description);
		Command.Parameters.AddWithValue("in_created_by",NpgsqlDbType.Bigint, objgen_term_and_conditions.created_by==null? DBNull.Value:objgen_term_and_conditions.created_by);
		Command.Parameters.AddWithValue("in_created_date",NpgsqlDbType.Date, objgen_term_and_conditions.created_date==null? DBNull.Value:objgen_term_and_conditions.created_date);
		Command.Parameters.AddWithValue("in_updated_by",NpgsqlDbType.Bigint, objgen_term_and_conditions.updated_by==null? DBNull.Value:objgen_term_and_conditions.updated_by);
		Command.Parameters.AddWithValue("in_updated_date",NpgsqlDbType.Date, objgen_term_and_conditions.updated_date==null? DBNull.Value:objgen_term_and_conditions.updated_date);
		Command.Parameters.AddWithValue("in_menu_id",NpgsqlDbType.Bigint, objgen_term_and_conditions.menu_id==null? DBNull.Value:objgen_term_and_conditions.menu_id);


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
      public async Task<bool> gen_term_and_conditions_update_sp(gen_term_and_conditions_DTO objgen_term_and_conditions)
                                     {
                                        using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                                        {
                                            connection.Open();

                                           using (var transaction = connection.BeginTransaction())
                                           {
                                               try
                                               {
                                                   var Command = new NpgsqlCommand("gen_term_and_conditions_update", connection);
			   
                                                   Command.CommandType = CommandType.StoredProcedure;

				                                    		Command.Parameters.AddWithValue("in_term_condition_name",NpgsqlDbType.Text, objgen_term_and_conditions.term_condition_name==null? DBNull.Value:objgen_term_and_conditions.term_condition_name);
		Command.Parameters.AddWithValue("in_description",NpgsqlDbType.Text, objgen_term_and_conditions.description==null? DBNull.Value:objgen_term_and_conditions.description);
		Command.Parameters.AddWithValue("in_created_by",NpgsqlDbType.Bigint, objgen_term_and_conditions.created_by==null? DBNull.Value:objgen_term_and_conditions.created_by);
		Command.Parameters.AddWithValue("in_created_date",NpgsqlDbType.Date, objgen_term_and_conditions.created_date==null? DBNull.Value:objgen_term_and_conditions.created_date);
		Command.Parameters.AddWithValue("in_updated_by",NpgsqlDbType.Bigint, objgen_term_and_conditions.updated_by==null? DBNull.Value:objgen_term_and_conditions.updated_by);
		Command.Parameters.AddWithValue("in_updated_date",NpgsqlDbType.Date, objgen_term_and_conditions.updated_date==null? DBNull.Value:objgen_term_and_conditions.updated_date);
		Command.Parameters.AddWithValue("in_menu_id",NpgsqlDbType.Bigint, objgen_term_and_conditions.menu_id==null? DBNull.Value:objgen_term_and_conditions.menu_id);


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
     public async Task<List<rpc_gen_term_and_conditions_DTO>> GetAllJoined_GenTermAndConditionsAsync(Int64 currnet_page,Int64 page_size)
            {
                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = $"SELECT * FROM proc_sp_get_data_gen_term_and_conditions( @currnet_page,@page_size)";

                        var dataList = connection.Query<rpc_gen_term_and_conditions_DTO>(query,
                              new {
                                  currnet_page = currnet_page,
                                  page_size =  page_size }
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

