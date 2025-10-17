using System;
using System.Collections.Generic;

namespace RPG
{
    public class Program
    {
        private static readonly Random rng = new Random();

        public static void Main(string[] args)
        {
           
            Console.WriteLine("Wähle deine Klasse: (1) Krieger  (2) Magier  (3) Dieb  (4) Heiler");
            Spielerklasse klasse = EingabeSpielerklasse();

            Console.Write("Gib deinen Namen ein: ");
            string name = Console.ReadLine() ?? "Held";

            int startHp = klasse switch
            {
                Spielerklasse.Krieger => 100,
                Spielerklasse.Magier  => 60,
                Spielerklasse.Dieb    => 70,
                Spielerklasse.Heiler  => 80,
                _ => 80
            };
            var spieler = new Spieler(name, klasse, startHp);

            
            Console.WriteLine("\nWähle eine Waffe:");
            Console.WriteLine("1) Schwert (8)   2) Zauberstab (6)   3) Dolch (5)");
            var waffe = EingabeWaffe();
            spieler.SetzeWaffe(waffe);

            
            spieler.FuegeWaffeHinzu(new Waffe("Axt", 9));
            spieler.FuegeWaffeHinzu(new Waffe("Speer", 7));

            
            spieler.Heiltraenke = 2;

            Console.WriteLine("\n== Dein Charakter ==");
            spieler.Vorstellen();
            Console.WriteLine($"Heiltränke: {spieler.Heiltraenke}");
            spieler.ZeigeInventar();

            
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
                   
                    spieler.RundePassiv();

                    
                    Console.WriteLine("\nAktion wählen: 1) Angreifen  2) Heiltrank benutzen  3) Waffe wechseln");
                    Console.Write("Deine Wahl: ");
                    string aktion = Console.ReadLine();

                    if (aktion == "2")
                    {
                        if (spieler.BenutzeHeiltrank())
                            Console.WriteLine($"Heiltrank benutzt. {spieler.HP}/{spieler.MaxHP} HP – Übrig: {spieler.Heiltraenke}");
                        else
                            Console.WriteLine("Kein Heiltrank vorhanden oder bereits volle HP.");
                    }
                    else if (aktion == "3")
                    {
                        WaffeWechseln(spieler);
                    }
                    else
                    {
                       
                        int spielerSchaden = BerechneSpielerSchadenMitCrit(spieler);
                        Console.WriteLine($"{spieler.Name} greift mit {spieler.Waffe.Name} an und verursacht {spielerSchaden} Schaden.");
                        gegner.NimmSchaden(spielerSchaden);
                        if (!gegner.IstLebendig())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{gegner.Name} ist besiegt!");
                            Console.ResetColor();
                            break; // nächster Gegner
                        }
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
                    case "4": return Spielerklasse.Heiler;
                    default:  Console.WriteLine("Ungültig. Bitte 1, 2, 3 oder 4 eingeben."); break;
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
                    default:  Console.WriteLine("Ungültig. Bitte 1, 2 oder 3 eingeben."); break;
                }
            }
        }

      
        private static int BerechneSpielerSchadenMitCrit(Spieler s)
        {
            int chance = rng.Next(1, 101); // 1..100
            if (chance <= 20) // 20% Crit
            {
                int crit = rng.Next(40, 61); // 40..60
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(">>> CRITICAL HIT! <<<");
                Console.ResetColor();
                return crit + s.KlassenSchadenBonus();
            }
            return s.Waffe.Schaden + rng.Next(0, 3) + s.KlassenSchadenBonus();
        }

        private static int BerechneGegnerSchaden(Gegner g)
        {
            return g.Schaden + rng.Next(0, 3);
        }

       
        private static void WaffeWechseln(Spieler s)
        {
            if (s.WaffenAnzahl == 0)
            {
                Console.WriteLine("Du hast keine weiteren Waffen im Inventar.");
                return;
            }

            Console.WriteLine("\nDein Waffen-Inventar:");
            s.ZeigeInventar();
            Console.Write("Index der Waffe zum Ausrüsten eingeben (0..n): ");
            var txt = Console.ReadLine();
            if (int.TryParse(txt, out int index))
            {
                if (s.WechselWaffe(index))
                    Console.WriteLine($"Waffe gewechselt zu: {s.Waffe}");
                else
                    Console.WriteLine("Ungültiger Index.");
            }
            else
            {
                Console.WriteLine("Eingabe ungültig.");
            }
        }
    }
}

