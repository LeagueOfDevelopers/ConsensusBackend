namespace ConsensusLibrary.UserContext.Views
{
    public class GetUserProfileView
    {
        public string Name { get; }
        public string Avatar { get; }
        public string About { get; }
        public int Reputation { get; }
        public string Email { get; }

        public GetUserProfileView(
            string name, 
            string avatar, 
            string about, 
            int reputation, 
            string email)
        {
            Name = name;
            Avatar = avatar;
            About = about;
            Reputation = reputation;
            Email = email;
        }
    }
}
