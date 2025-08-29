namespace RPG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Magier erstellen 
            var merlin = new Spieler("Merlin", Spielerklasse.Magier, 60);
            merlin.Vorstellen();

            // Spieler nimmt Schaden
            merlin.NimmSchaden(15);
            Console.Write("Nach Schaden: "); merlin.Vorstellen();

            // Spieler heilt sich wieder
            merlin.Heilen(10);
            Console.Write("Nach Heilen:  "); merlin.Vorstellen();

            // Andere Klassen testen
            var arthur = new Spieler("Arthur", Spielerklasse.Krieger, 100);
            var sly    = new Spieler("Sly", Spielerklasse.Dieb, 70);

            arthur.Vorstellen();
            sly.Vorstellen();

            // IstLebendig() testen
            Console.WriteLine($"Lebt Merlin? {merlin.IstLebendig()}");

            Console.ReadLine(); 
        }
    }
}

