using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLib.Model
{
    public enum Type { Washer, Dryer, Rollingmachine }
    public enum Status { Available, Unavailable, UnderService }
    public class Machine
    {
        public TimeOnly MinimumTime { get; private set; }
        public int TypeNumber { get; private set; }
        public Type Type { get; private set; }
        public Status Status { get; private set; }
        public double Cost { get; private set; }
        public int MachineID { get; private set; }

        public TimeOnly WashTime = new(0, 45);
        public TimeOnly DryerTime = new(0, 20);
        public TimeOnly RollingTime = new(0, 30);
        public Machine()
        {

        }
        public Machine(int typeNumber, Type type, Status status, double cost)
        {
            if (Type == Type.Washer)
            {
                MinimumTime = WashTime;
            }
            else if (Type == Type.Dryer)
            { 
                MinimumTime = DryerTime; 
            }
            else if (Type == Type.Rollingmachine)
            { 
                MinimumTime = RollingTime;
            }
            TypeNumber = typeNumber;
            Type = type;
            Status = status;
            Cost = cost;
        }
        public Machine(int typeNumber, Type type, Status status, double cost, int machineID)
        {
            if (Type == Type.Washer)
            {
                MinimumTime = WashTime;
            }
            else if (Type == Type.Dryer)
            {
                MinimumTime = DryerTime;
            }
            else if (Type == Type.Rollingmachine)
            {
                MinimumTime = RollingTime;
            }
            TypeNumber = typeNumber;
            Type = type;
            Status = status;
            Cost = cost;
            MachineID = machineID;
        }

    }
}
