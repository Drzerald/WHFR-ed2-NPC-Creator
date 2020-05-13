using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace WHFR_ed2_NPC_Creator {
	class Profession {
		public int Id { get; }
		public string Name { get; }
		private bool isAdvanced;

		public Characteristics characteristics = new Characteristics();
		public List<SkillProfile> skills = new List<SkillProfile>();
		private List<Item> inventory = new List<Item>();
		public List<Talent> talents = new List<Talent>();

		public Profession(int id) {
			this.Id = id;
			string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM professions", connection)) {
				connection.Open();
				System.Data.DataTable profesionTable = new System.Data.DataTable();
				dataAdapter.Fill(profesionTable);
				
				Name = profesionTable.Rows[id]["FullName"].ToString();
				int[] arrayOfMainCharateristics ={
					(int)profesionTable.Rows[id]["WS"],
					(int)profesionTable.Rows[id]["BS"],
					(int)profesionTable.Rows[id]["S"],
					(int)profesionTable.Rows[id]["T"],
					(int)profesionTable.Rows[id]["Agi"],
					(int)profesionTable.Rows[id]["Int"],
					(int)profesionTable.Rows[id]["WP"],
					(int)profesionTable.Rows[id]["Fel"],
				};
				characteristics.setMainCharacteristics(arrayOfMainCharateristics);
				connection.Close();
			}

			string SQLQuerryTalents = "SELECT Talents.Id FROM Talents INNER JOIN ProfessionTalents ON Talents.Id = ProfessionTalents.TalentId Where ProfessionTalents.ProfessionId = " + Id.ToString();
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLQuerryTalents, connection)) {
				connection.Open();
				System.Data.DataTable profesionTable = new System.Data.DataTable();
				dataAdapter.Fill(profesionTable);
				int numberOfRecords = profesionTable.Select().Length;
				for (int i = 0; i< numberOfRecords; i++) {
					talents.Add(new Talent((int)profesionTable.Rows[i]["Id"]));
				}
				connection.Close();
			}

			string SQLQuerrySkills = "SELECT Skills.Id FROM Skills INNER JOIN ProfessionSkills ON Skills.Id = ProfessionSkills.SkillId Where ProfessionSkills.ProfessionId = " + Id.ToString();
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter(SQLQuerrySkills, connection)) {
				connection.Open();
				System.Data.DataTable profesionTable = new System.Data.DataTable();
				dataAdapter.Fill(profesionTable);
				int numberOfRecords = profesionTable.Select().Length;
				for (int i = 0; i < numberOfRecords; i++) {
					skills.Add(new SkillProfile((int)profesionTable.Rows[i]["Id"]));
				}
				connection.Close();
			}
		}


		public void debugPrint() {
			System.Diagnostics.Debug.WriteLine("PROFESSION: " + Name);
			System.Diagnostics.Debug.WriteLine("	talents: " );
			foreach(Talent talent in talents) {
				System.Diagnostics.Debug.WriteLine("		" + talent.Name);
			}
		}
		
	}
}
