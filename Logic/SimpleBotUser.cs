using SimpleBot.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        //private static IUserRepo _repo = new UserMongo();
        private static IUserRepo _repo = new UserMSSQL(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BOT;Integrated Security=True;MultipleActiveResultSets=True");

        public static string Reply(Message message)
        {
            //FiapMongo mongo = new FiapMongo();

            //mongo.InsertMessage(message);

            string userId = message.Id;

            var perfil = GetProfile(userId);

            perfil.Visitas++;

            SetProfile(userId, perfil);

            return $"{message.User} conversou {perfil.Visitas} vezes";
        }

        public static UserProfile GetProfile(string id)
        {
            return _repo.FindProfile(id);
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            _repo.UpdateProfile(id, profile);
        }
    }
}