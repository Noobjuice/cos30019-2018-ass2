using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	abstract class InferenceEngine
	{
		protected List<String> facts = new List<String>();
		protected List<String> clauses = new List<String>();

		protected string question;
		public abstract bool Infer();
        private int lineCount;
		public abstract string getResult();

		public void fileIn(string fileName)
		{
			System.IO.StreamReader file = new System.IO.StreamReader(fileName);
			string line;    //Holds current line from the file

			//TODO: Change this to work off the content of the prevous line, not the line number.
			// Read the file line by line.
			while ((line = file.ReadLine()) != null)
			{
				//Get relevent data from lines 1 and 4 (starting from 0)
				switch (lineCount)
				{
					//TELL
					case 1:
						//Remove the trailing semicolon and convert to an array.

						line = line.TrimEnd(line[line.Length - 1]);
						line = line.Replace(" ", "");
						string[] splitLine = line.Split(';');

						//TODO: Finish this
						for (int i=0; i<splitLine.Length; i++){
							if (splitLine[i].Contains("=>") == false)
							{
								facts.Add(splitLine[i]);
							}

							else
							{
								clauses.Add(splitLine[i]);
							}
						}
						break;

					//ASK
					case 3:
						//tell = line;
						question = line;
						break;
				}
				lineCount++;
			}

			//Close the File when finished
			file.Close();
		}
		public InferenceEngine(string filename)
		{
			fileIn(filename);
		}
	}
}
