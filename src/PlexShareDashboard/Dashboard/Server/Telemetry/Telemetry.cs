using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlexShareDashboard.Dashboard.Server.Telemetry
{
    public class Telemetry : ITelemetry
    {

        //defining the variables to store the telemteric data 
        public Dictionary<DateTime, int> userCountVsEachTimeStamp = new Dictionary<DateTime, int>();
        public Dictionary<User,DateTime> eachUserEnterTimeInMeeting  = new Dictionary<User,DateTime>();
        public Dictionary<User, DateTime> eachUserExitTime = new Dictionary<User, DateTime>();
        public Dictionary<int, int> userIdVsChatCount = new Dictionary<int, int>();
        public List<int> listOfInSincereMembers = new List<int>();



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

            var finalSessionAnalyticsToSave = new SessionAnalytics();
            finalSessionAnalyticsToSave.chatCountForEachUser = userIdVsChatCount;
            finalSessionAnalyticsToSave.listOfInSincereMembers = listOfInSincereMembers; 



            //say everything went fine 
            return;
        }



        //function to get the useridvschatcount 
        public 
    }
}
