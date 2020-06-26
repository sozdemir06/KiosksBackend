using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.Specifications;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserRoleDal : EfEntityRepositoryBase<UserRole, DataContext>,IUserRoleDal
    {
       
    }
}