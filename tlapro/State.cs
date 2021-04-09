using System;
using System.Collections.Generic;
using System.Text;

namespace tlapro
{
    class State
    {
         public string NameState;
          public List<State>[] deltafunction =new List<State>[50];

     
        public State(string nameState)
        {
            NameState = nameState;

         }
   
    }
}
