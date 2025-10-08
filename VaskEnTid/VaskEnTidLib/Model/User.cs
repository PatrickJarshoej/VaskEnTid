using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLib.Model
{
    public class User
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public int UserID { get; private set; }
        public int DomicileID { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public User()
        {

        }
        public User(string name, string lastName, int domicileID, string email, string phone)
        {
            Name = name;
            LastName = lastName;
            DomicileID = domicileID;
            Email = email;
            Phone = phone;
        }
        public User(string name, string lastName, int userID, int domicileID, string email, string phone)
        {
            Name = name;
            LastName = lastName;
            UserID = userID;
            DomicileID = domicileID;
            Email = email;
            Phone = phone;
        }
    }
}
