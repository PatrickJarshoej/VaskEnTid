using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;
using VaskEnTidLib.Repo;

namespace VaskEnTidLib.Service
{
    public class MachineService
    {

        private IMachineRepo _machineRepo;
        public MachineService(IMachineRepo machineRepo)
        {
            _machineRepo = machineRepo;
        }
        public void Create(Enum machineType)
        {
            throw new NotImplementedException();

        }
        public List<Machine> GetAll()
        {
            return _machineRepo.GetAll();
        }
        public void Update(int machineID,Status theStatus, double theCost)
        {
            Machine theMachine=GetByID(machineID);
            Machine tempMachine = new Machine
                (
                theMachine.TypeNumber,
                theMachine.Type,
                theStatus,
                theCost,
                machineID
                );
            _machineRepo.Update(tempMachine);
        }
        public void DeleteByID(int machineID)
        {
            throw new NotImplementedException();

        }
        public Machine GetByID(int machineID)
        {
            return _machineRepo.GetByID(machineID);
        }

    }
}
