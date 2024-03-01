using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using progetto_settimanaleS17L5.Models;
using System;
using System.Collections.Generic;

namespace progetto_settimanaleS17L5.Controllers
{
    public class ReportController : Controller
    {
        string connString = "Server=LAPTOP-4EULLOI7\\SQLEXPRESS;Initial Catalog=Polizia Municipale;Integrated Security=True; TrustServerCertificate=True";


        // Metodo di azione per visualizzare il totale dei verbali trascritti raggruppati per trasgressore
        [HttpGet]
        public ActionResult VerbaliPerTrasgressore()
        {
            List<VerbaliPerTrasgressore> risultati = new List<VerbaliPerTrasgressore>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = @"
                    SELECT A.IDAnagrafica, A.Cognome, A.Nome, COUNT(V.IDVerbale) AS TotaleVerbali
                    FROM Anagrafica A
                    LEFT JOIN Verbale V ON A.IDAnagrafica = V.IDAnagrafica
                    GROUP BY A.IDAnagrafica, A.Cognome, A.Nome";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VerbaliPerTrasgressore result = new VerbaliPerTrasgressore
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                TotaleVerbali = (int)reader["TotaleVerbali"]
                            };
                            risultati.Add(result);
                        }
                    }
                }
            }

            return View(risultati);
        }

        // Metodo di azione per visualizzare il totale dei punti decurtati raggruppati per trasgressore
        [HttpGet]
        public ActionResult PuntiDecurtatiPerTrasgressore()
        {
            List<PuntiDecurtatiPerTrasgressore> risultati = new List<PuntiDecurtatiPerTrasgressore>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = @"
                    SELECT V.IDAnagrafica, A.Cognome, A.Nome, SUM(V.DecurtamentoPunti) AS TotalePuntiDecurtati
                    FROM Verbale V
                    INNER JOIN Anagrafica A ON V.IDAnagrafica = A.IDAnagrafica
                    GROUP BY V.IDAnagrafica, A.Cognome, A.Nome";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PuntiDecurtatiPerTrasgressore result = new PuntiDecurtatiPerTrasgressore
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                TotalePuntiDecurtati = (int)reader["TotalePuntiDecurtati"]
                            };
                            risultati.Add(result);
                        }
                    }
                }
            }

            return View(risultati);
        }

        // Metodo di azione per visualizzare le violazioni con punti decurtati superiori a 10
        [HttpGet]
        public ActionResult ViolazioniConPuntiDecurtatiAlti()
        {
            List<ViolazioniConPuntiElevati> risultati = new List<ViolazioniConPuntiElevati>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = @"
                    SELECT V.*, A.Cognome, A.Nome
                    FROM Verbale V
                    INNER JOIN Anagrafica A ON V.IDAnagrafica = A.IDAnagrafica
                    WHERE V.DecurtamentoPunti > 10";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ViolazioniConPuntiElevati result = new ViolazioniConPuntiElevati
                            {
                                IDVerbale = (int)reader["IDVerbale"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                                NominativoAgente = reader["NominativoAgente"].ToString(),
                                DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"],
                                Importo = (decimal)reader["Importo"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString()
                            };
                            risultati.Add(result);
                        }
                    }
                }
            }

            return View(risultati);
        }

        // Metodo di azione per visualizzare le violazioni con importo maggiore di 400
        [HttpGet]
        public ActionResult ViolazioniConImportoAlto()
        {
            List<ViolazioniConImportoElevato> risultati = new List<ViolazioniConImportoElevato>();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = @"
                    SELECT *
                    FROM Verbale
                    WHERE Importo > 400";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ViolazioniConImportoElevato result = new ViolazioniConImportoElevato
                            {
                                IDVerbale = (int)reader["IDVerbale"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                                NominativoAgente = reader["NominativoAgente"].ToString(),
                                DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"],
                                Importo = (decimal)reader["Importo"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"]
                            };
                            risultati.Add(result);
                        }
                    }
                }
            }

            return View(risultati);
        }
    }
}
