using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlexShare.Dashboard.UI.ViewModel
{
    internal class DashboardViewModel
    {
        //this is the view mode for the dashboard in this we will be fetaching the details from the models and then storing it in the viewmodel and then we will be binding to the view of the application 

        //defining the list of users 
        public List<User> ListOfUsers { get; set; }
        public string TestingVariable { get; set; }

        //defining the constructor for the dashboardviewmodel
        public DashboardViewModel()
        {
            ListOfUsers = new List<User>();
            User user1 = new User("Rupesh Kumar", "Presenting");
            User user2 = new User("Shubham Raj", "Presenting");
            User user3 = new User("Hrishi Raaj", "Presenting");
            User user4 = new User("Saurabh kumar", "Not Presenting");
            User user5 = new User("Aditya Agarwal", "Not Presenting");
            ListOfUsers.Add(user1);
            ListOfUsers.Add(user2);
            ListOfUsers.Add(user3);
            ListOfUsers.Add(user4);
            ListOfUsers.Add(user5);
            //just adding the random comment for testing the tagging of the commit for this purpose 
            TestingVariable = "Hi this is rupesh and i am implementing the dashboard UI for this purpose";
        }

    }
}
