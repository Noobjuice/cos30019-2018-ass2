using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{

	class BackwardChaining : InferenceEngine
	{


		private string outputResult()
		{
			string result;

			//TODO: If successfully infered, return result (FORMAT: YES: a, b, p2, p3, p1, d)
				result = "YES: ";
				//Loop through list of entailed values in reverse 
			

			return result;
		}

		public override string Infer()
		{
			//TODO: Finish This
			return outputResult();
		}

		public BackwardChaining(string input) : base(input)
		{
			//TODO: Finish This
		}
	}
}
