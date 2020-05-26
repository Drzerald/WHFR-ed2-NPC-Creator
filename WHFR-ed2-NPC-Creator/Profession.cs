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
		public Characteristics Characteristics { get; set; } = new Characteristics();
		public List<SkillProfile> skills = new List<SkillProfile>();
		public List<Talent> talents = new List<Talent>();

		//Constructor
		public Profession(int id) {
			this.Id = id;
			string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM professions Where id = " + id.ToString(), connection)) {
				connection.Open();
				System.Data.DataTable profesionTable = new System.Data.DataTable();
				dataAdapter.Fill(profesionTable);
				
				Name = profesionTable.Rows[0]["FullName"].ToString();
				int[] arrayOfMainCharateristics ={
					(int)profesionTable.Rows[0]["WS"],
					(int)profesionTable.Rows[0]["BS"],
					(int)profesionTable.Rows[0]["S"],
					(int)profesionTable.Rows[0]["T"],
					(int)profesionTable.Rows[0]["Agi"],
					(int)profesionTable.Rows[0]["Int"],
					(int)profesionTable.Rows[0]["WP"],
					(int)profesionTable.Rows[0]["Fel"],
				};
				Characteristics.setMainCharacteristics(arrayOfMainCharateristics);
				Characteristics.Attacks = (int)profesionTable.Rows[0]["A"];
				Characteristics.Wounds = (int)profesionTable.Rows[0]["W"];
				Characteristics.Mobility = (int)profesionTable.Rows[0]["Mob"];
				Characteristics.Magick = (int)profesionTable.Rows[0]["mob"];

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

		public override string ToString() {
			return Name;
		}

		//DEBUG:
		public void debugPrint() {
			System.Diagnostics.Debug.WriteLine("PROFESSION: " + Name);
			System.Diagnostics.Debug.WriteLine("	talents: " );
			foreach(Talent talent in talents) {
				System.Diagnostics.Debug.WriteLine("		" + talent.Name);
			}
		}
		
	}
}
