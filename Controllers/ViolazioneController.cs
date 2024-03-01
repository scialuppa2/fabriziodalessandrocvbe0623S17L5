using Microsoft.AspNetCore.Mvc;
using progetto_settimanaleS17L5.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace progetto_settimanaleS17L5.Controllers
{
    public class ViolazioneController : Controller
    {
        string connString = "Server=LAPTOP-4EULLOI7\\SQLEXPRESS;Initial Catalog=Polizia Municipale;Integrated Security=True; TrustServerCertificate=True";

        private Violazione GetViolazioneById(int IDviolazione)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM TIPO_VIOLAZIONE WHERE IDviolazione = @IDviolazione";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IDviolazione", IDviolazione);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Violazione violazione = new Violazione
                            {
                                IDviolazione = (int)reader["IDviolazione"],
                                Descrizione = reader["Descrizione"].ToString(),
                            };
                            return violazione;
                        }
                        return null;
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult ListaViolazioni()
        {
            List<Violazione> violazioni = new List<Violazione>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM TIPO_VIOLAZIONE";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Violazione violazione = new Violazione
                            {
                                IDviolazione = (int)reader["IDviolazione"],
                                Descrizione = reader["Descrizione"].ToString(),
                            };

                            violazioni.Add(violazione);
                        }
                    }
                }
            }
            return View(violazioni);
        }

        [HttpGet]
        public ActionResult AggiungiViolazione()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AggiungiViolazione(Violazione model)
        {
            if (ModelState.IsValid)
            {
                string query = "INSERT INTO TIPO_VIOLAZIONE (Descrizione)" + "VALUES (@Descrizione)";

                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Descrizione", model.Descrizione);

                        cmd.ExecuteNonQuery();
                    }
                }
                TempData["Messaggio"] = "Violazione aggiunta con successo!";
                return RedirectToAction("ListaViolazioni");
            }
            TempData["Errore"] = "Il modello non è valido. Correggi gli errori e riprova.";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminaViolazione(int IDviolazione)
        {
            Violazione violazioneDaEliminare = GetViolazioneById(IDviolazione);

            if (violazioneDaEliminare == null)
            {
                TempData["Errore"] = "Violazione non trovata!";
            }
            else
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "DELETE FROM TIPO_VIOLAZIONE WHERE IDviolazione = @IDviolazione";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDviolazione", IDviolazione);
                        cmd.ExecuteNonQuery();
                    }
                }
                TempData["Messaggio"] = "Violazione eliminata con successo!";
            }
            return RedirectToAction("ListaViolazioni");
        }

        [HttpGet]
        public ActionResult ModificaViolazione(int IdViolazione)
        {
            Violazione violazioneDaModificare = GetViolazioneById(IdViolazione);

            if (violazioneDaModificare == null)
            {
                TempData["Errore"] = "Violazione non trovata!";
            }

            return View(violazioneDaModificare);
        }

        [HttpPost]
        public ActionResult ModificaViolazione(Violazione violazioneModificata)
        {
            if (ModelState.IsValid)
            {
                string query =
                    "UPDATE TIPO_VIOLAZIONE SET " +
                    "Descrizione = @Descrizione " +
                    "WHERE IDviolazione = @IDviolazione";

                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdViolazione", violazioneModificata.IDviolazione);
                        cmd.Parameters.AddWithValue("@Descrizione", violazioneModificata.Descrizione);

                        cmd.ExecuteNonQuery();
                    }
                }
                TempData["Messaggio"] = "Violazione modificata con successo!";
            }
            return RedirectToAction("ListaViolazioni");
        }
    }
}
