using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBot.Mongo;

namespace SimpleBot
{
    public class SimpleBotUser
    {       
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
            FiapMongo mongo = new FiapMongo();

            return mongo.FindProfile(id);
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            FiapMongo mongo = new FiapMongo();

            mongo.UpdateProfile(id, profile);
        }
    }
}