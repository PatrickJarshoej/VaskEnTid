using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using VaskEnTidLib.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VaskEnTidLib.Repo
{
    public class UserRepo : IUserRepo
    {
        private string _connectionString;
        public UserRepo()
        {
            _connectionString = "Data Source=mssql15.unoeuro.com;User ID=arvedlund_com;Password=BdpAFfg62xzDnR3wkcht;Encrypt=False; Database=arvedlund_com_db_vask_en_tid; Command Timeout=30;MultipleActiveResultSets=true;";
        }
        /// <summary>
        /// Add's a User to the Users database
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("INSERT INTO Users (FirstName, LastName, Email, Phone, Password, IsAdmin) VALUES (@FirstName, @LastName, @Email, @Phone, @Password, @IsAdmin)", connection);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Deletes an entry in the Users database.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteByUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("DELETE FROM Users WHERE ID = @ID", connection);
                    var command2 = new SqlCommand("DELETE FROM MapDomicileID WHERE ID = @ID and DomID = @DomID", connection);
                    command.Parameters.AddWithValue("@ID", user.UserID);
                    command2.Parameters.AddWithValue("@ID", user.UserID);
                    command2.Parameters.AddWithValue("@DomID", user.DomicileID[0]);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error ind Repo Delete User");
                    Debug.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Returns all Users from Users database
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT UserID, FirstName, LastName, Email, Phone, Password, IsAdmin FROM Users", connection);
                connection.Open();
                try
                {

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User(

                                (int)reader["UserID"],
                                (string)reader["FirstName"],
                                (string)reader["LastName"],
                                (string)reader["Email"],
                                (string)reader["Phone"],
                                (string)reader["Password"],
                                (bool)reader["IsAdmin"]

                                );
                            user.DomicileID = GetDomicileIDs((int)reader["UserID"], connection);

                            users.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in User GetAll method");
                    Debug.WriteLine($"Error: {ex}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return users;
        }
        public User GetIDFromCreation(User user)
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT UserID FROM Users WHERE FirstName = @FirstName and LastName = @LastName and Email = @Email and Phone = @Phone and Password = @Password and IsAdmin = @IsAdmin", connection);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                connection.Open();
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.UserID = ((int)reader["UserID"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex}");
                }
                finally
                {
                    connection.Close();
                }
                return user;
            }
        }

        /// <summary>
        /// Returns domicile ID's based on a UserID 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public List<int> GetDomicileIDs(int userid, SqlConnection connection)
        {
            List<int> domicileIDs = new List<int>();
            var command = new SqlCommand("SELECT DomicileID FROM MapDomicileID WHERE UserId = @Id", connection);
            command.Parameters.AddWithValue("@Id", userid);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        domicileIDs.Add((int)reader["DomicileID"]);
                    }
                }
                return domicileIDs;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex}");
            }
            finally
            {
            }
            return domicileIDs;
        }

        /// <summary>
        /// Not sure if it works correct. Might be an issue with using UserIDs list to find users?
        /// Gives a list of Users when question on what Users lives at a specific domicile.
        /// </summary>
        /// <param name="domicileID"></param>
        /// <returns></returns>
        public List<User> GetByDomicileID(int domicileID)
        {
            List<User> DomicileUsers = new();
            List<int> UserIDs = new();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("select FROM MapDomicileID WHERE DomicileID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", domicileID);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserIDs.Add((int)reader["UserID"]);
                        }
                    }
                    var command2 = new SqlCommand("select FROM Users WHERE UserID = @UserID", connection);
                    command2.Parameters.AddWithValue("@UserID", UserIDs);
                    using (var reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User(

                            (int)reader["UserID"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (string)reader["Email"],
                            (string)reader["Phone"],
                            (string)reader["Password"],
                            (bool)reader["IsAdmin"]

                                );
                            List<int> domicileIDs = new List<int>();
                            var command3 = new SqlCommand("SELECT * FROM MapDomicileID WHERE UserID = @Id", connection);
                            command3.Parameters.AddWithValue("@Id", UserIDs);
                            using (var reader3 = command3.ExecuteReader())
                            {
                                while (reader3.Read())
                                {
                                    domicileIDs.Add((int)reader3["DomicileID"]);
                                }
                            }
                            user.DomicileID = domicileIDs;
                            DomicileUsers.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
                return DomicileUsers;
            }
        }
        /// <summary>
        /// Get User by UserID from the Users database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetByID(int id)
        {
            Console.WriteLine("hep1");
            using (var connection = new SqlConnection(_connectionString))
            {
                User IDuser = new();
                try
                {
                    var command = new SqlCommand("SELECT * FROM Users WHERE UserID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        Debug.WriteLine("ID");
                        IDuser.UserID = ((int)reader["UserID"]);
                        Debug.WriteLine("FN");
                        IDuser.FirstName = ((string)reader["FirstName"]);
                        Debug.WriteLine("LN");
                        IDuser.LastName = ((string)reader["LastName"]);
                        Debug.WriteLine("EM");
                        IDuser.Email = ((string)reader["Email"]);
                        Debug.WriteLine("Phone");
                        IDuser.Phone = ((string)reader["Phone"]);
                        Debug.WriteLine("Pass");
                        IDuser.Password = ((string)reader["Password"]);
                        Debug.WriteLine("IsAdmin");
                        IDuser.IsAdmin = ((bool)reader["IsAdmin"]);

                        List<int> domicileIDs = new List<int>();
                        var command2 = new SqlCommand("SELECT * FROM MapDomicileID WHERE UserID = @Id", connection);
                        command2.Parameters.AddWithValue("@Id", id);
                        using (var reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                domicileIDs.Add((int)reader2["DomicileID"]);
                            }
                        }
                        IDuser.DomicileID = domicileIDs;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
                return IDuser;
            }
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
        public User CheckPassword(int userID, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {

                    var command = new SqlCommand("SELECT * FROM Users WHERE UserID = @ID and Password = @Password", connection);
                    command.Parameters.AddWithValue("@ID", userID);
                    command.Parameters.AddWithValue("@Password", password);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        User user = new();
                        if (reader.Read())
                        {
                            user.UserID = (int)reader["UserID"];
                            Debug.WriteLine("ID");
                            user.UserID = ((int)reader["UserID"]);
                            Debug.WriteLine("FN");
                            user.FirstName = ((string)reader["FirstName"]);
                            Debug.WriteLine("LN");
                            user.LastName = ((string)reader["LastName"]);
                            Debug.WriteLine("EM");
                            user.Email = ((string)reader["Email"]);
                            Debug.WriteLine("Phone");
                            user.Phone = ((string)reader["Phone"]);
                            Debug.WriteLine("Pass");
                            user.Password = ((string)reader["Password"]);
                            Debug.WriteLine("IsAdmin");
                            user.IsAdmin = ((bool)reader["IsAdmin"]);
                        }

                        List<int> domicileIDs = new List<int>();
                        var command2 = new SqlCommand("SELECT * FROM MapDomicileID WHERE UserID = @Id", connection);
                        command2.Parameters.AddWithValue("@Id", userID);
                        using (var reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                domicileIDs.Add((int)reader2["DomicileID"]);
                            }
                        }
                        user.DomicileID = domicileIDs;
                        return user;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Catch caught a bug in Repo");
                    User user = new();
                    return user;
                }
                finally
                {
                    connection.Close();
                }

            }

        }
    }

}
