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
        public void Add()
        {
            throw new NotImplementedException();
        }

        public void DeleteByID()
        {
            throw new NotImplementedException();
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
                            machine = new((int)reader["TypeNumber"], CheckType((string)reader["Type"]), CheckStatus((string)reader["Status"]), decimal.ToDouble((decimal)reader["Cost"]), (int)reader["MachineID"]);
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
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
