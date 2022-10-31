using Client.Models;
using PlexShare.Dashboard.Server.SessionManagement;
using PlexShare.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard;

namespace PlexShareDashboard.Dashboard.Server.Telemetry
{
    public class Telemetry : ITelemetry, ITelemetryNotifications
    {
        //private readonly ITelemetryPe
        private readonly ITelemetrySessionManager serverSessionManager = SessionManagerFactory.GetServerSessionManager();

        //defining the variables to store the telemteric data 
        public Dictionary<DateTime, int> userCountVsEachTimeStamp = new Dictionary<DateTime, int>();
        public Dictionary<User,DateTime> eachUserEnterTimeInMeeting  = new Dictionary<User,DateTime>();
        public Dictionary<User, DateTime> eachUserExitTime = new Dictionary<User, DateTime>();
        public Dictionary<int, int> userIdVsChatCount = new Dictionary<int, int>();
        public List<int> listOfInSincereMembers = new List<int>();

        //constructor for telemetry module 
        public Telemetry()
        {
            //we have to subscribe to the ITelemetryNotifications 
            serverSessionManager.Subscribe(this);
            
        }


        public SessionAnalytics GetTelemetryAnalytics(string allChatMessages)
        {
            //here goes the implementation 
            SessionAnalytics sessionAnalytics = new SessionAnalytics(); 
            return sessionAnalytics;
        }

        //function fetch the details from the chatcontext and then giving it to persistent to save the analytics on the server 
        public void SaveAnalytics(string allChatMessages)
        {
            GetUserIdVsChatCount(allChatMessages);
            GetListOfInsincereMembers();
            var currTotalUser = 0;
            var currTotalChatCount = 0;

            //using the for loop to find these values 
            foreach(var eachUser in userIdVsChatCount)
            {
                currTotalChatCount = currTotalChatCount + eachUser.Value;
                currTotalUser = currTotalUser + 1;
            }


            var finalSessionAnalyticsToSave = new SessionAnalytics();
            finalSessionAnalyticsToSave.chatCountForEachUser = userIdVsChatCount;
            finalSessionAnalyticsToSave.listOfInSincereMembers = listOfInSincereMembers;
            finalSessionAnalyticsToSave.userCountVsTimeStamp = userCountVsEachTimeStamp;
            finalSessionAnalyticsToSave.sessionSummary.chatCount = currTotalChatCount;
            finalSessionAnalyticsToSave.sessionSummary.userCount = currTotalUser;
               

            //calling the persistent module to save these analytics 
            //persistence.Save(finalSessionAnalyticsToSave)


            //say everything went fine 
            return;
        }

        public void GetUserIdVsChatCount(string allMesssages)
        {
            //say everything went fine 
            return;
        }

        public void GetListOfInsincereMembers()
        {

            //say everything went fine 
            return;
        }

        public void OnAnalyticsChanged(SessionData newSession)
        {
            //we have to update the telemetric analytics
        }

        //function to get the useridvschatcount 
        public 
    }
}
