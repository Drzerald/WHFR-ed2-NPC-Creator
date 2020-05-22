using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WHFR_ed2_NPC_Creator {
	class Race {
		public int id { get; set; }
		public string Name { get; set; }
		public Characteristics Characteristics { get; set; } = new Characteristics();


		public Race(int id) {
			this.id = id;

			string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Races", connection)) {
				connection.Open();
				System.Data.DataTable raceTable = new System.Data.DataTable();
				dataAdapter.Fill(raceTable);

				Name = raceTable.Rows[id]["Name"].ToString();
				int[] arrayOfManiCharateristics ={
					(int)raceTable.Rows[id]["WS"],
					(int)raceTable.Rows[id]["BS"],
					(int)raceTable.Rows[id]["S"],
					(int)raceTable.Rows[id]["T"],
					(int)raceTable.Rows[id]["Agi"],
					(int)raceTable.Rows[id]["Int"],
					(int)raceTable.Rows[id]["WP"],
					(int)raceTable.Rows[id]["Fel"],
				};
				this.Characteristics.setMainCharacteristics(arrayOfManiCharateristics);
				Characteristics.Attacks = int.Parse(raceTable.Rows[id]["A"].ToString());
				this.Characteristics.Mobility = (int)raceTable.Rows[id]["Mob"];
				this.Characteristics.Magick = (int)raceTable.Rows[id]["Mag"];

				connection.Close();

				
			}
			DieRoller die = new DieRoller();
			int rollforWouds = die.rollD10();
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM WoundsRoll", connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);
				Characteristics.Wounds = (int)table.Rows[rollforWouds][id + 1];
				System.Diagnostics.Debug.WriteLine(String.Format("Roll Value {0:d} Wouds val {1:d}", rollforWouds, Characteristics.Wounds));
			}

		}
	}
}
