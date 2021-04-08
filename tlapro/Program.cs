using System;

namespace tlapro
{
    class Program
    {
        static void Main(string[] args)
        {

            int i = 0;
            string[] States=Console.ReadLine().Split(',');
            string[] Alphabet = Console.ReadLine().Split(',');
            string[] FinalState = Console.ReadLine().Split(',');
            int Transfers = int.Parse(Console.ReadLine());
            for ( i = 0; i < Alphabet.Length; i++)
            {
                NFA.maping.Add(Alphabet[i],i);
            }
            NFA.maping.Add(" ", i);
            NFA.InitialState = States[0];
            for (int j = 1; j <= Transfers; j++)
            {
                
                string[] delta = Console.ReadLine().Split(',');
                var help = ishere(delta[0]);
                if (NFA.StateS.Count == 0||help==null)
                {
                    NFA.StateS.Add(new State(delta[0], delta[1], NFA.maping[delta[2]]));
                }
                else
                {
                    help.adding(delta[1], NFA.maping[delta[2]]);
                    
                }
                
            }
            
        }
        static State ishere(string here)
        {

            foreach (var item in NFA.StateS)
            {
                if (item.NameState == here)
                    return item;
            }
            return null;
           
        }
        
    }
}
