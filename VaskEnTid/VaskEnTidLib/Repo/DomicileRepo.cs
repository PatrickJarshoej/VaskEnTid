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
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("INSERT INTO Domiciles(RoadName, Floor, Door, PostalCode, City, Region, Country) VALUES (@DateAndTime, @DomicileID, @MachineIDs, @Duration, @TotalCost)", connection);
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
                                (string)reader["Postalcode"],
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
                                (string)reader["Postalcode"],
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
            finally
            {
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
