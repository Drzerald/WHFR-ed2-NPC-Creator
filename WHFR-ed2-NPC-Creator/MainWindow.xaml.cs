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
		CreateNewCharacter createNewCharacterWinodw { get; set; }


		public MainWindow() {
			InitializeComponent();
			//resetDataContext();
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

		private void ListBox_Talents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			textBlock_TalentDescription.DataContext = listBox_Talents.SelectedItem;
		}
		
		private void SaveCharacterButton_Click(object sender, RoutedEventArgs e) {
			int x = dBControler.SaveToDataBase((Character)listBox_Characters.SelectedItem);
			listBox_Characters.DataContext = dBControler;
			listBox_Characters.Items.Refresh();
		}

		private void ListBox_Characters_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			resetDataContext();
		}

		private void Button1_Click(object sender, RoutedEventArgs e) {
			createNewCharacterWinodw = new CreateNewCharacter();
			createNewCharacterWinodw.Closed += refreshLists;
			if (createNewCharacterWinodw == null) {
				createNewCharacterWinodw = new CreateNewCharacter();
			}
			createNewCharacterWinodw.Owner = this;
			createNewCharacterWinodw.ShowDialog();
		}

		public void refreshLists(object sender, System.EventArgs e) {
			createNewCharacterWinodw = null;
			dBControler.UpdateListOfCharacters();
			listBox_Characters.Items.Refresh();
		}


		private void resetDataContext() {

			textBox_CharacterName.DataContext = listBox_Characters.SelectedItem;
			listBox_Talents.DataContext = listBox_Characters.SelectedItem;
			listBox_Skills.DataContext = listBox_Characters.SelectedItem;
			groupBox.DataContext = listBox_Characters.SelectedItem;
			textBox_CharacterRace.DataContext = listBox_Characters.SelectedItem;

		}


	}
}
