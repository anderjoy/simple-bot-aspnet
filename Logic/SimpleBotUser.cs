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
            FiapMongo mongo = new FiapMongo();

            mongo.InsertMessage(message);


            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            return null;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
        }
    }
}