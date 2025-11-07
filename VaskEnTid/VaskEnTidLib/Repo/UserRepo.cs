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
    internal class UserRepo : IUserRepo
    {
        private List<User> _users;
        private string _connectionString;
        public UserRepo()
        {
            _connectionString = "Data Source=mssql15.unoeuro.com; ID=arvedlund_com; password=BdpAFfg62xzDnR3wkcht; Database=Test;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;MultipleActiveResultSets=true;";
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
                    var command = new SqlCommand("INSERT INTO Users (FirstName, LastName, DomicileID, Email, Phone) VALUES (@FirstName, @LastName, @DomicileID, @Email, @Phone)", connection);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@DomicileID", user.DomicileID);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
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
        public void DeleteByID(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("DELETE FROM Users WHERE ID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);
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
        /// Returns all Users from Users database
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT UserID, FirstName, LastName, Email, Phone FROM Users", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User(

                            (int)reader["UserID"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (string)reader["Email"],
                            (string)reader["Phone"]

                            );
                        user.DomicileID = GetDomicileIDs((int)reader["UserID"], connection);
                        users.Add(user);
                    }
                }
                connection.Close();
            }
            return users;
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
            connection.Open();
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
            catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex}");
            }
            finally
            {
                connection.Close();
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
                                (string)reader["Phone"]

                                );
                            user.DomicileID = GetDomicileIDs((int)reader["UserID"], connection);
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
            User user = new();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("select FROM Users WHERE UserID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        user = new User(

                            (int)reader["UserID"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (string)reader["Email"],
                            (string)reader["Phone"]

                            );
                        user.DomicileID = GetDomicileIDs((int)reader["UserID"], connection);
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
            }
            return user;
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }

}
