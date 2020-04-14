using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator {
	class SkillList {

		private List<Skill> skills = new List<Skill>();

		public List<Skill> Skills{
			get{ return skills; }
		}

		void advanceSkill(string skillName) {
			foreach (Skill skill in skills) {
				if(skill.ToString() == skillName) {
					skill.Advance();
				}
			}
		}



	}
}
