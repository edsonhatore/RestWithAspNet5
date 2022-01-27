using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Repository
{
    public interface IUserRepository
    {
        User ValidateCredencial(UserVO user);
        User ValidateCredencial(string  username);

         User RefresUserInfo(User user);
        bool RevokeToken(string username);


    }
}
