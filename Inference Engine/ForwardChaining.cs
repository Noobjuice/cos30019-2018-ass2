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
            foreach (String str in clauses)
            {
                Console.WriteLine(str);
            }
        }

        public override bool Infer()
        {
            //TODO: Finish This
            return false;
        }
    }
}
