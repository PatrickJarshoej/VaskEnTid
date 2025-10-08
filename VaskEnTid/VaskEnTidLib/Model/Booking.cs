using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLib.Model
{
    public class Booking
    {
        public DateTime DateAndTime {  get; set; }
        public int DomicileID { get; set; }
        public List<int> MachineIDs { get; set; }
        public TimeSpan Duration { get; set; }
        public double TotalCost { get; set; }
        public int BookingID { get; set; }

        public Booking(DateTime dateAndTime, int domicileID, TimeSpan duration, double totalCost, int bookingID)
        {
            DateAndTime = dateAndTime;
            DomicileID = domicileID;
            Duration = duration;
            TotalCost = totalCost;
            BookingID = bookingID;
        }
        public Booking(DateTime dateAndTime, int domicileID, List<int> machineIDs, TimeSpan duration, double totalCost, int bookingID): this(dateAndTime, domicileID, duration, totalCost, bookingID)
        {
            MachineIDs = machineIDs;
        }
    }
}
