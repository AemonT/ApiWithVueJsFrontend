using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public ContactRepository()
        {

        }
        public Contact[] GetAllContacts()
        {


            using (StreamReader streamReader = new StreamReader(jsonFile))
            {
                string json = streamReader.ReadToEnd();
                var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
                if (contacts != null)
                {
                    return contacts.ToArray();

                }
                

            }

            
            return new Contact[]
            {
                new Contact
                {
                    Id = 0,
                    Name = "Placeholder"
                }

            };
        }
        public bool SaveContact(Contact contact)
        {
            List<Contact> contacts;
            using (StreamReader streamReader = new StreamReader(jsonFile))
            {
                string json = streamReader.ReadToEnd();
                contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
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
    }
}