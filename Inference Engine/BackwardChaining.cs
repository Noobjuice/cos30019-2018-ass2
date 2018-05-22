using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{

	class BackwardChaining : InferenceEngine
	{
        List<String> agenda;
        List<String> inferred;
        public BackwardChaining(string input) : base(input)
        {
            //initializing values
            agenda = new List<string>();
            inferred = new List<string>();
            //adding the question to the agenda
            agenda.Add(question);
        }
        

        protected string[] getComponents(String clause)
        {
            //returns the head and premise of the clause as an array
            string[] array = { "=>" };
            string[] components = clause.Split(array, StringSplitOptions.RemoveEmptyEntries);
            return components;
        }

        protected string[] getSymbolsFromPremise(String premises)
        {
            string[] symbols = premises.Split('&');
            return symbols;
        }

        public override string Infer()
		{
            while (agenda.Count != 0)
            {
                //getting the first symbol of the agenda
                String symbol = agenda[0];
                //removing the popper symbol from the agenda
                agenda.RemoveAt(0);
                //adding it to the inferred list
                inferred.Add(symbol);
                //If the given query is a fact it is true already as facts are true by themselves
                if (!facts.Contains(symbol))
                {
                    //if the question is not a fact then we need to add the new symbols to a temporary list
                    List<String> temp = new List<string>();
                    //go through the clauses to check if the question matches any conclusion and if it does than add the premises to the temp list
                    for (int i = 0; i < clauses.Count; i++)
                    {
                        String[] components = getComponents(clauses[i]);
                        String head = components[1];
                        //check head against the question
                        if (head.Equals(symbol))
                        {
                            //For Debugging
                            // Console.WriteLine("Found a clause head matching symbol" + " Clause "+ clauses[i] + " Head: " + head);
                            //get the premises of that clause
                            //premises should not be already in the agenda which would created infinite loop
                            //add the symbols from premises to the temp list created
                            //checking first if the premises contain multiple symbols
                            if (components[0].Contains("&"))
                            {
                                String[] symbols = getSymbolsFromPremise(components[0]);
                                //Checking if any of the symbols are already in the agenda before adding it to the list
                                for (int j = 0; j < symbols.Length; j++)
                                {
                                    if (!agenda.Contains(symbols[j]))
                                    {
                                        //if agenda does not contain any of the symbols from premise
                                        //add it to the temp list
                                        temp.Add(symbols[j]);
                                    }
                                }
                            }
                            else
                            {
                                //check the lone symbol in the premise against agenda
                                if (!agenda.Contains(components[0]))
                                {
                                    //if the symbol is not there in the agenda
                                    //add it to the temp list holding new symbols
                                    temp.Add(components[0]);
                                }
                            }

                        }
                    }
                    //check if there are symbols there in the temp list 
                    if (temp.Count == 0)
                    {
                        //As there were no clauses which had a conclusion same as question
                        //there is no possible solution for it
                        return "false";
                    }
                    else
                    {
                        //add the generate symbols stored in the temp list to the agenda
                        //Checking it against inferred hence making sure duplicates are not inserted in the agenda
                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (!inferred.Contains(temp[i]))
                            {
                                //Since these symbols are not processed yet and were not already in agenda
                                //risks of duplication and infinite loop have been removed
                                //hence we can add the symbols to the agenda for going till the fact
                                //to prove that the question is true
                                agenda.Add(temp[i]);
                            }
                        }
                    }
                }
            }
			return "true";
		}

        public override string getPath()
        {
            inferred.Reverse();
            String output = "";
            for (int i = 0; i < inferred.Count; i++)
            {
                output += inferred[i];
                if (i != inferred.Count - 1)
                {
                    output += ", ";
                }
            }
            return output;
        }

        
	}
}
