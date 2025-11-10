using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;
using VaskEnTidLib.Repo;

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
            Domicile theDomicile=new(roadname, postalcode, floor, door, city, region, country);
            _domicileRepo.Add(theDomicile);
        }
        public void AddUserIDbyDomID(int userID, int domID) 
        {  
            _domicileRepo.AddUserIDByDomID(userID, domID);
        }
        public List<Domicile> GetAll()
        {
            return _domicileRepo.GetAll();
        }
        public void Update(Domicile theDomicile)
        {
            if (_domicileRepo.GetByID(theDomicile.DomicileID) == null)
            {
                Debug.WriteLine("Domicile does not exist");
            }
            else
            {
                _domicileRepo.Update(theDomicile);
            }
        }
        public void DeleteByID(int domID) 
        {
            _domicileRepo.DeleteByID(domID);
        }
        public Domicile GetByUserID(int userID) 
        { 
            return _domicileRepo.GetByUserID(userID);
        }

        public Domicile GetByID(int domID)
        {
            return _domicileRepo.GetByID(domID);
        }
        public double CalcPriceTallyByID(double newCost, int ID)
        {
            _domicileRepo.CalculatePriceTallyByID(newCost, ID);
            
            return GetByID(ID).PriceTally;
        }
    }
}
