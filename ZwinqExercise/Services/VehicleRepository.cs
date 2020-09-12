using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using ZwinqExercise.Models;
using System.Data;
using ZwinqExercise.DatabaseConnection;
namespace ZwinqExercise.Services
{
    public class VehicleRepository
    {

        private LocalDatabaseConnection databaseConnection = new LocalDatabaseConnection();
        private SqlConnection sqlConnection;
        private string[] filters = { "Benzine", "Diesel", "Electricity" };


        public VehicleRepository()
        {
            sqlConnection = new SqlConnection(databaseConnection.connectionPath);
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
                if (sql != null)
                {
                    sql.Close();
                }
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }



        }
        public List<Vehicle> CreateNewVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                try
                {
                    SqlCommand sql = new SqlCommand("Insert into ZwinqExerciseDB.dbo.Zwinq_Vehicle_Table values('"
                        + vehicle.Id + "','"
                        + vehicle.LicensePlate + "','"
                        + vehicle.VehicleManufacturer + "','"
                        + vehicle.VehicleType + "','"
                        + vehicle.FuelType + "')", sqlConnection);
                    if (filters.Contains(vehicle.FuelType))
                    {
                        sqlConnection.Open();
                        sql.CommandType = CommandType.Text;
                        sql.ExecuteNonQuery();
                        sqlConnection.Close();
                    }

                    return GetAllVehicles();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return GetAllVehicles();
                }
            }
            return GetAllVehicles();
        }
        public List<Vehicle> SearchOnFuelType(Vehicle vehicle)
        {
            SqlDataReader sql = null;
            List<Vehicle> results = new List<Vehicle>();
            if (vehicle != null)
            {

                if ((vehicle.FuelType != null || vehicle.FuelType != " ") && filters.Contains(vehicle.FuelType))
                {
                    try
                    {

                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand("select * from ZwinqExerciseDB.dbo.Zwinq_Vehicle_Table where fuel_type ='" + vehicle.FuelType + "'", sqlConnection);
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
                        if (sql != null)
                        {
                            sql.Close();
                        }
                        if (sqlConnection != null)
                        {
                            sqlConnection.Close();
                        }
                    }
                }
            }
            return GetAllVehicles();
        }

        public List<Vehicle> DeleteExistingVehicle(Vehicle vehicle)
        {
            try
            {
                SqlCommand sql = new SqlCommand("DELETE from ZwinqExerciseDB.dbo.Zwinq_Vehicle_Table where vehicle_id ='" + vehicle.Id + "'", sqlConnection);
                sqlConnection.Open();
                int i = sql.ExecuteNonQuery();
                sqlConnection.Close();
                return GetAllVehicles();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return GetAllVehicles();
            }

        }


    }
}