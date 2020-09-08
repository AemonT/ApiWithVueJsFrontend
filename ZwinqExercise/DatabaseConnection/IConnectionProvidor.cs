using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZwinqExercise.DatabaseConnection
{
    public interface IConnectionProvidor
    {
        string connectionPath {get; set;}
        string username { get; set; }
        string password { get; set; }
       

    }
}