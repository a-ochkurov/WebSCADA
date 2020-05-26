using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using WebSCADA.BLL.Interfaces;
using WebSCADA.DAL.Entities;
using WebSCADA.DAL.Interfaces;
using WebSCADA.Domain.Models;

namespace WebSCADA.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Role> roleRepository;
        private readonly IMapper mapper;

        public UserService(IRepository<User> userRepository, IRepository<Role> roleRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }

        public UserDomain Get(string login, string password)
        {
            var hashPassword = CreateHash(password);
            var user = userRepository.Get(u => u.Login == login && u.Password == hashPassword).FirstOrDefault();
            var userDomain = mapper.Map<UserDomain>(user);

            if (user != null)
            {
                var role = roleRepository.Get(user.RoleId);
                if (role != null)
                {
                    userDomain.Role = role.RoleName;
                }
            }

            return userDomain;
        }

        public void Add(UserDomain userDomain)
        {
            userDomain.Password = CreateHash(userDomain.Password);
            var user = mapper.Map<User>(userDomain);

            userRepository.Create(user);
        }

        private string CreateHash(string value)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            byte[] hash = sha512.ComputeHash(bytes);
            var result = GetStringFromHash(hash);

            return result;
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
