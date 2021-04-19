using System;
using System.Collections.Generic;
using System.Linq;

namespace TLA_Project
{
    class Program
    {
        static void Main(string[] args)
        {

            int i = 0;
            string[] States = Console.ReadLine().Split(',');
            string[] Alphabet = Console.ReadLine().Split(',');
            string[] FinalState = Console.ReadLine().Split(',');
            int Transfers = int.Parse(Console.ReadLine());
            for (i = 0; i < Alphabet.Length; i++)
            {
                NFA.maping.Add(Alphabet[i], i);
            }
            NFA.maping.Add(" ", i);
            NFA.InitialState = States[0];
            NFA.Alphabet = Alphabet;
            NFA.final = FinalState;
            for (int r = 0; r < States.Length; r++)
            {
                NFA.StateS.Add(new State(States[r]));
            }
            for (int j = 1; j <= Transfers; j++)
            {

                string[] delta = Console.ReadLine().Split(',');
                var help = ishere(delta[0]);
                var help2 = ishere(delta[1]);
                if (help.deltafunction[NFA.maping[delta[2]]] == null)
                {
                    help.deltafunction[NFA.maping[delta[2]]] = new List<State>();
                    help.deltafunction[NFA.maping[delta[2]]].Add(help2);

                }
                else
                {
                    help.deltafunction[NFA.maping[delta[2]]].Add(help2);
                }

                //added by ali for findRegExp method
                var state1 = ishere(delta[0]);
                var state2 = ishere(delta[1]);
                if (delta[2] == " ") delta[2] = "λ";
                if (state1.OutV.ContainsKey(state2))
                {
                    state1.OutV[state2] = state1.OutV[state2] + "+" + delta[2];
                    state2.InV[state1] = state2.InV[state1] + "+" + delta[2];
                }
                else
                {
                    state1.OutV.Add(state2, delta[2]);
                    state2.InV.Add(state1, delta[2]);
                }
                //
            }
            NFA FA = new NFA();

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
