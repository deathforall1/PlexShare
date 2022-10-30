using System;
using System.Collections.Generic;
using System.Diagnostics;
using Content;
using Dashboard.Server.Telemetry;
using Networking;
using ScreenSharing;
using Whiteboard;

namespace Dashboard.Client.SessionManagement
{
    public delegate void NotifyEndMeet();

    public delegate void NotifyAnalyticsCreated(SessionAnalytics analytics);

    public delegate void NotifySummaryCreated(string summary);

    //     ClientSessionManager class is used to maintain the client side
    //     session data and requests from the user. It communicates to the server session manager
    //     to update the current session or to fetch summary and analytics.
    public class ClientSessionManager : IUXClientSessionManager, INotificationHandler
    {

    }
}