using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RPG
{
    // Klasse
    public class Waffe
    {
        // Eigenschaften 
        public string Name { get; }
        public int Schaden { get; }


        public Waffe(string name, int schaden)
        {
            Name = name;
            Schaden = schaden;
        }

        // Anzeigen in der Konsole
        public override string ToString() => $"{Name} (Schaden {Schaden})";
    }

}
