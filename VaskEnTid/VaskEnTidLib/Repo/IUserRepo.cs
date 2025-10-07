using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using VaskEnTidLib.Model;

namespace VaskEnTidLib.Repo
{
    public interface IUserRepo
    {
        public List<User> GetAll();
        public List<User> GetByDomicileID(int domicileID);
        public User GetByID(int id);
        public void Update(User user);
        public void Add(User user);
        public void DeleteByID(int id);
    }
}
