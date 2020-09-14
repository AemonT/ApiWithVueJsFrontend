using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZwinqExercise.DatabaseConnection
{
    /// <summary>
    /// An interface that you can use to set up database connections. 
    /// Every class that inherits from this interface should be put on the .gitignore list
    /// </summary>
    public interface IConnectionProvidor
    {
        string connectionPath {get; set;}
        string username { get; set; }
        string password { get; set; }
       

    }
}