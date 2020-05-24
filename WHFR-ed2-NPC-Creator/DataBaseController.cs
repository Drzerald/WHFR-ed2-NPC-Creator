using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;

namespace WHFR_ed2_NPC_Creator
{
    class DataBaseController
    {
		public int SaveToDataBase(Character character) {
			string connectionStr = ConfigurationManager.ConnectionStrings["WHFR_ed2_NPC_Creator.Properties.Settings.DBConnection"].ConnectionString;

			string insertQuerry = "INSERT INTO dbo.Character (Name, WS, BS, S, T) VALUES (@Name, @WS, @BS, @S, @T)";
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlCommand command = new SqlCommand(insertQuerry, connection)) {
				//command.CommandType = 

				command.Parameters.AddWithValue("@Name", character.Name);
				command.Parameters.AddWithValue("@WS", character.Characteristics.WeaponSkills);
				command.Parameters.AddWithValue("@BS", character.Characteristics.BalisticSkills);
				command.Parameters.AddWithValue("@S", character.Characteristics.Strength);
				command.Parameters.AddWithValue("@T", character.Characteristics.Toughness);
				
				connection.Open();
				int x = command.ExecuteNonQuery();
				System.Diagnostics.Debug.WriteLine(x);

				connection.Close();
			}

			int numberOfRecords;
			using (SqlConnection connection = new SqlConnection(connectionStr))
			using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Character", connection)) {
				connection.Open();
				System.Data.DataTable table = new System.Data.DataTable();
				dataAdapter.Fill(table);
				numberOfRecords = table.Select().Length;
				System.Diagnostics.Debug.WriteLine("table length: " + numberOfRecords.ToString());
				connection.Close();

			}
			return numberOfRecords;
		}
    }
}
