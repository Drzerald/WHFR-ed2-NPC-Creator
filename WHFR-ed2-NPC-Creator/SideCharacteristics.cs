using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHFR_ed2_NPC_Creator
{
	class SideCharacteristics {
		public int attacks;
		public int wounds;
		public int strengthBonus;
		public int toughnessBonus;
		public int mobility;
		public int magick;
		public int insanityPoints;
		public int fatePoints;

		public SideCharacteristics(){
			setCharacteristics(0,0,0,0,0,0,0,0);
		}


		public SideCharacteristics(int Att, int W, int SB, int TB,int M,int Mag,int IP,int FP) {
			setCharacteristics(Att, W, SB, TB, M, Mag, IP, FP);
		}

		void setCharacteristics(int Att, int W, int SB, int TB, int M, int Mag, int IP, int FP) {
			attacks = Att;
			wounds = W;
			strengthBonus = SB;
			toughnessBonus = TB;
			mobility = M;
			magick = Mag;
			insanityPoints = IP;
			fatePoints = FP;
		}
	}
}
