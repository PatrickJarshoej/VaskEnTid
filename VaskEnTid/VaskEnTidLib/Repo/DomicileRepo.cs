using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;

namespace VaskEnTidLib.Repo
{

    internal class DomicileRepo: IDomicileRepo
    {
        
        private List<Domicile> _domiciles;
        private string _connectionString;
        public DomicileRepo(List<Domicile> domiciles)
        {
            _domiciles = domiciles;
            _connectionString= string.Empty;
        }
        public void AddUserIDByDomID(int userID, int domicileID)
        {
            throw new NotImplementedException();
        }
        public void Add(Domicile theDomicile)
        {
            throw new NotImplementedException();
        }

        public List<Domicile> GetAll() 
        { 
            throw new NotImplementedException(); 
        }

        public Domicile GetByID(int id)
        {
            throw new NotImplementedException();
        }
        public Domicile GetByUserID(int userID)
        {
            throw new NotImplementedException();
        }
        public void Update(Domicile theDomicile)
        {
            throw new NotImplementedException();
        }
        public void DeleteByID(int id)
        {
            throw new NotImplementedException();
        }
        public double CalculatePriceTallyByID(double newCost, int id) 
        { 
            throw new NotImplementedException(); 
        }

    }
}
