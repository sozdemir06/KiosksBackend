using System;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Helpers
{
    public class AnnounceStatusCheck : IAnnounceStatusCheck
    {
     
        public Task<bool> CheckAnnounceStatus(AnnounceForReturnDto announce)
        {
            bool check = false;
            if ((!announce.IsNew && announce.IsPublish == true && !announce.Reject &&
               announce.PublishStartDate <= DateTime.Now && announce.PublishFinishDate >= DateTime.Now) || (announce.PublishStartDate >= DateTime.Now))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            return Task.FromResult(check);
        }

        public async Task<Role> CheckPublicRole(IRoleDal roleDal,IRoleCategoryDal roleCategoryDal)
        {
            var publicRole = await roleDal.GetAsync(x=>x.Name.ToLower()=="public");
            if(publicRole==null)
            {
                var checkRoleCategory=await roleCategoryDal.GetAsync(x=>x.Name.ToLower()=="public");
                if(checkRoleCategory==null)
                {
                    checkRoleCategory=new RoleCategory
                    {
                        Name="Users",
                        Description="Kullanıcı ile ilgili yetkiler"

                    };

                    await roleCategoryDal.Add(checkRoleCategory);
                }

                var role=new Role
                {
                    Name="Public",
                    RoleCategoryId=checkRoleCategory.Id,
                    Description="Public Kullanıcı Yetkileri.."
                };

                await  roleDal.Add(role);
            }

            return await roleDal.GetAsync(x=>x.Name.ToLower()=="public");
        }
    }
}