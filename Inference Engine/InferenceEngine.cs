using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	abstract class InferenceEngine
	{
		protected List<String> facts = new List<String>();		//List of all facts in the knowlege base
		protected List<String> clauses = new List<String>();    //List of all clauses in the knowlege base
		protected string question;                              //Question being asked (alpha)

		public abstract bool Infer();			//Checks if KB entails Alpha	
		public abstract string getResult();     //Prints the answer of KB entailing alpha

		/*
		* Gets the knowlege base and question from file
		* @fileName: The name of the file to read from
		*/
		public void fileIn(string fileName)
		{
			string line;				//Holds current line from the file
			string previousLine = "";   //Holds the previous line in the file.

			//Streamer for reading from file
			System.IO.StreamReader file = new System.IO.StreamReader(fileName);

			// Read the file line by line.
			while ((line = file.ReadLine()) != null)
			{
				//Get List of clauses and facts
				if (previousLine == "TELL")
				{
					//Remove the spaces and trailing semicolon.
					line = line.Replace(" ", "");
					line = line.TrimEnd(line[line.Length - 1]);

					//Convert line into an array of clauses and facts
					string[] splitLine = line.Split(';');

					//Add all the clauses and facts to their respective lists
					for (int i = 0; i < splitLine.Length; i++)
					{
						//If the line contains "=>" add to clauses
						if (splitLine[i].Contains("=>") == false)
						{
							facts.Add(splitLine[i]);
						}

						//If the line doesn't contains "=>", add to facts
						else
						{
							clauses.Add(splitLine[i]);
						}
					}
				}

				//Get question from file
				else if (previousLine == "ASK")
				{
					question = line;
				}

				//Save the current line, so it can be used to check the next line
				previousLine = line;
			}

			//Close the File IO stream when finished
			file.Close();
		}
		public InferenceEngine(string filename)
		{
			//Get Knowlege base and question from file
			fileIn(filename);
		}
	}
}
