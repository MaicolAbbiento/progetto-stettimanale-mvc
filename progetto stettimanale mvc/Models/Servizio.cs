using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace progetto_stettimanale_mvc.Models
{
    public class Servizio
    {
        public int idtiposervizio { get; set; }
        public int idprenotazione { get; set; }
        public string descrizione { get; set; }
        public decimal costo { get; set; }
        public string Errore { get; set; }

        public Servizio()
        { }

        public Servizio(string descrizione)
        {
            this.descrizione = descrizione;
        }

        public Servizio(int idtiposervizio, string descrizione, decimal costo)
        {
            this.idtiposervizio = idtiposervizio;
            this.descrizione = descrizione;
            this.costo = costo;
        }

        public Servizio(int idtiposervizio)
        {
            this.idtiposervizio = idtiposervizio;
        }

        public List<string> getservizio()
        {
            string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand;

                sqlCommand = new SqlCommand("Select * FROM tiposervizio", sqlConnection);

                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                List<string> list = new List<string>();
                while (sqlDataReader.Read())
                {
                    Servizio c = new Servizio
                        (
                             sqlDataReader["descrizione"].ToString()

                        );
                    list.Add(c.descrizione);
                }
                return list;
            }
            catch
            {
                return new List<string>();
            }
            finally { sqlConnection.Close(); }
        }

        public void addservizio(Servizio p)
        {
            Prenotazione preno = new Prenotazione();
            preno = preno.selectprenotazione(p.idprenotazione);
            p.idprenotazione = preno.Idprenotazione;
            if (p.idprenotazione > 0)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connectionString);
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                    "INSERT INTO  servizi   VALUES ( @idtiposervizio, @idprenotazione)", conn);
                    cmd.Parameters.AddWithValue("idtiposervizio", p.idtiposervizio);
                    cmd.Parameters.AddWithValue("idprenotazione", p.idprenotazione);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Errore = "qualcosa è andato storto";
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int getidservizio(string p)
        {
            string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand;

                sqlCommand = new SqlCommand("Select * FROM tiposervizio where descrizione = @descrizione", sqlConnection);
                sqlCommand.Parameters.AddWithValue("descrizione", p);

                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                int c = 0;
                while (sqlDataReader.Read())
                {
                    c = Convert.ToInt32(sqlDataReader["idtiposervizio"]);
                }
                return c;
            }
            catch
            {
                return 0;
            }
            finally { sqlConnection.Close(); }
        }

        public Servizio getserviziutente(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand;

                sqlCommand = new SqlCommand("Select * FROM tiposervizio where idtiposervizio = @idtiposervizio", sqlConnection);
                sqlCommand.Parameters.AddWithValue("idtiposervizio", id);

                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                Servizio servizio = new Servizio();
                while (sqlDataReader.Read())
                {
                    servizio.idtiposervizio = Convert.ToInt32(sqlDataReader["idtiposervizio"]);
                    servizio.descrizione = sqlDataReader["descrizione"].ToString();
                    servizio.costo = Convert.ToDecimal(sqlDataReader["costo"]);
                }
                return servizio;
            }
            catch
            {
                return new Servizio();
            }
            finally { sqlConnection.Close(); }
        }

        public List<int> selectservizi(int prenotazione)
        {
            string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand;

                sqlCommand = new SqlCommand("Select * FROM servizi where idprenotazione = @idprenotazione", sqlConnection);
                sqlCommand.Parameters.AddWithValue("idprenotazione", prenotazione);

                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                List<int> list = new List<int>();
                while (sqlDataReader.Read())
                {
                    Servizio servizio = new Servizio(
                      Convert.ToInt32(sqlDataReader["idtiposervizio"])
                    );
                    list.Add(servizio.idtiposervizio);
                }
                return list;
            }
            catch
            {
                return new List<int>();
            }
        }
    }
}