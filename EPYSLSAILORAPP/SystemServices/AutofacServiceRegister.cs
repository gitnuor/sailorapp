using Autofac;
using AutoMapper;
using EPYSLSAILORAPP.Application.Interface.Common;
using EPYSLSAILORAPP.Infrastructure.Identity.Services;
using EPYSLSAILORAPP.Infrastructure.Services;
using EPYSLSAILORAPP.Infrastructure.Services.Common;
//using EPYSLSAILORAPP.Infrastructure.Services.Employee;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace EPYSLSAILORAPP.SystemServices
{
    public class AutofacServiceRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            #region Configure Dependencies


            builder.RegisterGeneric(typeof(DapperCRUDService<>)).As(typeof(IDapperCRUDService<>)).InstancePerLifetimeScope();

            //builder.RegisterType<JwtService>().As(typeof(IJwtService)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(StylecategoryService).Assembly)
                .Where(t => t.Name.EndsWith("Service")).
                AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.Register(c => HttpContext.Current.GetOwinContext().Authentication);
            //builder.RegisterType<EmailService>().AsSelf().InstancePerRequest();
            builder.RegisterGeneric(typeof(PasswordHasher<>)).AsSelf().SingleInstance();

            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterType<MenuService>().As(typeof(IMenuService)).InstancePerRequest();
           // builder.RegisterType<SignatureService>().As(typeof(ISignatureService)).InstancePerRequest();
            //builder.RegisterType<UserService>().As(typeof(IUserService)).InstancePerRequest();

            builder.RegisterType<EntityTypeService>().As(typeof(IEntityTypeService)).InstancePerRequest();
            //builder.RegisterType<CommonInterfaceService>().As(typeof(ICommonInterfaceService)).InstancePerRequest();
            //builder.RegisterType<Mapper>().As(typeof(IMapper)).InstancePerRequest();
            //builder.RegisterType<CommonHelpers>().As(typeof(ICommonHelpers)).InstancePerRequest();

            builder.RegisterType<JwtService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            //builder.RegisterType<EmployeeService>().As(typeof(IEmployee)).InstancePerRequest();
            //builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
            //builder.RegisterType<DeSerializeJwtToken>().AsImplementedInterfaces().InstancePerRequest();

            //builder.RegisterType<CommonHelpers>().AsImplementedInterfaces().InstancePerRequest();
            //builder.RegisterType<RDLReportDocument>().AsSelf().InstancePerRequest();
            //builder.RegisterType<ReportingService>().AsImplementedInterfaces().InstancePerRequest();
            //builder.RegisterModule<NLogModule>();

            // Validators
            //builder.RegisterModule<ValidationModule>();
            //builder.RegisterModule<ValidationModuleWebApi>();

            // Automapper
            //builder.RegisterModule<MapperService>();


            //builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            #endregion Configure Dependencies


        }
    }
}
