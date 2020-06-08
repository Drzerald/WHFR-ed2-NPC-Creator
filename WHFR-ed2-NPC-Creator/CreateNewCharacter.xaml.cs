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
		Character character; //= new Character(0, 1);
		DataBaseController dBController = new DataBaseController();



		public CreateNewCharacter() {
			InitializeComponent();
			groupBox_Professions.DataContext = professions;
			listBox_Talents.DataContext = character;
			Race_GroupBox.DataContext = professions;
			groupBox.DataContext = character;
		}


		private void RollCharacteristic_Button(object sender, RoutedEventArgs e) {
			if(character != null) {
				character.rerollCharateristics();
			}
		}

		private void ListBox_Talents_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			textBlock_TalentDescription.DataContext = listBox_Talents.SelectedItem;
		}

		private void ComboBox_Profession_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			buildCharacter();
		}

		private void buildCharacter() {
			string name = "";
			if (character != null) {
				if (character.Name != "") {
					name = character.Name;
				}
			}
			List<Profession> characterProfessions = new List<Profession>();
			if (comboBox_Profession0.SelectedItem != null) {
				characterProfessions.Add((Profession)comboBox_Profession0.SelectedItem);
			}
			if (comboBox_Profession1.SelectedItem != null) {
				characterProfessions.Add((Profession)comboBox_Profession1.SelectedItem);
			}
			if (comboBox_Profession2.SelectedItem != null) {
				characterProfessions.Add((Profession)comboBox_Profession2.SelectedItem);
			}
			if (comboBox_Race.SelectedItem != null && characterProfessions.Count > 0) {
				character = new Character((Race)comboBox_Race.SelectedItem, characterProfessions);
				if(name != "") { character.Name = name; }
				groupBox.DataContext = character;
				listBox_Talents.DataContext = character;
				listBox_Skills.DataContext = character;
				textBox_CharacterName.DataContext = character;
			} else {
				character = null;
			}
		}

		private void SaveCharacter_button_Click(object sender, RoutedEventArgs e) {
			if(character != null && character.Name != "") {
				if (character.Name != new Character(0, 0).Name) {
					dBController.SaveToDataBase(character);
					this.Close();
					return;
				} else {
					MessageBox.Show(String.Format("Imie '{0}' jest niedozwolone", new Character(0, 0).Name));
				}
			} else {
				MessageBox.Show("Imie jest wymagane");
			}
		}

		private void RandomizeWouds_Button_Click(object sender, RoutedEventArgs e) {
			professions.NewWouds();
		}

		private void RandomizeName_Button_Click(object sender, RoutedEventArgs e) {
			System.Diagnostics.Debug.WriteLine("New Name: " + character.randomizeName());
			textBox_CharacterName.DataContext = character;
		}
	}
}