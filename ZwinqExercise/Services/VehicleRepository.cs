using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using ZwinqExercise.Models;
using System.Data;
namespace ZwinqExercise.Services
{
    public class VehicleRepository
    {
        private const string Cachekey = "ContactStore";
        private const string jsonFile = @"C:\Users\aemon\source\repos\ZwinqExercise\DummyData.txt";
        private SqlConnection sqlConnection = new SqlConnection("Data Source =(local); Initial Catalog=ZwinqExerciseDB; Integrated Security=SSPI");


        public VehicleRepository()
        {

        }
        public List<Vehicle> GetAllVehicles()
        {
            SqlDataReader sql = null;
            List<Vehicle> results = new List<Vehicle>();
            try
            {
                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("select * from ZwinqExerciseDB.dbo.Zwinq_Vehicle_Table", sqlConnection);

                sql = cmd.ExecuteReader();

                while (sql.Read())
                {
                    results.Add(new Vehicle
                    {
                        Id = sql["vehicle_id"].ToString(),
                        LicensePlate = sql["license_plate"].ToString(),
                        VehicleManufacturer = sql["vehicle_brand"].ToString(),
                        VehicleType = sql["vehicle_type"].ToString(),
                        FuelType = sql["fuel_type"].ToString()
                    });
                }
                return results;



            }
            finally
            {
                if(sql != null)
                {
                    sql.Close();
                }
                if(sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }



        }
        public bool CreateNewVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                try
                {
                    SqlCommand sql = new SqlCommand("Insert into ZwinqExerciseDB.dbo.Zwinq_Vehicle_Table values('"
                        + vehicle.Id +"','" 
                        + vehicle.LicensePlate + "','"
                        + vehicle.VehicleManufacturer + "','"
                        + vehicle.VehicleType + "','"
                        + vehicle.FuelType + "')", sqlConnection);
                    sqlConnection.Open();
                    sql.CommandType = CommandType.Text;
                    int i = sql.ExecuteNonQuery();
                    sqlConnection.Close();
                    if(i != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            return false;
        }

        public bool DeleteExistingVehicle(Vehicle vehicle)
        {
            try
            {
                SqlCommand sql = new SqlCommand("DELETE from ZwinqExerciseDB.dbo.Zwinq_Vehicle_Table where vehicle_id ='" + vehicle.Id + "'", sqlConnection);
                sqlConnection.Open();
                int i = sql.ExecuteNonQuery();
                sqlConnection.Close();
                if(i != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return false;
        }


    }
}