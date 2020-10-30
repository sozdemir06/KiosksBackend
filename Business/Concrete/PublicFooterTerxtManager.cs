using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.ValidaitonRules.FluentValidation;
using BusinessAspects.AutoFac;
using Core.Aspects.AutoFac.Validation;
using DataAccess.Abstract;
using Entities.Dtos;
using System.Linq;
using Core.Entities.Concrete;

namespace Business.Concrete
{
    public class PublicFooterTerxtManager : IPublicFooterTextService
    {
        private readonly IPublicfooterTextDal publicFooterTextDal;
        private readonly IMapper mapper;
        public PublicFooterTerxtManager(IPublicfooterTextDal publicFooterTextDal, IMapper mapper)
        {
            this.mapper = mapper;
            this.publicFooterTextDal = publicFooterTextDal;

        }

        [SecuredOperation("Sudo", Priority = 1)]
        [ValidationAspect(typeof(PublicFooterTextValidator), Priority = 2)]
        public async Task<PublicFooterTextForReturnDto> Create(PublicFooterTextForCreationDto creationDto)
        {
             var getFooterText=await publicFooterTextDal.GetFooterTextAsync();

             if(getFooterText!=null)
             {
                 getFooterText.ContentPhoneNumber=creationDto.ContentPhoneNumber;
                 getFooterText.FooterText=creationDto.FooterText;
                 await publicFooterTextDal.Update(getFooterText);   

             }else if(getFooterText==null){

                 var mapForCreate = mapper.Map<PublicFooterText>(creationDto);
                getFooterText=await publicFooterTextDal.Add(mapForCreate);
             }
             return mapper.Map<PublicFooterText,PublicFooterTextForReturnDto>(getFooterText);  
        }

        public async Task<PublicFooterTextForReturnDto> GetFooterTextAsync()
        {
             var checkIfExist=await publicFooterTextDal.GetListAsync();
             var getFooterText=checkIfExist.FirstOrDefault();
             return mapper.Map<PublicFooterText,PublicFooterTextForReturnDto>(getFooterText);
        }
    }
}