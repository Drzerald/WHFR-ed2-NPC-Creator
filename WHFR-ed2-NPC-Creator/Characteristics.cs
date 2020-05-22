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

		public int[] getSideCharacteristics() {
			int[] x = { Attacks, Wounds, StrengthBonus, ToughnessBonus, Mobility, Magick, InsanityPoints, FatePoints };
			return x;
		}


		public void DebugPrint() {

			string line = "| WS| BS| S | T |Agi|Int| WP|Fel|";
			System.Diagnostics.Debug.WriteLine(line);
			line = "| {0:d2}| {1:d2}| {2:d2}| {3:d2}| {4:d2}| {5:d2}| {6:d2}| {7:d2}|";
			int[] array = getMainCharacteristics();
			line = String.Format(line, array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]);
			System.Diagnostics.Debug.WriteLine(line);

			line = "| A | W | SB| TB|Mob|Mag| IP| FP|";
			System.Diagnostics.Debug.WriteLine(line);
			line = "| {0:d2}| {1:d2}| {2:d2}| {3:d2}| {4:d2}| {5:d2}| {6:d2}| {7:d2}|";
			array = getSideCharacteristics();
			line = String.Format(line, array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]);
			System.Diagnostics.Debug.WriteLine(line);
		}


	}
}
