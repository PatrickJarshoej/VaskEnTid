using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;
using VaskEnTidLib.Repo;

namespace VaskEnTidLib.Service
{
    public class BookingService
    {
        private IBookingRepo _bookingRepo;
        private IMachineRepo _machineRepo;
        public BookingService(IBookingRepo bookingRepo, IMachineRepo machineRepo)
        {
            _bookingRepo = bookingRepo;
            _machineRepo = machineRepo;
        }
       

        public void Create(DateTime dateAndTime, int domicileID, List<int> machineIDs)
        {
            TimeSpan duration = new TimeSpan();
            Booking booking = new(dateAndTime,domicileID,machineIDs,duration,0,0);
            _bookingRepo.Add(booking);
        }

        public List<Booking> GetAll()
        {
            return _bookingRepo.GetAll();
        }

        public void Update(Booking booking)
        {
            if (_bookingRepo.GetByID(booking.BookingID) == null) 
            {
                Debug.WriteLine("Booking does not exist");
            }
            else
            {
                _bookingRepo.Update(booking);
            }
        }
        public void DeleteByID(int id)
        {
            _bookingRepo.DeleteByID(id);
        }

        public Booking GetByDomicileID(int id)
        {
            return _bookingRepo.GetByDomicileID(id);
        }
        public Booking GetByID(int id)
        {
            return _bookingRepo.GetByID(id);
        }

        public double CalcCost(Booking booking)
        {
            double cost = 0;
            foreach (var m in booking.MachineIDs)
            {
                Model.Machine machine = _machineRepo.GetByID(m);
                cost += machine.Cost;
            }
            return cost;
        }


    }
}
