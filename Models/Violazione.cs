namespace progetto_settimanaleS17L5.Models
{
    public class Violazione
    {
        public int IDviolazione { get; set; }
        public string Descrizione { get; set; }

        public string ViolazioneCompleta => $"{IDviolazione} - {Descrizione}";
    }
}
