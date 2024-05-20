using UserApi.UserApiDtos;
using UserApi.UserApiModels;

namespace UserApi
{
    public static class Extensions
    {
        public static UserDto AsDto(this User entity, int id,string name, string surname)
        {
            return new UserDto(entity.Id, name, surname);
        }
        public static User AsEntity(this UserDto user,int id,string name, string surname)
        {
            return new User(user.id, name, surname);
        }
    }
}

