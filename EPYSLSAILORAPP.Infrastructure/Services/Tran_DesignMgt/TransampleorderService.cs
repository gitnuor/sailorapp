using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.Entity;
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
using ServiceStack;
using System.Data;


namespace EPYSLSAILORAPP.Infrastructure.Services.Tran_DesignMgt
{

    public class TransampleorderService : ITransampleorderService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<TransampleorderService> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TransampleorderService(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IMapper mapper, ILogger<TransampleorderService> logger)
        {

            _mapper = mapper;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
          }


        public async Task<bool> SaveAsync(tran_sample_order_DTO entity)
        {
            try
            {
              
                foreach (var singleimage in entity.List_base64String_File)
                {

                    singleimage.server_filename = Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);

                    string base64String = singleimage.base64string;

                    byte[] byteArray = Convert.FromBase64String(base64String);

                    try
                    {

                        string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder"), "sample_order");

                        if (!Directory.Exists(filePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(filePath);
                        }

                        File.WriteAllBytes(Path.Combine(filePath, singleimage.server_filename), byteArray);

                    }
                    catch (Exception ex)
                    {

                    }

                    singleimage.base64string = "";

                }

                entity.pattern = JsonConvert.SerializeObject(entity.obj_pattern);
                entity.fit_name = JsonConvert.SerializeObject(entity.obj_fit_name);

                var model = _mapper.Map<tran_sample_order_entity>(entity);

                model.sample_photos = JArray.Parse(JsonConvert.SerializeObject(entity.List_base64String_File)).ToString();

                // var ret_master = await _connectionSupabse.From<tran_sample_order_entity>().Insert(model, new QueryOptions { Returning = ReturnType.Representation });

                foreach (var obj in entity.List_Detl)
                {
                    //obj.tran_sample_order_id = Convert.ToInt64(ret_master.Model.PrimaryKey.FirstOrDefault().Value);
                    obj.added_by = entity.added_by;
                    obj.date_added = entity.date_added;
                }

                foreach (var obj in entity.List_Embellishmet)
                {
                    //obj.tran_sample_order_id = Convert.ToInt64(ret_master.Model.PrimaryKey.FirstOrDefault().Value);
                    obj.added_by = entity.added_by;
                    obj.date_added = entity.date_added;
                }

                foreach (var obj in entity.List_SubGroup)
                {
                    //obj.tran_sample_order_id = Convert.ToInt64(ret_master.Model.PrimaryKey.FirstOrDefault().Value);
                    obj.added_by = entity.added_by;
                    obj.date_added = entity.date_added;
                }

                var List_Detl = _mapper.Map<List<tran_sample_order_detl_entity>>(entity.List_Detl);

                var List_Embellishment = _mapper.Map<List<tran_sample_order_embellishment_entity>>(entity.List_Embellishmet);

                var List_SubGroup = _mapper.Map<List<tran_sample_order_subgroup_entity>>(entity.List_SubGroup);

                entity.so_detl = JArray.Parse(JsonConvert.SerializeObject(List_Detl)).ToString();

                entity.embellishment_detl = JArray.Parse(JsonConvert.SerializeObject(List_Embellishment)).ToString();

                entity.subgroup_detl = JArray.Parse(JsonConvert.SerializeObject(List_SubGroup)).ToString();

                entity.sample_photos = JArray.Parse(JsonConvert.SerializeObject(entity.List_base64String_File)).ToString();

                return await tran_sample_order_insert_sp(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> UpdateAsync(tran_sample_order_DTO entity)
        {
            try
            {
               

                foreach (var singleimage in entity.List_base64String_File)
                {

                    if (singleimage.current_state == 1) //add
                    {
                        singleimage.server_filename = Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);

                        string base64String = singleimage.base64string;

                        byte[] byteArray = Convert.FromBase64String(base64String);

                        try
                        {

                            string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder"), "sample_order");

                            if (!Directory.Exists(filePath))
                            {
                                DirectoryInfo di = Directory.CreateDirectory(filePath);
                            }

                            File.WriteAllBytes(Path.Combine(filePath, singleimage.server_filename), byteArray);
                        }
                        catch (Exception es)
                        {

                        }

                        singleimage.base64string = "";
                    }
                    else if (singleimage.current_state == 3) //delete
                    {
                        //var response = await _connectionSupabse.Storage
                        //   .From("sailor_bucket") // Replace with your actual storage bucket name
                        //   .Remove("sample_order/" + singleimage.server_filename);
                    }

                }

                entity.pattern = JsonConvert.SerializeObject(entity.obj_pattern);
                entity.fit_name = JsonConvert.SerializeObject(entity.obj_fit_name);

                var model = _mapper.Map<tran_sample_order_entity>(entity);

                model.sample_photos = JArray.Parse(JsonConvert.SerializeObject(entity.List_base64String_File.Where(p => p.current_state != 3))).ToString();

                foreach (var obj in entity.List_Detl)
                {
                    //obj.tran_sample_order_id = model.tran_sample_order_id;
                    obj.added_by = entity.added_by;
                    obj.date_added = entity.date_added;

                    if (obj.current_state == 2)
                    {
                        obj.updated_by = entity.added_by;
                        obj.date_updated = entity.date_added;
                    }
                }

                foreach (var obj in entity.List_Embellishmet)
                {
                    //obj.tran_sample_order_id = model.tran_sample_order_id;
                    obj.added_by = entity.added_by;
                    obj.date_added = entity.date_added;
                }

                foreach (var obj in entity.List_SubGroup)
                {
                    //obj.tran_sample_order_id = model.tran_sample_order_id;
                    obj.added_by = entity.added_by;
                    obj.date_added = entity.date_added;
                }

                entity.so_detl = JArray.Parse(JsonConvert.SerializeObject(entity.List_Detl)).ToString();

                entity.embellishment_detl = JArray.Parse(JsonConvert.SerializeObject(entity.List_Embellishmet)).ToString();

                entity.subgroup_detl = JArray.Parse(JsonConvert.SerializeObject(entity.List_SubGroup)).ToString();

                entity.sample_photos = JArray.Parse(JsonConvert.SerializeObject(entity.List_base64String_File)).ToString();


                return await tran_sample_order_update_sp(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<tran_sample_order_entity>> GetAllAsync()
        {
            List<tran_sample_order_entity> list = new List<tran_sample_order_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_sample_order_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_sample_order_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_sample_order_entity>> GetAsync(long Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_sample_order m   where m.tran_sample_order_id=@Id";

                    var dataList = connection.Query<tran_sample_order_entity>(query,
                        new { @Id = Id }).ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_sample_order_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_sample_order_DTO> GetSingleWithImageByIdAsync(long Id,long? image_position=0)
        {
            List<file_upload> objfile_list = new List<file_upload>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_sample_order m   where m.tran_sample_order_id=@Id";

                    var ret = connection.Query<tran_sample_order_entity>(query,
                        new { @Id = Id }).FirstOrDefault();

                    var objReturn= JsonConvert.DeserializeObject<tran_sample_order_DTO>(JsonConvert.SerializeObject(ret));

                    if (!string.IsNullOrEmpty(ret.sample_photos))
                    {

                        var images =JsonConvert.DeserializeObject<List<file_upload>>( ret.sample_photos);

                        if (image_position > 0)
                        {
                            images = images.Where(p => p.imagetype == Image_Position_Type.Front.ToString()).ToList();
                        }

                        foreach (var file in images)
                        {
                            try
                            {

                                string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                                string filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/sample_order/" + file.server_filename;

                                if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                                {
                                    var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                                    string base64String = Convert.ToBase64String(bytes);

                                    var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                                   
                                    if (file_extension == "pdf")
                                    {
                                        file.base64string = $"data:application/{file_extension};base64,{base64String}";
                                    }
                                    else
                                    {
                                        file.base64string = $"data:image/{file_extension};base64,{base64String}";

                                    }
                                   

                                    file.current_state = 2;

                                    objfile_list.Add(file);
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

                    objReturn.List_base64String_File = objfile_list;

                    return objReturn; //JsonConvert.DeserializeObject<List<tran_sample_order_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
          
        }


        public async Task<bool> tran_sample_order_insert_sp(tran_sample_order_DTO objtran_sample_order)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_sample_order_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_va_plan_detl_id", NpgsqlDbType.Bigint, objtran_sample_order.tran_va_plan_detl_id == null ? DBNull.Value : objtran_sample_order.tran_va_plan_detl_id);
                        Command.Parameters.AddWithValue("in_tran_sample_order_number", NpgsqlDbType.Text, objtran_sample_order.tran_sample_order_number == null ? DBNull.Value : objtran_sample_order.tran_sample_order_number);
                        Command.Parameters.AddWithValue("in_order_date", NpgsqlDbType.Date, objtran_sample_order.order_date == null ? DBNull.Value : objtran_sample_order.order_date);
                        Command.Parameters.AddWithValue("in_delivery_date", NpgsqlDbType.Date, objtran_sample_order.delivery_date == null ? DBNull.Value : objtran_sample_order.delivery_date);
                        Command.Parameters.AddWithValue("in_delivery_unit_id", NpgsqlDbType.Bigint, objtran_sample_order.delivery_unit_id == null ? DBNull.Value : objtran_sample_order.delivery_unit_id);
                        Command.Parameters.AddWithValue("in_order_quantity", NpgsqlDbType.Bigint, objtran_sample_order.order_quantity == null ? DBNull.Value : objtran_sample_order.order_quantity);
                        Command.Parameters.AddWithValue("in_designer_member_id", NpgsqlDbType.Bigint, objtran_sample_order.designer_member_id == null ? DBNull.Value : objtran_sample_order.designer_member_id);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_sample_order.remarks == null ? DBNull.Value : objtran_sample_order.remarks);
                        Command.Parameters.AddWithValue("in_is_submit", NpgsqlDbType.Boolean, objtran_sample_order.is_submit == null ? DBNull.Value : objtran_sample_order.is_submit);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, objtran_sample_order.is_approved == null ? DBNull.Value : objtran_sample_order.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_sample_order.approved_by == null ? DBNull.Value : objtran_sample_order.approved_by);
                        Command.Parameters.AddWithValue("in_date_approved", NpgsqlDbType.Date, objtran_sample_order.date_approved == null ? DBNull.Value : objtran_sample_order.date_approved);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_sample_order.added_by == null ? DBNull.Value : objtran_sample_order.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_sample_order.date_added == null ? DBNull.Value : objtran_sample_order.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_sample_order.updated_by == null ? DBNull.Value : objtran_sample_order.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_sample_order.date_updated == null ? DBNull.Value : objtran_sample_order.date_updated);
                        Command.Parameters.AddWithValue("in_sample_photos", NpgsqlDbType.Text,string.IsNullOrEmpty (objtran_sample_order.sample_photos) ? DBNull.Value : objtran_sample_order.sample_photos);
                        Command.Parameters.AddWithValue("in_tran_va_plan_detl_style_id", NpgsqlDbType.Bigint, objtran_sample_order.tran_va_plan_detl_style_id == null ? DBNull.Value : objtran_sample_order.tran_va_plan_detl_style_id);
                        Command.Parameters.AddWithValue("in_fit_name", NpgsqlDbType.Text, objtran_sample_order.fit_name == null ? DBNull.Value : objtran_sample_order.fit_name);
                        Command.Parameters.AddWithValue("in_pattern", NpgsqlDbType.Text, objtran_sample_order.pattern == null ? DBNull.Value : objtran_sample_order.pattern);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_sample_order.fiscal_year_id == null ? DBNull.Value : objtran_sample_order.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_sample_order.event_id == null ? DBNull.Value : objtran_sample_order.event_id);
                        Command.Parameters.AddWithValue("in_so_detl", NpgsqlDbType.Text, string.IsNullOrEmpty(objtran_sample_order.so_detl)? DBNull.Value : objtran_sample_order.so_detl);
                        Command.Parameters.AddWithValue("in_embellishment_detl", NpgsqlDbType.Text, string.IsNullOrEmpty(objtran_sample_order.embellishment_detl) ? DBNull.Value : objtran_sample_order.embellishment_detl);
                        Command.Parameters.AddWithValue("in_subgroup_detl", NpgsqlDbType.Text, string.IsNullOrEmpty(objtran_sample_order.subgroup_detl) ? DBNull.Value : objtran_sample_order.subgroup_detl);


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
        public async Task<bool> tran_sample_order_update_sp(tran_sample_order_DTO objtran_sample_order)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_sample_order_update", connection);


                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("in_tran_sample_order_id", NpgsqlDbType.Bigint, objtran_sample_order.tran_sample_order_id);
                        Command.Parameters.AddWithValue("in_tran_va_plan_detl_id", NpgsqlDbType.Bigint, objtran_sample_order.tran_va_plan_detl_id == null ? DBNull.Value : objtran_sample_order.tran_va_plan_detl_id);
                        Command.Parameters.AddWithValue("in_tran_sample_order_number", NpgsqlDbType.Text, objtran_sample_order.tran_sample_order_number == null ? DBNull.Value : objtran_sample_order.tran_sample_order_number);
                        Command.Parameters.AddWithValue("in_order_date", NpgsqlDbType.Date, objtran_sample_order.order_date == null ? DBNull.Value : objtran_sample_order.order_date);
                        Command.Parameters.AddWithValue("in_delivery_date", NpgsqlDbType.Date, objtran_sample_order.delivery_date == null ? DBNull.Value : objtran_sample_order.delivery_date);
                        Command.Parameters.AddWithValue("in_delivery_unit_id", NpgsqlDbType.Bigint, objtran_sample_order.delivery_unit_id == null ? DBNull.Value : objtran_sample_order.delivery_unit_id);
                        Command.Parameters.AddWithValue("in_order_quantity", NpgsqlDbType.Bigint, objtran_sample_order.order_quantity == null ? DBNull.Value : objtran_sample_order.order_quantity);
                        Command.Parameters.AddWithValue("in_designer_member_id", NpgsqlDbType.Bigint, objtran_sample_order.designer_member_id == null ? DBNull.Value : objtran_sample_order.designer_member_id);
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_sample_order.remarks == null ? DBNull.Value : objtran_sample_order.remarks);
                        Command.Parameters.AddWithValue("in_is_submit", NpgsqlDbType.Boolean, objtran_sample_order.is_submit == null ? DBNull.Value : objtran_sample_order.is_submit);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Boolean, objtran_sample_order.is_approved == null ? DBNull.Value : objtran_sample_order.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_sample_order.approved_by == null ? DBNull.Value : objtran_sample_order.approved_by);
                        Command.Parameters.AddWithValue("in_date_approved", NpgsqlDbType.Date, objtran_sample_order.date_approved == null ? DBNull.Value : objtran_sample_order.date_approved);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_sample_order.added_by == null ? DBNull.Value : objtran_sample_order.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_sample_order.date_added == null ? DBNull.Value : objtran_sample_order.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_sample_order.updated_by == null ? DBNull.Value : objtran_sample_order.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_sample_order.date_updated == null ? DBNull.Value : objtran_sample_order.date_updated);
                        Command.Parameters.AddWithValue("in_sample_photos", NpgsqlDbType.Text, string.IsNullOrEmpty(objtran_sample_order.sample_photos) ? DBNull.Value : objtran_sample_order.sample_photos);
                        Command.Parameters.AddWithValue("in_tran_va_plan_detl_style_id", NpgsqlDbType.Bigint, objtran_sample_order.tran_va_plan_detl_style_id == null ? DBNull.Value : objtran_sample_order.tran_va_plan_detl_style_id);
                        Command.Parameters.AddWithValue("in_fit_name", NpgsqlDbType.Text, objtran_sample_order.fit_name == null ? DBNull.Value : objtran_sample_order.fit_name);
                        Command.Parameters.AddWithValue("in_pattern", NpgsqlDbType.Text, objtran_sample_order.pattern == null ? DBNull.Value : objtran_sample_order.pattern);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_sample_order.fiscal_year_id == null ? DBNull.Value : objtran_sample_order.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_sample_order.event_id == null ? DBNull.Value : objtran_sample_order.event_id);
                        Command.Parameters.AddWithValue("in_so_detl", NpgsqlDbType.Text, string.IsNullOrEmpty(objtran_sample_order.so_detl) ? DBNull.Value : objtran_sample_order.so_detl);
                        Command.Parameters.AddWithValue("in_embellishment_detl", NpgsqlDbType.Text, string.IsNullOrEmpty(objtran_sample_order.embellishment_detl) ? DBNull.Value : objtran_sample_order.embellishment_detl);
                        Command.Parameters.AddWithValue("in_subgroup_detl", NpgsqlDbType.Text, string.IsNullOrEmpty(objtran_sample_order.subgroup_detl) ? DBNull.Value : objtran_sample_order.subgroup_detl);

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


        

    }

}

