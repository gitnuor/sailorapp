
using AutoMapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.General;
using EPYSLSAILORAPP.Domain.Security;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using Supabase;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class MenuService : IMenuService
    {

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;
        IRedisService<Menu> _redisService;
        public MenuService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
 
            _redisService = new RedisService<Menu>(_configuration);
        }

        public async Task<bool> SaveAsync(menu_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<menu_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(menu_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<menu_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<menu_entity>> GetAllAsync()
        {
            List<menu_entity> list = new List<menu_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<menu_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<menu_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<menu_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM menu m
                                           where case
                                                     when @search_text is null then true
                                                     when @search_text is not null and (
                                                            m. ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<menu_entity>(query,
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

        public async Task<menu_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM menu m   where m.menu_id=@Id";

                    var dataList = connection.Query<menu_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<menu_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new menu_entity { menu_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        private void PopulateMenus(ref List<Menu> parentList, List<Menu> menuList)
        {
            foreach (var parentMenu in parentList)
            {
                parentMenu.Childs = menuList.FindAll(x => x.parent_id == parentMenu.parent_id).OrderBy(x => x.seq_no).ToList();

                //var subParents = parentMenu.Childs.FindAll(x => x.navigate_url.IsNullOrEmpty());
                var subParents = parentMenu.Childs;
                foreach (var item in subParents) item.Childs = PopulateChildMenu(menuList, item.menu_id);
            }
        }

        private List<Menu> PopulateChildMenu(List<Menu> menuList, Int64? parentId)
        {
            var childList = menuList.FindAll(x => x.parent_id == parentId && x.menu_id != x.parent_id).OrderBy(x => x.seq_no).ToList();
            //var subParents = childList;
            var subParents = childList.FindAll(x => string.IsNullOrEmpty(x.menu_caption));
            foreach (var childMenu in subParents) childMenu.Childs = PopulateChildMenu(menuList, childMenu.menu_id);

            return childList;
        }

        private void PopulateNestableMenus(ref List<NestableElement> parentList, List<NestableElement> menuList)
        {
            foreach (var parentMenu in parentList)
            {
                parentMenu.Children = menuList.FindAll(x => x.Id != parentMenu.ParentId && x.ParentId == parentMenu.Id).OrderBy(x => x.SeqNo).ToList();

                var subParents = parentMenu.Children.FindAll(x => string.IsNullOrEmpty(x.PageName));
                foreach (var item in subParents) item.Children = PopulateNestableChildMenu(menuList, item.Id);
            }
        }

        private List<NestableElement> PopulateNestableChildMenu(List<NestableElement> menuList, int parentId)
        {
            var childList = menuList.FindAll(x => x.ParentId == parentId).OrderBy(x => x.SeqNo).ToList();

            var subParents = childList.FindAll(x => string.IsNullOrEmpty(x.PageName));
            foreach (var childMenu in subParents) childMenu.Children = PopulateNestableChildMenu(menuList, childMenu.Id);

            return childList;
        }
        public async Task<List<Menu>> GetMenusAsync(int userId, int applicationId, int companyId)
        {
            try
            {
                List<Menu> data_Parent = new List<Menu>();
                List<Menu> data_Child = new List<Menu>();

                var finalParentList = new List<Menu>();

                if (_redisService.IsKeyExists("menu_data_parent_" + Convert.ToString(userId)))
                {
                    data_Parent = _redisService.GetDataFromRedisCache("menu_data_parent_" + Convert.ToString(userId));
                    data_Child = _redisService.GetDataFromRedisCache("menu_data_child_" + Convert.ToString(userId));
                }
                else
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = $"SELECT * FROM sp_get_menu_list_for_api_module(@userid)";

                        data_Child = connection.Query<Menu>(query,
                              new { userid = userId }
                             ).ToList();

                    }

                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        string query = $"SELECT * FROM sp_get_parent_menu_list_for_api_module()";

                        data_Parent = connection.Query<Menu>(query).ToList();

                    }

                    _redisService.SaveDataInRedisCache("menu_data_parent_" + Convert.ToString(userId), data_Parent);
                    _redisService.SaveDataInRedisCache("menu_data_child_" + Convert.ToString(userId), data_Child);
                }

                PopulateMenus(ref data_Parent, data_Child);
                return data_Parent;
            }
            catch (Exception ex)
            {
                //_connection.Close();
                throw (ex);
            }
            finally
            {
                //_connection.Close();
            }
        }

    }

}

