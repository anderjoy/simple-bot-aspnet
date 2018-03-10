using System;
using System.Data.SqlClient;

namespace SimpleBot.Repositorio
{
    public class UserMSSQL : IUserRepo
    {
        private string _connString;
        private SqlConnection _con;

        public UserMSSQL(string connString)
        {
            this._connString = connString;

            _con = new SqlConnection(_connString);
            _con.Open();            
        }

        public UserProfile FindProfile(string Id)
        {
            var _command = _con.CreateCommand();

            _command.CommandText = "SELECT * FROM PROFILE WHERE \"ID\" = @ID";
            _command.Parameters.Clear();
            _command.Parameters.AddWithValue("@ID", Id);

            try
            {
                var reader = _command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    return new UserProfile()
                    {
                        Id = reader.GetString(reader.GetOrdinal("ID")),
                        Visitas = reader.GetInt32(reader.GetOrdinal("VISITAS"))
                    };
                }
                else
                {
                    SqlCommand _insert = _con.CreateCommand();

                    _insert.CommandText = "INSERT INTO \"PROFILE\" (ID, VISITAS) VALUES (@ID, @VISITAS)";
                    _insert.Parameters.Clear();
                    _insert.Parameters.AddWithValue("@ID", Id);
                    _insert.Parameters.AddWithValue("@VISITAS", 0);
                    _insert.ExecuteNonQuery();

                    return FindProfile(Id);
                }
            }
            catch (Exception E)
            {
                throw;
            }             
        }

        public void InsertMessage(Message message)
        {
            var _command = _con.CreateCommand();

            _command.CommandText = "INSERT INTO MESSAGE (ID, USER, CONTEUDO) VALUES (@ID, @USER, @CONTEUDO)";
            _command.Parameters.Clear();
            _command.Parameters.AddWithValue("@ID", message.Id);
            _command.Parameters.AddWithValue("@USER", message.User);
            _command.Parameters.AddWithValue("@CONTEUDO", message.Text);

            _command.ExecuteNonQuery();
        }

        public void UpdateProfile(string id, UserProfile profile)
        {
            var _command = _con.CreateCommand();

            _command.CommandText = "UPDATE PROFILE SET VISITAS = @VISITAS WHERE ID = @ID";
            _command.Parameters.Clear();            
            _command.Parameters.AddWithValue("@VISITAS", profile.Visitas);
            _command.Parameters.AddWithValue("@ID", id);

            _command.ExecuteNonQuery();
        }

        ~UserMSSQL()
        {
            _con.Dispose();
        }
    }
}