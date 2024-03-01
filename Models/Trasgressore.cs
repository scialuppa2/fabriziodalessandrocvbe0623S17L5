namespace progetto_settimanaleS17L5.Models
{
    public class Trasgressore
    {
        public int IDanagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Città { get; set; }
        public string Cap { get; set; }
        public string CodiceFiscale { get; set; }

        public string AnagraficaCompleta => $"{IDanagrafica} - {Cognome} {Nome}";
    }
}
