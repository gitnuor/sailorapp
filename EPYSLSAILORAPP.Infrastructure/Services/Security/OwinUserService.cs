using AutoMapper;
using BDO.Core.Base;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.CustomIdentityManagers;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Security;
using EPYSLSAILORAPP.Domain;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using System.Net;
using System.Security.Claims;
using Web.Core.Frame.Helpers;


namespace EPYSLSAILORAPP.Infrastructure.Services.Security
{
    public class OwinUserService : IOwinUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IGenDesignationService _GenDesignationService;
        public readonly IRPCDbService _RPCDbService;
        private readonly IMapper _mapper;
        private readonly ApplicationSignInManager<owin_user_DTO> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ITranEmailNotificationMasterService _TranEmailNotificationMasterService;


        public OwinUserService(IConfiguration configuration, IMapper mapper
            , ApplicationSignInManager<owin_user_DTO> signInManager
            , IHttpContextAccessor contextAccessor, IRPCDbService RPCDbService,
            ITranEmailNotificationMasterService TranEmailNotificationMasterService,
            IGenDesignationService GenDesignationService, IHostingEnvironment hostingEnvironment
            )
        {
            _hostingEnvironment = hostingEnvironment;
            _GenDesignationService = GenDesignationService;
            _mapper = mapper;
            _configuration = configuration;
            _signInManager = signInManager;
            _RPCDbService = RPCDbService;
            _contextAccessor = contextAccessor;
            _TranEmailNotificationMasterService = TranEmailNotificationMasterService;
        }

        public async Task<bool> SaveAsync(owin_user_DTO entity)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<owin_user_entity>(JsonConvert.SerializeObject(entity));

                var server_filename = entity.emp_pic;

                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", "emp_pic");

                if (!string.IsNullOrEmpty(entity.new_emp_pic))
                {
                    server_filename = Guid.NewGuid().ToString().Replace("-", "_") + "." + entity.new_emp_pic_extension.Split('/')[1];

                    string base64String = entity.new_emp_pic;

                    string clean_base64 = base64String.Split(new string[] { "base64," }, StringSplitOptions.None)[1];

                    byte[] byteArray = Convert.FromBase64String(clean_base64);

                    try
                    {

                        if (!Directory.Exists(filePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(filePath);
                        }

                        File.WriteAllBytes(Path.Combine(filePath, server_filename), byteArray);

                        model.emp_pic = server_filename;
                    }
                    catch (Exception es)
                    {

                    }
                }


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    model.user_name = model.employee_code.ToString();

                    model.password = clsUtil.EncodeString(model.password);

                    var userid = connection.Insert(model);


                    //filePath = Path.Combine(_hostingEnvironment.WebRootPath, "UploadFolder", "Attachment", "Test_attach.pdf");

                    //byte[] pdfBytes = File.ReadAllBytes(filePath);

                    //string pdfBase64 = Convert.ToBase64String(pdfBytes);

                    //string combinedString = $"{"Test_attach.pdf"}|{pdfBase64}";

                    await _TranEmailNotificationMasterService.SendNotificationEmail(model.email, model.name, "User Creation Notification", null, model.userid);

                }

