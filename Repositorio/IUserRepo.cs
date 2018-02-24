namespace SimpleBot.Repositorio
{
    public interface IUserRepo
    {
        void InsertMessage(Message message);

        UserProfile FindProfile(string Id);

        void UpdateProfile(string id, UserProfile profile);
    }
}