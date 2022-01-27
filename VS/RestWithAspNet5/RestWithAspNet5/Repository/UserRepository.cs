using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RestWithAspNet5.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly MySQLContext _context;
        public UserRepository(MySQLContext context )
        {
            _context = context;
        }

        public User ValidateCredencial(UserVO user)
        {

            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
          //  return _context.Users.FirstOrDefault(u => (u.Username == user.UserName) && (user.Password == pass));
            return _context.Users.FirstOrDefault(u => u.Username == user.UserName);


        }

        public User ValidateCredencial(string username)
        {
            return  _context.Users.SingleOrDefault(u => u.Username == username);
        }

        public User RefresUserInfo(User user)
        {

            if (!_context.Users.Any(v => v.id.Equals(user.id))) return null;
            
            var result = _context.Users.SingleOrDefault(v => v.id.Equals(user.id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user.id);
                    _context.SaveChanges();

                    return result;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
           
                return result;

            }
    

            private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

   

        public bool RevokeToken(string username)
        {
        var user =    _context.Users.SingleOrDefault(u => (u.Username == username));
            if (user is null) return false;

            user.RefreshToken = null;
            _context.SaveChanges();
            return true;


        }

       
    }
}
