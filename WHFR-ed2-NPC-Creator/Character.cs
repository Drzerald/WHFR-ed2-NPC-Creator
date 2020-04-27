using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator {
	class Character {

		public string name = "";

		
		public Characteristics characteristicsFromRace = new Characteristics();
		public Characteristics characteristicsFromRolls = new Characteristics();
		public Characteristics characteristicsFromProffessions = new Characteristics();
		
		public Characteristics characteristics = new Characteristics(); //THINK

		Profession[] professionHistory = new Profession[3];

		public SkillList skills= new SkillList();
		public List<Talent> talents = new List<Talent>();
		//public TalentList talens = new TalentList(); //Idea, Is Talent List class is importanee

	}
}
