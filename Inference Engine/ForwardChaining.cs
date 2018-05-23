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


		public ForwardChaining(string input) : base(input)
		{
            //Initializes all the list and variables
            inferred = new List<string>();
            agenda = new List<string>();
            count = new List<int>();
            initializeValues();

		}

        public override string getPath()
        {
            //this method outputs the path to the goal
            String output = "";
            for (int i = 0; i < inferred.Count; i++)
            {
                output += inferred[i] + ",";
            }
            return output += question;
        }

        protected void initializeValues()
        {
            for (int i = 0; i < clauses.Count; i++)
            {
                //for each clause increase the count for number of clause in premise
                count.Add(clauses[i].Split('&').Length);
            }
            //add facts to the agenda
            for (int i = 0; i < facts.Count; i++)
            {
                agenda.Add(facts[i]);
            }
        }

        protected bool containsSymbolInPremise(String clause, String symbol)
        {
            //Separate the implication into premise and head
            String[] components = DifferentiateComponents(clause);
            String premise = components[0];
            String head = components[1];
            if (premise.Contains("&"))
            {
                //if the premise contains two symbols then separate it and then check against symbol
                String[] symbols = premise.Split('&');
                //check the symbols against the given symbol
                return symbols.Contains(symbol);
            }
            else
            {
                //else return if the symbol matches the premise
                return premise.Contains(symbol);
            }
        }

        protected string[] DifferentiateComponents(String clause)
        {
            String[] separator = { "=>" };
            string[] components = clause.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < components.Length; i++)
            {
                components[i] = components[i].Trim();
            }
            return components;
        }

        public override string Infer()
        {
            //TODO: Finish This
            while (agenda.Count != 0)
            {
                //get the first fact to process through the clauses
                String symbol = agenda[0];
                //pop the item of the list
                agenda.RemoveAt(0);
                //Add this to inferred list as this symbol is being processed
                inferred.Add(symbol);
                //check if the fact matches the query
                if (symbol == question)
                {
                    return "true";
                }
                for (int i = 0; i < clauses.Count; i++)
                {
                    //check if the symbol exist within any premise among all clauses in knowledge base
                    if (containsSymbolInPremise(clauses[i], symbol))
                    {
                        //if yes then deduct the count by 1 and if the count is equal to 0 then add the head of the implication to the agenda
                        count[i] = count[i] - 1;
                        if (count[i] == 0)
                        {
                            String[] components = DifferentiateComponents(clauses[i]);
                            String head = components[1];
                            //check again if the head matches the query
                            if (head.Equals(question))
                            {
                                return "true";
                            }
                            agenda.Add(head);
                        }
                    }
                }
            }
            return "false";
        }
    }
}
