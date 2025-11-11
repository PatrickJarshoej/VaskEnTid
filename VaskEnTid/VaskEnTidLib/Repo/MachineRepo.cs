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
    public class MachineRepo : IMachineRepo
    {
        private string _connectionString;
        public MachineRepo()
        {
            _connectionString = "Data Source=mssql15.unoeuro.com;User ID=arvedlund_com;Password=BdpAFfg62xzDnR3wkcht;Encrypt=False; Database=arvedlund_com_db_vask_en_tid; Command Timeout=30;MultipleActiveResultSets=true;";
        }
        public void Add(Machine theMachine)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("INSERT INTO Machines(Type, TypeNumber, Status, Cost) VALUES (@Type, @TypeNumber, @Status, @Cost)", connection);
                    command.Parameters.AddWithValue("@Type", nameof(theMachine.Type) );
                    command.Parameters.AddWithValue("@TypeNumber", theMachine.TypeNumber);
                    command.Parameters.AddWithValue("@Status", nameof(theMachine.Status));
                    command.Parameters.AddWithValue("@Cost", theMachine.Cost);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error in Add() in MachineRepo");
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
                    var command = new SqlCommand("DELETE FROM Machines WHERE MachineID = @ID", connection);
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

        public List<Machine> GetAll()
        {
            List<Machine> machines = new List<Machine>();
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    Machine machine;
                    var command = new SqlCommand("SELECT * FROM Machines", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            machine = new(
                                (int)reader["TypeNumber"], 
                                CheckType((string)reader["Type"]), 
                                CheckStatus((string)reader["Status"]), 
                                decimal.ToDouble((decimal)reader["Cost"]), 
                                (int)reader["MachineID"]);
                            machines.Add(machine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error in MachineRepo.GetAll()");
                    Debug.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return machines;
        }

        public Model.Type CheckType(string e) //For for enum
        {
            if (e == "Washer")
            {
                return Model.Type.Washer;
            }
            else if (e == "Dryer")
            {
                return Model.Type.Dryer;
            }
            else
            {
                return Model.Type.Rollingmachine;
            }
        }
        
        public Model.Status CheckStatus(string e)
        {

            if (e == "Available")
            {
                return Model.Status.Available;
            }
            else if (e == "UnderService")
            {
                return Model.Status.UnderService;
            }
            else
            {
                return Model.Status.Unavailable;
            }
        }

        public Machine GetByID(int id)
        {
            Machine machine = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = new SqlCommand("SELECT * FROM Machine WHERE MachineID=@Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            machine = new(
                                (int)reader["TypeNumber"], 
                                CheckType((string)reader["Type"]), 
                                CheckStatus((string)reader["Status"]), 
                                decimal.ToDouble((decimal)reader["Cost"]), 
                                (int)reader["MachineID"]
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
            return machine;
        }

        public void Update(Machine theMachine)
        {
            throw new NotImplementedException();
        }
    }
}
