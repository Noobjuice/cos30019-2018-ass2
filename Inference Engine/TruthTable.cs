using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	class TruthTable : InferenceEngine
	{
		private int counter = 0;

		public override string Infer()
		{
			//Foreach node in KB.iterateOverAllModels() do
			for (int i = 1; i < Math.Pow(2, facts.Count()); i++)
			{
				//Calculate truth table row

				//If Test (n, KB) true
				if (false)
				{
					//If not Test(n, Q) then
					if (false)
					{
						return "NO";
					}
				}
			}
			return "YES " + counter;



			//TODO: Finish This
			return "Truth Table";
		}

		public TruthTable(string input) : base(input)
		{
			//TODO: Finish This

		}
	}
}
