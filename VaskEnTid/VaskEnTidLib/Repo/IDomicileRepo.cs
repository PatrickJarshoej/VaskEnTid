using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;

namespace VaskEnTidLib.Repo
{
    internal interface IDomicileRepo
    {
        void AddUserIDByDomID(int userID, int domicileID);
        void Add(Domicile theDomicile);

        List<Domicile> GetAll();

        Domicile GetByID(int id);

        Domicile GetByUserID(int userID);

        void Update(Domicile theDomicile);

        void DeleteByID(int id);

        double CalculatePriceTallyByID(double newCost, int id);

    }
}
