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

		private void Button_Click(object sender, RoutedEventArgs e) {
			DieRoller die = new DieRoller();
			textBox.Text = "";

			SkillList skills = new SkillList();
			Skill skill = new Skill("Climbing");
			skills.addSkill(skill);
			skills.addSkill(skill);

			for (int i = 0; i < 50; i++) {
				System.Diagnostics.Debug.Write(die.rollD10().ToString() + ", ");
			}
			System.Diagnostics.Debug.Write("\n");

			//System.Diagnostics.Debug.WriteLine(skill.ToString());
			//skills.advanceSkill("Climbing");
			//System.Diagnostics.Debug.WriteLine(skill.ToString());
			//textBox.Text = skill.ToString();
		}

	}
}