                return true;
            }
            catch (Exception ex)
            {
                //_connection.Close();
                throw (ex);
            }

        }
        public async Task<bool> UpdateAsync(owin_user_DTO entity)
        {
            try
            {
                var server_filename = entity.emp_pic;

                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", "emp_pic");

                if (entity.new_emp_pic.IndexOf(entity.emp_pic) < 0 && !string.IsNullOrEmpty(entity.new_emp_pic))
                {
                    server_filename = Guid.NewGuid().ToString().Replace("-", "_") + "." + entity.new_emp_pic_extension.Split('/')[1];

                    string base64String = entity.new_emp_pic;

                    string clean_base64 = base64String.Split(new string[] { "base64," }, StringSplitOptions.None)[1];

                    byte[] byteArray = Convert.FromBase64String(clean_base64);

                    try
                    {

                        if (!Directory.Exists(filePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(filePath);
                        }

                        File.WriteAllBytes(Path.Combine(filePath, server_filename), byteArray);
                    }
                    catch (Exception es)
                    {

                    }
                }

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"Update owin_user  set password=COALESCE(@password, password), is_active=@is_active,role_id=@role_id,gen_team_group_id=@gen_team_group_id,
                                    emp_pic=@emp_pic,designation_id=@designation_id where userid=@userid";

                    connection.Execute(query,
                       new
                       {
                           password = string.IsNullOrEmpty(entity.password) ? (object)DBNull.Value : clsUtil.EncodeString(entity.password),
                           is_active = entity.is_active,
                           role_id = entity.role_id,
                           gen_team_group_id = entity.gen_team_group_id,
                           emp_pic = server_filename,
                           designation_id = entity.designation_id,
                           userid = entity.userid
                       });
                   

                }

                await _TranEmailNotificationMasterService.SendNotificationEmail(entity.email, entity.name, "User Creation Notification", null, entity.userid);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public async Task<bool> UpdatePasswordAsync(owin_user_DTO entity)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    entity.password = clsUtil.EncodeString(entity.password);

                    string query = @"Update owin_user set password=@password where userid=@userid";

                    connection.Execute(query,
                       new
                       {
                           password = entity.password,
                           userid = entity.userid
                       });

                    await _TranEmailNotificationMasterService.SendNotificationEmail(entity.email, entity.name, "User Creation Notification", null, entity.userid);

                    return true;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public async Task<List<owin_user_entity>> GetAll()
        {
            List<owin_user_entity> list = new List<owin_user_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<owin_user_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<owin_user_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<List<owin_user_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM owin_user m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.employee_code::text ilike '%' || @search_text || '%' or 
                                                            m.name ilike '%' || @search_text || '%' or 
                                                            m.email ilike '%' || @search_text || '%' 
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<owin_user_entity>(query,
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

        public async Task<List<owin_user_entity_Ext>> GetAllPagedDataForSelect2(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*,d.designation_name
                                           FROM owin_user m
                                           inner join gen_designation d on d.designation_id=m.designation_id
                                           where 
                                            case when @team_group_id is null then true
                                                 when m.gen_team_group_id=@team_group_id then true
                                                 else false end and
                                            case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.employee_code::text ilike '%' || @search_text || '%' or 
                                                            m.name ilike '%' || @search_text || '%' or 
                                                            m.email ilike '%' || @search_text || '%' or 
                                                            d.designation_name ilike '%' || @search_text || '%' 
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<owin_user_entity_Ext>(query,
                        new
                        {
                            team_group_id = param.MasterID,
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

        public async Task<owin_user_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM owin_user m   where m.userid=@Id";

                    var dataList = connection.Query<owin_user_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return JsonConvert.DeserializeObject<owin_user_DTO>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }


        public async Task<owin_user_DTO> GetSingleAsyncByROle(string username)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"select m.* from owin_user m where m.user_name=@username";

                    var dataList = connection.Query<owin_user_entity>(query,
                        new { username = username }).ToList().FirstOrDefault();

                    return JsonConvert.DeserializeObject<owin_user_DTO>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public async Task<bool> UpdateProfilePictureAsync(owin_user_DTO entity)
        {
            try
            {
                var server_filename = entity.emp_pic;

                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", "emp_pic");

                if (entity.new_emp_pic.IndexOf(entity.emp_pic) < 0 && !string.IsNullOrEmpty(entity.new_emp_pic))
                {
                    server_filename = Guid.NewGuid().ToString().Replace("-", "_") + "." + entity.new_emp_pic_extension.Split('/')[1];

                    string base64String = entity.new_emp_pic;

                    string clean_base64 = base64String.Split(new string[] { "base64," }, StringSplitOptions.None)[1];

                    byte[] byteArray = Convert.FromBase64String(clean_base64);

                    try
                    {

                        if (!Directory.Exists(filePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(filePath);
                        }

                        File.WriteAllBytes(Path.Combine(filePath, server_filename), byteArray);
                    }
                    catch (Exception es)
                    {

                    }
                }

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"Update owin_user set 
                                    emp_pic=@emp_pic where userid=@userid";

                    connection.Execute(query,
                       new
                       {

                           emp_pic = server_filename,
                           userid = entity.userid
                       });
                    return true;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



        public async Task<List<owin_user_DTO>> GetMemberListByTeamID(Int64 gen_team_group_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM owin_user m   where m.gen_team_group_id=@gen_team_group_id";

                    var dataList = connection.Query<owin_user_DTO>(query,
                        new { gen_team_group_id = gen_team_group_id }).ToList();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
        public async Task<ResultEntity> CheckUserForLogin(owin_user_DTO model)
        {
            try
            {
                //await _connectionSupabse.InitializeAsync();

                var objuser = await _RPCDbService.GetAllget_user_infoAsync(model.user_name);

                var result = new ResultEntity();

                if (objuser == null)
                {
                    result.Status_Code = ((int)HttpStatusCode.BadRequest).ToString();
                    result.Status_Result = "Username or Password is not correct.";
                }
                else
                {
                    // string EncrPWd = clsUtil.EncodeString(objuser.password);

                    string DecrPWd = clsUtil.DecodeString(objuser.password);

                    if (model.password != DecrPWd)
                    {
                        result.Status_Code = ((int)HttpStatusCode.BadRequest).ToString();
                        result.Status_Result = "Username or Password is not correct.";
                    }
                    else
                    {
                        result.Status_Code = ((int)HttpStatusCode.OK).ToString();
                        result.Status_Result = JsonConvert.SerializeObject(objuser);

                        SecurityCapsule _securityCapsule = new SecurityCapsule();

                        _securityCapsule.updatedbyusername = objuser.user_name;
                        _securityCapsule.userfullname = objuser.name;

                        _securityCapsule.updateddate = DateTime.Now;
                        _securityCapsule.createdbyusername = objuser.user_name;
                        _securityCapsule.createddate = DateTime.Now;
                        _securityCapsule.transid = "NEWTRANSID";
                        _securityCapsule.userid = objuser.userid;
                        _securityCapsule.email = objuser.email;
                        _securityCapsule.username = objuser.user_name;
                        _securityCapsule.isauthenticated = true;

                        _securityCapsule.emp_pic = !string.IsNullOrEmpty(objuser.emp_pic) ? "/images/emp_pic/" + objuser.emp_pic : "/images/user.png";

                        _securityCapsule.roleid = objuser.role_id;
                        _securityCapsule.rolename = objuser.role_name;
                        _securityCapsule.groupname = objuser.team_group_name != null ? objuser.team_group_name : "";
                        _securityCapsule.gen_team_group_id = objuser.gen_team_group_id != null ? objuser.gen_team_group_id : 0; ;

                        _securityCapsule.sessionid = _contextAccessor.HttpContext.Session.Id;
                        _securityCapsule.usertoken = _securityCapsule.transid;

                        _securityCapsule.pageurl = _contextAccessor.HttpContext.Request.GetDisplayUrl();
                        _securityCapsule.ipaddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                        string strserialize = JsonConvert.SerializeObject(_securityCapsule);

                        var claims = new List<Claim>();

                        claims.Add(new Claim("secobject", strserialize));
                        claims.Add(new Claim("emp_pic", _securityCapsule.emp_pic));

                        claims.Add(new Claim("userid", objuser.userid.ToString()));
                        claims.Add(new Claim("username", objuser.user_name));
                        claims.Add(new Claim("userfullname", objuser.name));

                        claims.Add(new Claim("roleid", objuser.role_id.ToString()));
                        claims.Add(new Claim("rolename", objuser.role_name.ToString()));
                        claims.Add(new Claim("gen_team_group_id", objuser.gen_team_group_id != null ? objuser.gen_team_group_id.ToString() : "0"));
                        claims.Add(new Claim("team_group_name", objuser.team_group_name != null ? objuser.team_group_name : ""));

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                        var authProperties = new AuthenticationProperties
                        {

                            ExpiresUtc = DateTime.Now.AddMinutes(10),

                            IsPersistent = true,

                            IssuedUtc = DateTime.Now,
                        };

                        await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
               principal, new AuthenticationProperties() { IsPersistent = true });

                    };


                }

                return result;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {

            }

        }
        public async Task UserLogOff()
        {
            await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            _contextAccessor.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            foreach (var cookie in _contextAccessor.HttpContext.Request.Cookies.Keys)
            {
                //Erase the data in the cookie
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(-1);
                option.Secure = true;
                option.IsEssential = true;
                _contextAccessor.HttpContext.Response.Cookies.Append(cookie, string.Empty, option);

                //Then delete the cookie
                _contextAccessor.HttpContext.Response.Cookies.Delete(cookie);
            }


        }

        public async Task<string> RoleById(long? roleid)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM owin_role m   where m.owin_role_id=@Id";

                    var dataList = connection.Query<owin_role_entity>(query,
                        new { @Id = roleid }).ToList();

                    return dataList.FirstOrDefault().role_name;//JsonConvert.DeserializeObject<List<owin_role_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        public async Task<List<owin_user_DTO>> GetAllUserByNavigateUrl(string navigate_url)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"select distinct u.user_name
                                    from owin_user u
                                             inner join owin_role r on r.owin_role_id = u.role_id
                                             inner join owin_role_permission o on o.role_id = u.role_id and o.is_permitted = true
                                    inner join menu_action a on o.menu_action_id=a.menu_action_id
                                    where a.action_url =@navigate_url";

                    var dataList = connection.Query<owin_user_DTO>(query,
                        new
                        {
                            navigate_url = navigate_url
                        }).ToList();

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
