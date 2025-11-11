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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserID { get; set; }
        public List<int> DomicileID { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public User()
        {
            FirstName = "not a user";
            LastName = "you fucked up";
            UserID = 0;
            List<int> jon = new(0);
            Email = "You should probably fix this";
            Phone = "Unless it is what you wanted?";
            Password = "Which in that case, good job i guess?";
            IsAdmin = false;
        }
        public User(int userID)
        {
            UserID = userID;
        }
        public User(int userID, string firstname, string lastName, string email, string phone, string password, bool isAdmin)
        {
            FirstName = firstname;
            LastName = lastName;
            UserID = userID;
            Email = email;
            Phone = phone;
            Password = password;
            IsAdmin = isAdmin;
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
