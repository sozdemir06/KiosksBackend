using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Photos;
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
            builder.RegisterType<FlatOfHomeManager>().As<IFlatOfHomeService>();
            builder.RegisterType<EfFlatOfHomeDal>().As<IFlatOfHomeDal>();
            builder.RegisterType<HeatingTypeManager>().As<IHeatingTypeService>();
            builder.RegisterType<EfHeatingTypeDal>().As<IHeatingTypeDal>();
            builder.RegisterType<VehicleCategoryManager>().As<IVehicleCategoryService>();
            builder.RegisterType<EfVehicleCategoryDal>().As<IVehicleCategoryDal>();
            builder.RegisterType<VehicleBrandManager>().As<IVehicleBrandService>();
            builder.RegisterType<EfVehicleBrandDal>().As<IVehicleBrandDal>();
            builder.RegisterType<VehicleModelManager>().As<IVehicleModelService>();
            builder.RegisterType<EfVehicleModelDal>().As<IVehicleModelDal>();
            builder.RegisterType<VehicleFuelTypeManager>().As<IVehicleFuelTypeService>();
            builder.RegisterType<EfVehicleFuelTypeDal>().As<IVehicleFuelTypeDal>();

            builder.RegisterType<VehicleGearTypeManager>().As<IVehicleGearTypeService>();
            builder.RegisterType<EfVehicleGearTypeDal>().As<IVehicleGearTypeDal>();

            builder.RegisterType<VehicleEngineSizeManager>().As<IVehicleEngineSizeService>();
            builder.RegisterType<EfVehicleEngineSizeDal>().As<IVehicleEngineSizeDal>();

            builder.RegisterType<ScreenManager>().As<IScreenService>();
            builder.RegisterType<EfScreenDal>().As<IScreenDal>();

            builder.RegisterType<SubScreenManager>().As<ISubScreenService>();
            builder.RegisterType<EfSubScreenDal>().As<ISubSCreenDal>();

            builder.RegisterType<HomeAnnounceManager>().As<IHomeAnnounceService>();
            builder.RegisterType<EfHomeAnnounceDal>().As<IHomeAnnounceDal>();

            builder.RegisterType<HomeAnnouncePhotoManager>().As<IHomeAnnouncePhotoService>();
            builder.RegisterType<EfHomeAnnouncePhotoDal>().As<IHomeAnnouncePhotoDal>();

            builder.RegisterType<HomeAnnouncesubScreenManager>().As<IHomeAnnounceSubScreenService>();
            builder.RegisterType<EfHomeAnnounceSubScreenDal>().As<IHomeAnnounceSubScreenDal>();

            builder.RegisterType<VehicleAnnounceManager>().As<IVehicleAnnounceService>();
            builder.RegisterType<EfVehicleAnnounceDal>().As<IVehicleAnnounceDal>();

            builder.RegisterType<VehicleAnnouncePhotoManager>().As<IVehicleAnnouncePhotoService>();
            builder.RegisterType<EfVehicleAnnouncePhotoDal>().As<IVehicleAnnouncePhotoDal>();

            builder.RegisterType<VehicleAnnouncesubScreenManager>().As<IVehicleAnnounceSubScreenService>();
            builder.RegisterType<EfVehicleAnnounceSubScreenDal>().As<IVehicleAnnounceSubScreenDal>();

            builder.RegisterType<AnnounceManager>().As<IAnnounceService>();
            builder.RegisterType<EfAnnounceDal>().As<IAnnounceDal>();
            builder.RegisterType<AnnouncePhotoManager>().As<IAnnouncePhotoService>();
            builder.RegisterType<EfAnnouncePhotoDal>().As<IAnnouncePhotoDal>();
            builder.RegisterType<AnnounceSubScreenManager>().As<IAnnounceSubScreenService>();
            builder.RegisterType<EfAnnounceSubScreenDal>().As<IAnnounceSubScreenDal>();

            builder.RegisterType<NewsManager>().As<INewsService>();
            builder.RegisterType<EfNewsDal>().As<INewsDal>();
            builder.RegisterType<NewsPhotoManager>().As<INewsPhotoService>();
            builder.RegisterType<EfNewsPhotoDal>().As<INewsPhotoDal>();
            builder.RegisterType<NewsSubScreenManager>().As<INewsSubScreenService>();
            builder.RegisterType<EfNewssubScreenDal>().As<INewsSubScreenDal>();

            builder.RegisterType<AnnounceContentTypeManager>().As<IAnnounceContentTypeService>();
            builder.RegisterType<EfAnnounceContentTypeDal>().As<IAnnounceContentTypeDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<UploadFile>().As<IUploadFile>();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                   .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                   {
                       Selector = new AspectInteerceptorSelector()
                   }).SingleInstance();

        }
    }
}