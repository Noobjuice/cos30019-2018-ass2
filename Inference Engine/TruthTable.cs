﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	class TruthTable : InferenceEngine
	{
		private int count = 0;      //Total rows entailed by knowlege base
		private int rowCount;       //Number of rows in the truth table
		private int columnCount;    //Number of collumns in the truth table

		private List<String> colHeadings;   //A list of all the facts, including ones that are part of clauses
		private List<String> alphaClauses = new List<string>();

		/*
		* Adds all the facts in the clauses list to the facts list
		*/
		private void getColHeaders()
		{
			string[] clauseLiterals;   //Holds the facts in each clause
			string[] leftLiterals;     //Holds the facts in the left side of the clause (if there's more than one fact)

			//Go through all clauses and extract facts
			foreach (string clause in clauses)
			{
				//Get an array of facts from the clause
				clauseLiterals = clause.Split(
					new[] { "=>" },
					StringSplitOptions.RemoveEmptyEntries
					);

				//Add right fact
				addColHeader(clauseLiterals[1]);

				//If multiple facts on left side of clause, extract each one and add to list of facts
				if (clauseLiterals[0].Contains("&"))
				{
					//Extract each fact
					leftLiterals = clauseLiterals[0].Split(
					new[] { "&" },
					StringSplitOptions.RemoveEmptyEntries
					);

					//Add each fact to the list
					for (int i = 0; i < clauseLiterals.Length; i++)
					{
						addColHeader(leftLiterals[i]);
					}
				}
				//If only one fact on left side, add to facts list
				else
				{
					addColHeader(clauseLiterals[0]);
				}
			}
		}

		/*
		 * Adds a single fact to the facts list if it isn't already in the list.
		 * @fact: the fact to be added
		*/
		private void addColHeader(string fact)
		{
			bool inList = false;    //Used to determine if fact is already in list
			//If fact isn't in facts list, add it
			foreach (string f in colHeadings)
			{
				if (fact == f)
				{
					inList = true;
					break;
				}
			}
			if (!inList)
			{
				colHeadings.Add(fact);
			}
		}

		/*
		* TODO: Finish This
		*/
		private void GetAlphaClauses()
		{
			string[] clauseLiterals;   //Holds all the facts in the clause
			string[] leftLiterals;     //Holds the facts on the left side of the clause (if more than one)

			foreach (string clause in clauses)
			{

				//Break clause into facts
				clauseLiterals = clause.Split(
					new[] { "=>" },
					StringSplitOptions.RemoveEmptyEntries
					);

				//Check if right side of clause contains question
				if (question == clauseLiterals[1])
				{
					alphaClauses.Add(clause);
				}
				//Check if left side of clause contains question
				else
				{
					//If left side has multiple facts, check them all
					if (clauseLiterals[0].Contains('&'))
					{
						leftLiterals = clauseLiterals[0].Split(
							new[] { "&" },
							StringSplitOptions.RemoveEmptyEntries
							);
						for (int i = 0; i < clauseLiterals.Length; i++)
						{
							if (question == leftLiterals[i])
							{
								alphaClauses.Add(clause);
								break;
							}
						}
					}
					//If the left side contains one fact, check if it's the question
					else
					{
						if (question == clauseLiterals[1])
						{
							alphaClauses.Add(clause);
						}
					}
				}
			}
		}

		/*
		* TODO: Finish This
		*/
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

			return result;
		}

		/*
		* TODO: Finish This
		*/
		private bool solveClause(string clause, List<bool> row)
		{
			bool left = true;       //Used to store result of left side of clause
			bool right = true;      //Used to store result of right side of clause
			string[] clauseLiterals;   //Holds all the facts in the clause
			string[] leftLiterals;     //Holds the facts on the left side of the clause (if more than one)

			//Break clause into facts
			clauseLiterals = clause.Split(
				new[] { "=>" },
				StringSplitOptions.RemoveEmptyEntries
				);

			//Get corresponding boolean values for facts
			right = row[colHeadings.IndexOf(clauseLiterals[1])];
			//If left side contains more than one fact, combine all corresponding boolean values
			if (clauseLiterals[0].Contains('&'))
			{
				leftLiterals = clauseLiterals[0].Split(
					new[] { "&" },
					StringSplitOptions.RemoveEmptyEntries
					);

				for (int i = 0; i < leftLiterals.Length; i++)
				{
					left = left && row[colHeadings.IndexOf(leftLiterals[i])];
				}

			}
			//If the left side contains one fact, find the corresponding boolean value for the current row.
			else
			{
				left = row[colHeadings.IndexOf(clauseLiterals[0])];
			}

			//Perform the implication (changed using De Morgan's law)
			return !left | right;
		}

		/*
		* TODO: Finish This
		*/
		private bool testKB(List<bool> row)
		{
			//Check if all the facts are true on current line
			foreach (string fact in facts)
			{
				if (!row[colHeadings.IndexOf(fact)])
				{
					return false;
				}
			}

			//Check if all the clauses are true on the current line
			foreach (string clause in clauses)
			{
				if (! solveClause(clause, row))
				{
					return false;
				}
			}
			//If left facts entail right fact for all entries in the row, then this row is entailed by the knowlege base
			return true;
		}

		/*
		* TODO: Finish This
		*/
		private bool testAlpha(List<bool> row)
		{
			//bool alphaVal = row[colHeadings.IndexOf(question)];

			//TODO: Check if Alpha is a fact
			//TODO: Figure out how to test for alpha properly

			//Check if alpha is entailed in this row
			if (! row[colHeadings.IndexOf(question)])
			{
				return false;
			}
			/*foreach (string clause in alphaClauses)
			{
				//if (!(solveClause(clause, row) && alphaVal))
				if (!(solveClause(clause, row) && alphaVal))
				{
					return false;
				}
			}*/
			return true;
		}
		
		/*
		* TODO: Finish This
		*/
		public override string Infer()
		{
			//Check if question is in knowlege base
			bool rowIsTrue = true;
			List<bool> row;

			//Foreach node in KB.iterateOverAllModels() do
			for (int i = 0; i < rowCount; i++)
			{
				row = getRow(i);
				

				//If Test (n, KB) true
				if (testKB(row))
				{
					//If not Test(n, Q) then
					if (testAlpha(row))
					{
						count++;
					}
					else
					{
						return "NO";
					}
				}
			}
			return "YES " + count;
		}

		public TruthTable(string input) : base(input)
		{
			colHeadings = new List<string>(facts);
			getColHeaders();
			GetAlphaClauses();
			//columnCount = facts.Count() + 1;
			columnCount = colHeadings.Count();
			rowCount = Convert.ToInt32(Math.Pow(2, columnCount));

		}
	}
}
