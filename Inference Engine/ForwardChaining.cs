using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	class ForwardChaining : InferenceEngine
	{
        List<String> inferred; //this list contains the premises that are inferred
        List<String> agenda;    //this list contains the symbols that are not yet processed.
        List<int> count;		//this contains the occurance of a premise within all implications
        List<string> symbolsEntailed; //this contains the flow of symbols that lead to the query
        List<string> clauses;


		public ForwardChaining(string input) : base(input)
		{
            //Initializes all the list and variables
            inferred = new List<string>();
            agenda = new List<string>();
            count = new List<int>();
            symbolsEntailed = new List<string>();
            clauses = new List<string>();
            initializeValues();

		}

        public void initializeValues()
        {
            for (int i = 0; i < ask.Length; i++)
            {
                //check if it is an implication else add it as a fact
                if (ask[i].Contains("=>"))
                {
                    //add this clause
                    //increase the count for that symbol
                    clauses.Add(ask[i]);
                    count.Add(ask[i].Split('&').Length);
                }
                else
                {
                    //add them as facts to the agenda
                    agenda.Add(ask[i]);
                }
            }
        }

        public bool containsSymbolInPremise(String clause, String symbol)
        {
            //Separate the implication into premise and head
            String[] separator = { "=>" };
            String[] components = clause.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            String premise = components[0];
            String head = components[1];
            if (premise.Contains("&"))
            {
                //if the premise contains two symbols then separate it and then check against symbol
                String[] symbols = premise.Split('&');
                //check the symbols against the given symbol
                return symbol.Contains(symbol);
            }
            else
            {
                //else return if the symbol matches the premise
                return premise.Contains(symbol);
            }
        }

        public override bool Infer()
        {
            //TODO: Finish This
            return false;
        }
    }
}
