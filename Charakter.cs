using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{

    //Klasse erstellen und Attribute und Methoden festsetzen
    public class Charakter
    {

        public string Name;
        public int HP;

        public void Vorstellen()
        {
            Console.WriteLine($"Was geht ich bin ein {Name} und habe {HP} HP!");

        }


    }
}
