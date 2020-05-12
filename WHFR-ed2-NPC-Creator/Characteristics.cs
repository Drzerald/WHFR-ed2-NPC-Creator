using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator
{
    class Characteristics
    {
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
		private int attacks { get; set; } = 1;
		private int wounds { get; set; } = 0;
		private int strengthBonus { get; set;}
		private int toughnessBonus { get; set;}
		private int mobility { get; set; } = 0;
		private int magick { get; set; } = 0;
		private int insanityPoints { get; set; } = 0;
		private int fatePoints { get; set; } = 0;

		//GETTER SETTER:
		//Main
		public int Strength {
			get { return strength; }
			set { strength = value;
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

		public void setMainCharacteristics(int[] arrayOfMainCharacteristics ) {
			if(arrayOfMainCharacteristics.Length == 8) {
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
		
	}
}
