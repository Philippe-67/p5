namespace p5.Models
{
    public class Reparation
    {
        public int Id { get; set; }
        public string Categorie { get; set; }

       
        public Voiture? Voiture { get; set; }
        public int? VoitureId { get; set; }

    }
}


