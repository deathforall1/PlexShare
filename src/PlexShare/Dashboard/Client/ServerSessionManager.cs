// This file contains the implementation of Server session manager.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Content;
using Dashboard.Server.Summary;
using Dashboard.Server.Telemetry;
using Networking;
using ScreenSharing;
using Whiteboard;

namespace Dashboard.Server.SessionManagement
{
    // Delegate for the MeetingEnded event
    public delegate void NotifyEndMeet();

    public class ServerSessionManager : ITelemetrySessionManager, IUXServerSessionManager, INotificationHandler
    {
        private readonly ICommunicator _communicator;
        private readonly IContentServer _contentServer;
        private readonly ISerializer _serializer;

        private readonly SessionData _sessionData;
        private readonly ISummarizer _summarizer;

        private readonly List<ITelemetryNotifications> _telemetrySubscribers;

        private readonly string moduleIdentifier;
        private readonly bool testmode;  
        private MeetingCredentials _meetingCredentials;
        private SessionAnalytics _sessionAnalytics;
        private string _sessionSummary;
        private ITelemetry _telemetry;
        public bool summarySaved;
        private int userCount;
        private ScreenShareServer _screenShareServer;

        //Constructor for the ServerSessionManager.
        //It initialises whiteboard module,content module, screenshare module,
        //networking module,summary module, telemetry module
        //and creates a list for telemetry subscribers .
        //Session manager is also subscribes to the communicator for notifications.
        //It maintains the userCount.
        public ServerSessionManager()
        {
          
            moduleIdentifier = "Dashboard";
            summarySaved = false;
            _sessionData = new SessionData();
            _serializer = new Serializer();
            _telemetrySubscribers = new List<ITelemetryNotifications>();
            _summarizer = SummarizerFactory.GetSummarizer();

            userCount = 0;

            _communicator = CommunicationFactory.GetCommunicator(false);
            _communicator.Subscribe(moduleIdentifier, this);

            //_telemetry = new Telemetry.Telemetry();
            _ = ServerBoardCommunicator.Instance;
            _screenShareServer = ScreenShareFactory.GetScreenShareServer();
            _contentServer = ContentServerFactory.GetInstance();
        }


        //constructor for testing to be added 


        // This function is called by the networking module when a user joins the meeting.
        // The  SocketObject received from the networking module is then passed again but with a unique ID to identify object uniquely.
        public void OnClientJoined<T>(T socketObject)
        {
            lock (this)
            {
                userCount += 1;
                if (userCount == 1)
                    _telemetry = testmode ? new Telemetry.Telemetry(this) : TelemetryFactory.GetTelemetryInstance();
                UserData tempUser = new("dummy", userCount);
                _communicator.AddClient(userCount.ToString(), socketObject);
                SendDataToClient("newID", null, null, null, tempUser, userCount);
            }

        }

        //     This function is called by the networking module when the user is disconnected from the meet.
        public void OnClientLeft(string userIDString)
        {
            var userIDInt = int.Parse(userIDString);
            RemoveClientProcedure(null, userIDInt);
        }

        //     Networking module calls this function once the data is sent from the client side.
        //     The SerializedObject is the data sent by the client module which is first deserialized and processed accordingly

        public void OnDataReceived(string serializedObject)
        {
            if (serializedObject == null)
            {
                Trace.WriteLine("[Server Dashboard] Null received from client");
                return;
            }

            // the object is obtained by deserializing the string and handling the cases 
            // based on the 'eventType' field of the deserialized object. 
            var deserializedObj = _serializer.Deserialize<ClientToServerData>(serializedObject);

            // If a null object or username is received, return without further processing.
            if (deserializedObj == null || deserializedObj.username == null)
            {
                Trace.WriteLine("[Server Dashboard] Null object provided by the client.");
                return;
            }

            switch (deserializedObj.eventType)
            {
                case "addClient":
                    ClientArrivalProcedure(deserializedObj);
                    return;

                case "getSummary":
                    GetSummaryProcedure(deserializedObj);
                    return;

                case "getAnalytics":
                    GetAnalyticsProcedure(deserializedObj);
                    return;

                case "removeClient":
                    RemoveClientProcedure(deserializedObj);
                    return;

                case "endMeet":
                    EndMeetProcedure(deserializedObj);
                    return;

                default:
                    Trace.WriteLine("[Server Dashboard] Incorrect Event type specified");
                    return;
            }
        }

    }