namespace RPG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Spieler erstellen und testen
            var merlin = new Spieler("Merlin", Spielerklasse.Magier, 60);
            merlin.Vorstellen();

            // Spieler nimmt Schaden
            merlin.NimmSchaden(15);
            Console.Write("Nach Schaden: ");
            merlin.Vorstellen();

            // Spieler heilt sich wieder
            merlin.Heilen(10);
            Console.Write("Nach Heilen:  ");
            merlin.Vorstellen();

            // Andere Klassen testen
            var arthur = new Spieler("Arthur", Spielerklasse.Krieger, 100);
            var sly    = new Spieler("Sly", Spielerklasse.Dieb, 70);

            arthur.Vorstellen();
            sly.Vorstellen();

            Console.WriteLine($"Lebt Merlin? {merlin.IstLebendig()}");
            Console.WriteLine();

            // Gegner erstellen und Angreifen testen
            var goblin = new Gegner("Goblin", 40, 8);
            goblin.Vorstellen();

            goblin.Angreifen(merlin);  // Gegner verursacht Schaden am Spieler
            Console.Write("Nach Goblin-Angriff: ");
            merlin.Vorstellen();

            Console.ReadLine();
        }
    }
}
