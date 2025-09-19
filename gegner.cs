namespace RPG
{
    public class Gegner
    {
        public string Name { get; set; }
        public int MaxHP { get; private set; }
        public int HP { get; private set; }
        public int Schaden { get; set; }

        // 👉 NEU: XP-Reward
        public int XpReward { get; }

        public Gegner(string name, int maxHp, int schaden, int xpReward = 20)
        {
            Name = name;
            MaxHP = maxHp > 0 ? maxHp : 1;
            HP = MaxHP;
            Schaden = schaden;
            XpReward = xpReward;
        }

        public bool IstLebendig() => HP > 0;

        public void NimmSchaden(int punkte)
        {
            if (punkte <= 0 || !IstLebendig()) return;
            HP = Math.Max(0, HP - punkte); // falls .NET: Math.Max
        }

        public void Angreifen(Spieler ziel)
        {
            if (!IstLebendig()) return;
            Console.WriteLine($"{Name} greift {ziel.Name} an und verursacht {Schaden} Schaden!");
            ziel.NimmSchaden(Schaden);
        }

        public void Vorstellen()
        {
            Console.WriteLine($"Gegner: {Name} – HP: {HP}/{MaxHP} – Schaden: {Schaden} – XP: {XpReward}");
        }
    }
}
