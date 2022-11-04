using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlexShareContent;

namespace PlexShareDashboard.Dashboard.Server.Summary
{
    public interface ISummarizer
    {
        string GetSummary(ChatContext[] chats);
        bool SaveSummary(ChatContext[] chats);

    }
}
