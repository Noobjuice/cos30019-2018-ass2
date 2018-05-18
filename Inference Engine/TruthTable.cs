using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	class TruthTable : InferenceEngine
	{
		private int count = 0;
		private int columnCount;

		//Calculate truth table row
		//TODO: Figure out why it's giving a list of 3-5 results instead of always 4.
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

			//Convert into an array
			return result;
			
			//TODO: Delete this
			//return new[] { rowNumStr };

		}

		public override string Infer()
		{
			//Foreach node in KB.iterateOverAllModels() do
			for (int i = 1; i <= Math.Pow(2, columnCount); i++)
			{
				List<bool> row = getRow(i);

				//If Test (n, KB) true
				if (true)
				{
					//If not Test(n, Q) then
					if (false)
					{
						return "NO";
					}
					else
					{
						count++;
					}
				}
			}
			return "YES " + count;

			//TODO: DELETE THIS
			//return "Truth Table";
		}

		public TruthTable(string input) : base(input)
		{
			columnCount = facts.Count() + 1;
		}
	}
}
