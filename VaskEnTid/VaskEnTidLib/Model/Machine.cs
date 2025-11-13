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
        public TimeSpan MinimumTime { get; private set; }
        public int TypeNumber { get; private set; }
        public Type Type { get; private set; }
        public Status Status { get; private set; }
        public double Cost { get; private set; }
        public int MachineID { get; private set; }

        public TimeSpan WashTime = new(0, 45, 0);
        public TimeSpan DryerTime = new(0, 20, 0);
        public TimeSpan RollingTime = new(0, 30, 0);
        public Machine()
        {

        }
        public Machine(int typeNumber, Type type, Status status=Status.Unavailable, double cost=25)
        {
            if (type == Type.Washer)
            {
                MinimumTime = WashTime;
            }
            else if (type == Type.Dryer)
            { 
                MinimumTime = DryerTime; 
            }
            else if (type == Type.Rollingmachine)
            { 
                MinimumTime = RollingTime;
            }
            Type = type;
            TypeNumber = typeNumber;
            Status = status;
            Cost = cost;
        }
        public Machine(int typeNumber, Type type, Status status, double cost, int machineID)
        {
            if (type == Type.Washer)
            {
                MinimumTime = WashTime;
            }
            else if (type == Type.Dryer)
            {
                MinimumTime = DryerTime;
            }
            else if (type == Type.Rollingmachine)
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
