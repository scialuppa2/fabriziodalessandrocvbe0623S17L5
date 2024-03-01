using progetto_settimanaleS17L5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace progetto_settimanaleS17L5.Controllers
{
    public class TrasgressoreController : Controller
    {
        string connString = "Server=LAPTOP-4EULLOI7\\SQLEXPRESS;Initial Catalog=Polizia Municipale;Integrated Security=True; TrustServerCertificate=True";



        private Trasgressore GetTrasgressoreById(int IdAnagrafica)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM Anagrafica WHERE IDanagrafica = @IDanagrafica";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdAnagrafica", IdAnagrafica);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Trasgressore trasgressore = new Trasgressore
                            {
                                IDanagrafica = (int)reader["IdAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Indirizzo = reader["Indirizzo"].ToString(),
                                Città = reader["Città"].ToString(),
                                Cap = reader["Cap"].ToString(),
                                CodiceFiscale = reader["CodiceFiscale"].ToString()
                            };
                            return trasgressore;
                        }
                        return null;
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult ListaTrasgressori()
        {
            List<Trasgressore> trasgressori = new List<Trasgressore>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM Anagrafica";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Trasgressore trasgressore = new Trasgressore
                            {
                                IDanagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Indirizzo = reader["Indirizzo"].ToString(),
                                Città = reader["Città"].ToString(),
                                Cap = reader["CAP"].ToString(),
                                CodiceFiscale = reader["CodiceFiscale"].ToString()
                            };

                            trasgressori.Add(trasgressore);
                        }
                    }
                }

            }
            return View(trasgressori);
        }

        [HttpGet]
        public ActionResult AggiungiTrasgressore()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AggiungiTrasgressore(Trasgressore model)
        {
            if (ModelState.IsValid)
            {
                string query = "INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Città, Cap, CodiceFiscale)" + "VALUES (@Cognome, @Nome, @Indirizzo, @Città, @Cap, @CodiceFiscale)";

                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cognome", model.Cognome);
                        cmd.Parameters.AddWithValue("@Nome", model.Nome);
                        cmd.Parameters.AddWithValue("@Indirizzo", model.Indirizzo);
                        cmd.Parameters.AddWithValue("@Città", model.Città);
                        cmd.Parameters.AddWithValue("@Cap", model.Cap);
                        cmd.Parameters.AddWithValue("@CodiceFiscale", model.CodiceFiscale);

                        cmd.ExecuteNonQuery();
                    }
                }
                TempData["Messaggio"] = "Trasgressore aggiunto con successo!";
                return RedirectToAction("ListaTrasgressori");
            }
            TempData["Errore"] = "Il modello non è valido. Correggi gli errori e riprova.";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminaTrasgressore(int IdAnagrafica)
        {
            Trasgressore trasgressoreDaEliminare = GetTrasgressoreById(IdAnagrafica);

            if (trasgressoreDaEliminare == null)
            {
                TempData["Errore"] = "Trasgressore non trovato!";
            }
            else
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "DELETE FROM Anagrafica WHERE IdAnagrafica = @IdAnagrafica";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdAnagrafica", IdAnagrafica);
                        cmd.ExecuteNonQuery();
                    }
                }
                TempData["Messaggio"] = "Trasgressore eliminato con successo!";
            }
            return RedirectToAction("ListaTrasgressori");
        }

        [HttpGet]
        public ActionResult ModificaTrasgressore(int IdAnagrafica)
        {
            Trasgressore trasgressoreDaModificare = GetTrasgressoreById(IdAnagrafica);

            if (trasgressoreDaModificare == null)
            {
                TempData["Errore"] = "Trasgressore non trovato!";
            }

            return View(trasgressoreDaModificare);
        }

        [HttpPost]
        public ActionResult ModificaTrasgressore(Trasgressore trasgressoreModificato)
        {
            if (ModelState.IsValid)
            {
                string query = "UPDATE Anagrafica SET " +
                    "Cognome = @Cognome, " +
                    "Nome = @Nome, " +
                    "Indirizzo = @Indirizzo, " +
                    "Città = @Città, " +
                    "Cap = @Cap, " +
                    "CodiceFiscale = @CodiceFiscale " +
                    "WHERE IdAnagrafica = @IdAnagrafica";

                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdAnagrafica", trasgressoreModificato.IDanagrafica);
                        cmd.Parameters.AddWithValue("@Cognome", trasgressoreModificato.Cognome);
                        cmd.Parameters.AddWithValue("@Nome", trasgressoreModificato.Nome);
                        cmd.Parameters.AddWithValue("@Indirizzo", trasgressoreModificato.Indirizzo);
                        cmd.Parameters.AddWithValue("@Città", trasgressoreModificato.Città);
                        cmd.Parameters.AddWithValue("@Cap", trasgressoreModificato.Cap);
                        cmd.Parameters.AddWithValue("@CodiceFiscale", trasgressoreModificato.CodiceFiscale);

                        cmd.ExecuteNonQuery();
                    }
                }
                TempData["Messaggio"] = "Trasgressore modificato con successo!";
            }
            return RedirectToAction("ListaTrasgressori");
        }
    }
}