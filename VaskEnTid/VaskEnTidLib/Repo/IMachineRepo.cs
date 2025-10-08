using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;

namespace VaskEnTidLib.Repo
{
    public interface IMachineRepo
    {
        public List<Machine> GetAll();
        public Machine GetByID();
        public void Add();
        public void DeleteByID();
        public void Update();
    }
}
