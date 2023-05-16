namespace p5.Models
{
    public class Voiture
    {
        public int Id { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }

        // Propriété de navigation vers les réparations associées à la voiture
        public ICollection<Reparation>? Reparations { get; set; }
    }
}
