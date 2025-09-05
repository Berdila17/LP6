namespace RPG
{
    // Spielerklasse
    public enum Spielerklasse { Krieger, Magier, Dieb }

    public class Spieler
    {
        // Eigenschaften des Spielers
        public string Name { get; set; }
        public Spielerklasse Klasse { get; }
        public int MaxHP { get; private set; }  // maximale Lebenspunkte
        public int HP { get; private set; }     // aktuelle Lebenspunkte
        public Waffe Waffe { get; private set; } // Standardwaffe des Spielers

        // erstellt einen Spieler mit Name, Klasse und Lebenspunkten
        public Spieler(string name, Spielerklasse klasse, int maxHp)
        {
            Name = name;
            Klasse = klasse;
            MaxHP = maxHp > 0 ? maxHp : 1; // Sicherheit: kein 0 oder negative HP
            HP = MaxHP;

            // Standardwaffe je nach Spielerklasse setzen
            Waffe = klasse switch
            {
                Spielerklasse.Magier  => new Waffe("Zauberstab", 6),
                Spielerklasse.Krieger => new Waffe("Schwert", 8),
                Spielerklasse.Dieb    => new Waffe("Dolch", 5),
                _ => new Waffe("Holzknüppel", 3) // Fallback
            };
        }

        // prüft ob der Spieler noch lebt
        public bool IstLebendig() => HP > 0;

        // Spieler wird geheilt 
        public void Heilen(int punkte)
        {
            if (punkte <= 0 || !IstLebendig()) return; // nur wenn lebendig + Heilwert > 0
            HP = Math.Min(MaxHP, HP + punkte);
        }

        // Spieler nimmt Schaden 
        public void NimmSchaden(int punkte)
        {
            if (punkte <= 0 || !IstLebendig()) return;
            HP = Math.Max(0, HP - punkte);
        }

        // Spieler stellt sich vor 
        public void Vorstellen()
        {
            Console.WriteLine($"{Name} ({Klasse}) – HP: {HP}/{MaxHP}, Waffe: {Waffe}");
        }

        // Spieler kann auch eine neue Waffe ausrüsten
        public void SetzeWaffe(Waffe neueWaffe)
        {
            if (neueWaffe != null) Waffe = neueWaffe;
        }
    }
}
