using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WHFR_ed2_NPC_Creator {
	class Character  : INotifyPropertyChanged{

		public event PropertyChangedEventHandler PropertyChanged;

	
		private string name = "Character name (NOT LISKARM)";
		public int Id { get; set; } = -1;
		public event Action OnCharacteristicRecalculate = delegate { };
		public string Name {
			get { return name; }
			set {
				name = value.Trim();
				OnPropertyChanged();
			}
		}
		public Characteristics CharacteristicsFromRolls { get; set; } = new Characteristics();
		public Characteristics CharacteristicsFromProfessions { get; set; } = new Characteristics();
		public Race Race { get; set; }
		public List<Profession> Professions { get; set; } = new List<Profession>();
		public Characteristics Characteristics { get; set; } = new Characteristics(); //THINK
		public SkillList skills { get; set; } = new SkillList();
		public List<Talent> Talents { get; set; } = new List<Talent>();

		public Character(Race race, List<Profession> professions) {
			Race = race;
			foreach (Profession profession in professions) {
				Professions.Add(profession);
			}
			rerollCharateristics();
			Race.Characteristics.OnCharacteristicChange += recalculate;
			CharacteristicsFromRolls.OnCharacteristicChange += recalculate;
			recalculate();
		}
		
		public Character(int raceID, int professionId) {
			Race = new Race(raceID);
			Professions.Add(new Profession(professionId));
			rerollCharateristics();
			Race.Characteristics.OnCharacteristicChange += recalculate;
			CharacteristicsFromRolls.OnCharacteristicChange += recalculate;
		}

		public Character(int raceID, int professionId0, int professionId1) {
			Race = new Race(raceID);
			Professions.Add(new Profession(professionId0));
			Professions.Add(new Profession(professionId1));
			rerollCharateristics();
			Race.Characteristics.OnCharacteristicChange += recalculate;
			CharacteristicsFromRolls.OnCharacteristicChange += recalculate;
		}

		public Character(int id) {
			string connectionStr = ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Character Where Character.Id = " + id, connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);
				Id = id;
				Name = table.Rows[0]["Name"].ToString();
				
				this.Race = new Race((int)table.Rows[0]["RaceId"]);
				Race.Characteristics.Wounds = (int)table.Rows[0]["W"];
				CharacteristicsFromRolls.WeaponSkills  = (int)table.Rows[0]["WS"];
				CharacteristicsFromRolls.BalisticSkills = (int)table.Rows[0]["BS"];
				CharacteristicsFromRolls.Strength  = (int)table.Rows[0]["S"];
				CharacteristicsFromRolls.Toughness = (int)table.Rows[0]["T"];
				CharacteristicsFromRolls.Agility   = (int)table.Rows[0]["Agi"];
				CharacteristicsFromRolls.Intelligence = (int)table.Rows[0]["Int"];
				CharacteristicsFromRolls.WillPower  = (int)table.Rows[0]["WP"];
				CharacteristicsFromRolls.Fellowship = (int)table.Rows[0]["Fel"];
				connection.Close();
			}

			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Professions.Id FROM Professions INNER JOIN CharacterProfessions ON Professions.Id = CharacterProfessions.ProfessionId Where CharacterProfessions.CharacterId = " + Id.ToString(), connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);

				for (int i = 0; i < table.Rows.Count; i++) {
					Professions.Add(new Profession((int)table.Rows[i]["Id"]));
				}
			}
			Race.Characteristics.OnCharacteristicChange += recalculate;
			CharacteristicsFromRolls.OnCharacteristicChange += recalculate;
			recalculate();
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

		public string randomizeName() {
			string name;
			string connectionStr = ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Names", connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);

				DieRoller dieRoller = new DieRoller();
				
				int rollValue = dieRoller.rollDie(1, table.Select().Length -1);
				name = table.Rows[rollValue][Race.Id + 1].ToString();
				Name = name;
			}
			return name;
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
			refreshSkills();
			refreshTalents();
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


		public override string ToString() {
			if (Name != null) {
				return Name;
			} else {
				return Id.ToString();
			}
 		}

		protected void OnPropertyChanged([CallerMemberName] string name = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
