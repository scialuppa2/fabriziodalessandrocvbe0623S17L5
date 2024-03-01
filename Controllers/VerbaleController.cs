using Microsoft.AspNetCore.Mvc;
using progetto_settimanaleS17L5.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace progetto_settimanaleS17L5.Controllers
{
    public class VerbaleController : Controller
    {
        string connString = "Server=LAPTOP-4EULLOI7\\SQLEXPRESS;Initial Catalog=Polizia Municipale;Integrated Security=True; TrustServerCertificate=True";
        private List<Violazione> listaViolazioni;
        private List<Trasgressore> listaTrasgressori;
        private Verbale GetVerbaleById(int IDverbale)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM Verbale WHERE IDverbale = @IDverbale";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdVerbale", IDverbale);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Verbale verbale = new Verbale
                            {
                                IDverbale = (int)reader["IDverbale"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                                NominativoAgente = reader["NominativoAgente"].ToString(),
                                DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"],
                                Importo = (decimal)reader["Importo"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"],
                                IDanagrafica = (int)reader["IDanagrafica"],
                                IDviolazione = (int)reader["IDviolazione"]
                            };
                            return verbale;
                        }
                        return null;
                    }
                }
            }
        }

        [HttpGet]
        public List<Violazione> ListaViolazioni()
        {
            if (listaViolazioni == null)
            {
                listaViolazioni = new List<Violazione>();

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

                                listaViolazioni.Add(violazione);
                            }
                        }
                    }
                }
            }
            return listaViolazioni;
        }


        [HttpGet]
        public List<Trasgressore> ListaTrasgressori()
        {
            if (listaTrasgressori == null)
            {
                listaTrasgressori = new List<Trasgressore>();

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
                                    IDanagrafica = (int)reader["IDanagrafica"],
                                    Cognome = reader["Cognome"].ToString(),
                                    Nome = reader["Nome"].ToString(),
                                    Indirizzo = reader["Indirizzo"].ToString(),
                                    Città = reader["Città"].ToString(),
                                    Cap = reader["CAP"].ToString(),
                                    CodiceFiscale = reader["CodiceFiscale"].ToString()
                                };

                                listaTrasgressori.Add(trasgressore);
                            }
                        }
                    }
                }
            }
            return listaTrasgressori;
        }

        [HttpGet]
        public ActionResult ListaVerbali()
        {
            List<Verbale> verbali = new List<Verbale>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM Verbale";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Verbale verbale = new Verbale
                            {
                                IDverbale = (int)reader["IDverbale"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                                NominativoAgente = reader["NominativoAgente"].ToString(),
                                DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"],
                                Importo = (decimal)reader["Importo"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"],
                                IDanagrafica = (int)reader["IDanagrafica"],
                                IDviolazione = (int)reader["IDviolazione"]
                            };

                            verbali.Add(verbale);
                        }
                    }
                }

            }
            return View(verbali);
        }

        [HttpGet]
        public ActionResult AggiungiVerbale()
        {
            if (listaViolazioni == null)
            {
                listaViolazioni = ListaViolazioni();
            }

            if (listaTrasgressori == null)
            {
                listaTrasgressori = ListaTrasgressori();
            }

            var trasgressoriSelectList = new SelectList(listaTrasgressori, "IDanagrafica", "AnagraficaCompleta");

            var violazioniSelectList = new SelectList(listaViolazioni, "IDviolazione", "ViolazioneCompleta");

            ViewBag.ListaAnagrafica = trasgressoriSelectList;
            ViewBag.ListaViolazioni = violazioniSelectList;

            return View();
        }

        [HttpPost]
        public ActionResult AggiungiVerbale(Verbale model)
        {
            if (ModelState.IsValid)
            {
                string query = "INSERT INTO Verbale (DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IdAnagrafica, IdViolazione)" + "VALUES (@DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @IdAnagrafica, @IdViolazione)";

                using (var conn = new SqlConnection(connString))

                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DataViolazione", model.DataViolazione);
                        cmd.Parameters.AddWithValue("@IndirizzoViolazione", model.IndirizzoViolazione);
                        cmd.Parameters.AddWithValue("@NominativoAgente", model.NominativoAgente);
                        cmd.Parameters.AddWithValue("@DataTrascrizioneVerbale", model.DataTrascrizioneVerbale);
                        cmd.Parameters.AddWithValue("@Importo", model.Importo);
                        cmd.Parameters.AddWithValue("@DecurtamentoPunti", model.DecurtamentoPunti);
                        cmd.Parameters.AddWithValue("@IdAnagrafica", model.IDanagrafica);
                        cmd.Parameters.AddWithValue("@IdViolazione", model.IDviolazione);

                        cmd.ExecuteNonQuery();
                    }
                }
                TempData["Messaggio"] = "Verbale aggiunto con successo!";
                return RedirectToAction("ListaVerbali");
            }
            TempData["Errore"] = "Il modello non è valido. Correggi gli errori e riprova.";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminaVerbale(int IdVerbale)
        {
            Verbale verbaleDaEliminare = GetVerbaleById(IdVerbale);

            if (verbaleDaEliminare == null)
            {
                TempData["Errore"] = "Violazione non trovata!";
            }
            else
            {
                using (var conn = new SqlConnection(connString))

                {
                    conn.Open();
                    string query = "DELETE FROM Verbale WHERE IDverbale = @IDverbale";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDverbale", IdVerbale);
                        cmd.ExecuteNonQuery();
                    }
                }
                TempData["Messaggio"] = "Verbale eliminato con successo!";
            }
            return RedirectToAction("ListaVerbali");
        }

        [HttpGet]
        public ActionResult ModificaVerbale(int IDverbale)
        {
            Verbale verbaleDaModificare = GetVerbaleById(IDverbale);

            if (verbaleDaModificare == null)
            {
                TempData["Errore"] = "Verbale non trovato!";
            }

            return View(verbaleDaModificare);
        }

        [HttpPost]
        public ActionResult ModificaVerbale(Verbale verbaleModificato)
        {
            if (ModelState.IsValid)
            {
                string query =
                    "UPDATE Verbale SET " +
                    "DataViolazione = @DataViolazione, " +
                    "IndirizzoViolazione = @IndirizzoViolazione, " +
                    "NominativoAgente = @NominativoAgente, " +
                    "DataTrascrizioneVerbale = @DataTrascrizioneVerbale, " +
                    "Importo = @Importo, " +
                    "DecurtamentoPunti = @DecurtamentoPunti " +
                    "WHERE IDverbale = @IDverbale";

                using (var conn = new SqlConnection(connString))

                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDverbale", verbaleModificato.IDverbale);
                        cmd.Parameters.AddWithValue("@DataViolazione", verbaleModificato.DataViolazione);
                        cmd.Parameters.AddWithValue("@IndirizzoViolazione", verbaleModificato.IndirizzoViolazione);
                        cmd.Parameters.AddWithValue("@NominativoAgente", verbaleModificato.NominativoAgente);
                        cmd.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbaleModificato.DataTrascrizioneVerbale);
                        cmd.Parameters.AddWithValue("@Importo", verbaleModificato.Importo);
                        cmd.Parameters.AddWithValue("@DecurtamentoPunti", verbaleModificato.DecurtamentoPunti);

                        cmd.ExecuteNonQuery();
                    }
                }
                TempData["Messaggio"] = "Verbale modificato con successo!";
            }
            return RedirectToAction("ListaVerbali");
        }



    }

}
