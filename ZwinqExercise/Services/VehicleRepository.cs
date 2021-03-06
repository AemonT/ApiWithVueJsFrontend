﻿using Newtonsoft.Json;
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
        /// <summary>
        /// Get all the vehicles
        /// </summary>
        /// <returns>Returns all the vehicles in the database</returns>
        public List<Vehicle> GetAllVehicles()
        {
            SqlDataReader sql = null;
            List<Vehicle> results = new List<Vehicle>();
            //try to make the connection and retrieve all the data in the database
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
            //close the connection
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
        /// <summary>
        /// Uses the data it gets from the frontend and inserts it in the database
        /// </summary>
        /// <param name="vehicle">The new vehicle that got send from the frontend</param>
        /// <returns>All the vehicles in the database</returns>
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
        /// <summary>
        /// Searches on fueltype and checks if the fueltype given is one of the three predertimend (The frontend part doesnt work).
        /// TODO: fix the frontend part
        /// </summary>
        /// <param name="vehicle">The fueltype that is asked</param>
        /// <returns>List of the selected vehicles</returns>
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
        /// <summary>
        /// Deletes a vehicle based on the id.
        /// </summary>
        /// <param name="vehicle">The vehicle that needs to be deleted</param>
        /// <returns>The entire list of vehicles after the given one got deleted</returns>
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