using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WHFR_ed2_NPC_Creator {
	/// <summary>
	/// Logika interakcji dla klasy CreateNewCharacter.xaml
	/// </summary>
	public partial class CreateNewCharacter : Window {

		
		ListOfProfessions professions = new ListOfProfessions();
		Character character = new Character(0,1);

		public CreateNewCharacter() {
			InitializeComponent();
			comboBox_Profession0.DataContext = professions;
			comboBox_Profession1.DataContext = professions;
			comboBox_Profession2.DataContext = professions;
		}


		private void ListBox_Talents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			
		}

		private void ComboBox_Profession0_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			
		}
	}
}
