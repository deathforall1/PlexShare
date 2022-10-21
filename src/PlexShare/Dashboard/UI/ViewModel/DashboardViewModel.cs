using Client.Models;
using LiveCharts;
using PlexShare.Dashboard.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Markup;

namespace PlexShare.Dashboard.UI.ViewModel
{
    internal class DashboardViewModel
    {
        //this is the view mode for the dashboard in this we will be fetaching the details from the models and then storing it in the viewmodel and then we will be binding to the view of the application 

        //defining the list of users 
        public List<User> ListOfUsers { get; set; }
        public string TestingVariable { get; set; }

        //deifining the list to store the number of userlist at a given time 
        //public List<int> UsersCounList { get; set; }

        ////defining the list to store the time stamp 
        //public List<double> TimeStamp { get; set; }
        
        public List<UserCountVsTimeStamp> userCountVsTimeStamps { get; set; }
        public int attentiveUsers { get; set; }
        public int nonAttentiveUsers { get; set; }

        //public ObservableCollection<AttentiveNotAttentive> AttentiveNotAttentiveData { get; set; }


        //public List<int> x{ get; set; }
        //public List<int> y{ get; set; }

        //defining the constructor for the dashboardviewmodel
        public DashboardViewModel()
        {
            
            
            //DataContext = this;
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

            userCountVsTimeStamps = new List<UserCountVsTimeStamp>()
            {
                new UserCountVsTimeStamp { UserCount = 10, TimeStamp = 1.0},
                new UserCountVsTimeStamp  { UserCount = 20, TimeStamp = 2.0 },
                new UserCountVsTimeStamp { UserCount= 30, TimeStamp = 3.0 },
                new UserCountVsTimeStamp { UserCount = 40, TimeStamp = 4.0 }
            };


            attentiveUsers = 60;
            nonAttentiveUsers = 100 - attentiveUsers;
            //AttentiveNotAttentiveData = new ObservableCollection<AttentiveNotAttentive>()
            //{
            //    new AttentiveNotAttentive { field = "Attentive" , numberOfStudents = 10 },
            //    new AttentiveNotAttentive { field = "Non Attentive" , numberOfStudents = 20}
            //};
        
            ////allocating the memory for the usercount list and the time stamp list 
            //UsersCounList = new List<int>()
            //{ 
            //    10, 20, 30, 40 , 50, 60
            //};
            //TimeStamp = new List<double>()
            //{ 
            //    1.0, 2.0, 3.0, 4.0, 5.0, 6.0
            //};

        }

    }
}
