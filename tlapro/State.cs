using System;
using System.Collections.Generic;
using System.Text;

namespace tlapro
{
    class State
    {
         public string NameState;
         List<string>[] deltafunction =new List<string>[50];

        public State(string nameState,string tra,int alph)
        {
            NameState = nameState;
          
            if(deltafunction[alph]==null)
            {
                deltafunction[alph] = new List<string>();
                deltafunction[alph].Add(tra);
            }
            else
            {
                deltafunction[alph].Add(tra);
            }
            
           
        }
        public void adding(string tra, int alp)
        {
            if (deltafunction[alp] == null)
            {
                deltafunction[alp] = new List<string>();
                deltafunction[alp].Add(tra);
            }
            else
            {
                deltafunction[alp].Add(tra);
            }
           
        }
    }
}
