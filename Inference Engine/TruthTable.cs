using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	class TruthTable : InferenceEngine
	{
		private int count = 0;
		private int columnCount;

		//Calculate truth table row
		//TODO: Figure out why it's giving a list of 3-5 results instead of always 4.
		private List<bool> getRow(int rowNum)
		{
			List<bool> result = new List<bool>();
			string rowNumStr;
			char[] rowNumArray;

			//Convert the row number into a binary string
			rowNumStr = Convert.ToString(rowNum, 2);
			//TODO: Delete This
			//rowNumStr.Reverse();

			//Pad out the string with extra zeros
			rowNumStr = rowNumStr.PadLeft(columnCount, '0');
			rowNumArray = rowNumStr.ToCharArray();

			//TODO: Delete this
			//padding = new string('*', facts.Count - rowNumStr.Length);
			//rowNumStr = rowNumStr.concat(padding);

			//Generate the truth table row based on input
			for (int i = 0; i < rowNumStr.Length; i++)
			{

				if (rowNumArray[i] == '1')
				{
					result.Add(true);
				}
				else
				{
					result.Add(false);
				}
			}

			//Convert into an array
			return result;
			
			//TODO: Delete this
			//return new[] { rowNumStr };

		}

		private bool testKB(List<bool> row)
		{
			foreach (string clause in clauses)
			{
				bool left = true;
				bool right = true;

				string[] clauseFacts;
				string[] leftFacts;


				//Break clause into facts
				clauseFacts = clause.Split(
					new[] { "=>" },
					StringSplitOptions.RemoveEmptyEntries
					);

				//Get corresponding boolean values for facts
				right = row[facts.IndexOf(clauseFacts[1])];
				//If left side contains more than one fact, combine all corresponding row values
				if (clauseFacts[0].Contains('&'))
				{
					leftFacts = clauseFacts[0].Split(
					new[] { "&" },
					StringSplitOptions.RemoveEmptyEntries
					);

					for (int i = 0; i < leftFacts.Length; i++)
					{
						left = left && row[facts.IndexOf(leftFacts[0])];
					}

				}
				//If the left side contains one fact, find the corresponding boolean value for this world.
				else
				{
					left = row[facts.IndexOf(clauseFacts[0])];
				}

				if (!(!left | right))
				{
					return false;
				}
			}
			return true;
		}

		private bool testAlpha(List<bool> row)
		{
			//TODO: Find the corresponding row in the table
			//bool alpha = row[row.Count-1];
			bool alpha = row[facts.IndexOf(question)];
			return false;
		}

		public override string Infer()
		{
			//Foreach node in KB.iterateOverAllModels() do
			for (int i = 0; i <= Math.Pow(2, columnCount); i++)
			{
				List<bool> row = getRow(i);

				//If Test (n, KB) true
				if (testKB(row))
				{
					//If not Test(n, Q) then
					if (testAlpha(row))
					{
						return "NO";
					}
					else
					{
						count++;
					}
				}
			}
			return "YES " + count;
		}

		/*
		* Adds all the facts in the clauses list to the facts list
		*/ 
		private void AddAllFacts()
		{
			string[] clauseFacts;   //Holds the facts in each clause
			string[] leftFacts;		//Holds the facts in the left side of the clause (if there's more than one fact)
			bool inFactsList;       //Used to determine if fact is already in the facts list

			//Go through all clauses and extract facts
			foreach (string clause in clauses)
			{
				//Get an array of facts from the clause
				clauseFacts = clause.Split(
					new[] { "=>" },
					StringSplitOptions.RemoveEmptyEntries
					);

				//Add right fact
				addFact(clauseFacts[1]);

				//If multiple facts on left side of clause, extract each one and add to list of facts
				if (clauseFacts[0].Contains("&"))
				{
					//Extract each fact
					leftFacts = clause.Split(
					new[] { "&" },
					StringSplitOptions.RemoveEmptyEntries
					);

					//Add each fact to the list
					for (int i = 0; i > clauseFacts.Length; i++)
					{
						addFact(leftFacts[i]);
					}
				}
				//If only one fact on left side, add to facts list
				else
				{
					addFact(clauseFacts[0]);
				}
			}
		}

		/*
		 * Adds a single fact to the facts list if it isn't already in the list.
		 * @fact: the fact to be added
		*/
		private void addFact(string fact)
		{
			bool inList = false;	//Used to determine if fact is already in list
			//If fact isn't in facts list, add it
			foreach (string f in facts)
			{
				if (fact == f)
				{
					inList = true;
					break;
				}
			}
			if (!inList)
			{
				facts.Add(fact);
			}
		}

		public TruthTable(string input) : base(input)
		{
			AddAllFacts();
			//columnCount = facts.Count() + 1;
			columnCount = facts.Count();

		}
	}
}
