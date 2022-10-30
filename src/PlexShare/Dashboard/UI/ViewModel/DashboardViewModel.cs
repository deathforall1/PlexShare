using Client.Models;
using LiveCharts;
using LiveCharts.Defaults;
using PlexShare.Dashboard.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Markup;

namespace PlexShare.Dashboard.UI.ViewModel
{
    internal class DashboardViewModel : INotifyPropertyChanged
    {
        //this is the view mode for the dashboard in this we will be fetaching the details from the models and then storing it in the viewmodel and then we will be binding to the view of the application 
        //ObservableCollection  for storing the list of pariticipants and their status of screensharing
        public ObservableCollection<User> participantsList { get; set; }
        public string TestingVariable { get; set; }

       //ObservableCollection for storing usercount at every time stamp 
        public ObservableCollection<UserCountVsTimeStamp> userCountVsTimeStamps { get; set; }

        //ObservableCollection for storing the number of chat count for each user 
        public ObservableCollection<UserIdVsChatCount> userIdVsChatCounts { get; set; }

        //storing the attentive and non attentive users in the meeting 
        public int attentiveUsers { get; set; }
        public int nonAttentiveUsers { get; set; }

      
        //constructor for view model 
        public DashboardViewModel()
        {
            
            
            //initialising participantsList 
            participantsList = new ObservableCollection<User>();
            User user1 = new User(1, "Rupesh Kumar", "Presenting");
            User user2 = new User(2, "Shubham Raj", "Presenting");
            User user3 = new User(3, "Hrishi Raaj", "Presenting");
            User user4 = new User(4, "Saurabh kumar", "Not Presenting");
            User user5 = new User(5, "Aditya Agarwal", "Not Presenting");
            participantsList.Add(user1);
            participantsList.Add(user2);
            participantsList.Add(user3);
            participantsList.Add(user4);
            participantsList.Add(user5);
           


            //initialising userCountVsTimeStamps
            userCountVsTimeStamps = new ObservableCollection<UserCountVsTimeStamp>()
            {
                new UserCountVsTimeStamp { UserCount = 10, TimeStamp = 1.0},
                new UserCountVsTimeStamp  { UserCount = 20, TimeStamp = 2.0 },
                new UserCountVsTimeStamp { UserCount= 30, TimeStamp = 3.0 },
                new UserCountVsTimeStamp { UserCount = 40, TimeStamp = 4.0 }
            };


            //initialising the uservschatcount collection 
            userIdVsChatCounts = new ObservableCollection<UserIdVsChatCount>(){
                 new UserIdVsChatCount { userId = 1, chatCount = 10},
                new UserIdVsChatCount  { userId = 2, chatCount = 12 },
                new UserIdVsChatCount { userId= 3, chatCount = 13 },
                new UserIdVsChatCount { userId = 4, chatCount = 4 }
            };



            attentiveUsers = 60;
            nonAttentiveUsers = 100 - attentiveUsers;
           

        }

        //public event PropertyChangedEventHandler? PropertyChanged;
        //the following function notifies the view whenever the property changes on the viewmodel 
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
