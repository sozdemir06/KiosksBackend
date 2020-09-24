using Core.Entities;

namespace Entities.Dtos
{
    public class UserForChangePasswordDto:IDto
    {
        public string OldPassword { get; set; }
        public string  NewPassword { get; set; }
        public string  ConfirmNewPassword { get; set; }
    }
}