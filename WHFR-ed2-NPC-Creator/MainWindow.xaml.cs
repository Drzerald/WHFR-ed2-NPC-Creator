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

		Character character { get; set; } = new Character(0, 1, 0);
		DataBaseController dBControler = new DataBaseController();

		public MainWindow() {
			InitializeComponent();
			RaceLabel.DataContext = character.Race;
			groupBox.DataContext = character;
			listBox_Talents.DataContext = character;

			listBox_Skills.DataContext = character;
			listBox_Characters.DataContext = dBControler;
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			character.debugPrint();
		}

		private void Button_Chars(object sender, RoutedEventArgs e) {
			character.Characteristics.DebugPrint();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		//private void updateCharacteristics(object sender, RoutedEventArgs e) {
		//	character.updateCharateristics();
		//}

		private void ListBox_Talents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			//Character selection = (Character)listBox_Characters.SelectedItem;
			//textBlock_TalentDescription.DataContext = selection.Talents;
			textBlock_TalentDescription.DataContext = listBox_Talents.SelectedItem;
		}
		
		private void SaveCharacterButton_Click(object sender, RoutedEventArgs e) {
			int x = dBControler.SaveToDataBase((Character)listBox_Characters.SelectedItem);
			listBox_Characters.DataContext = dBControler;
			listBox_Characters.Items.Refresh();
		}

		private void ListBox_Characters_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			//character.ToString() ;
			textBox_CharacterName.DataContext = listBox_Characters.SelectedItem;
			listBox_Talents.DataContext = listBox_Characters.SelectedItem;
			listBox_Skills.DataContext = listBox_Characters.SelectedItem;
			groupBox.DataContext = listBox_Characters.SelectedItem;	
			//character = dBControler.ListOfCharacters[listBox_Characters.SelectedIndex];
		}

		private void Button1_Click(object sender, RoutedEventArgs e) {
			CreateNewCharacter createNewCharacterWinodw = new CreateNewCharacter();
			createNewCharacterWinodw.Show();
		}
	}
}
