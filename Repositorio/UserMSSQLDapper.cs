using System;
using System.Data.SqlClient;
using Dapper;

namespace SimpleBot.Repositorio
{
    public class UserMSSQLDapper : IUserRepo
    {
        private string _connString;
        private SqlConnection _con;

        public UserMSSQLDapper(string connString)
        {
            this._connString = connString;

            _con = new SqlConnection(_connString);      
        }

        public UserProfile FindProfile(string Id)
        {           
            try
            {
                var query = _con.Query("SELECT * FROM PROFILE WHERE \"ID\" = @ID", new { ID = Id } ).AsList();

                if (query.Count > 0)
                {                   
                    return new UserProfile()
                    {
                        Id = (string)query[0].ID,
                        Visitas = (int)query[0].VISITAS
                    };
                }
                else
                {
                    _con.Execute("INSERT INTO \"PROFILE\" (ID, VISITAS) VALUES (@ID, @VISITAS)", new { ID = Id, VISITAS = 0 });

                    return FindProfile(Id);
                }
            }
            catch (Exception)
            {
                throw;
            }             
        }

        public void InsertMessage(Message message)
        {
            _con.Execute("INSERT INTO MESSAGE (ID, USER, CONTEUDO) VALUES (@ID, @USER, @CONTEUDO)", new { ID = message.Id, USER = message.User, CONTEUDO = message.Text });
        }

        public void UpdateProfile(string id, UserProfile profile)
        {
            _con.Execute("UPDATE PROFILE SET VISITAS = @VISITAS WHERE ID = @ID", new { VISITAS = profile.Visitas, ID = id });
        }

        ~UserMSSQLDapper()
        {
            _con.Dispose();
        }
    }
}