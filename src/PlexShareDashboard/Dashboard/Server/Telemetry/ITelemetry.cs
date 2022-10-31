using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlexShareDashboard.Dashboard.Server.Telemetry
{

    //this is the interface for others to use the telemetry submodule 
    public interface ITelemetry
    {
        public void SaveAnalytics(string allChatMessages);

        public SessionAnalytics GetTelemetryAnalytics(string allChatMessages);

    }
}
