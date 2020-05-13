using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator {
	class Character {

		public string name = "";

		public Characteristics characteristicsFromRolls = new Characteristics();
		public Characteristics characteristicsFromProfessions = new Characteristics();
		public Race race;
		public List<Profession> professions = new List<Profession>();
		public Characteristics characteristics = new Characteristics(); //THINK

		Profession[] professionHistory = new Profession[3];

		public SkillList skills = new SkillList();
		public List<Talent> talents = new List<Talent>();


		public Character(int raceID, int professionId) {
			race = new Race(raceID);
			professions.Add(new Profession(professionId));
			rerollCharateristics();
		}

		public Character(int raceID, int professionId0, int professionId1) {
			race = new Race(raceID);
			professions.Add(new Profession(professionId0));
			professions.Add(new Profession(professionId1));
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
			refreshSkills();
			refreshCharateristics();
		}

		public void debugPrint() {
			System.Diagnostics.Debug.WriteLine("race: " + race.Name);
			race.characteristics.DebugPrint();
			//professions[0].debugPrint();
			characteristicsFromRolls.DebugPrint();
			characteristics.DebugPrint();
			System.Diagnostics.Debug.Write("\n///SKILLS:\n");
			foreach (Skill skill in skills.skillsArray) {
				System.Diagnostics.Debug.WriteLine(skill.ToString());
			}
		}


		private void refreshCharateristics() {
			int[] characteristicsArray = { 0, 0, 0, 0, 0, 0, 0, 0 };
			int[] characteristicsArrayProfessions = { 0, 0, 0, 0, 0, 0, 0, 0 };

			int[] characteristicsArrayRace = race.characteristics.getMainCharacteristics();
			int[] characteristicsArrayRolls = characteristicsFromRolls.getMainCharacteristics();
			foreach (Profession profession in professions) {
				int[] array = profession.characteristics.getMainCharacteristics();
				for (int i = 0; i < 8; i++) {
					if (characteristicsArrayProfessions[i] < array[i]) {
						characteristicsArrayProfessions[i] = array[i];
					}
				}
			}
			for (int i = 0; i < 8; i++) {
				characteristicsArray[i] += characteristicsArrayRace[i];
				characteristicsArray[i] += characteristicsArrayRolls[i];
				characteristicsArray[i] += characteristicsArrayProfessions[i];
			}
			characteristics.setMainCharacteristics(characteristicsArray);
		}

		private void refreshSkills() {
			skills.Zero();
			foreach (Profession profession in professions) {
				foreach (SkillProfile skillprof in profession.skills) {
					skills.advanceSkill(skillprof.Id);
				}
			}
		}
	}
}
