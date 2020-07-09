using QS.Core.Attributes;
using QS.DataLayer.Entities;
using QS.ServiceLayer.User.Dtos.InputDto;

namespace QS.ServiceLayer.User.Dtos.OutputDto
{
    [MapFrom(typeof(UserEntity))]
    public class UserGetOutputDto : UserUpdateInputDto
    {

    }
}
