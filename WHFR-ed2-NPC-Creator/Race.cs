using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WHFR_ed2_NPC_Creator {
	class Race {
		public int id;
		public string Name;
		public Characteristics characteristics = new Characteristics();
		

		public Race(int id) {
			this.id = id;
			string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Races", connection)) {
				connection.Open();
				System.Data.DataTable raceTable = new System.Data.DataTable();
				dataAdapter.Fill(raceTable);

				Name = raceTable.Rows[id]["Name"].ToString();
				int[] arrayOfManiCharateristics={
					(int)raceTable.Rows[id]["WS"],
					(int)raceTable.Rows[id]["BS"],
					(int)raceTable.Rows[id]["S"],
					(int)raceTable.Rows[id]["T"],
					(int)raceTable.Rows[id]["Agi"],
					(int)raceTable.Rows[id]["Int"],
					(int)raceTable.Rows[id]["WP"],
					(int)raceTable.Rows[id]["Fel"],
				};
				this.characteristics.setMainCharacteristics(arrayOfManiCharateristics);
				 
				connection.Close();
			}



		}

	}
}
