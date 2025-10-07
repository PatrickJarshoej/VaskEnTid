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
        public TimeOnly Duration { get; set; }
        public double TotalCost { get; set; }
        public int ID { get; set; }

        public Booking(DateTime dateAndTime, int domicileID, List<int> machineIDs, TimeOnly duration, double totalCost, int id)
        {
            DateAndTime = dateAndTime;
            DomicileID = domicileID;
            MachineIDs = machineIDs;
            Duration = duration;
            TotalCost = totalCost;
            ID = id;
        }
    }
}
