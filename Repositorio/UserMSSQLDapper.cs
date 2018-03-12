using System;
using System.Data.SqlClient;
using Dapper;

namespace SimpleBot.Repositorio
{
    public class UserMSSQLDapper : IUserRepo
    {
        private string _connString;

        public UserMSSQLDapper(string connString)
        {
            this._connString = connString;
        }

        public UserProfile FindProfile(string Id)
        {
            using (SqlConnection _con = new SqlConnection(_connString))
            {
                var query = _con.Query("SELECT * FROM PROFILE WHERE \"ID\" = @ID", new { ID = Id }).AsList();

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
                    //cria uma profile default
                    AddProfileDefault(Id);                    

                    return FindProfile(Id);
                }
            }
        }

        private void AddProfileDefault(string Id)
        {
            using (SqlConnection _con = new SqlConnection(_connString))
            {
                _con.Execute("INSERT INTO \"PROFILE\" (ID, VISITAS) VALUES (@ID, @VISITAS)", new { ID = Id, VISITAS = 0 });
            }
        }

        public void InsertMessage(Message message)
        {
            using (SqlConnection _con = new SqlConnection(_connString))
            {
                _con.Execute("INSERT INTO MESSAGE (ID, USER, CONTEUDO) VALUES (@ID, @USER, @CONTEUDO)", new { ID = message.Id, USER = message.User, CONTEUDO = message.Text });
            }
        }

        public void UpdateProfile(string id, UserProfile profile)
        {
            using (SqlConnection _con = new SqlConnection(_connString))
            {
                _con.Execute("UPDATE PROFILE SET VISITAS = @VISITAS WHERE ID = @ID", new { VISITAS = profile.Visitas, ID = id });
            }
        }
    }
}