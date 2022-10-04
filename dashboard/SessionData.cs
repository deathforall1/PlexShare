
using System.Collections.Generic;

namespace Dashboard
{

    ///     This class is used to store the data about the
    ///     current session
  
    public class SessionData
    {
        // the List of users in the meeting 
        public List<UserData> users;

        // default SessionMode is LabMode
        public string SessionMode = "LabMode";



        ///     Constructor to initialise and empty list of users
        public SessionData()
        {
            if (users == null) users = new List<UserData>();
        }

        ///     Adds a user to the list of users in the session
        /// <param name="user"> An instance of the UserData class </param>
        public void AddUser(UserData user)
        {
            users.Add(user);
        }

        ///     Overrides the ToString() method to pring the sessionData object for testing, debugging and logging.
        /// Returns a string which contains the data of each user separated by a newline character 
        public override string ToString()
        {
            var output = "";
            for (var i = 0; i < users.Count; ++i)
            {
                output += users[i].ToString();
                output += "\n";
            }

            return output;
        }

        
        ///     Removes the user from the user list in the sessionData.  
        /// The UserID of the user who is to be removed 
        ///   An optional paramter indicating the name of the user.  
     
        public UserData RemoveUserFromSession(int userID, string username = null)
        {
            // Check if the user is in the list and if so, then remove it and return true
            for (var i = 0; i < users.Count; ++i)
                if (users[i].userID.Equals(userID))
                    lock (this)
                    {
                        UserData removedUser = new(users[i].username, users[i].userID);
                        users.RemoveAt(i);
                        return removedUser;
                    }

            return null;
        }
    }
}