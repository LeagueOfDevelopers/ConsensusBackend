using System;

namespace ConsensusLibrary.UserContext
{
    public class UserProfile
    {
        public string About { get; internal set; }
        public string AvatarLink { get; internal set; }
        public int Reputation { get; internal set; }
        public DateTimeOffset RegistrationDate { get; }

        public UserProfile(DateTimeOffset registrationDate)
        {
            RegistrationDate = registrationDate;
        }
    }
}