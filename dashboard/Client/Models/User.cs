using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class User
    {
        //in this we will store the data about the user
        public string userName { get; set; }
        public string status { get; set; }

        //defining the constructor for the user 
        public User(string userName, string status)
        {
            userName = userName;
            status = status;
        }
    }
}
