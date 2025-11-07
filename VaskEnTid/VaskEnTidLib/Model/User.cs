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
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int UserID { get; private set; }
        public List<int> DomicileID { get; set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public User()
        {

        }
        public User(int userID, string firstname, string lastName, string email, string phone)
        {
            FirstName = firstname;
            LastName = lastName;
            UserID = userID;
            Email = email;
            Phone = phone;
        }
        public User(string firstname, string lastName, List<int> domicileID, string email, string phone)
        {
            FirstName = firstname;
            LastName = lastName;
            DomicileID = domicileID;
            Email = email;
            Phone = phone;
        }
        public User(string firstname, string lastName, int userID, List<int> domicileID, string email, string phone)
        {
            FirstName = firstname;
            LastName = lastName;
            UserID = userID;
            DomicileID = domicileID;
            Email = email;
            Phone = phone;
        }
    }
}
