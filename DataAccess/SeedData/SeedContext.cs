using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.SeedData
{
    public class SeedContext
    {
        private readonly DataContext _context;
        private readonly ILoggerFactory _loggerFactory;
        public SeedContext()
        {
            _context = ServiceTool.ServiceProvider.GetService<DataContext>();
            _loggerFactory = ServiceTool.ServiceProvider.GetService<ILoggerFactory>();
        }

        public void SeedAsync()
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
                else
                {


                    if (!_context.Campuses.Any())
                    {
                        var campuseData = File.ReadAllText(path + @"/SeedData/Campus.json");
                        var campuses = JsonSerializer.Deserialize<List<Campus>>(campuseData);

                        foreach (var campus in campuses)
                        {
                            _context.Campuses.Add(campus);

                        }
                    }

                    if (!_context.Departments.Any())
                    {
                        var departmentData = File.ReadAllText(path + @"/SeedData/Department.json");
                        var departments = JsonSerializer.Deserialize<List<Department>>(departmentData);

                        foreach (var department in departments)
                        {
                            _context.Departments.Add(department);
                        }
                    }

                    if (!_context.Degrees.Any())
                    {
                        var degreesData = File.ReadAllText(path + @"/SeedData/Degree.json");
                        var degrees = JsonSerializer.Deserialize<List<Degree>>(degreesData);

                        foreach (var degree in degrees)
                        {
                            _context.Degrees.Add(degree);
                        }
                    }

                    if (!_context.Users.Any())
                    {
                        var userData = File.ReadAllText(path + @"/SeedData/User.json");
                        var users = JsonSerializer.Deserialize<List<User>>(userData);

                        foreach (var user in users)
                        {
                            byte[] passwordHash, passwordSalt;

                            CreatePasswordHash("466357", out passwordHash, out passwordSalt);
                            user.PasswordHash = passwordHash;
                            user.PasswordSalt = passwordSalt;
                            user.IsActive = true;
                            user.Created = DateTime.Now;
                            user.Updated = DateTime.Now;
                            _context.Users.Add(user);
                        }
                    }

                    if (!_context.RoleCategories.Any())
                    {
                        var roleCategoriesData = File.ReadAllText(path + @"/SeedData/RoleCategory.json");
                        var roleCategories = JsonSerializer.Deserialize<List<RoleCategory>>(roleCategoriesData);

                        foreach (var roleCategory in roleCategories)
                        {
                            _context.RoleCategories.Add(roleCategory);
                        }
                    }

                    if (!_context.Roles.Any())
                    {
                        var rolesData = File.ReadAllText(path + @"/SeedData/Role.json");
                        var roles = JsonSerializer.Deserialize<List<Role>>(rolesData);

                        foreach (var role in roles)
                        {
                            _context.Roles.Add(role);
                        }
                    }

                    if (!_context.NumberOfRooms.Any())
                    {
                        var numberOfRoomData = File.ReadAllText(path + @"/SeedData/NumberOfRoom.json");
                        var numberOfrooms = JsonSerializer.Deserialize<List<NumberOfRoom>>(numberOfRoomData);

                        foreach (var rooms in numberOfrooms)
                        {
                            _context.NumberOfRooms.Add(rooms);
                        }
                    }

                    if (!_context.BuildingsAge.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/BuildingAge.json");
                        var dataList = JsonSerializer.Deserialize<List<BuildingAge>>(data);

                        foreach (var item in dataList)
                        {
                            _context.BuildingsAge.Add(item);
                        }
                    }

                    if (!_context.FlatsOfHome.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/FlatOfHome.json");
                        var dataList = JsonSerializer.Deserialize<List<FlatOfHome>>(data);

                        foreach (var item in dataList)
                        {
                            _context.FlatsOfHome.Add(item);
                        }
                    }

                    if (!_context.HeatingTypes.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/HeatingType.json");
                        var dataList = JsonSerializer.Deserialize<List<HeatingType>>(data);

                        foreach (var item in dataList)
                        {
                            _context.HeatingTypes.Add(item);
                        }
                    }

                    if (!_context.VehicleCategories.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/VehicleCategory.json");
                        var dataList = JsonSerializer.Deserialize<List<VehicleCategory>>(data);

                        foreach (var item in dataList)
                        {
                            _context.VehicleCategories.Add(item);
                        }
                    }

                    if (!_context.VehicleBrands.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/VehicleBrand.json");
                        var dataList = JsonSerializer.Deserialize<List<VehicleBrand>>(data);

                        foreach (var item in dataList)
                        {
                            _context.VehicleBrands.Add(item);
                        }
                    }

                    if (!_context.VehicleModels.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/VehicleModel.json");
                        var dataList = JsonSerializer.Deserialize<List<VehicleModel>>(data);

                        foreach (var item in dataList)
                        {
                            _context.VehicleModels.Add(item);
                        }
                    }

                    if (!_context.VehicleFuelTypes.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/VehicleFuelType.json");
                        var dataList = JsonSerializer.Deserialize<List<VehicleFuelType>>(data);

                        foreach (var item in dataList)
                        {
                            _context.VehicleFuelTypes.Add(item);
                        }
                    }

                    if (!_context.VehicleGearTypes.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/VehicleGearType.json");
                        var dataList = JsonSerializer.Deserialize<List<VehicleGearType>>(data);

                        foreach (var item in dataList)
                        {
                            _context.VehicleGearTypes.Add(item);
                        }
                    }

                    if (!_context.VehicleEngineSizes.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/VehicleEngineSize.json");
                        var dataList = JsonSerializer.Deserialize<List<VehicleEngineSize>>(data);

                        foreach (var item in dataList)
                        {
                            _context.VehicleEngineSizes.Add(item);
                        }
                    }

                    if (!_context.AnnounceContentTypes.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/AnnounceContentType.json");
                        var dataList = JsonSerializer.Deserialize<List<AnnounceContentType>>(data);

                        foreach (var item in dataList)
                        {
                            _context.AnnounceContentTypes.Add(item);
                        }
                    }

                    if (!_context.Screens.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/Screen.json");
                        var dataList = JsonSerializer.Deserialize<List<Screen>>(data);

                        foreach (var item in dataList)
                        {
                            _context.Screens.Add(item);
                        }
                    }

                    if (!_context.SubScreens.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/SubScreen.json");
                        var dataList = JsonSerializer.Deserialize<List<SubScreen>>(data);

                        foreach (var item in dataList)
                        {
                            _context.SubScreens.Add(item);
                        }
                    }

                    if (!_context.Cities.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/Cities.json");
                        var dataList = JsonSerializer.Deserialize<List<City>>(data);

                        foreach (var item in dataList)
                        {
                            _context.Cities.Add(item);
                        }
                    }

                    if (!_context.Currencies.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/Currencies.json");
                        var dataList = JsonSerializer.Deserialize<List<Currency>>(data);

                        foreach (var item in dataList)
                        {
                            _context.Currencies.Add(item);
                        }
                    }

                    if (!_context.LiveTvLists.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/Tvlist.json");
                        var dataList = JsonSerializer.Deserialize<List<LiveTvList>>(data);

                        foreach (var item in dataList)
                        {
                            _context.LiveTvLists.Add(item);
                        }
                    }

                    if (!_context.NotifyGroups.Any())
                    {
                        var data = File.ReadAllText(path + @"/SeedData/NotifyGroup.json");
                        var dataList = JsonSerializer.Deserialize<List<NotifyGroup>>(data);

                        foreach (var item in dataList)
                        {
                            _context.NotifyGroups.Add(item);
                        }
                    }

                    if (!_context.UserRoles.Any())
                    {                                                                       
                        var user = _context.Users.FirstOrDefault(x => x.Email.ToLower().Contains("sukru.ozdemir@hmb.gov.tr"));
                        if (user != null)
                        {
                            var sudoRole = _context.Roles.FirstOrDefault(x => x.Name.ToLower() == "sudo");
                            if (sudoRole != null)
                            {
                                var userrole = new UserRole
                                {
                                    UserId = user.Id,
                                    RoleId = sudoRole.Id
                                };

                                _context.UserRoles.Add(userrole);
                            }
                        }
                    }

                    _context.SaveChanges();

                }


            }
            catch (Exception ex)
            {
                var logger = _loggerFactory.CreateLogger<SeedContext>();
                logger.LogError("Seeding Error", ex.InnerException);

            }
        }

        private static void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}