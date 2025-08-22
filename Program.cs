namespace RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Erzeugen eines Objekts
            Charakter paladin;
            paladin = new Charakter();

            //Attribute setzen
            paladin.Name = "Paladin";
            paladin.HP = 75;

            //Methode aufrufen

            paladin.Vorstellen();
            Console.ReadLine();


        }
    }
}
