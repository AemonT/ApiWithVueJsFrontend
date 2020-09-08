using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZwinqExercise.DatabaseConnection
{
    public class LocalDatabaseConnection : IConnectionProvidor
    {
        private string connUrl;
        private string usernameValue;
        private string passwordValue;
        public string connectionPath { get { return connUrl; } set { connUrl = value; } }
        public string username { get { return usernameValue; } set { usernameValue = value; } }
        public string password { get { return passwordValue; } set { passwordValue = value; } }

        public LocalDatabaseConnection()
        {
            this.connectionPath = "Data Source =(local); Initial Catalog=ZwinqExerciseDB; Integrated Security=SSPI";
        }
    }
}