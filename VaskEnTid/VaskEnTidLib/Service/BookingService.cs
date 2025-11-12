using Microsoft.Identity.Client;
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
            try
            {
                if (dateAndTime >= DateTime.Now)
                {
                    Booking booking = new(dateAndTime, domicileID, machineIDs, CalcDuration(machineIDs), CalcCost(machineIDs), 0);
                    Debug.WriteLine(booking);
                    _bookingRepo.Add(booking);
                }
                else { Debug.WriteLine("DateTime Is Invalid"); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in Create() in BookingService");
                Debug.WriteLine("Error: "+ex);
            }
           
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
            Booking b = _bookingRepo.GetByID(id);
            if (b == null)
            {
                b = new Booking(DateTime.MinValue, 0, TimeSpan.MinValue, 0,0);
            }
            return b;
        }

        public double CalcCost(List<int> machineIDs)
        {
            double cost = 0;
            foreach (var m in machineIDs)
            {
                Model.Machine machine = _machineRepo.GetByID(m);
                cost += machine.Cost;
            }
            return cost;
        }
        public TimeSpan CalcDuration(List<int> machineIDs)
        {
            TimeSpan duration = new TimeSpan();

            foreach(var m in machineIDs)
            {
                Model.Machine machine = _machineRepo.GetByID(m);
                if (duration < machine.MinimumTime)
                {
                    duration = machine.MinimumTime.Add(new TimeSpan(0,10,0));
                }
            }
            return duration;
        }
    }
}
