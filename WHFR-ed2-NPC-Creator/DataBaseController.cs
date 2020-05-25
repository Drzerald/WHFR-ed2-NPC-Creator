using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WHFR_ed2_NPC_Creator
{
    class DataBaseController : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		public List<Character> ListOfCharacters { get; set; } = new List<Character>();
		//public System.Collections.ObjectModel.ObservableCollection<Character> ListOfCharacters { get; set; } = new System.Collections.ObjectModel.ObservableCollection<Character>();


		public DataBaseController() {
			updateListOfCharacters();
		}


		private void updateListOfCharacters() {
			ListOfCharacters.Clear();
			string connectionStr = ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Id FROM Character", connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);

				for (int i = 0; i < table.Rows.Count; i++) {
					int x = (int)table.Rows[i]["Id"];
					ListOfCharacters.Add(new Character(x));
				}
			}
			OnPropertyChanged("ListOfCharacters");
			//DebugPrint();
		}


		public int SaveToDataBase(Character character) {

			string connectionStr = ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;

			int characterId;

			string insertQuerry = "INSERT INTO dbo.Character (Name, RaceId, WS, BS, S, T, Agi, Int, WP, Fel, W) output INSERTED.ID VALUES (@Name, @RaceId, @WS, @BS, @S, @T, @Agi, @Int, @WP, @Fel, @W)";
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlCommand command = new SqlCommand(insertQuerry, connection)) {
				//command.CommandType = 
				int doneQuerrys = 0;

				command.Parameters.AddWithValue("@Name", character.Name);
				command.Parameters.AddWithValue("@RaceId", character.Race.Id);
				command.Parameters.AddWithValue("@WS", character.CharacteristicsFromRolls.WeaponSkills);
				command.Parameters.AddWithValue("@BS", character.CharacteristicsFromRolls.BalisticSkills);
				command.Parameters.AddWithValue("@S", character.CharacteristicsFromRolls.Strength);
				command.Parameters.AddWithValue("@T", character.CharacteristicsFromRolls.Toughness);
				command.Parameters.AddWithValue("@Agi", character.CharacteristicsFromRolls.Agility);
				command.Parameters.AddWithValue("@Int", character.CharacteristicsFromRolls.Intelligence);
				command.Parameters.AddWithValue("@WP", character.CharacteristicsFromRolls.WillPower);
				command.Parameters.AddWithValue("@Fel", character.CharacteristicsFromRolls.Fellowship);
				command.Parameters.AddWithValue("@W", character.Race.Characteristics.Wounds);

				connection.Open();
				characterId = (int)command.ExecuteScalar();
				System.Diagnostics.Debug.WriteLine(characterId);

				command.CommandText = "INSERT INTO dbo.CharacterProfessions (CharacterId, ProfessionId) Values (@CharacterId, @ProfessionId)";
				command.Parameters.AddWithValue("@CharacterId", 0);
				command.Parameters.AddWithValue("@ProfessionId", 0);
				List<int> professionIds = new List<int>();
				foreach (Profession profession in character.Professions) {
					//professionIds.Add(profession.Id);
					command.Parameters["@CharacterId"].Value = characterId;
					command.Parameters["@ProfessionId"].Value = profession.Id;
					command.ExecuteNonQuery();
				}
				//command.Parameters.AddRange();
				if (doneQuerrys == character.Professions.Count) { System.Diagnostics.Debug.WriteLine("DONE"); }
				connection.Close();
			}

			int numberOfRecords;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Character", connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);
				numberOfRecords = table.Select().Length;
				System.Diagnostics.Debug.WriteLine("table length: " + numberOfRecords.ToString());
				connection.Close();
			}
			updateListOfCharacters();
			return numberOfRecords;
		}



		//DEBUG
		public void DebugPrint() {
			foreach (Character character in ListOfCharacters) {
				//System.Diagnostics.Debug.WriteLine("Character Name: " + character.ToString());
				character.debugPrint();
			}
		}
		//INTERFACE
		protected void OnPropertyChanged([CallerMemberName] string name = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

	}
}
