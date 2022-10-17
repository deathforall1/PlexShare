﻿
/// This file contains the interface for UX to access Client session manager's methods and fields.

namespace Dashboard.Server.SessionManagement
{
    public interface IUXServerSessionManager
    {
        /// <summary>
        ///     Returns the credentials required to
        ///     Join or start the meeting
        /// </summary>
        /// <returns> A MeetingCredentials Object </returns>
        MeetingCredentials GetPortsAndIPAddress();

        /// <summary>
        ///     Event to notify the UX Server about the end of the meeting.
        /// </summary>
        public event NotifyEndMeet MeetingEnded;
    }
}