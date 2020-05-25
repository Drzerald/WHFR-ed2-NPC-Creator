using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WHFR_ed2_NPC_Creator {
	class Race {
		public int Id { get; set; }
		public string Name { get; set; }
		public Characteristics Characteristics { get; set; } = new Characteristics();


		public Race(int id) {
			this.Id = id;

			string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Races Where Id = " + id.ToString(), connection)) {
				connection.Open();
				System.Data.DataTable raceTable = new System.Data.DataTable();
				dataAdapter.Fill(raceTable);

				Name = raceTable.Rows[0]["Name"].ToString();
				int[] arrayOfManiCharateristics ={
					(int)raceTable.Rows[0]["WS"],
					(int)raceTable.Rows[0]["BS"],
					(int)raceTable.Rows[0]["S"],
					(int)raceTable.Rows[0]["T"],
					(int)raceTable.Rows[0]["Agi"],
					(int)raceTable.Rows[0]["Int"],
					(int)raceTable.Rows[0]["WP"],
					(int)raceTable.Rows[0]["Fel"],
				};
				this.Characteristics.setMainCharacteristics(arrayOfManiCharateristics);
				this.Characteristics.Attacks = int.Parse(raceTable.Rows[0]["A"].ToString());
				this.Characteristics.Mobility = (int)raceTable.Rows[0]["Mob"];
				this.Characteristics.Magick = (int)raceTable.Rows[0]["Mag"];
				connection.Close();
			}
			RollNewWoundsValue();
		}

		public void RollNewWoundsValue() {
			string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			DieRoller die = new DieRoller();
			int rollforWouds = die.rollD10();
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM WoundsRoll Where id = " + rollforWouds.ToString(), connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);
				Characteristics.Wounds = (int)table.Rows[0][Id + 1];
				System.Diagnostics.Debug.WriteLine(String.Format("Roll Value {0:d} Wouds val {1:d}", rollforWouds, Characteristics.Wounds));
			}
		}

		public override string ToString() {
			return Name;
		}

	}
}
