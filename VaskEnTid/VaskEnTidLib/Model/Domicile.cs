using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLib.Model
{
    
    public class Domicile
    {
        public int DomicileID {  get; private set; }
        public List<int> UserIDs { get; private set; }

        public string Roadname { get; private set; }
        public int Postalcode { get; private set; }
        public string Floor { get; private set; }
        public string Door { get; private set; }
        public string City { get; private set; }
        public string Region{ get; private set; }
        public string Country { get; private set; }
        public double PriceTally { get; private set; }
        public Domicile(string roadname, int postalcode, string floor, string door, string city, string region, string country, List<int> userIDs, int domicileID=0, double priceTally = 0)
        {
            DomicileID = domicileID;
            UserIDs = userIDs;
            Roadname = roadname;
            Postalcode = postalcode;
            Floor = floor;
            Door = door;
            City = city;
            Region = region;
            Country = country;
            PriceTally = priceTally;
        }
        public Domicile(string roadname, int postalcode, string floor, string door, string city, string region, string country, int domicileID = 0, double priceTally = 0)
        {
            DomicileID = domicileID;
            UserIDs = new List<int>();
            Roadname = roadname;
            Postalcode = postalcode;
            Floor = floor;
            Door = door;
            City = city;
            Region = region;
            Country = country;
            PriceTally = priceTally;
        }
    }
}
