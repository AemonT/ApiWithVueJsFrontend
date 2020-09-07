using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using ZwinqExercise.Models;
namespace ZwinqExercise.Services
{
    public class ContactRepository
    {
        private const string Cachekey = "ContactStore";
        private const string jsonFile = @"C:\Users\aemon\source\repos\ZwinqExercise\DummyData.txt";
        private SqlConnection sqlConnection = new SqlConnection("Data Source =(local); Initial Catalog=ZwinqExerciseDB; Integrated Security=SSPI");


        public ContactRepository()
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
                        VehicleFactorer = sql["vehicle_brand"].ToString(),
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
            //using (StreamReader streamReader = new StreamReader(jsonFile))
            //{
            //    string json = streamReader.ReadToEnd();
            //    var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
            //    if (contacts != null)
            //    {
            //        return contacts.ToArray();

            //    }
                

            //}


        }
        public bool SaveContact(Vehicle contact)
        {
            List<Vehicle> contacts;
            using (StreamReader streamReader = new StreamReader(jsonFile))
            {
                string json = streamReader.ReadToEnd();
                contacts = JsonConvert.DeserializeObject<List<Vehicle>>(json);
            }
            if (contact != null)
            {
                try
                {

                    using (StreamWriter file = File.CreateText(jsonFile))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        contacts.Add(contact);
                        serializer.Serialize(file, contacts);
                        return true;
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
        public Vehicle[] GetChosenContacts(Vehicle contact)
        {

            return null;
        }
    }
}