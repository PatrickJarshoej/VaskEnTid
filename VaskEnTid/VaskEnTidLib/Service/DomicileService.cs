using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;
using VaskEnTidLib.Repo;

namespace VaskEnTidLib.Service
{
    public class DomicileService
    {
        private IDomicileRepo _domicileRepo;

        public DomicileService(IDomicileRepo domicileRepo) 
        { 
            _domicileRepo = domicileRepo;
        }

        public void Create(string roadname, int postalcode, string floor, string door, string city, string region, string country) 
        {
            Domicile theDomicile=new(roadname, postalcode, floor, door, city, region, country);
            _domicileRepo.Add(theDomicile);
        }
        public void AddUserIDbyDomID(int userID, int domID) 
        {  
            _domicileRepo.AddUserIDByDomID(userID, domID);
        }
        public void RemoveUserByID(int userID)
        {
            _domicileRepo.RemoveUserByID(userID);
        }
        public List<Domicile> GetAll()
        {
            return _domicileRepo.GetAll();
        }
        public void Update(int domicileID, string roadname, string door, string city, string region, string country)
        {
            Domicile theDomicile=_domicileRepo.GetByID(domicileID);
            Domicile tempDomicile = new Domicile(roadname, theDomicile.Postalcode, theDomicile.Floor, door, city, region, country, theDomicile.UserIDs, domicileID, theDomicile.PriceTally);
            if (_domicileRepo.GetByID(theDomicile.DomicileID) == null)
            {
                Debug.WriteLine("Domicile does not exist");
            }
            else
            {
                //Domicile theDomicile = new Domicile(roadname, postalcode, floor, door, city, region, country, userIDs, domicileID = 0, priceTally = 0);
                _domicileRepo.Update(tempDomicile);
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
