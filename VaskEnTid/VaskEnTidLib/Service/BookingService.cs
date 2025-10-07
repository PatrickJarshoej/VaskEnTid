using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Repo;
using VaskEnTidLib.Model;

namespace VaskEnTidLib.Service
{
    public class BookingService
    {
        private IBookingRepo _bookingRepo;
        public BookingService(IBookingRepo bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public void Create()
        {

        }
    }
}
