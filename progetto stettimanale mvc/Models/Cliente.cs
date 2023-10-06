using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;

namespace progetto_stettimanale_mvc.Models
{
    public class Cliente
    {
        public int Idclienti { get; set; }

        [Required(ErrorMessage = "Il codice fiscale è obbligatorio")]
        public string Codicefiscale { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "la città è obbligatorio")]
        public string Città { get; set; }

        [Required(ErrorMessage = "la provincia è obbligatorio")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "l'email è obbligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il telefono è obbligatorio")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Il cellulare è obbligatorio")]
        public string Cellulare { get; set; }

        public string Errore { get; set; }

        public void addcliente(Cliente c)
        {
            Cliente t = utente(c.Codicefiscale);

            if (t.Codicefiscale != c.Codicefiscale)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connectionString);
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                    "INSERT INTO clienti  VALUES (@codicefiscale, @Cognome, @Nome,  @Città , @Provincia, @Email, @Telefono, @Cellulare )", conn);
                    cmd.Parameters.AddWithValue("codicefiscale", c.Codicefiscale);
                    cmd.Parameters.AddWithValue("Cognome", c.Cognome);
                    cmd.Parameters.AddWithValue("Nome", c.Nome);
                    cmd.Parameters.AddWithValue("Città", c.Città);
                    cmd.Parameters.AddWithValue("Provincia", c.Provincia);
                    cmd.Parameters.AddWithValue("Email", c.Email);
                    cmd.Parameters.AddWithValue("Telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("Cellulare", c.Cellulare);
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
            else
            {
                Errore = "cliente gia presente";
            }
        }

        public Cliente utente(string U)
        {
            string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Select * FROM clienti where codicefiscale=@codicefiscale", sqlConnection);
                sqlCommand.Parameters.AddWithValue("codicefiscale", U);
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                Cliente cliente = new Cliente();
                while (sqlDataReader.Read())
                {
                    cliente.Codicefiscale = sqlDataReader["codiceFiscale"].ToString();
                    cliente.Idclienti = Convert.ToInt32(sqlDataReader["Idclienti"]);
                }
                return cliente;
            }
            catch
            {
                Cliente cliente = new Cliente();
                return cliente;
            }
            finally { sqlConnection.Close(); }
        }
    }
}