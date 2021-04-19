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

        public NFA()
        {
            findRegExp();
        }

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
           // try
            {
                List<State> StateCopy = new List<State>();
                for (int i = 0; i < StateS.Count; i++)//making a copy of each state and put in StateCopy
                {
                    StateCopy.Add(new State(StateS[i].NameState));
                }
                for (int i = 0; i < StateS.Count; i++)
                {
                    State temp = StateCopy[i];
                    for (int j = 0; j < StateS[i].InV.Count; j++) 
                    {
                        var x = StateS[i].InV.ElementAt(j);
                        int k;
                        for (k = 0; k < StateS.Count; k++) if (x.Key.NameState == StateCopy[k].NameState) break;
                        temp.InV.Add(StateCopy[k], x.Value);
                    }
                    for (int j = 0; j < StateS[i].OutV.Count; j++)
                    {
                        var x = StateS[i].OutV.ElementAt(j);
                        int k;
                        for (k = 0; k < StateS.Count; k++) if (x.Key.NameState == StateCopy[k].NameState) break;
                        temp.OutV.Add(StateCopy[k], x.Value);
                    }
                    //StateCopy.Add(temp);
                }

                State finalState = new State("final");
                StateCopy.Add(finalState);
                for(int i = 0; i < StateCopy.Count; i++)
                    if (StateCopy[i].NameState.isfinal())
                    {
                        StateCopy[i].OutV.Add(finalState, "λ");//در آوردن استیت از حالت پایانی
                        finalState.InV.Add(StateCopy[i], "λ");
                    }

                State firstState = new State("first");
                if (StateS.Count == 0) throw new Exception("mashin tohi ast");
                firstState.OutV.Add(StateCopy[0], "λ");
                StateCopy[0].InV.Add(firstState, "λ");
                StateCopy.Insert(0, firstState);



                while (StateCopy[1] != finalState)
                {
                    State temp = StateCopy[1];
                    bool togheh = temp.OutV.ContainsKey(temp);//شامل یال طوقه باشد
                    for (int i = 0; i < temp.InV.Count; i++)
                    {
                        var x = temp.InV.ElementAt(i);

                        if (x.Key == temp) continue;

                        x.Key.OutV.Remove(temp);

                        for (int j = 0; j < temp.OutV.Count; j++)
                        {
                            var y = temp.OutV.ElementAt(j);
                            if (y.Key == temp) continue;

                            if (x.Key.OutV.ContainsKey(y.Key))//x -> y 
                            {
                                if (togheh)
                                {
                                    string r = x.Key.OutV[y.Key] + "+" + "(" + x.Value + ")" + "(" + temp.OutV[temp] + ")*" + y.Value;
                                    x.Key.OutV[y.Key] = r;
                                    y.Key.InV[x.Key] = r;
                                }
                                else
                                {
                                    string r = x.Key.OutV[y.Key] + "+" + "(" + x.Value + ")" + y.Value;
                                    x.Key.OutV[y.Key] = r;
                                    y.Key.InV[x.Key] = r;
                                }
                            }
                            else //x -/-> y
                            {
                                if (togheh)
                                {
                                    string r = "(" + x.Value + ")" + "(" + temp.OutV[temp] + ")*" + y.Value;
                                    x.Key.OutV.Add(y.Key, r);
                                    y.Key.InV.Add(x.Key, r);
                                }
                                else
                                {
                                    string r = "(" + x.Value + ")" + y.Value;
                                    x.Key.OutV.Add(y.Key, r);
                                    y.Key.InV.Add(x.Key, r);
                                }
                            }
                        }
                    }
                    for (int k = 0; k < temp.OutV.Count; k++)
                    {
                        var y = temp.OutV.ElementAt(k);
                        y.Key.InV.Remove(temp);
                    }
                    StateCopy.Remove(temp);
                }

                if (firstState.OutV.ContainsKey(finalState)) Console.WriteLine(firstState.OutV[finalState]);
                else Console.WriteLine("na motanahi");/////////////////////
           }
            //catch(Exception e)
            {
          //     Console.WriteLine(e.Message);
            }
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
