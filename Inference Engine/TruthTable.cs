using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	class TruthTable : InferenceEngine
	{
		//getModels


		public override string Infer()
		{
			//Foreach node in KB.iterateOverAllModels() do
				//If Test (n, KB) true
						//If not Test(n, Q) then
							//Return false



			//TODO: Finish This
			return "Truth Table";
		}

		public TruthTable(string input) : base(input)
		{
			//TODO: Finish This

		}
	}
}
