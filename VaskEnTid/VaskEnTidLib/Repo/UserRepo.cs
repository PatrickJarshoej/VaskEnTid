using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;

namespace VaskEnTidLib.Repo
{
    internal class UserRepo
    {
        private List<User> _users;
        public List<User> GetByDomicileID(int id)
        {
            return _users;
        }
        //public User GetByID(int id)
        //{
        //    return;
        //}
        public void Update(User user)
        {

        }
        public void Add(User user)
        {

        }
        public void DeleteByID(int id)
        {

        }
    }
}
