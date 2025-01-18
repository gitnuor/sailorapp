
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using static Dapper.SqlMapper;


namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class OwinRoleService : IOwinRoleService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public OwinRoleService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
           }

        public async Task<bool> SaveAsync(owin_role_DTO entity)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<owin_role_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var id= connection.Insert(model);

                    foreach (var obj in entity.Role_Permission_List)
                    {
                        obj.role_id = id;
                        obj.added_by = entity.added_by;
                        obj.date_added = DateTime.Now;
                    }

                    var actionlist = JsonConvert.DeserializeObject<List<owin_role_permission_entity>>
                        (JsonConvert.SerializeObject(entity.Role_Permission_List));

                    if (actionlist.Count > 0)
                    {
                        connection.Insert(actionlist);
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(owin_role_DTO entity)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<owin_role_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                     connection.Update(model);

                    var objToDelete = new owin_role_permission_entity { role_id = entity.owin_role_id };

                    bool deleted = connection.Delete(objToDelete);

                    foreach (var obj in entity.Role_Permission_List)
                    {
                        obj.role_id = entity.owin_role_id;
                        obj.added_by = entity.added_by;
                        obj.date_added = DateTime.Now;
                    }

                    var actionlist = JsonConvert.DeserializeObject<List<owin_role_permission_entity>>
                        (JsonConvert.SerializeObject(entity.Role_Permission_List));

                    if (actionlist.Count > 0)
                    {
                        connection.Insert(actionlist);
                    }

                }
               

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<owin_role_DTO>> GetAllAsync()
        {
            List<owin_role_entity> list = new List<owin_role_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<owin_role_entity>().ToList();

                    return JsonConvert.DeserializeObject<List<owin_role_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }



        public async Task<owin_role_DTO> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM owin_role m   where m.owin_role_id=@Id";

                    var dataList = connection.Query<owin_role_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return JsonConvert.DeserializeObject<owin_role_DTO>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new owin_role_entity { owin_role_id = Id };

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

