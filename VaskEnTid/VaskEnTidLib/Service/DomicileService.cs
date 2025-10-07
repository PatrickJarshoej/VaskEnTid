using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Repo;
using VaskEnTidLib.Model;

namespace VaskEnTidLib.Service
{
    internal class DomicileService
    {
        private IDomicileRepo _domicileRepo;

        public DomicileService(IDomicileRepo domicileRepo) 
        { 
            _domicileRepo = domicileRepo;
        }

        public void Create(string roadname, string postalcode, string floor, string door, string city, string region, string country) 
        {
            throw new NotImplementedException();
        }
        public void AddUserIDbyDomID(int userID, int domID) 
        {  
            throw new NotImplementedException(); 
        }
        public List<Domicile> GetAll()
        {
            throw new NotImplementedException();
        }
        public void Update(Domicile theDomicile)
        {
            throw new NotImplementedException();
        }
        public void DeleteByID(int domID) 
        {
            throw new NotImplementedException(); 
        }
        public Domicile GetByUserID(int userID) 
        { 
            throw new NotImplementedException();
        }

        public Domicile GetByID(int domID)
        {
            throw new NotImplementedException();
        }
        public double CalcPriceTallyByID(double newCost, int ID)
        {
            throw new NotImplementedException();
        }
    }
}
