using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferenceEngine
{
	class TruthTable : InferenceEngine
	{
		public override bool Infer()
		{
			//TODO: Finish This
			return false;
		}
        public override string getPath()
        {
            return "path";
        }

        public TruthTable(string input) : base(input)
		{
			//TODO: Finish This
		}
	}
}
