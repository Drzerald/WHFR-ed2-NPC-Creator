using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator {
	class Skill : SkillProfile {

		//Variable Declaration
		protected int level;
		protected int skillBonus;

		// Get/Set 
		public int Level {          //Skill level 
			get { return level; }
			set {
				if (value < 0) { level = 0; } else if (value > 3) { level = 3; } else { level = value; }
				SkillBonus = (level - 1) * 10;
			}
		}

		public int SkillBonus {             //if Skill is unlearned then Bonus is -10 (IT'S NOT THE SAME AS IN GAME)
			set { skillBonus = value; }
			get { return skillBonus; }
		}
		
		//Constructor
		public Skill(SkillProfile skillProfile) {
			Id = skillProfile.Id;
			level = 0;
			name = skillProfile.Name;
			description = skillProfile.Description;
		}
		public Skill(int idNum) {
			Id = idNum;
			level = 0;

		}


		//		public Skill(string skillname) {  //DEBUG DELETE LATER
		//			level = 1;
		//			name = skillname;
		//		}

		//To string  "skillname level (bonus)"
		public override string ToString() {
			return name.ToString() + 
				" lvl" + Level.ToString() + 
				" (" + SkillBonus.ToString() + ")";
		}


		public void Advance() {
			if (level >= 3) {
				level = 3;
			} else {
				level += 1;
			}
		}

		public void SetLevel(int level) {
			this.level = level;
		}

	}
}
