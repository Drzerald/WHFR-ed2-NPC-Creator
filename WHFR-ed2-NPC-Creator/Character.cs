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

		public SkillList skills = new SkillList();
		public List<Talent> talents = new List<Talent>();

		public Character() {
			rerollCharateristics();
		}

		public void rerollCharateristics() {
			DieRoller die = new DieRoller();
			characteristicsFromRolls.WeaponSkills = die.rollD10(2);
			characteristicsFromRolls.BaliscticSkills = die.rollD10(2);
			characteristicsFromRolls.Strength = die.rollD10(2);
			characteristicsFromRolls.Toughness = die.rollD10(2);
			characteristicsFromRolls.Agility = die.rollD10(2);
			characteristicsFromRolls.Intelligence = die.rollD10(2);
			characteristicsFromRolls.WillPower = die.rollD10(2);
			characteristicsFromRolls.Fellowship = die.rollD10(2);
		}

		public void debugPrint() {
			System.Diagnostics.Debug.Print(
				characteristicsFromRolls.WeaponSkills.ToString() + " " +
				characteristicsFromRolls.BaliscticSkills.ToString() + " " +
				characteristicsFromRolls.Strength.ToString() + " " +
				characteristicsFromRolls.Toughness.ToString() + " " +
				characteristicsFromRolls.Agility.ToString() + " " +
				characteristicsFromRolls.Intelligence.ToString() + " " +
				characteristicsFromRolls.WillPower.ToString() + " " +
				characteristicsFromRolls.Fellowship.ToString()+"\n"
			);
			foreach(Skill skill in skills.skillsArray) {
				System.Diagnostics.Debug.Print(skill.ToString());
			}
		}
	}
}
