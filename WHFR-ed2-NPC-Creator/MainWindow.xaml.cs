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

		//Character character { get; set; } = new Character(0, 1, 0);
		DataBaseController dBControler = new DataBaseController();
		CreateNewCharacter createNewCharacterWinodw { get; set; }

		public MainWindow() {
			InitializeComponent();
			listBox_Characters.DataContext = dBControler;	
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void ListBox_Talents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			textBlock_TalentDescription.DataContext = listBox_Talents.SelectedItem;
		}
		
		private void SaveCharacterButton_Click(object sender, RoutedEventArgs e) {
			//int x = dBControler.SaveToDataBase((Character)listBox_Characters.SelectedItem);
			//dBControler.UpdateListOfCharacters();
			foreach (Character character in dBControler.ListOfCharacters) {
				dBControler.SaveChanges(character);
			}
			listBox_Characters.Items.Refresh();
			listBox_Characters.DataContext = dBControler;
		}

		private void ListBox_Characters_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			resetDataContext();
			if(listBox_Characters.SelectedItem == null) {
				SaveChanges_Button.IsEnabled = false;
				removeCharacter_Button.IsEnabled = false;
			} else {
				SaveChanges_Button.IsEnabled = true;
				removeCharacter_Button.IsEnabled = true;
			}
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
			raceGUIGroup_Grid.DataContext = listBox_Characters.SelectedItem;
			textBox_CharacterRace.DataContext = listBox_Characters.SelectedItem;
		}

		private void RemoveCharacter_Button_Click(object sender, RoutedEventArgs e) {
			if (listBox_Characters.SelectedItem != null) {
				MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(String.Format("Czy na pewno chcesz usunąć {0:}", listBox_Characters.SelectedItem.ToString()) , "Potwierdzenie Usunięcia", System.Windows.MessageBoxButton.YesNo);
				if (messageBoxResult == MessageBoxResult.Yes) {
					dBControler.RemoveCharacter((Character)listBox_Characters.SelectedItem);
					dBControler.UpdateListOfCharacters();
					listBox_Characters.Items.Refresh();
				}
			}
		}

		private void RevertChanges_Button_Click(object sender, RoutedEventArgs e) {
			dBControler.UpdateListOfCharacters();
			listBox_Characters.Items.Refresh();
		}
	}
}
