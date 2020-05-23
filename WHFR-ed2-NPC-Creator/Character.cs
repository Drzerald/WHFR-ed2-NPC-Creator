using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator {
	class Character {

		public event Action OnCharacteristicRecalculate = delegate { };
		public string Name = "";
		public Characteristics CharacteristicsFromRolls { get; set; } = new Characteristics();
		public Characteristics CharacteristicsFromProfessions { get; set; } = new Characteristics();
		public Race Race { get; set; }
		public List<Profession> Professions { get; set; } = new List<Profession>();
		public Characteristics Characteristics { get; set; } = new Characteristics(); //THINK
		public SkillList skills { get; set; } = new SkillList();
		public List<Talent> Talents { get; set; } = new List<Talent>();

		
		public Character(int raceID, int professionId) {
			Race = new Race(raceID);
			Professions.Add(new Profession(professionId));
			rerollCharateristics();
			Race.Characteristics.OnCharacteristicChange += recalculate;
		}

		public Character(int raceID, int professionId0, int professionId1) {
			Race = new Race(raceID);
			Professions.Add(new Profession(professionId0));
			Professions.Add(new Profession(professionId1));
			rerollCharateristics();
			Race.Characteristics.OnCharacteristicChange += recalculate;
		}

		public void rerollCharateristics() {
			DieRoller die = new DieRoller();
			CharacteristicsFromRolls.WeaponSkills = die.rollD10(2);
			CharacteristicsFromRolls.BalisticSkills = die.rollD10(2);
			CharacteristicsFromRolls.Strength = die.rollD10(2);
			CharacteristicsFromRolls.Toughness = die.rollD10(2);
			CharacteristicsFromRolls.Agility = die.rollD10(2);
			CharacteristicsFromRolls.Intelligence = die.rollD10(2);
			CharacteristicsFromRolls.WillPower = die.rollD10(2);
			CharacteristicsFromRolls.Fellowship = die.rollD10(2);
			refreshSkills();
			refreshTalents();
			updateCharateristics();
		}

		public void debugPrint() {
			System.Diagnostics.Debug.WriteLine("Race: " + Race.Name);
			System.Diagnostics.Debug.WriteLine("Charateristics from: Race");
			Race.Characteristics.DebugPrint();
			System.Diagnostics.Debug.WriteLine("Charateristics from: Rolls");
			CharacteristicsFromRolls.DebugPrint();
			System.Diagnostics.Debug.WriteLine("Charateristics Output:");
			Characteristics.DebugPrint();

			System.Diagnostics.Debug.WriteLine("Charateristics from Professions:");
			foreach (int num in getCharacteristicsFromProfessions()) {
				System.Diagnostics.Debug.Write(num.ToString("D2") + " ");
			}

			System.Diagnostics.Debug.Write("\n\n///SKILLS:\n");
			foreach (Skill skill in skills.skillsArray) {
				System.Diagnostics.Debug.WriteLine(skill.ToString());
			}
			System.Diagnostics.Debug.Write("\n///TALENTS:\n");
			foreach (Talent talent in Talents) {
				System.Diagnostics.Debug.WriteLine(talent.ToString());
			}
		}
		
		public void updateCharateristics() {
			updateProfessionCharateristics();
			int[] characteristicsArray = { 0, 0, 0, 0, 0, 0, 0, 0 };
			//Rolls + Prof + Race (main)
			for (int i = 0; i < 8; i++) {
				characteristicsArray[i] += Race.Characteristics.getMainCharacteristics()[i];
				characteristicsArray[i] += CharacteristicsFromRolls.getMainCharacteristics()[i];
				characteristicsArray[i] += CharacteristicsFromProfessions.getMainCharacteristics()[i];
			}
			// Prof + Race (side)
			Characteristics.Attacks = CharacteristicsFromProfessions.Attacks + Race.Characteristics.Attacks;
			Characteristics.Wounds = CharacteristicsFromProfessions.Wounds + Race.Characteristics.Wounds;
			Characteristics.Mobility = CharacteristicsFromProfessions.Mobility + Race.Characteristics.Mobility;
			Characteristics.Magick = CharacteristicsFromProfessions.Magick + Race.Characteristics.Magick;
			//Seting
			Characteristics.setMainCharacteristics(characteristicsArray);
			OnCharacteristicRecalculate();
		}


		private void updateProfessionCharateristics() {
			int[] maxAdvancement = { 0, 0, 0, 0, 0, 0, 0, 0 };
			foreach (Profession profession in Professions) {
				int[] array = profession.Characteristics.getMainCharacteristics();
				for(int i = 0; i<8; i++) {
					if (maxAdvancement[i] < array[i]) {
						maxAdvancement[i] = array[i];
					}
				}
				if (CharacteristicsFromProfessions.Attacks < profession.Characteristics.Attacks) {
					CharacteristicsFromProfessions.Attacks = profession.Characteristics.Attacks;
				}
				if (CharacteristicsFromProfessions.Wounds < profession.Characteristics.Wounds) {
					CharacteristicsFromProfessions.Wounds = profession.Characteristics.Wounds;
				}
				if (CharacteristicsFromProfessions.Mobility < profession.Characteristics.Mobility) {
					CharacteristicsFromProfessions.Mobility = profession.Characteristics.Mobility;
				}
				if (CharacteristicsFromProfessions.Magick < profession.Characteristics.Magick) {
					CharacteristicsFromProfessions.Magick = profession.Characteristics.Magick;
				}
			}
			CharacteristicsFromProfessions.setMainCharacteristics(maxAdvancement);
		}

		private void recalculate() {
			updateCharateristics();
		}

		private void refreshSkills() {
			skills.Zero();
			foreach (Profession profession in Professions) {
				foreach (SkillProfile skillprof in profession.skills) {
					skills.advanceSkill(skillprof.Id);
				}
			}
		}

		private int[] getCharacteristicsFromProfessions() {
			int[] characteristicsArrayProfessions = {0,0,0,0,0,0,0,0};
			foreach (Profession profession in Professions) {
				int[] array = profession.Characteristics.getMainCharacteristics();
				for (int i = 0; i< 8; i++) {
					if (characteristicsArrayProfessions[i] < array[i]) {
						characteristicsArrayProfessions[i] = array[i];
					}
				}
			}
			return characteristicsArrayProfessions;
		}

		private void refreshTalents() {
			List<int> list = new List<int>();
			foreach (Profession profession in Professions) {
				foreach(Talent talent in profession.talents) {
					if(!list.Contains(talent.Id)) {
						list.Add(talent.Id);
						Talents.Add(talent);
					}
				}
			}
		}


	}
}
