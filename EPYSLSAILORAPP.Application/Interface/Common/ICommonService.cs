using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using System.Linq.Expressions;

namespace EPYSLSAILORAPP.Application.Interface.Common
{
    public interface ICommonService
    {
        Task<List<file_upload>> UploadFiles(List<file_upload> files, string foldername);

        Task Delete_Files(List<string> DeleteFiles, List<file_upload>? file_list, string foldername);

        Task<List<file_upload>> LoadAllFiles(List<file_upload>? file_list, string foldername);

        Task<file_upload> LoadSingleFiles(file_upload? file, string foldername);

        Task<List<owin_user_DTO>> LoadAllEmployeePic(List<owin_user_DTO>? user_List);

        Task<owin_user_DTO> LoadAllEmployeePic(owin_user_DTO? user_List);
    }
}
