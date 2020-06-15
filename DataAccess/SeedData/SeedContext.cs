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
                if(_context.Database.GetPendingMigrations().Count()>0)
                {
                    _context.Database.Migrate();
                }

                if(_context.Database.GetPendingMigrations().Count()==0)
                {
                     if (!_context.Campuses.Any())
                {
                    var campuseData = File.ReadAllText("../DataAccess/SeedData/Campus.json");
                    var campuses = JsonSerializer.Deserialize<List<Campus>>(campuseData);

                    foreach (var campus in campuses)
                    {
                        _context.Campuses.Add(campus);

                    }
                }

                if (!_context.Campuses.Any())
                {
                    var departmentData = File.ReadAllText("../DataAccess/SeedData/Department.json");
                    var departments = JsonSerializer.Deserialize<List<Department>>(departmentData);

                    foreach (var department in departments)
                    {
                        _context.Departments.Add(department);
                    }
                }

                if (!_context.Users.Any())
                {
                    var userData = File.ReadAllText("../DataAccess/SeedData/User.json");
                    var users = JsonSerializer.Deserialize<List<User>>(userData);

                    foreach (var user in users)
                    {
                        byte[] passwordHash,passwordSalt;

                        CreatePasswordHash("466357",out passwordHash,out passwordSalt);
                        user.PasswordHash=passwordHash;
                        user.PasswordSalt=passwordSalt;
                        user.IsActive=false;
                        user.Created=DateTime.Now;
                        user.Updated=DateTime.Now;
                        _context.Users.Add(user);
                    }
                }

                _context.SaveChanges();

                }

               
            }
            catch (Exception ex)
            {
                var logger = _loggerFactory.CreateLogger<SeedContext>();
                logger.LogError("Seeding Error", ex.Message);

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