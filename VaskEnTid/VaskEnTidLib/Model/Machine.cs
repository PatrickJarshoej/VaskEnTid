using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLib.Model
{
    public enum Type { Washingmachine, Dryer, Rollingmachine}
    public enum Status { Available, Unavailable, UnderService}
    public class Machine
    {
        public TimeOnly MinimumTime { get; private set; }
        public int TypeNumber { get; private set; }
        public Type Type { get; private set; }
        public Status Status { get; private set; }
        public double Cost { get; private set; }
        public int MachineID { get; private set; }

        public Machine()
        {

        }
        public Machine(TimeOnly minimumTime, int typeNumber, Type type, Status status, double cost)
        {
            MinimumTime = minimumTime;
            TypeNumber = typeNumber;
            Type = type;
            Status = status;
            Cost = cost;
        }
        public Machine(TimeOnly minimumTime, int typeNumber, Type type, Status status, double cost, int machineID)
        {
            MinimumTime = minimumTime;
            TypeNumber = typeNumber;
            Type = type;
            Status = status;
            Cost = cost;
            MachineID = machineID;
        }

    }
}
