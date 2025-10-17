using System;
using System.Collections.Generic;

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

        
        private readonly List<Waffe> _waffen = new List<Waffe>();
        public int Heiltraenke { get; set; } = 0;

        public Spieler(string name, Spielerklasse klasse, int maxHp)
        {
            Name = name;
            Klasse = klasse;
            MaxHP = Math.Max(1, maxHp);
            HP = MaxHP;

            
            Waffe = klasse switch
            {
                Spielerklasse.Krieger => new Waffe("Schwert", 8),
                Spielerklasse.Magier => new Waffe("Zauberstab", 6),
                Spielerklasse.Dieb => new Waffe("Dolch", 5),
                Spielerklasse.Heiler => new Waffe("Stab", 6),
                _ => new Waffe("Holzknüppel", 3)
            };
            
            _waffen.Add(Waffe);
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
            Console.WriteLine($"{Name} ({Klasse}) – HP: {HP}/{MaxHP}, Waffe: {Waffe}");
        }

        public void SetzeWaffe(Waffe neueWaffe)
        {
            if (neueWaffe == null) return;
            Waffe = neueWaffe;
            if (!_waffen.Contains(neueWaffe))
                _waffen.Add(neueWaffe);
        }

        public int WaffenAnzahl => _waffen.Count;

        public void FuegeWaffeHinzu(Waffe w)
        {
            if (w != null) _waffen.Add(w);
        }

        public bool WechselWaffe(int index)
        {
            if (index < 0 || index >= _waffen.Count) return false;
            Waffe = _waffen[index];
            return true;
        }

        public void ZeigeInventar()
        {
            for (int i = 0; i < _waffen.Count; i++)
                Console.WriteLine($"[{i}] {_waffen[i]}{(Waffe == _waffen[i] ? "  (ausgerüstet)" : "")}");
        }

        
        public void RundePassiv()
        {
            if (Klasse == Spielerklasse.Heiler && IstLebendig())
            {
                int vorher = HP;
                Heilen(2);
                if (HP > vorher)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{Name} (Heiler) regeneriert +2 HP (jetzt {HP}/{MaxHP}).");
                    Console.ResetColor();
                }
            }
        }

        
        public int KlassenSchadenBonus() => Klasse == Spielerklasse.Krieger ? 2 : 0;

       
        public bool BenutzeHeiltrank()
        {
            if (Heiltraenke <= 0) return false;
            if (HP >= MaxHP) return false;

            Heiltraenke--;
            
            int heal = Math.Max(10, (int)Math.Round(MaxHP * 0.25));
            Heilen(heal);
            return true;
        }
    }
}

