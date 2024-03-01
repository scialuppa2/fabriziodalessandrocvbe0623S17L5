namespace progetto_settimanaleS17L5.Models
{
    public class VerbaliPerTrasgressore
    {
        public int IDAnagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public int TotaleVerbali { get; set; }
    }

    public class PuntiDecurtatiPerTrasgressore
    {
        public int IDAnagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public int TotalePuntiDecurtati { get; set; }
    }

    public class ViolazioniConPuntiElevati
    {
        public int IDVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string NominativoAgente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
    }

    public class ViolazioniConImportoElevato
    {
        public int IDVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string NominativoAgente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
    }
}
