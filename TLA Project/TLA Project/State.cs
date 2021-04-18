using System;
using System.Collections.Generic;
using System.Text;

namespace TLA_Project
{
    class State
    {
        public string NameState;
        public List<State>[] deltafunction = new List<State>[50];

        //Input edges
        public Dictionary<State, string> InV = new Dictionary<State, string>();
        //

        //Output edges
        public Dictionary<State, string> OutV = new Dictionary<State, string>();
        //

        public State(string nameState)
        {
            NameState = nameState;
            
        }

    }
}
