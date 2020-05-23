using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WHFR_ed2_NPC_Creator {
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		Character character { get; set; } = new Character(2, 1, 0);

		public MainWindow() {
			InitializeComponent();
			RaceLabel.DataContext = character.Race;
			groupBox.DataContext = character;
			listBox_Talents.DataContext = character;
			
			listBox_Skills.DataContext = character.skills;

			ww1.DataContext = character.CharacteristicsFromRolls;
			WW4.DataContext = character.Characteristics;

		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			character.debugPrint();
			

		}


		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		//private void updateCharacteristics(object sender, RoutedEventArgs e) {
		//	character.updateCharateristics();
		//}

		private void ListBox_Talents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			textBlock_TalentDescription.DataContext = character.Talents[listBox_Talents.SelectedIndex];
		}
	}
}
