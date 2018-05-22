using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	abstract class InferenceEngine
	{
		protected char question;
		int counter = 0;
		public abstract bool Infer();
        protected string[] ask;
        protected string[] premises;
        protected List<String> symbols;
		protected string tell;


		public void fileIn(string fileName)
		{
			System.IO.StreamReader file = new System.IO.StreamReader(fileName);
			string line;    //Holds current line from the file

			//TODO: Change this to work off the content of the prevous line, not the line number.
			// Read the file line by line.  
			while ((line = file.ReadLine()) != null)
			{
				//Get relevent data from lines 1 and 4 (starting from 0)
				switch (counter)
				{
					//TELL
					case 1:
						//Remove the trailing semicolon and convert to an array.
                        //TODO: remove whitespace between symbols
						line = line.TrimEnd(line[line.Length - 1]);
						ask = line.Split(';');
						break;

					//ASK
					case 3:
						tell = line;
						break;
				}
				counter++;
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
