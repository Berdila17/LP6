namespace TextRPG
{
    // Klasse beschreibt eine Waffe 
    public class Waffe
    {
        // Eigenschaften 
        public string Name { get; }
        public int Schaden { get; }

        //Waffe wird beim Erstellen mit Name und Schaden initialisiert
        public Waffe(string name, int schaden)
        {
            Name = name;
            Schaden = schaden;
        }

        // Überschreiben von ToString damit beim Ausgeben der Waffenname angezeigt wird (Mit ChatGBT gemacht er hat mir diesen Tipp gegeben)
        public override string ToString() => $"{Name} (Schaden {Schaden})";
    }
}
