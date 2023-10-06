using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace progetto_stettimanale_mvc.Models
{
    public class Prenotazione
    {
        public string Codicefiscale { get; set; }
        public int Idprenotazione { get; set; }
        public decimal Caparra { get; set; }
        public decimal Costotot { get; set; }
        public decimal Costonotte { get; set; }
        public string Dettagli { get; set; }
        public string Anno { get; set; }
        public DateTime Datarrivo { get; set; }
        public DateTime Datapartenza { get; set; }
        public int Idclienti { get; set; }
        public int Idcamera { get; set; }
        public string Errore { get; set; }

        public Prenotazione()
        { }

        public Prenotazione(int idprenotazione, decimal costotot, string dettagli, string anno, DateTime datarrivo, DateTime datapartenza, int idclienti, int idcamera)
        {
            Idprenotazione = idprenotazione;
            Costotot = costotot;
            Dettagli = dettagli;
            Anno = anno;
            Datarrivo = datarrivo;
            Datapartenza = datapartenza;
            Idclienti = idclienti;
            Idcamera = idcamera;
        }

        public decimal addprenotazione(Prenotazione p)
        {
            p.Caparra = 50;

            if (p.Dettagli == "mezza pensione")
            { p.Costonotte = 50; }
            else if (p.Dettagli == "pensione completa")
            { p.Costonotte = 70; }
            else { p.Costonotte = 40; }
            p.Datarrivo = DateTime.Now;
            TimeSpan d = p.Datapartenza - p.Datarrivo;
            int giorni = d.Days;
            if (giorni > 0)
            {
                p.Costotot = p.Caparra + p.Costonotte * giorni;

                p.Anno = DateTime.Now.Year.ToString();
                Cliente cliente = new Cliente();
                cliente = cliente.utente(p.Codicefiscale);
                p.Codicefiscale = cliente.Codicefiscale;
                p.Idclienti = cliente.Idclienti;
                camera c = new camera();
                c = c.selectcamera(p.Idcamera);
                p.Idcamera = c.idcamera;
                if (c.isdoppia == true)
                {
                    p.Costotot = p.Costotot + 40 * giorni;
                }
                if (p.Idcamera > 0)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
                    SqlConnection conn = new SqlConnection(connectionString);
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(
                        "INSERT INTO prenotazione  VALUES (@Costotot, @Dettagli, @Anno,  @Datarrivo , @Datapartenza, @Idclienti, @Idcamera )", conn);
                        cmd.Parameters.AddWithValue("Costotot", p.Costotot);
                        cmd.Parameters.AddWithValue("Dettagli", p.Dettagli);
                        cmd.Parameters.AddWithValue("Anno", p.Anno);
                        cmd.Parameters.AddWithValue("Datarrivo", p.Datarrivo);
                        cmd.Parameters.AddWithValue("Datapartenza", p.Datapartenza);
                        cmd.Parameters.AddWithValue("Idclienti", p.Idclienti);
                        cmd.Parameters.AddWithValue("Idcamera", p.Idcamera);
                        cmd.ExecuteNonQuery();
                        return p.Costotot;
                    }
                    catch (Exception ex)
                    {
                        Errore = "qualcosa è andato storto";
                        return 0;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    p.Costotot = 0;
                    Errore = "camera prenotata o non esistente";
                    return p.Costotot;
                }
            }
            else
            {
                p.Costotot = 0;
                Errore = "la datta di arrivo e maggiore di quella di partenza iserire data in modo corretto";
                return p.Costotot;
            }
        }

        public Prenotazione selectprenotazione(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Select * FROM prenotazione where idprenotazione=@idprenotazione", sqlConnection);
                sqlCommand.Parameters.AddWithValue("idprenotazione", id);
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                Prenotazione p = new Prenotazione();
                while (sqlDataReader.Read())
                {
                    p.Idprenotazione = Convert.ToInt32(sqlDataReader["idprenotazione"]);
                    p.Costotot = Convert.ToDecimal(sqlDataReader["Costotot"]);
                }
                return p;
            }
            catch
            {
                Prenotazione p = new Prenotazione();
                return p;
            }
            finally { sqlConnection.Close(); }
        }

        public List<Prenotazione> checkout()
        {
            string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Select * FROM prenotazione", sqlConnection);
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();

                List<Prenotazione> p = new List<Prenotazione>();
                while (sqlDataReader.Read())
                {
                    Prenotazione prenotazione = new Prenotazione(
                        Convert.ToInt32(sqlDataReader["idprenotazione"]),
                        Convert.ToDecimal(sqlDataReader["costotot"]),
                        sqlDataReader["dettagli"].ToString(),
                        sqlDataReader["anno"].ToString(),
                        Convert.ToDateTime(sqlDataReader["datarrivo"]),
                        Convert.ToDateTime(sqlDataReader["datapartenza"]),
                        Convert.ToInt32(sqlDataReader["Idclienti"]),
                        Convert.ToInt32(sqlDataReader["idcamera"])

                     );
                    p.Add(prenotazione);
                }
                return p;
            }
            catch
            {
                return new List<Prenotazione>();
            }
            finally { sqlConnection.Close(); }
        }
    }
}