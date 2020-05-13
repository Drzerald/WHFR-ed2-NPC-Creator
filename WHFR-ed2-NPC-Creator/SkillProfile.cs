using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WHFR_ed2_NPC_Creator {
	class SkillProfile {


		public int Id { get; }
		public string Name { get; }
		public bool IsAdvanced { get; }
		public string SkillCharacteristic { get; }


		protected string description;
		public string Description {
			get { return description; }
			set { description = value; }
		}

		public SkillProfile(int id) {
			Id = id;
			string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Skills", connection)) {
				connection.Open();
				System.Data.DataTable skillsTable = new System.Data.DataTable();
				dataAdapter.Fill(skillsTable);

				Name = skillsTable.Rows[id]["name"].ToString();
				IsAdvanced = (bool)skillsTable.Rows[id]["isAdvanced"];
				SkillCharacteristic = skillsTable.Rows[id]["SkillCharacteristic"].ToString();
			
				connection.Close();
			}
		}

		public void debugPrint() {

		}

		public override string ToString() {
			return Name.ToString();
		}
	}
}
