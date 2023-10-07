using System.Configuration;
using System.Data.SqlClient;
using System;
using System.Web.Security;

namespace progetto_stettimanale_mvc.Models
{
    public class Dipendente
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public void getutente(Dipendente d)
        {
            string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Select * FROM dipendenti where Username=@Username and Password= @Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Username", d.Username);
                sqlCommand.Parameters.AddWithValue("Password", d.Password);
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                Dipendente dipendente = new Dipendente();
                while (sqlDataReader.Read())
                {
                    dipendente.Username = sqlDataReader["Username"].ToString();
                    FormsAuthentication.SetAuthCookie(dipendente.Username, false);
                }
            }
            catch
            {
            }
            finally { sqlConnection.Close(); }
        }
    }
}