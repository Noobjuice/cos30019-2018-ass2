using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	class TruthTable : InferenceEngine
	{
		private int count = 0;      //Total rows entailed by knowledge base
		private Int64 rowCount;		//Number of rows in the truth table
		private int columnCount;    //Number of columns in the truth table

		private List<String> colHeadings;   //List of all the literals, including ones that are part of clauses
		private List<String> alphaClauses = new List<string>(); //List of all the clauses that contain the question (alpha).

		/*
		* Gets the value of a boolean for a coresponding literal in the truth table
		* @columnHeading: The literal being searched for
		* @row: the current row in the truth table
		*/
		private bool getRowValue(string columnHeading, List<bool> row)
		{
			return row[colHeadings.IndexOf(columnHeading)];
		}

		/*
		* Adds all the facts in the clauses list to the facts list
		*/
		private void getColHeadings()
		{
			string[] splitClause;      //Holds the left and right side of the clause after they're split on
			string[] leftLiterals;     //Holds the facts in the left side of the clause (if there's more than one fact)
			
			//Make a new list of facts to add the clause literals to
			colHeadings = new List<string>(facts);

			//Go through all clauses and extract facts
			foreach (string clause in clauses)
			{
				//Get an array of facts from the clause
				splitClause = clause.Split(
					new[] { "=>" },
					StringSplitOptions.RemoveEmptyEntries
					);

				//Add literal from the right of the clause
				addColHeader(splitClause[1]);

				//If there are multiple literals on left side of clause, extract each one and add to list of literals
				if (splitClause[0].Contains("&"))
				{
					//Extract each literal
					leftLiterals = splitClause[0].Split(
					new[] { "&" },
					StringSplitOptions.RemoveEmptyEntries
					);

					//Add each literal to the list
					for (int i = 0; i < splitClause.Length; i++)
					{
						addColHeader(leftLiterals[i]);
					}
				}
				//If only one literal on left side, add to literals list
				else
				{
					addColHeader(splitClause[0]);
				}
			}
		}

		/*
		 * Adds a single literal to the literals list if it isn't already present in the list.
		 * @literal: the literal to be added
		*/
		private void addColHeader(string literal)
		{
			bool inList = false;    //Used to determine if literal is already in list
			
			//Check if the literal is already in the list
			foreach (string l in colHeadings)
			{
				if (l == literal)
				{
					inList = true;
					break;
				}
			}
			//If literal isn't in the list, add it
			if (!inList)
			{
				colHeadings.Add(literal);
			}
		}

		/*
		* Creates a list of all the clauses that have the question (alpha) in them.
		*/
		private void GetAlphaClauses()
		{
			string[] splitClause;		//Holds the left and right side of the clause after they're split on "=>"
			string[] leftLiterals;     //Holds the literals on the literals side of the clause (if more than one)

			//Check all the clauses
			foreach (string clause in clauses)
			{

				//Break clause into facts
				splitClause = clause.Split(
					new[] { "=>" },
					StringSplitOptions.RemoveEmptyEntries
					);

				//Check if right side of clause contains question
				if (question == splitClause[1])
				{
					alphaClauses.Add(clause);
				}
				//Check if left side of clause contains question
				else
				{
					//If left side has multiple literals, check them all
					if (splitClause[0].Contains('&'))
					{
						//Split the left side of the clause into multiple literals
						leftLiterals = splitClause[0].Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);

						//Check all literals to see if they contain the question
						for (int i = 0; i < splitClause.Length; i++)
						{
							if (question == leftLiterals[i])
							{
								alphaClauses.Add(clause);
								break;
							}
						}
					}
					//If the left side contains one literal, check if that is the question
					else
					{
						if (question == splitClause[1])
						{
							alphaClauses.Add(clause);
						}
					}
				}
			}
		}

		/*
		* Generates each row in the truth table
		* @rowNum: The current row number
		*/
		private List<bool> getRow(int rowNum)
		{
			List<bool> result = new List<bool>();	//The results of the current row as a list of boolean values
			string rowNumStr;						//The row number, converted to a string in binary.
			char[] rowNumArray;                     //The binary string row number converted into a char array

			//Convert the row number into a binary string
			rowNumStr = Convert.ToString(rowNum, 2);

			//Pad out the string with extra zeros
			rowNumStr = rowNumStr.PadLeft(columnCount, '0');
			rowNumArray = rowNumStr.ToCharArray();

			//Generate the truth table row based on input
			for (int i = 0; i < rowNumStr.Length; i++)
			{
				//If value in array is '1', add true to the result
				if (rowNumArray[i] == '1')
				{
					result.Add(true);
				}
				//If value in array is '0', add true to the result
				else
				{
					result.Add(false);
				}
			}

			//Return the current row as a list of boolean values
			return result;
		}

		/*
		* Solves a clause using the current row in the truth table
		* @clause: the clause to be solved
		* @row: the current row in the truth table
		*/
		private bool solveClause(string clause, List<bool> row)
		{
			bool left = true;			//Used to store result of left side of clause
			bool right = true;			//Used to store result of right side of clause
			string[] clauseLiterals;	//Holds all the facts in the clause
			string[] leftLiterals;		//Holds the facts on the left side of the clause (if more than one)

			//Split clause on the "=>"
			clauseLiterals = clause.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);

			//Get corresponding boolean values for literals
			right = getRowValue(clauseLiterals[1], row);

			//If left side contains more than one literals, combine all corresponding boolean values
			if (clauseLiterals[0].Contains('&'))
			{
				//Extract each literal on the left side of the clause
				leftLiterals = clauseLiterals[0].Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);

				//
				for (int i = 0; i < leftLiterals.Length; i++)
				{
					left = left && getRowValue(leftLiterals[i], row);
				}

			}
			//If the left side contains one literals, find the corresponding boolean value.
			else
			{
				left = getRowValue(clauseLiterals[0], row);
			}

			//Solve the clause
			return !left | right;
		}


		/*
		* Tests the current row of the truth table to see if it's entailed in the knowlege base
		* @row: the current row in the truth table
		*/
		private bool testKB(List<bool> row)
		{
			//Check if all the facts are true on current row
			foreach (string fact in facts)
			{
				//If the current fact is false, the row isn't entailed in the knowlege base
				if (getRowValue(fact, row) == false)
				{
					return false;
				}
			}

			//Check if all the clauses are true on the current row
			foreach (string clause in clauses)
			{
				//If the current clause is false, the row isn't entailed in the knowlege base
				if (solveClause(clause, row) == false)
				{
					return false;
				}
			}
			
			//All facts and clauses are true, so the row is entailed in the knowlege base
			return true;
		}

		/*
		* Checks if the question (alpha) is entailed in the knowlege base
		*/
		public override bool Infer()
		{
			List<bool> row;			//The current row of the truth table
			bool alphaInKB = false;	//Used to check if the question (alpha) is in any of the facts or clauses in the knowlege base

			//Check if the alpha literal is contained somewhere in the knowlege base
			foreach (string s in colHeadings)
			{
				if (s == question)
				{
					alphaInKB = true;
					break;
				}
			}

			//If alpha isn't present in the knowlege base, end the search now.
			if (!alphaInKB)
			{
				return false;
			}

			//Generate each row of the truth table
			for (int i = 0; i < rowCount; i++)
			{
				//Get the current row of the truth table
				row = getRow(i);

				//Check if the current row is entailed in the knowlege base
				if (testKB(row))
				{
					//If alpha is true in the current row of the truth table, increment the number of true worlds
					if (getRowValue(question, row))
					{
						count++;
					}
					//If alpha isn't true, then alpha isn't entailed in the knowlege base
					else
					{
						return false;
					}
				}
			}
			//If alpha is true on all rows that are entailed in the knowlege base, then alpha is also entailed.
			return true;
		}

		/*
		* Returns a string of the number of true rows in the knowlege base.
		*/
		public override string getResult()
		{
			return count.ToString();
		}

		public TruthTable(string input) : base(input)
		{
			//Get a list of all the literals
			getColHeadings();
			
			//Get all the clauses that contain the question (alpha)
			GetAlphaClauses();

			//Calculate the number of collumns and rows
			columnCount = colHeadings.Count();
			rowCount = Convert.ToInt64(Math.Pow(2, columnCount));
		}
	}
}
