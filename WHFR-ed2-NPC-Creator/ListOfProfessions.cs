using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace WHFR_ed2_NPC_Creator {
	class ListOfProfessions {

		public List<Profession> AllProfessions { get; private set; } = new List<Profession>();
		public List<Race> AllRaces { get; private set; } = new List<Race>();
		


		public ListOfProfessions() {
			string connectionStr = ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Professions", connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);

				int numberOfRecords = table.Select().Length;

				for (int i = 0; i < numberOfRecords; i++) {
					AllProfessions.Add(new Profession((int)table.Rows[i]["Id"]));
				}
				connection.Close();
			}

			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Races", connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);

				int numberOfRecords = table.Select().Length;

				for (int i = 0; i < numberOfRecords; i++) {
					AllRaces.Add(new Race((int)table.Rows[i]["Id"]));
				}
				connection.Close();
			}
		}

		public void NewWouds() {
			foreach(Race race in AllRaces) {
				race.RollNewWoundsValue();
			}
		}

	}
}
