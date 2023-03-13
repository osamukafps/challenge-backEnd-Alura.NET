using AluraFlixChallenge.API.Entities;

namespace AluraFlixChallenge.API.Repository
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Name = "Samuel", Password = "samuel123", Role = "Manager" });
            users.Add(new User { Id = 1, Name = "Thanos", Password = "thanos123", Role = "User" });

            return users
                   .Where(x => x.Name.ToLower() == username.ToLower() && x.Password == password)
                   .FirstOrDefault();
        }
    }
}
