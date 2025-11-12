using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Model;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace VaskEnTidLib.Repo
{
    public class BookingRepo : IBookingRepo
    {
        private string _connectionString;
        private List<Booking> _bookings;
        public BookingRepo()
        {
            _connectionString = "Data Source=mssql15.unoeuro.com;User ID=arvedlund_com;Password=BdpAFfg62xzDnR3wkcht;Encrypt=False; Database=arvedlund_com_db_vask_en_tid; Command Timeout=30;MultipleActiveResultSets=true;";
        }
        //public List<Booking> bookings;
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
                    Debug.WriteLine("Error in DeleteByID() in BookingRepo");
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
                    Booking booking;
                    var command = new SqlCommand("SELECT DateAndTime, DomicileID, Duration, TotalCost, BookingID FROM Bookings", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            booking = new Booking((DateTime)reader["DateAndTime"], (int)reader["DomicileID"], (TimeSpan)reader["Duration"], decimal.ToDouble((decimal)reader["TotalCost"]), (int)reader["BookingID"]);
                            booking.MachineIDs = GetMachineIDs((int)reader["BookingID"], connection);
                            bookings.Add(booking);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"you did something wrong");
                    Debug.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return bookings;
        }

        private List<int> GetMachineIDs(int id, SqlConnection connection)
        {
            List<int> ids = new();
            Debug.WriteLine(id);
            //var command = new SqlCommand("select m.* from Machines m, MapMachineID mm where mm.BookingID = @ID and m.MachineID = mm.MachineID", connection);
            var command = new SqlCommand("select * from MapMachineID where BookingID = @ID ", connection);
            command.Parameters.AddWithValue("@ID", id);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Debug.WriteLine((int)reader["MachineID"]);
                        ids.Add((int)reader["MachineID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetMachineIDs()");
                Debug.WriteLine($"Error: {ex}");
            }
            finally
            {
            }
            return ids;
        }

        public Booking GetByDomicileID(int id)
        {
            Booking booking = null;
            int bookingID = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("SELECT * FROM Bookings WHERE DomicileID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bookingID = (int)reader["BookingID"];
                        }

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in GetByDomicileID in BookingRepo");
                    Debug.WriteLine("Error: " + ex);
                }
                finally
                {
                    connection.Close();
                }
                booking = GetByID(bookingID);
            }
            return booking;

        }

        public Booking GetByID(int id)
        {
            Booking ?booking = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("SELECT * FROM Bookings WHERE BookingID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            booking = new Booking((DateTime)reader["DateAndTime"], (int)reader["DomicileID"], (TimeSpan)reader["Duration"], decimal.ToDouble((decimal)reader["TotalCost"]), (int)reader["BookingID"]);
                            booking.MachineIDs = GetMachineIDs((int)reader["BookingID"], connection);
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
            return booking;
            //throw new NotImplementedException();
        }

        public void Update(Booking booking)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("UPDATE Bookings SET DateAndTime=@DateAndTime, Duration=@Duration, TotalCost=@TotalCost Where BookingID=@BookingID ", connection);
                    command.Parameters.AddWithValue("@BookingID", booking.BookingID);
                    command.Parameters.AddWithValue("@DateAndTime", booking.DateAndTime);
                    command.Parameters.AddWithValue("@Duration", booking.Duration);
                    command.Parameters.AddWithValue("@TotalCost", booking.TotalCost);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in Booking() in BookingRepo");
                    Debug.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
