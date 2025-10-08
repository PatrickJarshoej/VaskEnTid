using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;
using Microsoft.Data.SqlClient;

namespace VaskEnTidLib.Repo
{
    internal class BookingRepo : IBookingRepo
    {
        private string _connectionString;
        private List<Booking> _bookings;
        public BookingRepo()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=VaskEnTidDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;";
        }
        public List<Booking> bookings;
        public void Add(Booking booking)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("INSERT INTO Bookings (DateAndTime, DomicileID, MachineIDs, Duration, TotalCost) VALUES (@DateAndTime, @DomicileID, @MachineIDs, @Duration, @TotalCost)", connection);
                    command.Parameters.AddWithValue("@DateAndTime", booking.DateAndTime);
                    command.Parameters.AddWithValue("@DomicileID", booking.DomicileID);
                    command.Parameters.AddWithValue("@MachineIDs", booking.MachineIDs);
                    command.Parameters.AddWithValue("@Duration", booking.Duration);
                    command.Parameters.AddWithValue("@TotalCost", booking.TotalCost);
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

        public double CalcCost(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void DeleteByID(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("DELETE FROM Bookings WHERE ID = @ID", connection);
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

        public List<Booking> GetAll()
        {
            List<Booking> bookings = new List<Booking>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {

                    var command = new SqlCommand("SELECT * FROM Bookings", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var booking = new Booking((DateTime)reader["DateAndTime"], (int)reader["DomicileID"], (TimeOnly)reader["Duration"], (double)reader["TotalCost"], (int)reader["ID"]);
                            bookings.Add(booking);
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
            }
            throw new NotImplementedException();
        }

        public Booking GetByDomicileID(int id)
        {
            throw new NotImplementedException();
        }

        public Booking GetByID(int id)
        {
            //Booking booking;
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    try
            //    {
            //        var command = new SqlCommand("SELECT * FROM Bookings", connection);
            //        connection.Open();
            //        using (var reader = command.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                var booking = new Booking((DateTime)reader["DateAndTime"], (int)reader["DomicileID"], (int)reader["MachineIDs"], (TimeOnly)reader["Duration"], (double)reader["TotalCost"], (int)reader["ID"]);
            //                booking = booking
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.WriteLine($"Error: {ex.Message}");
            //    }
            //    finally
            //    {
            //        connection.Close();
            //    }
            //    return booking;
            //}
            throw new NotImplementedException();
        }

        public void Update(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
