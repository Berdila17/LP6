using System;
using System.Collections.Generic;

namespace RPG
{
    public class Program
    {
        private static readonly Random rng = new Random();

        public static void Main(string[] args)
        {
            
            Console.WriteLine("Wähle deine Klasse: (1) Krieger  (2) Magier  (3) Dieb");
            Spielerklasse klasse = EingabeSpielerklasse();

            Console.Write("Gib deinen Namen ein: ");
            string name = Console.ReadLine() ?? "Held";

            int startHp = klasse switch
            {
                Spielerklasse.Krieger => 100,
                Spielerklasse.Magier => 60,
                Spielerklasse.Dieb => 70,
                _ => 80
            };
            var spieler = new Spieler(name, klasse, startHp);

            
            Console.WriteLine("\nWähle eine Waffe:");
            Console.WriteLine("1) Schwert   2) Zauberstab   3) Dolch");
            var waffe = EingabeWaffe();
            spieler.SetzeWaffe(waffe);

            Console.WriteLine("\n== Dein Charakter ==");
            spieler.Vorstellen();

            
            var gegnerListe = new List<Gegner>
            {
                new Gegner("Goblin", 40,  8),
                new Gegner("Ork",    60, 12),
                new Gegner("Drache",120, 20)
            };

            foreach (var gegner in gegnerListe)
            {
                Console.WriteLine($"\n== Neuer Gegner erscheint: {gegner.Name} ==");
                gegner.Vorstellen();

                while (spieler.IstLebendig() && gegner.IstLebendig())
                {
                    
                    int spielerSchaden = BerechneSpielerSchadenMitCrit(spieler);
                    Console.WriteLine($"{spieler.Name} greift mit {spieler.Waffe.Name} an und verursacht {spielerSchaden} Schaden.");
                    gegner.NimmSchaden(spielerSchaden);
                    if (!gegner.IstLebendig())
                    {
                        Console.WriteLine($"{gegner.Name} ist besiegt!");
                        break; 
                    }

                    
                    int gegnerSchaden = BerechneGegnerSchaden(gegner);
                    Console.WriteLine($"{gegner.Name} greift {spieler.Name} an und verursacht {gegnerSchaden} Schaden!");
                    spieler.NimmSchaden(gegnerSchaden);

                    Console.WriteLine($"Status: {spieler.Name} {spieler.HP}/{spieler.MaxHP} HP | {gegner.Name} {gegner.HP}/{gegner.MaxHP} HP");
                    Console.WriteLine("Weiter mit [Enter]...");
                    Console.ReadLine();
                }

                if (!spieler.IstLebendig())
                {
                    Console.WriteLine($"\n{spieler.Name} wurde besiegt. Spiel Ende.");
                    Console.ReadLine();
                    return;
                }
            }

            Console.WriteLine($"\nAlle Gegner besiegt! Glückwunsch, {spieler.Name}!");
            Console.ReadLine();
        }

        
        private static Spielerklasse EingabeSpielerklasse()
        {
            while (true)
            {
                Console.Write("Deine Wahl: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1": return Spielerklasse.Krieger;
                    case "2": return Spielerklasse.Magier;
                    case "3": return Spielerklasse.Dieb;
                    default: Console.WriteLine("Ungültig. Bitte 1, 2 oder 3 eingeben."); break;
                }
            }
        }

        private static Waffe EingabeWaffe()
        {
            while (true)
            {
                Console.Write("Deine Wahl: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1": return new Waffe("Schwert", 8);
                    case "2": return new Waffe("Zauberstab", 6);
                    case "3": return new Waffe("Dolch", 5);
                    default: Console.WriteLine("Ungültig. Bitte 1, 2 oder 3 eingeben."); break;
                }
            }
        }

        
        private static int BerechneSpielerSchadenMitCrit(Spieler s)
        {
            int chance = rng.Next(1, 101); 
            if (chance <= 20)
            {
                int crit = rng.Next(40, 61); 
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(">>> CRITICAL HIT! <<<");
                Console.ResetColor();
                return crit;
            }
            return s.Waffe.Schaden + rng.Next(0, 3);
        }

        private static int BerechneGegnerSchaden(Gegner g)
        {
            return g.Schaden + rng.Next(0, 3);
        }
    }
}
