﻿using System;
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

        protected void initializeValues()
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
                return symbol.Contains(symbol);
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
            return components;
        }

        public override bool Infer()
        {
            //TODO: Finish This
            while (agenda.Count != 0)
            {
                //get the first fact to process through the clauses
                String symbol = agenda[0];
                Console.WriteLine(symbol);
                //pop the item of the list
                agenda.RemoveAt(0);
                //Add this to inferred list as this symbol is being processed
                inferred.Add(symbol);
                //check if the fact matches the query
                if (symbol == tell)
                {
                    return true;
                }
                foreach (String clause in clauses)
                {
                    Console.WriteLine(clause);
                    int i = 0;
                    //check if the symbol exist within any premise among all clauses in knowledge base
                    if (containsSymbolInPremise(clause, symbol))
                    {
                        //if yes then deduct the count by 1 and if the count is equal to 0 then add the head of the implication to the agenda
                        count[i] = count[i] - 1;
                        if (count[i] == 0)
                        {
                            Console.WriteLine("One premise completed");
                            String[] components = DifferentiateComponents(clause);
                            String head = components[1];
                            //check again if the head matches the query
                            if (head.Equals(tell))
                            {
                                return true;
                            }
                            agenda.Add(head);
                        }
                    }
                    i++;
                }
            }
            return false;
        }
    }
}
