using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace WHFR_ed2_NPC_Creator {
	class Talent {
		public int Id { get; }
		public string Name { get; private set; }
		public string Description { get; }

		public Talent(int id) {
			Id = id;

			string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Talents", connection)) {
				connection.Open();
				System.Data.DataTable talentsTable = new System.Data.DataTable();
				dataAdapter.Fill(talentsTable);

				Name = talentsTable.Rows[id]["FullName"].ToString();
				Description = talentsTable.Rows[id]["ShortDescription"].ToString();

				connection.Close();
			} 
		}

		public override string ToString() {
			return Name;
		}
	}
}

