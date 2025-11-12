using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Repo;
using VaskEnTidLib.Model;

namespace VaskEnTidLib.Service
{
    public class UserService
    {
        private IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public List<User> GetByDomicileID(int id)
        {
            return _userRepo.GetByDomicileID(id);
        }
        public User GetByID(int id)
        {
            return _userRepo.GetByID(id);
        }
        public void Update(User user)
        {

        }
        public List<User> GetAll()
        {
            List<User> users = new();
            users = _userRepo.GetAll();
            return users;
        }
        public void CreateUser(string name, string lastName, List<int> domicileID, string email, string phone)
        {
            User user = new(name, lastName, domicileID, email, phone);
            _userRepo.Add(user);
        }
        public User CheckPassword(int UserID, string Password)
        {
            User user = _userRepo.CheckPassword(UserID, Password);
            return user;
        }
    }
}
