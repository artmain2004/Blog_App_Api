using Application.DTO;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public static class UserMapper
    {

        public static UserDto ToUserDto(User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Lastname = user.Lastname,
                Email = user.Email,
                Avatar = user.ImageBase64
            };
        }

    }
}
