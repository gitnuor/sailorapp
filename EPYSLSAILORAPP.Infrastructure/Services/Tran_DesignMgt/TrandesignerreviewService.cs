using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using Serilog.Context;
using System.Data;
using static Dapper.SqlMapper;


namespace EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt
{

    public class TranDesignerReviewService : ITranDesignerReviewService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<TranDesignerReviewService> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TranDesignerReviewService(IConfiguration configuration, IMapper mapper, IHostingEnvironment hostingEnvironment, ILogger<TranDesignerReviewService> logger)
        {

            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SaveAsync(tran_designer_review_entity entity, List<file_upload> file_upload)
        {
            try
            {

                List<file_upload> files = new List<file_upload>();
                foreach (var singleimage in file_upload)
                {
                    file_upload file = new file_upload();
                    file.filetype = singleimage.extension;
                    file.server_filename = System.Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);
                    file.filename = singleimage.filename;
                    file.extension = Path.GetExtension(singleimage.filename);
                    byte[] byteArray = Convert.FromBase64String(singleimage.base64string);


                    try
                    {

                        string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                        file.filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/designer_review/" + file.server_filename;

                        if (!Directory.Exists(file.filePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(file.filePath);
                        }

                        File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath), byteArray);
                        files.Add(file);
                    }
                    catch (Exception ex)
                    {

                    }


                }
                entity.document = JArray.Parse(JsonConvert.SerializeObject(files.ToList())).ToString();

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var Command = new NpgsqlCommand("tran_designer_review_insert", connection);

                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.AddWithValue("in_tran_pre_costing_id", NpgsqlDbType.Bigint, entity.tran_pre_costing_id == null ? DBNull.Value : entity.tran_pre_costing_id);
                            Command.Parameters.AddWithValue("in_no_review", NpgsqlDbType.Bigint, entity.no_review == null ? DBNull.Value : entity.no_review);
                            Command.Parameters.AddWithValue("in_no_confirmation", NpgsqlDbType.Bigint, entity.no_confirmation == null ? DBNull.Value : entity.no_confirmation);
                            Command.Parameters.AddWithValue("in_no_confirmation_with_modification", NpgsqlDbType.Bigint, entity.no_confirmation_with_modification == null ? DBNull.Value : entity.no_confirmation_with_modification);
                            Command.Parameters.AddWithValue("in_no_of_not_confirmed", NpgsqlDbType.Bigint, entity.no_of_not_confirmed == null ? DBNull.Value : entity.no_of_not_confirmed);
                            Command.Parameters.AddWithValue("in_status", NpgsqlDbType.Bigint, entity.status == null ? DBNull.Value : entity.status);
                            Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, entity.remarks == null ? DBNull.Value : entity.remarks);
                            Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, entity.is_submitted == null ? DBNull.Value : entity.is_submitted);
                            Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, entity.is_approved == null ? DBNull.Value : entity.is_approved);
                            Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, entity.approved_by == null ? DBNull.Value : entity.approved_by);
                            Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, entity.approve_date == null ? DBNull.Value : entity.approve_date);
                            Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, entity.approve_remarks == null ? DBNull.Value : entity.approve_remarks);
                            Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, entity.added_by == null ? DBNull.Value : entity.added_by);
                            Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, entity.updated_by == null ? DBNull.Value : entity.updated_by);
                            Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, entity.date_added == null ? DBNull.Value : entity.date_added);
                            Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, entity.date_updated == null ? DBNull.Value : entity.date_updated);
                            Command.Parameters.AddWithValue("in_document", NpgsqlDbType.Text, entity.document == null ? DBNull.Value : entity.document);
                            Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, entity.fiscal_year_id == null ? DBNull.Value : entity.fiscal_year_id);
                            Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, entity.event_id == null ? DBNull.Value : entity.event_id);


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
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(tran_designer_review_entity entity, List<file_upload> file_upload, List<string> DeleteList)
        {
            try
            {

                List<file_upload> files = new List<file_upload>();
                foreach (var singleimage in file_upload)
                {
                    file_upload file = new file_upload();
                    file.filetype = singleimage.extension;
                    file.server_filename = System.Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);
                    file.filename = singleimage.filename;
                    file.extension = Path.GetExtension(singleimage.filename);
                    byte[] byteArray = Convert.FromBase64String(singleimage.base64string);


                    try
                    {

                        string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                        file.filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/designer_review/" + file.server_filename;

                        if (!Directory.Exists(file.filePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(file.filePath);
                        }

                        File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath), byteArray);
                        files.Add(file);
                    }
                    catch (Exception ex)
                    {

                    }


                }
                entity.document = JArray.Parse(JsonConvert.SerializeObject(files.ToList())).ToString();
                
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var Command = new NpgsqlCommand("tran_designer_review_update", connection);

                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.AddWithValue("in_tran_designer_review_id", NpgsqlDbType.Bigint, entity.tran_designer_review_id == null ? DBNull.Value : entity.tran_designer_review_id);
                            Command.Parameters.AddWithValue("in_tran_pre_costing_id", NpgsqlDbType.Bigint, entity.tran_pre_costing_id == null ? DBNull.Value : entity.tran_pre_costing_id);
                            Command.Parameters.AddWithValue("in_no_review", NpgsqlDbType.Bigint, entity.no_review == null ? DBNull.Value : entity.no_review);
                            Command.Parameters.AddWithValue("in_no_confirmation", NpgsqlDbType.Bigint, entity.no_confirmation == null ? DBNull.Value : entity.no_confirmation);
                            Command.Parameters.AddWithValue("in_no_confirmation_with_modification", NpgsqlDbType.Bigint, entity.no_confirmation_with_modification == null ? DBNull.Value : entity.no_confirmation_with_modification);
                            Command.Parameters.AddWithValue("in_no_of_not_confirmed", NpgsqlDbType.Bigint, entity.no_of_not_confirmed == null ? DBNull.Value : entity.no_of_not_confirmed);
                            Command.Parameters.AddWithValue("in_status", NpgsqlDbType.Bigint, entity.status == null ? DBNull.Value : entity.status);
                            Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, entity.remarks == null ? DBNull.Value : entity.remarks);
                            Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Boolean, entity.is_submitted == null ? DBNull.Value : entity.is_submitted);
                            Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, entity.is_approved == null ? DBNull.Value : entity.is_approved);
                            Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, entity.approved_by == null ? DBNull.Value : entity.approved_by);
                            Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, entity.approve_date == null ? DBNull.Value : entity.approve_date);
                            Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, entity.approve_remarks == null ? DBNull.Value : entity.approve_remarks);
                            Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, entity.added_by == null ? DBNull.Value : entity.added_by);
                            Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, entity.updated_by == null ? DBNull.Value : entity.updated_by);
                            Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, entity.date_added == null ? DBNull.Value : entity.date_added);
                            Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, entity.date_updated == null ? DBNull.Value : entity.date_updated);
                            Command.Parameters.AddWithValue("in_document", NpgsqlDbType.Text, entity.document == null ? DBNull.Value : entity.document);
                            Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, entity.fiscal_year_id == null ? DBNull.Value : entity.fiscal_year_id);
                            Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, entity.event_id == null ? DBNull.Value : entity.event_id);


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
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<bool> ApproveAsync(tran_designer_review_entity entity)
        {
            try
            {
                
                var objEntity = JsonConvert.DeserializeObject<tran_designer_review_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    string query = @"update tran_designer_review 
                                    set approved_by=@approved_by,
                                        approve_date=now(),
                                        approve_remarks=@approve_remarks,
                                        is_approved=@is_approved
                                    where tran_designer_review_id=@tran_designer_review_id";

                    var dataList = connection.Execute(query,
                        new
                        {
                            approved_by = entity.approved_by,
                            approve_remarks = entity.approve_remarks,
                            is_approved = entity.is_approved,
                            tran_designer_review_id = entity.tran_designer_review_id
                        });
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<tran_designer_review_entity>> GetAllAsync()
        {
            try
            {
                List<tran_designer_review_entity> list = new List<tran_designer_review_entity>();

                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                    {
                        connection.Open();

                        var dataList = connection.GetAll<tran_designer_review_entity>().ToList();

                        return dataList;//JsonConvert.DeserializeObject<List<tran_designer_review_DTO>>(JsonConvert.SerializeObject(dataList));
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<tran_designer_review_entity> GetAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_designer_review m   where m.tran_designer_review_id=@Id";

                    var dataList = connection.Query<tran_designer_review_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_designer_review_entity>>(JsonConvert.SerializeObject(dataList));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<tran_designer_review_DTO> GetFullAsyncDesinerData(Int64 Id)
        {
            try
            {

                List<file_upload> objfile_list = new List<file_upload>();
                List<file_upload> objpdf_list = new List<file_upload>();
                
                var response = await GetAsync(Id);

                var ret = JsonConvert.DeserializeObject<tran_designer_review_DTO>(JsonConvert.SerializeObject( response));

                var list_photos = JsonConvert.DeserializeObject<List<file_upload>>(response.document);

                if (list_photos.Count > 0)
                {
                    foreach (var file in list_photos)
                    {
                        try
                        {
                           
                            if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath)))
                            {
                                var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, file.filePath));

                                file.base64string = Convert.ToBase64String(bytes);
                                
                                objpdf_list.Add(file);
                            }
                        }
                        catch (Exception ex)
                        {

                            using (LogContext.PushProperty("SpecialLogType", true))
                            {
                                _logger.LogError(ex.ToString());
                            }
                        }

                    }
                }

                ret.List_base64String_File = objfile_list;
                ret.List_Files = objpdf_list;

                return ret;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_data_for_designer_review_DTO>> GetAllsp_get_data_for_designer_reviewAsync
            (Filter_RangePlan_DataTable param)
        {
            try
            {
                var searchtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_designer_review_pending( @event_id_filter,@fiscal_year_id_filter,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@search_text)";

                    var dataList = connection.Query<rpc_sp_get_data_for_designer_review_DTO>(query,
                          new
                          {
                              event_id_filter = param.event_id,
                              fiscal_year_id_filter = param.fiscal_year_id,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              row_index = param.Start,
                              page_size = param.Length,
                              search_text = searchtext
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sp_get_data_for_designer_review_DTO>> GetAllsp_get_data_for_designer_review_dataAsync
            (Filter_RangePlan_DataTable param, Int64 actionType)
        {
            try
            {

                var searchtext = !string.IsNullOrEmpty(param.Search.Value) ? param.Search.Value : null;

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM sp_get_data_for_designer_review_data( @event_id_filter,@fiscal_year_id_filter,@item_type_id_filter,@product_type_id_filter,@gender_id_filter,@origin_id_filter,@master_category_id_filter,@row_index,@page_size,@actionType,@search_text)";

                    var dataList = connection.Query<rpc_sp_get_data_for_designer_review_DTO>(query,
                          new
                          {
                              event_id_filter = param.event_id,
                              fiscal_year_id_filter = param.fiscal_year_id,
                              item_type_id_filter = param.style_item_type_id,
                              product_type_id_filter = param.style_product_type_id,
                              gender_id_filter = param.style_gender_id,
                              origin_id_filter = param.style_item_origin_id,
                              master_category_id_filter = param.style_master_category_id,
                              row_index = param.Start,
                              page_size = param.Length,
                              actionType = actionType,
                              search_text = searchtext
                          }
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

