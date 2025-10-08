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
        public List<User> GetByDomicileID(int id)
        {
            throw new NotImplementedException();
        }
        public User GetByID(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(User user)
        {

        }
        public void CreateUser(string name, string lastName, int domicileID, string email, string phone)
        {
            User user = new(name, lastName, domicileID, email, phone);
        }
    }
}
