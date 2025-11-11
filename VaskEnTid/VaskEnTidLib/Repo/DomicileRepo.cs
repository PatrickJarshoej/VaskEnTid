using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;

namespace VaskEnTidLib.Repo
{

    public class DomicileRepo: IDomicileRepo
    {
        
        private List<Domicile> _domiciles;
        private string _connectionString;
        public DomicileRepo()
        {
            _connectionString = "Data Source=mssql15.unoeuro.com;User ID=arvedlund_com;Password=BdpAFfg62xzDnR3wkcht;Encrypt=False; Database=arvedlund_com_db_vask_en_tid; Command Timeout=30;MultipleActiveResultSets=true;";
        }
        public void AddUserIDByDomID(int userID, int domicileID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("INSERT INTO MapDomicileID(DomicileID, UserID) VALUES (@DomicileID @UserID)", connection);
                    command.Parameters.AddWithValue("@DomicileID", domicileID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in Add() in DomicileRepo");
                    Debug.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void RemoveUserByID(int userID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("DELETE FROM MapDomicileID WHERE UserID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", userID);
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
        public void Add(Domicile theDomicile)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("INSERT INTO Domiciles(RoadName, Floor, Door, PostalCode, City, Region, Country) VALUES (@RoadName, @Floor, @Door, @PostalCode, @City, @Region, @Country)", connection);
                    command.Parameters.AddWithValue("@RoadName", theDomicile.Roadname);
                    command.Parameters.AddWithValue("@Floor", theDomicile.Floor);
                    command.Parameters.AddWithValue("@Door", theDomicile.Door);
                    command.Parameters.AddWithValue("@PostalCode", theDomicile.Postalcode);
                    command.Parameters.AddWithValue("@City", theDomicile.City);
                    command.Parameters.AddWithValue("@Region", theDomicile.Region);
                    command.Parameters.AddWithValue("@Country", theDomicile.Country);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in Add() in DomicileRepo");
                    Debug.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Domicile> GetAll() 
        {
            var domiciles = new List<Domicile>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("SELECT * FROM Domiciles", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var domicile = new Domicile
                            (
                                (string)reader["Roadname"],
                                (int)reader["Postalcode"],
                                (string)reader["Floor"],
                                (string)reader["Door"],
                                (string)reader["City"],
                                (string)reader["Region"],
                                (string)reader["Country"],
                                GetUserIDs((int)reader["DomicileID"], connection),
                                (int)reader["DomicileID"],
                                GetTally((int)reader["DomicileID"], connection)
                            );
                            domiciles.Add(domicile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in GetAll() in DomicileRepo");
                    Debug.WriteLine($"Error: {ex}");
                }
                finally { connection.Close(); }


            }
            return domiciles;
        }

        public Domicile GetByID(int id)
        {
            Domicile domicile = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("SELECT * FROM Domiciles, WHERE DomicileID=@Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            domicile = new Domicile
                            (

                                (string)reader["Roadname"],
                                (int)reader["Postalcode"],
                                (string)reader["Floor"],
                                (string)reader["Door"],
                                (string)reader["City"],
                                (string)reader["Region"],
                                (string)reader["Country"],
                                GetUserIDs((int)reader["DomicileID"], connection),
                                (int)reader["DomicileID"],
                                GetTally((int)reader["DomicileID"], connection)
                            );
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in GetByID() in DomicileRepo");
                    Debug.WriteLine($"Error: {ex}");
                }
                finally { connection.Close(); }

            }
            return domicile;
        }
        private List<int> GetUserIDs(int id, SqlConnection connection)
        {
            List<int> ids = new();
            Debug.WriteLine(id);
            var command = new SqlCommand("select * from MapDomicileID where DomicileID=@ID", connection);
            command.Parameters.AddWithValue("@ID", id);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine((int)reader["UserID"]);
                        ids.Add((int)reader["UserID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetUserIDs() in DomicileRepo");
                Debug.WriteLine($"Error: {ex}");
            }
            return ids;
        }
        private double GetTally(int id, SqlConnection connection)
        {
            double tally = 0;
            var command = new SqlCommand("select * from DomicileTally where DomicileID=@ID", connection);
            command.Parameters.AddWithValue("@ID", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tally = (double)reader["Pricetally"];
                }
            }
            return tally;
        }
        public Domicile GetByUserID(int userID)
        {
            Domicile domicile = null;
            int domicileID=0;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("SELECT * FROM MapDomicileID, WHERE UserID=@Id", connection);
                    command.Parameters.AddWithValue("@Id", userID);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            domicileID = (int)reader["DomicileID"];
                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in GetByUserID() in DomicileRepo");
                    Debug.WriteLine($"Error: {ex}");
                }
                finally { connection.Close(); }
                domicile=GetByID(domicileID);

            }
            return domicile;

        }
        public void Update(Domicile theDomicile)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("UPDATE Domiciles SET RoadName=@roadName, Floor=@floor, Door=@door, PostalCode=@postalcode, City=@city, Region=@region, Country=@country WHERE DomicileID=@DomicileID ", connection);
                    command.Parameters.AddWithValue("@roadName", theDomicile.Roadname);
                    command.Parameters.AddWithValue("@floor", theDomicile.Floor);
                    command.Parameters.AddWithValue("@door", theDomicile.Door);
                    command.Parameters.AddWithValue("@postalCode", theDomicile.Postalcode);
                    command.Parameters.AddWithValue("@city", theDomicile.City);
                    command.Parameters.AddWithValue("@region", theDomicile.Region);
                    command.Parameters.AddWithValue("@country", theDomicile.Country);
                    command.Parameters.AddWithValue("@DomicileID", theDomicile.DomicileID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in Add() in DomicileRepo");
                    Debug.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void DeleteByID(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("DELETE FROM Domiciles WHERE DomicileID = @ID", connection);
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
        public double CalculatePriceTallyByID(double newCost, int id) 
        {
            double tally = newCost;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("Update DomicileTally SET PriceTally=@priceTally WHERE ID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    {
                        tally+=GetTally(id, connection);
                        command.Parameters.AddWithValue("@priceTally", tally);
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in CalcualatePriceTallyByID() in DomicileRepo");
                    Debug.WriteLine($"Error: {ex}");
                }
                finally { connection.Close(); }

            }
            return tally;
        }

    }
}
