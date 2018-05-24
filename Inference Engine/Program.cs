using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	class Program
	{
		static void Main(string[] args)
		{
			//Get Arguments from Command Line
			String[] arguments = Environment.GetCommandLineArgs();

			string method = arguments[1];		//Name of the method to be used
			string filename = arguments[2];		//Name of the file to be used
			
			//FOR TESTING PURPOSES
			//string method = "TT";
			//string filename = "test8.txt";

			InferenceEngine IE;	//InferenceEngine object to be used
			
			//Initiate the correct Inference Engine according to input paramaters
			switch (method)
			{
				//Truth Table checking
				case "TT":
					IE = new TruthTable(filename);
					break;
				//Forward Chaining
				case "FC":
					IE = new ForwardChaining(filename);
					break;
				//Backward Chaining
				case "BC":
					IE = new BackwardChaining(filename);
					break;
				default:
					IE = null;
					break;
			}

			//If Inference Engine Initiated, get results
			if (IE != null)
			{
				//If Inference Engine returns a result, print it out
				if (IE.getInference() == true)
                {
                    Console.WriteLine("YES: " + IE.getResult());
                }
				//If Inference engine doesn't return a result, print "NO"
                else
                {
                    Console.WriteLine("NO");
                }
			}
		}
	}
}
