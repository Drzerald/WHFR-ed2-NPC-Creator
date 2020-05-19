using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator
{
	class Characteristics {
		//Main 
		public int WeaponSkills { get; set; } = 0;
		public int BaliscticSkills { get; set; } = 0;
		private int strength = 0;
		private int toughness = 0;
		public int Agility { get; set; } = 0;
		public int Intelligence { get; set; } = 0;
		public int WillPower { get; set; } = 0;
		public int Fellowship { get; set; } = 0;

		//Side
		public int Attacks { get; set; } = 1;
		public int Wounds { get; set; } = 0;
		private int strengthBonus { get; set; }
		private int toughnessBonus { get; set; }
		public int Mobility { get; set; } = 0;
		public int Magick { get; set; } = 0;
		public int InsanityPoints { get; set; } = 0;
		public int FatePoints { get; set; } = 0;

		//GETTER SETTER:
		//Main
		public int Strength {
			get { return strength; }
			set {
				strength = value;
				strengthBonus = (int)Math.Floor(strength / 10d);
			}
		}


		public int Toughness {
			get { return toughness; }
			set {
				toughness = value;
				toughnessBonus = (int)Math.Floor(toughness / 10d);
			}
		}

		public int StrengthBonus {
			get { return strengthBonus; }
		}
		public int ToughnessBonus {
			get { return toughnessBonus; }
		}


		public int[] getMainCharacteristics() {
			int[] x = { WeaponSkills, BaliscticSkills, Strength, Toughness, Agility, Intelligence, WillPower, Fellowship };
			return x;
		}

		public void setMainCharacteristics(int[] arrayOfMainCharacteristics) {
			if (arrayOfMainCharacteristics.Length == 8) {
				WeaponSkills = arrayOfMainCharacteristics[0];
				BaliscticSkills = arrayOfMainCharacteristics[1];
				Strength = arrayOfMainCharacteristics[2];
				Toughness = arrayOfMainCharacteristics[3];
				Agility = arrayOfMainCharacteristics[4];
				Intelligence = arrayOfMainCharacteristics[5];
				WillPower = arrayOfMainCharacteristics[6];
				Fellowship = arrayOfMainCharacteristics[7];
			}
		}


		public void DebugPrint() {
			System.Diagnostics.Debug.WriteLine("|WS |BS |S  |T  |AGI|INT|WP |FEL|");
			System.Diagnostics.Debug.Write("|" + WeaponSkills.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + BaliscticSkills.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + Strength.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + Toughness.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + Agility.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + Intelligence.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + WillPower.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + Fellowship.ToString("D2") + " |\n");
			System.Diagnostics.Debug.WriteLine("| A | W |SB |TB |MOB|MAG|IP |FP |");
			System.Diagnostics.Debug.Write("|" + Attacks.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + Wounds.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + StrengthBonus.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + ToughnessBonus.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + Mobility.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + Magick.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + InsanityPoints.ToString("D2") + " ");
			System.Diagnostics.Debug.Write("|" + FatePoints.ToString("D2") + " |\n\n");
		}


	}
}
