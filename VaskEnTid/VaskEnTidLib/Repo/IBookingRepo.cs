using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;

namespace VaskEnTidLib.Repo
{
    public interface IBookingRepo
    {

        public void Add(Booking booking);
        public List<Booking> GetAll();
        public void Update(Booking booking);
        public void DeleteByID(int id);
        public Booking GetByDomicileID(int id);
        public Booking GetByID(int id);
        public double CalcCost(Booking booking);
    }
}
