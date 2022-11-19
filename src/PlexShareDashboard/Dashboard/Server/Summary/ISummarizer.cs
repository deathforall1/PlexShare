/// <author>Morem Jayanth Kumar</author>
/// <created>3/11/2022</created>
/// <summary>
///		This file represents the main functions of the summarizer
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlexShareContent.DataModels;

namespace PlexShareDashboard.Dashboard.Server.Summary
{
    public interface ISummarizer
    {
        string GetSummary(ChatThread[] chats);
        bool SaveSummary(ChatThread[] chats);

    }
}
