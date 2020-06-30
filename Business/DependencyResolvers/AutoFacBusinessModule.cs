using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.AutoFac
{
    public class AutoFacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();



            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<CampusManager>().As<ICampusService>();
            builder.RegisterType<EfCampusDal>().As<ICampusDal>();

            builder.RegisterType<DepartmentManager>().As<IDepartmentService>();
            builder.RegisterType<EfDepartmentDal>().As<IDepartmentDal>();

            builder.RegisterType<DegreeManager>().As<IDegreeService>();
            builder.RegisterType<EfDegreeDal>().As<IDegreeDal>();

            builder.RegisterType<UserRoleManager>().As<IUserroleService>();
            builder.RegisterType<EfUserRoleDal>().As<IUserRoleDal>();


            builder.RegisterType<EfRoleDal>().As<IRoleDal>();
            builder.RegisterType<RoleManager>().As<IRoleService>();

            builder.RegisterType<RoleCategoryManager>().As<IRoleCategoryService>();
            builder.RegisterType<EfRoleCategoryDal>().As<IRoleCategoryDal>();

            builder.RegisterType<NumberOfRoomManager>().As<INumberOfRoomService>();
            builder.RegisterType<EfNumberOfRoomDal>().As<INumberOfRoomDal>();

            builder.RegisterType<BuildignAgeManager>().As<IBuildingageService>();
            builder.RegisterType<EfBuildingAgeDal>().As<IBuildingAgeDal>();



            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                   .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                   {
                       Selector = new AspectInteerceptorSelector()
                   }).SingleInstance();

        }
    }
}