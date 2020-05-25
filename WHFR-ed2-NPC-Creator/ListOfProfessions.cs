using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace WHFR_ed2_NPC_Creator {
	class ListOfProfessions {

		public List<Profession> Professions { get; set; } = new List<Profession>();

		public ListOfProfessions() {
			string connectionStr = ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Professions", connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);

				int numberOfRecords = table.Select().Length;

				for (int i = 0; i < numberOfRecords; i++) {
					Professions.Add(new Profession((int)table.Rows[i]["Id"]));
				}
				connection.Close();
			}
		}


	}
}
