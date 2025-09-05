namespace RPG
{
    //Klasse Gegner 
    public class Gegner
    {
        public string Name { get; set; }
        public int MaxHP { get; private set; }
        public int HP { get; private set; }
        public int Schaden { get; set; }

        // Konstruktor für Gegner
        public Gegner(string name, int maxHp, int schaden)
        {
            Name = name;
            MaxHP = maxHp > 0 ? maxHp : 1;
            HP = MaxHP;
            Schaden = schaden;
        }

        // prüft ob Gegner noch lebt
        public bool IstLebendig() => HP > 0;

        // Gegner nimmt Schaden 
        public void NimmSchaden(int punkte)
        {
            if (punkte <= 0 || !IstLebendig()) return;
            HP = Math.Max(0, HP - punkte);
        }

        // Gegner macht Schaden am Spieler
        public void Angreifen(Spieler ziel)
        {
            if (!IstLebendig()) return;
            Console.WriteLine($"{Name} greift {ziel.Name} an und verursacht {Schaden} Schaden!");
            ziel.NimmSchaden(Schaden);
        }

        // Ausgabe
        public void Vorstellen()
        {
            Console.WriteLine($"Gegner: {Name} – HP: {HP}/{MaxHP} – Schaden: {Schaden}");
        }
    }
}
