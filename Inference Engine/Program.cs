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


			//TODO: Revert this back (makes testing easier)
			//string method = arguments[1];
			//string filename = arguments[2];
			string method = "FC";
			string filename = "test1(2).txt";

			InferenceEngine IE;

			//TODO: Get Problem from file and pass to inference engine.

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
                if (IE.Infer() == "true")
                {
                    Console.WriteLine("YES:" + IE.getPath());
                }
                else
                {
                    Console.WriteLine("NO");
                }
				Console.ReadLine();
			}
		}
	}
}
