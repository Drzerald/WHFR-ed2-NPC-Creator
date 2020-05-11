using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator {
	class Skill : SkillProfile {

		//Variable Declaration
		protected int level;
		protected bool isAdvanced;
		protected int skillBonus;
		protected string description;

		// Get/Set 
		public int Level {          //Skill level 
			get { return level; }
			set {
				if (value < 0) {
					level = 0;
				} else if (value > 3) {
					level = 2;
				} else {
					level = value;
				}
				if (level == 0) {
					SkillBonus = -20;
				} else {
					SkillBonus = (level - 1) * 10;
				}
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
			Level = 0;
		}

		public Skill(int idNum, string name, bool isAdv, string dsc) {
			Id = idNum;
			Name = name;
			isAdvanced = isAdv;
			description = dsc;
			Level = 0;
		}

		//To string  "skillname level (bonus)"
		public override string ToString() {
			return Id.ToString() + ": " +
				name.ToString() + 
				" lvl" + Level.ToString() + 
				" (" + SkillBonus.ToString() + ")";
		}


		public void Advance() {
			if (Level >= 3) {
				Level = 3;
			} else {
				Level += 1;
			}
		}

		public void SetLevel(int level) {
			this.Level = level;
		}

	}
}
