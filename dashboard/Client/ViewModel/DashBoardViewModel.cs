using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    internal class DashBoardViewModel
    {
        //this is the view mode for the dashboard in this we will be fetaching the details from the models and then storing it in the viewmodel and then we will be binding to the view of the application 

        //defining the list of users 
        public List<User> ListOfUsers { get; set; }


        //defining the constructor for the dashboardviewmodel
        public DashBoardViewModel()
        { 
            ListOfUsers = new List<User>();
            User user1 = new User("User1", true);
            User user2 = new User("User2", true);
            User user3 = new User("User3", false);
            User user4 = new User("User4", false);
            ListOfUsers.Add(user1);
            ListOfUsers.Add(user2);
            ListOfUsers.Add(user3);
            ListOfUsers.Add(user4);
        }
    }
}
