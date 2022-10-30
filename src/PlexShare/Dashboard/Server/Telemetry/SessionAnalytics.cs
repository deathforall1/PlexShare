using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlexShare.Dashboard.Server.Telemetry
{
    public class SessionAnalytics
    {
        public Dictionary<int, int> chatCountForEachUser;

        public List<int> inSincereMembers;

        public Dictionary<DateTime, int> userCountAtAnyTime;
    }
}
