using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

namespace TLA_Project
{
    class NFA
    {
        public static string InitialState;
        public static string[] Alphabet;
        public static string[] final;
        static Stack<object> stack_help = new Stack<object>();
        public static Dictionary<string, int> maping = new Dictionary<string, int>();
        public static List<State> StateS = new List<State>();
        private bool isAcceptByNFA()
        {
            string Tape_String = Console.ReadLine().ToLower();
            if ((Tape_String.Length == 0 && InitialState.isfinal()))
            {
                return true;
            }
            if (Tape_String.check_string(Alphabet.ToList()))
            {
                return false;
            }
            else
            {
                char[] tape_arr = Tape_String.ToCharArray();
                List<char> tape_list = tape_arr.ToList();
                State now = StateS[0];
                while (tape_list.Count != 0)
                {
                    if (now.deltafunction[maping[tape_list[0].ToString()]] != null)
                    {

                        List<State> ss = now.deltafunction[maping[tape_list[0].ToString()]];
                        addtostack(ss);
                        now = (State)stack_help.Pop();
                        tape_list.RemoveAt(0);
                    }
                    else if (now.deltafunction[maping[" "]] != null)
                    {
                        List<State> ss = now.deltafunction[maping[" "]];
                        addtostack(ss);
                        now = (State)stack_help.Pop();
                        tape_list.RemoveAt(0);
                    }
                    else
                    {
                        now = (State)stack_help.Pop();
                    }
                }

                if (final.Any(x => x == now.NameState))
                {
                    return true;
                }


                return false;


            }

        }
        private void createEquivalentDFA()
        {

        }
        private void findRegExp()
        {
            List<State> StateCopy = new List<State>();

            foreach(State state in StateS)//making a copy of each state and put in StateCopy
            {
                State temp = new State(state.NameState);
                foreach(var x in state.InV)
                {
                    temp.InV.Add(x.Key, x.Value);
                }
                foreach (var x in state.OutV)
                {
                    temp.OutV.Add(x.Key, x.Value);
                }
                StateCopy.Add(temp);
            }

            State finalState = new State("final");
            StateCopy.Add(finalState);
            foreach (State x in StateCopy) if (x.NameState.isfinal()) x.OutV.Add(finalState, "λ");//در آوردن استیت از حالت پایانی

            State firstState = new State("first");
            firstState.OutV.Add(StateCopy[0], "λ");
            StateCopy.Insert(0, firstState);



        }
        private void addtostack(IEnumerable s)
        {
            foreach (var item in s)
            {
                stack_help.Push(item);
            }
        }
        public static List<string> tostringarr(string tape)
        {
            List<string> s = new List<string>();
            for (int i = 0; i < tape.Length; i++)
            {
                s.Add(tape[i].ToString());
            }
            return s;
        }
    }
    static class extension
    {
        static public bool check_string(this string tape_string, List<string> alphabet)
        {
            List<string> ta = NFA.tostringarr(tape_string);

            var ex = ta.Except(alphabet);
            return ex.Count() == 0 ? false : true;

        }
        static public bool isfinal(this string state)
        {
            foreach (var item in NFA.final)
            {
                if (item == state)
                {
                    return true;
                }
            }
            return false;
        }
    }
    
}
