
using Autofac;
using Autofac.Extensions.DependencyInjection;

using EPYSLSAILORAPP.SystemServices;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
using AutoMapper;
using ServiceStack.Text;

using Microsoft.AspNetCore.Identity;
using Web.Core.Frame.CustomStores;
using EPYSLSAILORAPP.Application.CustomIdentityManagers;
using BDO.Core.DataAccessObjects.ExtendedEntities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Dapper;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 2000000000;

});

builder.Services.Configure<FormOptions>(o =>  // currently all set to max, configure it to your needs!
{
    o.ValueLengthLimit = int.MaxValue;
    o.ValueCountLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = 2000000000;//long.MaxValue; // <-- !!! long.MaxValue
                                            //o.MultipartBoundaryLengthLimit = int.MaxValue;
                                            //o.MultipartHeadersCountLimit = int.MaxValue;
                                            //o.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN"; // Specify the header name to be used for the token
});

#region All Service Register
//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Host.UseSerilog((context, config) =>
 {
     config.ReadFrom.Configuration(context.Configuration);
 });

//Activity.DefaultIdFormat = ActivityIdFormat.W3C;

builder.Services.AddControllers();// Its mandatory
builder.Services.AddMemoryCache();
builder.Services.AddSignalR(); // Add SignalR services
builder.Services.AddAutoMapper(typeof(MappingProfile));

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddIdentityCore<owin_user_DTO>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
             .AddRoles<IdentityRole>()

            .AddDefaultTokenProviders()
            .AddUserManager<ApplicationUserManager<owin_user_DTO>>()
            .AddSignInManager<ApplicationSignInManager<owin_user_DTO>>();


builder.Services.AddSingleton<IUserStore<owin_user_DTO>, CustomUserStore>();

builder.Services.AddSingleton<IRoleStore<IdentityRole>, CustomRoleStore>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<owin_user_DTO>, UserClaimsPrincipalFactory>();

builder.Services.AddScoped<ViewComponentServices>();

builder.Services.AddTransient<HelperService>();




//builder.Services.AddControllersWithViews()
//        .AddJsonOptions(options =>
//        {
//            options.ContractResolver = new CustomContractResolver();
//        });

//builder.Services.AddControllers()
//        .AddNewtonsoftJson(options =>
//        {
//            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
//        });


//builder.Services.AddMvc().AddNewtonsoftJson(o =>
//{
//    o.SerializerSettings.ContractResolver =
//        new CustomContractResolver();
//});
//builder.Services.AddControllers()
//        .AddJsonOptions(options =>
//        {
//            options.JsonSerializerOptions.PropertyNamingPolicy = null;
//        });
//builder.Services.AddMvc().AddNewtonsoftJson
//            (options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

//Configure Swagger

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(options =>
//     {
//         options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//         options.SlidingExpiration = true;
//         options.AccessDeniedPath = "/Forbidden/";
//     });
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
     {
         options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
         options.SlidingExpiration = true;
         options.LoginPath = "/Account/Login"; // Customize the login path as needed
         options.AccessDeniedPath = "/Account/AccessDenied"; // Customize the access denied path as needed

     });

var redisConnectionStrings = builder.Configuration.GetSection(nameof(RedisConnectionStrings)).Get<RedisConnectionStrings>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = redisConnectionStrings.redisSessionCookieName;
    options.IdleTimeout = TimeSpan.FromSeconds(redisConnectionStrings.IdleTimeout);
    options.Cookie.SameSite = SameSiteMode.Strict;
    //options.Cookie.HttpOnly = true;
    //options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireLoggedIn", policy => policy.RequireAuthenticatedUser());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.WithOrigins("https://fonts.googleapis.com", "http://localhost:5032").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowCredentials()
                 .WithOrigins("http://localhost:5032")
                 //.WithOrigins((_configuration.GetSection("HostingDomainSettings").Get<HostingDomainSettings>()).CompleteDomainURL)//("http://localhost:30046") //
                 .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services.AddHttpContextAccessor();

builder.Services.RegisterService(builder);

#endregion

#region Service Build in Autofac 
var builderAutoFac = new ContainerBuilder();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacServiceRegister());
    });

builderAutoFac.Populate(builder.Services);
var container = builderAutoFac.Build();
#endregion

#region Middleware

var configuration = new ConfigurationBuilder()
     .AddJsonFile($"appsettings.json");

var app = builder.Build();

//app.UseExceptionHandler("/Home/Error");

//app.UseDeveloperExceptionPage();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{

    app.UseExceptionHandler("/Home/Error");
    // app.UseHsts();
}


app.UseStaticFiles();
app.UseSerilogRequestLogging(); //// Serilog

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

//#region CORS
//var _appSettings = builder.Configuration.GetSection(nameof(CORS)).Get<CORS>();
//app.UseCors(_appSettings.AllowAnyOrigin ? "AllowAnyOrigin" : "AllowedOrigins");
//#endregion

app.UseSession();


var cookiePolicyOptions = new CookiePolicyOptions
{
    //Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always,
};
app.UseCookiePolicy(cookiePolicyOptions);

app.UseAuthentication();

app.UseAuthorization();


app.UseGlobalExceptionMiddleware();

app.MapControllers();


//app.UseResponseCompression();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapHub<ChatHub>("/chathub"); // Map the SignalR hub
});

#endregion

app.Run();


