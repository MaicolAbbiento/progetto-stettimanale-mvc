using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;
using System.Linq;

namespace progetto_stettimanale_mvc.Models
{
    public class camera
    {
        public int idcamera { get; set; }
        public string descrizione { get; set; }
        public bool isdoppia { get; set; }
        public bool occupata { get; set; }

        public camera(int idcamera, bool isdoppia)
        {
            this.idcamera = idcamera;
            this.isdoppia = isdoppia;
        }

        public camera()
        { }

        public camera selectcamera(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Select * FROM camera where idcamera=@idcamera and occupata=0", sqlConnection);
                sqlCommand.Parameters.AddWithValue("idcamera", id);
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                camera d = new camera();
                while (sqlDataReader.Read())
                {
                    camera c = new camera
                        (
                         Convert.ToInt32(sqlDataReader["idcamera"]),
                          Convert.ToBoolean(sqlDataReader["isdoppia"])
                        );
                    d = c;
                }

                return d;
            }
            catch
            {
                camera d = new camera();
                return d;
            }
            finally { sqlConnection.Close(); }
        }

        public void uptadecamera(int id)
        {
            if (id != 0)
            {
                string connetionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connetionString);
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "update camera set  occupata = 0  where idcamera = @idcamera";
                    cmd.Parameters.AddWithValue("idcamera", id);
                    int IsOk = cmd.ExecuteNonQuery();
                }
                catch { }
                finally { conn.Close(); }
            }
        }

        public void uptadecamera2(int id)
        {
            if (id != 0)
            {
                string connetionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connetionString);
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "update camera set  occupata = 1  where idcamera = @idcamera";
                    cmd.Parameters.AddWithValue("idcamera", id);
                    int IsOk = cmd.ExecuteNonQuery();
                }
                catch { }
                finally { conn.Close(); }
            }
        }

        public void cameralasciata()
        {
            string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Select * FROM prenotazione ", sqlConnection);
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                List<int> list = new List<int>();
                List<int> camere = new List<int>();
                List<int> camerelibere = new List<int>();
                Prenotazione prenotazione = new Prenotazione();
                while (sqlDataReader.Read())

                {
                    prenotazione.Datapartenza = Convert.ToDateTime(sqlDataReader["Datapartenza"]);
                    if (prenotazione.Datapartenza > DateTime.Now)
                    {
                        int id2 = Convert.ToInt32(sqlDataReader["idcamera"]);
                        list.Add(id2);
                    }

                    int id = Convert.ToInt32(sqlDataReader["idcamera"]);
                    camere.Add(id);
                }
                IEnumerable<int> valoriNonComuni = list.Except(camere).Union(camere.Except(list));
                foreach (var item in valoriNonComuni)
                {
                    camerelibere.Add(item);
                }
                if (camerelibere.Count > 0)
                {
                    foreach (var item in camerelibere)
                    {
                        uptadecamera(item);
                    }
                }
            }
            catch
            {
            }
            finally { sqlConnection.Close(); }
        }

        public void addcamera()
        {
        }
    }
}