using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator
{
	class Characteristics : INotifyPropertyChanged{
		public event PropertyChangedEventHandler PropertyChanged;
		public event Action OnCharacteristicChange = delegate { };
		//Main 
		private int weaponSkills = 0;
		private int balisticSkills = 0;
		private int strength = 0;
		private int toughness = 0;
		private int agility = 0;
		private int intelligence = 0;
		private int willPower = 0;
		private int fellowship = 0;

		//Side
		private int attacks = 0;
		private int wounds = 0;
		public int StrengthBonus { get; private set; }
		public int ToughnessBonus { get; private set; } 
		private int mobility = 0;
		private int magick = 0;
		private int insanityPoints = 0;
		private int fatePoints = 0;

		//GETTER SETTER:
		//Main
		public int WeaponSkills{
			get { return weaponSkills; }
			set {
				weaponSkills = value;
				OnCharacteristicChange();
				OnPropertyChanged("WeaponSkills");
			}
		}

		public int BalisticSkills{
			get { return balisticSkills; }
			set {
				balisticSkills = value;
				OnCharacteristicChange();
				OnPropertyChanged();
			}
		}

		public int Strength {
			get { return strength; }
			set {
				strength = value;
				StrengthBonus = (int)Math.Floor(strength / 10d);
				OnCharacteristicChange();
				OnPropertyChanged();
			}
		}

		public int Toughness {
			get { return toughness; }
			set {
				toughness = value;
				ToughnessBonus = (int)Math.Floor(toughness / 10d);
				OnCharacteristicChange();
				OnPropertyChanged();
			}
		}

		public int Agility {
			get { return agility; }
			set {
				agility = value;
				OnCharacteristicChange();
				OnPropertyChanged();
			}
		} 

		public int Intelligence {
			get { return intelligence; }
			set {
				intelligence = value;
				OnCharacteristicChange();
				OnPropertyChanged();
			}
		}

		public int WillPower {
			get { return willPower; }
			set {
				willPower = value;
				OnCharacteristicChange();
				OnPropertyChanged();
			}
		}

		public int Fellowship {
			get { return fellowship; }
			set {
				fellowship = value;
				OnCharacteristicChange();
				OnPropertyChanged();
			}
		}

		//Side
		public int Attacks {
			get { return attacks; }
			set {
				attacks = value;
				OnCharacteristicChange();
				//OnPropertyChanged();
			}
		}
		public int Wounds {
			get { return wounds; }
			set {
				wounds = value;
				OnCharacteristicChange();
				//OnPropertyChanged();
			}
		}

		public int Mobility {
			get { return mobility; }
			set {
				mobility = value;
				OnCharacteristicChange();
				//OnPropertyChanged();
			}
		}

		public int Magick {
			get { return magick; }
			set {
				magick = value;
				OnCharacteristicChange();
				//OnPropertyChanged();
			}
		}

		public int InsanityPoints {
			get { return insanityPoints; }
			set {
				insanityPoints = value;
				OnCharacteristicChange();
				//OnPropertyChanged();
			}
		}

		public int FatePoints {
			get { return fatePoints; }
			set {
				fatePoints = value;
				OnCharacteristicChange();
				//OnPropertyChanged();
			}
		}


		public int[] getMainCharacteristics() {
			int[] x = { WeaponSkills, BalisticSkills, Strength, Toughness, Agility, Intelligence, WillPower, Fellowship };
			return x;
		}

		public void setMainCharacteristics(int[] arrayOfMainCharacteristics) {
			if (arrayOfMainCharacteristics.Length == 8) {
				WeaponSkills = arrayOfMainCharacteristics[0];
				BalisticSkills = arrayOfMainCharacteristics[1];
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

		protected void OnPropertyChanged([CallerMemberName] string name = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}


	}
}
