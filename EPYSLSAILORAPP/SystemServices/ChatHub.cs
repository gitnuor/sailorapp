using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Application.Interface.Security;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Security;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using EPYSLSAILORAPP.Infrastructure.Services.Security;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver.Core.Connections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.SystemServices
{
    public class ChatHub : Hub
    {
        public static readonly ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>();
        public static readonly ConcurrentDictionary<string, ConcurrentBag<string>> _singleuserAllConnections = new ConcurrentDictionary<string, ConcurrentBag<string>>();

        private IRedisService<SecurityCapsule> _objUser;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly ITranChatService _TranChatService;
        private readonly ITranNotificationService _TranNotificationService;
        private readonly IGenChatGroupService _GenChatGroupService;
        private readonly IOwinUserService _OwinUserService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public ChatHub(IOwinUserService OwinUserService, IHubContext<ChatHub> hubContext, IConfiguration configuration, ITranNotificationService TranNotificationService, IGenChatGroupService GenChatGroupService, IMemoryCache cache, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, ITranChatService TranChatService)
        {
            _configuration = configuration;
            _cache = cache;
            _objUser = new RedisService<SecurityCapsule>(_configuration);
            _TranChatService = TranChatService;
            _hostingEnvironment = hostingEnvironment;
            _GenChatGroupService = GenChatGroupService;
            _TranNotificationService = TranNotificationService;
            _OwinUserService = OwinUserService;
            _hubContext = hubContext;
        }
        public override Task OnConnectedAsync()
        {
            try
            {
                var sec_object = new SecurityCapsule();

                var claim = Context.User.Identity as ClaimsIdentity;

                List<Claim> listClaims = (List<Claim>)claim.Claims;

                if (listClaims.Count > 0)
                {
                    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                    _userConnections.AddOrUpdate(sec_object.username, Context.ConnectionId, (key, oldValue) => Context.ConnectionId);

                    var connections = _singleuserAllConnections.GetOrAdd(sec_object.username, _ => new ConcurrentBag<string>());

                    connections.Add(Context.ConnectionId);

                    List<SecurityCapsule> list = new List<SecurityCapsule>();

                    if (!_cache.TryGetValue("online_user_list", out list))
                    {
                        list = new List<SecurityCapsule>();
                        list.Add(sec_object);

                        _cache.Set("online_user_list", list);
                    }
                    else
                    {
                        if (list.Where(p => p.username == sec_object.username).ToList().Count == 0)
                        {
                            list.Add(sec_object);

                            _cache.Set("online_user_list", list);
                        }

                    }

                    //if (_singleuserAllConnections.TryGetValue(sec_object.username, out var connectionIds))
                    //{
                    //    foreach (var con in connectionIds)
                    //    {
                    //        if (con != Context.ConnectionId)
                    //        {
                    //            Clients.Client(con).SendAsync("UserLogOut");
                    //        }
                    //    }
                    //}

                    Clients.All.SendAsync("LoadAllUserList", JsonConvert.SerializeObject(list));
                }
            }
            catch (Exception ex)
            {

            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var sec_object = new SecurityCapsule();

                var claim = Context.User.Identity as ClaimsIdentity;

                List<Claim> listClaims = (List<Claim>)claim.Claims;

                if (listClaims.Count > 0)
                {
                    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                    //if (_singleuserAllConnections.TryGetValue(sec_object.username, out var connectionIds))
                    //{
                    //    foreach (var con in connectionIds)
                    //    {
                    //        Clients.Client(con).SendAsync("UserLogOut");
                    //    }
                    //}

                    _userConnections.TryRemove(sec_object.username, out _);

                    //_singleuserAllConnections.TryRemove(sec_object.username, out _);

                    List<SecurityCapsule> list = new List<SecurityCapsule>();

                    if (_cache.TryGetValue("online_user_list", out list))
                    {

                        if (list.Where(p => p.username == sec_object.username).ToList().Count == 1)
                        {
                            list.Remove(list.Where(p => p.username == sec_object.username).FirstOrDefault());

                            _cache.Set("online_user_list", list);

                            Clients.All.SendAsync("RemoveCurrentUser", JsonConvert.SerializeObject(sec_object));
                        }
                    }

                   
                }
            }
            catch (Exception ex)
            {

            }

            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        public async Task SendMessageToUser(string userId, string message, string json_message)
        {
            try
            {
                long count = 0;

                var sec_object = new SecurityCapsule();

                var claim = Context.User.Identity as ClaimsIdentity;

                List<Claim> listClaims = (List<Claim>)claim.Claims;

                if (listClaims.Count > 0)
                {
                    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                    var objchat = new tran_chat_DTO();

                    objchat.is_view = 0;
                    objchat.from_user_name = sec_object.username;
                    objchat.to_user_name = userId;
                    objchat.date_added = DateTime.Now;
                    objchat.message = message;

                    if (!string.IsNullOrEmpty(json_message))
                    {
                        objchat.message_json = JObject.Parse(json_message).ToString();
                    }

                    count = await _TranChatService.SaveAndGetNewMessageCount(objchat);


                    if (_userConnections.TryGetValue(userId, out var connectionId))
                    {

                        await Clients.Client(connectionId).SendAsync("ReceiveChatMessageNotification", sec_object.username, count.ToString(), message, json_message);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SendMessageToGroup(string groupId, string message, string json_message)
        {
            try
            {
                long count = 0;

                var sec_object = new SecurityCapsule();

                var claim = Context.User.Identity as ClaimsIdentity;

                List<Claim> listClaims = (List<Claim>)claim.Claims;

                if (listClaims.Count > 0)
                {
                    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);


                    if (!string.IsNullOrEmpty(groupId))
                    {
                        var objGroup = await _GenChatGroupService.GetSingleAsync(Convert.ToInt64(groupId));

                        if (objGroup != null)
                        {

                            if (!string.IsNullOrEmpty(objGroup.group_users))
                            {
                                var ListUser = JsonConvert.DeserializeObject<List<owin_user_entity>>(objGroup.group_users);

                                foreach (var objuser in ListUser)
                                {

                                    var objchat = new tran_chat_DTO();

                                    objchat.is_view = 0;
                                    objchat.from_user_name = sec_object.username;
                                    objchat.to_user_name = objuser.user_name;
                                    objchat.date_added = DateTime.Now;

                                    objchat.message = message;
                                    objchat.is_group = 1;
                                    objchat.to_chat_group_id = Convert.ToInt64(groupId);
                                    objchat.to_chat_group_name = objGroup.chat_group_name; ;
                                    objchat.to_chat_group_users = objGroup.group_users;

                                    if (!string.IsNullOrEmpty(json_message))
                                    {
                                        objchat.message_json = JObject.Parse(json_message).ToString();
                                    }

                                    count = await _TranChatService.SaveAndGetNewMessageCount(objchat);

                                    if (objuser.user_name != sec_object.username)
                                    {
                                        if (_userConnections.TryGetValue(objuser.user_name, out var connectionId))
                                        {
                                            await Clients.Client(connectionId).SendAsync("ReceiveGroupChatMessageNotification", groupId, objGroup.chat_group_name, sec_object.username, sec_object.emp_pic, count.ToString(), message, json_message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SendNoticationToUser(string title, string navigate_url, string notification_link)
        {
            try
            {
                long count = 0;

                var sec_object = new SecurityCapsule();

                var claim = Context.User.Identity as ClaimsIdentity;

                List<Claim> listClaims = (List<Claim>)claim.Claims;

                if (listClaims.Count > 0)
                {
                    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);

                    var List = await _OwinUserService.GetAllUserByNavigateUrl(navigate_url);

                    foreach (var obj_user in List)
                    {

                        var objnotification = new tran_notification_DTO();

                        objnotification.is_view = 0;
                        objnotification.to_user_name = obj_user.user_name;
                        objnotification.date_added = DateTime.Now;
                        objnotification.message = title;
                        objnotification.notifacation_link = notification_link;

                        var obj = await _TranNotificationService.tran_notification_insert_sp(objnotification);

                        if (_userConnections.TryGetValue(obj_user.user_name, out var connectionId))
                        {
                            await Clients.Client(connectionId).SendAsync("ReceiveSystemNotification", sec_object.username, title, obj.new_notification_id_ret.ToString(), notification_link, obj.total_notification.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task RefreshClientPageOnFiscalYearChange()
        {
            try
            {
                long count = 0;

                var sec_object = new SecurityCapsule();

                var claim = Context.User.Identity as ClaimsIdentity;

                List<Claim> listClaims = (List<Claim>)claim.Claims;

                if (listClaims.Count > 0)
                {
                    var strsecobject = listClaims.Find(c => c.Type == "secobject").Value;

                    sec_object = JsonConvert.DeserializeObject<SecurityCapsule>(strsecobject);


                    if (_singleuserAllConnections.TryGetValue(sec_object.username, out var connectionIds))
                    {
                        foreach (var con in connectionIds)
                        {
                            await Clients.Client(con).SendAsync("RefreshClientOnFiscalYearChange");
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
