using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace WHFR_ed2_NPC_Creator {
	class SkillList {

		public Skill[] skillsArray { get; set; }
		public List<Skill> LearnedSkills { get; set; }

		public SkillList() {
			string connectionStr = ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;

			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Skills", connection)) {
				connection.Open();
				System.Data.DataTable skillTable = new System.Data.DataTable();
				dataAdapter.Fill(skillTable);

				int numberOfRecords = skillTable.Select().Length;
				skillsArray = new Skill[numberOfRecords];
				for (int i = 0; i < numberOfRecords; i++) {
					skillsArray[i] = new Skill((int)skillTable.Rows[i]["Id"]);
				}
				connection.Close();
			}
		}


		private void updateLearnedSkills() {
			LearnedSkills = new List<Skill>();
			foreach (Skill skill in skillsArray) {
				if (skill.Level > 0) {
					LearnedSkills.Add(skill);
				}
			}
		}

		public void advanceSkill(string skillName) {
			foreach (Skill skill in skillsArray) {
				if(skill.Name == skillName) {
					skill.Advance();
				}
			}
			updateLearnedSkills();
		}
		public void advanceSkill(int Id) {
			foreach (Skill skill in skillsArray) {
				if (skill.Id == Id) {
					skill.Advance();
				}
			}
			updateLearnedSkills();
		}

		public void Zero() {
			foreach(Skill skill in skillsArray) {
				skill.Zero();
			}
			updateLearnedSkills();
		}
	}
}
