using AutoMapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Security;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Infrastructure.Services.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MimeMapping.KnownMimeTypes;

namespace EPYSLSAILORAPP.Infrastructure.Helper
{
    public class CommonService : ICommonService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        public CommonService(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {

            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public async Task<List<file_upload>> UploadFiles(List<file_upload> files, string foldername)
        {
            long po_id = 0;
            List<file_upload> doc_files = new List<file_upload>();

            foreach (var singleimage in files)
            {
                file_upload file = new file_upload();
                file.filetype = singleimage.extension;
                file.server_filename = System.Guid.NewGuid().ToString().Replace("-", "_") + Path.GetExtension(singleimage.filename);
                file.filename = singleimage.filename;
                file.extension = Path.GetExtension(singleimage.filename);
                file.filePath = foldername;
                byte[] byteArray = Convert.FromBase64String(singleimage.base64string);

                try
                {
                    string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                    var filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/" + foldername + "/" + file.server_filename;

                    if (!Directory.Exists(filePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(filePath);
                    }

                    File.WriteAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath), byteArray);

                    doc_files.Add(file);
                }
                catch (Exception ex)
                {

                }


            }

            return doc_files;
        }

        public async Task Delete_Files(List<string> DeleteFiles, List<file_upload>? file_list, string foldername)
        {
            foreach (var file_new in file_list)
            {

                foreach (string SName in DeleteFiles)
                {
                    if (SName == file_new.server_filename)
                    {

                        try
                        {
                            string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                            var filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/" + file_new.filePath + "/" + file_new.server_filename;

                            if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                            {
                                File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, filePath));
                            }

                        }
                        catch (Exception ex)
                        {

                        }

                    }

                }

            }
        }

        public async Task<List<file_upload>> LoadAllFiles(List<file_upload>? file_list, string foldername)
        {
            List<file_upload> objpdf_list = new List<file_upload>();

            foreach (var file in file_list)
            {
                try
                {
                    string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                    var filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/" + (!string.IsNullOrEmpty(file.filePath) ? file.filePath : foldername) + "/" + file.server_filename;


                    if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                    {
                        var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                        var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                        var base64String = Convert.ToBase64String(bytes);

                        if (file_extension == "pdf")
                        {
                            file.base64string = base64String;
                        }
                        else
                        {
                            file.base64string = $"data:image/{file_extension};base64,{base64String}";

                        }

                        objpdf_list.Add(file);
                    }
                }
                catch (Exception ex)
                {

                    using (LogContext.PushProperty("SpecialLogType", true))
                    {
                        //_logger.LogError(ex.ToString());
                    }
                }
            }

            return objpdf_list;
        }


        public async Task<file_upload> LoadSingleFiles(file_upload? file, string foldername)
        {

            try
            {
                string folderPath = _configuration.GetValue<string>("UploadFolderPath");

                var filePath = (!string.IsNullOrEmpty(folderPath) ? folderPath : "UploadFolder") + "/" + (!string.IsNullOrEmpty(file.filePath) ? file.filePath : foldername) + "/" + file.server_filename;

                if (File.Exists(Path.Combine(_hostingEnvironment.WebRootPath, filePath)))
                {
                    var bytes = File.ReadAllBytes(Path.Combine(_hostingEnvironment.WebRootPath, filePath));

                    var base64String = Convert.ToBase64String(bytes);

                    var file_extension = Path.GetExtension(file.server_filename).Trim('.');

                    if (file_extension == "pdf")
                    {
                        file.base64string = base64String;
                    }
                    else
                    {
                        file.base64string = $"data:image/{file_extension};base64,{base64String}";

                    }


                }
            }
            catch (Exception ex)
            {

            }


            return file;
        }

        public async Task<List<owin_user_DTO>> LoadAllEmployeePic(List<owin_user_DTO>? user_List)
        {

            foreach (var file in user_List)
            {
                try
                {
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", "emp_pic") + "/" + file.emp_pic;

                    if (File.Exists(Path.Combine(filePath)))
                    {
                        var bytes = File.ReadAllBytes(filePath);

                        var base64string = Convert.ToBase64String(bytes);

                        var file_extension = Path.GetExtension(file.emp_pic).Trim('.');

                        var image_source = $"data:image/{file_extension};base64,{base64string}";

                        file.new_emp_pic = @"<span style='text-align:center;display:block'><img style='display: inline-block;height:80px;width:90px;' src=" + image_source + " /><br/> " + file.name + "(" + file.employee_code.ToString() + ")<br/>" + file.designation_name + "</span>";

                    }
                }
                catch (Exception ex)
                {

                    using (LogContext.PushProperty("SpecialLogType", true))
                    {
                        //_logger.LogError(ex.ToString());
                    }
                }
            }

            return user_List;
        }

        public async Task<owin_user_DTO> LoadAllEmployeePic(owin_user_DTO? user_List)
        {

            try
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", "emp_pic") + "/" + user_List.emp_pic;

                if (File.Exists(Path.Combine(filePath)))
                {
                    var bytes = File.ReadAllBytes(filePath);

                    var base64string = Convert.ToBase64String(bytes);

                    var file_extension = Path.GetExtension(user_List.emp_pic).Trim('.');

                    var image_source = $"data:image/{file_extension};base64,{base64string}";

                    user_List.new_emp_pic = @"<span style='text-align:center;display:block'><img style='display: inline-block;height:80px;width:90px;' src=" + image_source + " /><br/> " + user_List.name + "(" + user_List.employee_code.ToString() + ")<br/>" + user_List.designation_name + "</span>";

                }
            }
            catch (Exception ex)
            {

                using (LogContext.PushProperty("SpecialLogType", true))
                {
                    //_logger.LogError(ex.ToString());
                }
            }


            return user_List;
        }

    }

}
