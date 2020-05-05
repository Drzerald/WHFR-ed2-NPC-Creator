using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator {
	class SkillProfile {


		public int Id;
		protected string name;
		public string Name {
			get { return name; }
			set { name = value; }
		}

		protected string description;
		public string Description {
			get { return description; }
			set { description = value; }
		}

		public override string ToString() {
			return name.ToString();
		}
	}
}
