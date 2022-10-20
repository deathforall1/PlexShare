// defining the datastructure for the Meeting Credential

namespace Dashboard
{
    public class MeetingCredentials
    {
        public string ipAddress;
        public int port;

       
        ///     Instances of this class will store the
        ///     credentials required to join/start
        
        /// <param name="address"> String parameter to store the IP Address </param>
        /// <param name="portNumber"> Int parameter for the port number </param>
        public MeetingCredentials(string address, int portNumber)
        {
            ipAddress = address;
            port = portNumber;
        }
    }
}