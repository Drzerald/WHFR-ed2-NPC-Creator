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
		public MainWindow() {
			InitializeComponent();
		}

		private MainCharacteristics characteristicsFromRolls = new MainCharacteristics();
		private MainCharacteristics characteristicsFromProffesion = new MainCharacteristics();
		private MainCharacteristics characteristicsSum = new MainCharacteristics();

		



		private void Button_Click(object sender, RoutedEventArgs e) {

			SkillList skillList = new SkillList();
			skillList.advanceSkill(1);
			skillList.advanceSkill("m1");
			skillList.advanceSkill(2);
			skillList.advanceSkill(2);
			skillList.advanceSkill(0);
			skillList.advanceSkill(4);
			skillList.advanceSkill(4);
			skillList.advanceSkill(4);
			skillList.advanceSkill(4);
			skillList.advanceSkill(4);
			skillList.advanceSkill("Brzuchomówstwo");
			foreach (Skill skill in skillList.skillsArray) {
				System.Diagnostics.Debug.WriteLine( skill.ToString());
			}

			
			//DieRoller die = new DieRoller();
			//textBox.Text = "";

			//SkillList skills = new SkillList();
			//Skill skill = new Skill("Climbing");
			//skills.addSkill(skill);
			//skills.addSkill(skill);

			//for (int i = 0; i < 50; i++) {
			//	System.Diagnostics.Debug.Write(die.rollD10().ToString() + ", ");
			//}
			//System.Diagnostics.Debug.Write("\n");

			//System.Diagnostics.Debug.WriteLine(skill.ToString());
			//skills.advanceSkill("Climbing");
			//System.Diagnostics.Debug.WriteLine(skill.ToString());
			//textBox.Text = skill.ToString();
		}



		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


	}
}
