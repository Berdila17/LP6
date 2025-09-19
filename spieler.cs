namespace RPG
{
    public enum Spielerklasse { Krieger, Magier, Dieb, Heiler } 

    public class Spieler
    {
        public string Name { get; set; }
        public Spielerklasse Klasse { get; }
        public int MaxHP { get; private set; }
        public int HP { get; private set; }
        public Waffe Waffe { get; private set; }
        public int Level { get; private set; } = 1;
        public int XP { get; private set; } = 0;

        public Spieler(string name, Spielerklasse klasse, int maxHp)
        {
            Name = name;
            Klasse = klasse;
            MaxHP = maxHp > 0 ? maxHp : 1;
            HP = MaxHP;

            Waffe = klasse switch
            {
                Spielerklasse.Magier => new Waffe("Zauberstab", 6),
                Spielerklasse.Krieger => new Waffe("Schwert", 8),
                Spielerklasse.Dieb => new Waffe("Dolch", 5),
                Spielerklasse.Heiler => new Waffe("Stab", 6),
                _ => new Waffe("Holzknüppel", 3)
            };
        }

        public bool IstLebendig() => HP > 0;

        public void Heilen(int punkte)
        {
            if (punkte <= 0 || !IstLebendig()) return;
            HP = Math.Min(MaxHP, HP + punkte);
        }

        public void NimmSchaden(int punkte)
        {
            if (punkte <= 0 || !IstLebendig()) return;
            HP = Math.Max(0, HP - punkte);
        }

        public void Vorstellen()
        {
            Console.WriteLine($"{Name} ({Klasse}) – Lvl {Level}, XP {XP} – HP: {HP}/{MaxHP}, Waffe: {Waffe}");
        }

        public void SetzeWaffe(Waffe neueWaffe)
        {
            if (neueWaffe != null) Waffe = neueWaffe;
        }

        
        public void RundePassiv()
        {
            if (!IstLebendig()) return;
            if (Klasse == Spielerklasse.Heiler)
            {
                int vor = HP;
                Heilen(2);
                if (HP > vor)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Name} (Heiler) regeneriert +2 HP (jetzt {HP}/{MaxHP}).");
                    Console.ResetColor();
                }
            }
        }

        // kleiner Klassenbonus 
        public int KlassenSchadenBonus() => Klasse == Spielerklasse.Krieger ? 2 : 0;

        // XP erhalten + Level-Up prüfen
        public void GainXp(int amount)
        {
            if (amount <= 0) return;
            XP += amount;
            TryLevelUp();
        }

        private void TryLevelUp()
        {
            // Einfach: 50 XP * aktuelles Level
            int needed = Level * 50;
            while (XP >= needed)
            {
                XP -= needed;
                Level++;
                MaxHP += 10; // stabiler werden
                HP = MaxHP;  
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"LEVEL UP! {Name} ist jetzt Level {Level} (MaxHP {MaxHP}).");
                Console.ResetColor();
                needed = Level * 50;
            }
        }
    }
}
