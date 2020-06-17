using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Extensions;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDal departmentDal;
        private readonly IMapper mapper;
        public DepartmentManager(IDepartmentDal departmentDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.departmentDal = departmentDal;

        }
        public async Task<List<DepartmentForListDto>> GetDepartmentListAsync()
        {
            var departments = await departmentDal.GetListAsync();
            if (departments == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { DepartmentListNotFound = Messages.DepartmentListNotFound });
            }

            var departmentForReturn=mapper.Map<List<Department>,List<DepartmentForListDto>>(departments);

            return departmentForReturn;

        }
    }
}