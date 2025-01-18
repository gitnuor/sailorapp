using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using Postgrest;

using System.Data;
using static Postgrest.Constants;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class PpMeetingService : IPpMeetingService
    {

        private readonly IConfiguration _configuration;
       
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICommonService _commonService;

        public PpMeetingService(IConfiguration configuration, IMapper mapper, IWebHostEnvironment webHostEnvironment, ICommonService commonService)
        {

            _mapper = mapper;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _commonService = commonService;
        }

       

        public async Task<bool> SaveAsync(tran_pp_meeting_DTO entity)
        {
            string uniqueFileName = null;
            var base64Parts = entity.imageBase64.Split(',');
            var imageData = Convert.FromBase64String(base64Parts[1]);

            uniqueFileName = Guid.NewGuid().ToString() + ".jpg"; // Or other file extension
            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UploadFolder/ppMeeting/");
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            string basePath = _webHostEnvironment.WebRootPath;
            string relativePath = filePath.Replace(basePath, "").TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);


            System.IO.File.WriteAllBytes(filePath, imageData);

            if (!string.IsNullOrEmpty(entity.imageBase64))
            {
                
                entity.imagePath = relativePath;
            }



            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_pp_meeting_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, entity.techpack_id == null ? DBNull.Value : entity.techpack_id);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, entity.remarks == null ? DBNull.Value : entity.remarks);
                        Command.Parameters.AddWithValue("in_meeting_conducted_by", NpgsqlDbType.Bigint, entity.meeting_conducted_by == null ? DBNull.Value : entity.meeting_conducted_by);
                        Command.Parameters.AddWithValue("in_meeting_conducted_date", NpgsqlDbType.Date, entity.meeting_conducted_date == null ? DBNull.Value : entity.meeting_conducted_date);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, entity.event_id == null ? DBNull.Value : entity.event_id);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, entity.fiscal_year_id == null ? DBNull.Value : entity.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, entity.added_by == null ? DBNull.Value : entity.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, entity.date_added == null ? DBNull.Value : entity.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, entity.updated_by == null ? DBNull.Value : entity.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, entity.date_updated == null ? DBNull.Value : entity.date_updated);
                        Command.Parameters.AddWithValue("in_imagepath", NpgsqlDbType.Text, entity.imagePath == null ? DBNull.Value : entity.imagePath);


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

        public async Task<bool> UpdateAsync(tran_pp_meeting_entity entity)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_pp_meeting_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;


                        Command.Parameters.AddWithValue("in_tran_pp_meeting_id", NpgsqlDbType.Bigint, entity.tran_pp_meeting_id == null ? DBNull.Value : entity.tran_pp_meeting_id);

                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, entity.techpack_id == null ? DBNull.Value : entity.techpack_id);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, entity.remarks == null ? DBNull.Value : entity.remarks);
                        Command.Parameters.AddWithValue("in_meeting_conducted_by", NpgsqlDbType.Bigint, entity.meeting_conducted_by == null ? DBNull.Value : entity.meeting_conducted_by);
                        Command.Parameters.AddWithValue("in_meeting_conducted_date", NpgsqlDbType.Date, entity.meeting_conducted_date == null ? DBNull.Value : entity.meeting_conducted_date);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, entity.event_id == null ? DBNull.Value : entity.event_id);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, entity.fiscal_year_id == null ? DBNull.Value : entity.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, entity.added_by == null ? DBNull.Value : entity.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, entity.date_added == null ? DBNull.Value : entity.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, entity.updated_by == null ? DBNull.Value : entity.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, entity.date_updated == null ? DBNull.Value : entity.date_updated);


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

        public async Task<List<tran_pp_meeting_DTO>> GetAllAsync()
        {
            List<tran_pp_meeting_entity> list = new List<tran_pp_meeting_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_pp_meeting_entity>().ToList();

                    //return dataList;
                   return JsonConvert.DeserializeObject<List<tran_pp_meeting_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        

        public async Task<List<tran_pp_meeting_DTO>> GetAllPagedDataForSelect2(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_pp_meeting m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.remarks ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_pp_meeting_DTO>(query,
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


        public async Task<tran_pp_meeting_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                   // string query = @"SELECT m.*  FROM tran_pp_meeting m   where m.tran_pp_meeting_id=@Id";
                    string query = $"select * from get_meeting_details(@p_meeting_id)";

                    var dataList = connection.Query<tran_pp_meeting_DTO>(query,
                        new { p_meeting_id = Id }).ToList().FirstOrDefault();


                    file_upload file = new file_upload();
                    string fileName = Path.GetFileName(dataList.imagePath);
                    string directoryPath = Path.GetDirectoryName(dataList.imagePath);
                    string folderName = Path.GetFileName(directoryPath);
                    file.server_filename = fileName;
                    await _commonService.LoadSingleFiles(file, folderName);
                    dataList.imageBase64 = file.base64string;

                    return dataList;
                    
                    
                   
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

                    var objToDelete = new tran_pp_meeting_entity { tran_pp_meeting_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

     

        public async Task<List<rpc_tran_pp_meeting_DTO>> GetAllJoined_TranPpMeetingAsync(long row_index, long page_size,long event_id,long fiscal_year_id)
        {



            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_pp_meeting( @row_index,@page_size, @p_fiscal_year_id, @p_event_id)";

                    var dataList = connection.Query<rpc_tran_pp_meeting_DTO>(query,
                          new
                          {
                              row_index = row_index,
                              page_size = page_size,
                              p_fiscal_year_id= fiscal_year_id,
                              p_event_id= event_id
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

        public string SaveBase64Image(string base64String)
        {
            var base64Parts = base64String.Split(',');
            var imageData = Convert.FromBase64String(base64Parts[1]);

            string uniqueFileName = Guid.NewGuid().ToString() + ".jpg"; // Or other file extension
            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UploadFolder/ppMeeting/");
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            System.IO.File.WriteAllBytes(filePath, imageData);

            return uniqueFileName;
        }

      

    }

}

